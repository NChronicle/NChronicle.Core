using System;

namespace NChronicle.Core.Interfaces {

    public interface IChronicle {

        void Critical(string message, params string[] tags);

        void Critical(string message, Exception exception, params string[] tags);

        void Critical(Exception exception, params string[] tags);

        void Warning(string message, params string[] tags);

        void Warning(string message, Exception exception, params string[] tags);

        void Warning(Exception exception, params string[] tags);

        void Debug(string message, params string[] tags);

        void Debug(string message, Exception exception, params string[] tags);

        void Debug(Exception exception, params string[] tags);

        void Info(string message, params string[] tags);

        void Info(string message, Exception exception, params string[] tags);

        void Info(Exception exception, params string[] tags);

    }

}
