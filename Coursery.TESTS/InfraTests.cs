using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Coursery.Infrastucture.Data; // Adjust as necessary for the actual namespace

namespace Coursery.TESTS;

public class InfraTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<CourseryDbContext> _dbContextOptions;

    public InfraTests()
    {
        // Create and open an in-memory SQLite connection
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _dbContextOptions = new DbContextOptionsBuilder<CourseryDbContext>()
            .UseSqlite(_connection)
            .Options;
    }

    [Fact]
    public void Can_Create_Database_Context()
    {
        Assert.Equal("", "1");
    }
    
    [Fact]
    public void Can_Create_And_Save_Course()
    {
        Assert.Equal("", "1");
    }
    
    [Fact]
    public void Can_Retrieve_Course_By_Id()
    {
        Assert.Equal("", "1");
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}