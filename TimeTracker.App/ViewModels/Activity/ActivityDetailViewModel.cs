using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using System.Net.Http.Headers;
using Microsoft.UI.Xaml.Media;

namespace TimeTracker.App.ViewModels.Activity;

[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
[QueryProperty(nameof(ActivityId), nameof(ActivityId))]
public partial class ActivityDetailViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;
    private readonly IUserFacade _userFacade;


    public Guid ActivityId { get; set; }
    public Guid ActiveUserId { get; set; }

    public ActivityDetailModel? Activity { get; private set; }

    public String UserOwnerName { get; set; } = "";

    public ActivityDetailViewModel (
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IUserFacade userFacade,
        IAlertService alertService) 
        : base (messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _alertService = alertService;
        _userFacade = userFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync(); 

        Activity = await _activityFacade.GetAsync(ActivityId);
        UserDetailModel? userOwner = await _userFacade.GetAsync(Activity.UserId);
        UserOwnerName = userOwner.Name;
    }


    [RelayCommand]
    private async Task GoToEditAsync()
    {
        Dictionary<string, object?> parametersToPass = new();
        parametersToPass[nameof(ActivityEditViewModel.ActivityId)] = ActivityId;
        await _navigationService.GoToAsync<ActivityEditViewModel>(parametersToPass);
        MessengerService.Send(new GetUserMessage());
    }

    public async void Receive (ActivityEditMessage message)
    {
        if (message.ActivityId == Activity?.Id)
        {
            await LoadDataAsync();
        }
    }
}

