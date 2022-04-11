using ContoPizzaApi.Models;
using ContoPizzaApi.Services;
using ContoPizzaApi.Interfaces;
using MediatR;
using Microsoft.Extensions.Azure;



var builder = WebApplication.CreateBuilder(args);

var MyPolicy = "MyPolicy";

//builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyPolicy,
                      builder =>
                      {
                          builder.WithOrigins("*")
                          .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                          .WithHeaders("Accept", "Accept-Language", "Content-Language", "Content-Type")
                          .SetIsOriginAllowedToAllowWildcardSubdomains();
                      });
});

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<AzureSettings>(
    builder.Configuration.GetSection("AzureSettings"));

builder.Services.AddSingleton<IPizzaService,PizzaService>();
builder.Services.AddSingleton<ISandwitchService,SandwitchService>();
builder.Services.AddSingleton<IBeverageService,BeverageService>();
builder.Services.AddSingleton<IBackupServiceBeforeDelete, MemoryServiceBeforeDelete>();
builder.Services.AddSingleton<IBackupServiceOnCreate, MemoryServiceOnCreate>();
builder.Services.AddSingleton<IBackupServiceBlob, MemoryPizzaServiceAzureBlob>();
builder.Services.AddSingleton<IBackupServiceFile, MemoryPizzaServiceAzureFile>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["AzureSettings:ConnectionURI:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["AzureSettings:ConnectionURI:queue"], preferMsi: true);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors( options => options.WithOrigins("https://localhost:7095").AllowAnyMethod() );

app.UseCors(MyPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
