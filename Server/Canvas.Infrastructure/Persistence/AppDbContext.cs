using Canvas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canvas.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<VerificationToken> VerificationTokens => Set<VerificationToken>();
    public DbSet<Board> Boards => Set<Board>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Session>()
            .HasIndex(s => s.Token)
            .IsUnique();

        modelBuilder.Entity<Session>()
            .HasIndex(s => s.UserId)
            .IsUnique(false);

        modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VerificationToken>()
            .HasIndex(t => t.Token)
            .IsUnique();

        modelBuilder.Entity<VerificationToken>()
            .HasIndex(t => t.UserId)
            .IsUnique(false);

        modelBuilder.Entity<VerificationToken>()
            .HasOne(t => t.User)
            .WithMany(u => u.VerificationTokens)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Board>()
            .HasIndex(b => b.Title)
            .IsUnique();

        modelBuilder.Entity<Board>()
            .HasOne(b => b.User)
            .WithMany(u => u.OwnedBoards)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}