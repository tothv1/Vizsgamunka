using System;
using System.Collections.Generic;
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

    public virtual DbSet<ConfirmationKey> ConfirmationKeys { get; set; }

    public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

    public virtual DbSet<Registry> Registries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

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
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ConfirmationKey>(entity =>
        {
            entity.HasKey(e => e.Keyid).HasName("PRIMARY");

            entity.ToTable("confirmation_keys");

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

        modelBuilder.Entity<RegisteredUser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity.ToTable("registered_users");

            entity.HasIndex(e => e.ConfirmationKeyid, "confirmation_keyid");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Roleid, "roleid").IsUnique();

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
                .IsRequired()
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
        });

        modelBuilder.Entity<Registry>(entity =>
        {
            entity.HasKey(e => e.TempUserid).HasName("PRIMARY");

            entity.ToTable("registry");

            entity.HasIndex(e => e.TempEmail, "temp_email").IsUnique();

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
            entity.Property(e => e.TempUsername)
                .HasMaxLength(64)
                .HasColumnName("temp_username")
                .UseCollation("utf8_hungarian_ci")
                .HasCharSet("utf8");
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

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.Roleid, "roleid");

            entity.Property(e => e.UserRoleId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("user_role_id");
            entity.Property(e => e.Roleid)
                .HasColumnType("int(11)")
                .HasColumnName("roleid");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("user_roles_ibfk_1");

            entity.HasOne(d => d.UserRoleNavigation).WithOne(p => p.UserRole)
                .HasPrincipalKey<RegisteredUser>(p => p.Roleid)
                .HasForeignKey<UserRole>(d => d.UserRoleId)
                .HasConstraintName("user_roles_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
