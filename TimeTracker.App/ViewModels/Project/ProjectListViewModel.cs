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
using TimeTracker.App.ViewModels.User;

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
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public Guid ActiveUserId { get; set; }

    public UserDetailModel? ActiveUser { get; set; }
    public ProjectListViewModel(
        IProjectFacade projectFacade,
        IProjectAmountFacade projectAmountFacade,
        IProjectAmountModelMapper projectAmountModelMapper,
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IActivityFacade activityFacade,
        IAlertService alertService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _projectAmountFacade = projectAmountFacade;
        _userFacade = userFacade;
        _projectAmountModelMapper = projectAmountModelMapper;
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
        ActiveUser = await _userFacade.GetAsync(ActiveUserId);
    }
    
    [RelayCommand]
    private async Task GoToCreateAsync(Guid activeUserId)
        => await _navigationService.GoToAsync<ProjectCreateViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectCreateViewModel.ActiveUserId)] = activeUserId });

    [RelayCommand]
    private async Task GoToProjectEditAsync(Guid projectId)
    {

        Dictionary<string, object?> parametersToPass = new();
        parametersToPass[nameof(ProjectEditViewModel.ActiveUserId)] = ActiveUserId;
        parametersToPass[nameof(ProjectEditViewModel.ProjectId)] = projectId;

        await _navigationService.GoToAsync<ProjectCreateViewModel>(parametersToPass);

    }
    [RelayCommand]
    private async Task GoToDetailOrEditAsync(Guid projectId)
    {
        UserDetailModel? userToJoin = await _userFacade.GetAsync(ActiveUserId);
        if (userToJoin == null)
        {
            throw new NullReferenceException("ProjectListViewModel userToJoin is null");
        }

        if (userToJoin.Projects.Any(p => p.ProjectId == projectId))
        {
            Dictionary<string, object?> parametersToPass = new();
            parametersToPass[nameof(ProjectEditViewModel.ProjectId)] = projectId;
            parametersToPass[nameof(ProjectEditViewModel.ActiveUserId)] = ActiveUserId;

            await _navigationService.GoToAsync<ProjectEditViewModel>(parametersToPass);
        } else
        {
            await _navigationService.GoToAsync<ProjectDetailViewModel>(
                new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.ProjectId)] = projectId });
        }
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
                throw new NullReferenceException("ProjectListViewModel activeUser is null");
            }


            if (activeUser.Projects.Any(p => p.ProjectId == projectListModel.Id && p.UserId == ActiveUserId))
            {
                await _alertService.DisplayAsync("Join project error", "User is already in project"); 
            }
            else
            {
                await _projectAmountFacade.SaveAsync(projectAmountDetailModelNew);

                ProjectDetailModel? projectToJoin = await _projectFacade.GetAsync(projectListModel.Id);

                if (projectToJoin == null)
                {
                    throw new NullReferenceException("An error occurred trying to add user to project.");
                }
            }
        }
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task LeaveAsync(ProjectListModel? projectListModel)
    {
        if (ActiveUserId != Guid.Empty &&
            projectListModel != null
            )
        {

            UserDetailModel? activeUser = await _userFacade.GetAsync(ActiveUserId);

            if (activeUser == null)
            {
                throw new NullReferenceException("ProjectListViewModel activeUser is null");
            }
            ProjectAmountListModel? projectAmountToDelete = activeUser.Projects.SingleOrDefault<ProjectAmountListModel>(u => u.UserId == activeUser.Id && u.ProjectId == projectListModel.Id);
            var activitiesToDele = activeUser.Activities.Where(act => act.ProjectId == projectListModel.Id);

            foreach(var activity in activitiesToDele)
            {
                await _activityFacade.DeleteAsync(activity.Id);
            }

            if (projectAmountToDelete != null)
            {
                activeUser.Projects.Remove(projectAmountToDelete);

                await _projectAmountFacade.DeleteAsync(projectAmountToDelete.Id);
            }
            else
            { 
                await _alertService.DisplayAsync("Leave project error", "User is not in this project"); // fix alerts
            }
        }
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task DeleteAsync(Guid Id)
    {
        await _projectFacade.DeleteAsync(Id);

        MessengerService.Send(new ProjectDeleteMessage());

    }

    public async void Receive(UserToProjectAdd message)
    {

        await LoadDataAsync();
    }

    public async void Receive(UserToProjectRemove message)
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
