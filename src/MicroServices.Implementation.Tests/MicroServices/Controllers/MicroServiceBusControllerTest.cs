﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Kingo.MicroServices.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Kingo.MicroServices.MessageFactoryTest;

namespace Kingo.MicroServices.Controllers
{
    [TestClass]
    public sealed class MicroServiceBusControllerTest
    {
        #region [====== MicroServiceBusControllerStub ======]

        private sealed class MicroServiceBusControllerStub : MicroServiceBusController
        {
            private readonly MicroServiceBusControllerOptions _options;

            public MicroServiceBusControllerStub(IMicroProcessor processor, MicroServiceBusControllerOptions options) : base(processor)
            {
                _options = options;
            }

            protected override MicroServiceBusControllerOptions Options =>
                _options;

            public new MicroServiceBusOutboxStub Outbox =>
                base.Outbox as MicroServiceBusOutboxStub;

            protected override MicroServiceBusOutbox CreateOutbox(IMicroServiceBus bus) =>
                new MicroServiceBusOutboxStub(bus);

            public new MicroServiceBusInboxStub Inbox =>
                base.Inbox as MicroServiceBusInboxStub;

            protected override MicroServiceBusInbox CreateInbox(IEnumerable<IMicroServiceBusEndpoint> endpoints) =>
                new MicroServiceBusInboxStub(endpoints);
        }

        #endregion

        #region [====== MicroServiceBusOutboxStub ======]

        private sealed class MicroServiceBusOutboxStub : MicroServiceBusOutbox
        {
            private sealed class SenderClientStub : MicroServiceBusClientStub
            {
                private readonly MicroServiceBusOutboxStub _outbox;

                public SenderClientStub(MicroServiceBusOutboxStub outbox)
                {
                    _outbox = outbox;
                }

                protected override async Task SendAsync(IMessage[] messages)
                {
                    await base.SendAsync(messages);
                    await _outbox.Receiver.SendAsync(messages);
                }

                protected override TransactionScope CreateTransactionScope() =>
                    new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled);
            }

            private sealed class ReceiverClientStub : MicroServiceBusClientStub
            {
                private readonly MicroServiceBusOutboxStub _outbox;

                public ReceiverClientStub(MicroServiceBusOutboxStub outbox)
                {
                    _outbox = outbox;
                }

                protected override async Task SendAsync(IMessage[] messages)
                {
                    await base.SendAsync(messages);
                    await _outbox.ServiceBus.SendAsync(messages);
                }

                protected override TransactionScope CreateTransactionScope() =>
                    new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled);
            }

            private MicroServiceBusClientStub _sender;
            private MicroServiceBusClientStub _receiver;

            public MicroServiceBusOutboxStub(IMicroServiceBus serviceBus) : base(serviceBus) { }

            public new MicroServiceBusClientStub Sender =>
                _sender;

            public new MicroServiceBusClientStub Receiver =>
                _receiver;

            protected override Task<MicroServiceBusClient> CreateSenderAsync(CancellationToken token) =>
                Task.FromResult<MicroServiceBusClient>(_sender = new SenderClientStub(this));

