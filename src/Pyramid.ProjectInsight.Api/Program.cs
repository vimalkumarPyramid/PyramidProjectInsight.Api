using Pyramid.ProjectInsight.Common.Events;
using Pyramid.ProjectInsight.Common.Services;

namespace Pyramid.ProjectInsight.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //subscribe create active
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ActivityCreated>()
                .Build()
                .Run();
        }
    }
}
