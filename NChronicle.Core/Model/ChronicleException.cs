using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSharp.NChronicle.Core.Converters;
using Newtonsoft.Json;

namespace KSharp.NChronicle.Core.Model
{

    /// <summary>
    /// NChronicle wrapper for a recorded exception.
    /// </summary>
    [JsonConverter(typeof(ChronicleExceptionJsonConverter))]
    public class ChronicleException : IEquatable<ChronicleException>
    {

        /// <summary>
        /// The message that describes the exception.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// The HRESULT, a coded numerical value that is assigned to
        /// a specific exception.
        /// </summary>
        public int HResult { get; internal set; }

        /// <summary>
        /// The link to the help file associated with this exception.
        /// </summary>
        public string HelpLink { get; internal set; }

        /// <summary>
        /// The name of the application or the object
        /// that causes the error.
        /// </summary>
        public string Source { get; internal set; }

        /// <summary>
        /// Gets a string representation of the immediate frames on the call stack.
        /// </summary>
        public string StackTrace { get; internal set; }

        /// <summary>
        /// The collection of key/value pairs that provide additional
        /// user-defined information about the exception.
        /// </summary>
        public IDictionary Data { get; internal set; }

        /// <summary>
        /// The <see cref="Type"/> of the exception. 
        /// </summary>
        public Type ExceptionType { get; internal set; }

        /// <summary>
        /// The inner exceptions of this exception. If the <see cref="ExceptionType"/> is
        /// not an <see cref="AggregateException"/> there will be only one exception
        /// in this enumerable.
        /// </summary>
        public IEnumerable<ChronicleException> InnerExceptions { get; internal set; }

        internal ChronicleException() { }

        /// <summary>
        /// Create a new Chronicle Exception wrapping the given <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to wrap.</param>
        public ChronicleException(Exception exception)
        {
            this.ExceptionType = exception.GetType();
            this.Message = exception.Message;
            this.Source = exception.Source;
            this.HResult = exception.HResult;
            this.HelpLink = exception.HelpLink;
            this.StackTrace = exception.StackTrace;
            this.Data = exception.Data;
            if (exception is AggregateException aggException)
            {
                this.InnerExceptions = new ConcurrentBag<ChronicleException>(aggException.InnerExceptions.Select(e => new ChronicleException(e)));
            }
            if (exception.InnerException != null)
            {
                this.InnerExceptions = new ConcurrentBag<ChronicleException>(new[] { new ChronicleException(exception.InnerException) });
            }
        }

        /// <summary>
        /// Get a boolean value indicating whether this Chronicle Exception can
        /// be considered equal to <paramref name="object"/> by
        /// comparing all members.
        /// </summary>
        /// <param name="object">The other <see cref="Object"/> to compare to.</param>
        /// <returns>Whether this exception and <paramref name="object"/> are considered equal.</returns>
        public override bool Equals(object @object)
        {
            if (@object is ChronicleException chronicleException)
            {
                return this.Equals(chronicleException);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get a boolean value indicating whether this Chronicle Exception can
        /// be considered equal to <paramref name="anotherException"/> by
        /// comparing all members.
        /// </summary>
        /// <param name="anotherException">The other <see cref="ChronicleException"/> to compare to.</param>
        /// <returns>Whether this exception and <paramref name="anotherException"/> are considered equal.</returns>
        public bool Equals(ChronicleException anotherException)
        {
            return this.ExceptionType == anotherException.ExceptionType
                && this.HResult == anotherException.HResult
                && this.HelpLink == anotherException.HelpLink
                && this.Message == anotherException.Message
                && this.StackTrace == anotherException.StackTrace
                && this.Source == anotherException.Source
                && (this.InnerExceptions == null) == (anotherException.InnerExceptions == null)
                && (this.InnerExceptions == null || new HashSet<ChronicleException>(this.InnerExceptions).SetEquals(anotherException.InnerExceptions));
        }

        /// <summary>
        /// Get a Hash Code for this Chronicle Exception. The Hash Code is
        /// calculated from <see cref="Message"/>, <see cref="HResult"/>
        /// and <see cref="ExceptionType"/>.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + this.Message.GetHashCode();
                hash = hash * 23 + this.HResult.GetHashCode();
                hash = hash * 23 + this.ExceptionType.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns>A string representation of the current exception.</returns>
        public override string ToString() {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(this.HResult);
            stringBuilder.Append(": ");
            stringBuilder.Append(this.Message);
            if (!string.IsNullOrEmpty(this.StackTrace))
            {
                stringBuilder.AppendLine();
                stringBuilder.Append(this.StackTrace);
            }
            if (this.InnerExceptions?.Any() ?? false)
            {
                foreach (var exception in this.InnerExceptions)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.Append(exception);
                }
            }
            return stringBuilder.ToString();
        }

    }
}
