﻿using System;
using System.Collections.Generic;
using Kingo.Reflection;

namespace Kingo.MicroServices.Controllers
{
    internal sealed class MicroServiceBusResolver : IMicroServiceBusResolver
    {
        private readonly IEnumerable<Type> _microServiceBusTypes;

        public MicroServiceBusResolver(IEnumerable<Type> microServiceBusTypes)
        {
            _microServiceBusTypes = microServiceBusTypes;
        }

        public IMicroServiceBus ResolveMicroServiceBus(IServiceProvider serviceProvider) =>
            new MicroServiceBus(serviceProvider, _microServiceBusTypes);

        public override string ToString() =>
            typeof(MicroServiceBus).FriendlyName();
    }
}
