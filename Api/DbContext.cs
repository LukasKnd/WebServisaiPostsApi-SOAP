using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options) { }

    public DbSet<PostEntity> Posts => Set<PostEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostEntity>().HasData(
            new PostEntity { Id = 1, Title = "Title1", Content = "Content1", Created = DateTime.UtcNow, Updated = DateTime.UtcNow, TagsJson = "[]"},
            new PostEntity { Id = 2, Title = "Title2", Content = "Content2", Created = DateTime.UtcNow, Updated = DateTime.UtcNow, TagsJson = "[]"},
            new PostEntity { Id = 3, Title = "Title3", Content = "Content3", Created = DateTime.UtcNow, Updated = DateTime.UtcNow, TagsJson = "[]"});
        base.OnModelCreating(modelBuilder);
    }
    
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess, 
        CancellationToken cancellationToken = default(CancellationToken)
    )
    {
        OnBeforeSaving();
        return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
            cancellationToken));
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.Updated = utcNow;

                        entry.Property("Created").IsModified = false;
                        break;

                    case EntityState.Added:
                        trackable.Created = utcNow;
                        trackable.Updated = utcNow;
                        break;
                }
            }
        }
    }
}