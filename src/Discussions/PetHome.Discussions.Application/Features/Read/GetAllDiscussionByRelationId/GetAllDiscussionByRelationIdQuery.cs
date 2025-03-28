﻿using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Discussions.Application.Features.Read.GetAllDiscussionByRelationId;
public record GetAllDiscussionByRelationIdQuery(Guid RelationId, int PageSize, int PageNum) : IQuery;
