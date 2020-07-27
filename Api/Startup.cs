using Api.Controllers.Mappers.Interfaces;
using Api.Core.Services;
using Api.Domain.Repositories;
using Api.Domain.Services;
using Api.Mappers;
using Api.Repositories.DbConnection;
using Api.Repositories.DbConnection.Interfaces;
using Api.Repositories.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer Container { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var builder = new ContainerBuilder();

            var cacheConnectionString = "";
            builder.RegisterType<RedisCacheService>().As<ICacheService>()
                .WithParameter("cacheConnectionString", cacheConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<GoalService>().As<IGoalService>().InstancePerLifetimeScope();
            builder.RegisterType<GoalMapper>().As<IGoalMapper>().InstancePerLifetimeScope();

            var databaseConnectionString = "";
            builder.RegisterType<SqlConnectionFactory>().As<ISqlConnectionFactory>()
                .WithParameter("databaseConnectionString", databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<GoalRepository>().As<IGoalRepository>().InstancePerLifetimeScope();

            builder.Populate(services);

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
