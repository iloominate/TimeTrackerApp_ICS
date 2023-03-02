using System;

namespace TimeTracker.DAL.Entities;

public interface IEntity
{
    Guid Id { get; set; }
}