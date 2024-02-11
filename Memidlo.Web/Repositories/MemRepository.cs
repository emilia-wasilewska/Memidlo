using Memidlo.Web.Data;
using Memidlo.Web.Models.Domain;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Linq;

namespace Memidlo.Web.Repositories
{
    public class MemRepository : IMemRepository
    {
        private readonly MemidloDbContext memidloDbContext;

        public MemRepository(MemidloDbContext memidloDbContext)
        {
            this.memidloDbContext = memidloDbContext;
        }
        public async Task<Mem> AddAsync(Mem mem)
        {
            await memidloDbContext.Mems.AddAsync(mem);
            await memidloDbContext.SaveChangesAsync();
            return mem;
        }

        public async Task<IEnumerable<Mem>> GetAllAsync()
        {
            return await memidloDbContext.Mems.Include(nameof(Mem.Tags)).Include(nameof(Mem.Likes)).ToListAsync();
        }

        public async Task<IEnumerable<Mem>> GetAllAsync(string tagName)
        {
            return await memidloDbContext.Mems.Include(nameof(Mem.Tags)).Where(x => x.Tags.Any(x => x.Name == tagName)).ToListAsync();
        }

        public async Task<Mem> GetAsync(int id)
        {
            return await memidloDbContext.Mems.Include(nameof(Mem.Tags)).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Mem> MoveToMainAsync(int memID)
        {
            var existingMem = await memidloDbContext.Mems.FirstOrDefaultAsync(x => x.Id == memID);
            if (existingMem != null)
            {
                existingMem.Main = true;
                existingMem.PublishDate = DateTime.Now;
            }

            await memidloDbContext.SaveChangesAsync();

            return existingMem;
        }

        public async Task<Mem> UpdateAsync(Mem mem)
        {
            var existingMem = await memidloDbContext.Mems.Include(nameof(Mem.Tags)).FirstOrDefaultAsync(x => x.Id == mem.Id);

            if (existingMem != null)
            {
                existingMem.PageTitle = mem.PageTitle;
                existingMem.Heading = mem.Heading;
                existingMem.Content = mem.Content;
                existingMem.FeaturedImageUrl = mem.FeaturedImageUrl;
                existingMem.Author = mem.Author;
                existingMem.Main = mem.Main;
                existingMem.PublishDate = mem.PublishDate;

                if (mem.Tags != null && mem.Tags.Any())
                {
                    memidloDbContext.Tags.RemoveRange(existingMem.Tags);
                }

                mem.Tags.ToList().ForEach(x => x.MemId = existingMem.Id);
                await memidloDbContext.Tags.AddRangeAsync(mem.Tags);
            }

            await memidloDbContext.SaveChangesAsync();
            return existingMem;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingMem = await memidloDbContext.Mems.FindAsync(id);

            if (existingMem != null)
            {
                memidloDbContext.Mems.Remove(existingMem);
                await memidloDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> CountMainAsync()
        {
            return await memidloDbContext.Mems.CountAsync(x => x.Main);
        }

        public async Task<int> CountWaitingRoomAsync()
        {
            return await memidloDbContext.Mems.CountAsync(x => !x.Main);
        }

        public async Task<int> CountTaggedAsync(string tagName)
        {
            return (await GetAllAsync(tagName)).Count();
        }

        public async Task<Mem?> GetRandom()
        {
            return await memidloDbContext.Mems
                .Include(x => x.Tags)
                .Include(x => x.Likes)
                .OrderBy(x=> Guid.NewGuid())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Mem>> GetPaginated(int pageSize, int pageNumber, bool? isMain = null)
        {
            var baseQuery = memidloDbContext.Mems.Include(nameof(Mem.Tags)).Include(nameof(Mem.Likes));

            if(isMain == true) baseQuery = baseQuery.Where(x => x.Main == isMain.Value).OrderByDescending(x => x.PublishDate);
            else baseQuery = baseQuery.Where(x => x.Main == isMain.Value);

            return await baseQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
