using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.App.Messages;
using CommunityToolkit.Mvvm.Input;
using TimeTracker.BL.Facades;
using Windows.System;
using CommunityToolkit.Mvvm.Messaging;

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
public partial class ProjectCreateViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    //private readonly IAlertService _alertService;

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;
    //public Guid ProjectId { get; set; } = Guid.Empty;
    public Guid ActiveUserId { get; set; }

    public ProjectCreateViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _projectFacade.SaveAsync(Project with {
            Id = Guid.NewGuid(),
            CreatorId = ActiveUserId,
            Users = default!,
            Activities = default! });

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

        _navigationService.SendBackButtonPressed();
    }

    /*private async Task LoadDataAsync()
    {
        Project = new()
        {
            Id = Guid.NewGuid(),
            Name = "",
            CreatorId = ActiveUserId

        };
    }
    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }*/

}
