using System;

namespace KSharp.NChronicle.Core.Abstractions
{
    public interface IChronicleScope : IDisposable {

        int Verbosity { get; }

        IChronicleScope Parent { get; }

        IChronicle Chronicle { get; }

    }

}