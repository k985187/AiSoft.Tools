using System;
using AiSoft.Tools.Extensions;
using Newtonsoft.Json;

namespace AiSoft.Tools.Converters
{
    public class StringEncryptConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var val = serializer.Deserialize<string>(reader);
            if (!string.IsNullOrWhiteSpace(val))
            {
                return val.DecryptTo();
            }
            return val;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is string val && !string.IsNullOrWhiteSpace(val))
            {
                writer.WriteValue(val.EncryptTo());
                return;
            }
            writer.WriteValue(value);
        }

        public override bool CanConvert(Type type)
        {
            return typeof(string) == type;
        }
    }
}