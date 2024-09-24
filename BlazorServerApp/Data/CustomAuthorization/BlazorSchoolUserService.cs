using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace BlazorServerApp.Data.CustomAuthorization
{
    public class BlazorSchoolUserService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly string _blazorSchoolStorageKey = "blazorSchoolIdentity";

        public BlazorSchoolUserService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<User?> FindUserFromDatabaseAsync(string username, string password)
        {
            var userInDatabase = new User { 
            Username="ajeet",
            Password = "ajeet",
            Age = 2,
            Roles = { "View", "Admin"}
            };

            if (userInDatabase is not null)
            {
                await PersistUserToBrowserAsync(userInDatabase);
            }

            return userInDatabase;
        }

        public async Task PersistUserToBrowserAsync(User user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            await _protectedLocalStorage.SetAsync(_blazorSchoolStorageKey, userJson);
        }

        public async Task<User?> FetchUserFromBrowserAsync()
        {
            try // When Blazor Server is rendering at server side, there is no local storage. Therefore, put an empty try catch to avoid error
            {
                var fetchedUserResult = await _protectedLocalStorage.GetAsync<string>(_blazorSchoolStorageKey);

                if (fetchedUserResult.Success && !string.IsNullOrEmpty(fetchedUserResult.Value))
                {
                    var user = JsonConvert.DeserializeObject<User>(fetchedUserResult.Value);

                    return user;
                }
            }
            catch
            {

            }

            return null;
        }

        public async Task ClearBrowserUserDataAsync() => await _protectedLocalStorage.DeleteAsync(_blazorSchoolStorageKey);
    }
}
