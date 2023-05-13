using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Models;
using TimeTracker.App.ViewModels;
using TimeTracker.App.View;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UsersPage), typeof(UserChooseListViewModel)),
        new("//users/edit", typeof(UserEdit), typeof(UserEditViewModel)),

        new("//projects", typeof(ProjectList), typeof(ProjectListViewModel)),
        new("//project/edit", typeof(ProjectEdit), typeof(ProjectEditViewModel)),

        new("//activities", typeof(ActivitiesList), typeof(ActivityListViewModel)),
        new("//activities/edit", typeof(ActivityEdit), typeof(ActivityEditViewModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
