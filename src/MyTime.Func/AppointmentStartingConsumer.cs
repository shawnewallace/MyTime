using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Azure.Storage.Blobs;
using System.Text;

namespace MyTime.Func;

public class AppointmentStartingConsumer
{
	private readonly AppOptions _settings;
	// private readonly ILogger _log;

	// public AppointmentStartingConsumer(IOptions<AppOptions> options, ILogger log)
	// {
	// 	// _settings = options.Value;
	// 	// _log = log;
	// }

	[FunctionName("AppointmentStarting")]
	public async Task<IActionResult> RunAsync(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] AppointmentStartingNotification req,
			ILogger _log)
	{
		_log.LogInformation("C# HTTP trigger function processed a request.");

		var payloadAsString = JsonConvert.SerializeObject(req);

		_log.LogInformation("The Payload {x}", payloadAsString);

		// await WriteEventToBlobStorage("AppointmentStarting", req);

		string responseMessage = "This HTTP triggered function executed successfully.";

		return new OkObjectResult(responseMessage);
	}

	// private async Task WriteEventToBlobStorage(string eventName, AppointmentStartingNotification evt)
	// {
	// 	var blobStorageConnectionString = _settings.BlobStorageConnectionString;
	// 	var blobStorageContainerName = _settings.BlobStorageContainerName;

	// 	var correlationId = evt.eventId;
	// 	var rightNowUtc = DateTime.UtcNow;
	// 	var blobName = $"{rightNowUtc:yyyyMMddHHmmssfff}-{eventName}-{correlationId}.json";

	// 	_log.LogInformation($"Writing new event {eventName} to file {blobName}. CorrelationId is {correlationId}");

	// 	var container = new BlobContainerClient(blobStorageConnectionString, blobStorageContainerName);
	// 	var blob = container.GetBlobClient(blobName);

	// 	using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt))))
	// 	{
	// 		await blob.UploadAsync(ms);
	// 	}
	// }
}

public class AppointmentStartingNotification
{
	public string eventId { get; set; }
	public string subject { get; set; }
	public DateTime startTime { get; set; }
	public string name { get; set; }
}
