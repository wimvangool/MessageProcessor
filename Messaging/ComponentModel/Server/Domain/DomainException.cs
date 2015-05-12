﻿using System.Resources;
using System.Runtime.Serialization;

namespace System.ComponentModel.Server.Domain
{
    /// <summary>
    /// This type of exception is thrown when something goes wrong in the domain of an application.
    /// </summary>    
    [Serializable]
    public abstract class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        protected DomainException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        protected DomainException(string message) : base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="innerException">Cause of the exception.</param>
        protected DomainException(string message, Exception innerException) : base(message, innerException) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        /// <summary>
        /// Converts this instance into an instance of <see cref="InvalidMessageException" />.
        /// </summary>
        /// <param name="failedMessage">The message that caused the exception.</param>
        /// <returns>
        /// An instance of <see cref="InvalidMessageException" /> that wraps this exception and the inner
        /// exception.
        /// </returns>
        public virtual InvalidMessageException AsInvalidMessageException(IMessage failedMessage)
        {
            if (failedMessage == null)
            {
                throw new ArgumentNullException("failedMessage");
            }
            var messageFormat = ExceptionMessages.DomainModelException_CommandFailed;
            var message = string.Format(messageFormat, failedMessage.GetType());
            return new InvalidMessageException(failedMessage, message, this);
        }

        /// <summary>
        /// Converts this instance into an instance of <see cref="CommandExecutionException" />.
        /// </summary>
        /// <param name="failedMessage">The message that caused the exception.</param>
        /// <returns>
        /// An instance of <see cref="CommandExecutionException" /> that wraps this exception and the inner
        /// exception.
        /// </returns>
        public virtual CommandExecutionException AsCommandExecutionException(IMessage failedMessage)
        {
            if (failedMessage == null)
            {
                throw new ArgumentNullException("failedMessage");
            }
            var messageFormat = ExceptionMessages.DomainModelException_CommandFailed;
            var message = string.Format(messageFormat, failedMessage.GetType());
            return new CommandExecutionException(failedMessage, message, this);
        }        
    }
}
