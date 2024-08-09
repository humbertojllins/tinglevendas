using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TingleVendas.Models;

namespace TingleVendas
{
    public class Startup
    {
        public static int Progress { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebRootPath = env.WebRootPath;
        }

        public IConfiguration Configuration { get; }
        public static string WebRootPath { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddControllersWithViews();
            services.AddDbContext<TingleVendasContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("tingleVendas"),(obj) => obj.EnableRetryOnFailure(5, TimeSpan.FromSeconds(20), null)
                    ));


            //Configuraçoes para utilizar session
            services.AddDistributedMemoryCache();
            services.AddSession();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie
                (
                    options =>
                    {
                        options.LoginPath = "/Login/Index";
                        options.LogoutPath = "/Login/Logout";
                    }
                )
                ;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Configuracao para permitir ao alterar o .cshtml atualizar o browser e refletir o resultado (hot reload)
            services.AddControllersWithViews().AddRazorRuntimeCompilation();


            //services.AddScoped<CustomCookieAuthenticationEvents>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{controller=Login}/{action=Index}/{id?}");
                    pattern: "{controller=Oi360}/{action=Index}/{id?}");
            });

        }
    }
}
