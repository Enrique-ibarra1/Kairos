using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kairos.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// Pattybranch added the line below for Stripe 
using Stripe;

namespace Kairos
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<HomeContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
            
            services.AddSession(options => options.Cookie.IsEssential=true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<StripeSetting>(Configuration.GetSection("Stripe"));
            // Below line added by Pattybranch
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Pattybranch added the line below for setting the API key of our stripe config to 
            // pull the Api Keys that was set in the appsetting.json file
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            // Pattybranch created a new class called "StripeSetting"
            // It will hold the secret key and the publishable key
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
