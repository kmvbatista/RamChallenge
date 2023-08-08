using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using dotenv;
using dotenv.net;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyOrigin() // Replace with your frontend origin
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    // .AllowCredentials(); // Allow credentials (cookies, authentication)
                });
            });
            services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use 'None' in development, 'Always' in production
});

            services.AddMvc(options =>
                options.Filters.Add(typeof(JsonExceptionFilter))
            );
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }));
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            var connectionString = System.Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            // services.AddDbContext<MainContext>(builder => builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<MainContext>(builder => builder.UseNpgsql(connectionString));
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IStatusRepository, StatusRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
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
            MigrateDatabase(app);
        }

        private void MigrateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<MainContext>();
            context.Database.Migrate();
        }
    }
}
