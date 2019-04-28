using Microsoft.VisualStudio.TestTools.UnitTesting;
using NChronicle.Core.Interfaces;
using NChronicle.Core.Model;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NChronicle.Core.Tests.ForChronicle
{
    public abstract class WhenUsingAChronicleTestBase
    {

        protected static IEnumerable<object[]> _chronicleLevel => Enum.GetNames(typeof(ChronicleLevel)).Select(levelName => new object[] { Enum.Parse<ChronicleLevel>(levelName) });

        protected IChronicleLibrary _fakeLibrary;
        protected Chronicle _chronicle;
        protected string _message;
        protected string[] _tags;
        protected IChronicleRecord _receivedRecord;

        [TestInitialize]
        public virtual void Init()
        {
            this._fakeLibrary = Substitute.For<IChronicleLibrary>();
            NChronicle.Configure(c => c.WithLibrary(_fakeLibrary)); // todo: Shouldn't be needed to test Chronicle
            this._chronicle = new Chronicle();

            // Arrange
            this._fakeLibrary.Handle(Arg.Do<IChronicleRecord>(record => this._receivedRecord = record));
            this._message = "This is a test message.";
            this._tags = new[] { "test1", "Tr1angl3" };
        }

        protected void CallAction(ChronicleLevel level, string message = null, Exception exception = null, params string[] tags)
        {
            if (message != null && exception != null)
            {
                switch (level)
                {
                    case ChronicleLevel.Critical: this._chronicle.Critical(message, exception, tags); break;
                    case ChronicleLevel.Debug: this._chronicle.Debug(message, exception, tags); break;
                    case ChronicleLevel.Info: this._chronicle.Info(message, exception, tags); break;
                    case ChronicleLevel.Success: this._chronicle.Success(message, exception, tags); break;
                    case ChronicleLevel.Warning: this._chronicle.Warning(message, exception, tags); break;
                }
            }
            else if (message != null)
            {
                switch (level)
                {
                    case ChronicleLevel.Critical: this._chronicle.Critical(message, tags); break;
                    case ChronicleLevel.Debug: this._chronicle.Debug(message, tags); break;
                    case ChronicleLevel.Info: this._chronicle.Info(message, tags); break;
                    case ChronicleLevel.Success: this._chronicle.Success(message, tags); break;
                    case ChronicleLevel.Warning: this._chronicle.Warning(message, tags); break;
                }
            }
            else if (exception != null)
            {
                switch (level)
                {
                    case ChronicleLevel.Critical: this._chronicle.Critical(exception, tags); break;
                    case ChronicleLevel.Debug: this._chronicle.Debug(exception, tags); break;
                    case ChronicleLevel.Info: this._chronicle.Info(exception, tags); break;
                    case ChronicleLevel.Success: this._chronicle.Success(exception, tags); break;
                    case ChronicleLevel.Warning: this._chronicle.Warning(exception, tags); break;
                }
            }
        }

    }
}
