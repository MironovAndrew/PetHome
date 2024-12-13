﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Infrastructure.DataBase;

#nullable disable

namespace PetHome.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241212191405_Init")]
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

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.Breed", b =>
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

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("SpeciesId")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.Pet", b =>
                {
                    b.Property<Guid>("Id")
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

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

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
                        .HasMaxLength(500)
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

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

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.PetShelter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.PetManagment.PetEntity.PetShelter.Name#ShelterName", b1 =>
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

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.Species", b =>
                {
                    b.Property<Guid>("Id")
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

            modelBuilder.Entity("PetHome.Domain.PetManagment.VolunteerEntity.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("soft_deleted_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateTime>("StartVolunteeringDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_volunteering_date");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetHome.Domain.PetManagment.VolunteerEntity.Volunteer.FullName#FullName", b1 =>
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

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.Breed", b =>
                {
                    b.HasOne("PetHome.Domain.PetManagment.PetEntity.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_breeds_specieses_species_id");
                });

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.Pet", b =>
                {
                    b.HasOne("PetHome.Domain.PetManagment.VolunteerEntity.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("ValueObjectList", "Requisites", b1 =>
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

                            b1.OwnsMany("PetHome.Domain.PetManagment.GeneralValueObjects.Requisites", "Values", b2 =>
                                {
                                    b2.Property<Guid>("ValueObjectListPetId")
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

                                    b2.HasKey("ValueObjectListPetId", "__synthesizedOrdinal")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("ValueObjectListPetId")
                                        .HasConstraintName("fk_pets_pets_value_object_list_pet_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PetHome.Domain.Shared.ValueObjectList<PetHome.Domain.PetManagment.GeneralValueObjects.Media>", "Medias", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("photos");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PetHome.Domain.PetManagment.GeneralValueObjects.Media", "Values", b2 =>
                                {
                                    b2.Property<Guid>("ValueObjectListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("BucketName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("FileName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ValueObjectListPetId", "__synthesizedOrdinal")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("ValueObjectListPetId")
                                        .HasConstraintName("fk_pets_pets_value_object_list_pet_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.Navigation("Medias")
                        .IsRequired();

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Domain.PetManagment.VolunteerEntity.Volunteer", b =>
                {
                    b.OwnsOne("PetHome.Domain.Shared.ValueObjectList<PetHome.Domain.PetManagment.GeneralValueObjects.Requisites>", "Requisites", b1 =>
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

                            b1.OwnsMany("PetHome.Domain.PetManagment.GeneralValueObjects.Requisites", "Values", b2 =>
                                {
                                    b2.Property<Guid>("ValueObjectList<Requisites>VolunteerId")
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

                                    b2.HasKey("ValueObjectList<Requisites>VolunteerId", "__synthesizedOrdinal")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("ValueObjectList<Requisites>VolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_value_object_list_requisites_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PetHome.Domain.Shared.ValueObjectList<PetHome.Domain.PetManagment.GeneralValueObjects.PhoneNumber>", "PhoneNumbers", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("phone_numbers");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetHome.Domain.PetManagment.GeneralValueObjects.PhoneNumber", "Values", b2 =>
                                {
                                    b2.Property<Guid>("ValueObjectListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ValueObjectListVolunteerId", "__synthesizedOrdinal");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("ValueObjectListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_value_object_list_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PetHome.Domain.Shared.ValueObjectList<PetHome.Domain.PetManagment.GeneralValueObjects.SocialNetwork>", "SocialNetworks", b1 =>
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

                            b1.OwnsMany("PetHome.Domain.PetManagment.GeneralValueObjects.SocialNetwork", "Values", b2 =>
                                {
                                    b2.Property<Guid>("ValueObjectListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ValueObjectListVolunteerId", "__synthesizedOrdinal");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("ValueObjectListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_value_object_list_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.Navigation("PhoneNumbers")
                        .IsRequired();

                    b.Navigation("Requisites")
                        .IsRequired();

                    b.Navigation("SocialNetworks")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Domain.PetManagment.PetEntity.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetHome.Domain.PetManagment.VolunteerEntity.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
