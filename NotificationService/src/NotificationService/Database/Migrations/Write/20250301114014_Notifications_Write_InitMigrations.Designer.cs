﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationService.Database.Write;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotificationService.Database.Migrations.Write
{
    [DbContext(typeof(NotificationWriteDbContext))]
    [Migration("20250301114014_Notifications_Write_InitMigrations")]
    partial class Notifications_Write_InitMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Notifications")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NotificationService.Domain.UserNotificationSettings", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<bool?>("IsEmailSend")
                        .HasColumnType("boolean")
                        .HasColumnName("is_email_send");

                    b.Property<bool?>("IsTelegramSend")
                        .HasColumnType("boolean")
                        .HasColumnName("is_telegram_send");

                    b.Property<bool?>("IsWebSend")
                        .HasColumnType("boolean")
                        .HasColumnName("is_web_send");

                    b.HasKey("UserId")
                        .HasName("pk_user_notification_settings");

                    b.ToTable("UserNotificationSettings", "Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
