using BlogEngine.Shared.Events.Posts;
using MassTransit;
using Nest;
using System;
using System.Threading.Tasks;

namespace BlogEngine.SearchEngine.PostIndexer
{
    public class PostSearchEngine
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://127.0.0.1"), h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                });
                sbc.ReceiveEndpoint("test_queue", ep =>
                {
                    ep.Handler<PostAdded>(context =>
                    {
                        IndexPost(context.Message);
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Title}");
                    });
                });
            });

            await bus.StartAsync(); // This is important!
            Console.WriteLine("Press any key to continue ...");
            await Task.Run(() => Console.ReadKey());
            await bus.StopAsync();
        }

        private static void IndexPost(PostAdded message)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("posts");
            var client = new ElasticClient(settings);
            PostSearchEngine post = new PostSearchEngine
            {
                Id = message.Id,
                Title = message.Title,
                Body = message.Body,
                ShortDescription = message.ShortDescription
            };
            var indexResponse = client.IndexDocument(post);


        }
    }
}
