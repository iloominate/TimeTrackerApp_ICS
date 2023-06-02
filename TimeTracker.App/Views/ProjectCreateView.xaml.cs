using TimeTracker.App.ViewModels.Project;

namespace TimeTracker.App.Views;

public partial class ProjectCreateView
{
    public ProjectCreateView(ProjectCreateViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}