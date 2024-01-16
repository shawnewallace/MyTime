using System;
using System.Collections.Generic;
using MediatR;
using MyTime.Persistence.Models;

namespace MyTime.App.WeekSummary;

public record GetCategoryReportByWeekQuery(DateTime From, DateTime To) : IRequest<List<CategorySummaryModel>>;
