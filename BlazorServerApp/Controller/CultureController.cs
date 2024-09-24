using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerApp.Controller
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CultureController : ControllerBase
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if(culture != null)
            {
                var requestCulture = new RequestCulture(culture, culture);
                var cookieName = CookieRequestCultureProvider.DefaultCookieName;
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);

                HttpContext.Response.Cookies.Append(cookieName, cookieValue);
            }

            return Redirect(redirectUri);
            //IRequestCultureFeature feature = HttpContext.Features.Get<IRequestCultureFeature>();
            //Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(new string[] { "en-US", "fr-FR" }.Where(s => s != feature.RequestCulture.Culture.Name).First())));
            //return Redirect("/local");
        }
    }
}
