using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<UserManager>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<UserService>();

app.Run();