using NChronicle.Core.Model;

namespace NChronicle.File.Extensions {

    /// <summary>
    /// Container for configuration extension methods. 
    /// </summary>
    public static class ConfigurationExtensions {

        /// <summary>
        /// Create and add a <see cref="FileChronicleLibrary"/> to the specified <see cref="ChronicleConfiguration"/>.
        /// </summary>
        /// <param name="config">The <see cref="ChronicleConfiguration"/> for which to add the <see cref="FileChronicleLibrary"/>.</param>
        /// <returns>The created <see cref="FileChronicleLibrary"/>.</returns>
        public static FileChronicleLibrary WithFileLibrary (this ChronicleConfiguration config) {
            var lib = new FileChronicleLibrary();
            config.WithLibrary(lib);
            return lib;
        }

    }

}