using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Data;

namespace WebApp.Articles
{
    public class ArticleService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(AppDbContext dbContext, ILogger<ArticleService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Article>> GetAllAsync() 
        {
            try
            {
                var articles = await _dbContext.Articles.ToListAsync();
                return articles;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get all articles.");
                return new List<Article>();
            }
        }

        public async Task<Article> CreateAsync(Article article)
        {
            try
            {
                _dbContext.Articles.Add(article);
                await _dbContext.SaveChangesAsync();
                return article;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create article.");
                return null;
            }
        }
    }
}