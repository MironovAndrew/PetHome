﻿using PetHome.Core.Domain.Models;
using PetHome.SharedKernel.ValueObjects.Discussion;
using PetHome.SharedKernel.ValueObjects.MainInfo;
using PetHome.SharedKernel.ValueObjects.User;
using PetHome.SharedKernel.ValueObjects.VolunteerRequest;

namespace PetHome.VolunteerRequests.Domain;
public class VolunteerRequest : DomainEntity<VolunteerRequestId>
{
    public VolunteerRequestId Id { get; private set; }
    public UserId? AdminId { get; private set; }
    public UserId UserId { get; private set; }
    public VolunteerInfo? VolunteerInfo { get; private set; }
    public VolunteerRequestStatus? Status { get; private set; }
    public Date CreatedAt { get; private set; }
    public RequestComment? RejectedComment { get; private set; }
    public DiscussionId? DiscussionId { get; private set; }

    public VolunteerRequest(VolunteerRequestId id, UserId userId, VolunteerInfo? volunteerInfo)
        : base(id)
    {
        Id = id;
        CreatedAt = Date.Create().Value;
        Status = VolunteerRequestStatus.Submitted;
        VolunteerInfo = volunteerInfo;
    }

    public static VolunteerRequest Create(
        UserId userId,
        VolunteerInfo? volunteerInfo)
    {
        VolunteerRequest volunteerRequest = new VolunteerRequest(
            VolunteerRequestId.Create().Value,
            userId,
            volunteerInfo);
        return volunteerRequest;
    }

    public void SetOnReview(
        UserId adminId,
        DiscussionId? discussionId)
    {
        AdminId = adminId;
        Status = VolunteerRequestStatus.OnReview;
        DiscussionId = discussionId;
    }

    public void SetRevisionRequired(
        UserId adminId,
        RequestComment rejectedComment)
    {
        AdminId = adminId;
        Status = VolunteerRequestStatus.RevisionRequired;
        RejectedComment = rejectedComment;
    }

    public void SetRejected(
        UserId adminId,
        RequestComment rejectedComment)
    {
        AdminId = adminId;
        Status = VolunteerRequestStatus.Rejected;
        RejectedComment = rejectedComment;
    }

    public void SetApproved(
        UserId adminId)
    {
        AdminId = adminId;
        Status = VolunteerRequestStatus.Approved;
    }

    public void SetSubmitted(
        UserId adminId)
    {
        AdminId = adminId;
        Status = VolunteerRequestStatus.Submitted;
    }
}
