using NChronicle.Console.Configuration;

namespace NChronicle.Console.Delegates {

    /// <summary>
    /// A function to set <see cref="ConsoleChronicleLibrary"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="ConsoleChronicleLibrary"/> to configure.</param>
    public delegate void ConsoleChronicleLibraryConfigurationDelegate (ConsoleChronicleLibraryConfiguration configuration);

}