﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Discussions.Infrastructure.Database.Write;

#nullable disable

namespace PetHome.Discussions.Infrastructure.Migrations.Write
{
    [DbContext(typeof(DiscussionDbContext))]
    [Migration("20250226161409_Discussions_Write_InitMigrations")]
    partial class Discussions_Write_InitMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Discussions")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHome.Discussions.Domain.Discussion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("RelationId")
                        .HasColumnType("uuid")
                        .HasColumnName("relation_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_discussions");

                    b.HasIndex("RelationId")
                        .HasDatabaseName("ix_discussions_relation_id");

                    b.ToTable("discussions", "Discussions");
                });

            modelBuilder.Entity("PetHome.Discussions.Domain.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_id");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("boolean")
                        .HasColumnName("is_edited");

                    b.Property<string>("Text")
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_messages");

                    b.HasIndex("DiscussionId")
                        .HasDatabaseName("ix_messages_discussion_id");

                    b.ToTable("messages", "Discussions");
                });

            modelBuilder.Entity("PetHome.Discussions.Domain.Relation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_relations");

                    b.ToTable("relations", "Discussions");
                });

            modelBuilder.Entity("PetHome.Discussions.Domain.Discussion", b =>
                {
                    b.HasOne("PetHome.Discussions.Domain.Relation", "Relation")
                        .WithMany("Discussions")
                        .HasForeignKey("RelationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_discussions_relations_relation_id");

                    b.OwnsMany("PetHome.Core.ValueObjects.User.UserId", "UserIds", b1 =>
                        {
                            b1.Property<Guid>("DiscussionId")
                                .HasColumnType("uuid")
                                .HasColumnName("discussion_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("user_id");

                            b1.HasKey("DiscussionId", "Id")
                                .HasName("pk_user_id");

                            b1.ToTable("user_id", "Discussions");

                            b1.WithOwner()
                                .HasForeignKey("DiscussionId")
                                .HasConstraintName("fk_user_id_discussions_discussion_id");
                        });

                    b.Navigation("Relation");

                    b.Navigation("UserIds");
                });

            modelBuilder.Entity("PetHome.Discussions.Domain.Message", b =>
                {
                    b.HasOne("PetHome.Discussions.Domain.Discussion", "Discussion")
                        .WithMany("Messages")
                        .HasForeignKey("DiscussionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_messages_discussions_discussion_id");

                    b.Navigation("Discussion");
                });

            modelBuilder.Entity("PetHome.Discussions.Domain.Discussion", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("PetHome.Discussions.Domain.Relation", b =>
                {
                    b.Navigation("Discussions");
                });
#pragma warning restore 612, 618
        }
    }
}
