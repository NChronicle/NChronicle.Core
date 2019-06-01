using System;
using KSharp.NChronicle.Core.Abstractions;

namespace KSharp.NChronicle.Core.Model
{

    public class ChronicleScope : IChronicleScope
    {

        public int Verbosity => this.Parent?.Verbosity + 1 ?? 1;
        public IChronicleScope Parent { get; }
        public IChronicle Chronicle { get; }

        private bool _disposed;

        internal ChronicleScope(Chronicle chronicle, IChronicleScope parent)
        {
            this.Chronicle = chronicle;
            this.Parent = parent;
        }

        #region IDisposable Support
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;
            this.Chronicle.ScopeOut();
        }
        #endregion

    }

}