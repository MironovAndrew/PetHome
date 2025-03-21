﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.Validation.Validator;

namespace PetHome.Core.Interfaces.FeatureManagment;
public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, ErrorList>> Execute(TCommand command, CancellationToken ct);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> Execute(TCommand command, CancellationToken ct);
}
