using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Models;
using MyTime.Persistence;

namespace Mytime.App.Entries.GetEntryList
{
		public class GetEntryListQuery : IRequest<List<EntryModel>>{}

	public class GetEntryListQueryHandler : IRequestHandler<GetEntryListQuery, List<EntryModel>>
	{
		private readonly MyTimeSqlDbContext _context;

		public GetEntryListQueryHandler(MyTimeSqlDbContext context)
		{
			_context = context;
		}
		public async Task<List<EntryModel>> Handle(GetEntryListQuery request, CancellationToken cancellationToken)
		{
			var result = await _context.Entries.ToListAsync(cancellationToken);

			var response = new List<EntryModel>();

			foreach(var item in result)
			{
				response.Add(new EntryModel(item));
			}

			return response;
		}
	}
}