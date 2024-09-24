using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace BlazorServerApp.Data.Configuration
{
    public class CustomConfiguration
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Define supported cultures
            var supportedCultures = new[] { "en-US", "fr-FR", "es-ES" };
            var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("en-US")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);
        }
    }
}
