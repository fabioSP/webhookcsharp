const string server = "http://localhost:5127";
const string callback = "http://localhost:5276/wh/item/new";
const string topic = "item.new";

var client = new HttpClient();

Console.WriteLine($"Subscribing top topic {topic} with callback {callback}");
await client.PostAsJsonAsync(server + "/subscribe", new { topic, callback });

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

var app = builder.Build();

app.MapPost("/wh/item/new", (object payload, ILogger<Program> logger) =>
{
 logger.LogInformation("Received payload: {payload}", payload);
});

app.MapGet("/", () => "Hello World!");

app.Run();
