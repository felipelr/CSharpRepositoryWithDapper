using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Blog.Models
{
    [Table("[Post]")]
    public class Post
    {
        public Post() => Category = new Category();
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public Category Category { get; set; }
    }
}