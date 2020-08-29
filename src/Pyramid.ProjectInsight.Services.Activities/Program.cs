using Microsoft.AspNetCore.Hosting;
using Pyramid.ProjectInsight.Common.Commands;
using Pyramid.ProjectInsight.Common.Services;

namespace Pyramid.ProjectInsight.Services.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateActivity>()
                .Build()
                .Run();
        }
    }
}
