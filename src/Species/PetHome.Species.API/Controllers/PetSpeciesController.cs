﻿using Microsoft.AspNetCore.Mvc;
using PetHome.Species.API.Controllers.Requests;
using PetHome.Species.Application.Features.Read.Species.GetAllSpecies;
using PetHome.Species.Application.Features.Write.CreateSpecies;
using PetHome.Species.Application.Features.Write.DeleteSpeciesById;

namespace PetHome.Species.API.Controllers;

public class PetSpeciesController : ParentController
{
    [HttpPost]
    public async Task<IActionResult> CreateSpecies(
        [FromBody] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesUseCase useCase,
        CancellationToken ct = default)
    {
        var result = await useCase.Execute(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllSpeciesUseCase useCase,
        CancellationToken ct)
    {
        var result = await useCase.Execute(ct);
        return Ok(result);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSpeciesWithBreeds(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesByIdUseCase useCase,
        CancellationToken ct)
    {
        DeleteSpeciesByIdRequest request = new DeleteSpeciesByIdRequest(id);
        var result = await useCase.Execute(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
