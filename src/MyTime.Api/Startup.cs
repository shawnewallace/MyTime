using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Infrastructure;
using MyTime.Persistence;

namespace MyTime.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			_env = env;
		}

		public IConfiguration Configuration { get; }
		private readonly IWebHostEnvironment _env;

		// This method gets called by the runtime. Use this method to add services to the container.
		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddCors(options =>
			{
				options.AddPolicy("development", builder =>
							{
								builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
							});
			});

			// Add MediatR and load handlers from Lib project
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
			services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(CreateNewEntryCommandHandler).Assembly));

			// services.AddSwaggerDocument(document =>
			// {
			// 	document.DocumentName = "latest";
			// 	document.Title = "ReferenceApp API";
			// 	document.Description = "API routes for interacting with ReferenceApp services.";
			// });


			// FOR DEMONSTRATION PURPOSES
			services.AddDbContext<MyTimeSqlDbContext>(
					options => options.UseSqlServer(
							Configuration.GetConnectionString("MyTimeSqlDbContextConnectionString"),
							optionsBiuilder => optionsBiuilder.MigrationsAssembly("MyTime.Api"))
			);

			// services.AddSingleton<ITelemetryInitializer>(new ApplicationNameTelemetryInitializer("MyTime.Api"));
			// services.Configure<ApplicationInsightsServiceOptions>(Configuration.GetSection("ApplicationInsights"));
			// services.AddApplicationInsightsTelemetry();
			// services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public virtual void Configure(IApplicationBuilder app)
		{

			if (_env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors("developement");

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// app.UseOpenApi();
			// app.UseSwaggerUi3();
		}
	}
}
