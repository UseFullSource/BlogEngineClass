using BlogEngine.PostManagement.Models.Posts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.PostManagement.Models.Posts.Contracts
{
    public interface IPostRepository
    {
        Post Create(Post post);
        Post Get(string id);
        void Remove(Post post);
        void Remove(string id);
        void Update(string id, Post post);
        List<Post> Get();
    }
}
