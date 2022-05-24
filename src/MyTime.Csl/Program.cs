using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.Persistence;

IConfiguration config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.AddEnvironmentVariables()
	.Build();

var connectionString = config.GetConnectionString("MyTimeSqlDbContextConnectionString");
var optionsBuilder = new DbContextOptionsBuilder<MyTimeSqlDbContext>();
optionsBuilder.UseSqlServer(connectionString);

var context = new MyTimeSqlDbContext(optionsBuilder.Options);

Console.WriteLine(connectionString);

var handler = new CreateNewEntryCommandHandler(context);

Random r = new Random();

// for(var i = 1; i <= 500; i++)
// {
// 	var theDay = DateTime.Now.AddDays(-r.Next(1, 180));
// 	var command = new CreateNewEntryCommand();	
// 	command.OnDate = theDay;
// 	command.Description = $"Entry {i}";
// 	command.Duration = r.Next(0, 120);
// 	command.IsUtilization = (i%2 == 0);
// 	command.Notes = "???";
// 	var result = await handler.Handle(command, CancellationToken.None);
// }

// var entries = await context.Entries.ToListAsync();

// foreach(var entry in entries.OrderBy(e => e.OnDate))
// {
// 	Console.WriteLine($"{entry.Id} - {entry.Description} - {entry.OnDate.ToShortDateString()} - {entry.Duration}");
// }


var jan1 = new DateTime(DateTime.Today.Year, 1, 1);
var startOfWeek = jan1.AddDays(1 - (int) jan1.DayOfWeek - 1);

Console.WriteLine($"The First Day of the Year is a {jan1.DayOfWeek}");

Console.WriteLine($"{jan1.ToShortDateString()} -> {startOfWeek.ToShortDateString()}");

Console.WriteLine("=> The Year in Weeks");
var theYearInWeeks = new List<ADateRange>();
while(startOfWeek.Year <= jan1.Year)
{
	theYearInWeeks.Add(new ADateRange(startOfWeek, startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59)));
	startOfWeek = startOfWeek.AddDays(7);
}

foreach(var week in theYearInWeeks) Console.WriteLine(week);

Console.WriteLine("=> The Year in Months");
var theYearInMonths = new List<ADateRange>();
var firstDayOfMonth = jan1;
while(firstDayOfMonth.Year <= jan1.Year)
{
	theYearInMonths.Add(new ADateRange(firstDayOfMonth, firstDayOfMonth.AddMonths(1).AddSeconds(-1)));
	firstDayOfMonth = firstDayOfMonth.AddMonths(1);
}
foreach (var month in theYearInMonths) Console.WriteLine(month);

