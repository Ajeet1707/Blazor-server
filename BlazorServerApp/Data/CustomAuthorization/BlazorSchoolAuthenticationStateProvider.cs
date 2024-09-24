using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorServerApp.Data.CustomAuthorization
{
    public class BlazorSchoolAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        public User CurrentUser { get; private set; } = new();
        private readonly BlazorSchoolUserService _blazorSchoolUserService;

        public BlazorSchoolAuthenticationStateProvider(BlazorSchoolUserService blazorSchoolUserService)
        {
            _blazorSchoolUserService = blazorSchoolUserService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await _blazorSchoolUserService.FindUserFromDatabaseAsync(username, password);
            CurrentUser = user;

            if (user is not null)
            {
                principal = user.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = await _blazorSchoolUserService.FetchUserFromBrowserAsync();

            if (user is not null)
            {
                var authenticatedUser = await _blazorSchoolUserService.FindUserFromDatabaseAsync(user.Username, user.Password);
                CurrentUser = authenticatedUser;

                if (authenticatedUser is not null)
                {
                    principal = authenticatedUser.ToClaimsPrincipal();
                }
            }

            return new(principal);
        }

        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                CurrentUser = User.FromClaimsPrincipal(authenticationState.User);
            }
        }

        public async Task LogoutAsync()
        {
            await _blazorSchoolUserService.ClearBrowserUserDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
    }
}
