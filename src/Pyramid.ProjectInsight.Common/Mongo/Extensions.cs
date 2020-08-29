using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Pyramid.ProjectInsight.Common.Mongo
{
    /// <summary>
    /// class for extend mongo db method
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// register instance required for mongo db
        /// </summary>
        /// <param name="services">for register instance</param>
        /// <param name="configuration">get value from configuration</param>
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<MongoClient>(c => 
            {
                var options = c.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });
            services.AddScoped<IMongoDatabase>(c => 
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
            
                return client.GetDatabase(options.Value.Database);
            });
            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }        
    }
}