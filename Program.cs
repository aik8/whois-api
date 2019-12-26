using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KowWhoisApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KowWhoisApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webHost = CreateHostBuilder(args).Build();

			using (var scope = webHost.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var db = services.GetRequiredService<WhoisContext>();
					db.Database.Migrate();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occured while migrating the datbase.");
				}
			}

			webHost.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					// Make sure it listens to 127.0.0.1.
					// (Docker does not like IPv6 that much.)
					webBuilder.ConfigureKestrel(serverOptions =>
						serverOptions.Listen(IPAddress.Any, 5000));

					webBuilder.UseStartup<Startup>();
				});
	}
}
