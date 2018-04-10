using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Articles;

namespace WebApp.Controllers
{
    [Route("api/articles")]
    public class ArticlesController : Controller
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() => Ok(await _articleService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Article article)
        {
            var newArticle = await _articleService.CreateAsync(article);

            if(newArticle == null)
                return BadRequest();

            var uri = new Uri("/articles/" + article.Id, UriKind.Relative);
            return Created(uri, article);
        }
    }
}