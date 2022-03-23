using ContoPizzaApi.Models;
using ContoPizzaApi.Services;
using ContoPizzaApi.Interfaces;







var builder = WebApplication.CreateBuilder(args);

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
