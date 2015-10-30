﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Kingo.BuildingBlocks.Messaging
{
    /// <summary>
    /// Serves as a simple base-implementation of the <see cref="IMessage{TMessage}" /> interface.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://www.kingo.com/buildingblocks")]
    public abstract class Message<TMessage> : Message, IMessage<TMessage> where TMessage : Message<TMessage>
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="Message{TMessage}" /> class.
        /// </summary>
        protected Message() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="message">The message to copy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="message"/> is <c>null</c>.
        /// </exception>
        protected Message(Message<TMessage> message)
            : base(message) { }                

        #region [====== Copy ======]

        internal override IMessage CopyMessage()
        {
            return Copy();
        }

        TMessage IMessage<TMessage>.Copy()
        {
            return Copy();
        }

        /// <summary>
        /// Creates and returns a copy of this message. The default implementation uses
        /// the <see cref="DataContractSerializer" /> to copy this instance.
        /// </summary>
        /// <returns>A copy of this message.</returns>
        public virtual TMessage Copy()
        {
            var memoryStream = new MemoryStream();
            var writer = XmlDictionaryWriter.CreateBinaryWriter(memoryStream);
            var reader = XmlDictionaryReader.CreateBinaryReader(memoryStream, XmlDictionaryReaderQuotas.Max);
            var serializer = new DataContractSerializer(GetType());
            
            serializer.WriteObject(writer, this);
            writer.Flush();
            memoryStream.Position = 0;

            return (TMessage) serializer.ReadObject(reader);
        }

        #endregion               

        #region [====== Validation ======]

        /// <inheritdoc />
        public override MessageErrorInfo Validate()
        {
            var validator = CreateValidator();
            if (validator == null)
            {
                return MessageErrorInfo.Empty;
            }
            return validator.Validate(Copy());
        }

        /// <summary>
        /// Creates and returns a <see cref="IMessageValidator{T}" /> that can be used to validate this message,
        /// or <c>null</c> if this message does not require validation.
        /// </summary>
        /// <returns>A new <see cref="IMessageValidator{T}" /> that can be used to validate this message.</returns>
        protected virtual IMessageValidator<TMessage> CreateValidator()
        {
            return null;
        }

        #endregion
    }
}
