using CommunityToolkit.Mvvm.Input;
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

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
public partial class ProjectEditViewModel : ViewModelBase,
    IRecipient<ProjectEditMessage>,
    IRecipient<ActivityEditMessage>,
    IRecipient<UserToProjectRemove>,
    IRecipient<UserToProjectAdd>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly IProjectAmountFacade _projectAmountFacade;
    private readonly IUserModelMapper _userModelMapper;


    private readonly INavigationService _navigationService;

    public Guid ProjectId { get; set; }
    public Guid ActiveUserId { get; set; }
    public ObservableCollection<ActivityListModel> ActivityList { get; set; } = new();
    public ObservableCollection<UserListModel> UserList { get; set; } = new();
    public ProjectDetailModel? Project { get; set; }

    public ProjectEditViewModel(
        IProjectFacade projectFacade,
        IUserFacade userFacade,
        IActivityFacade activityFacade,
        IProjectAmountFacade projectAmountFacade,
        INavigationService navigationService,
        IUserModelMapper userModelMapper,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _userFacade = userFacade;
        _activityFacade = activityFacade;
        _projectAmountFacade = projectAmountFacade;
        _navigationService = navigationService;
        _userModelMapper = userModelMapper;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(ProjectId);
        foreach (ProjectAmountListModel User in Project.Users)
        {
            UserList.Append<UserListModel>(_userModelMapper.MapToListModel(await _userFacade.GetAsync(User.UserId)));
        }
        ActivityList = Project.Activities;
    }
    [RelayCommand]
    private async Task GoToActivityDetailAsync(Guid activityId)
    {
        Dictionary<string, object?> parametersToPass = new();
        parametersToPass[nameof(ActivityDetailViewModel.ActivityId)] = activityId;
        parametersToPass[nameof(ActivityDetailViewModel.ActiveUserId)] = ActiveUserId;

        await _navigationService.GoToAsync<ActivityDetailViewModel>(parametersToPass);
        MessengerService.Send(new GetUserMessage()); // ensures that Activity model will be loaded
    }

    [RelayCommand]
    private async Task GoToActivityEditAsync()
    {
        Dictionary<string, object?> parametersToPass = new();
        parametersToPass[nameof(ActivityEditViewModel.ProjectId)] = ProjectId;
        parametersToPass[nameof(ActivityEditViewModel.ActiveUserId)] = ActiveUserId;

        await _navigationService.GoToAsync<ActivityEditViewModel>(parametersToPass);
        MessengerService.Send(new GetActivityMessage()); // ensures that Activity model will be loaded
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
}