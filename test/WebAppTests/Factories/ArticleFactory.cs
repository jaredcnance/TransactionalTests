using System.Collections.Generic;
using Bogus;
using WebApp.Articles;

namespace WebAppTests.Factories
{
    public static class ArticleFactory
    {
        public static List<Article> Create(int count = 1) 
            => new Faker<Article>()
                .RuleFor(m => m.Title, f => f.Lorem.Sentence())
                .Generate(count);
    }
}