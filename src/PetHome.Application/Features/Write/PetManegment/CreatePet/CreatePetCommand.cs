﻿using PetHome.Application.Features.Dtos.Pet;
using PetHome.Application.Interfaces.FeatureManagment;

namespace PetHome.Application.Features.Write.PetManegment.CreatePet;
public record CreatePetCommand(Guid VolunteerId, PetMainInfoDto PetMainInfoDto) : ICommand;