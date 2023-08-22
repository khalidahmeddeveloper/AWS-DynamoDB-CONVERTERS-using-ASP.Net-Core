using Amazon.DynamoDBv2.DataModel;
using System.Text.Json.Serialization;

namespace DynamoDBTest
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public string Date { get; set; }

        public Temperature Temperature { get; set; }
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public Wind Wind { get; set; }
        //we define the weathertype injection at context level so no need for this
       // [JsonConverter(typeof(JsonStringEnumConverter))]
       // [DynamoDBProperty(Converter= typeof(JsonStringEnumConverter))]
        public WeatherType WeatherType { get; set; }
    }

    public class Wind
    {
        public decimal Speed { get; set; }
        public string Direction { get; set; }
       
        public WeatherType WeatherType { get; set; }
    }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WeatherType
    {
        None,
        Sunny,
        Rainy,
        Cloudy,
        Windy,
        Stormy
    }
}