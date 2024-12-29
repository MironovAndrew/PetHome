﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio.DataModel.Args;
using PetHome.Core.Interfaces;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.SharedKernel.Providers.Minio;
public partial class MinioProvider : IFilesProvider
{
    //Удалить файлы
    public async Task<Result<string, Error>> DeleteFile(
         MinioFilesInfoDto fileInfoDto, CancellationToken ct)
    {
        var isExistBucketResult = await CheckIsExistBucket(fileInfoDto.BucketName, ct);
        if (isExistBucketResult.IsFailure)
            return isExistBucketResult.Error;

        foreach (var fileName in fileInfoDto.FileNames)
        {
            var minioFileArgs = new RemoveObjectArgs()
                .WithBucket(fileInfoDto.BucketName)
                .WithObject(fileName.Value);
            await _minioClient.RemoveObjectAsync(minioFileArgs).ConfigureAwait(false);

            _logger.LogInformation("Файл {0} в bucket {1}  успешно удалён", fileInfoDto.FileNames, fileInfoDto.BucketName);
        }
        string message = $"В bucket {fileInfoDto.BucketName} успешно удалены удалены:\n\t {string.Join("\n\t", fileInfoDto.FileNames)}";
        _logger.LogInformation(message);
        return message;
    }
}