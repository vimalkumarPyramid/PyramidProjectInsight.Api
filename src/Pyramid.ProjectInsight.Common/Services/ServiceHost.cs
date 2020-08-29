using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pyramid.ProjectInsight.Common.Commands;
using Pyramid.ProjectInsight.Common.Events;
using Pyramid.ProjectInsight.Common.RabbitMq;
using RawRabbit;

namespace Pyramid.ProjectInsight.Common.Services
{
    /// <summary>
    /// for host the service
    /// </summary>
    public class ServiceHost : IServiceHost
    {
        /// <summary>
        /// web host object
        /// </summary>
        private readonly IWebHost _webHost;

        /// <summary>
        /// for intialize webhost
        /// </summary>
        /// <param name="webHost">web host</param>
        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        /// <summary>
        /// for run the host
        /// </summary>
        public void Run() => _webHost.Run();

        /// <summary>
        /// create host
        /// </summary>
        /// <typeparam name="TStartup">tstartup</typeparam>
        /// <param name="args">arguments</param>
        /// <returns></returns>
        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase 
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMq()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus; 

            /// <summary>
            /// initialize web hist and bus
            /// </summary>
            /// <param name="webHost">web host</param>
            /// <param name="bus">bus client</param>
            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            /// <summary>
            /// subscribe command
            /// </summary>
            /// <typeparam name="TCommand">TCommand</typeparam>
            /// <returns></returns>
            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var serviceScopeFactory = _webHost.Services.GetService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var handler = (ICommandHandler<TCommand>)scope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));
                    _bus.WithCommandHandlerAsync(handler);
                }

                return this;
            }

            /// <summary>
            /// subscribe event
            /// </summary>
            /// <typeparam name="TEvent">TEvent</typeparam>
            /// <returns></returns>
            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var serviceScopeFactory = _webHost.Services.GetService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var handler = (IEventHandler<TEvent>)scope.ServiceProvider
                    .GetService(typeof(IEventHandler<TEvent>));
                    _bus.WithEventHandlerAsync(handler);
                }

                return this;
            }

            /// <summary>
            /// build host
            /// </summary>
            /// <returns></returns>
            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}