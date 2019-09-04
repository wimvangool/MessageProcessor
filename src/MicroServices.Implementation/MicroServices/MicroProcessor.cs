﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Kingo.Reflection;
using Kingo.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Kingo.MicroServices
{
    /// <summary>
    /// Represents a basic implementation of the <see cref="IMicroProcessor" /> interface.
    /// </summary>
    public class MicroProcessor : IMicroProcessor
    {
        #region [====== ServiceScope ======]

        private sealed class MicroProcessorServiceProvider : IMicroProcessorServiceProvider
        {
            private readonly MicroProcessor _processor;
            private readonly IServiceProvider _serviceProvider;

            public MicroProcessorServiceProvider(MicroProcessor processor, IServiceProvider serviceProvider)
            {
                _processor = processor;
                _serviceProvider = serviceProvider;
            }

            public object GetService(Type serviceType) =>
                _serviceProvider.GetService(serviceType);

            public IServiceScope CreateScope() =>
                new MicroProcessorServiceScope(_processor, _serviceProvider.CreateScope());
        }

        private sealed class MicroProcessorServiceScope : IServiceScope
        {
            private readonly IDisposable _contextScope;
            private readonly IServiceScope _serviceScope;            

            public MicroProcessorServiceScope(MicroProcessor processor, IServiceScope serviceScope)
            {
                // Every time a new scope is created by a client of the processor, the local service provider
                // property is updated to refer to the scoped provider. This mechanism makes sure that every time
                // the internal components of the processor resolve a new dependency, it will do so in the latest
                // created scope.
                _contextScope = processor._serviceProviderContext.OverrideAsyncLocal(new MicroProcessorServiceProvider(processor, serviceScope.ServiceProvider));
                _serviceScope = serviceScope;                
            }

            public void Dispose()
            {
                _contextScope.Dispose();
                _serviceScope.Dispose();                
            }

            public IServiceProvider ServiceProvider =>
                _serviceScope.ServiceProvider;
        }

        #endregion
                
        private readonly Context<IMicroProcessorServiceProvider> _serviceProviderContext;
        private readonly Context<IPrincipal> _principalContext;
        private readonly Lazy<IMicroProcessorOptions> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MicroProcessor" /> class.
        /// </summary>                     
        /// <param name="serviceProvider">
        /// Service-provider that will be used to resolve message-handlers, their dependencies and other components.
        /// </param>        
        public MicroProcessor(IServiceProvider serviceProvider)
        {                                    
            _serviceProviderContext = new Context<IMicroProcessorServiceProvider>(CreateServiceProvider(serviceProvider));
            _principalContext = new Context<IPrincipal>();
            _options = new Lazy<IMicroProcessorOptions>(ResolveOptions, true);
        }

        /// <inheritdoc />
        public override string ToString() =>
            GetType().FriendlyName();

        #region [====== Options ======]

        internal IMicroProcessorOptions Options =>
            _options.Value;

        private IMicroProcessorOptions ResolveOptions()
        {
            var options = ServiceProvider.GetService<MicroProcessorOptions>();
            if (options == null)
            {
                return new MicroProcessorOptions();
            }
            return options;
        }

        #endregion

        #region [====== UsePrincipal ======]        

        /// <inheritdoc />
        public IDisposable AssignUser(IPrincipal user) =>
            _principalContext.OverrideAsyncLocal(user ?? throw new ArgumentNullException(nameof(user)));

        internal ClaimsPrincipal CreatePrincipal() =>
            CreatePrincipal(_principalContext.Current);

        /// <summary>
        /// Creates and returns a <see cref="ClaimsPrincipal" /> based on the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The principal to convert to a <see cref="ClaimsPrincipal"/>.</param>
        /// <returns>A new <see cref="ClaimsPrincipal"/> based on the specified <paramref name="user"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="user"/> is <c>null</c>.
        /// </exception>
        protected virtual ClaimsPrincipal CreatePrincipal(IPrincipal user) =>
            user == null ? new ClaimsPrincipal() : new ClaimsPrincipal(user);

        #endregion

        #region [====== ServiceProvider ======]        

        /// <inheritdoc />
        public virtual IMicroProcessorServiceProvider ServiceProvider =>
            _serviceProviderContext.Current;

        private IMicroProcessorServiceProvider CreateServiceProvider(IServiceProvider serviceProvider) =>
            new MicroProcessorServiceProvider(this, serviceProvider ?? CreateDefaultServiceProvider());

        private static IServiceProvider CreateDefaultServiceProvider() =>
            new ServiceCollection().BuildServiceProvider(true);

        #endregion

        #region [====== MethodEndpoints ======]        

        /// <inheritdoc />
        public virtual IEnumerable<IMicroServiceBusEndpoint> CreateServiceBusEndpoints()
        {
            var methodFactory = ServiceProvider.GetService<IHandleAsyncMethodFactory>();
            if (methodFactory == null)
            {
                return Enumerable.Empty<IMicroServiceBusEndpoint>();
            }
            return methodFactory.CreateMicroServiceBusEndpoints(this);
        }            

        #endregion

        #region [====== ExecuteAsync (Commands & Queries) ======]                  

        /// <inheritdoc />
        public Task<MessageHandlerOperationResult> ExecuteCommandAsync<TCommand>(IMessageHandler<TCommand> messageHandler, TCommand message, CancellationToken? token = null) =>
            ExecuteOperationAsync(new CommandHandlerOperation<TCommand>(this, messageHandler, message, token));

        /// <inheritdoc />
        public Task<MessageHandlerOperationResult> HandleEventAsync<TEvent>(IMessageHandler<TEvent> messageHandler, TEvent message, CancellationToken? token = null) =>
            ExecuteOperationAsync(new EventHandlerOperation<TEvent>(this, messageHandler, message, token));

        /// <inheritdoc />
        public Task<QueryOperationResult<TResponse>> ExecuteQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken? token = null) =>
            ExecuteOperationAsync(new QueryOperationImplementation<TResponse>(this, query, token));

        /// <inheritdoc />
        public Task<QueryOperationResult<TResponse>> ExecuteQueryAsync<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest message, CancellationToken? token = null) =>
            ExecuteOperationAsync(new QueryOperationImplementation<TRequest, TResponse>(this, query, message, token));

        #endregion

        #region [====== ExecuteAsync (Operations) ======]

        /// <summary>
        /// Executes the specified <paramref name="operation"/> and returns its result.
        /// </summary>        
        /// <param name="operation">The operation to execute.</param>
        /// <returns>The result of the operation.</returns>
        protected internal virtual Task<MessageHandlerOperationResult> ExecuteOperationAsync(MessageHandlerOperation operation) =>
            ExecuteOperationAsync<MessageHandlerOperationResult>(operation);

        /// <summary>
        /// Executes the specified <paramref name="operation"/> and returns its result.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response of the query.</typeparam>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>The result of the operation.</returns>
        protected virtual Task<QueryOperationResult<TResponse>> ExecuteOperationAsync<TResponse>(QueryOperation<TResponse> operation) =>
            ExecuteOperationAsync<QueryOperationResult<TResponse>>(operation);

        /// <summary>
        /// Executes the specified <paramref name="operation"/> and returns its result.
        /// </summary>
        /// <typeparam name="TResult">Type of the result of the operation.</typeparam>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>The result of the operation.</returns>
        protected virtual Task<TResult> ExecuteOperationAsync<TResult>(MicroProcessorOperation<TResult> operation) =>
            operation.ExecuteAsync();

        #endregion        
    }
}