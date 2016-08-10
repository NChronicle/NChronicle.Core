using NChronicle.Core.Delegates;
using NChronicle.Core.Model;

namespace NChronicle.Core {

    public static class NChronicle {

        internal static ChronicleConfiguration Configuration { get; private set; }

        static NChronicle() {
            Configuration = new ChronicleConfiguration();
        }

        public static void Configure (ChronicleConfigurationDelegate configurationDelegate) {
            configurationDelegate.Invoke(Configuration);
        }

    }

}