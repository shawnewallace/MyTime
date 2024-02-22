using Carter;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyTime.Api.Infrastructure;
using MyTime.App;
using MyTime.Persistence;
using MyTime.Persistence.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("development", corsPolicyBuilder => corsPolicyBuilder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()));
builder.Services.AddCarter();

builder.Services
  .AddApp()
  .AddPersistence(builder.Configuration.GetConnectionString("MyTimeSqlDbContextConnectionString"))
  .AddHealthChecks()
    .AddDbContextCheck<MyTimeSqlDbContext>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
  try
  {
    MyTimeSqlDbContext? context = scope.ServiceProvider.GetService<MyTimeSqlDbContext>();
    context?.Database.Migrate();

    MyTimeSqlDbContextInitializer.Initialize(context);
  }
  catch (Exception ex)
  {
    ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
  }
}

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.MapCarter();

app.UseHttpsRedirection();

// app.MapHealthChecks("/health", new HealthCheckOptions
// {
//   ResponseWriter = HealthCheckResponseWriters.WriteJsonResponse
// }); 

app.MapHealthChecks(pattern: "health", new HealthCheckOptions
{
  ResponseWriter = async (context, report) =>
  {
    context.Response.ContentType = "application/json";

    var response = new
    {
      status = report.Status.ToString(),
      errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
    };

    await context.Response.WriteAsJsonAsync(response);
  }
});

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseCors("development");

app.MapControllers();

app.Run();
