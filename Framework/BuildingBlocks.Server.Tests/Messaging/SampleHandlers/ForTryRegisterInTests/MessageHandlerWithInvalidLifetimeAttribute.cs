﻿using System.Threading.Tasks;

namespace Kingo.BuildingBlocks.Messaging.SampleHandlers.ForTryRegisterInTests
{
    [MessageHandler((InstanceLifetime) 5)]
    internal sealed class MessageHandlerWithInvalidLifetimeAttribute : IMessageHandler<Command>
    {
        public Task HandleAsync(Command message)
        {
            return Task.Delay(0);
        }            
    }
}