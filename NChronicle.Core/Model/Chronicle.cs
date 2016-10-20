using System;
using System.Collections.Concurrent;
using System.Linq;
using NChronicle.Core.Interfaces;

namespace NChronicle.Core.Model {

    public class Chronicle : IChronicle {

        private ChronicleConfiguration _configuration;
        private ConcurrentBag <string> _tags;

        public Chronicle () {
            this._configuration = NChronicle.GetConfiguration();
            NChronicle.ConfigurationChanged += this.ConfigurationChangedHandler;
            this._tags = new ConcurrentBag <string>();
        }

        private void ConfigurationChangedHandler () {
            this._configuration = NChronicle.GetConfiguration();
        }

        #region Tags
        public void PersistTags (params string[] tags) {
            foreach (var tag in tags) {
                if (this._tags.Contains(tag)) continue;
                this._tags.Add(tag);
            }
        }

        public void ClearTags () {
            this._tags = new ConcurrentBag <string>();
        }
        #endregion

        #region Recorders

        #region Critical
        public void Critical (string message, params string[] tags) {
            this.Critical(message, null, tags);
        }

        public void Critical (Exception exception, params string[] tags) {
            this.Critical(exception.Message, exception, tags);
        }

        public void Critical (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Critical));
        }
        #endregion

        #region Warning
        public void Warning (string message, params string[] tags) {
            this.Warning(message, null, tags);
        }

        public void Warning (Exception exception, params string[] tags) {
            this.Warning(exception.Message, exception, tags);
        }

        public void Warning (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Warning));
        }
        #endregion

        #region Debug
        public void Debug (string message, params string[] tags) {
            this.Debug(message, null, tags);
        }

        public void Debug (Exception exception, params string[] tags) {
            this.Debug(exception.Message, exception, tags);
        }

        public void Debug (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Debug));
        }
        #endregion

        #region Success
        public void Success (string message, params string[] tags) {
            this.Success(message, null, tags);
        }

        public void Success (Exception exception, params string[] tags) {
            this.Success(exception.Message, exception, tags);
        }

        public void Success (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Success));
        }
        #endregion

        #region Info
        public void Info (string message, params string[] tags) {
            this.Info(message, null, tags);
        }

        public void Info (Exception exception, params string[] tags) {
            this.Info(exception.Message, exception, tags);
        }

        public void Info (string message, Exception exception, params string[] tags) {
            this.SendToLibraries(new ChronicleRecord(message, tags, exception, ChronicleLevel.Info));
        }
        #endregion

        #endregion

        private void SendToLibraries (ChronicleRecord record) {
            foreach (var tag in this._tags) {
                if (record.Tags.Contains(tag)) continue;
                (record.Tags as ConcurrentQueue<string>)?.Enqueue(tag);
            }
            foreach (var library in this._configuration.Libraries) {
                library.Store(record); 
            }
        }

        ~Chronicle () {
            NChronicle.ConfigurationChanged -= this.ConfigurationChangedHandler;
        }

    }

}