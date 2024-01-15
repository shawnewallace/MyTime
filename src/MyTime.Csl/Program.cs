using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.Persistence;

IConfiguration config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.AddJsonFile("appsettings.Development.json", optional: true)
	.AddEnvironmentVariables()
	.Build();

var devConnectionString = config.GetConnectionString("MyTimeSqlDbContextConnectionStringDEV");
var prodConnectionString = config.GetConnectionString("MyTimeSqlDbContextConnectionStringPROD");

var devOptionsBuilder = new DbContextOptionsBuilder<MyTimeSqlDbContext>();
devOptionsBuilder.UseSqlServer(devConnectionString);
var devContext = new MyTimeSqlDbContext(devOptionsBuilder.Options);

var prodOptionsBuilder = new DbContextOptionsBuilder<MyTimeSqlDbContext>();
prodOptionsBuilder.UseSqlServer(prodConnectionString);
var prodContext = new MyTimeSqlDbContext(prodOptionsBuilder.Options);

Console.WriteLine("DEV Connection string: " + devConnectionString);
Console.WriteLine("PROD Connection string: " + prodConnectionString);

var x = devContext.Entries.Count();
var y = prodContext.Entries.Count();
Console.WriteLine($"BEFORE => DEV Entries: {x}     PROD Entries: {y}");

x = devContext.Categories.Count();
y = prodContext.Categories.Count();
Console.WriteLine($"BEFORE => DEV Categories: {x}     PROD Categories: {y}");

await RemoveAllEntries(devContext);
await RemoveAllCategories(devContext);

var prodCategories = prodContext.Categories.ToList();
devContext.Categories.AddRange(prodCategories);
devContext.SaveChanges();

var prodEntries = prodContext.Entries.ToList();
devContext.Entries.AddRange(prodEntries);
devContext.SaveChanges();

x = devContext.Categories.Count();
y = prodContext.Categories.Count();
Console.WriteLine($"AFTER => DEV Categories: {x}     PROD Categories: {y}");

x = devContext.Entries.Count();
y = prodContext.Entries.Count();
Console.WriteLine($"AFTER => DEV Entries: {x}     PROD Entries: {y}");

async Task RemoveAllCategories(MyTimeSqlDbContext ctx) => await ctx.Categories.ExecuteDeleteAsync();
async Task RemoveAllEntries(MyTimeSqlDbContext ctx) => await ctx.Entries.ExecuteDeleteAsync();
