using Memidlo.Web.Data;
using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Memidlo.Web.Pages.Profile
{
    public class MyProfileModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly IMemRepository memRepository;
        private readonly UserManager<IdentityUser> userManager;


        [BindProperty]
        public UserVM UserVM { get; set; }

        [BindProperty]
        public List<Mem> Mems { get; set; }

        public MyProfileModel(IUserRepository userRepository, IMemRepository memRepository)
        {
            this.userRepository = userRepository;
            this.memRepository = memRepository;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var loggedUser = await userRepository.GetAsync(id);

            if (loggedUser != null)
            {
                UserVM = new UserVM
                {
                    Id = Guid.Parse(loggedUser.Id),
                    UserName = loggedUser.UserName,
                    Email = loggedUser.Email,
                    Password = loggedUser.PasswordHash
                };
            }

            Mems = (await memRepository.GetAllAsync())
                     .Where(x => x.Author == UserVM.UserName).OrderByDescending(x => x.PublishDate).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPost(string id)
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

                return RedirectToPage("/Profile/MyProfile", new {id = id});
            }

            ViewData["Notification"] = new NotificationVM
            {
                Type = Enums.NotificationType.Error,
                Message = "Nie udalo się zapisać zmian"
            };

            return RedirectToPage("/Profile/MyProfile", new { id = id });
        }
    }
}