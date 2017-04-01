using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DotNetify;
using React.AspNet;
using Microsoft.Extensions.Configuration;

namespace Tearc.SPA
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            // Add Localization if you want your site support multiple languages.
            services.AddLocalization();

            // SignalR and Memory Cache are required by dotNetify.
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();  // Required by ReactJS.NET.
            services.AddSignalR();//services.AddSignalR(options => options.Hubs.EnableDetailedErrors = true);

            services.AddReact();
            services.AddDotNetify();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseReact(config => { });
            app.UseStaticFiles();

            // Required by dotNetify.
            app.UseWebSockets();
            app.UseSignalR();
            // The default configuration assumes all view model classes are in the web project
            app.UseDotNetify();

            //app.UseDotNetify(config => {
            //    // Use built-in ASP.NET Core dependency injection. To change this configuration.
            //    // This will cause all the classes inside the assembly that inherits from DotNetify.BaseVM to be known as view models.
            //    config.RegisterAssembly("Tearc.ViewModels");/* name of the assembly where the view model classes are located */

            //    // Set the factory method to provide view model instances, where you can use your favorite IoC container.
            //    // If this isn't set, it will default to using ASP.NET Core DI container.
            //    // let your favorite IoC library creates the view model instance
            //    var provider = app.ApplicationServices;
            //    config.SetFactoryMethod((type, args) => 
            //    {
            //        return ActivatorUtilities.CreateInstance(provider, type, args ?? new object[] { });
            //    });
            //});

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                       name: "default",
                       template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
