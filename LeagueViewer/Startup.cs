using System;
using AutoMapper;
using LeagueViewer.Models;
using LeagueViewer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeagueViewer
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // App Settings
            var appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);

            var apiCredentials = new ApiCredentials();
            Configuration.GetSection("ApiCredentials").Bind(apiCredentials);

            services.AddSingleton<AppSettings>(appSettings);

            // Current League Seasons
            var currentLeagueSeasons = new CurrentLeagueSeasons();
            Configuration.GetSection("CurrentLeagueSeasons").Bind(currentLeagueSeasons);

            services.AddSingleton<CurrentLeagueSeasons>(currentLeagueSeasons);

            // Register services
            services.AddTransient<ILeagueService, LeagueService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ISeasonService, SeasonService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IFixtureService, FixtureService>();

            // Http Clients
            services.AddHttpClient("rapidFootball", c =>
            {
                c.BaseAddress = new Uri(apiCredentials.RapidFootballUrl);

                // Headers
                c.DefaultRequestHeaders.Add("x-rapidapi-host", apiCredentials.ApiHost);
                c.DefaultRequestHeaders.Add("x-rapidapi-key", apiCredentials.ApiKey);
            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=League}/{action=Detail}/{id?}");
            });
        }
    }
}
