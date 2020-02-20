using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Shared.Events.Posts
{
    public class PostAdded
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public List<string> Keywords { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}
