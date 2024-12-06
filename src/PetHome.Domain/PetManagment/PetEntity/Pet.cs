﻿using CSharpFunctionalExtensions;
using PetHome.Domain.PetManagment.GeneralValueObjects;
using PetHome.Domain.PetManagment.VolunteerEntity;
using PetHome.Domain.Shared.Error;
using PetHome.Domain.Shared.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace PetHome.Domain.PetManagment.PetEntity;
public class Pet : SoftDeletableEntity
{
    private Pet() { }

    private Pet(
        PetId id,
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
        RequisitesDetails requisitesDetails,
        Date profileCreateDate)
    {
        Id = id;
        Name = name;
        SpeciesId = speciesId;
        Description = description;
        BreedId = breedId;
        Color = color;
        ShelterId = shelterId;
        Weight = weight;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        Status = status;
        RequisitesDetails = requisitesDetails;
        ProfileCreateDate = profileCreateDate;
    }


    public PetId Id { get; private set; }
    public PetName Name { get; private set; }
    public SpeciesId SpeciesId;
    public Description Description { get; private set; }
    public BreedId? BreedId { get; private set; }
    public Color Color { get; private set; }
    public PetShelterId ShelterId { get; private set; }
    public double Weight { get; private set; }
    public bool IsCastrated { get; private set; }
    public Date? BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public PetStatusEnum Status;
    public RequisitesDetails? RequisitesDetails { get; private set; }
    public Date ProfileCreateDate { get; private set; }
    public VolunteerId VolunteerId { get; private set; }
    public SerialNumber SerialNumber { get; private set; }

    public static Result<Pet, Error> Create(
        PetId id,
        PetName name,
        SpeciesId speciesId,
        Description description,
        BreedId breedId,
        Color color,
        PetShelterId ShelterId,
        double weight,
        bool isCastrated,
        Date birthDate,
        bool isVaccinated,
        PetStatusEnum status,
        RequisitesDetails requisitesDetails,
        Date profileCreateDate)
    {

        if (weight > 500 || weight <= 0)
            return Errors.Validation("Вес");

        return new Pet(
            id,
            name,
            speciesId,
            description,
            breedId,
            color,
            ShelterId,
            weight,
            isCastrated,
            birthDate,
            isVaccinated,
            status,
            requisitesDetails,
            profileCreateDate)
        { };
    }

    public override void SoftDelete() => base.SoftDelete();
    public override void SoftRestore() => base.SoftRestore();
    // Присвоить serial number = max + 1
    public void InitSerialNumer(IEnumerable<Pet> pets)
    {
        SerialNumber serialNumber = pets.Count() == 0
            ? SerialNumber.Create(1)
            : SerialNumber.Create(pets.ToList().Select(x => x.SerialNumber).Max().Value + 1);

        SerialNumber = serialNumber;
    }
    //Изменить serial number
    public void ChangeSerialNumber(IEnumerable<Pet> pets, int number)
    {
        pets.Where(x => x.SerialNumber.Value >= number)
            .Select(x => x.SerialNumber = SerialNumber.Create(SerialNumber.Value + 1));

        SerialNumber = SerialNumber.Create(number);
    }
    // Присвоить serial number =  1
    public void ChangeSerialNumberToBegining(IEnumerable<Pet> pets)
    {
        ChangeSerialNumber(pets, 1);
    }
}
