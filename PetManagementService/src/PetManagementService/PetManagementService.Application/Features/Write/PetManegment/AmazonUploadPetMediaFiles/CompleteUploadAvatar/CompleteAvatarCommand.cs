﻿using FilesService.Core.Request.AmazonS3.MultipartUpload;
using PetHome.Core.Application.Interfaces.FeatureManagement;

namespace PetManagementService.Application.Features.Write.PetManegment.AmazonUploadPetMediaFiles.CompleteUploadAvatar;
public record CompleteUploadAvatarCommand(
    Guid VolunteerId,
    Guid PetId, 
    IEnumerable<string> Keys,
    IEnumerable<CompleteMultipartRequest> CompleteMultipartRequests) : ICommand;
