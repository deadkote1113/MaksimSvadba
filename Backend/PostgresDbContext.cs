using Microsoft.EntityFrameworkCore;
using Svadba.DBModel;

namespace Svadba;

public class PostgresDbContext : DbContext
{
    public DbSet<Forma> Formas { get; set; }

    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
