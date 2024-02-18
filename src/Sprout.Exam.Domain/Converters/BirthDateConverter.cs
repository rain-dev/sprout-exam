
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sprout.Exam.Domain.Converters
{
    public class BirthDateConverter : JsonConverter<DateTime>
    {
        private const string FORMAT = "yyyy-MM-dd";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParseExact(reader.GetString(), FORMAT, null, System.Globalization.DateTimeStyles.None, out var birthDate))
                return birthDate;

            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(FORMAT));
        }
    }
}
