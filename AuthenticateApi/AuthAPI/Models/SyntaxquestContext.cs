using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AuthAPI.Models;

public partial class SyntaxquestContext : DbContext
{
    public SyntaxquestContext()
    {
    }

    public SyntaxquestContext(DbContextOptions<SyntaxquestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<BlacklistedToken> BlacklistedTokens { get; set; }

    public virtual DbSet<LoggedInUser> LoggedInUsers { get; set; }

    public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

    public virtual DbSet<Registry> Registries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TempRole> TempRoles { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserStat> UserStats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            string connectionString = configuration.GetConnectionString("Connection")!;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_hungarian_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.AchievementId).HasName("PRIMARY");

            entity.ToTable("achievements");

            entity.HasIndex(e => e.AchievementName, "achievement_name").IsUnique();

            entity.Property(e => e.AchievementId)
                .HasColumnType("int(11)")
                .HasColumnName("achievement_id");
            entity.Property(e => e.AchievementName)
                .HasMaxLength(254)
                .HasColumnName("achievement_name");
        });

        modelBuilder.Entity<BlacklistedToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity.ToTable("blacklisted_tokens");

            entity.HasIndex(e => e.Token, "token").IsUnique();

            entity.Property(e => e.TokenId)
                .HasMaxLength(254)
                .HasColumnName("token_id");
            entity.Property(e => e.BlacklistedStatusExpires)
                .HasColumnType("datetime")
                .HasColumnName("blacklisted_status_expires");
            entity.Property(e => e.Token)
                .HasColumnType("text")
                .HasColumnName("token");
        });

        modelBuilder.Entity<LoggedInUser>(entity =>
        {
            entity.HasKey(e => e.LoggedIsUsersId).HasName("PRIMARY");

            entity.ToTable("logged_in_users");

            entity.HasIndex(e => e.Userid, "userid");

            entity.Property(e => e.LoggedIsUsersId)
                .HasColumnType("int(11)")
                .HasColumnName("logged_is_users_id");
            entity.Property(e => e.SessionExpires)
                .HasColumnType("datetime")
                .HasColumnName("sessionExpires");
            entity.Property(e => e.Token)
                .HasColumnType("text")
                .HasColumnName("token");
            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasMaxLength(254)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.LoggedInUsers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("logged_in_users_ibfk_1");
        });

        modelBuilder.Entity<RegisteredUser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity.ToTable("registered_users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Roleid, "roleid");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid");
            entity.Property(e => e.ChangePasswordConfirmationKey)
                .HasColumnType("text")
                .HasColumnName("change_password_confirmation_key");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(254)
                .HasColumnName("fullname");
            entity.Property(e => e.Hash)
                .HasColumnType("text")
                .HasColumnName("hash");
            entity.Property(e => e.IsLoggedIn).HasColumnName("is_logged_in");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("datetime")
                .HasColumnName("lastlogin");
            entity.Property(e => e.Regdate)
                .HasColumnType("datetime")
                .HasColumnName("regdate");
            entity.Property(e => e.Roleid)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)")
                .HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.RegisteredUsers)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registered_users_ibfk_2");
        });

        modelBuilder.Entity<Registry>(entity =>
        {
            entity.HasKey(e => e.TempUserid).HasName("PRIMARY");

            entity.ToTable("registry");

            entity.HasIndex(e => e.TempEmail, "temp_email").IsUnique();

            entity.HasIndex(e => e.TempRoleid, "temp_roleid");

            entity.HasIndex(e => e.TempUsername, "temp_username").IsUnique();

            entity.Property(e => e.TempUserid)
                .HasMaxLength(254)
                .HasColumnName("temp_userid");
            entity.Property(e => e.TempConfirmationKey)
                .HasColumnType("text")
                .HasColumnName("temp_confirmation_key");
            entity.Property(e => e.TempEmail)
                .HasMaxLength(64)
                .HasColumnName("temp_email");
            entity.Property(e => e.TempFullname)
                .HasMaxLength(254)
                .HasColumnName("temp_fullname");
            entity.Property(e => e.TempHash)
                .HasColumnType("text")
                .HasColumnName("temp_hash");
            entity.Property(e => e.TempRegdate)
                .HasColumnType("datetime")
                .HasColumnName("temp_regdate");
            entity.Property(e => e.TempRoleid)
                .HasColumnType("int(11)")
                .HasColumnName("temp_roleid");
            entity.Property(e => e.TempUserExpire)
                .HasColumnType("datetime")
                .HasColumnName("temp_user_expire");
            entity.Property(e => e.TempUsername)
                .HasMaxLength(64)
                .HasColumnName("temp_username");

            entity.HasOne(d => d.TempRole).WithMany(p => p.Registries)
                .HasForeignKey(d => d.TempRoleid)
                .HasConstraintName("registry_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "role_name").IsUnique();

            entity.Property(e => e.Roleid)
                .HasColumnType("int(11)")
                .HasColumnName("roleid");
            entity.Property(e => e.RoleName)
                .HasMaxLength(64)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<TempRole>(entity =>
        {
            entity.HasKey(e => e.TempRoleId).HasName("PRIMARY");

            entity.ToTable("temp_roles");

            entity.Property(e => e.TempRoleId)
                .HasColumnType("int(11)")
                .HasColumnName("temp_role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(16)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => e.UserAchievementId).HasName("PRIMARY");

            entity.ToTable("user_achievements");

            entity.HasIndex(e => e.AchievementId, "achievement_id");

            entity.HasIndex(e => e.Userid, "userid");

            entity.Property(e => e.UserAchievementId)
                .HasColumnType("int(11)")
                .HasColumnName("user_achievement_id");
            entity.Property(e => e.AchievementDate)
                .HasColumnType("datetime")
                .HasColumnName("achievement_date");
            entity.Property(e => e.AchievementId)
                .HasColumnType("int(11)")
                .HasColumnName("achievement_id");
            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid");

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.AchievementId)
                .HasConstraintName("user_achievements_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("user_achievements_ibfk_1");
        });

        modelBuilder.Entity<UserStat>(entity =>
        {
            entity.HasKey(e => e.UserStatId).HasName("PRIMARY");

            entity.ToTable("user_stats");

            entity.HasIndex(e => e.Userid, "userid").IsUnique();

            entity.Property(e => e.UserStatId)
                .HasColumnType("int(11)")
                .HasColumnName("user_stat_id");
            entity.Property(e => e.Deaths)
                .HasColumnType("int(11)")
                .HasColumnName("deaths");
            entity.Property(e => e.HighestKillCount)
                .HasColumnType("int(11)")
                .HasColumnName("highestKillCount");
            entity.Property(e => e.HighestLevel)
                .HasColumnType("int(11)")
                .HasColumnName("highestLevel");
            entity.Property(e => e.Kills)
                .HasColumnType("int(11)")
                .HasColumnName("kills");
            entity.Property(e => e.Timesplayed)
                .HasColumnType("int(11)")
                .HasColumnName("timesplayed");
            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithOne(p => p.UserStat)
                .HasForeignKey<UserStat>(d => d.Userid)
                .HasConstraintName("user_stats_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
