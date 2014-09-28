﻿using System.ComponentModel.Messaging.Server;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel.Messaging.Client
{
    /// <summary>
    /// Represents a task that can execute a query asynchronously.
    /// </summary>
    public class QueryExecutionTask<TResponse> : AsyncExecutionTask<IQueryDispatcher<TResponse>> where TResponse : IMessage
    {
        private readonly IQueryDispatcher<TResponse> _dispatcher;
        private readonly ObjectCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryExecutionTask{T}" /> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher that is used to execute the query.</param>
        /// <param name="cache">
        /// Optional cache that is supplied to the query to store and/or retrieve its result from.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispatcher"/> is <c>null</c>.
        /// </exception>
        public QueryExecutionTask(IQueryDispatcher<TResponse> dispatcher, ObjectCache cache)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }
            _dispatcher = dispatcher;
            _cache = cache;
        }

        /// <inheritdoc />
        protected override IQueryDispatcher<TResponse> Dispatcher
        {
            get { return _dispatcher; }
        }

        /// <summary>
        /// If the task has been started, returns the associated <see cref="Task" /> instance of this task.
        /// </summary>
        protected Task<TResponse> Task
        {
            get;
            private set;
        }

        /// <inheritdoc />
        protected override void Execute(CancellationToken token, IProgressReporter reporter)
        {
            Task = _dispatcher.ExecuteAsync(RequestId, _cache, token, reporter);
        }
    }
}