﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.App.Messages;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.App.ViewModels.Activity;
using System.Collections.ObjectModel;
using TimeTracker.BL.Models.ListModels;
using System.Diagnostics;
using TimeTracker.BL.Mappers;
using TimeTracker.Common.Enums;
using Windows.Graphics.Printing;
using System.Globalization;

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
public partial class ProjectDetailViewModel : ViewModelBase, 
    IRecipient<ProjectEditMessage>,
    IRecipient<ActivityEditMessage>,
    IRecipient<UserToProjectRemove>,
    IRecipient<UserToProjectAdd>,
    IRecipient<ActivityDeleteMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade; 
    private readonly IProjectAmountFacade _projectAmountFacade;
    private readonly IUserModelMapper _userModelMapper;
    private readonly IAlertService _alertService;

    private readonly INavigationService _navigationService;

    public Guid ProjectId { get; set; }
    public Guid ActiveUserId { get; set; }
    public ProjectDetailModel? Project { get; set; }


    public ObservableCollection<UserListModel> UserList { get; set; } = new();
    public ObservableCollection<ActivityListModel> ActivityList { get; set; } = new();

    public string FilterStartDate { get; set; } = "";
    public string FilterFinishDate { get; set; } = "";
    public ActivityType? FilterActivityType { get; set; } = null;
    public Guid? FilterUserId { get; set; } = null;



    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        IUserFacade userFacade,
        IActivityFacade activityFacade,
        IProjectAmountFacade projectAmountFacade,
        INavigationService navigationService,
        IUserModelMapper userModelMapper,
        IAlertService alertService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _userFacade = userFacade;
        _activityFacade = activityFacade;
        _projectAmountFacade = projectAmountFacade;
        _userModelMapper = userModelMapper;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    [RelayCommand]
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(ProjectId);
        if (Project == null)
        {
            throw new NullReferenceException("ProjectDetailViewModel Project is null");
        }

        UserList.Clear();
        foreach (ProjectAmountListModel User in Project.Users)
        {
            UserDetailModel? userDetailToAdd = await _userFacade.GetAsync(User.UserId);

            if (userDetailToAdd == null)
            {
                throw new NullReferenceException("ProjectDetailViewModel userDetailToAdd is null");
            }

            UserListModel? userListToAdd = _userModelMapper.MapToListModel(userDetailToAdd);
            UserList.Insert(0, userListToAdd);
        }
        ActivityList = Project.Activities;
    }

    [RelayCommand]
    private async Task FastFilterByTypeAsync(FastFilterType type)
    {
        switch (type)
        {
            case FastFilterType.Day:
                {
                    await FastFilterAsync(DateTime.Now.AddHours(-24), DateTime.Now);
                    break;
                }
            case FastFilterType.Week:
                {
                    await FastFilterAsync(DateTime.Now.AddDays(-7), DateTime.Now);
                    break;
                }
            case FastFilterType.Month:
                {
                    await FastFilterAsync(DateTime.Now.AddDays(-31), DateTime.Now);
                    break;
                }
            case FastFilterType.Year:
                {
                    await FastFilterAsync(DateTime.Now.AddDays(-365), DateTime.Now);
                    break;
                }
        }
    }

    private async Task FastFilterAsync(DateTime start, DateTime finish)
    {
        await base.LoadDataAsync();
        ActivityList.Clear();
        IEnumerable<ActivityListModel> activitiesEnumerable = await _activityFacade.GetFilteredAsync(ProjectId, start, finish);
        foreach (ActivityListModel act in activitiesEnumerable)
        {
            ActivityList.Add(act);
        }
    }

    [RelayCommand]
    private async Task FilterAsync()
    {
        DateTime _startDateTime;
        DateTime _finishDateTime;

        if (FilterStartDate == "")
        {
            _startDateTime = DateTime.MinValue;
        }
        else if (!DateTime.TryParseExact(FilterStartDate, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _startDateTime))
        {
            await _alertService.DisplayAsync("Invalid Date Time format",
                   "DateTime must be in the format 'yyyy/mm/dd hh/mm'");
            return;
        }

        if (FilterFinishDate == "")
        {
            _finishDateTime = DateTime.MaxValue;
        }
        else if (!DateTime.TryParseExact(FilterFinishDate, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _finishDateTime))
        {
            await _alertService.DisplayAsync("Invalid Date Time format",
                   "DateTime must be in the format 'yyyy/mm/dd hh/mm'");
            return;
        }


        await base.LoadDataAsync();

        ActivityList.Clear();

        IEnumerable<ActivityListModel> activitiesEnumerable = await _activityFacade.GetFilteredAsync(ProjectId, _startDateTime, _finishDateTime, FilterUserId, FilterActivityType);
        foreach (ActivityListModel act in activitiesEnumerable)
        {
            ActivityList.Add(act);
        }
    }


    public async void Receive(ProjectEditMessage message)
    {
        if (message.ProjectId == Project?.Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(ActivityEditMessage message)
    {
        if (message.ProjectId == ProjectId)
        {
            await LoadDataAsync();
        }
    }


    public async void Receive(UserToProjectAdd message)
    {
        if (message.ProjectId == ProjectId)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(UserToProjectRemove message)
    {
        if (message.ProjectId == ProjectId)
        {
            await LoadDataAsync();
        }
    }
    public async void Receive(ActivityDeleteMessage message)
    {
        if (message.ProjectId == ProjectId)
        {
            await LoadDataAsync();
        }
    }
}
