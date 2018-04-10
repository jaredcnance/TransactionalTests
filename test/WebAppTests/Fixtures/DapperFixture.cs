using System;
using System.Data;
using Npgsql;

namespace WebAppTests.Fixtures
{
    public class DapperFixture : Fixture, IDisposable
    {
        public DapperFixture()
        {
            Connection = new NpgsqlConnection(Configuration["DbConnection"]);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        protected IDbConnection Connection { get; }
        protected IDbTransaction Transaction { get; }

        public void Dispose()
        {
            if(Transaction != null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
                Connection.Dispose();
            }
        }
    }
}