﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kingo.Messaging
{
    /// <summary>
    /// When implemented by a class, represents a sequence of messages, ready to be executed.
    /// </summary>
    public interface IMessageSequence
    {
        /// <summary>
        /// Processes all messages of this sequence using the specified processor.
        /// </summary>
        /// <param name="processor">Processor that will be used to execute this sequence.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="processor"/> is <c>null</c>.
        /// </exception>
        void ProcessWith(IMessageProcessor processor);

        /// <summary>
        /// Processes all messages of this sequence using the specified processor asynchronously.
        /// </summary>
        /// <param name="processor">Processor that will be used to execute this sequence.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="processor"/> is <c>null</c>.
        /// </exception>
        /// <returns>A task carrying out this operation.</returns>
        Task ProcessWithAsync(IMessageProcessor processor);

        /// <summary>
        /// Processes all messages of this sequence using the specified processor asynchronously.
        /// </summary>
        /// <param name="processor">Processor that will be used to execute this sequence.</param>
        /// <param name="token">Token that can be used to cancel the operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="processor"/> is <c>null</c>.
        /// </exception>
        /// <returns>A task carrying out this operation.</returns>
        Task ProcessWithAsync(IMessageProcessor processor, CancellationToken token);

        /// <summary>
        /// Appends the specified sequence to the current sequence and returns the resulting sequence.
        /// </summary>
        /// <param name="sequence">The sequence of messages to append.</param>
        /// <returns>The new, concatenated sequence of messages.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <c>null</c>.
        /// </exception>
        IMessageSequence Append(IMessageSequence sequence);

        /// <summary>
        /// Appends the specified message to the current sequence and returns the resulting sequence.
        /// </summary>
        /// <typeparam name="TMessage">Type of the message to append.</typeparam>
        /// <param name="message">The message to append.</param>
        /// <returns>The new, concatenated sequence of messages.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        IMessageSequence Append<TMessage>(TMessage message) where TMessage : class, IMessage<TMessage>;

        /// <summary>
        /// Appends the specified message to the current sequence and returns the resulting sequence.
        /// </summary>
        /// <typeparam name="TMessage">Type of the message to append.</typeparam>
        /// <param name="message">The message to append.</param>
        /// <param name="handler">The handler to invoke.</param>
        /// <returns>The new, concatenated sequence of messages.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        IMessageSequence Append<TMessage>(TMessage message, Action<TMessage> handler) where TMessage : class, IMessage<TMessage>;

        /// <summary>
        /// Appends the specified message to the current sequence and returns the resulting sequence.
        /// </summary>
        /// <typeparam name="TMessage">Type of the message to append.</typeparam>
        /// <param name="message">The message to append.</param>
        /// <param name="handler">The handler to invoke.</param>
        /// <returns>The new, concatenated sequence of messages.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        IMessageSequence Append<TMessage>(TMessage message, Func<TMessage, Task> handler) where TMessage : class, IMessage<TMessage>;

        /// <summary>
        /// Appends the specified message to the current sequence and returns the resulting sequence.
        /// </summary>
        /// <typeparam name="TMessage">Type of the message to append.</typeparam>
        /// <param name="message">The message to append.</param>
        /// <param name="handler">The handler to invoke.</param>
        /// <returns>The new, concatenated sequence of messages.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        IMessageSequence Append<TMessage>(TMessage message, IMessageHandler<TMessage> handler) where TMessage : class, IMessage<TMessage>;
    }
}