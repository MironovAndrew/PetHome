﻿using PetHome.Core.Application.Interfaces.FeatureManagement;

namespace PetManagementService.Application.Features.Write.PetManegment.SoftDeleteRestore;
public record SoftDeleteRestorePetCommand(
    Guid VolunteerId,
    Guid PetId,
    bool ToDelete) : ICommand;
