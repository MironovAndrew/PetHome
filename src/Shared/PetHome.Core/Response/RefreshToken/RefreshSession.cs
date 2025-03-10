﻿using PetHome.Core.ValueObjects.User;

namespace PetHome.Core.Response.RefreshToken;
public class RefreshSession
{ 
    public Guid Id { get; init; }
    public Guid RefreshToken { get; init; }
    public Guid JTI { get; init; }
    public Guid UserId { get; init; }
    public DateTime ExpiredIn { get; init; }
    public DateTime CreatedAt { get; init; }
}
