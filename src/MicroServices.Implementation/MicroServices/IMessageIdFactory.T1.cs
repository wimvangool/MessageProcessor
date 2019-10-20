﻿using System;

namespace Kingo.MicroServices
{
    /// <summary>
    /// When implemented by a class, represents a factory of unique identifiers for a specific type of message.
    /// </summary>
    /// <typeparam name="TMessage">Type of message to generate the identifiers for.</typeparam>
    public interface IMessageIdFactory<in TMessage>
    {
        /// <summary>
        /// Generates a new identifier for the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Content of a message.</param>
        /// <returns>A new identifier based on the specified <paramref name="message"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        string GenerateMessageIdFor(TMessage message);
    }
}
