using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NChronicle.Core.Tests.ForChronicleException
{

    [TestClass]
    public partial class WhenUsingChronicleException
    {

        [TestMethod]
        public void ThereAreNoPublicSetters()
        {
            PropertyInfo[] properties = typeof(ChronicleException).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Assert.IsTrue(properties.All(p => p.SetMethod == null || !p.SetMethod.IsPublic));
        }

        [TestMethod]
        public void ThereAreNoPublicNonReadOnlyFields()
        {
            FieldInfo[] fields = typeof(ChronicleException).GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            Assert.IsTrue(fields.All(f => f.IsInitOnly));
        }

        [TestMethod]
        public void ItCanBeSerializedToJson()
        {
            (string json, _) = GetSerializedChronicleException();

            Assert.IsNotNull(json, "The ChronicleException completed serializing but there was no output");
        }

        [TestMethod]
        public void ItCanBeDeserializedFromJson()
        {
            // Arrange
            (string json, ChronicleException originalException) = GetSerializedChronicleException();

            // Act
            ChronicleException deserializedException = JsonConvert.DeserializeObject<ChronicleException>(json);

            // Assert
            Assert.IsNotNull(deserializedException, "The exception was not deserialized at all.");

            Assert.AreEqual(originalException.Message, deserializedException.Message, "The exception was deserialized but incorrectly; the message is incorrect.");
            Assert.AreEqual(originalException.HResult, deserializedException.HResult, "The exception was deserialized but incorrectly; the HResult is incorrect.");
            Assert.AreEqual(originalException.HelpLink, deserializedException.HelpLink, "The exception was deserialized but incorrectly; the help link is incorrect.");
            Assert.AreEqual(originalException.StackTrace, deserializedException.StackTrace, "The exception was deserialized but incorrectly; the stack trace is incorrect.");
            Assert.AreEqual(originalException.Source, deserializedException.Source, "The exception was deserialized but incorrectly; the source is incorrect.");

            Assert.IsNotNull(originalException.InnerExceptions, "The inner exceptions were not deserialized.");

            Assert.AreEqual(originalException.InnerExceptions.First().Message, deserializedException.InnerExceptions.First().Message, "The inner exception was deserialized but incorrectly; the message is incorrect.");
            Assert.AreEqual(originalException.InnerExceptions.First().HResult, deserializedException.InnerExceptions.First().HResult, "The inner exception was deserialized but incorrectly; the HResult is incorrect.");
            Assert.AreEqual(originalException.InnerExceptions.First().HelpLink, deserializedException.InnerExceptions.First().HelpLink, "The inner exception was deserialized but incorrectly; the help link is incorrect.");
            Assert.AreEqual(originalException.InnerExceptions.First().StackTrace, deserializedException.InnerExceptions.First().StackTrace, "The inner exception was deserialized but incorrectly; the stack trace is incorrect.");
            Assert.AreEqual(originalException.InnerExceptions.First().Source, deserializedException.InnerExceptions.First().Source, "The inner exception was deserialized but incorrectly; the source is incorrect.");
        }

        private static (string, ChronicleException) GetSerializedChronicleException()
        {
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

            ChronicleException chronicleException = new ChronicleException(exception);

            return (JsonConvert.SerializeObject(chronicleException), chronicleException);
        }


    }
}
