using Microsoft.EntityFrameworkCore;

namespace AbstractFactoryEdu
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}