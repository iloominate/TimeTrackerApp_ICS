using TimeTracker.App.ViewModels.Activity;

namespace TimeTracker.App.Views;

public partial class ActivityEditView
{
	public ActivityEditView(ActivityEditViewModel viewModel)
	    : base (viewModel)
	{
		InitializeComponent();
	}
}