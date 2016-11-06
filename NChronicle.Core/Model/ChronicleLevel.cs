namespace NChronicle.Core.Model {

    /// <summary>
    /// Level/severity of a Chronicle record
    /// </summary>
    public enum ChronicleLevel {

        /// <summary>
        /// A critical level record - best used in events of paramount importance.
        /// </summary>
        Critical,

        /// <summary>
        /// A warning level record - best used with unexpected exceptions.
        /// </summary>
        Warning,

        /// <summary>
        /// A success level record - best used in events of notable success.
        /// </summary>
        Success,

        /// <summary>
        /// A informational level record - best used with notable information.
        /// </summary>
        Info,

        /// <summary>
        /// A debug level record - best used with less notable information for later debugging.
        /// </summary>
        Debug

    }

}