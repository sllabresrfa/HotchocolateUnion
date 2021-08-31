using Company.API.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Company.API
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<CompanyDbContext>(options =>
            options
            .UseSqlite($"Data Source=uniontest.db"));
            services
                .AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddProjections()
                .AddFiltering();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var context = app.ApplicationServices.GetService<IDbContextFactory<CompanyDbContext>>().CreateDbContext();

                context.Database.EnsureCreated();
                context.Database.Migrate();

            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
