﻿using CSharpFunctionalExtensions;
using PetHome.Core.Application.Interfaces.Database;
using PetHome.Core.Domain.Models;
using PetHome.SharedKernel.Responses.ErrorManagement;
using PetHome.SharedKernel.ValueObjects.MainInfo;
using PetHome.SharedKernel.ValueObjects.PetManagment.Extra;
using PetHome.SharedKernel.ValueObjects.User;
using PetManagementService.Domain.PetManagment.PetEntity;
using PetManagementService.Domain.SpeciesManagment.BreedEntity;
using PetManagementService.Domain.SpeciesManagment.SpeciesEntity;

namespace PetManagementService.Domain.PetManagment.VolunteerEntity;
public class Volunteer : DomainEntity<VolunteerId>, ISoftDeletableEntity
{
    private Volunteer(VolunteerId id) : base(id) { Id = id; }
    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Date startVolunteeringDate,
        ValueObjectList<PhoneNumber> phoneNumbers,
        ValueObjectList<SocialNetwork> socialNetworks,
        ValueObjectList<Requisites> requisites) : base(id)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Description = description;
        StartVolunteeringDate = startVolunteeringDate;
        PhoneNumbers = phoneNumbers;
        SocialNetworks = socialNetworks;
        Requisites = requisites;

    }

    public VolunteerId Id { get; private set; }
    public UserId UserId { get; private set; }
    public FullName FullName { get; private set; }
    public Email? Email { get; private set; }
    public Description Description { get; private set; }
    public Date StartVolunteeringDate { get; private set; }
    public List<Pet> Pets { get; private set; } = new List<Pet>();
    public int HomedPetsCount => GetPetCountByStatusAndVolunteer(PetStatusEnum.isHomed);
    public int FreePetsCount => GetPetCountByStatusAndVolunteer(PetStatusEnum.isFree);
    public int TreatmentPetsCount => GetPetCountByStatusAndVolunteer(PetStatusEnum.isTreatment);
    public ValueObjectList<PhoneNumber> PhoneNumbers { get; private set; }
    public ValueObjectList<Requisites> Requisites { get; private set; }
    public ValueObjectList<SocialNetwork> SocialNetworks { get; private set; } 
    public DateTime DeletionDate { get; set; }
    public bool IsDeleted { get; set; }

    private int GetPetCountByStatusAndVolunteer(PetStatusEnum status) 
        => Pets.Count(pet => pet.Status == status && pet.VolunteerId == Id);

    public static Result<Volunteer, Error> Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        Date startVolunteeringDate,
        ValueObjectList<PhoneNumber> phoneNumbers,
        ValueObjectList<Requisites> requisites,
        ValueObjectList<SocialNetwork> socialNetworks)
    {
        return new Volunteer(
            id,
            fullName,
            email,
            description,
            startVolunteeringDate,
            phoneNumbers,
            socialNetworks,
            requisites)
        { };
    }

    public void SetUserId(UserId userId)
    {
        UserId = userId;
    }

    public void UpdateMainInfo(
        FullName fullName,
        Description description,
        ValueObjectList<PhoneNumber> phoneNumbers,
        Email email)
    {
        FullName = fullName;
        Description = description;
        PhoneNumbers = phoneNumbers;
        Email = email;
    }
     
    public void SoftDelete()
    {
        DeletionDate = DateTime.UtcNow;
        IsDeleted = true;
    }

    public void SoftRestore()
    {
        DeletionDate = default;
        IsDeleted = false;
    }

    public Result<Pet, Error> CreatePet(
        PetName name,
        SpeciesId speciesId,
        Description description,
        BreedId breedId,
        Color color,
        PetShelterId shelterId,
        double weight,
        bool isCastrated,
        Date birthDate,
        bool isVaccinated,
        PetStatusEnum status,
        ValueObjectList<Requisites> requisites)
    {
        Pet.Pets = Pets;

        var result = Pet.Create(
              name,
              speciesId,
              description,
              breedId,
              color,
              shelterId,
              weight,
              isCastrated,
              birthDate,
              isVaccinated,
              status,
              Id,
              requisites);
        if (result.IsFailure)
            return result.Error;

        Pet pet = result.Value;
        Pets.Add(pet);

        return pet;
    }
}
