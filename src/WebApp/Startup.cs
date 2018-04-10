using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Articles;
using WebApp.Data;

namespace WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql("Host=localhost;Port=5432;Database=TransactionalTests;User ID=postgres;Password=postgres"),
                ServiceLifetime.Transient);
            services.AddScoped<ArticleService>();
        }

        public void Configure(IApplicationBuilder app, AppDbContext dbContext)
        {
            dbContext.Database.Migrate();
            app.UseMvc();
        }
    }
}
