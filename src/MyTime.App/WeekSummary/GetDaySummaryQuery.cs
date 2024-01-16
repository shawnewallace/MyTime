using System;
using System.Collections.Generic;
using MediatR;

namespace MyTime.App.WeekSummary;

public record GetDaySummaryQuery(DateTime From, DateTime To) : IRequest<List<DaySummaryModel>>;
