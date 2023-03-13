using Newtonsoft.Json;
using WebApiWeatherForecastClient.WebApiWeatherForecastServiceReference;

//Establecer un certificado que no es de confianza
var httpHandler = new HttpClientHandler();
httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
var httpClient = new HttpClient(httpHandler);

// The port number must match the port of the gRPC server.
var client = new WebApiWeatherForecastServiceReference("https://localhost:7140", httpClient);

// LLamo al servicio
var reply = await client.GetWeatherForecastAsync();
var strResply = JsonConvert.SerializeObject(reply, Formatting.Indented);

Console.WriteLine("Result: " + strResply);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();