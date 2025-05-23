﻿using FilesService.Core.Interfaces;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Volunteers.Application.Dto.Pet;

namespace PetHome.Volunteers.Application.Features.Write.PetManegment.MinioUploadPetMediaFiles;
public record UploadPetMediaFilesCommand(
    IEnumerable<Stream> Streams,
    IEnumerable<string> FileNames,
    UploadPetMediaFilesDto UploadPetMediaDto,
    IMinioFilesHttpClient FilesHttpClient,
    Guid VolunteerId) : ICommand;