using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Toast;
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
        public Startup(IHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostingEnvironment { get; }

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
                    if (HostingEnvironment.IsDevelopment())
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

            services.AddBlazoredToast();
            services.AddBlazoredModal();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
