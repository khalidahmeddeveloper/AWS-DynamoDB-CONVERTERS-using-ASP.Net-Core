using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBTest
{
    public class WeatherTypeDynamoDbConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
           return Enum.Parse<WeatherType>(entry.AsString());
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return new Primitive(value.ToString());
        }
    }
}
