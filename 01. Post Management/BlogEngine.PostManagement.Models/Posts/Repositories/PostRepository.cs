using BlogEngine.PostManagement.Models.Posts.Configurations;
using BlogEngine.PostManagement.Models.Posts.Contracts;
using BlogEngine.PostManagement.Models.Posts.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.PostManagement.Models.Posts.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _posts;

        public PostRepository(IMongoDbConfiguration settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _posts = database.GetCollection<Post>(settings.PostCollectionName);
        }
        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public Post Get(string id)
        {
            return _posts.Find(p => p.Id == id).FirstOrDefault();
        }

        public List<Post> Get()
        {

            return _posts.Find(p => true).ToList();
        }

        public void Remove(Post post)
        {
            _posts.DeleteOne(p => p.Id == post.Id);
        }

        public void Remove(string id)
        {
            _posts.DeleteOne(p => p.Id == id);
        }

        public void Update(string id, Post post)
        {
            _posts.ReplaceOne(p => p.Id == id, post);
        }
    }
}
