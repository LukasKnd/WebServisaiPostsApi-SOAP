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
            new PostEntity { Id = 1, Title = "Title1", Content = "Content1"},
            new PostEntity { Id = 2, Title = "Title2", Content = "Content2"},
            new PostEntity { Id = 3, Title = "Title3", Content = "Content3"});
        base.OnModelCreating(modelBuilder);
    }
}