using TimeTracker.App.ViewModels;
using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.View;

public partial class UsersPage : ContentPageBase
{

    public UsersPage(UserChooseListViewModel viewModel) 
        : base(viewModel)
    {
        InitializeComponent();
    }

    /*    private async void OnLogInClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjectList());
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserEdit());
        }*/
}