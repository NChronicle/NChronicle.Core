using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSharp.NChronicle.Core.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;

namespace KSharp.NChronicle.Core.Tests.ForChronicleRecord
{

    [TestClass]
    public partial class WhenUsingChronicleRecord
    {

        [TestMethod]
        public void ThereAreNoPublicSetters()
        {
            PropertyInfo[] properties = typeof(ChronicleRecord).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Assert.IsTrue(properties.All(p => p.SetMethod == null || !p.SetMethod.IsPublic));
        }

        [TestMethod]
        public void ThereAreNoPublicNonReadOnlyFields()
        {
            FieldInfo[] fields = typeof(ChronicleRecord).GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            Assert.IsTrue(fields.All(f => f.IsInitOnly));
        }

        [TestMethod]
        public void ItCanBeSerializedToJson()
        {
            (string json, _) = GetSerializedChronicleRecord();

            Assert.IsNotNull(json, "The ChronicleRecord completed serializing but there was no output");
        }

        [TestMethod]
        public void ItCanBeDeserializedFromJson()
        {
            // Arrange
            (string json, ChronicleRecord originalRecord) = GetSerializedChronicleRecord();

            // Act
            ChronicleRecord deserializedRecord = Task.Run(() => JsonConvert.DeserializeObject<ChronicleRecord>(json)).Result;

            // Assert
            Assert.IsNotNull(deserializedRecord, "The record was not deserialized at all.");
            Assert.IsNotNull(deserializedRecord.Message, "The message was not deserialized.");
            Assert.IsNotNull(deserializedRecord.Exception, "The exception was not deserialized.");
            Assert.IsNotNull(deserializedRecord.Tags, "The tags were not deserialized.");

            Assert.AreEqual(originalRecord.ThreadId, deserializedRecord.ThreadId, "The message was deserialized but incorrectly; the ThreadId was incorrect.");
            Assert.AreEqual(originalRecord.Message, deserializedRecord.Message, "The message was deserialized but incorrectly; the message was incorrect.");
            Assert.AreEqual(originalRecord.UtcTime, deserializedRecord.UtcTime, "The time was not deserialized correctly; the time was incorrect.");
            Assert.AreEqual(originalRecord.Level, deserializedRecord.Level, "The level was not deserialized correctly; the level was incorrect.");
            Assert.AreEqual(originalRecord.Verbosity, deserializedRecord.Verbosity, "The message was deserialized but incorrectly; the verbosity was incorrect.");
            CollectionAssert.AreEqual(originalRecord.ScopeStack as ICollection, deserializedRecord.ScopeStack as ICollection, "The message was deserialized but incorrectly; the scope stack was incorrect.");
            CollectionAssert.AreEquivalent(originalRecord.Tags as ICollection, deserializedRecord.Tags as ICollection, "The message was deserialized but incorrectly; the tags were incorrect.");

            Assert.AreEqual(originalRecord.Exception.Message, deserializedRecord.Exception.Message, "The exception was deserialized but incorrectly; the message is incorrect.");
            Assert.AreEqual(originalRecord.Exception.HResult, deserializedRecord.Exception.HResult, "The exception was deserialized but incorrectly; the HResult is incorrect.");
            Assert.AreEqual(originalRecord.Exception.HelpLink, deserializedRecord.Exception.HelpLink, "The exception was deserialized but incorrectly; the help link is incorrect.");
            Assert.AreEqual(originalRecord.Exception.StackTrace, deserializedRecord.Exception.StackTrace, "The exception was deserialized but incorrectly; the stack trace is incorrect.");
            Assert.AreEqual(originalRecord.Exception.Source, deserializedRecord.Exception.Source, "The exception was deserialized but incorrectly; the source is incorrect.");

            Assert.IsNotNull(deserializedRecord.Exception.InnerExceptions, "The inner exceptions were not deserialized.");

            Assert.AreEqual(originalRecord.Exception.InnerExceptions.First().Message, deserializedRecord.Exception.InnerExceptions.First().Message, "The inner exception was deserialized but incorrectly; the message is incorrect.");
            Assert.AreEqual(originalRecord.Exception.InnerExceptions.First().HResult, deserializedRecord.Exception.InnerExceptions.First().HResult, "The inner exception was deserialized but incorrectly; the HResult is incorrect.");
            Assert.AreEqual(originalRecord.Exception.InnerExceptions.First().HelpLink, deserializedRecord.Exception.InnerExceptions.First().HelpLink, "The inner exception was deserialized but incorrectly; the help link is incorrect.");
            Assert.AreEqual(originalRecord.Exception.InnerExceptions.First().StackTrace, deserializedRecord.Exception.InnerExceptions.First().StackTrace, "The inner exception was deserialized but incorrectly; the stack trace is incorrect.");
            Assert.AreEqual(originalRecord.Exception.InnerExceptions.First().Source, deserializedRecord.Exception.InnerExceptions.First().Source, "The inner exception was deserialized but incorrectly; the source is incorrect.");
        }

        private static (string, ChronicleRecord) GetSerializedChronicleRecord()
        {
            string message = "This is a test message";
            Exception exception;
            try
            {
                try
                {
                    throw new FileNotFoundException("Couldn't find it");
                }
                catch (Exception e)
                {
                    e.Source = "Inner Test Source";
                    e.HelpLink = "https://fake.inner.help.link.com/";
                    e.Data.Add("InnerKey0", "InnerValueO");
                    e.Data.Add("InnerKey1", "InnerValue1");
                    throw new IOException(null, e);
                }
            }
            catch (Exception e)
            {
                e.Source = "Test Source";
                e.HelpLink = "https://fake.help.link.com/";
                e.Data.Add("Key0", "ValueO");
                e.Data.Add("Key1", "Value1");
                exception = e;
            }

            string[] tags = new[] { "Tag1", "Tag2" };
            ChronicleLevel level = ChronicleLevel.Critical;

            ChronicleRecord chronicleRecord = new ChronicleRecord(level, message, exception, new [] { "ascope", "aninnerscope" }, tags);

            return (JsonConvert.SerializeObject(chronicleRecord), chronicleRecord);
        }


    }
}
