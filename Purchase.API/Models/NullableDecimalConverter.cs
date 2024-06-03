using System.Text.Json.Serialization;
using System.Text.Json;

namespace Purchase.API.Models
{
    public class NullableDecimalConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }


            if (reader.TokenType == JsonTokenType.Number && reader.TryGetDecimal(out decimal value))
            {
                return value;
            }
            if (decimal.TryParse(reader.GetString(), out value))
            {
                return value;
            }


            // Handle other cases, such as empty strings or invalid formats, if necessary
            if (reader.TokenType == JsonTokenType.String && string.IsNullOrWhiteSpace(reader.GetString()))
            {
                return null;
            }

            throw new JsonException($"Unable to convert \"{reader.GetString()}\" to a nullable decimal.");
        }

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
