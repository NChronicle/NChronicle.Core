using NChronicle.File.Configuration;

namespace NChronicle.File.Delegates {

    /// <summary>
    /// A function to configure a <see cref="RetentionPolicy"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="RetentionPolicy"/> configuration.</param>
    public delegate void RetentionPolicyConfigurationDelegate (RetentionPolicyConfiguration configuration);

}