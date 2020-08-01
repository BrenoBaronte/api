using Api.Controllers.Mappers.Interfaces;
using Api.Core.Services;
using Api.Domain.Caches;
using Api.Domain.Repositories;
using Api.Domain.Services;
using Api.Mappers;
using Api.Repositories.Caches;
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

            var cacheConnectionString = "baronte.redis.cache.windows.net:6380,password=1COzWe7wIXDuCxfmI+sgsfWHPG0XDd8yn808boCblPA=,ssl=True,abortConnect=False";
            builder.RegisterType<RedisCache>().As<ICache>()
                .WithParameter("cacheConnectionString", cacheConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<GoalService>().As<IGoalService>().InstancePerLifetimeScope();
            builder.RegisterType<GoalMapper>().As<IGoalMapper>().InstancePerLifetimeScope();

            builder.RegisterType<GoalQuery>().Named<IGoalQuery>("GoalQuery");
            builder.Register<IGoalQuery>((context) =>
                new GoalQueryWithCache(
                        context.Resolve<ICache>(),
                        context.ResolveNamed<IGoalQuery>("GoalQuery")));

            var databaseConnectionString = "Server=tcp:barontedb.database.windows.net,1433;Initial Catalog=barontedb;Persist Security Info=False;User ID=baronte;Password=Mypasss4p;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
