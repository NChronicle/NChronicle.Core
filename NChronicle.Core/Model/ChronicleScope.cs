using System;
using KSharp.NChronicle.Core.Abstractions;

namespace KSharp.NChronicle.Core.Model
{

    public class ChronicleScope : IChronicle, IDisposable
    {

        private bool _disposed;
        private Chronicle _chronicle;

        internal ChronicleScope(Chronicle chronicle)
        {
            this._chronicle = chronicle;
        }

        public void Critical(string message, params string[] tags) 
            => this._chronicle.Critical(message, tags);

        public void Critical(string message, Exception exception, params string[] tags) 
            => this._chronicle.Critical(message, exception, tags);

        public void Critical(Exception exception, params string[] tags) 
            => this._chronicle.Critical(exception, tags);

        public void Debug(string message, params string[] tags) 
            => this._chronicle.Debug(message, tags);

        public void Debug(string message, Exception exception, params string[] tags) 
            => this._chronicle.Debug(message, exception, tags);

        public void Debug(Exception exception, params string[] tags) 
            => this._chronicle.Debug(exception, tags);

        public void Emergency(string message, params string[] tags)
            => this._chronicle.Emergency(message, tags);

        public void Emergency(string message, Exception exception, params string[] tags)
            => this._chronicle.Emergency(message, exception, tags);

        public void Emergency(Exception exception, params string[] tags)
            => this._chronicle.Emergency(exception, tags);

        public void Fatal(string message, params string[] tags)
            => this._chronicle.Fatal(message, tags);

        public void Fatal(string message, Exception exception, params string[] tags)
            => this._chronicle.Fatal(message, exception, tags);

        public void Fatal(Exception exception, params string[] tags)
            => this._chronicle.Fatal(exception, tags);

        public void Info(string message, params string[] tags)
            => this._chronicle.Info(message, tags);

        public void Info(string message, Exception exception, params string[] tags)
            => this._chronicle.Info(message, exception, tags);

        public void Info(Exception exception, params string[] tags)
            => this._chronicle.Info(exception, tags);

        public void Success(string message, params string[] tags)
            => this._chronicle.Success(message, tags);

        public void Success(string message, Exception exception, params string[] tags)
            => this._chronicle.Success(message, exception, tags);

        public void Success(Exception exception, params string[] tags)
            => this._chronicle.Success(exception, tags);

        public void Trace(string message, params string[] tags)
            => this._chronicle.Trace(message, tags);

        public void Trace(string message, Exception exception, params string[] tags)
            => this._chronicle.Trace(message, exception, tags);

        public void Trace(Exception exception, params string[] tags)
            => this._chronicle.Trace(exception, tags);

        public void Warning(string message, params string[] tags)
            => this._chronicle.Warning(message, tags);

        public void Warning(string message, Exception exception, params string[] tags)
            => this._chronicle.Warning(message, exception, tags);

        public void Warning(Exception exception, params string[] tags)
            => this._chronicle.Warning(exception, tags);

        #region IDisposable Support
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;
            this._chronicle.ScopeOut();
        }
        #endregion

    }

}