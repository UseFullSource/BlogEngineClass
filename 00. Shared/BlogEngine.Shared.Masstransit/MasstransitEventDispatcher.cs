using MassTransit;
using Microsoft.Extensions.Configuration;
using System;

namespace BlogEngine.Shared.Masstransit
{
    public class MasstransitEventDispatcher : IEventDispatcher, IDisposable
    {
        private readonly IBusControl bus;

        public MasstransitEventDispatcher(IConfiguration configuration)
        {
            bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(configuration["RabbitMq:Url"]), h =>
                {
                    h.Username(configuration["RabbitMq:User"]);
                    h.Password(configuration["RabbitMq:Password"]);

                });
            });
            bus.Start();
        }
        public void Dispatch<T>(params T[] events)
        {
            foreach (var @event in events)
            {
                bus.Publish(@event);
            }
        }

        public void Dispose()
        {
            bus.Stop();
        }
    }
}
