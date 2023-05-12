namespace TimeTracker.App.View;

public partial class ProjectList : ContentPage
{
	public ProjectList()
	{
		InitializeComponent();
	}

    private async void OnViewClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ActivitiesList());
    }
}