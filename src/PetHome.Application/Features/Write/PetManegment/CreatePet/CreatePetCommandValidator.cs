﻿using FluentValidation;
using PetHome.Application.Validator;
using PetHome.Domain.PetManagment.GeneralValueObjects;
using PetHome.Domain.PetManagment.PetEntity;
using PetHome.Domain.PetManagment.VolunteerEntity;

namespace PetHome.Application.Features.Write.PetManegment.CreatePet;
public class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(p => p.PetMainInfoDto.Name).MustBeValueObject(PetName.Create);
        RuleFor(p => p.PetMainInfoDto.SpeciesId).MustBeValueObject(SpeciesId.Create);
        RuleFor(p => p.PetMainInfoDto.Description).MustBeValueObject(Description.Create);
        RuleFor(p => p.PetMainInfoDto.BreedId).MustBeValueObject(BreedId.Create);
        RuleFor(p => p.PetMainInfoDto.Color).MustBeValueObject(Color.Create);
        RuleFor(p => p.PetMainInfoDto.ShelterId).MustBeValueObject(PetShelterId.Create);
        RuleFor(p => p.PetMainInfoDto.BirthDate).MustBeValueObject(Date.Create);

        RuleForEach(p => p.PetMainInfoDto.Requisites)
            .MustBeValueObject(r => Requisites.Create(r.Name, r.Desc, r.PaymentMethod));

        RuleFor(p => p.VolunteerId).MustBeValueObject(VolunteerId.Create);
        RuleFor(p => p.PetMainInfoDto.ProfileCreateDate).MustBeValueObject(Date.Create);
    }
}