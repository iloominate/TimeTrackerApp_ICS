using TimeTracker.App.ViewModels.Project;

namespace TimeTracker.App.Views;

public partial class ProjectListView
{
	public ProjectListView( ProjectListViewModel viewModel)
        : base (viewModel)
	{
		InitializeComponent();
	}
}