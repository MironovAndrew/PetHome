﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Core.Interfaces.Database;
using PetHome.Core.Models;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.ValueObjects.File;
using PetHome.Core.ValueObjects.MainInfo;
using PetHome.Core.ValueObjects.RolePermission;
using PetHome.Core.ValueObjects.User;

namespace PetHome.Accounts.Domain.Aggregates;
public class User : IdentityUser<Guid>, ISoftDeletableEntity
{
    public static RoleName ROLE = RoleName.Create("user").Value;
    public IReadOnlyList<SocialNetwork>? SocialNetworks { get; private set; } = [];
    public IReadOnlyList<PhoneNumber>? PhoneNumbers { get; private set; } = [];
    public RoleId? RoleId { get; private set; }
    public Role? Role { get; set; }
    public Date? BirthDate { get; set; }
    public DateTime DeletionDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public AdminAccount? Admin { get; private set; }
    public ParticipantAccount? Participant { get; private set; }
    public VolunteerAccount? Volunteer { get; private set; }
    public IReadOnlyList<MediaFile> Medias { get; private set; } = [];
    public MediaFile? Avatar { get; private set; }

    public User() { }

    private User(
            Email email,
            UserName userName,
            Role role,
            MediaFile avatar = null)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = userName;
        Role = role;
        RoleId = RoleId.Create(role.Id).Value;
        Avatar = avatar;
    }

    public static Result<User, Error> Create(
            Email email,
            UserName userName,
            Role role,
            MediaFile avatar = null)
    {
        if (role != null && email != null && role != null)
            return new User(email, userName, role, avatar);

        return Errors.Validation("User");
    }

    private User(
        IReadOnlyList<SocialNetwork> socialNetworks,
        IReadOnlyList<MediaFile> medias,
        IReadOnlyList<PhoneNumber> phoneNumbers,
        RoleId roleId)
    {
        SocialNetworks = socialNetworks;
        Medias = medias;
        PhoneNumbers = phoneNumbers;
        RoleId = roleId;
    }

    public static User Create(
        IEnumerable<SocialNetwork> socialNetworks,
        IEnumerable<MediaFile> medias,
        IEnumerable<PhoneNumber> phoneNumbers,
        RoleId roleId)
    {
        return new User(
            socialNetworks.ToList(),
            medias.ToList(),
            phoneNumbers.ToList(),
            roleId);
    }

    public UnitResult<Error> SetMainInfo(UserName userName, Email email, Date birthDate)
    {
        UserName = userName;
        Email = email;
        BirthDate = birthDate;
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> SetSocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> SetMedia(IEnumerable<MediaFile> medias)
    {
        Medias = medias.ToList();
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> SetPhoneNumbers(IEnumerable<PhoneNumber> phoneNumbers)
    {
        PhoneNumbers = phoneNumbers.ToList();
        return UnitResult.Success<Error>();
    }

     
    public void SetAvatar(MediaFile avatar)
    {
        Avatar = avatar;
    }


    //Добавить медиа
    public UnitResult<Error> UploadMedia(IEnumerable<MediaFile> mediasToUpload)
    {
        List<MediaFile> newMediaFiles = new List<MediaFile>(mediasToUpload); 

        IReadOnlyList<MediaFile> oldMedias = Medias.ToList();
        oldMedias.ToList().ForEach(x => newMediaFiles.Add(MediaFile.Create(x.BucketName, x.FileName).Value));
         
        Medias = newMediaFiles;

        return Result.Success<Error>();
    }


    //Удалить медиа
    public UnitResult<Error> RemoveMedia(IEnumerable<MediaFile> mediasToDelete)
    {
        List<MediaFile> oldMediaFiles = Medias
            .Select(m => MediaFile.Create(m.BucketName, m.FileName).Value).ToList();

        List<MediaFile> newMediaFiles = oldMediaFiles.Except(mediasToDelete).ToList();
         
        Medias = newMediaFiles;

        return Result.Success<Error>();
    }


    //Класс не может наследоваться от двух абстрактных классов одновременно,
    //поэтому реализация интерфейса ISoftDeletableEntity
    public void SoftDelete()
    {
        DeletionDate = DateTime.UtcNow;
        IsDeleted = true;
    }

    public void SoftRestore()
    {
        DeletionDate = default;
        IsDeleted = false;
    }
}
