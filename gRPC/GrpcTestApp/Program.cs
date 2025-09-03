using Grpc.Net.Client;
using WebApi;

Console.WriteLine("Press any key to connect and run...");
Console.ReadKey();

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new UserGrpcService.UserGrpcServiceClient(channel);

var request = new GetUsersRequest();
var response = await client.GetUsersAsync(request);

foreach(var item in response.Users)
{
    Console.WriteLine($"{item.FirstName} {item.LastName} <{item.Email}>");
}

Console.ReadKey();