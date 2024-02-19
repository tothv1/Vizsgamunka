using System;
using System.Collections.Generic;
using AuthAPI.Migrations;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AuthAPI.Models;

public partial class AuthContext : DbContext
{
    public AuthContext()
    {
    }

    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlacklistedToken> BlacklistedTokens { get; set; }

    public virtual DbSet<ConfirmationKey> ConfirmationKeys { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

    public virtual DbSet<Registry> Registries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TempRole> TempRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user id=root;database=auth", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<BlacklistedToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity
                .ToTable("blacklisted_tokens")
                .UseCollation("utf8mb4_hungarian_ci");

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

        modelBuilder.Entity<ConfirmationKey>(entity =>
        {
            entity.HasKey(e => e.Keyid).HasName("PRIMARY");

            entity
                .ToTable("confirmation_keys")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Userid, "userid");

            entity.Property(e => e.Keyid)
                .HasColumnType("int(11)")
                .HasColumnName("keyid");
            entity.Property(e => e.ConfirmationKey1)
                .HasColumnType("text")
                .HasColumnName("confirmation_key");
            entity.Property(e => e.ExpirationTime)
                .HasColumnType("int(11)")
                .HasColumnName("expiration_time");
            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<RegisteredUser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity
                .ToTable("registered_users")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.ConfirmationKeyid, "confirmation_keyid");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Roleid, "roleid");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Userid)
                .HasMaxLength(254)
                .HasColumnName("userid")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ConfirmationKeyid)
                .HasColumnType("int(11)")
                .HasColumnName("confirmation_keyid");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Fullname)
                .HasMaxLength(254)
                .HasColumnName("fullname")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Hash)
                .HasColumnType("text")
                .HasColumnName("hash")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Regdate)
                .HasColumnType("datetime")
                .HasColumnName("regdate");
            entity.Property(e => e.Roleid)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)")
                .HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .HasColumnName("username")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");

            entity.HasOne(d => d.ConfirmationKey).WithMany(p => p.RegisteredUsers)
                .HasForeignKey(d => d.ConfirmationKeyid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("registered_users_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.RegisteredUsers)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registered_users_ibfk_2");
        });

        modelBuilder.Entity<Registry>(entity =>
        {
            entity.HasKey(e => e.TempUserid).HasName("PRIMARY");

            entity
                .ToTable("registry")
                .UseCollation("utf8mb4_hungarian_ci");

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
                .HasColumnName("temp_email")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.TempFullname)
                .HasMaxLength(254)
                .HasColumnName("temp_fullname")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.TempHash)
                .HasColumnType("text")
                .HasColumnName("temp_hash")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
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
                .HasColumnName("temp_username")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");

            entity.HasOne(d => d.TempRole).WithMany(p => p.Registries)
                .HasForeignKey(d => d.TempRoleid)
                .HasConstraintName("registry_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .UseCollation("utf8mb4_hungarian_ci");

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

            entity
                .ToTable("temp_roles")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.TempRoleId)
                .HasColumnType("int(11)")
                .HasColumnName("temp_role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(16)
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
