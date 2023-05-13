using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Views;

public partial class UserEditView
{
    public UserEditView(UserEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}