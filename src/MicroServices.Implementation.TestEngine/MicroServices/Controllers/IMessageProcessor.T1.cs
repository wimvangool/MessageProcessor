﻿using System;
using System.Threading.Tasks;

namespace Kingo.MicroServices.Controllers
{
    /// <summary>
    /// When implemented by a class, represents a processor that can process a specific message.
    /// </summary>
    /// <typeparam name="TMessage">Type of the message that can be processed.</typeparam>
    public interface IMessageProcessor<TMessage>
    {
        #region [====== ExecuteCommandAsync ======]

        /// <summary>
        /// Executes a command with a specific message handler.
        /// </summary>
        /// <typeparam name="TMessageHandler">Type of the message handler that will handle the specified <paramref name="message"/>.</typeparam>
        /// <param name="message">The command to execute.</param>        
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        Task ExecuteCommandAsync<TMessageHandler>(TMessage message) where TMessageHandler : class, IMessageHandler<TMessage>;

        /// <summary>
        /// Executes a command with a specific message handler.
        /// </summary>
        /// <param name="messageHandler">Message handler that will handle the specified <paramref name="message"/>.</param>
        /// <param name="message">The command to execute.</param>        
        /// <exception cref="ArgumentNullException">
        /// <paramref name="messageHandler"/> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        Task ExecuteCommandAsync(IMessageHandler<TMessage> messageHandler, TMessage message);

        #endregion

        #region [====== HandleEventAsync ======]

        /// <summary>
        /// Handles an event with a specific message handler.
        /// </summary>
        /// <typeparam name="TMessageHandler">Type of the message handler that will handle the specified <paramref name="message"/>.</typeparam>
        /// <param name="message">The event to handle.</param>        
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        Task HandleEventAsync<TMessageHandler>(TMessage message) where TMessageHandler : class, IMessageHandler<TMessage>;

        /// <summary>
        /// Handles an event with a specific message handler.
        /// </summary>
        /// <param name="messageHandler">Message handler that will handle the specified <paramref name="message"/>.</param>
        /// <param name="message">The event to handle.</param>        
        /// <exception cref="ArgumentNullException">
        /// <paramref name="messageHandler"/> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        Task HandleEventAsync(IMessageHandler<TMessage> messageHandler, TMessage message);

        #endregion
    }
}
