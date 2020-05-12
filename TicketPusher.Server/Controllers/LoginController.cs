using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;
using System.Threading.Tasks;

namespace BlazorServerWithOkta.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("Login")]
        public IActionResult Login([FromQuery]string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            return Challenge();
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout([FromQuery]string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return new SignOutResult(
                new[]
                {
                        OktaDefaults.MvcAuthenticationScheme,
                        CookieAuthenticationDefaults.AuthenticationScheme,
                },
                new AuthenticationProperties { RedirectUri = returnUrl ?? Url.Content("~/") });
        }

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public IActionResult PostLogout([FromQuery]string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            returnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.Content("~/");

            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            }

            return new SignOutResult(
                new[]
                {
                        OktaDefaults.MvcAuthenticationScheme,
                        CookieAuthenticationDefaults.AuthenticationScheme,
                },
                new AuthenticationProperties { RedirectUri = returnUrl ?? Url.Content("~/") });
        }

    }
}