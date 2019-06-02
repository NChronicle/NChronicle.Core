using System.Collections;
using System.Collections.Generic;
using KSharp.NChronicle.Core.Abstractions;

namespace KSharp.NChronicle.Core.Model
{

    /// <summary>
    /// A scope in which to create more verbose <see cref="IChronicleRecord"/>s, 
    /// representing a context or area of execution. A scope can be apart
    /// of a scope stack, navigatable via the <see cref="Parent"/> property
    /// or by enumerating on this <see cref="IChronicleScope"/>.
    /// </summary>
    public class ChronicleScope : IChronicleScope
    {

        /// <summary>
        /// The name for this scope.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The verbosity level of this scope, equivilent to the 
        /// number of scopes in this stack of scopes.
        /// </summary>
        public int Verbosity { get; }

        /// <summary>
        /// The immediately enclosing scope of this scope;
        /// the next scope down the stack of scopes. 
        /// </summary>
        public IChronicleScope Parent { get; }

        private IChronicle Chronicle { get; }

        internal ChronicleScope(Chronicle chronicle, IChronicleScope parent, string scopeName = null)
        {
            this.Chronicle = chronicle;
            this.Parent = parent;
            this.Name = scopeName;
            this.Verbosity = parent?.Verbosity + 1 ?? 1;
        }

        #region IDisposable Support
        /// <summary>
        /// Restore the parent of the current scope as the new current scope, 
        /// decreasing the verbosity level of records created on this thread.
        /// </summary>
        public void Dispose()
        {
            this.Chronicle.ScopeOut();
        }
        #endregion

        #region Enumerable Support
        /// <summary>
        /// Get the enumerator to enumerator through each scope
        /// in the scope stack from this scope down through it's parents.
        /// </summary>
        public IEnumerator<IChronicleScope> GetEnumerator()
        {
            IChronicleScope next = this;
            while (next != null)
            {
                yield return next;
                next = next.Parent;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();
        #endregion

    }

}