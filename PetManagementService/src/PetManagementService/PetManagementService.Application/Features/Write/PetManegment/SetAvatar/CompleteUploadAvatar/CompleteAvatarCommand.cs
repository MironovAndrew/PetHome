﻿using FilesService.Core.Request.AmazonS3.MultipartUpload;
using PetHome.Core.Application.Interfaces.FeatureManagement;

namespace PetManagementService.Application.Features.Write.PetManegment.SetAvatar.CompleteUploadAvatar;
public record CompleteUploadAvatarCommand(
    Guid VolunteerId,
    Guid PetId, 
    string Key,
    CompleteMultipartRequest CompleteMultipartRequest) : ICommand;
