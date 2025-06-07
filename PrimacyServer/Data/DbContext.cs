using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<Site> Sites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>().ToTable("entity");
        modelBuilder.Entity<Division>().ToTable("division");
        modelBuilder.Entity<Site>().ToTable("site");

        modelBuilder.Entity<Division>()
            .HasOne(d => d.Entity)
            .WithMany(e => e.Divisions)
            .HasForeignKey(d => d.entityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Site>()
            .HasOne(d => d.Division)
            .WithMany(e => e.Sites)
            .HasForeignKey(d => d.divisionId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}