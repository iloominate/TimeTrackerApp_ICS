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

namespace TimeTracker.App.ViewModels.Project;

public partial class ProjectListViewModel : ViewModelBase, 
    IRecipient<ProjectEditMessage>,
    IRecipient<ProjectDeleteMessage>,
    IRecipient<ProjectToUserAdd>
{
    private readonly IProjectFacade _projectFacade;
    private readonly IProjectAmountFacade _projectAmountFacade;
    private readonly IProjectAmountModelMapper _projectAmountModelMapper;
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public Guid ActiveUserId { get; set; }

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        IProjectAmountFacade projectAmountFacade,
        IProjectAmountModelMapper projectAmountModelMapper,
        IUserFacade userFacade, 
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _projectAmountFacade = projectAmountFacade;
        _userFacade = userFacade;
        _projectAmountModelMapper = projectAmountModelMapper;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await _navigationService.GoToAsync<ProjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id });

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task JoinAsync(ProjectListModel projectListModel)
    {
        if ( ActiveUserId != Guid.Empty && 
            projectListModel != null
            )
        {
            ProjectAmountDetailModel projectAmountDetailModelNew = _projectAmountModelMapper.MapToNewDetailModel(projectListModel, ActiveUserId);
            await _projectAmountFacade.SaveAsync(projectAmountDetailModelNew);

            UserDetailModel activeUser = await _userFacade.GetAsync (ActiveUserId);

            if ( activeUser == null )
            {
                throw new NullReferenceException("An error occured trying to join project.");
            }

            activeUser.Projects.Add(_projectAmountModelMapper.MapToListModel(projectAmountDetailModelNew));

            ProjectDetailModel projectToJoin = await _projectFacade.GetAsync(projectListModel.Id);
            
            if (projectToJoin == null ) 
            { 
                throw new NullReferenceException("An error occurred trying to add user to project.");
            }

            projectToJoin.Users.Add(_projectAmountModelMapper.MapToListModel(projectAmountDetailModelNew));

            MessengerService.Send(new ProjectToUserAdd());
            MessengerService.Send(new UserToProjectAdd());
        }
    }

    public async void Receive(ProjectToUserAdd message)
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
