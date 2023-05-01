using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.ViewModels;

public interface IViewModel
{
    Task OnAppearingAsync();
}
