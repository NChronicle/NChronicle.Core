using NChronicle.Core.Model;

namespace NChronicle.SMTP.Extensions {

    /// <summary>
    /// Container for configuration extension methods. 
    /// </summary>
    public static class ConfigurationExtensions {

        /// <summary>
        /// Create and add a <see cref="SmtpChronicleLibrary"/> to the specified <see cref="ChronicleConfiguration"/>.
        /// </summary>
        /// <param name="config">The <see cref="ChronicleConfiguration"/> for which to add the <see cref="SmtpChronicleLibrary"/>.</param>
        /// <returns>The created <see cref="SmtpChronicleLibrary"/>.</returns>
        public static SmtpChronicleLibrary WithSmtpLibrary (this ChronicleConfiguration config) {
            var lib = new SmtpChronicleLibrary();
            config.WithLibrary(lib);
            return lib;
        }

    }

}