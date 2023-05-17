﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Messages;

public record ActivityEditMessage
{
    public required Guid ProjectId { get; set;}
    public required Guid ActivityId { get; set;}
}
