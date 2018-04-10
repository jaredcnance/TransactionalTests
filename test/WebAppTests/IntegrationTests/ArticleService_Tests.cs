using System.Threading.Tasks;
using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using WebApp.Articles;
using WebAppTests.Factories;
using WebAppTests.Fixtures;
using Xunit;

namespace WebAppTests.IntegrationTests
{
    public class ArticleService_Tests : DbContextFixture
    {
        private Faker _faker = new Faker();
        
        [Fact]
        public async Task GetAllAsync_Returns_All_Articles()
        {
            // arrange
            var expectedArticleCount = _faker.Random.Int(5, 15);
            var expectedArticles = ArticleFactory.Create(expectedArticleCount);
            DbContext.Articles.AddRange(expectedArticles);
            await DbContext.SaveChangesAsync();

            var service = GetService();

            // act
            var actualArticles = await service.GetAllAsync();

            // assert
            Assert.Equal(expectedArticleCount, actualArticles.Count);
        }

        private ArticleService GetService() 
            => new ArticleService(DbContext, new Mock<ILogger<ArticleService>>().Object);
    }
}