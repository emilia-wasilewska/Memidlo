using Memidlo.Web.Data;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Memidlo.Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditUserModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly AuthDbContext authDbContext;

        [BindProperty]
        public UserVM UserVM { get; set; }

       
        public EditUserModel(IUserRepository userRepository, AuthDbContext authDbContext)
        {
            this.userRepository = userRepository;
            this.authDbContext = authDbContext;
        }
        public async Task OnGet(Guid id)
        {
            var selectedUser = await userRepository.GetAsync(id);

            if (selectedUser != null)
            {
                UserVM = new UserVM
                {
                    Id = Guid.Parse(selectedUser.Id),
                    UserName = selectedUser.UserName,
                    Email = selectedUser.Email,
                    Password = selectedUser.PasswordHash
                };
            }
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            var userForEdition = UserVM;

            if (userForEdition != null)
            {
                userForEdition.Id = UserVM.Id;
                userForEdition.UserName = UserVM.UserName;
                userForEdition.Email = UserVM.Email;
            }

            if (UserVM.Password != null)
            {
                userForEdition.Password = new PasswordHasher<UserVM>().HashPassword(userForEdition, UserVM.Password);
            }

            var updateResult = await userRepository.UpdateAsync(userForEdition);

            if (updateResult)
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Zmiany zapisane pomyślnie"
                };

                return Page();
            }

            ViewData["Notification"] = new NotificationVM
            {
                Type = Enums.NotificationType.Error,
                Message = "Nie udalo się zapisać zmian"
            };

            return Page();

        }

    }

}

