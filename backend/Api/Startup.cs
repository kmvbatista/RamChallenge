using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;

namespace Api
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

            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }));
            services.AddControllers();
            var connectionString = System.Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            // services.AddDbContext<MainContext>(builder => builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<MainContext>(builder => builder.UseNpgsql(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
