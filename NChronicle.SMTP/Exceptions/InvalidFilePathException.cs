using System;

namespace NChronicle.File.Exceptions {

    /// <summary>
    /// The exception that is thrown when the specified file path is invalid. 
    /// </summary>
    public class InvalidFilePathException : Exception {

        /// <summary>
        /// Create a new <see cref="InvalidFilePathException"/> instance with
        /// the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        public InvalidFilePathException (string message) : base(message) {}

        /// <summary>
        /// Create a new <see cref="InvalidFilePathException"/> instance with 
        /// the specified <paramref name="message"/> and <paramref name="innerException"/>.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public InvalidFilePathException (string message, Exception innerException) : base(message, innerException) {}

    }

}