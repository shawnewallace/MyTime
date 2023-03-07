using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Text;
using Azure.Storage.Blobs;
using MyTime.App.Entries.CreateNewEntry;
using MediatR;

namespace MyTime.Fns;

public class EventStarting
{
	private readonly MyOptions _settings;
	private IMediator _mediator;

	public EventStarting(IOptions<MyOptions> options, IMediator mediator)
	{
		_settings = options.Value;
		_mediator = mediator;
	}

	[FunctionName("EventStarting")]
	public async Task<IActionResult> RunAsync(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] AppointmentStartingNotification req,
			ILogger log)
	{
		log.LogInformation("C# HTTP trigger function processed a request.");

		var payloadAsString = JsonConvert.SerializeObject(req);

		log.LogInformation("The Payload {x}", payloadAsString);

		await WriteEventToBlobStorage("AppointmentStarting", req, log);

		var command = new CreateNewEntryCommand
		{
			OnDate = req.startTime.Date,
			Description = req.subject,
			Notes = "Created Automatically by EventStarting function",
			CorrelationId = req.eventId
		};

		var newEntry = await _mediator.Send(command);

		return new CreatedResult("", newEntry);
		// return new OkObjectResult(responseMessage);
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

		using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt))))
		{
			await blob.UploadAsync(ms);
		}
	}
}
