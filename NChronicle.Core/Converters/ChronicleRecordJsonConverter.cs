using System;
using System.Collections.ObjectModel;
using KSharp.NChronicle.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KSharp.NChronicle.Core.Converters
{
    internal class ChronicleRecordJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            typeof(ChronicleRecord) == objectType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ChronicleRecord chronicleRecord = new ChronicleRecord();
            JObject record = JObject.Load(reader);

            JToken utcTime = record["UtcTime"];
            JToken exception = record["Exception"];
            JToken level = record["Level"];
            JToken message = record["Message"];
            JToken tags = record["Tags"];

            if (utcTime != null)
                chronicleRecord.UtcTime = utcTime.ToObject<DateTime>();
            if (exception != null)
                chronicleRecord.Exception = exception.ToObject<ChronicleException>(serializer);
            if (level != null)
                chronicleRecord.Level = level.ToObject<ChronicleLevel>();
            if (message != null)
                chronicleRecord.Message = message.Value<string>();
            if (tags != null)
                chronicleRecord.Tags = tags.ToObject<ReadOnlyCollection<string>>();

            return chronicleRecord;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ChronicleRecord record = value as ChronicleRecord;
            JObject json = new JObject();

            json.Add("UtcTime", JToken.FromObject(record.UtcTime));
            json.Add("Level", JToken.FromObject(record.Level));
            if (record.Exception != null)
                json.Add("Exception", JToken.FromObject(record.Exception));
            if (record.Message != null)
                json.Add("Message", JToken.FromObject(record.Message));
            if (record.Tags != null)
                json.Add("Tags", JToken.FromObject(record.Tags));

            json.WriteTo(writer);
        }
    }
}
