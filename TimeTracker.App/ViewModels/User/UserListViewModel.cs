using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;

namespace TimeTracker.App.ViewModels.User;



public partial class UserListViewModel : ViewModelBase, IRecipient<UserCreateMessage>, IRecipient<UserEditMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;

    public Guid TEST_ID { get; set; } = new Guid();

    public UserListViewModel(
        IUserFacade userFacade, 
        INavigationService navigationService, 
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()    
    {
        await _navigationService.GoToAsync("/edit");
        MessengerService.Send(new GetUserMessage()); // ensures that User model will be loaded
    }

    [RelayCommand]
    private async Task GoToProjectListAsync(Guid id)
        => await _navigationService.GoToAsync<ProjectListViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectListViewModel.ActiveUserId)] = id });

    [RelayCommand]
    private async Task GoToUserEditAsync(Guid id)
    {
        MessengerService.Send(new GetUserMessage()); // ensures that User model will be loaded
        await _navigationService.GoToAsync<UserEditViewModel>(
            new Dictionary<string, object?> { [nameof(UserEditViewModel.UserId)] = id });

    } 

    public async void Receive(UserCreateMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }
}
