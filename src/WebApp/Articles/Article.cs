using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Articles
{
    [Table("articles")]
    public class Article
    {
        [Column("id")] public int Id { get; set; }
        [Column("Title")] public string Title { get; set; }
    }
}