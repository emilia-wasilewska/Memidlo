using Memidlo.Web.Models.View;
using Microsoft.AspNetCore.Identity;

namespace Memidlo.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllAsync();

        Task<IEnumerable<IdentityUser>> GetByIds(IEnumerable<string> ids);
        Task<IdentityUser> GetAsync(Guid id);
        Task<IdentityUser> GetAsync(string id);
        Task<bool> AddAsync(IdentityUser identityUser, string password, List<string> roles);

        Task<bool> UpdateAsync(UserVM user);
        Task DeleteAsync(Guid id);
    }
}
