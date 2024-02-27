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

    public virtual DbSet<AchievementsConnect> AchievementsConnects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userachievement> Userachievements { get; set; }

    public virtual DbSet<Userstat> Userstats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user id=root;database=game", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("achievements");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AchievementName)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("achievement_name");
        });

        modelBuilder.Entity<AchievementsConnect>(entity =>
        {
            entity.HasKey(e => e.AchiId).HasName("PRIMARY");

            entity.ToTable("achievements_connect");

            entity.HasIndex(e => e.Userid, "userid");

            entity.Property(e => e.AchiId)
                .HasColumnType("int(11)")
                .HasColumnName("achi_id");
            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.AchievementsConnects)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("achievements_connect_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Roleid, "User_fk0");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.UserStatsId, "user_stats_id");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(254)
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("datetime")
                .HasColumnName("lastlogin");
            entity.Property(e => e.Regdate)
                .HasColumnType("datetime")
                .HasColumnName("regdate");
            entity.Property(e => e.Roleid)
                .HasColumnType("int(11)")
                .HasColumnName("roleid");
            entity.Property(e => e.UserStatsId)
                .HasColumnType("int(11)")
                .HasColumnName("user_stats_id");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_fk0");

            entity.HasOne(d => d.UserStats).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStatsId)
                .HasConstraintName("user_ibfk_1");
        });

        modelBuilder.Entity<Userachievement>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("userachievements");

            entity.HasIndex(e => e.AchievementId, "UserAchievements_fk0");

            entity.HasIndex(e => e.AchiConnectId, "achi_connect_id");

            entity.Property(e => e.UserId)
                .HasMaxLength(254)
                .HasColumnName("user_id");
            entity.Property(e => e.AchiConnectId)
                .HasColumnType("int(11)")
                .HasColumnName("achi_connect_id");
            entity.Property(e => e.AchievementDate)
                .HasColumnType("datetime")
                .HasColumnName("achievement_date");
            entity.Property(e => e.AchievementId)
                .HasColumnType("int(11)")
                .HasColumnName("achievement_id");

            entity.HasOne(d => d.AchiConnect).WithMany(p => p.Userachievements)
                .HasForeignKey(d => d.AchiConnectId)
                .HasConstraintName("userachievements_ibfk_1");

            entity.HasOne(d => d.Achievement).WithMany(p => p.Userachievements)
                .HasForeignKey(d => d.AchievementId)
                .HasConstraintName("userachievements_ibfk_2");
        });

        modelBuilder.Entity<Userstat>(entity =>
        {
            entity.HasKey(e => e.UserStatId).HasName("PRIMARY");

            entity.ToTable("userstats");

            entity.Property(e => e.UserStatId)
                .HasColumnType("int(11)")
                .HasColumnName("user_stat_id");
            entity.Property(e => e.Deaths)
                .HasColumnType("int(11)")
                .HasColumnName("deaths");
            entity.Property(e => e.Kills)
                .HasColumnType("int(11)")
                .HasColumnName("kills");
            entity.Property(e => e.Timesplayed)
                .HasColumnType("int(11)")
                .HasColumnName("timesplayed");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
