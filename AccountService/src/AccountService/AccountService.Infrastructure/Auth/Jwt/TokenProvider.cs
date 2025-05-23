﻿using AccountService.Application;
using AccountService.Domain.Aggregates;
using AccountService.Infrastructure.DI.Auth;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetHome.Core.Web.Extentions.ErrorExtentions;
using PetHome.Core.Web.Options.Accounts;
using PetHome.SharedKernel.Responses.ErrorManagement;
using PetHome.SharedKernel.Responses.RefreshToken;
using PetHome.SharedKernel.ValueObjects.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.Infrastructure.Auth.Jwt;
public class TokenProvider : ITokenProvider
{
    private readonly JwtOption _options;
    private readonly IConfiguration _configuration;

    public TokenProvider(IConfiguration configuration)
    {
        _options = TokenValidationManager.GetJwtOptions(configuration);
        _configuration = configuration;
    }

    public Result<RefreshSession, ErrorList> GenerateRefreshToken(
        User user, RefreshSession oldRefreshSession)
    {
        Guid userId = oldRefreshSession.UserId;
        Guid jti = oldRefreshSession.JTI;

        bool isValidUser = userId == user.Id;
        if (isValidUser)
        {
            RefreshSession refreshSession = new RefreshSession()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ExpiredIn = DateTime.UtcNow.AddDays(10),
                UserId = UserId.Create(user.Id).Value,
                RefreshToken = Guid.NewGuid(),
                JTI = jti
            };
            return refreshSession;
        }
        return Errors.Validation("Refresh token").ToErrorList();
    }


    public Result<RefreshSession, ErrorList> GenerateRefreshToken(
        User user, string accessToken)
    {
        var getUserIdResult = GetUserId(accessToken);
        var getJtiResult = GetJti(accessToken);
        if (getUserIdResult.IsFailure || getJtiResult.IsFailure)
            return new ErrorList([getJtiResult.Error, getJtiResult.Error]);

        Guid userId = getUserIdResult.Value;
        Guid jti = getJtiResult.Value;

        bool isValidUser = userId == user.Id;
        if (isValidUser)
        {
            RefreshSession refreshSession = new RefreshSession()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ExpiredIn = DateTime.UtcNow.AddDays(10),
                UserId = UserId.Create(user.Id).Value,
                RefreshToken = Guid.NewGuid(),
                JTI = jti
            };
            return refreshSession;
        }
        return Errors.Validation("Refresh token").ToErrorList();
    }

    public string GenerateAccessToken(User user)
    {
        Guid jti = Guid.NewGuid();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiredMinutes));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }


    public IReadOnlyList<Claim> GetClaims(string accessToken)
    {
        var validations = TokenValidationManager.GetTokenValidationParameters(_configuration);
        var handler = new JwtSecurityTokenHandler();
        var claims = handler.ValidateToken(accessToken, validations, out var tokenSecure)?.Claims.ToList();
        return claims;
    }

    public Result<Guid, Error> GetUserId(string accessToken)
    {
        Claim? userIdClaim = GetClaims(accessToken).First();

        bool isUserIdParsed = Guid.TryParse(userIdClaim?.Value, out Guid userId);
        if (isUserIdParsed)
            return userId;
        else
            return Errors.Validation("UserId claim");
    }

    public Result<Guid, Error> GetJti(string accessToken)
    {
        Claim? jtiClaim = GetClaims(accessToken)
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

        bool isJtiParsed = Guid.TryParse(jtiClaim?.Value, out Guid jti);
        if (isJtiParsed)
            return jti;
        else
            return Errors.Validation("UserId claim");
    }
}
