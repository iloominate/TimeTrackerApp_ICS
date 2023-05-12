using TimeTracker.App.View;

namespace TimeTracker.App;

public partial class UsersPage : ContentPage
{

    public UsersPage()
    {
        InitializeComponent();
    }

    private async void OnLogInClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectList());
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserEdit());
    }
}