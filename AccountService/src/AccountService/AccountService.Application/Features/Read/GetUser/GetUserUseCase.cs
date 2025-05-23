﻿using AccountService.Application.Database.Repositories;
using AccountService.Domain.Aggregates;
using CSharpFunctionalExtensions;
using FilesService.Core.Dto.File;
using FilesService.Core.Interfaces;
using FilesService.Core.Request.AmazonS3;
using PetHome.Core.Application.Interfaces.FeatureManagement;
using PetHome.Core.Web.Extentions.ErrorExtentions;
using PetHome.SharedKernel.Responses.ErrorManagement;

namespace AccountService.Application.Features.Read.GetUser;
public class GetUserUseCase
    : IQueryHandler<User, GetUserQuery>
{
    private readonly IAuthenticationRepository _repository;
    private readonly IAmazonFilesHttpClient _httpClient;

    public GetUserUseCase(
        IAuthenticationRepository repository, IAmazonFilesHttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task<Result<User, ErrorList>> Execute(
        GetUserQuery query, CancellationToken ct)
    {
        var result = await _repository.GetUserById(query.UserId, ct);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        User user = result.Value;

        MediaFile? avatar = user.Avatar;
        GetPresignedUrlRequest getPresignedAvatarUrl = new GetPresignedUrlRequest(avatar.BucketName);
        var getAvatarUrl = await _httpClient.GetPresignedUrl(avatar.Key.ToString(), getPresignedAvatarUrl, ct);
        if (getAvatarUrl.IsSuccess)
            user.AvatarUrl = getAvatarUrl.Value.Url;

        List<string> photosUrls = new List<string>();
        foreach (var photo in user.Photos)
        {
            GetPresignedUrlRequest getPresignedPhotoUrlRequest = new GetPresignedUrlRequest(avatar.BucketName);
            var getPhotoUrlResult = await _httpClient.GetPresignedUrl(photo.Key.ToString(), getPresignedPhotoUrlRequest, ct);
            if (getPhotoUrlResult.IsSuccess)
                photosUrls.Add(getPhotoUrlResult.Value.Url);
        }
        user.PhotosUrls = photosUrls;

        return user;
    }
}
