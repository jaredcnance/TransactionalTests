using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Articles;
using WebApp.Data;

namespace WebAppTests.Stubs
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql("Host=localhost;Port=5432;Database=TransactionalTestsTests;User ID=postgres;Password=postgres"),
                ServiceLifetime.Singleton);
            services.AddScoped<ArticleService>();
        }

        public void Configure(IApplicationBuilder app, AppDbContext dbContext)
        {
            dbContext.Database.Migrate();
            app.UseMvc();
        }
    }
}