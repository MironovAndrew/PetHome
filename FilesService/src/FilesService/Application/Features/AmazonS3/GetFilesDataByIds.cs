﻿using FilesService.Application.Endpoints;
using FilesService.Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Application.Features.AmazonS3;

public static class GetFilesDataByIds
{
    private record GetFilesDataByIdsRequest(IEnumerable<Guid> Ids);

    public sealed class Endpoint : IEndpoint
    {
        //Использую POST, потому что у GET нет Body
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("amazon/files", Handler);
        }
    }

    /// <summary>
    /// Получить данные файлов через коллекцию id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="repository"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private static async Task<IResult> Handler(
           [FromBody] GetFilesDataByIdsRequest request,
           MongoDbRepository repository,
           CancellationToken ct)
    {
        var result = await repository.Get(request.Ids, ct);
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return Results.BadRequest();
    }
}
