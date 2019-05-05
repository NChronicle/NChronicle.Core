using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KSharp.NChronicle.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KSharp.NChronicle.Core.Converters
{
    internal class ChronicleExceptionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            typeof(ChronicleException) == objectType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ChronicleException chronicleException = new ChronicleException();
            JObject exception = JObject.Load(reader);

            JToken message = exception["Message"];
            JToken hResult = exception["HResult"];
            JToken helpLink = exception["HelpLink"];
            JToken source = exception["Source"];
            JToken stackTrace = exception["StackTrace"];
            JToken data = exception["Data"];
            JToken exceptionType = exception["ExceptionType"];
            JToken innerExceptions = exception["InnerExceptions"];

            if (message != null)
                chronicleException.Message = message.Value<string>();
            if (hResult != null)
                chronicleException.HResult = hResult.Value<int>();
            if (helpLink != null)
                chronicleException.HelpLink = helpLink.Value<string>();
            if (source != null)
                chronicleException.Source = source.Value<string>();
            if (stackTrace != null)
                chronicleException.StackTrace = stackTrace.Value<string>();
            if (data != null)
                chronicleException.Data = data.ToObject<Dictionary<string, string>>(serializer);
            if (exceptionType != null)
                chronicleException.ExceptionType = exceptionType.ToObject<Type>(serializer);
            if (innerExceptions != null)
                chronicleException.InnerExceptions = innerExceptions.ToObject<ReadOnlyCollection<ChronicleException>>(serializer);

            return chronicleException;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ChronicleException excpeption = value as ChronicleException;
            JObject json = new JObject();

            json.Add("HResult", JToken.FromObject(excpeption.HResult));
            if (excpeption.Message != null)
                json.Add("Message", JToken.FromObject(excpeption.Message));
            if (excpeption.HelpLink != null)
                json.Add("HelpLink", JToken.FromObject(excpeption.HelpLink));
            if (excpeption.Source != null)
                json.Add("Source", JToken.FromObject(excpeption.Source));
            if (excpeption.StackTrace != null)
                json.Add("StackTrace", JToken.FromObject(excpeption.StackTrace));
            if (excpeption.Data != null)
                json.Add("Data", JToken.FromObject(excpeption.Data));
            if (excpeption.ExceptionType != null)
                json.Add("ExceptionType", JToken.FromObject(excpeption.ExceptionType));
            if (excpeption.InnerExceptions != null)
                json.Add("InnerExceptions", JToken.FromObject(excpeption.InnerExceptions));

            json.WriteTo(writer);

        }
    }
}
