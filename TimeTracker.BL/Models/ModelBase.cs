using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeTracker.BL.Models;

public abstract record ModelBase : INotifyPropertyChanged, IModel
{
    public Guid Id { get; init; }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}