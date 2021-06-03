using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Cosmetic.Services;
using Services.PayPal;
using Cosmetic.Models;
using Cosmetic.GATagHelpers;
using EC.SecurityService.Services;
using EMarket.Services.PayPal;


namespace Cosmetic
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
            services.AddOptions();
            // Register the Google Analytics configuration
            services.Configure<GoogleAnalyticsOptions>(options => Configuration.GetSection("GoogleAnalytics").Bind(options));
            // Register the TagHelperComponent
            services.AddTransient<ITagHelperComponent, GoogleAnalyticsTagHelperComponent>();
            // Google Analytics (2 dòng trên)
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<MyPhamContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebMyPham")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // 2FA
            services.AddHttpClient();
            services.AddTransient<IAuthy, Authy>();
            //SMS
            services.AddTransient<ISmsService, SmsService>();

            services.AddSession();
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });

            //////////////////////////////////////////////////////////////////////////////////////////////
            //Sử dụng PostgreSQL Entity Framework Core
            //services.AddEntityFrameworkNpgsql().AddDbContext<MyPhamContext>(options => options.UseNpgsql(Configuration.GetConnectionString("WebMyPham")));
            
            //Sử dụng Microsoft SQL Server
            services.AddDbContext<MyPhamContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebMyPham")));
            //////////////////////////////////////////////////////////////////////////////////////////////
            
            services.AddSingleton<IPayPalPayment, PayPalPayment>();

            services.Configure<PayPalAuthOptions>(Configuration.GetSection("PayPalPayment"));
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/");
            });
        }
    }
}
