﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LsLocalizeHelperLib.Models;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Sets property if it does not equal existing value. Notifies listeners if change occurs.
    /// </summary>
    /// <typeparam name="T">Type of property.</typeparam>
    /// <param name="member">The property's backing field.</param>
    /// <param name="value">The new value.</param>
    /// <param name="propertyName">Name of the property used to notify listeners.  This
    /// value is optional and can be provided automatically when invoked from compilers
    /// that support <see cref="CallerMemberNameAttribute"/>.</param>
    protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(x: member, y: value))
        {
            return false;
        }

        member = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    /// Notifies listeners that a property value has changed.
    /// </summary>
    /// <param name="propertyName">Name of the property, used to notify listeners.</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => this.PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
}