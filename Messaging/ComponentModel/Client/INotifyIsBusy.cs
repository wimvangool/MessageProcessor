﻿namespace System.ComponentModel.Client
{
    /// <summary>
    /// Represents a component that can indicate whether or not it is busy.
    /// </summary>
    public interface INotifyIsBusy : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when <see cref="IsBusy" /> changed.
        /// </summary>
        event EventHandler IsBusyChanged;

        /// <summary>
        /// Indicates whether or not the component is busy.
        /// </summary>
        bool IsBusy
        {
            get;
        }
    }
}