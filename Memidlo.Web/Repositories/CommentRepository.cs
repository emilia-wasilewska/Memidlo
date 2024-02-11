using Memidlo.Web.Data;
using Memidlo.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidlo.Web.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MemidloDbContext memidloDbContext;

        public CommentRepository(MemidloDbContext memidloDbContext)
        {
            this.memidloDbContext = memidloDbContext;
        }
        public async Task<Comment> AddAsync(Comment comment)
        {
            await memidloDbContext.Comments.AddAsync(comment);
            await memidloDbContext.SaveChangesAsync();

            return comment;
        }

       
        public async Task<IEnumerable<Comment>> GetAllForMemAsync(int id)
        {
            return await memidloDbContext.Comments.Where(x => x.MemId == id).ToListAsync();
        }
    }
}
