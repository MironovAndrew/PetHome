﻿using PetHome.Application.Features.Dtos.Pet;

namespace PetHome.Application.Features.Write.PetManegment.CreatePet;
public record CreatePetCommand(Guid VolunteerId, PetMainInfoDto PetMainInfoDto);