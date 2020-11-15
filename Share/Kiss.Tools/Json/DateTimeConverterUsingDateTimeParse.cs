using System;
#if NET45
    using Newtonsoft.Json.Converters;
#else
    using System.Text.Json;
    using System.Text.Json.Serialization;
#endif

namespace Kiss.Tools.Json
{
    public class JsonParse
    {
#if NET45
        public IsoDateTimeConverter TimeConverter
        {
            get
            {
                var timeConverter = new IsoDateTimeConverter {DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"};
                return timeConverter;
            }
        }
#else
        public JsonSerializerOptions TimeConverter
        {
            get
            {
                var timeConverter = new JsonSerializerOptions();
                timeConverter.Converters.Add(new DateTimeConverterUsingDateTimeParse());
                return timeConverter;
            }
        }
#endif
    }

#if !NET45
    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
#endif
}