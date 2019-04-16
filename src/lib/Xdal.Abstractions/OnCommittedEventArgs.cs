using System;

// ReSharper disable once CheckNamespace
namespace Xdal
{
    /// <summary>
    /// Provides information about the unit of work after it has been committed.
    /// </summary>
    public class OnCommittedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the unit of work that was committed.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets a value indicating if the commit was sucessful.
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        /// Gets the exception raised in case of unsuccessful commit.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="OnCommittedEventArgs"/>.
        /// </summary>
        /// <param name="unitOfWork">The committed <see cref="IUnitOfWork"/>.</param>
        /// <param name="successful">Indicates if the commit was successful.</param>
        /// <param name="exception">Optional. The exception raised during commit.</param>
        public OnCommittedEventArgs(IUnitOfWork unitOfWork, bool successful, Exception exception = null)
        {
            UnitOfWork = unitOfWork;
            Successful = successful;
            Exception = exception;
        }
    }
}
