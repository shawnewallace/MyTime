using System.Net;
using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyTime.App.Entries.CreateNewEntry;

namespace MyTime.Functions
{
	public class EventStarting
	{
		private readonly MyOptions _settings;
		private IMediator _mediator;

		public EventStarting(IOptions<MyOptions> options, IMediator mediator)
		{
			_settings = options.Value;
			_mediator = mediator;
		}

		[Function("EventStarting")]
		public async Task<HttpResponseData> RunAsync(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
			FunctionContext context)
		{
			var _logger = context.GetLogger("EventStarting");
			_logger.LogInformation("C# HTTP trigger function processed a request.");

			var eventDetails = await req.ReadFromJsonAsync<AppointmentStartingNotification>();
			var eventDetailsAsString = JsonSerializer.Serialize(eventDetails);

			_logger.LogInformation("The Payload {x}", eventDetailsAsString);

			await WriteEventToBlobStorage("AppointmentStarting", eventDetails, _logger);

			var command = new CreateNewEntryCommand
			{
				OnDate = eventDetails.startTime.Date,
				Description = eventDetails.subject,
				Notes = "Created Automatically by EventStarting function",
				CorrelationId = eventDetails.eventId
			};

			var newEntry = await _mediator.Send(command);

			var response = req.CreateResponse(HttpStatusCode.OK);
			await response.WriteAsJsonAsync(newEntry);
			// response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

			return response;
		}
		private async Task WriteEventToBlobStorage(string eventName, AppointmentStartingNotification evt, ILogger log)
		{
			var blobStorageConnectionString = _settings.BlobStorageConnectionString;
			var blobStorageContainerName = _settings.BlobStorageContainerName;

			var correlationId = Guid.NewGuid();
			var rightNowUtc = DateTime.UtcNow;
			var blobName = $"{rightNowUtc:yyyyMMddHHmmssfff}-{eventName}-{correlationId}.json";

			log.LogInformation($"Writing new event {eventName} to file {blobName}. CorrelationId is {correlationId}");

			var container = new BlobContainerClient(blobStorageConnectionString, blobStorageContainerName);
			var blob = container.GetBlobClient(blobName);

			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evt))))
			{
				await blob.UploadAsync(ms);
			}
		}
	}



	public class AppointmentStartingNotification
	{
		public string eventId { get; set; }
		public string subject { get; set; }
		public DateTime startTime { get; set; }
		public string name { get; set; }
	}
}
