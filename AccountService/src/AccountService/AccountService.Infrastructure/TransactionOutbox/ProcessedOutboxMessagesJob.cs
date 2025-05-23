﻿using Quartz;

namespace AccountService.Infrastructure.TransactionOutbox;

[DisallowConcurrentExecution]
public class ProcessedOutboxMessagesJob(ProcessedOutboxMessagesService service) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await service.Execute(context.CancellationToken);
    }
}
