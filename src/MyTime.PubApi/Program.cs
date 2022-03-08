using MyTime.App.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using MyTime.App.Entries.CreateNewEntry;
using System.Reflection;
using MyTime.Persistence.Infrastructure;
using MyTime.App.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
builder.Services.AddMediatR(typeof(CreateNewEntryCommandHandler).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("/entry", async (HttpContext httpContext, IMediator mediator, NewEntryModel model) =>
{
	var command = new CreateNewEntryCommand
	{
		OnDate = model.OnDate,
		Description = model.Description,
		Duration = model.Duration,
		IsUtilization = model.IsUtilization,
		Notes = model.Notes
	};

	return await mediator.Send(command)
		is EntryModel newEntry
			? Results.Created("", newEntry)
			: Results.BadRequest();
}).WithName("CreateNewEntry");

app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class NewEntryModel : IEntry
{
	public DateTime OnDate { get; set; }
	public string Description { get; set; } = "";
	public float Duration { get; set; }
	public bool IsUtilization { get; set; }
	public string Notes { get; set; } = "";
}