﻿using CSharpFunctionalExtensions;
using FilesService.Core.Dto.File;
using FilesService.Core.ErrorManagment;
using FilesService.Core.Interfaces;
using FilesService.Core.Request.Minio;
using Minio.DataModel.Args;

namespace FilesService.Infrastructure.Minio;
public partial class MinioProvider : IMinioFilesHttpClient
{
    //Загрузить файл
    public async Task<Result<MediaFile, string>> UploadFile( 
       UploadFileRequest request,
       CancellationToken ct)
    {
        PutObjectArgs minioFileArgs = new PutObjectArgs()
        .WithBucket(request.FileInfo.BucketName.ToLower())
            .WithStreamData(request.Stream)
            .WithObjectSize(request.Stream.Length)
            .WithObject(request.FileInfo.FileName);

        var result = await _minioClient.PutObjectAsync(minioFileArgs, ct);
        string message = $"Файл {result.ObjectName} загружен в bucket = {request.FileInfo.BucketName}";
        _logger.LogInformation(message);
         
        MediaFile mediaFile = MediaFile.Create(request.FileInfo.BucketName, request.FileInfo.FileName).Value;
        return mediaFile;
    }
}
