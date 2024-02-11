using Memidlo.Web.Models.Domain;

namespace Memidlo.Web.Repositories
{
    public interface ITagRespository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
