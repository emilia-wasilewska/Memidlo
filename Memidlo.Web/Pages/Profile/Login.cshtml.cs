using Memidlo.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidlo.Web.Pages.Profile
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public LoginVM Login { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string ReturnUrl)
        {
            var signInResult = await signInManager.PasswordSignInAsync(Login.UserName, Login.Password, false, false);

            if(signInResult.Succeeded) 
            {
                if(!string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToPage(ReturnUrl);
                }
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Logowanie nie powiodlo się"
                };

                return Page();
            }
        }
    }
}
