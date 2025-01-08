﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Species.Infrastructure.Database.Write.DBContext;

#nullable disable

namespace PetHome.Species.Infrastructure.Migrations.Write
{
    [DbContext(typeof(SpeciesWriteDbContext))]
    [Migration("20250108125740_Species_Write_InitMigrations")]
    partial class Species_Write_InitMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHome.Species.Domain.SpeciesManagment.BreedEntity.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("SpeciesId")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("SpeciesId")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetHome.Species.Domain.SpeciesManagment.SpeciesEntity.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_specieses");

                    b.ToTable("specieses", (string)null);
                });

            modelBuilder.Entity("PetHome.Species.Domain.SpeciesManagment.BreedEntity.Breed", b =>
                {
                    b.HasOne("PetHome.Species.Domain.SpeciesManagment.SpeciesEntity.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_breeds_specieses_species_id");
                });

            modelBuilder.Entity("PetHome.Species.Domain.SpeciesManagment.SpeciesEntity.Species", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}