            protected override Task<MicroServiceBusClient> CreateReceiverAsync(CancellationToken token) =>
                Task.FromResult<MicroServiceBusClient>(_receiver = new ReceiverClientStub(this));
        }

        #endregion

        #region [====== MicroServiceBusInboxStub ======]

        private sealed class MicroServiceBusInboxStub : MicroServiceBusInbox
        {
            private MicroServiceBusClientStub _sender;
            private MicroServiceBusClientStub _receiver;

            public MicroServiceBusInboxStub(IEnumerable<IMicroServiceBusEndpoint> endpoints) : base(endpoints) { }

            public new MicroServiceBusClientStub Sender =>
                _sender;

            public new MicroServiceBusClientStub Receiver =>
                _receiver;

            protected override Task<MicroServiceBusClient> CreateSenderAsync(CancellationToken token) =>
                Task.FromResult<MicroServiceBusClient>(_sender = new MicroServiceBusClientStub());

            protected override Task<MicroServiceBusClient> CreateReceiverAsync(CancellationToken token) =>
                Task.FromResult<MicroServiceBusClient>(_receiver = new MicroServiceBusClientStub());
        }

        #endregion

        #region [====== MicroServiceBusClientStub  ======]

        private class MicroServiceBusClientStub : MicroServiceBusClient
        {
            private readonly List<IMessage> _messages;

            public MicroServiceBusClientStub()
            {
                _messages = new List<IMessage>();
            }

            public void AssertIsDisposed() =>
                Assert.IsTrue(IsDisposed);

            public void AssertMessageCountIs(int count) =>
                Assert.AreEqual(count, _messages.Count);

            protected override Task SendAsync(IMessage[] messages)
            {
                _messages.AddRange(messages);
                return Task.CompletedTask;
            }

            protected override TransactionScope CreateTransactionScope() =>
                new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled);
        }

        #endregion

        #region [====== StartAsync(...), StopAsync(...) & DisposeAsync(...) ======]

        [TestMethod]
        public async Task StartAsync_SuccessfullyStartsController_IfControllerIsStopped_And_ModeIsDisabled()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Disabled))
            {
                await controller.StartAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNull(controller.Outbox.Sender);
                Assert.IsNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNull(controller.Inbox.Sender);
                Assert.IsNull(controller.Inbox.Receiver);
            }
        }

        [TestMethod]
        public async Task StartAsync_SuccessfullyStartsController_IfControllerIsStopped_And_ModeIsSend()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Send))
            {
                await controller.StartAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNotNull(controller.Outbox.Sender);
                Assert.IsNotNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNotNull(controller.Inbox.Sender);
                Assert.IsNull(controller.Inbox.Receiver);
            }
        }

        [TestMethod]
        public async Task StartAsync_SuccessfullyStartsController_IfControllerIsStopped_And_ModeIsReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Receive))
            {
                await controller.StartAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNull(controller.Outbox.Sender);
                Assert.IsNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNull(controller.Inbox.Sender);
                Assert.IsNotNull(controller.Inbox.Receiver);
            }
        }

        [TestMethod]
        public async Task StartAsync_SuccessfullyStartsController_IfControllerIsStopped_And_ModeIsSendAndReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.SendAndReceive))
            {
                await controller.StartAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNotNull(controller.Outbox.Sender);
                Assert.IsNotNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNotNull(controller.Inbox.Sender);
                Assert.IsNotNull(controller.Inbox.Receiver);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task StartAsync_Throws_IfControllerIsAlreadyStarted()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Disabled))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.StartAsync(CancellationToken.None);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task StartAsync_Throws_IfControllerIsDisposed()
        {
            var controller = CreateController(MicroServiceBusModes.Disabled);
            controller.Dispose();

            await controller.StartAsync(CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task StartAsync_Throws_IfControllerIsDisposedAsynchronously()
        {
            var controller = CreateController(MicroServiceBusModes.Disabled);
            await controller.DisposeAsync();

            await controller.StartAsync(CancellationToken.None);
        }

        [TestMethod]
        public async Task StopAsync_DoesNothing_IfControllerIsStopped()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Disabled))
            {
                await controller.StopAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNull(controller.Outbox.Sender);
                Assert.IsNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNull(controller.Inbox.Sender);
                Assert.IsNull(controller.Inbox.Receiver);
            }
        }

        [TestMethod]
        public async Task StopAsync_SuccessfullyStopsController_IfControllerIsStarted_And_ModeIsDisabled()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Disabled))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.StopAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNull(controller.Outbox.Sender);
                Assert.IsNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNull(controller.Inbox.Sender);
                Assert.IsNull(controller.Inbox.Receiver);
            }
        }

        [TestMethod]
        public async Task StopAsync_SuccessfullyStopsController_IfControllerIsStarted_And_ModeIsSend()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Send))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.StopAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNotNull(controller.Outbox.Sender);
                Assert.IsNotNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNotNull(controller.Inbox.Sender);
                Assert.IsNull(controller.Inbox.Receiver);

                controller.Outbox.Sender.AssertIsDisposed();
                controller.Outbox.Receiver.AssertIsDisposed();
                controller.Inbox.Sender.AssertIsDisposed();
            }
        }

        [TestMethod]
        public async Task StopAsync_SuccessfullyStopsController_IfControllerIsStarted_And_ModeIsReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Receive))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.StopAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNull(controller.Outbox.Sender);
                Assert.IsNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNull(controller.Inbox.Sender);
                Assert.IsNotNull(controller.Inbox.Receiver);

                controller.Inbox.Receiver.AssertIsDisposed();
            }
        }

        [TestMethod]
        public async Task StopAsync_SuccessfullyStopsController_IfControllerIsStarted_And_ModeIsSendAndReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.SendAndReceive))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.StopAsync(CancellationToken.None);

                Assert.IsNotNull(controller.Outbox);
                Assert.IsNotNull(controller.Outbox.Sender);
                Assert.IsNotNull(controller.Outbox.Receiver);

                Assert.IsNotNull(controller.Inbox);
                Assert.IsNotNull(controller.Inbox.Sender);
                Assert.IsNotNull(controller.Inbox.Receiver);

                controller.Outbox.Sender.AssertIsDisposed();
                controller.Outbox.Receiver.AssertIsDisposed();
                controller.Inbox.Sender.AssertIsDisposed();
                controller.Inbox.Receiver.AssertIsDisposed();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task StopAsync_Throws_IfControllerIsDisposed()
        {
            var controller = CreateController(MicroServiceBusModes.Disabled);
            controller.Dispose();

            await controller.StopAsync(CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task StopAsync_Throws_IfControllerIsDisposedAsynchronously()
        {
            var controller = CreateController(MicroServiceBusModes.Disabled);
            await controller.DisposeAsync();

            await controller.StopAsync(CancellationToken.None);
        }

        #endregion

        #region [====== SendAsync(...) ======]

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendAsync_Throws_IfControllerIsStopped_And_ModeIsDisabled()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Disabled))
            {
                await controller.SendAsync(CreateMessages(0));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendAsync_Throws_IfControllerIsStarted_But_ModeIsDisabled()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Disabled))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.SendAsync(CreateMessages(0));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendAsync_Throws_IfControllerIsStopped_And_ModeIsSend()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Send))
            {
                await controller.SendAsync(CreateMessages(0));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SendAsync_Throws_IfControllerIsStarted_And_ModeIsSend_But_MessagesIsNull()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Send))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.SendAsync(null);
            }
        }

        [TestMethod]
        public async Task SendAsync_SuccessfullySendsMessages_IfControllerIsStarted_And_ModeIsSend_And_MessagesIsNotNull()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Send))
            {
                var messages = CreateMessages(DateTimeOffset.UtcNow.Millisecond + 1).ToArray();

                await controller.StartAsync(CancellationToken.None);
                await controller.SendAsync(messages);

                controller.Outbox.Sender.AssertMessageCountIs(messages.Length);
                controller.Outbox.Receiver.AssertMessageCountIs(messages.Length);
                controller.Inbox.Sender.AssertMessageCountIs(messages.Length);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendAsync_Throws_IfControllerIsStopped_And_ModeIsReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Receive))
            {
                await controller.SendAsync(CreateMessages(0));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendAsync_Throws_IfControllerIsStarted_But_ModeIsReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.Receive))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.SendAsync(CreateMessages(0));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendAsync_Throws_IfControllerIsStopped_And_ModeIsSendAndReceive()
        {
            await using (var controller = CreateController(MicroServiceBusModes.SendAndReceive))
            {
                await controller.SendAsync(CreateMessages(0));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SendAsync_Throws_IfControllerIsStarted_And_ModeIsSendAndReceive_But_MessagesIsNull()
        {
            await using (var controller = CreateController(MicroServiceBusModes.SendAndReceive))
            {
                await controller.StartAsync(CancellationToken.None);
                await controller.SendAsync(null);
            }
        }

        [TestMethod]
        public async Task SendAsync_SuccessfullySendsMessages_IfControllerIsStarted_And_ModeIsSendAndReceive_And_MessagesIsNotNull()
        {
            await using (var controller = CreateController(MicroServiceBusModes.SendAndReceive))
            {
                var messages = CreateMessages(DateTimeOffset.UtcNow.Millisecond + 1).ToArray();

                await controller.StartAsync(CancellationToken.None);
                await controller.SendAsync(messages);

                controller.Outbox.Sender.AssertMessageCountIs(messages.Length);
                controller.Outbox.Receiver.AssertMessageCountIs(messages.Length);
                controller.Inbox.Sender.AssertMessageCountIs(messages.Length);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task SendAsync_Throws_IfControllerIsDisposed()
        {
            var controller = CreateController(MicroServiceBusModes.SendAndReceive);
            controller.Dispose();
            
            await controller.SendAsync(CreateMessages(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task SendAsync_Throws_IfControllerIsDisposedAsynchronously()
        {
            var controller = CreateController(MicroServiceBusModes.SendAndReceive);
            await controller.DisposeAsync();

            await controller.SendAsync(CreateMessages(0));
        }

        #endregion

        private static MicroServiceBusControllerStub CreateController(MicroServiceBusModes modes) =>
            CreateController(modes, new ServiceCollection());

        private static MicroServiceBusControllerStub CreateController(MicroServiceBusModes modes, IServiceCollection services)
        {
            return
                services.AddTransient(provider => new MicroServiceBusControllerOptions()
                {
                    Modes = modes

                }).AddMicroProcessor(processor =>
                {
                    processor.ConfigureMicroServiceBusControllers(controllers =>
                    {
                        controllers.Add<MicroServiceBusControllerStub>();
                    });
                }).BuildServiceProvider(true).GetRequiredService<MicroServiceBusControllerStub>(); 
        }
    }
}
