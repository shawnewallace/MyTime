using System;
using MediatR;

namespace MyTime.App.Categories.ToggleActive
{
	public class ToggleActiveCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}