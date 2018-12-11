﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.MicroServices.Domain
{
    public abstract class AggregateRootSpy : AggregateRoot<Guid, int>
    {
        private readonly bool _subtractOneForNextVersion;

        protected AggregateRootSpy(bool subtractOneForNextVersion) :
            base(new AggregateRootSpyCreatedEvent(Guid.NewGuid()), true)
        {
            _subtractOneForNextVersion = subtractOneForNextVersion;
        }

        protected AggregateRootSpy(AggregateRootSpyCreatedEvent aggregateEvent, bool isNewAggregate) :
            base(aggregateEvent, isNewAggregate) { }        

        protected AggregateRootSpy(SnapshotMock snapshot) :
            base(snapshot)
        {
            Value = snapshot.Value;
        }

        protected override int NextVersion()
        {
            if (_subtractOneForNextVersion)
            {
                return Version - 1;
            }
            return Version + 1;
        }

        protected override void OnRemoved() =>
            Publish(new AggregateRootSpyRemovedEvent());

        protected int Value
        {
            get;
            set;
        }

        public abstract void ChangeValue(int newValue);

        public void AssertVersionIs(int version)
        {
            Assert.AreEqual(version, Version);
        }

        public void AssertValueIs(int value)
        {
            Assert.AreEqual(value, Value);
        }
    }
}