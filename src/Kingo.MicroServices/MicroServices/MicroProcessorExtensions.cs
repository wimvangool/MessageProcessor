﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kingo.MicroServices
{
    /// <summary>
    /// Contains extension methods for the <see cref="IMicroProcessor" /> interface.
    /// </summary>
    public static class MicroProcessorExtensions
    {
        #region [====== ExecuteAsync (Commands) ======]                  

        /// <summary>
        /// Executes a command with a specified <paramref name="messageHandler"/>.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="processor">The processor used to execute the command.</param>
        /// <param name="messageHandler">The message handler that will handle the command.</param>
        /// <param name="message">The command to handle.</param>
        /// <param name="token">Optional token that can be used to cancel the operation.</param>
        /// <returns>
        /// The result of the operation, which includes all published events and the number of message handlers that were invoked.
        /// </returns> 
        /// <exception cref="ArgumentNullException">
        /// <paramref name="messageHandler"/> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MicroProcessorOperationException">
        /// Something went wrong while executing the command.
        /// </exception> 
        public static Task<MessageHandlerOperationResult> ExecuteAsync<TCommand>(this IMicroProcessor processor, Action<TCommand, MessageHandlerOperationContext> messageHandler, TCommand message, CancellationToken? token = null) =>
            processor.ExecuteAsync(MessageHandlerDecorator<TCommand>.Decorate(messageHandler), message, token);

        /// <summary>
        /// Executes a command with a specified <paramref name="messageHandler"/>.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="processor">The processor used to execute the command.</param>
        /// <param name="messageHandler">The message handler that will handle the command.</param>
        /// <param name="message">The command to handle.</param>
        /// <param name="token">Optional token that can be used to cancel the operation.</param>
        /// <returns>
        /// The result of the operation, which includes all published events and the number of message handlers that were invoked.
        /// </returns> 
        /// <exception cref="ArgumentNullException">
        /// <paramref name="messageHandler"/> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MicroProcessorOperationException">
        /// Something went wrong while executing the command.
        /// </exception>  
        public static Task<MessageHandlerOperationResult> ExecuteAsync<TCommand>(this IMicroProcessor processor, Func<TCommand, MessageHandlerOperationContext, Task> messageHandler, TCommand message, CancellationToken? token = null) =>
            processor.ExecuteAsync(MessageHandlerDecorator<TCommand>.Decorate(messageHandler), message, token);

        #endregion

        #region [====== ExecuteAsync (Queries) ======]

        /// <summary>
        /// Executes the specified <paramref name="query"/> and returns its result asynchronously.
        /// </summary>
        /// <typeparam name="TResponse">Type of the message returned by the query.</typeparam>   
        /// <param name="processor">A processor.</param>     
        /// <param name="query">The query to execute.</param>               
        /// <param name="token">Optional token that can be used to cancel the operation.</param>          
        /// <returns>The result that carries the response returned by the <paramref name="query"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="query"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MicroProcessorOperationException">
        /// Something went wrong while executing the query.
        /// </exception>  
        public static Task<QueryOperationResult<TResponse>> ExecuteAsync<TResponse>(this IMicroProcessor processor, Func<QueryOperationContext, TResponse> query, CancellationToken? token = null) =>
            processor.ExecuteAsync(QueryDecorator<TResponse>.Decorate(query), token);

        /// <summary>
        /// Executes the specified <paramref name="query"/> and returns its result asynchronously.
        /// </summary>
        /// <typeparam name="TResponse">Type of the message returned by the query.</typeparam> 
        /// <param name="processor">A processor.</param>       
        /// <param name="query">The query to execute.</param>               
        /// <param name="token">Optional token that can be used to cancel the operation.</param>          
        /// <returns>The result that carries the response returned by the <paramref name="query"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="query"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MicroProcessorOperationException">
        /// Something went wrong while executing the query.
        /// </exception>  
        public static Task<QueryOperationResult<TResponse>> ExecuteAsync<TResponse>(this IMicroProcessor processor, Func<QueryOperationContext, Task<TResponse>> query, CancellationToken? token = null) =>
            processor.ExecuteAsync(QueryDecorator<TResponse>.Decorate(query), token);

        /// <summary>
        /// Executes the specified <paramref name="query"/> using the specified <paramref name="message"/> and returns its result asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">Type of the message going into the query.</typeparam>
        /// <typeparam name="TResponse">Type of the message returned by the query.</typeparam>
        /// <param name="processor">A processor.</param>
        /// <param name="query">The query to execute.</param>
        /// <param name="message">Message containing the parameters of this query.</param>
        /// <param name="token">Optional token that can be used to cancel the operation.</param>
        /// <returns>The result that carries the response returned by the <paramref name="query"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="query"/> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MicroProcessorOperationException">
        /// Something went wrong while executing the query.
        /// </exception>  
        public static Task<QueryOperationResult<TResponse>> ExecuteAsync<TRequest, TResponse>(this IMicroProcessor processor, Func<TRequest, QueryOperationContext, TResponse> query, TRequest message, CancellationToken? token = null) =>
            processor.ExecuteAsync(QueryDecorator<TRequest, TResponse>.Decorate(query), message, token);

        /// <summary>
        /// Executes the specified <paramref name="query"/> using the specified <paramref name="message"/> and returns its result asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">Type of the message going into the query.</typeparam>
        /// <typeparam name="TResponse">Type of the message returned by the query.</typeparam>
        /// <param name="processor">A processor.</param>
        /// <param name="query">The query to execute.</param>
        /// <param name="message">Message containing the parameters of this query.</param>
        /// <param name="token">Optional token that can be used to cancel the operation.</param>
        /// <returns>The result that carries the response returned by the <paramref name="query"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="query"/> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MicroProcessorOperationException">
        /// Something went wrong while executing the query.
        /// </exception>  
        public static Task<QueryOperationResult<TResponse>> ExecuteAsync<TRequest, TResponse>(this IMicroProcessor processor, Func<TRequest, QueryOperationContext, Task<TResponse>> query, TRequest message, CancellationToken? token = null) =>
            processor.ExecuteAsync(QueryDecorator<TRequest, TResponse>.Decorate(query), message, token);

        #endregion
    }
}