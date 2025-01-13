﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Accounts.Infrastructure.Database;

#nullable disable

namespace PetHome.Accounts.Infrastructure.Migrations
{
    [DbContext(typeof(AuthorizationDbContext))]
    [Migration("20250112115858_Accounts_Write_InitMigrations")]
    partial class Accounts_Write_InitMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Account")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_claim");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_claim_role_id");

                    b.ToTable("role_claim", "Account");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_claim");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_claim_user_id");

                    b.ToTable("user_claim", "Account");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_user_login");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_login_user_id");

                    b.ToTable("user_login", "Account");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_role_id");

                    b.ToTable("user_role", "Account");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_user_token");

                    b.ToTable("user_token", "Account");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<Guid>("PermissionsId")
                        .HasColumnType("uuid")
                        .HasColumnName("permissions_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("PermissionsId", "RoleId")
                        .HasName("pk_permission_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_permission_role_role_id");

                    b.ToTable("permission_role", "Account");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Accounts.AdminAccount", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.HasKey("UserId")
                        .HasName("pk_admin_accounts");

                    b.ToTable("admin_accounts", "Account");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Accounts.ParticipantAccount", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

                    b.Property<string>("FavoritePets")
                        .HasColumnType("text")
                        .HasColumnName("favorite_pets");

                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.HasKey("UserId")
                        .HasName("pk_participant_accounts");

                    b.ToTable("participant_accounts", "Account");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Accounts.VolunteerAccount", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Certificates")
                        .HasColumnType("text")
                        .HasColumnName("certificates");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Pets")
                        .HasColumnType("text")
                        .HasColumnName("pets");

                    b.Property<string>("Requisites")
                        .HasColumnType("text")
                        .HasColumnName("requisites");

                    b.Property<DateTime>("StartVolunteeringDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_volunteering_date");

                    b.HasKey("UserId")
                        .HasName("ak_volunteer_accounts_user_id");

                    b.ToTable("volunteer_accounts", "Account");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Aggregates.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("roles", "Account");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Aggregates.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid")
                        .HasColumnName("permission_id");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("pk_role_permission");

                    b.ToTable("role_permission", "Account");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Aggregates.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<Guid?>("AdminUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("admin_user_id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deletion_date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("Medias")
                        .HasColumnType("text")
                        .HasColumnName("medias");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<Guid?>("ParticipantUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("participant_user_id");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("PhoneNumbers")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id1");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<string>("SocialNetworks")
                        .HasColumnType("text")
                        .HasColumnName("social_networks");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.Property<Guid?>("VolunteerUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_user_id");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("AdminUserId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_admin_user_id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("ParticipantUserId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_participant_user_id");

                    b.HasIndex("RoleId1")
                        .HasDatabaseName("ix_users_role_id1");

                    b.HasIndex("VolunteerUserId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_volunteer_user_id");

                    b.ToTable("users", "Account", t =>
                        {
                            t.Property("PhoneNumber")
                                .HasColumnName("phone_number1");
                        });
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Tokens.RefreshToken.RefreshSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiredIn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expired_in");

                    b.Property<Guid>("JTI")
                        .HasColumnType("uuid")
                        .HasColumnName("jti");

                    b.Property<Guid>("RefreshToken")
                        .HasColumnType("uuid")
                        .HasColumnName("refresh_token");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_refresh_sessions");

                    b.ToTable("refresh_sessions", "Account");
                });

            modelBuilder.Entity("PetHome.Core.ValueObjects.RolePermission.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.HasKey("Id")
                        .HasName("pk_permissions");

                    b.ToTable("permissions", "Account");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("PetHome.Accounts.Domain.Aggregates.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_claim_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("PetHome.Accounts.Domain.Aggregates.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_claim_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("PetHome.Accounts.Domain.Aggregates.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_login_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("PetHome.Accounts.Domain.Aggregates.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_roles_role_id");

                    b.HasOne("PetHome.Accounts.Domain.Aggregates.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("PetHome.Accounts.Domain.Aggregates.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_token_users_user_id");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("PetHome.Core.ValueObjects.RolePermission.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permission_role_permissions_permissions_id");

                    b.HasOne("PetHome.Accounts.Domain.Aggregates.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permission_role_roles_role_id");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Aggregates.User", b =>
                {
                    b.HasOne("PetHome.Accounts.Domain.Accounts.AdminAccount", "Admin")
                        .WithOne("User")
                        .HasForeignKey("PetHome.Accounts.Domain.Aggregates.User", "AdminUserId")
                        .HasConstraintName("fk_users_admin_accounts_admin_user_id");

                    b.HasOne("PetHome.Accounts.Domain.Accounts.ParticipantAccount", "Participant")
                        .WithOne("User")
                        .HasForeignKey("PetHome.Accounts.Domain.Aggregates.User", "ParticipantUserId")
                        .HasConstraintName("fk_users_participant_accounts_participant_user_id");

                    b.HasOne("PetHome.Accounts.Domain.Aggregates.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId1")
                        .HasConstraintName("fk_users_roles_role_id1");

                    b.HasOne("PetHome.Accounts.Domain.Accounts.VolunteerAccount", "Volunteer")
                        .WithOne("User")
                        .HasForeignKey("PetHome.Accounts.Domain.Aggregates.User", "VolunteerUserId")
                        .HasConstraintName("fk_users_volunteer_accounts_volunteer_user_id");

                    b.Navigation("Admin");

                    b.Navigation("Participant");

                    b.Navigation("Role");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Accounts.AdminAccount", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Accounts.ParticipantAccount", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Accounts.Domain.Accounts.VolunteerAccount", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}