using Microsoft.EntityFrameworkCore;
using WebApp.Articles;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        public DbSet<Article> Articles { get; set; }
    }
}