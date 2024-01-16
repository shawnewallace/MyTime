using System;
using System.Collections.Generic;
using MediatR;

namespace MyTime.App.WeekSummary;

public record GetWeekSummaryQuery(DateTime From, DateTime To) : IRequest<List<WeekSummaryModel>>;
