using BlazorServerApp.Data.Models;

namespace BlazorServerApp.Data.Infrastructure
{
    public interface IDemoService
    {
        string GetMessage();
        string UserData(UserData userData);

        bool AuthenticateUser(LoginViewModel loginData);
    }
}
