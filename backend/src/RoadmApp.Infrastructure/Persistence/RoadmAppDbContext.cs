using Microsoft.EntityFrameworkCore;
using RoadmApp.Application.Abstractions;
using RoadmApp.Domain.Planning;
using RoadmApp.Domain.Users;

namespace RoadmApp.Infrastructure.Persistence;

public sealed class RoadmAppDbContext(DbContextOptions<RoadmAppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<RoadmapTask> Tasks => Set<RoadmapTask>();
    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Note> Notes => Set<Note>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Name).HasMaxLength(120).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(180).IsRequired();
            builder.Property(user => user.PasswordHash).HasMaxLength(512).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasMany(user => user.Tasks)
                .WithOne()
                .HasForeignKey(task => task.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Habits)
                .WithOne()
                .HasForeignKey(habit => habit.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Goals)
                .WithOne()
                .HasForeignKey(goal => goal.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Notes)
                .WithOne()
                .HasForeignKey(note => note.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RoadmapTask>(builder =>
        {
            builder.ToTable("Tasks");
            builder.HasKey(task => task.Id);
            builder.Property(task => task.Title).HasMaxLength(160).IsRequired();
            builder.Property(task => task.Description).HasMaxLength(1000);
            builder.Property(task => task.Status).HasConversion<string>().HasMaxLength(30);
            builder.HasIndex(task => new { task.UserId, task.Status });
        });

        modelBuilder.Entity<Habit>(builder =>
        {
            builder.ToTable("Habits");
            builder.HasKey(habit => habit.Id);
            builder.Property(habit => habit.Title).HasMaxLength(160).IsRequired();
            builder.Property(habit => habit.Frequency).HasMaxLength(40).IsRequired();
            builder.Property(habit => habit.Status).HasConversion<string>().HasMaxLength(30);
            builder.HasIndex(habit => new { habit.UserId, habit.Status });
        });

        modelBuilder.Entity<Goal>(builder =>
        {
            builder.ToTable("Goals");
            builder.HasKey(goal => goal.Id);
            builder.Property(goal => goal.Title).HasMaxLength(160).IsRequired();
            builder.Property(goal => goal.Description).HasMaxLength(1000);
            builder.Property(goal => goal.Status).HasConversion<string>().HasMaxLength(30);
            builder.HasIndex(goal => new { goal.UserId, goal.Status });
        });

        modelBuilder.Entity<Note>(builder =>
        {
            builder.ToTable("Notes");
            builder.HasKey(note => note.Id);
            builder.Property(note => note.Title).HasMaxLength(160).IsRequired();
            builder.Property(note => note.Content).HasMaxLength(4000).IsRequired();
            builder.HasIndex(note => note.UserId);
        });
    }
}
