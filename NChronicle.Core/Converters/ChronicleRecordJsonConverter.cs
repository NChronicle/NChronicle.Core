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
            var chronicleRecord = new ChronicleRecord();
            var record = JObject.Load(reader);

            var threadId = record["threadId"];
            var utcTime = record["utcTime"];
            var exception = record["exception"];
            var level = record["level"];
            var message = record["message"];
            var tags = record["tags"];

            if (threadId != null)
                chronicleRecord.ThreadId = threadId.Value<int>();
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
            var record = value as ChronicleRecord;
            var json = new JObject();

            json.Add("threadId", JToken.FromObject(record.ThreadId));
            json.Add("utcTime", JToken.FromObject(record.UtcTime));
            json.Add("level", JToken.FromObject(record.Level));
            if (record.Exception != null)
                json.Add("exception", JToken.FromObject(record.Exception));
            if (record.Message != null)
                json.Add("message", JToken.FromObject(record.Message));
            if (record.Tags != null)
                json.Add("tags", JToken.FromObject(record.Tags));

            json.WriteTo(writer);
        }
    }
}
