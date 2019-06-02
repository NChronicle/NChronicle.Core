using System;
using System.Collections.Generic;

namespace KSharp.NChronicle.Core.Abstractions
{

    /// <summary>
    /// A scope in which to create more verbose <see cref="IChronicleRecord"/>s, 
    /// representing a context or area of execution. A scope can be apart
    /// of a scope stack, navigatable via the <see cref="Parent"/> property
    /// or by enumerating on this <see cref="IChronicleScope"/>.
    /// </summary>
    public interface IChronicleScope : IDisposable, IEnumerable<IChronicleScope>
    {

        /// <summary>
        /// The name of this scope.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The verbosity level of this scope, equivilent to the 
        /// number of scopes in this stack of scopes.
        /// </summary>
        int Verbosity { get; }

        /// <summary>
        /// The immediately enclosing scope of this scope;
        /// the next scope down the stack of scopes. 
        /// </summary>
        IChronicleScope Parent { get; }

    }

}