using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KowWhoisApi.Services;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Data;
using KowWhoisApi.Middleware;
using KowWhoisApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace KowWhoisApi
{
	public class Startup
	{
		readonly string CorsAllowEverything = "dev_policy";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<PiosServiceOptions>(Configuration.GetSection(PiosServiceOptions.PiosService));

			services.AddTransient<IPiosService, PiosService>();
			services.AddTransient<IDomainsService, DomainsService>();
			services.AddTransient<IRegistrarsService, RegistrarsService>();
			services.AddTransient<INameServersService, NameServersService>();
			services.AddTransient<ISnapshotsService, SnapshotsService>();

			services.AddCors(options =>
			{
				options.AddPolicy(CorsAllowEverything, builder =>
				{
					builder.WithOrigins("*");
				});
			});

			services.AddDbContext<WhoisContext>(
				options => options.UseMySql(
					Configuration.GetConnectionString("WhoisDatabase"),
					new MariaDbServerVersion(new Version(10, 8, 3)))
				.UseLoggerFactory(LoggerFactory.Create(
					logging => logging
						.AddConsole()
						.AddFilter(level => level >= LogLevel.Information)))
				.EnableSensitiveDataLogging()
				.EnableDetailedErrors()
			);

			services.AddSingleton<IMemoryCache, MemoryCache>();

			services.AddControllers()
				.AddNewtonsoftJson(
					options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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
				// app.UseHsts();
			}

			app.UseStaticFiles();

			app.UseMiddleware<RequestLoggingMiddleware>();

			app.UseRouting();
			app.UseCors(CorsAllowEverything);

			// app.UseAuthentication();
			app.UseAuthorization();
			// app.UseSession();
			// app.UseResponseCaching();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
