﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Messaging.Client.DataVirtualization
{
    /// <summary>
    /// Represents the state of a <see cref="VirtualCollectionItem{T}" />.
    /// </summary>
    internal enum VirtualCollectionItemStatus
    {
        /// <summary>
        /// Indicates that the item has not (yet) been loaded.
        /// </summary>
        NotLoaded,

        /// <summary>
        /// Indicates that the item has been loaded.
        /// </summary>
        Loaded,

        /// <summary>
        /// Indicates that an error occurred while loading the item.
        /// </summary>
        FailedToLoad
    }
}