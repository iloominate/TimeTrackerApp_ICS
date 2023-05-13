namespace TimeTracker.App.View;

public partial class ActivitiesList : ContentPage
{
	public ActivitiesList()
	{
		InitializeComponent();
	}

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ActivityEdit());
    }
}