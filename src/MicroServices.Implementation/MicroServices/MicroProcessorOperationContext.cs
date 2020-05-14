﻿using System;
using System.Security.Claims;
using Kingo.Clocks;
using Kingo.Reflection;

namespace Kingo.MicroServices
{
    /// <summary>
    /// Represents the context in which a <see cref="MicroProcessor"/> operates.
    /// </summary>    
    public abstract class MicroProcessorOperationContext
    {        
        private readonly MicroProcessor _processor;
        private readonly ClaimsPrincipal _user;
        private readonly IClock _clock;
        private readonly AsyncMethodOperationStackTrace _stackTrace;

        internal MicroProcessorOperationContext(MicroProcessor processor, AsyncMethodOperationStackTrace stackTrace = null)
        {
            _processor = processor;
            _user = processor.CurrentUser();
            _clock = processor.CurrentClock();
            _stackTrace = stackTrace ?? AsyncMethodOperationStackTrace.Empty;
        }

        internal MicroProcessorOperationContext(MicroProcessorOperationContext context, IAsyncMethodOperation operation)
        {
            _processor = context._processor;
            _user = context._user.Clone();
            _clock = context._clock;
            _stackTrace = context._stackTrace.Push(operation);
        }

        internal MicroProcessor Processor =>
            _processor;

        /// <summary>
        /// Gets the user that is executing the current operation.
        /// </summary>
        public ClaimsPrincipal User =>
            _user;

        /// <summary>
        /// Get the clock that can provide the current date and time.
        /// </summary>
        public IClock Clock =>
            _clock;

        /// <summary>
        /// Returns a stack trace of all operations that are currently being executed.
        /// </summary>
        public IAsyncMethodOperationStackTrace StackTrace =>
            _stackTrace;

        /// <summary>
        /// Returns the <see cref="IServiceProvider" /> of this context.
        /// </summary>
        public IServiceProvider ServiceProvider =>
            _processor.ServiceProvider;

        /// <inheritdoc />
        public override string ToString() =>
            GetType().FriendlyName();

        internal QueryOperationContext PushOperation<TRequest, TResponse>(ExecuteAsyncMethodOperation<TRequest, TResponse> operation) =>
            new QueryOperationContext(this, operation);
    }
}
