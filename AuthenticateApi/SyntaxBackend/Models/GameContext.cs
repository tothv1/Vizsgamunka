using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SyntaxBackEnd.Models;

public partial class GameContext : DbContext
{
    public GameContext()
    {
    }

    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userachievement> Userachievements { get; set; }

    public virtual DbSet<Userstat> Userstats { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MySql")!;

            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("10.4.32-mariadb"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("achievements");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AchievementName)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("achievement_name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.PermissionId, "User_fk0");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("datetime")
                .HasColumnName("lastlogin");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.Regdate)
                .HasColumnType("datetime")
                .HasColumnName("regdate");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("username");

            entity.HasOne(d => d.Permission).WithMany(p => p.Users)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_fk0");
        });

        modelBuilder.Entity<Userachievement>(entity =>
        {
            entity.HasKey(e => e.MyRowId).HasName("PRIMARY");

            entity.ToTable("userachievements");

            entity.HasIndex(e => e.AchievementId, "UserAchievements_fk0");

            entity.HasIndex(e => e.UserId, "UserAchievements_fk1");

            entity.Property(e => e.MyRowId).HasColumnName("my_row_id");
            entity.Property(e => e.AchievementDate)
                .HasColumnType("datetime")
                .HasColumnName("achievement_date");
            entity.Property(e => e.AchievementId).HasColumnName("achievement_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Achievement).WithMany(p => p.Userachievements)
                .HasForeignKey(d => d.AchievementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserAchievements_fk0");

            entity.HasOne(d => d.User).WithMany(p => p.Userachievements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserAchievements_fk1");
        });

        modelBuilder.Entity<Userstat>(entity =>
        {
            entity.HasKey(e => e.MyRowId).HasName("PRIMARY");

            entity.ToTable("userstats");

            entity.HasIndex(e => e.UserId, "UserStats_fk0");

            entity.Property(e => e.MyRowId).HasColumnName("my_row_id");
            entity.Property(e => e.Deaths).HasColumnName("deaths");
            entity.Property(e => e.Kills).HasColumnName("kills");
            entity.Property(e => e.Timesplayed).HasColumnName("timesplayed");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Userstats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserStats_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
