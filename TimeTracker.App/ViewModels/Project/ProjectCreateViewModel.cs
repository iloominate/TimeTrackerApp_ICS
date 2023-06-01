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
using TimeTracker.BL.Models.ListModels;
using TimeTracker.BL.Mappers;
using Windows.Security.EnterpriseData;

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ActiveUserId), nameof(ActiveUserId))]
[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
public partial class ProjectCreateViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IProjectAmountFacade _projectAmountFacade;
    private readonly IProjectAmountModelMapper _projectAmountModelMapper;

    public ProjectDetailModel? Project { get; set; } = ProjectDetailModel.Empty;
    public Guid ActiveUserId { get; set; }
    public Guid ProjectId { get; set; } = Guid.Empty;

    public ProjectCreateViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IProjectAmountFacade projectAmountFacade,
        IProjectAmountModelMapper projectAmountModelMapper,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _projectAmountFacade = projectAmountFacade;
        _navigationService = navigationService;
        _projectAmountModelMapper = projectAmountModelMapper;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        
        if ( ProjectId  == Guid.Empty ) {
            var newProjectDetailModel = await _projectFacade.SaveAsync(Project with
            {
                Id = Guid.Empty,
                CreatorId = ActiveUserId,
                Users = default!,
                Activities = default!
            });

            ProjectAmountDetailModel projectAmountDetailModelNew = new()
            {
                Id = Guid.NewGuid(),
                ProjectId = newProjectDetailModel.Id,
                UserId = ActiveUserId
            };
            await _projectAmountFacade.SaveAsync(projectAmountDetailModelNew);
            MessengerService.Send(new ProjectEditMessage { ProjectId = newProjectDetailModel.Id });

        } else
        {
            await _projectFacade.SaveAsync(Project!);
            MessengerService.Send(new ProjectEditMessage { ProjectId = Project!.Id });
        }

        _navigationService.SendBackButtonPressed();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (ProjectId != Guid.Empty )
        {
            Project = await _projectFacade.GetAsync(ProjectId);
        }
    }

}
