﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.ValueObjects.RolePermission;

namespace PetHome.Accounts.Contracts.Contracts.AuthManagement;
public interface IGetRoleContract
{
    public Task<Result<RoleId, Error>> Execute(string name, CancellationToken ct);
}
