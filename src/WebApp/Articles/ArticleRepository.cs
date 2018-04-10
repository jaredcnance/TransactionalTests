using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace WebApp.Articles
{
    public class ArticleRepository
    {
        private readonly IDbConnection _dbConnection;

        public ArticleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Article>> GetArticlesAsync() => (await _dbConnection.GetAllAsync<Article>()).ToList();
        //(await _dbConnection.QueryAsync<Article>("select * from 'Articles'")).ToList();

        public async Task<int> CreateArticleAsync(Article article) => (await _dbConnection.InsertAsync<Article>(article));
    }
}