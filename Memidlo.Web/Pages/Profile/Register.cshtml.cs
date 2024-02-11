using Memidlo.Web.Data;
using Memidlo.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidlo.Web.Pages.Profile
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly AuthDbContext authDbContext;

        [BindProperty]
        public UserVM RegisterUserVM { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, AuthDbContext authDbContext)
        {
            this.userManager = userManager;
            this.authDbContext = authDbContext;

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                var user = new IdentityUser
                {
                    UserName = RegisterUserVM.UserName,
                    Email = RegisterUserVM.Email
                };

                if (authDbContext.Users.FirstOrDefault(x => x.UserName == RegisterUserVM.UserName) != null)
                {
                    ViewData["Notification"] = new NotificationVM
                    {
                        Type = Enums.NotificationType.Error,
                        Message = "Nazwa użytkownika jest już zajęta"
                    };
                }
                else if (authDbContext.Users.FirstOrDefault(x => x.Email == RegisterUserVM.Email) != null)
                {
                    ViewData["Notification"] = new NotificationVM
                    {
                        Type = Enums.NotificationType.Error,
                        Message = "Ten email już jest zarejestrowany"
                    };
                }
                else
                {
                    var identityResult = await userManager.CreateAsync(user, RegisterUserVM.Password);

                    if (identityResult.Succeeded)
                    {
                        var addRolesResult = await userManager.AddToRoleAsync(user, "User");

                        if (addRolesResult.Succeeded)
                        {
                            ViewData["Notification"] = new NotificationVM
                            {
                                Type = Enums.NotificationType.Success,
                                Message = "Rejestracja przebiegla pomyślnie"
                            };

                            return Page();
                        }

                    }

                    ViewData["Notification"] = new NotificationVM
                    {
                        Type = Enums.NotificationType.Error,
                        Message = "Ups. Coś poszlo nie tak"
                    };
                }

                return Page();
            }
            else
            {
                return Page();
            }
        }



    }
}
