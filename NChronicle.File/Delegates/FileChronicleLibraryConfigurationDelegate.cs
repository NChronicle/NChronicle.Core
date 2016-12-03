using NChronicle.File.Configuration;

namespace NChronicle.File.Delegates {

    /// <summary>
    /// A function to configure a <see cref="FileChronicleLibrary"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="FileChronicleLibrary"/> configuration.</param>
    public delegate void FileChronicleLibraryConfigurationDelegate (FileChronicleLibraryConfiguration configuration);

}