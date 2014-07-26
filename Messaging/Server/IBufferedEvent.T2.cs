﻿namespace System.ComponentModel.Messaging.Server
{
    internal interface IBufferedEvent<out TKey, out TVersion>
        where TKey : struct, IEquatable<TKey>
        where TVersion : struct, IEquatable<TVersion>
    {
        void WriteTo(IWritableEventStream<TKey, TVersion> stream);
    }
}