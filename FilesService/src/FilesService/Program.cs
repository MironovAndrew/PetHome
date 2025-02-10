using FilesService.Core.Loggers;
using FilesService.Core.Validation;
using FilesService.Extentions;
using FilesService.Infrastructure.MongoDB;
using MongoDB.Driver;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();
Log.Logger = SeqLogger.InitDefaultSeqConfiguration(builder.Configuration);

// Add services to the container.

builder.Services.AddEndpoints();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAmazonS3();

//Auto validation
builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

builder.Services.AddCors();

//����������� Mongo
builder.Services.AddSingleton<IMongoClient>(new MongoClient(
    builder.Configuration.GetConnectionString("Mongo")));

builder.Services.AddScoped<MongoDbContext>();
builder.Services.AddScoped<MongoDbRepository>();


var app = builder.Build();

//�������� CORS
app.AddCORS("http://localhost:5173");

//Middleware ��� ������ ���������� (-���� �����)
app.UseExceptionHandler();

app.UseSerilogRequestLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 

app.MapEndpoints();

app.Run();

