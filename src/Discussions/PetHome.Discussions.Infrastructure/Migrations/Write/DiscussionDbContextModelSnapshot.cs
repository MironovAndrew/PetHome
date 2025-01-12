﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Discussions.Infrastructure.Database;

#nullable disable

namespace PetHome.Discussions.Infrastructure.Migrations.Write
{
    [DbContext(typeof(DiscussionDbContext))]
    partial class DiscussionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Navigation("Relation");
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
