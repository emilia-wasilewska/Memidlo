using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidlo.Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public List<UserVM> Users { get; set; }

        [BindProperty]
        public Guid SelectedUserId { get; set; }

        [BindProperty]
        public UserVM User { get; set; }

        public UsersModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            var users = await userRepository.GetAllAsync();
            Users = new List<UserVM>();

            foreach (var user in users)
            {
                Users.Add(new UserVM()
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.PasswordHash
                });
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var identityUser = new IdentityUser
            {
                UserName = User.UserName,
                Email = User.Email
            };

            var roles = new List<string> { "User" };

            if(User.IsAdmin)
            {
                roles.Add("Admin");
            }

            var result = await userRepository.AddAsync(identityUser, User.Password, roles);

            if(result)
            {
                return RedirectToPage("/Admin/Users");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            await userRepository.DeleteAsync(SelectedUserId);
            return RedirectToPage("/Admin/Users"); 
        }
    }
}
