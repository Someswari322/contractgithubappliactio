using azureapplicationdemo.api.Controllers;
using azureapplicationdemo.api.Services;
using azureapplicationdemo.api.Services.AzureTable;
using azureapplicationdemo.api.Services.BlogStorage;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:queuecs"]);
    clientBuilder.AddTableServiceClient(builder.Configuration["ConnectionStrings:table"]);
    clientBuilder.AddBlobServiceClient(builder.Configuration["ConnectionStrings:blob"]);

});
builder.Services.AddScoped<IAzureinterfacebusservice, Azureinterfacebusservice>();
builder.Services.AddScoped<IAzurestorageTableservice, AzurestorageTableservice>();
builder.Services.AddScoped<IAzureBlogService, AzureBlogServices>();

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
