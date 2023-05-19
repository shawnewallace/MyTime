using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Web.Models;
using MyTime.App.EntryDays.GetEntryDayList;
using MyTime.App.Infrastructure;
using MyTime.App.Models;

namespace MyTime.Web.Controllers;

public class HomeController : ControllerBase
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger, IMediator mediator) : base(mediator)
	{
		_logger = logger;
	}

	public async Task<IActionResult> Index()
	{
		var theDay = DateTime.UtcNow;
		var firstDayOfMonth = theDay.FirstDayOfMonth();
		var lastDayOfMonth = firstDayOfMonth.LastDayOfMonth();

		var startOfFirstWeek = firstDayOfMonth.FirstDayOfWeek();
		var endOfLastWeek = lastDayOfMonth.LastDayOfWeek();

		var query = new GetEntryDayListQuery();
		query.From = startOfFirstWeek;
		query.To = endOfLastWeek;
		var result = await _mediator.Send(query);

		var description = $"{theDay.ToString("MMMM")} {theDay.Year}";
		var model = new MonthModel(startOfFirstWeek, endOfLastWeek, description);

		// var dateIncrementer = startOfFirstWeek;
		// while(dateIncrementer <= endOfLastWeek)
		// {
		// 	model.AddDay(
		// 		new DayModel(
		// 			dateIncrementer.Year, 
		// 			dateIncrementer.Month, 
		// 			dateIncrementer.Day));
		// 	dateIncrementer.AddDays(1);
		// }

		return View(model);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}

public class MonthModel
{
	public MonthModel(DateTime firstDay, DateTime lastDay, string description)
	{
		FirstDay = firstDay;
		LastDay = lastDay;
		Description = description;
	}

	public string Description { get; set; }
	public DateTime FirstDay { get; set; }
	public DateTime LastDay { get; set; }

	public List<DayModel> Days { get; private set; } = new List<DayModel>();

	public void AddDay(DayModel day)
	{
		Days.Add(day);
	}
}

public class DayModel : IEntryRollup
{
	public int Year { get; }
	public int Month { get; }
	public int DayOfMonth { get; }

	public DayModel() { }
	public DayModel(int year, int month, int day)
	{
		Year = year;
		Month = month;
		DayOfMonth = day;
	}

	public float Total => throw new NotImplementedException();

	public float UtilizedTotal => throw new NotImplementedException();
}