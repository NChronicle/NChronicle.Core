using System;
using System.Collections.Generic;
using NChronicle.Core.Interfaces;

namespace NChronicle.Core.Model {

    public class Chronicle : IChronicle {

        private ChronicleConfiguration _configuration;
        private readonly HashSet <string> _tags;

        public Chronicle () {
            this._configuration = NChronicle.GetConfiguration();
            NChronicle.SubscribeToChanges(this.ConfigurationSubscriber);
            this._tags = new HashSet <string>();
        }

        private void ConfigurationSubscriber () {
            this._configuration = NChronicle.GetConfiguration();
        }

        #region Tags
        public void PersistTags (params string[] tags) {
            foreach (var tag in tags) {
                this._tags.Add(tag);
            }
        }

        public void ClearTags () {
            this._tags.Clear();
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
            this.SendToLibraries
                (new ChronicleRecord {
                    Message = message,
                    Tags = new HashSet <string>(tags),
                    Exception = exception,
                    Level = ChronicleLevel.Critical
                });
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
            this.SendToLibraries
                (new ChronicleRecord {
                    Message = message,
                    Tags = new HashSet <string>(tags),
                    Exception = exception,
                    Level = ChronicleLevel.Warning
                });
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
            this.SendToLibraries
                (new ChronicleRecord {
                    Message = message,
                    Tags = new HashSet <string>(tags),
                    Exception = exception,
                    Level = ChronicleLevel.Debug
                });
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
            this.SendToLibraries
                (new ChronicleRecord {
                    Message = message,
                    Tags = new HashSet <string>(tags),
                    Exception = exception,
                    Level = ChronicleLevel.Success
            });
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
            this.SendToLibraries
                (new ChronicleRecord {
                    Message = message,
                    Tags = new HashSet <string>(tags),
                    Exception = exception,
                    Level = ChronicleLevel.Info
                });
        }
        #endregion

        #endregion

        private void SendToLibraries (ChronicleRecord record) {
            record.Tags.UnionWith(this._tags);
            foreach (var library in this._configuration.Libraries) {
                library.Store(record); 
            }
        }

    }

}