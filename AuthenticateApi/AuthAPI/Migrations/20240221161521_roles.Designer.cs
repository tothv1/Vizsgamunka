﻿// <auto-generated />
using System;
using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthAPI.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20240221161521_roles")]
    partial class roles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("AuthAPI.Models.BlacklistedToken", b =>
                {
                    b.Property<string>("TokenId")
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("token_id");

                    b.Property<DateTime>("BlacklistedStatusExpires")
                        .HasColumnType("datetime")
                        .HasColumnName("blacklisted_status_expires");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("TokenId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Token" }, "token")
                        .IsUnique();

                    b.ToTable("blacklisted_tokens", (string)null);

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_hungarian_ci");
                });

            modelBuilder.Entity("AuthAPI.Models.ConfirmationKey", b =>
                {
                    b.Property<int>("Keyid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("keyid");

                    b.Property<string>("ConfirmationKey1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("confirmation_key");

                    b.Property<int>("ExpirationTime")
                        .HasColumnType("int(11)")
                        .HasColumnName("expiration_time");

                    b.Property<string>("Userid")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("userid");

                    b.HasKey("Keyid")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Userid" }, "userid");

                    b.ToTable("confirmation_keys", (string)null);

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_hungarian_ci");
                });

            modelBuilder.Entity("AuthAPI.Models.LoggedInUser", b =>
                {
                    b.Property<string>("Userid")
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("userid");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("Userid")
                        .HasName("PRIMARY");

                    b.ToTable("logged_in_users", (string)null);
                });

            modelBuilder.Entity("AuthAPI.Models.RegisteredUser", b =>
                {
                    b.Property<string>("Userid")
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("userid")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Userid"), "utf8");

                    b.Property<int?>("ConfirmationKeyid")
                        .HasColumnType("int(11)")
                        .HasColumnName("confirmation_keyid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("email")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Email"), "utf8");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("fullname")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Fullname"), "utf8");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Hash"), "utf8");

                    b.Property<bool>("IsLoggedIn")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_logged_in");

                    b.Property<DateTime>("Regdate")
                        .HasColumnType("datetime")
                        .HasColumnName("regdate");

                    b.Property<int>("Roleid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("roleid")
                        .HasDefaultValueSql("'2'");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("username")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Username"), "utf8");

                    b.HasKey("Userid")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ConfirmationKeyid" }, "confirmation_keyid");

                    b.HasIndex(new[] { "Email" }, "email")
                        .IsUnique();

                    b.HasIndex(new[] { "Roleid" }, "roleid");

                    b.HasIndex(new[] { "Username" }, "username")
                        .IsUnique();

                    b.ToTable("registered_users", (string)null);

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_hungarian_ci");
                });

            modelBuilder.Entity("AuthAPI.Models.Registry", b =>
                {
                    b.Property<string>("TempUserid")
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("temp_userid");

                    b.Property<string>("TempConfirmationKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("temp_confirmation_key");

                    b.Property<string>("TempEmail")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("temp_email")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("TempEmail"), "utf8");

                    b.Property<string>("TempFullname")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)")
                        .HasColumnName("temp_fullname")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("TempFullname"), "utf8");

                    b.Property<string>("TempHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("temp_hash")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("TempHash"), "utf8");

                    b.Property<DateTime>("TempRegdate")
                        .HasColumnType("datetime")
                        .HasColumnName("temp_regdate");

                    b.Property<int?>("TempRoleid")
                        .HasColumnType("int(11)")
                        .HasColumnName("temp_roleid");

                    b.Property<DateTime>("TempUserExpire")
                        .HasColumnType("datetime")
                        .HasColumnName("temp_user_expire");

                    b.Property<string>("TempUsername")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("temp_username")
                        .UseCollation("utf8_hungarian_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("TempUsername"), "utf8");

                    b.HasKey("TempUserid")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "TempEmail" }, "temp_email")
                        .IsUnique();

                    b.HasIndex(new[] { "TempRoleid" }, "temp_roleid");

                    b.HasIndex(new[] { "TempUsername" }, "temp_username")
                        .IsUnique();

                    b.ToTable("registry", (string)null);

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_hungarian_ci");
                });

            modelBuilder.Entity("AuthAPI.Models.Role", b =>
                {
                    b.Property<int>("Roleid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("roleid");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("role_name");

                    b.HasKey("Roleid")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "RoleName" }, "role_name")
                        .IsUnique();

                    b.ToTable("roles", (string)null);

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_hungarian_ci");
                });

            modelBuilder.Entity("AuthAPI.Models.TempRole", b =>
                {
                    b.Property<int>("TempRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("temp_role_id");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("role_name");

                    b.HasKey("TempRoleId")
                        .HasName("PRIMARY");

                    b.ToTable("temp_roles", (string)null);

                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb4_hungarian_ci");
                });

            modelBuilder.Entity("AuthAPI.Models.RegisteredUser", b =>
                {
                    b.HasOne("AuthAPI.Models.ConfirmationKey", "ConfirmationKey")
                        .WithMany("RegisteredUsers")
                        .HasForeignKey("ConfirmationKeyid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("registered_users_ibfk_1");

                    b.HasOne("AuthAPI.Models.Role", "Role")
                        .WithMany("RegisteredUsers")
                        .HasForeignKey("Roleid")
                        .IsRequired()
                        .HasConstraintName("registered_users_ibfk_2");

                    b.Navigation("ConfirmationKey");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AuthAPI.Models.Registry", b =>
                {
                    b.HasOne("AuthAPI.Models.TempRole", "TempRole")
                        .WithMany("Registries")
                        .HasForeignKey("TempRoleid")
                        .HasConstraintName("registry_ibfk_1");

                    b.Navigation("TempRole");
                });

            modelBuilder.Entity("AuthAPI.Models.ConfirmationKey", b =>
                {
                    b.Navigation("RegisteredUsers");
                });

            modelBuilder.Entity("AuthAPI.Models.Role", b =>
                {
                    b.Navigation("RegisteredUsers");
                });

            modelBuilder.Entity("AuthAPI.Models.TempRole", b =>
                {
                    b.Navigation("Registries");
                });
#pragma warning restore 612, 618
        }
    }
}
