﻿using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.ValueObjects.User;

namespace PetHome.Accounts.Application.Features.Write.CreateVolunteer;
public record CreateVolunteerAccountCommand(
        string Email,
        string UserName,
        DateTime StartVolunteeringDate,
        IReadOnlyList<RequisitesesDto> Requisites,
        IReadOnlyList<CertificateDto> Certificates) : ICommand;
