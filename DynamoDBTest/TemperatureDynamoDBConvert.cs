using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBTest
{
    public class TemperatureDynamoDBConvert : IPropertyConverter
    {
        object IPropertyConverter.FromEntry(DynamoDBEntry entry)
        {
            if (entry != null)
            {
                var tempString = entry.ToString();
                var tempStrings = tempString.Split("°", StringSplitOptions.RemoveEmptyEntries);
                var unit = tempStrings[1] switch
                {
                    "C" => TemperatureType.Celsius,
                    "F" => TemperatureType.Farenheit,
                    _ => throw new Exception("Invalid Temperature Type")
                };

                return new Temperature(Decimal.Parse(tempStrings[0]), unit);

            }

            return null;
        }

        DynamoDBEntry IPropertyConverter.ToEntry(object value)
        {
            if (value is Temperature temperature)
            {
                var unit = temperature.TemperatureType == TemperatureType.Celsius ? "C" : "F";
                return new Primitive($"{temperature.Degree}°{unit}");
            }

            return null;
        }
    }
}
