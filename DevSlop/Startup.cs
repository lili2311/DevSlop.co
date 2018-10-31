using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevSlop.Slop;
using DevSlop.Slop.Data;
using DevSlop.Slop.Repositories;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevSlop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            builder.AddAzureKeyVault(
                $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
                config["azureKeyVault:clientId"],
                config["azureKeyVault:clientSecret"]
            );

            this.Configuration = builder.Build();

            //example of how to read value
            var connectionString = Configuration["appSettings:connectionStrings:DefaultConnection"];
            var aiClient = new TelemetryClient();
            aiClient.TrackTrace("connection string from Startup(): " + connectionString, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IScheduleRepository, ScheduleRepository>();

            // Register the IConfiguration instance which MyOptions binds against.
            services.Configure<ConnectionStrings>(this.Configuration);
            //var connectionString = this.Configuration.GetValue<string>("DefaultConnection");
            var connectionString = this.Configuration["appSettings:connectionStrings:DefaultConnection"];

            var aiClient = new TelemetryClient();
            aiClient.TrackTrace("connection string from ConfigureServices(): " + connectionString, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);


            // Registering DBContext, need this to get Identity to work
            services.AddDbContext<DevSlopContext>(options => options.UseSqlServer(connectionString));
            
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

            //Security headers make me happy
            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());             app.UseXContentTypeOptions();             app.UseReferrerPolicy(opts => opts.NoReferrer());             app.UseXXssProtection(options => options.EnabledWithBlockMode());             app.UseXfo(options => options.Deny());              app.UseCsp(opts => opts                 .BlockAllMixedContent()                 .StyleSources(s => s.Self())                 .StyleSources(s => s.UnsafeInline())                 .FontSources(s => s.Self())                 .FormActions(s => s.Self())                 .FrameAncestors(s => s.Self())                 .ImageSources(s => s.Self())                 .ScriptSources(s => s.Self())             );             //End Security Headers

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
