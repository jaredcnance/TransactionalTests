using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using WebApp.Data;

namespace WebAppTests.Fixtures
{
    public class WebFixture<TStartup> : Fixture, IDisposable where TStartup : class
    {
        private readonly TestServer _server;
        private readonly IServiceProvider _services;

        protected readonly HttpClient Client;
        protected AppDbContext DbContext { get; }
        protected IDbContextTransaction Transaction { get; }

        public WebFixture()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();

            _server = new TestServer(builder);
            _services = _server.Host.Services;

            Client = _server.CreateClient();
            DbContext = GetService<AppDbContext>();
            Transaction = DbContext.Database.BeginTransaction();
        }

        protected T GetService<T>() => (T)_services.GetService(typeof(T));

        protected StringContent Serialize(object obj) 
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }
        }
    }
}