using System;
using EvenTransit.Core.Abstractions.Common;
using EvenTransit.Core.Constants;
using EvenTransit.Messaging.RabbitMq.Abstractions;
using EvenTransit.Messaging.RabbitMq.Domain;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace EvenTransit.Messaging.RabbitMq.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddScoped<IEventConsumer, EventConsumer>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddSingleton<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();
            services.AddScoped<IRabbitMqDeclaration, RabbitMqDeclaration>();
            services.AddScoped(typeof(IConnectionFactory), provider =>
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = Environment.GetEnvironmentVariable(RabbitMqConstants.Host),
                    UserName = Environment.GetEnvironmentVariable(RabbitMqConstants.UserName),
                    Password = Environment.GetEnvironmentVariable(RabbitMqConstants.Password),
                    AutomaticRecoveryEnabled = true
                };

                return connectionFactory;
            });

            var serviceProvider = services.BuildServiceProvider();
            BindRabbitMqExchanges(serviceProvider);

            return services;
        }

        private static void BindRabbitMqExchanges(IServiceProvider serviceProvider)
        {
            var rabbitMqConnection = serviceProvider.GetRequiredService<IRabbitMqConnectionFactory>();
            var rabbitMqDeclaration = serviceProvider.GetRequiredService<IRabbitMqDeclaration>();

            using var channel = rabbitMqConnection.ProducerConnection.CreateModel();
            rabbitMqDeclaration.DeclareQueuesAsync(channel).GetAwaiter().GetResult();
        }
    }
}