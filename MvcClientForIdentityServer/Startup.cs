using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Authentication set up
            services.AddAuthentication(config => {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookie", options=> {
                    //setting the cookie policies
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                    options.Cookie.IsEssential = true;
                })
                .AddOpenIdConnect("oidc", config => {
                    config.Authority = "https://localhost:46834/";
                    config.ClientId = "portal-api";
                    //Ýf the Client secret config is true in identity server side, pls set ClientSecret config.
                    //config.ClientSecret = "xxxxxxxxx"
                    config.SaveTokens = true;
                    config.RequireHttpsMetadata = false;
                    config.ResponseType = "code";
                    //config.CallbackPath = "/signin-oidc";
                    config.SignedOutCallbackPath = "/Home/Index";
                    config.SignedOutRedirectUri = "http://localhost:57407/signout-callback-oidc";
                    config.SignInScheme = "Cookie";
                    config.MetadataAddress = "http://localhost:46834/.well-known/openid-configuration";

                    //Get claims
                    config.GetClaimsFromUserInfoEndpoint = true;
                                     
                });

            services.AddSession(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.IsEssential = true;
            });

            

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

        

            app.UseMvc();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}