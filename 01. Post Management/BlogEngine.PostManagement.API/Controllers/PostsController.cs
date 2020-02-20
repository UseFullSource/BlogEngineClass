using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.PostManagement.Models.Posts.Contracts;
using BlogEngine.PostManagement.Models.Posts.Entities;
using BlogEngine.Shared.Events.Posts;
using BlogEngine.Shared.Masstransit;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngine.PostManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public PostsController(IPostRepository postRepository, IEventDispatcher eventDispatcher)
        {
            _postRepository = postRepository;
            _eventDispatcher = eventDispatcher;
        }

        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return _postRepository.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetPost")]
        public ActionResult<Post> Get(string id)
        {
            var book = _postRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Post> Create(Post post)
        {
            _postRepository.Create(post);
            _eventDispatcher.Dispatch(new PostAdded
            {
                Author = post.Author,
                Body = post.Body,
                Category = post.Category,
                Id = post.Id,
                Keywords = post.Keywords,
                ShortDescription = post.ShortDescription,
                Title = post.Title
            });
            return CreatedAtRoute("GetPost", new { id = post.Id.ToString() }, post);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Post post)
        {
            var curent = _postRepository.Get(id);

            if (curent == null)
            {
                return NotFound();
            }

            _postRepository.Update(id, post);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var curent = _postRepository.Get(id);

            if (curent == null)
            {
                return NotFound();
            }

            _postRepository.Remove(curent.Id);

            return NoContent();
        }
    }
}
