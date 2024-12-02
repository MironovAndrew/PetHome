﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Infrastructure;

#nullable disable

namespace PetHome.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
<<<<<<< HEAD:src/PetHome.Infrastructure/Migrations/20241202203644_Init.Designer.cs
    [Migration("20241202203644_Init")]
=======
    [Migration("20241202210812_Init")]
>>>>>>> :src/PetHome.Infrastructure/Migrations/20241202210812_Init.Designer.cs
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHome.Domain.PetEntity.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("SpeciesId")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", null, t =>
                        {
                            t.Property("species_id")
                                .HasColumnName("species_id1");
                        });
                });

            modelBuilder.Entity("PetHome.Domain.PetEntity.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date")
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
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
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

                    b.Property<DateOnly>("ProfileCreateDate")
                        .HasColumnType("date")
                        .HasColumnName("profile_create_date");

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
                        .HasMaxLength(500)
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", null, t =>
                        {
                            t.Property("volunteer_id")
                                .HasColumnName("volunteer_id1");
                        });
                });

            modelBuilder.Entity("PetHome.Domain.PetEntity.PetShelter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.PetEntity.PetShelter.Name#ShelterName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_shelters");

                    b.ToTable("shelters", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.PetEntity.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.VolunteerEntity.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateOnly>("StartVolunteeringDate")
                        .HasColumnType("date")
                        .HasColumnName("start_volunteering_date");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetHome.Domain.VolunteerEntity.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("last_name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.PetEntity.Breed", b =>
                {
                    b.HasOne("PetHome.Domain.PetEntity.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("PetHome.Domain.PetEntity.Pet", b =>
                {
                    b.HasOne("PetHome.Domain.VolunteerEntity.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PetHome.Domain.GeneralValueObjects.RequisitesDetails", "RequisitesDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("pets");

                            b1.ToJson("requisites");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("PetHome.Domain.GeneralValueObjects.Requisites", "Values", b2 =>
                                {
                                    b2.Property<Guid>("RequisitesDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<int>("PaymentMethod")
                                        .HasColumnType("integer");

                                    b2.HasKey("RequisitesDetailsPetId", "__synthesizedOrdinal")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisitesDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_requisites_details_pet_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.Navigation("RequisitesDetails");
                });

            modelBuilder.Entity("PetHome.Domain.VolunteerEntity.Volunteer", b =>
                {
                    b.OwnsOne("PetHome.Domain.GeneralValueObjects.PhoneNumbersDetails", "PhoneNumberDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("phonenumbers");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetHome.Domain.GeneralValueObjects.PhoneNumber", "Values", b2 =>
                                {
                                    b2.Property<Guid>("PhoneNumbersDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("PhoneNumbersDetailsVolunteerId", "__synthesizedOrdinal");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("PhoneNumbersDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_phone_numbers_details_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PetHome.Domain.GeneralValueObjects.SocialNetworkDetails", "SocialNetworkDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("social_networks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetHome.Domain.GeneralValueObjects.SocialNetwork", "Values", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("SocialNetworkDetailsVolunteerId", "__synthesizedOrdinal");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_details_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PetHome.Domain.GeneralValueObjects.RequisitesDetails", "RequisitesDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("requisites");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetHome.Domain.GeneralValueObjects.Requisites", "Values", b2 =>
                                {
                                    b2.Property<Guid>("RequisitesDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<int>("PaymentMethod")
                                        .HasColumnType("integer");

                                    b2.HasKey("RequisitesDetailsVolunteerId", "__synthesizedOrdinal")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisitesDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisites_details_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.Navigation("PhoneNumberDetails");

                    b.Navigation("RequisitesDetails");

                    b.Navigation("SocialNetworkDetails");
                });

            modelBuilder.Entity("PetHome.Domain.PetEntity.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetHome.Domain.VolunteerEntity.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
