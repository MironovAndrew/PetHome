﻿using FluentValidation;
using PetHome.Core.Application.Validation.Validator;
using PetHome.SharedKernel.Responses.ErrorManagement;

namespace PetManagementService.Application.Features.Write.PetManegment.DeletePetMediaFiles;
public class DeletePetMediaFileCommandValidator : AbstractValidator<DeletePetMediaFilesCommand>
{
    public DeletePetMediaFileCommandValidator()
    {
        RuleFor(d => d.DeletePetMediaFilesDto.BucketName)
            .Must(b => !string.IsNullOrWhiteSpace(b))
            .WithError(Errors.Validation("Название bucket"));

        RuleForEach(d => d.DeletePetMediaFilesDto.FilesName)
            .Must(b => !string.IsNullOrWhiteSpace(b))
            .WithError(Errors.Validation("Имя файла"));
    }
}
