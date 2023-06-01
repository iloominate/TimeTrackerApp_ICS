using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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

    public string? StartString { get; set; }

    public string? EndString { get; set; }

    private DateTime _startDateTime;
    private DateTime _endDateTime;
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
        StartString = Activity.Start.ToString("yyyy/MM/dd HH:mm");
        EndString = Activity.End.ToString("yyyy/MM/dd HH:mm");
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            if (DateTime.TryParseExact(StartString, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _startDateTime) &&
                 DateTime.TryParseExact(EndString, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _endDateTime))
            {
                Activity.Start = _startDateTime;
                Activity.End = _endDateTime;

                await _activityFacade.SaveAsync(Activity);
                MessengerService.Send(new ActivityEditMessage
                {
                    ProjectId = Activity.ProjectId,
                    ActivityId = Activity.Id,

                });

                _navigationService.SendBackButtonPressed();
            }
            else
            {
                await _alertService.DisplayAsync("Invalid DateTime format",
                    "DateTime must be in the format 'yyyy/mm/dd hh:mm'");
            }

        }
        catch (Exception e)
        {
            await _alertService.DisplayAsync("Activity save error", "Activities from one user can't intersect");
        }
    }

    public async void Receive(GetActivityMessage message)
    {
        await LoadDataAsync();
    }

    
}
