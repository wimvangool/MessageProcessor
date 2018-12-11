﻿using System;

namespace Kingo.MicroServices.Domain
{
    public sealed class SnapshotMock : Snapshot<Guid, int>
    {
        private readonly bool _aggregateHasEventHandlers;

        public SnapshotMock(bool aggregateHasEventHandlers)
        {
            _aggregateHasEventHandlers = aggregateHasEventHandlers;
        }        

        public int Value
        {
            get;
            set;
        }

        protected override TAggregate RestoreAggregate<TAggregate>() =>
            (TAggregate) RestoreAggregate();

        private IAggregateRoot RestoreAggregate()
        {
            if (_aggregateHasEventHandlers)
            {
                return new AggregateRootWithEventHandlers(this);
            }
            return new AggregateRootWithoutEventHandlers(this);
        }
    }
}