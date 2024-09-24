using BlazorServerApp.Data.Infrastructure;
using BlazorServerApp.Data.Models;

namespace BlazorServerApp.Data.Service
{
    public class DemoService : IDemoService
    {
        public string GetMessage()
        {
            return "Hello from MyService!";
        }

        public string UserData(UserData userData)
        {
            return "Form submitted Successfully";
        }

        public bool AuthenticateUser(LoginViewModel loginData)
        {
            if (loginData != null && loginData.UserName == "ajeet" && loginData.Password == "ajeet")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
