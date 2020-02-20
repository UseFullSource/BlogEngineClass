using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;


namespace BlogEngine.SearchEngine.API.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<PostSearchEngine> Get(string value)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
              .DefaultIndex("posts");

            var client = new ElasticClient(settings);




            var searchResponse = client.Search<PostSearchEngine>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                     .MultiMatch(m => m
                        .Fields(
                            f => f.Field($"title^10")
                                   .Field($"shortDescription^5")
                                   .Field($"body")
                        )
                        .Query(value)
                     )
                )
            );

            var posts = searchResponse.Documents;
            return posts;
        }


    }
}
