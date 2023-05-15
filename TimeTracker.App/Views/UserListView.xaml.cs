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
}