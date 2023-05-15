using TimeTracker.App.ViewModels.Project;

namespace TimeTracker.App.Views;

public partial class ProjectDetailView
{
    public ProjectDetailView(ProjectDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}