using ContoPizzaApi.Models;
using ContoPizzaApi.Services;

using Azure.Storage.Blobs;
using ContoPizzaApi.Interfaces;





//var blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=dejan24;AccountKey=iVlQRfDY0yvjOwGesD/3uy5f+ujnnE3BcQdwvk6MtxBmqy8bBt3xM9u22OCWX4If8dOc4e+V9TJlSYCT5eOjfw==;EndpointSuffix=core.windows.net";
//var blobStorageContainerName = "DeletedPizzas";

//var container = new BlobContainerClient(blobStorageConnectionString, blobStorageContainerName);
//var blob = container.GetBlobClient();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<AzureSettings>(
    builder.Configuration.GetSection("AzureSettings"));

builder.Services.AddSingleton<PizzasService>();
//builder.Services.AddSingleton<IBackupService,MemoryPizzaService>();
//builder.Services.AddSingleton<IBackupService,MemoryPizzaServiceAzureBlob>();
builder.Services.AddSingleton<IBackupService, MemoryPizzaServiceAzureFile>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
