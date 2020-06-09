﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kingo.MicroServices.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.MicroServices
{
    [TestClass]
    public sealed class MessageFactoryTest
    {
        #region [====== Messages ======]

        [Message(MessageKind.Command, "[{0}]", nameof(PropertyA))]
        private class MessageA
        {
            public MessageA(int propertyA)
            {
                PropertyA = propertyA;
            }

            public int PropertyA
            {
                get;
            }
        }

        [Message(MessageKind.Event, "[{0}-{1}]", nameof(PropertyA), nameof(PropertyB))]
        private class MessageB : MessageA
        {
            public MessageB(int propertyA, int propertyB) : base(propertyA)
            {
                PropertyB = propertyB;
            }

            public int PropertyB
            {
                get;
            }
        }

        private class MessageC : MessageB
        {
            public MessageC(int propertyA, int propertyB, int propertyC) : base(propertyA, propertyB)
            {
                PropertyC = propertyC;
            }

            public int PropertyC
            {
                get;
            }
        }

        #endregion

        private readonly MessagePipeline _builder;

        public MessageFactoryTest()
        {
            _builder = new MessagePipeline();
        }

        #region [====== Builder ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_Throws_IfMessageTypeIsNull()
        {
            _builder.Add(null, new MessageAttribute(MessageKind.Command));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_Throws_IfMessageAttributeNull()
        {
            _builder.Add(typeof(object), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Throws_IfMessageAttributeWasAlreadyAddedForSpecifiedType()
        {
            _builder.Add<object>(MessageKind.Command);
            _builder.Add<object>(MessageKind.Event);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetMessageKindResolver_Throws_IfResolverIsNull()
        {
            _builder.MessageKindResolver = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetMessageIdGenerator_Throws_IfGeneratorIsNull()
        {
            _builder.MessageIdGenerator = null;
        }

        #endregion

        #region [====== CreateMessage (Basic) ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMessage_Throws_IfContentIsNull()
        {
            CreateAndValidateMessage(null as object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateMessage_Throws_IfMessageKindIsNotValid()
        {
            CreateAndValidateMessage(new object(), MessageKind.Undefined);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateMessage_Throws_IfMessageDirectionIsNotValid()
        {
            CreateAndValidateMessage(new object(), MessageKind.Command, (MessageDirection) (-1));
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeDeclaresNoMessageAttribute_And_NoAttributeWasAddedForType()
        {
            var message = CreateAndValidateMessage(new object());

            Assert.IsInstanceOfType(message, typeof(IMessage<object>));
            Assert.AreEqual(MessageKind.Command, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreNotEqual(Guid.Empty, Guid.Parse(message.MessageId));
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfCustomMessageKindResolverWasSpecified()
        {
            _builder.MessageKindResolver = new FixedMessageKindResolver(MessageKind.Command);

            var message = CreateAndValidateMessage(new object());

            Assert.IsInstanceOfType(message, typeof(IMessage<object>));
            Assert.AreEqual(MessageKind.Command, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreNotEqual(Guid.Empty, Guid.Parse(message.MessageId));
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfCustomMessageIdGeneratorWasSpecified()
        {
            var messageId = Guid.NewGuid().ToString();

            _builder.MessageIdGenerator = new FixedMessageIdGenerator(messageId);

            var message = CreateAndValidateMessage(new object(), MessageKind.Request, MessageDirection.Internal);

            Assert.IsInstanceOfType(message, typeof(IMessage<object>));
            Assert.AreEqual(MessageKind.Request, message.Kind);
            Assert.AreEqual(MessageDirection.Internal, message.Direction);
            Assert.AreEqual(messageId, message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfDeliveryTimeWasSpecified()
        {
            var deliveryTime = DateTimeOffset.UtcNow;
            var message = CreateAndValidateMessage(new object(), MessageKind.Event, MessageDirection.Output, deliveryTime);

            Assert.IsInstanceOfType(message, typeof(IMessage<object>));
            Assert.AreEqual(MessageKind.Event, message.Kind);
            Assert.AreEqual(MessageDirection.Output, message.Direction);
            Assert.AreNotEqual(Guid.Empty, Guid.Parse(message.MessageId));
            Assert.IsNull(message.CorrelationId);
            Assert.AreEqual(deliveryTime, message.DeliveryTimeUtc);
        }

        #endregion

        #region [====== CreateMessage (MessageAttributes) ======]

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeDeclaresNoMessageAttribute_But_MessageAttributeWasAddedForType()
        {
            _builder.Add<string>(MessageKind.Command, "Id-{0}", nameof(string.Length));

            var message = CreateAndValidateMessage(Guid.NewGuid().ToString());

            Assert.IsInstanceOfType(message, typeof(IMessage<string>));
            Assert.AreEqual(MessageKind.Command, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreEqual("Id-36", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeDeclaresMessageAttribute_And_NoAttributeWasAddedForType()
        {
            var message = CreateAndValidateMessage(new MessageA(10));

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageA>));
            Assert.AreEqual(MessageKind.Command, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreEqual("[10]", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeDeclaresMessageAttribute_But_MessageAttributeWasAddedForType()
        {
            _builder.Add<MessageA>(MessageKind.Event, "({0})", nameof(MessageA.PropertyA));

            var message = CreateAndValidateMessage(new MessageA(10), MessageKind.Event);

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageA>));
            Assert.AreEqual(MessageKind.Event, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreEqual("(10)", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeDeclaresNewMessageAttribute_And_NoAttributeWasAddedForType()
        {
            var message = CreateAndValidateMessage(new MessageB(10, 20), MessageKind.Event);

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageB>));
            Assert.AreEqual(MessageKind.Event, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreEqual("[10-20]", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeDeclaresNewMessageAttribute_But_MessageAttributeWasAddedForType()
        {
            _builder.Add<MessageB>(MessageKind.Request, "({0}-{1})", nameof(MessageA.PropertyA), nameof(MessageB.PropertyB));

            var message = CreateAndValidateMessage(new MessageB(10, 20), MessageKind.Request);

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageB>));
            Assert.AreEqual(MessageKind.Request, message.Kind);
            Assert.AreEqual(MessageDirection.Input, message.Direction);
            Assert.AreEqual("(10-20)", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeInheritsMessageAttribute_And_NoAttributeWasAddedForType()
        {
            var message = CreateAndValidateMessage(new MessageC(10, 20, 30), MessageKind.Event);

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageC>));
            Assert.AreEqual(MessageKind.Event, message.Kind);
            Assert.AreEqual("[10-20]", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeInheritsMessageAttribute_But_MessageAttributeWasAddedForBaseType()
        {
            _builder.Add<MessageB>(MessageKind.Request, "({0}-{1})", nameof(MessageA.PropertyA), nameof(MessageB.PropertyB));

            var message = CreateAndValidateMessage(new MessageC(10, 20, 30), MessageKind.Request);

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageC>));
            Assert.AreEqual(MessageKind.Request, message.Kind);
            Assert.AreEqual("(10-20)", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        [TestMethod]
        public void CreateMessage_ReturnsExpectedMessage_IfContentTypeInheritsMessageAttribute_But_MessageAttributeWasAddedForType()
        {
            _builder.Add<MessageC>(MessageKind.Request, "({0}-{1}-{2})", nameof(MessageA.PropertyA), nameof(MessageB.PropertyB), nameof(MessageC.PropertyC));

            var message = CreateAndValidateMessage(new MessageC(10, 20, 30), MessageKind.Request);

            Assert.IsInstanceOfType(message, typeof(IMessage<MessageC>));
            Assert.AreEqual(MessageKind.Request, message.Kind);
            Assert.AreEqual("(10-20-30)", message.MessageId);
            Assert.IsNull(message.CorrelationId);
            Assert.IsNull(message.DeliveryTimeUtc);
        }

        #endregion

        #region [====== CreateMessage (Content Validation) ======]

        private sealed class SomeCommand
        {
            public SomeCommand(string id)
            {
                Id = id;
            }

            [Required(AllowEmptyStrings = true)]
            public string Id
            {
                get;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MessageValidationFailedException))]
        public void Validate_Throws_IfMessageKindDoesNotMatchExpectedMessageKind()
        {
            _builder.Add<object>(MessageKind.Command);

            CreateAndValidateMessage(new object(), MessageKind.Event);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageValidationFailedException))]
        public void Validate_Throws_IfMessageKindIsUndefined_And_UndefinedMessagesAreBlocked()
        {
            _builder.Validate(MessageDirection.Input, MessageValidationOptions.BlockUndefined);

            CreateAndValidateMessage(new object());
        }

        [TestMethod]
        public void Validate_ReturnsExpectedMessage_IfMessageKindIsUndefined_But_UndefinedMessagesAreAccepted()
        {
            var message = CreateAndValidateMessage(new object());

            Assert.AreEqual(MessageKind.Command, message.Kind);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageValidationFailedException))]
        public void Validate_Throws_IfMessageMustBeValidated_And_MessageContentIsNotValid()
        {
            _builder.Validate(MessageDirection.Input, MessageValidationOptions.Commands);

            CreateAndValidateMessage(new SomeCommand(null));
        }

        [TestMethod]
        public void Validate_ReturnsExpectedMessage_IfMessageMustNotBeValidated_And_MessageContentIsNotValid()
        {
            CreateAndValidateMessage(new SomeCommand(null));
        }

        [TestMethod]
        public void Validate_ReturnsExpectedMessage_IfMessageMustBeValidated_But_MessageContentIsValid()
        {
            _builder.Validate(MessageDirection.Input, MessageValidationOptions.Commands);

            CreateAndValidateMessage(new SomeCommand(string.Empty));
        }

        #endregion

        private IMessage CreateAndValidateMessage<TContent>(TContent content, MessageKind kind = MessageKind.Command, MessageDirection direction = MessageDirection.Input, DateTimeOffset? deliveryTime = null) =>
            _builder.BuildMessageFactory().CreateMessage(kind, direction, MessageHeader.Unspecified, content, deliveryTime).Validate(null);

        #region [====== CreateInt32Messages ======]

        private static readonly MessageFactory _MessageFactory = new MessagePipeline().BuildMessageFactory();

        internal static IEnumerable<IMessage> CreateMessages(int count, MessageKind kind = MessageKind.Event, MessageDirection direction = MessageDirection.Output)
        {
            for (int index = 0; index < count; index++)
            {
                yield return CreateMessage(index, kind, direction);
            }
        }

        internal static IMessage CreateMessage(object content, MessageKind kind, MessageDirection direction) =>
            _MessageFactory.CreateMessage(kind, direction, MessageHeader.Unspecified, content);

        #endregion
    }
}