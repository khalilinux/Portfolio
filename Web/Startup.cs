using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        // bach najmou nouslou lel appsetting li fiha el connectionstring => on injecte iconfiguration
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // hedhi pour les dépendances
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyPortfolioDB"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // hedha pour les middleware kima authenifications , 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // bach nestaamlou les fichier statique (dans notre cas dans le dossier wwwroot)
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Home et index sont des exemples 
                endpoints.MapControllerRoute(
                    "defaultRoute",
                    "{controller=Home}/{action=index}/{id?}");
            });
        }
    }
}
