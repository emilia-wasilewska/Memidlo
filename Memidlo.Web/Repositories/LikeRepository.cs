using Memidlo.Web.Data;
using Memidlo.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidlo.Web.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly MemidloDbContext memidloDbContext;

        public LikeRepository(MemidloDbContext memidloDbContext)
        {
            this.memidloDbContext = memidloDbContext;
        }

        public async Task AddLikeForMem(int memId, Guid userId)
        {
            var like = new Like
            {
                MemId = memId,
                UserId = userId
            };

            await memidloDbContext.Likes.AddAsync(like);
            await memidloDbContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalLikesForMem(int id)
        {
            return await memidloDbContext.Likes.CountAsync(x => x.MemId == id);
        }

        public async Task<IEnumerable<Like>> GetLikesForMem(int memId)
        {
            return await memidloDbContext.Likes.Where(x=>x.MemId== memId).ToListAsync();
        }

        public async Task<List<Like>> GetAllLikes()
        {
            return await memidloDbContext.Likes.ToListAsync();
        }
    }
}
