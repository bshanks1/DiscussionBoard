using DiscussionBoard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscussionBoard.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelBuilder.Entity<Forum>()
                .HasMany(f => f.Threads);

        }

        public DbSet<DiscussionThread> DiscussionThread { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
