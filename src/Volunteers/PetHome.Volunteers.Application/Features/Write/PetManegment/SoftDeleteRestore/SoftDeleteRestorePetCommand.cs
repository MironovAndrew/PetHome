﻿using PetHome.Application.Interfaces.FeatureManagment;

namespace PetHome.Application.Features.Write.PetManegment.SoftDeleteRestore;
public record SoftDeleteRestorePetCommand(
    Guid VolunteerId,
    Guid PetId,
    bool ToDelete) : ICommand;