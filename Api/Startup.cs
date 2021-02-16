using Api.Controllers.Mappers.Interfaces;
using Api.Core.Services;
using Api.Domain.Caches;
using Api.Domain.Queries;
using Api.Domain.Repositories;
using Api.Domain.Services;
using Api.Filters;
using Api.Mappers;
using Api.Repositories.Caches;
using Api.Repositories.DbConnection;
using Api.Repositories.DbConnection.Interfaces;
using Api.Repositories.Queries;
using Api.Repositories.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddMvc(setup =>
                    setup.Filters.Add(new ExceptionFilter()));

            var builder = new ContainerBuilder();

            var cacheConnectionString = Configuration.GetConnectionString("RedisCache");
            builder.RegisterType<RedisCache>().As<ICache>()
                .WithParameter("cacheConnectionString", cacheConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<GoalService>().As<IGoalService>().InstancePerLifetimeScope();
            builder.RegisterType<GoalMapper>().As<IGoalMapper>().InstancePerLifetimeScope();

            // Implementation of IGoalQuery with Dapper 
            // builder.RegisterType<GoalQuery>().Named<IGoalQuery>("GoalQuery");

            // Implementation of IGoalQuery with ADO.NET
            builder.RegisterType<GoalQuery2>().Named<IGoalQuery>("GoalQuery");

            builder.Register<IGoalQuery>((context) =>
                new GoalQueryWithCache(
                        context.Resolve<ICache>(),
                        context.ResolveNamed<IGoalQuery>("GoalQuery")));

            var databaseConnectionString = Configuration.GetConnectionString("Database");

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

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
