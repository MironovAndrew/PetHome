﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.SharedKernel.ValueObjects.Discussion;
using PetHome.SharedKernel.ValueObjects.Discussion.Message;
using PetHome.SharedKernel.ValueObjects.MainInfo;
using PetHome.SharedKernel.ValueObjects.User;
using PetHome.Discussions.Domain;

namespace DiscussionService.Infrastructure.Database.Write.Configuration;
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(x => x.Id);
        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => MessageId.Create(value).Value)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(m => m.Text)
            .HasConversion(
                t => t.Value,
                value => MessageText.Create(value).Value)
            .IsRequired(false)
            .HasColumnName("text");

        builder.Property(m => m.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .IsRequired()
            .HasColumnName("user_id");

        builder.Property(m => m.IsEdited)
            .IsRequired()
            .HasColumnName("is_edited");

        builder.Property(m => m.CreatedAt)
            .HasConversion(
                d => d.Value,
                value => Date.Create(value).Value)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(m => m.DiscussionId)
            .HasConversion(
                d => d.Value,
                value => DiscussionId.Create(value).Value)
            .IsRequired()
            .HasColumnName("discussion_id");
    }
}