using Microsoft.EntityFrameworkCore;

public class Context: DbContext
{
    public DbSet<WordPopularity.Entities.WorldPopularity> WordPopularity { get; set; }
    public Context(DbContextOptions<Context> options): base(options) { }
}


