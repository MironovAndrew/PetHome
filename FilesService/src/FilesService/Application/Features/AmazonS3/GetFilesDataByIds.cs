﻿using FilesService.Application.Endpoints;
using FilesService.Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Application.Features.AmazonS3;

public static class GetFilesDataByIds
{
    private record GetFilesDataByIdsRequest(Guid[] Ids);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("files", Handler);
        }
    }
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
