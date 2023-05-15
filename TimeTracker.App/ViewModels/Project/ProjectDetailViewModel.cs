using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.App.Messages;
using System.Collections.ObjectModel;
using TimeTracker.BL.Models.ListModels;

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
public partial class ProjectDetailViewModel : ViewModelBase, 
    IRecipient<ProjectEditMessage>,
    IRecipient<ProjectActivityAddMessage>,
    IRecipient<ProjectActivityDeleteMessage>,
    IRecipient<UserToProjectAdd>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade; 
    private readonly IProjectAmountFacade _projectAmountFacade;

    private readonly INavigationService _navigationService;

    public Guid ProjectId { get; set; }
    public Guid ActiveUserId { get; set; }
    public ProjectDetailModel? Project { get; set; }

    public ObservableCollection<ActivityListModel> ActivityList { get; set; } = new();

    public ObservableCollection<UserListModel> UserList { get; set; } = new();

    public UserDetailModel ActiveUser { get; set; }


    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        IUserFacade userFacade,
        IActivityFacade activityFacade,
        IProjectAmountFacade projectAmountFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _userFacade = userFacade;
        _activityFacade = activityFacade;
        _projectAmountFacade = projectAmountFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(ProjectId);
    }

    [RelayCommand]
    private async Task DeleteProjectAsync()
    {
        if (Project is not null)
        {
            await _projectFacade.DeleteAsync(Project.Id);

            MessengerService.Send(new ProjectDeleteMessage());

            _navigationService.SendBackButtonPressed();
        }
    }



    public async void Receive(ProjectEditMessage message)
    {
        if (message.ProjectId == Project?.Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(ProjectActivityAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserToProjectAdd message)
    {
        await LoadDataAsync();
    }

    public async void Recieve(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
