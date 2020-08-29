using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pyramid.ProjectInsight.Api.Handlers;
using Pyramid.ProjectInsight.Api.Repositories;
using Pyramid.ProjectInsight.Common.Auth;
using Pyramid.ProjectInsight.Common.Events;
using Pyramid.ProjectInsight.Common.Mongo;
using Pyramid.ProjectInsight.Common.RabbitMq;

namespace Pyramid.ProjectInsight.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddMongoDB(Configuration);
            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            databaseInitializer.InitializeAsync();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
