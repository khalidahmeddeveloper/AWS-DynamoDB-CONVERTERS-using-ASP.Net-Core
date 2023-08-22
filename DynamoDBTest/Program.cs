using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using DynamoDBTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var credentials = new BasicAWSCredentials("AKIA2K7Z23QMAFF5QUPL", "21Ke/yLkpZZN0oPALtT93KDHAIbd/Biz4tl+maoz");
var config = new AmazonDynamoDBConfig() { 
    RegionEndpoint = Amazon.RegionEndpoint.USEast1
};
var client = new AmazonDynamoDBClient(credentials, config);
builder.Services.AddSingleton<IAmazonDynamoDB>(client);

//this is required to weathertype direct inject into weathertypedynamoDBConverter
var context = new DynamoDBContext(client);
context.ConverterCache.Add(typeof(WeatherType), new WeatherTypeDynamoDbConverter());
context.ConverterCache.Add(typeof(Temperature), new TemperatureDynamoDBConvert());
builder.Services.AddSingleton<IDynamoDBContext>(context);

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
