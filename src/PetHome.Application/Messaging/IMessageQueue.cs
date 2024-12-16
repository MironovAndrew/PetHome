﻿namespace PetHome.Infrastructure.MessageQueues;

public interface IMessageQueue
{
    public Task WriteAsync(MinioFilesInfoDto filesInfoDto, CancellationToken ct);
    public Task<MinioFilesInfoDto> ReadAsync(CancellationToken ct);
}