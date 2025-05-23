﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PetHome.Core.Application.Interfaces.FeatureManagement;
using PetHome.Core.Domain.Models;
using PetHome.Core.Web.Extentions.Collection;
using PetHome.SharedKernel.Responses.ErrorManagement;
using PetManagementService.Application.Database;
using PetManagementService.Application.Database.Dto;

namespace PetManagementService.Application.Features.Read.Species.GetAllSpecies;
public class GetAllSpeciesUseCase
    : IQueryHandler<PagedList<SpeciesDto>, GetAllSpeciesQuery>
{
    private readonly IPetManagementReadDbContext _readDBContext;
    private readonly ILogger<GetAllSpeciesUseCase> _logger;
    private readonly IMemoryCache _cache;

    public GetAllSpeciesUseCase(
        IPetManagementReadDbContext readDBContext,
        ILogger<GetAllSpeciesUseCase> logger,
        IMemoryCache cache)
    {
        _readDBContext = readDBContext;
        _logger = logger;
        _cache = cache;
    }

    public async Task<Result<PagedList<SpeciesDto>, ErrorList>> Execute(
        GetAllSpeciesQuery query,
        CancellationToken ct)
    {
        string key = "species";

        var cachedSpecieses = _cache.Get<IEnumerable<SpeciesDto>>(key);
        if (cachedSpecieses is null)
        {
            PagedList<SpeciesDto> pagedSpeciesDto = await _readDBContext.Species
               .ToPagedList(query.PageNum, query.PageSize, ct);

            if (pagedSpeciesDto.Count > 0)
            {
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                _cache.Set(key, pagedSpeciesDto.Items, cacheOptions);
            }

            return pagedSpeciesDto;
        }

        var cachedPagedSpeciesDto = cachedSpecieses
            .ToPagedList(query.PageNum, query.PageSize);

        return cachedPagedSpeciesDto;
    }
}
