using System.Threading.Tasks;
using Bogus;
using Dapper;
using Dapper.Contrib.Extensions;
using WebApp.Articles;
using WebAppTests.Factories;
using WebAppTests.Fixtures;
using Xunit;

namespace WebAppTests.IntegrationTests
{
    public class ArticleRepository_Tests : DapperFixture
    {
        private Faker _faker = new Faker();
        
        [Fact]
        public async Task GetArticlesAsync_Returns_Articles()
        {
            // arrange
            var expectedArticleCount = _faker.Random.Int(5, 15);
            var expectedArticles = ArticleFactory.Create(expectedArticleCount);
            foreach(var article in expectedArticles)
                await Connection.InsertAsync<Article>(article);
            
            var repo = new ArticleRepository(Connection);

            // act
            var articles = await repo.GetArticlesAsync();

            // assert
            Assert.Equal(expectedArticleCount, articles.Count);
        }
    }
}