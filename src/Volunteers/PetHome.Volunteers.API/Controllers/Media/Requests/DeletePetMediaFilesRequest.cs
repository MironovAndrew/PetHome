﻿using PetHome.Core.Interfaces;
using PetHome.Volunteers.Application.Features.Dto.Pet;
using PetHome.Volunteers.Application.Features.Write.PetManegment.DeletePetMediaFiles;

public record DeletePetMediaFilesRequest(
    Guid VolunteerId,
    DeletePetMediaFilesDto DeletePetMediaFilesDto,
    IFilesProvider FileProvider)
{
    public static implicit operator DeletePetMediaFilesCommand(
        DeletePetMediaFilesRequest request)
    {
        return new DeletePetMediaFilesCommand(
            request.VolunteerId,
            request.DeletePetMediaFilesDto,
            request.FileProvider);
    } 
} 