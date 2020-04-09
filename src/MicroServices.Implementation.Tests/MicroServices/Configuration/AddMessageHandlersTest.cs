﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kingo.MicroServices.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.MicroServices.Configuration
{
    [TestClass]
    public sealed class AddMessageHandlersTest : MicroProcessorTest<MicroProcessor>
    {
        #region [====== MessageHandlerTypes ======]

        private abstract class AbstractMessageHandler : IMessageHandler<object>
        {
            public abstract Task HandleAsync(object message, IMessageHandlerOperationContext context);
        }

        private sealed class GenericMessageHandler<TMessage> : IMessageHandler<TMessage>
        {
            public Task HandleAsync(TMessage message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;
        }

        private sealed class MessageHandler1 : IMessageHandler<object>
        {
            public Task HandleAsync(object message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;
        }

        private sealed class MessageHandler2 : IMessageHandler<object>, IMessageHandler<int>
        {
            public Task HandleAsync(object message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;

            public Task HandleAsync(int message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;
        }

        [MicroProcessorComponent((ServiceLifetime) (-1))]
        private sealed class InvalidLifetimeMessageHandler : IMessageHandler<object>
        {
            public Task HandleAsync(object message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;
        }

        [MicroProcessorComponent(ServiceLifetime.Scoped)]
        private sealed class ScopedMessageHandler : IMessageHandler<object>
        {
            public Task HandleAsync(object message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;
        }

        [MicroProcessorComponent(ServiceLifetime.Singleton)]
        private sealed class SingletonMessageHandler : IMessageHandler<object>
        {
            public Task HandleAsync(object message, IMessageHandlerOperationContext context) =>
                Task.CompletedTask;
        }

        #endregion

        #region [====== Add (Registration & Mapping) ======]

        [TestMethod]
        public void Add_AddsNoMessageHandlers_IfThereAreNoMessageHandlerTypesToAdd()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(object), typeof(int));
            ProcessorBuilder.MessageHandlers.Add(typeof(AbstractMessageHandler), typeof(GenericMessageHandler<>));

            Assert.AreEqual(DefaultServiceCount, BuildServiceCollection().Count);
        }

        [TestMethod]
        public void Add_AddsExpectedMessageHandler_IfMessageHandlerIsClosedGenericType()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(GenericMessageHandler<object>));

            var provider = BuildServiceProvider();

            Assert.IsInstanceOfType(provider.GetRequiredService<IMessageHandler<object>>(), typeof(GenericMessageHandler<object>));
            Assert.IsNotNull(provider.GetRequiredService<GenericMessageHandler<object>>());
        }

        [TestMethod]
        public void Add_AddsExpectedMessageHandler_IfMessageHandlerIsRegularType()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(MessageHandler1));

            var provider = BuildServiceProvider();

            Assert.IsInstanceOfType(provider.GetRequiredService<IMessageHandler<object>>(), typeof(MessageHandler1));
            Assert.IsNotNull(provider.GetRequiredService<MessageHandler1>());
        }

        [TestMethod]
        public void Add_AddsExpectedMessageHandler_IfMessageHandlerImplementsMultipleInterfaces()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(MessageHandler2));

            var provider = BuildServiceProvider();

            Assert.IsInstanceOfType(provider.GetRequiredService<IMessageHandler<object>>(), typeof(MessageHandler2));
            Assert.IsInstanceOfType(provider.GetRequiredService<IMessageHandler<int>>(), typeof(MessageHandler2));
            Assert.IsNotNull(provider.GetRequiredService<MessageHandler2>());
        }

        [TestMethod]
        public void Add_AddsExpectedMessageHandlers_IfMultipleMessageHandlersImplementTheSameInterface()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(MessageHandler1), typeof(MessageHandler2));

            var provider = BuildServiceProvider();

            Assert.IsInstanceOfType(provider.GetRequiredService<IMessageHandler<object>>(), typeof(MessageHandler2));
            Assert.IsNotNull(provider.GetRequiredService<MessageHandler1>());
            Assert.IsNotNull(provider.GetRequiredService<MessageHandler2>());
            Assert.AreEqual(2, provider.GetRequiredService<IEnumerable<IMessageHandler<object>>>().Count());
        }

        #endregion

        #region [====== Add (Lifetime) ======]

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuildServiceProvider_Throws_IfMessageHandlerHasInvalidLifetime()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(InvalidLifetimeMessageHandler));

            BuildServiceProvider();
        }

        [TestMethod]
        public void Add_AddsTransientMessageHandler_IfMessageHandlerHasTransientLifetime()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(MessageHandler1));

            var provider = BuildServiceProvider();
            var messageHandlerA = provider.GetRequiredService<MessageHandler1>();

            using (var scope = provider.CreateScope())
            {
                var messageHandlerB = scope.ServiceProvider.GetRequiredService<MessageHandler1>();
                var messageHandlerC = scope.ServiceProvider.GetRequiredService<MessageHandler1>();

                Assert.AreNotSame(messageHandlerA, messageHandlerB);
                Assert.AreNotSame(messageHandlerB, messageHandlerC);
            }

            var messageHandlerD = provider.GetRequiredService<MessageHandler1>();

            Assert.AreNotSame(messageHandlerA, messageHandlerD);
        }

        [TestMethod]
        public void Add_AddsScopedMessageHandler_IfMessageHandlerHasScopedLifetime()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(ScopedMessageHandler));

            var provider = BuildServiceProvider();
            var messageHandlerA = provider.GetRequiredService<ScopedMessageHandler>();

            using (var scope = provider.CreateScope())
            {
                var messageHandlerB = scope.ServiceProvider.GetRequiredService<ScopedMessageHandler>();
                var messageHandlerC = scope.ServiceProvider.GetRequiredService<ScopedMessageHandler>();

                Assert.AreNotSame(messageHandlerA, messageHandlerB);
                Assert.AreSame(messageHandlerB, messageHandlerC);
            }

            var messageHandlerD = provider.GetRequiredService<ScopedMessageHandler>();

            Assert.AreSame(messageHandlerA, messageHandlerD);
        }

        [TestMethod]
        public void Add_AddsSingletonMessageHandler_IfMessageHandlerHasSingletonLifetime()
        {
            ProcessorBuilder.MessageHandlers.Add(typeof(SingletonMessageHandler));

            var provider = BuildServiceProvider();
            var messageHandlerA = provider.GetRequiredService<SingletonMessageHandler>();

            using (var scope = provider.CreateScope())
            {
                var messageHandlerB = scope.ServiceProvider.GetRequiredService<SingletonMessageHandler>();
                var messageHandlerC = scope.ServiceProvider.GetRequiredService<SingletonMessageHandler>();

                Assert.AreSame(messageHandlerA, messageHandlerB);
                Assert.AreSame(messageHandlerB, messageHandlerC);
            }

            var messageHandlerD = provider.GetRequiredService<SingletonMessageHandler>();

            Assert.AreSame(messageHandlerA, messageHandlerD);
        }

        #endregion

        #region [====== AddInstance ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddInstance_Throws_IfMessageHandlerIsNull()
        {
            ProcessorBuilder.MessageHandlers.AddInstance(null);
        }

        [TestMethod]
        public void AddInstance_AddsAllExpectedMappings_IfMessageHandlerImplementsMultipleInterfaces()
        {
            var messageHandler = new MessageHandler2();

            ProcessorBuilder.MessageHandlers.AddInstance(messageHandler);

            var provider = BuildServiceProvider();

            Assert.AreSame(messageHandler, provider.GetRequiredService<IMessageHandler<object>>());
            Assert.AreSame(messageHandler, provider.GetRequiredService<IMessageHandler<int>>());
            Assert.AreSame(messageHandler, provider.GetRequiredService<MessageHandler2>());
        }        

        [TestMethod]
        public void AddInstance_AddsAllExpectedMappings_IfMessageHandlerIsAction()
        {            
            ProcessorBuilder.MessageHandlers.AddInstance<object>((message, context) => { });

            var provider = BuildServiceProvider();

            var messageHandlerA = provider.GetRequiredService<IMessageHandler<object>>();
            var messageHandlerB = provider.GetRequiredService<IMessageHandler<object>>();

            Assert.AreSame(messageHandlerA, messageHandlerB);
        }

        [TestMethod]
        public void AddInstance_AddsAllExpectedMappings_IfMessageHandlerIsFunc()
        {
            ProcessorBuilder.MessageHandlers.AddInstance<object>((message, context) => Task.CompletedTask);

            var provider = BuildServiceProvider();

            var messageHandlerA = provider.GetRequiredService<IMessageHandler<object>>();
            var messageHandlerB = provider.GetRequiredService<IMessageHandler<object>>();

            Assert.AreSame(messageHandlerA, messageHandlerB);
        }

        #endregion
    }
}