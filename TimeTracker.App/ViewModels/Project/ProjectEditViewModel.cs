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

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ProjectActivityEditMessage>, IRecipient<ProjectActivityAddMessage>, IRecipient<ProjectActivityDeleteMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;
    public Guid projectId { get; set; }

    // Create Activity type??

    public ProjectEditViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

//    [RelayCommand]
//    private async Task GoToProjectActivityEditAsync()
 //   {
 //       await _navigationService.GoToAsync("/ingredients",
 //           new Dictionary<string, object?> { [nameof(ProjectActivitiesEditViewModel.Project)] = Project });
 //   }
    
    public async Task GoToActivityEditAsync (ProjectDetailModel projectDetail)
    {
        await _navigationService.GoToAsync("/activities", 
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Project)] = projectDetail });
    }
    public async void Receive(ProjectActivityEditMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(ProjectActivityAddMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(ProjectActivityDeleteMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        Project = await _projectFacade.GetAsync(Project.Id)
                 ?? ProjectDetailModel.Empty;
    }
}
