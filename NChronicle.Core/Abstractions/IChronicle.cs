using System;

namespace KSharp.NChronicle.Core.Abstractions
{

    /// <summary>
    /// A container for primary recording methods.
    /// </summary>
    public interface IChronicle
    {

        /// <summary>
        /// The current <see cref="IChronicleScope"/> for the calling thread.
        /// </summary>
        IChronicleScope CurrentScope { get; }

        /// <summary>
        /// Create a new child scope with the given <paramref name="scopeName"/> (optional) 
        /// from the current scope, and set it as the current scope, increasing the 
        /// verbosity level of records created on this thread.
        /// </summary>
        /// <param name="scopeName">The name for the new scope.</param>
        IChronicleScope ScopeIn(string scopeName = null);

        /// <summary>
        /// Restore the current scope to the given <paramref name="scope"/>, setting
        /// the verbosity level of records created on this thread to that of the given <paramref name="scope"/>.
        /// Use this when using scopes in a multi-threaded and/or asynchronous context.
        /// </summary>
        /// <param name="scope">The name for the new scope.</param>
        void ScopeIn(IChronicleScope scope);

        /// <summary>
        /// Restore the parent of the current scope as the new current scope, 
        /// decreasing the verbosity level of records created on this thread.
        /// </summary>
        void ScopeOut();

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Emergency"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Emergency(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Emergency"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Emergency(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Emergency"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Emergency(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Fatal"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Fatal(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Fatal"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Fatal(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Fatal"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Fatal(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Critical"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Critical(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Critical"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Critical(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Critical"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Critical(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Warning"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Warning(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Warning"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Warning(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Warning"/> level <see cref="Exception"/> with specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Warning(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Debug"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Debug(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Debug"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Debug(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Debug"/> level exception with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Debug(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Success"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Success(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Success"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Success(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Success"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Success(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Info"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Info(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Info"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Info(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Info"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Info(Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Trace"/> level message with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Trace(string message, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Trace"/> level message and <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Trace(string message, Exception exception, params string[] tags);

        /// <summary>
        /// Record a <see cref="Model.ChronicleLevel.Trace"/> level <see cref="Exception"/> with the specified <paramref name="tags"/>.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be recorded.</param>
        /// <param name="tags">Tags to be appended to this record.</param>
        void Trace(Exception exception, params string[] tags);

    }

}
