using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using KowWhoisApi.Services;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Data;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.MsgPack;
using KowWhoisApi.Middleware;
using KowWhoisApi.Models;

namespace KowWhoisApi
{
	public class Startup
	{
		readonly string CorsAllowEverything = "dev_policy";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			RedisConfiguration = Configuration.GetSection("Redis").Get<RedisConfiguration>();
		}

		public IConfiguration Configuration { get; }
		public RedisConfiguration RedisConfiguration { get; }

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
					new MariaDbServerVersion(new Version(10, 3, 24)),
					mysqlOptions => mysqlOptions
						.CharSet(CharSet.Utf8Mb4)
						.CharSetBehavior(CharSetBehavior.NeverAppend)
				));

			services.AddSingleton(RedisConfiguration);
			services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
			services.AddSingleton<IRedisCacheConnectionPoolManager, RedisCacheConnectionPoolManager>();
			services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
			services.AddSingleton<ISerializer, MsgPackObjectSerializer>();

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
