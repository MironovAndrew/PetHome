﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationService.Infrastructure.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotificationService.Infrastructure.Database.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    partial class NotificationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<long?>("TelegramChatId")
                        .HasColumnType("bigint")
                        .HasColumnName("telegram_chat_id");

                    b.HasKey("UserId")
                        .HasName("user_id");

                    b.ToTable("UserNotificationSettings", "Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
