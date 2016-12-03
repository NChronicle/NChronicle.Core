using NChronicle.Core.Model;

namespace NChronicle.Console.Extensions {

    /// <summary>
    /// Container for configuration extension methods. 
    /// </summary>
    public static class ConfigurationExtensions {

        /// <summary>
        /// Create and add a <see cref="ConsoleChronicleLibrary"/> to the specified <see cref="ChronicleConfiguration"/>.
        /// </summary>
        /// <param name="config">The <see cref="ChronicleConfiguration"/> for which to add the <see cref="ConsoleChronicleLibrary"/>.</param>
        /// <returns>The created <see cref="ConsoleChronicleLibrary"/>.</returns>
        public static ConsoleChronicleLibrary WithConsoleLibrary (this ChronicleConfiguration config) {
            var lib = new ConsoleChronicleLibrary();
            config.WithLibrary(lib);
            return lib;
        }

    }

}