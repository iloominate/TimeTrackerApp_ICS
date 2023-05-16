using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Options;


public record DALOptions
{
    public SqliteOptions? Sqlite { get; init; }
}

public record SqliteOptions
{
    public bool Enabled { get; init; }
    public string DatabaseName { get; init; } = null!;
    public bool RecreateDatabaseEachTime { get; init; } = false;
    public bool SeedDemoData { get; init; } = false;
}

