using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Messages;

namespace TimeTracker.App.ViewModels.Project;

public partial class ProjectActivitiesEditViewModel : ViewModelBase, IRecipient<ProjectUserAddMessage>, IRecipient<ProjectActivitiesEditMessage>,
    IRecipient<ProjectActivityAddMessage>, IRecipient<ProjectActivityDeleteMessage>, IRecipient<ProjectUserEditMessage>, IRecipient<ProjectUserDeleteMessage>

{

}
