﻿using Amazon.S3;
using Amazon.S3.Model;
using FilesService.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Features;

public static class GetPresignedUrl
{
    public sealed class EndPoint : IEndpoint
    {

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/{key:guid}/presigned", Handler);
        }
    }

    private record GetPresignedUrlRequest(string BucketName);

    private static async Task<IResult> Handler(
            [FromBody] GetPresignedUrlRequest request,
            Guid key,
            IAmazonS3 s3Client,
            CancellationToken ct)
    {
        try
        {
            GetPreSignedUrlRequest presignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = request.BucketName,
                Key = key.ToString(), 
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddDays(14),
                Protocol = Protocol.HTTP,
            };

            string? presignedUrl = await s3Client.GetPreSignedURLAsync(presignedUrlRequest);

            return Results.Ok(new
            {
                key,
                url = presignedUrl
            });
        }
        catch (AmazonS3Exception ex)
        {
            return Results.BadRequest($"S3: get presigned url failed: \r\t\n{ex.Message}");
        }
    }
}

