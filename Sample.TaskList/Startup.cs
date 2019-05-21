using GraphQL;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.TaskList.Graph;

namespace Sample.TaskList
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             * Hook GraphQL's IDependencyResolver to the our DI container. There is a default implementation for
             * IDependencyResolver which uses Activator.CreateInstance. See 
             * https://github.com/graphql-dotnet/graphql-dotnet/blob/master/src/GraphQL/IDependencyResolver.cs for
             * the implementation.
             */
            services.TryAddSingleton<IDependencyResolver>(q => new FuncDependencyResolver(q.GetRequiredService));

            // Register the root of our schema
            services.TryAddSingleton<TaskListSchema>();

            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = true;
                    options.ExposeExceptions = true;
                })
                .AddGraphTypes()
                .AddWebSockets();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseGraphQLWebSockets<TaskListSchema>("/graph");
            app.UseGraphQL<TaskListSchema>("/graph");
        }
    }
}
