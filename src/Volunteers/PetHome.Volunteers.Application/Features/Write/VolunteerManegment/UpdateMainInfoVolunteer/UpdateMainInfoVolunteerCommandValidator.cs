﻿using FluentValidation;
using PetHome.Core.Response.Validation.Validator;
using PetHome.Core.ValueObjects.MainInfo;
using PetHome.Core.ValueObjects.PetManagment.Extra;

namespace PetHome.Volunteers.Application.Features.Write.VolunteerManegment.UpdateMainInfoVolunteer;
public class UpdateMainInfoVolunteerCommandValidator
    : AbstractValidator<UpdateMainInfoVolunteerCommand>
{
    public UpdateMainInfoVolunteerCommandValidator()
    {
        RuleFor(u => u.UpdateMainInfoDto.FullNameDto).MustBeValueObject(n => FullName.Create(n.FirstName, n.LastName));

        RuleFor(u => u.UpdateMainInfoDto.Description).MustBeValueObject(Description.Create);

        RuleForEach(u => u.UpdateMainInfoDto.PhoneNumbers)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(u => u.UpdateMainInfoDto.Email).MustBeValueObject(Email.Create);
    }
}
