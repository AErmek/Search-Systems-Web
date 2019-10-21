using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Searcher.BLL.Infrastructure;
using Searcher.BLL.Interfaces;
using Searcher.BLL.Services;
using Searcher.BLL.Settings;
using Searcher.DAL.EF;
using Searcher.DAL.Interfaces;
using Searcher.DAL.Repositories;
using Searcher.Middleware;

namespace Searcher
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public readonly ILoggerFactory _loggerFactory;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.Configure<ApiKeyOptions>(Configuration.GetSection("ApiKeyOptions"));

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<SearcherContext>(dbcontextoption => dbcontextoption.UseSqlServer(connectionString));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.ConfigureExceptionHandler(_loggerFactory);

            InitializeDatabase(app);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<SearcherContext>().Database.Migrate();
            }
        }
    }
}
