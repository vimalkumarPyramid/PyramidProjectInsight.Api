using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pyramid.ProjectInsight.Common.Commands;
using Pyramid.ProjectInsight.Common.Events;
using RawRabbit;
using RawRabbit.Instantiation;

namespace Pyramid.ProjectInsight.Common.RabbitMq
{
    /// <summary>
    /// class for handle mongo db extension method
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// extend command handler
        /// </summary>
        /// <typeparam name="TCommand">generic object</typeparam>
        /// <param name="bus">bus client</param>
        /// <param name="handler">command handler</param>
        /// <returns></returns>
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        /// <summary>
        /// extend event handler
        /// </summary>
        /// <typeparam name="TEvent">generic object</typeparam>
        /// <param name="bus">bus client</param>
        /// <param name="handler">event handler</param>
        /// <returns></returns>
        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        /// <summary>
        /// get queue name
        /// </summary>
        /// <typeparam name="T">generic object</typeparam>
        /// <returns>return queue name</returns>
        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        /// <summary>
        /// register rabbit queue
        /// </summary>
        /// <param name="services">for register bus client</param>
        /// <param name="configuration">get rebbit mq configuration</param>
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}