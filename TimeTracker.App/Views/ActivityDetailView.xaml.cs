using TimeTracker.App.ViewModels.Activity;

namespace TimeTracker.App.Views;

public partial class ActivityDetailView
{
    public ActivityDetailView(ActivityDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}