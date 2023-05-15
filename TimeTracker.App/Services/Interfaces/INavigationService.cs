using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Models;
using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Services.Interfaces;

public interface INavigationService
{
    IEnumerable<RouteModel> Routes { get; }

    Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel;

    //Task GoToAsync<TViewModel>(Guid projectId, Guid userId, IDictionary<string, object?> parameters)
    //    where TViewModel : IViewModel;

    Task GoToAsync(string route);

    bool SendBackButtonPressed(); 

    Task GoToAsync(string route, IDictionary<string, object?> parameters);

    Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel;
}
