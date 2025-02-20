﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Volunteers.Infrastructure.Database.Read.DBContext;

#nullable disable

namespace PetHome.Volunteers.Infrastructure.Migrations.Read
{
    [DbContext(typeof(VolunteerReadDbContext))]
    [Migration("20250218141043_ChangedUsingAssembliesVolunteerReadMigration")]
    partial class ChangedUsingAssembliesVolunteerReadMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHome.Species.Application.Database.Dto.BreedDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

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

            modelBuilder.Entity("PetHome.Species.Application.Database.Dto.SpeciesDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_specieses");

                    b.ToTable("specieses", (string)null);
                });

            modelBuilder.Entity("PetHome.Volunteers.Application.Database.Dto.PetDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("bith_date");

                    b.Property<Guid?>("BreedId")
                        .HasColumnType("uuid")
                        .HasColumnName("breed_id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsCastrated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_castrated");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Photos")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("photos");

                    b.Property<DateTime>("ProfileCreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("profile_create_date");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("integer")
                        .HasColumnName("serial_number");

                    b.Property<Guid>("ShelterId")
                        .HasColumnType("uuid")
                        .HasColumnName("shelter_id");

                    b.Property<Guid>("SpeciesId")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.ComplexProperty<Dictionary<string, object>>("Avatar", "PetHome.Volunteers.Application.Database.Dto.PetDto.Avatar#MediaFile", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("BucketName")
                                .HasColumnType("text")
                                .HasColumnName("bucket_name");

                            b1.Property<string>("FileName")
                                .HasColumnType("text")
                                .HasColumnName("file_name");

                            b1.Property<Guid?>("Key")
                                .HasColumnType("uuid")
                                .HasColumnName("key");

                            b1.Property<string>("Type")
                                .HasColumnType("text")
                                .HasColumnName("type");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetHome.Volunteers.Application.Database.Dto.VolunteerDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("StartVolunteeringDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_volunteering_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetHome.Species.Application.Database.Dto.BreedDto", b =>
                {
                    b.HasOne("PetHome.Species.Application.Database.Dto.SpeciesDto", null)
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_breeds_specieses_species_id");
                });

            modelBuilder.Entity("PetHome.Species.Application.Database.Dto.SpeciesDto", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
