﻿using CSharpFunctionalExtensions;
using PetHome.SharedKernel.Responses.ErrorManagement;

namespace FilesService.Core.Dto.File;
public record MediaFile
{
    public Guid? Key { get; }
    public FileType? Type { get; }
    public string? BucketName { get; }
    public string? FileName { get; }

    private MediaFile() { }
    private MediaFile(
        FileType type,
        string bucketName,
        string fileName)
    {
        Key = Guid.NewGuid();
        Type = type;
        BucketName = bucketName;
        FileName = fileName;
    }

    public static Result<MediaFile, Error> Create(
        string bucketName,
        string fileName,
        FileType type = FileType.image)
    {
        if (string.IsNullOrWhiteSpace(bucketName)
            || string.IsNullOrWhiteSpace(fileName))
            return Errors.Validation("Название bucket и файла не должны бать пустыми");

        return new MediaFile(type, bucketName, fileName);
    }
}
