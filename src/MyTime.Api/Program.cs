using Carter;
using Microsoft.EntityFrameworkCore;
using MyTime.Api.Infrastructure;
using MyTime.App;
using MyTime.Persistence;
using MyTime.Persistence.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("development", corsPolicyBuilder => corsPolicyBuilder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()));
builder.Services.AddCarter();

builder.Services
  .AddApp()
  .AddPersistence(builder.Configuration.GetConnectionString("MyTimeSqlDbContextConnectionString"));


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

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseCors("development");

app.MapControllers();

app.Run();
