﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Models;
using TimeTracker.App.ViewModels;
using TimeTracker.App.Views;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.App.ViewModels.User;
using Microsoft.UI.Xaml.Media;

namespace TimeTracker.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UserListView), typeof(UserListViewModel)),
        new("//users/edit", typeof(UserEditView), typeof(UserEditViewModel)),

        new("//users/projects", typeof(ProjectListView), typeof(ProjectListViewModel)),

        new("//users/projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),
        new("//users/projects/editProject", typeof(ProjectEditView), typeof(ProjectEditViewModel)),
        new("//users/projects/createProject", typeof(ProjectCreateView), typeof(ProjectCreateViewModel)),

        new("//users/projects/editProject/activity", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        new("//users/projects/editProject/createActivity", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        new("//users/projects/editProject/activity/editActivity", typeof(ActivityEditView), typeof(ActivityEditViewModel))
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
