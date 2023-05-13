using TimeTracker.App.ViewModels;
using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Views;

public partial class UserListView
{

    public UserListView(UserListViewModel viewModel) 
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
            await Navigation.PushAsync(new UserEditView());
        }*/
}