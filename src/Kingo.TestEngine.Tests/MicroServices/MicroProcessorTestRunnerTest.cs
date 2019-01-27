﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.MicroServices
{
    [TestClass]
    public sealed class MicroProcessorTestRunnerTest : MicroProcessorTestRunner
    {
        #region [====== MessageHandlerTests ======]

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public async Task RunMessageHandlerTest_Throws_IfEmptyEventStreamIsExpected_But_ResultIsException()
        {
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) =>
                    {
                        throw NewRandomBusinessRuleException();
                    });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream => stream);
                });

            await RunAsync(test);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task RunMessageHandlerTest_Throws_IfEventStreamIsExpected_But_NumberOfExpectedEventsIsInvalid()
        {
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) => { });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream => stream, -1);
                });

            await RunAsync(test);
        }

        [TestMethod]        
        public async Task RunMessageHandlerTest_Succeeds_IfEmptyEventStreamIsExpected_And_NoEventsWerePublished()
        {
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) => { });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream => stream);
                });

            await RunAsync(test);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public async Task RunMessageHandlerTest_Throws_IfEmptyEventStreamIsExpected_But_OneEventWasPublished()
        {
            var @event = new object();
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) =>
                    {
                        context.EventBus.Publish(@event);
                    });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream => stream, 0);
                });

            await RunAsync(test);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public async Task RunMessageHandlerTest_Throws_IfOneEventIsExpected_But_NoEventsWerePublished()
        {            
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) => { });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream => stream, 1);
                });

            await RunAsync(test);
        }

        [TestMethod]        
        public async Task RunMessageHandlerTest_Succeeds_IfOneEventIsExpected_And_OneEventWasPublished()
        {
            var @event = new object();
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) =>
                    {
                        context.EventBus.Publish(@event);
                    });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream =>
                    {
                        Assert.AreSame(@event, stream[0]);
                        return stream;
                    }, 1);
                });

            await RunAsync(test);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public async Task RunMessageHandlerTest_Throws_IfOneEventIsExpected_But_DifferentEventWasPublished()
        {
            var @event = new object();
            var test = CreateMessageHandlerTest()
                .When(async (messageProcessor, testContext) =>
                {
                    await messageProcessor.HandleAsync(new object(), (processor, context) =>
                    {
                        context.EventBus.Publish(@event);
                    });
                })
                .Then((message, result, testContext) =>
                {
                    result.IsExpectedEventStream(stream =>
                    {
                        Assert.AreNotSame(@event, stream[0]);
                        return stream;
                    }, 1);
                });

            await RunAsync(test);
        }

        private static Exception NewRandomBusinessRuleException() =>
            new BusinessRuleException(Guid.NewGuid().ToString("N"));

        #endregion

        #region [====== QueryTests (1) ======]

        #endregion

        #region [====== QueryTests (2) ======]

        #endregion  

        private static MessageHandlerTestDelegate<object, EventStream> CreateMessageHandlerTest() =>
            CreateMessageHandlerTest<object, EventStream>();
        
        private static MessageHandlerTestDelegate<TMessage, TEventStream> CreateMessageHandlerTest<TMessage, TEventStream>() where TEventStream : EventStream =>
            new MessageHandlerTestDelegate<TMessage, TEventStream>();
    }
}