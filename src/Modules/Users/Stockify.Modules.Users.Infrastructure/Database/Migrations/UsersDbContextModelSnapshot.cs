﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stockify.Modules.Users.Infrastructure.Database;

#nullable disable

namespace Stockify.Modules.Users.Infrastructure.Database.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<string>("PermissionsCode")
                        .HasColumnType("character varying(100)")
                        .HasColumnName("permission_code");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("PermissionsCode", "RoleId")
                        .HasName("pk_role_permissions");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_permissions_role_id");

                    b.ToTable("role_permissions", "users");

                    b.HasData(
                        new
                        {
                            PermissionsCode = "administrator:full_access",
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId")
                        .HasName("pk_user_roles");

                    b.HasIndex("UsersId")
                        .HasDatabaseName("ix_user_roles_users_id");

                    b.ToTable("user_roles", "users");
                });

            modelBuilder.Entity("Stockify.Modules.Users.Domain.Roles.Permission", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("code");

                    b.HasKey("Code")
                        .HasName("pk_permissions");

                    b.ToTable("permissions", "users");

                    b.HasData(
                        new
                        {
                            Code = "users:access"
                        },
                        new
                        {
                            Code = "administrator:full_access"
                        });
                });

            modelBuilder.Entity("Stockify.Modules.Users.Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", "users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("Stockify.Modules.Users.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", "users");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("Stockify.Modules.Users.Domain.Roles.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permissions_permissions_permissions_code");

                    b.HasOne("Stockify.Modules.Users.Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permissions_role_role_id");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Stockify.Modules.Users.Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_roles_roles_id");

                    b.HasOne("Stockify.Modules.Users.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_users_users_id");
                });
#pragma warning restore 612, 618
        }
    }
}
