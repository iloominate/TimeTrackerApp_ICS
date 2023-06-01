using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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

[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class UserEditViewModel : ViewModelBase, IRecipient<GetUserMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public UserDetailModel User { get; set; } = UserDetailModel.Empty; 
    public Guid UserId { get; set; } = Guid.Empty;
    public UserEditViewModel(
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
        await _userFacade.SaveAsync(User with { Projects = default!, ProjectsCreared = default!});

        MessengerService.Send(new UserEditMessage{ UserId = User.Id });

        _navigationService.SendBackButtonPressed();
    }


    public async void Receive(GetUserMessage message)
    {
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        User = await _userFacade.GetAsync(UserId);
        if (User == null)
        {
            User = new()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Surname = "",
                PhotoUrl = null
            };
        }
    }
}
