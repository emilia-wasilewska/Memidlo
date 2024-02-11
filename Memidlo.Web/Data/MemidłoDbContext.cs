using Memidlo.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidlo.Web.Data
{
    public class MemidloDbContext : DbContext
    {
        public MemidloDbContext(DbContextOptions<MemidloDbContext>options) : base(options)
        {
        }

        public DbSet<Mem> Mems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
