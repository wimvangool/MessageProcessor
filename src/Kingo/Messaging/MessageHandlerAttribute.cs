﻿using System;

namespace Kingo.Messaging
{
    /// <summary>
    /// This attribute must be put on each <see cref="IMessageHandler{T}" /> class to support auto-registration of it
    /// by the <see cref="MessageHandlerFactory" /> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MessageHandlerAttribute : Attribute, IMessageHandlerConfiguration
    {
        private readonly MessageHandlerConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHandlerAttribute" /> class.
        /// </summary>
        /// <param name="lifetime">The lifetime of the <see cref="IMessageHandler{T}" />.</param>
        public MessageHandlerAttribute(InstanceLifetime lifetime)
        {
            _configuration = new MessageHandlerConfiguration(lifetime);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHandlerAttribute" /> class.
        /// </summary>
        /// <param name="lifetime">The lifetime of the <see cref="IMessageHandler{T}" />.</param>
        /// <param name="sources">Specifies which source(s) the message is accepted from.</param>
        public MessageHandlerAttribute(InstanceLifetime lifetime, MessageSources sources)
        {
            _configuration = new MessageHandlerConfiguration(lifetime, sources);
        }

        /// <summary>
        /// The lifetime of the <see cref="IMessageHandler{T}" />.
        /// </summary>
        public InstanceLifetime Lifetime =>
            _configuration.Lifetime;

        /// <summary>
        /// Specifies which source(s) the message is accepted from.
        /// </summary>
        public MessageSources Sources =>
            _configuration.Sources;

        /// <inheritdoc />
        public override string ToString() =>
            _configuration.ToString();
    }
}
