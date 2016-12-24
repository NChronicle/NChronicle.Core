using NChronicle.SMTP.Configuration;

namespace NChronicle.SMTP.Delegates {

    /// <summary>
    /// A function to configure a <see cref="SmtpChronicleLibrary"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="SmtpChronicleLibrary"/> configuration.</param>
    public delegate void SmtpChronicleLibraryConfigurationDelegate (SmtpChronicleLibraryConfiguration configuration);

}