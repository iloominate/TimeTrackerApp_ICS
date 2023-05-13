using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Messages;

public record UserEditMessage
{
    public required Guid UserId { get; init; }
}
