using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models.DetailModels;

namespace TimeTracker.App.ViewModels.User;

public partial class UserEditViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public UserDetailModel user { get; set; } = UserDetailModel.Empty; 
    UserEditViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _userFacade.SaveAsync(user with { Projects = default! });

        MessengerService.Send(new UserCreateMessage { UserId = user.Id });

        _navigationService.SendBackButtonPressed();
    }
}
