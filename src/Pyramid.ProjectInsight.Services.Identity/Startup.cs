using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pyramid.ProjectInsight.Common.Auth;
using Pyramid.ProjectInsight.Common.Commands;
using Pyramid.ProjectInsight.Common.Mongo;
using Pyramid.ProjectInsight.Common.RabbitMq;
using Pyramid.ProjectInsight.Services.Identity.Domain.Repositories;
using Pyramid.ProjectInsight.Services.Identity.Domain.Services;
using Pyramid.ProjectInsight.Services.Identity.Handlers;
using Pyramid.ProjectInsight.Services.Identity.Repositories;
using Pyramid.ProjectInsight.Services.Identity.Services;

namespace Pyramid.ProjectInsight.Services.Identity
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
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IEncrypter, Encrypter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
            , IDatabaseInitializer databaseInitializer, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            databaseInitializer.InitializeAsync();
            loggerFactory.AddLog4Net();
            app.UseMvc();
        }
    }
}
