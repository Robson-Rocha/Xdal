using System;

// ReSharper disable once CheckNamespace
namespace Xdal
{
    /// <summary>
    /// Provides information about the unit of work before it is committed.
    /// </summary>
    public class OnCommittingEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the unit of work being committed.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="OnCommittingEventArgs"/>.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> being committed.</param>
        public OnCommittingEventArgs(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
