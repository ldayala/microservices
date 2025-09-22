using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Masstransit
{
    public static class Extension
    {
        public static IServiceCollection AddMesageBroker(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly? assembly = null)
        {
            //Implement RabbitMQ masstransit configuration
            //
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                if (assembly != null) 
                    config.AddConsumers(assembly);

                //configura el bus para que use RabbitMQ como el transportador
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["MessageBroker:Host"]), host =>
                    {
                        host.Username(configuration["MessageBroker:Username"]);
                        host.Password(configuration["MessageBroker:Password"]);
                    });
                    cfg.ConfigureEndpoints(context);
                });
               
            });
            return services;
        }
    }
}
