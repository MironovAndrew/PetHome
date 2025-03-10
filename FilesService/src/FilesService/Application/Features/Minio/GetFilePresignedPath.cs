﻿using FilesService.Application.Endpoints; 
using FilesService.Core.Interfaces;
using FilesService.Core.Dto.File;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Application.Features.Minio;

public static class GetFilePresignedPath
{ 
    public sealed class Endpoint : IEndpoint
    { 
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("minio/file-presigned-path", Handler);
        }
    }
    private static async Task<IResult> Handler(
           [FromBody] MinioFilesInfoDto fileInfoDto,
           IMinioFilesHttpClient fileProvider,
           CancellationToken ct)
    {
        var result = await fileProvider.GetFilePresignedPath(fileInfoDto, ct);
        if (result.IsFailure)
            return Results.BadRequest(result.Error);

        return Results.Ok(result.Value);
    }
}
