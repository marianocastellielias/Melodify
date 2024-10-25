using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web.Converters
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Read();
                int hour = 0, minute = 0, second = 0;

                while (reader.TokenType != JsonTokenType.EndObject)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    if (propertyName == "hour")
                    {
                        hour = reader.GetInt32();
                    }
                    else if (propertyName == "minute")
                    {
                        minute = reader.GetInt32();
                    }
                    else if (propertyName == "second")
                    {
                        second = reader.GetInt32();
                    }

                    reader.Read();
                }

                return new TimeOnly(hour, minute, second);
            }

            throw new JsonException("Formato de TimeOnly inválido.");
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("hour", value.Hour);
            writer.WriteNumber("minute", value.Minute);
            writer.WriteNumber("second", value.Second);
            writer.WriteEndObject();
        }
    }
}

