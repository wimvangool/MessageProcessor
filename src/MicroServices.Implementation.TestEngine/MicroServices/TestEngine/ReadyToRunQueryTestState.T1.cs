﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kingo.Reflection;

namespace Kingo.MicroServices.TestEngine
{
    internal sealed class ReadyToRunQueryTestState<TResponse> : MicroProcessorTestState, IReadyToRunQueryTestState<TResponse>
    {
        private readonly MicroProcessorTest _test;
        private readonly IEnumerable<MicroProcessorTestOperation> _givenOperations;
        private readonly QueryTestOperation _whenOperation;

        public ReadyToRunQueryTestState(MicroProcessorTest test, IEnumerable<MicroProcessorTestOperation> givenOperations, QueryTestOperation whenOperation)
        {
            _test = test;
            _givenOperations = givenOperations;
            _whenOperation = whenOperation;
        }

        protected override MicroProcessorTest Test =>
            _test;

        public override string ToString() =>
            $"Ready to process request with query of type '{_whenOperation.QueryType.FriendlyName()}'...";

        public Task ThenOutputIs<TException>(Action<TResponse, TException, MicroProcessorTestContext> assertMethod = null) where TException : MicroProcessorOperationException =>
            throw new NotImplementedException();

        public Task ThenOutputIsResponse(Action<TResponse, MicroProcessorTestContext> assertMethod = null) =>
            throw new NotImplementedException();
    }
}
