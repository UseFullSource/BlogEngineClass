using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.PostManagement.Models.Posts.Entities
{
    public class Post
    {
        public Post()
        {
            Id = ObjectId.GenerateNewId().ToString();

        }
        public string Id { get; private set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public List<string> Keywords { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string Category { get; set; }
    }
}
