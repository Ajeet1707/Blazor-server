using Microsoft.JSInterop;

namespace BlazorServerApp.Data
{
    public class DontNetCode
    {
        public string SomeProperty { get; set; } = "Initial Property Value";

        [JSInvokable] // This attribute makes the method callable from JavaScript
        public Task<string> DotNetMethod(string message)
        {
            Console.WriteLine($"Received message from JavaScript: {message}");
            return Task.FromResult("Acknowledged by .NET!");
        }

    }
}
