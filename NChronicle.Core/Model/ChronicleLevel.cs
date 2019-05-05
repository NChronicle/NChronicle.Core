namespace KSharp.NChronicle.Core.Model
{

    /// <summary>
    /// Level/severity of a Chronicle record
    /// </summary>
    public enum ChronicleLevel {

        /// <summary>
        /// An emergency level record - best used only in catastrophic events that indicate real world emergency such as significant financial loss or threat to life.
        /// </summary>
        Emergency,

        /// <summary>
        /// A fatal level record - best used in events that induce complete application/service failure.
        /// </summary>
        Fatal,

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
        Debug,

        /// <summary>
        /// A trace level record - best used with high verbosity and/or meticulous messages.
        /// </summary>
        Trace

    }

}