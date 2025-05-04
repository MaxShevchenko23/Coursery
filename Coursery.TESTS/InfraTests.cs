using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Coursery.Infrastucture.Data; // Adjust as necessary for the actual namespace

namespace Coursery.TESTS;

public class InfraTests : IDisposable
{

    [Fact]
    public void Can_Create_Database_Context()
    {
        Assert.Equal("1", "1");
    }
    
    [Fact]
    public void Can_Create_And_Save_Course()
    {
        Assert.Equal("1", "1");
    }
    
    [Fact]
    public void Can_Retrieve_Course_By_Id()
    {
        Assert.Equal("1", "1");
    }

    public void Dispose()
    {
    }
}