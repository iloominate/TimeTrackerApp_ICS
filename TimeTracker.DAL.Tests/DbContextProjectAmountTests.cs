using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Common.Enums;
using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.Common.Tests;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.DAL.Tests;

public class DbContextProjectAmountTests : DbContextTestsBase
{
    public DbContextProjectAmountTests(ITestOutputHelper output) : base(output)
    {
    }

}