using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.Common.Enums;
using System.Globalization;
using CookBook.App.Services;

namespace TimeTracker.App.ViewModels.Activity;

public partial class ActivityListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService; 

    public IEnumerable<ActivityListModel> Activities { get; set; } = null;

    public string filterStartDate { get; set; } = "";
    public string filterFinishDate { get; set; } = "";

    public ActivityType? filterActivityType { get; set; } = null;


    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IAlertService alertService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync(); 
        Activities = await _activityFacade.GetAsync();
    }

    protected async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.ActivityId)] = id });
    } 

    private async Task FastFilterAsync(FastFilterType type)
    {
        switch (type)
        {
            case FastFilterType.Day:
                {
                    break;
                }
            case FastFilterType.Week:
                {
                    break;
                }
            case FastFilterType.Month:
                {
                    break;
                }
            case FastFilterType.Year:
                {
                    break;
                }
        }
    }

    private async Task FilterAsync(DateTime start, DateTime finish)
    {
        
    }
    public async void Receive (ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive (ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
