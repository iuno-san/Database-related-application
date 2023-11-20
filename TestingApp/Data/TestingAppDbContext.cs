using Microsoft.EntityFrameworkCore;
using TestingApp.Models;

namespace TestingApp.Data
{
    public class TestingAppDbContext : DbContext
    {
        public TestingAppDbContext(DbContextOptions<TestingAppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
