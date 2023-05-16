using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services;

namespace TimeTracker.App.ViewModels;


public abstract class ViewModelBase : ObservableRecipient , IViewModel 
{
    private bool isRefreshRequired = true;

    protected readonly IMessengerService MessengerService;

    protected ViewModelBase(IMessengerService messengerService)
        : base(messengerService.Messenger)
    {
        this.MessengerService = messengerService;
        IsActive = true; 
    }
    public async Task OnAppearingAsync()
    {
        if (isRefreshRequired)
        {
            await LoadDataAsync();
            isRefreshRequired = false;
        }
    }
    protected virtual Task LoadDataAsync()
    => Task.CompletedTask;
}
