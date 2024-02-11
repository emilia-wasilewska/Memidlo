using Memidlo.Web.Data;
using Memidlo.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidlo.Web.Repositories
{
    public class TagRespository : ITagRespository
    {
        private readonly MemidloDbContext memidloDbContext;

        public TagRespository(MemidloDbContext memidloDbContext)
        {
            this.memidloDbContext = memidloDbContext;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags =await memidloDbContext.Tags.ToListAsync();

            return tags.DistinctBy(x => x.Name.ToLower()); ;
        }
    }
}
