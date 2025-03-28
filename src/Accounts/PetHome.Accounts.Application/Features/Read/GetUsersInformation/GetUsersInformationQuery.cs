﻿using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.Models;
using PetHome.Core.Response.Dto;

namespace PetHome.Accounts.Application.Features.Read.GetUsersInformation;
public record GetUsersInformationQuery(PagedListDto PaginationSettings, UserFilterDto UserFilter) : IQuery;
