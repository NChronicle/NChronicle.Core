using KSharp.NChronicle.Core.Model;

namespace NChronicle.Core.Delegates {

    /// <summary>
    /// A function to set <see cref="ChronicleConfiguration"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="ChronicleConfiguration"/> to configure.</param>
    public delegate void ChronicleConfigurationDelegate (ChronicleConfiguration configuration);

}