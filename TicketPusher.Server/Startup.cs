using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketPusher.Server.CompletedTickets;
using TicketPusher.Server.Projects;
using TicketPusher.Server.Tickets;

namespace TicketPusher.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var ticketPusherApi = new Uri(Configuration["TicketPusherApi"]);
            void RegisterTypedClient<TClient, TImplementation>(Uri apiBaseUri)
                where TClient : class where TImplementation : class, TClient
            {
                services.AddHttpClient<TClient, TImplementation>(client =>
                {
                    client.BaseAddress = apiBaseUri;
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler();
                    if (_env.IsDevelopment())
                    {
                        handler.ServerCertificateCustomValidationCallback =
                            (message, cert, chain, errors) => true;
                    }
                    return handler;
                });
            };
            RegisterTypedClient<IProjectWriteDataService, ProjectWriteDataService>(ticketPusherApi);
            RegisterTypedClient<IProjectReadDataService, ProjectReadDataService>(ticketPusherApi);
            RegisterTypedClient<ITicketReadDataService, TicketReadDataService>(ticketPusherApi);
            RegisterTypedClient<ITicketWriteDataService, TicketWriteDataService>(ticketPusherApi);
            RegisterTypedClient<ICompletedTicketReadDataService, CompletedTicketReadDataService>(ticketPusherApi);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                options.ClientId = Configuration["Okta:ClientId"];
                options.ClientSecret = Configuration["Okta:ClientSecret"];
                options.CallbackPath = "/authorization-code/callback";
                options.Authority = Configuration["Okta:Issuer"];
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.TokenValidationParameters.ValidateIssuer = false;
                options.TokenValidationParameters.NameClaimType = "name";
            })
            .AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
