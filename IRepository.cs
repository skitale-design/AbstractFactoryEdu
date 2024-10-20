using Microsoft.EntityFrameworkCore;

namespace AbstractFactoryEdu
{

    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { 
            Database.EnsureCreated();
        }
    }

    public interface IDbContextFactory
    {
        CustomDbContext CreateDbContext(string connectionString);
    }

    public class DbContextFactory : IDbContextFactory
    {
        public CustomDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new CustomDbContext(optionsBuilder.Options);
        }
    }

}
