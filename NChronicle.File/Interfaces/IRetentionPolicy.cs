namespace NChronicle.File.Interfaces {

    /// <summary>
    /// A file retention policy for controlling the archiving of a
    /// <see cref="FileChronicleLibrary"/>'s output file. 
    /// </summary>
    public interface IRetentionPolicy {

        /// <summary>
        /// Check whether the output file at the given <paramref name="path"/> should 
        /// have the file retention policy invoked upon it. 
        /// </summary>
        /// <param name="path">The path to the output file.</param>
        /// <param name="pendingBytes">Any pending bytes that are to be written to the output file.</param>
        /// <returns>A <see cref="bool"/> indicating if the policy should be invoked.</returns>
        bool CheckPolicy (string path, byte[] pendingBytes);

        /// <summary>
        /// Invoke the file retention policy on the output file at 
        /// the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the output file.</param>
        void InvokePolicy (string path);

    }

}