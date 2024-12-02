﻿using CSharpFunctionalExtensions;
using PetHome.Domain.GeneralValueObjects;
using PetHome.Domain.PetEntity;
using PetHome.Domain.Shared.Error;

namespace PetHome.Domain.VolunteerEntity;
public class Volunteer
{
    private Volunteer() { }

    private Volunteer( 
        VolunteerId id,
        FullName fullName,
        Email email,
        string description,
        Date startVolunteeringDate,
        PhoneNumbersDetails phoneNumbersDetails,
        SocialNetworkDetails socialNetworkDetails,
        RequisitesDetails requisitesDetails)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Description = description;
        StartVolunteeringDate = startVolunteeringDate;
        PhoneNumberDetails = phoneNumbersDetails;
        SocialNetworkDetails = socialNetworkDetails;
        RequisitesDetails = requisitesDetails;

    }

    public VolunteerId Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email? Email { get; private set; }
    public string Description { get; private set; }
    public Date StartVolunteeringDate { get; private set; }
    public IReadOnlyList<Pet> Pets { get; private set; }
    public int HomedPetsCount => GetPetCountByStatusAndVolunteer(PetStatusEnum.isHomed);
    public int FreePetsCount => GetPetCountByStatusAndVolunteer(PetStatusEnum.isFree);
    public int TreatmentPetsCount => GetPetCountByStatusAndVolunteer(PetStatusEnum.isTreatment);
    public PhoneNumbersDetails? PhoneNumberDetails { get; private set; }
    public RequisitesDetails? RequisitesDetails { get; private set; }
    public SocialNetworkDetails? SocialNetworkDetails { get; private set; }


    private int GetPetCountByStatusAndVolunteer(PetStatusEnum status) => Pets.Where(pet => pet.Status == status && pet.VolunteerId == Id).Count();

    public static Result<Volunteer, Error> Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        string description,
        Date startVolunteeringDate,
        PhoneNumbersDetails? phoneNumbersDetails,
        SocialNetworkDetails? socialNetworkDetails,
        RequisitesDetails? requisitesDetails)
    { 
        return new Volunteer(id, fullName, email, description, startVolunteeringDate, phoneNumbersDetails, socialNetworkDetails, requisitesDetails) { };
    }
}
