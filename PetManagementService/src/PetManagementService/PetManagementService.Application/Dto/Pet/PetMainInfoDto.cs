﻿using PetManagementService.Domain.PetManagment.PetEntity;

namespace PetManagementService.Application.Dto.Pet;
public record PetMainInfoDto(
            string Name,
            Guid SpeciesId,
            string Description,
            Guid BreedId,
            string Color,
            Guid ShelterId,
            double Weight,
            bool IsCastrated,
            DateTime BirthDate,
            bool IsVaccinated,
            PetStatusEnum Status,
            DateTime ProfileCreateDate,
            IEnumerable<RequisitesesDto> Requisites);
