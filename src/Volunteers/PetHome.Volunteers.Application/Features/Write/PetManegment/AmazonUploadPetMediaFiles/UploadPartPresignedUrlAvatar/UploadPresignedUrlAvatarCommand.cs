﻿using FilesService.Core.Request.AmazonS3;
using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Volunteers.Application.Features.Write.PetManegment.AmazonUploadPetMediaFiles.UploadPartPresignedUrlAvatar;
public record UploadPresignedUrlAvatarCommand(
    IEnumerable<UploadPresignedUrlRequest> UploadPresignedUrlRequests) : ICommand;
