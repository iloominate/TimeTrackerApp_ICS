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
using TimeTracker.App.Messages;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.BL.Mappers;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models.DetailModels;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
public partial class ProjectListViewModel : ViewModelBase, 
    IRecipient<ProjectEditMessage>,
    IRecipient<ProjectDeleteMessage>,
    IRecipient<UserToProjectAdd>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IProjectAmountFacade _projectAmountFacade;
    private readonly IProjectAmountModelMapper _projectAmountModelMapper;
    private readonly IUserFacade _userFacade;
    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public Guid ActiveUserId { get; set; }

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        IProjectAmountFacade projectAmountFacade,
        IProjectAmountModelMapper projectAmountModelMapper,
        IUserFacade userFacade, 
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _projectAmountFacade = projectAmountFacade;
        _userFacade = userFacade;
        _projectAmountModelMapper = projectAmountModelMapper;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
    }
    [RelayCommand]
    private async Task GoToDetailAsync(Guid projectId)
    {
        Dictionary<string, object?> parametersToPass = new();
        parametersToPass[nameof(ProjectDetailViewModel.ProjectId)] = projectId;
        parametersToPass[nameof(ProjectDetailViewModel.ActiveUserId)] = ActiveUserId;

        await _navigationService.GoToAsync<ProjectDetailViewModel>(parametersToPass);
    }

    [RelayCommand]
    private async Task JoinAsync(ProjectListModel? projectListModel)
    {
        if (ActiveUserId != Guid.Empty &&
            projectListModel != null
            )
        {
            ProjectAmountDetailModel projectAmountDetailModelNew = _projectAmountModelMapper.MapToNewDetailModel(projectListModel, ActiveUserId);

            UserDetailModel? activeUser = await _userFacade.GetAsync(ActiveUserId);

            if (activeUser == null)
            {
                throw new NullReferenceException("An error occurred trying to join project.");
            }

            if (activeUser.Projects.Any(p => p.ProjectId == projectListModel.Id && p.UserId == ActiveUserId))
            {
                await _alertService.DisplayAsync("Join project error", "User is already in project"); // fix alerts 
            }
            else
            {
                await _projectAmountFacade.SaveAsync(projectAmountDetailModelNew);

                ProjectDetailModel projectToJoin = await _projectFacade.GetAsync(projectListModel.Id);

                if (projectToJoin == null)
                {
                    throw new NullReferenceException("An error occurred trying to add user to project.");
                }

                MessengerService.Send(new UserToProjectAdd());
            }
        }
    }

    [RelayCommand]
    private async Task LeaveAsync(ProjectListModel? projectListModel)
    {
        if (ActiveUserId != Guid.Empty &&
            projectListModel != null
            )
        {

            UserDetailModel activeUser = await _userFacade.GetAsync(ActiveUserId);

            if (activeUser == null)
            {
                throw new NullReferenceException("An error occurred trying to leave project.");
            }

            ProjectAmountListModel? projectAmountToDelete = activeUser.Projects.SingleOrDefault<ProjectAmountListModel>(u => u.UserId == activeUser.Id && u.ProjectId == projectListModel.Id);
            
            if (projectAmountToDelete != null)
            {
                activeUser.Projects.Remove(projectAmountToDelete);

                await _projectAmountFacade.DeleteAsync(projectAmountToDelete.Id);

                MessengerService.Send(new UserToProjectRemove {ProjectId = projectListModel.Id});
            }
            else
            { 
                await _alertService.DisplayAsync("Leave project error", "User is not in this project"); // fix alerts
            }
        }
    }

    public async void Receive(UserToProjectAdd message)
    {

        await LoadDataAsync();
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
