﻿using System;
using System.Collections.Generic;
using System.Text;
using Kingo.Reflection;

namespace Kingo.MicroServices.TestEngine
{
    internal sealed class WhenRequestState<TRequest> : MicroProcessorTestState, IWhenRequestState<TRequest>
    {
        private readonly MicroProcessorTest _test;
        private readonly MicroProcessorTestOperationQueue _givenOperations;

        public WhenRequestState(MicroProcessorTest test, MicroProcessorTestOperationQueue givenOperations)
        {
            _test = test;
            _givenOperations = givenOperations;
        }

        protected override MicroProcessorTest Test =>
            _test;

        public override string ToString() =>
            $"Configuring a query of type '{typeof(IQuery<,>).FriendlyName()}'...";

        public IWhenReturningState<TRequest, TResponse> Returning<TResponse>() =>
            _test.MoveToState(this, new WhenReturningState<TRequest, TResponse>(_test, _givenOperations));
    }
}