using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.Articles;
using WebAppTests.Factories;
using WebAppTests.Fixtures;
using WebAppTests.Stubs;
using Xunit;

namespace WebAppTests
{
    public class Articles_Tests : WebFixture<TestStartup>
    {
        [Fact]
        public async Task Can_Post_Article()
        {
            // arrange
            var article = ArticleFactory.Create(1).Single();
            var json = Serialize(article);

            // act
            var result = await Client.PostAsync("/api/articles", json);

            // assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Can_Get_Article()
        {
            // arrange
            var article = ArticleFactory.Create(1).Single();
            DbContext.Articles.Add(article);
            await DbContext.SaveChangesAsync();

            // act
            var result = await Client.GetAsync("/api/articles");

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var articles = await DeserializeAsync<List<Article>>(result);

            Assert.Equal(1, articles.Count);
            Assert.Equal(article.Id, articles.Single().Id);
            Assert.Equal(article.Title, articles.Single().Title);
        }
    }
}
