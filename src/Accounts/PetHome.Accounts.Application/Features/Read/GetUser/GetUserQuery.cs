﻿using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Accounts.Application.Features.Read.GetUserInformation;
public record GetUserQuery(Guid UserId) : IQuery;
