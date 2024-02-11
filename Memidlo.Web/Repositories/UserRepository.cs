using Memidlo.Web.Data;
using Memidlo.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Memidlo.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserRepository(AuthDbContext authDbContext, UserManager<IdentityUser> userManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
        {
            var users = await authDbContext.Users.ToListAsync();
            var superAdminUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.UserName.Contains("superadmin"));

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }

        public async Task<bool> AddAsync(IdentityUser identityUser, string password, List<string> roles)
        {
            var identityResult = await userManager.CreateAsync(identityUser, password);

            if(identityResult.Succeeded) 
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                if(identityResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateAsync(UserVM user)
        {
            var userForEdition = await GetAsync(user.Id);
            if (userForEdition != null) 
            {
                userForEdition.UserName= user.UserName;
                userForEdition.Email= user.Email;
                userForEdition.PasswordHash = user.Password;
            }

           var updateResult = await userManager.UpdateAsync(userForEdition);

            if(updateResult.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task DeleteAsync(Guid id)
        {
            var userForDeletion = await GetAsync(id);

            if (userForDeletion != null)
            {
                await userManager.DeleteAsync(userForDeletion);
            }
        }

        public async Task<IdentityUser> GetAsync(Guid id)
        {
            return await userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IdentityUser> GetAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<IdentityUser>> GetByIds(IEnumerable<string> ids)
        {
            return await authDbContext.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
