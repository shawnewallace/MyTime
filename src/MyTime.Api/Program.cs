using Microsoft.EntityFrameworkCore;
using MyTime.App;
using MyTime.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
	options.AddPolicy("development", corsPolicyBuilder =>
	{
		corsPolicyBuilder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

builder.Services
	.AddApp()
	.AddPersistence(builder.Configuration.GetConnectionString("MyTimeSqlDbContextConnectionString"));

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
	try
	{
		var context = scope.ServiceProvider.GetService<MyTimeSqlDbContext>();
		context?.Database.Migrate();

		MyTimeSqlDbContextInitializer.Initialize(context);
	}
	catch (Exception ex)
	{
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred while migrating or initializing the database.");
	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("development");

app.MapControllers();

app.Run();