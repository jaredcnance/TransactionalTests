using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApp.Data;

namespace WebAppTests.Fixtures
{
    public class DbContextFixture : Fixture, IDisposable
    {
        protected IDbContextTransaction Transaction { get; }
        protected AppDbContext DbContext { get; }

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(Configuration["DbConnection"])
                .Options;

            DbContext = new AppDbContext(options);
            Transaction = DbContext.Database.BeginTransaction();
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