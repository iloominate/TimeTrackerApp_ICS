using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.Common.Enums;

namespace TimeTracker.App.ViewModels.Activity;

[QueryProperty(nameof(ActivityId), nameof(ActivityId))]
[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
public partial class ActivityEditViewModel : ViewModelBase, IRecipient<GetActivityMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;

    public Guid ActivityId { get; set; } = Guid.Empty;
    public Guid ActiveUserId { get; set; }
    public Guid ProjectId { get; set; }
    public ActivityDetailModel? Activity { get; set; } = ActivityDetailModel.Empty;


    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IAlertService alertService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _alertService = alertService;
        _navigationService = navigationService;
    }

    private async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(ActivityId);

        if (Activity == null)
        {
            Activity = new()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Start = DateTime.Today,
                End = DateTime.Today,
                Type = ActivityType.Other,
                Description = string.Empty,

                UserId = ActiveUserId,
                ProjectId = ProjectId
            };
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            Activity.UserId = ActiveUserId;

            await _activityFacade.SaveAsync(Activity);
            MessengerService.Send(new ActivityEditMessage
            {
                ProjectId = Activity.ProjectId,
                ActivityId = Activity.Id,

            });
        }
        catch (Exception e)
        {
            await _alertService.DisplayAsync("Activity save error", "Activities from one user can't intersect");
        }

        MessengerService.Send(new ActivityEditMessage
        {
            ProjectId = ProjectId,
            ActivityId = ActivityId
        });

        _navigationService.SendBackButtonPressed();
        _navigationService.SendBackButtonPressed();
    }

    public async void Receive(GetActivityMessage message)
    {
        await LoadDataAsync();
    }

    
}
