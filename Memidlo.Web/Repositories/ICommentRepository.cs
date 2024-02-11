using Memidlo.Web.Models.Domain;

namespace Memidlo.Web.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddAsync(Comment comment);

        Task<IEnumerable<Comment>> GetAllForMemAsync(int id);

       
    }
}
