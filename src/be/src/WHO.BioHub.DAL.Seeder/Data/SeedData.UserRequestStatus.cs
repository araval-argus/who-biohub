using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static UserRequestStatus[] UserRequestStatuses = new UserRequestStatus[]
    {
        new()
        {
            Id = UserRequestStatusId1,
            Subject = "WHO BioHub User Access Request Notification",
            Message = "<p>Dear all,<br><br>A new user {firstname} {lastname} <a href=\"mailto:{email}\">{email}</a> with institute name {instituteName} has requested to join WHO.<br><br>Please check the information and provide feedback</p>",
            IsResponseMessage = false,
            Status = UserRegistrationStatus.Pending
        },
        new()
        {
            Id = UserRequestStatusId2,
            Subject = "WHO BioHub User Access Request Notification",
            Message = "<p>Dear {firstname} {lastname},<br><br>Thank you for contacting the BioHub System and expressing your interest in working with us.</p><p>We are pleased to inform you that your request for the IT tool registration has been successfully approved.<br><br>Please use this link to check your username and temporary password.<br><br>Once you log in to the system from this link, please reset your password through the User Profile page.</p><p>We are looking forward to collaborating with you through the WHO BioHub System.<br><br>Best regards,<br>The WHO BioHub System Secretariat</p>",
            IsResponseMessage = true,
            Status = UserRegistrationStatus.Approved
        },
        new()
        {
            Id = UserRequestStatusId3,
            Subject = "WHO BioHub User Access Request Notification",
            Message = "<p>Dear {firstname} {lastname},</p><p></p><p>Thank you for contacting the BioHub System and expressing your interest in working with us.</p><p></p><p>After our careful review of your provided information for this request, we regret to inform you that we could not approve your request for the following reason:</p><ul><li><p>We could not validate your identity with the provided information through making a call and/or emailing after multiple attempts;</p></li><li><p>The provided information was insufficient for us to approve the request and we could not complete the missing information by making a call and/or emailing after multiple attempts;</p></li><li><p>Your provided information was inaccurate for us to approve the request and we could not obtain correct information from you by making a call and/or emailing after multiple attempts.</p></li><li><p>[Free text for other reasons]</p></li></ul><p></p><p>We hope that you understand our decision but please don’t hesitate to contact us (<a target=\"_blank\" rel=\"noopener noreferrer nofollow\" href=\"mailto:biohub@who.int\">biohub@who.int</a>) if you have any questions.</p><p>Also, note that this does not necessarily mean you can no longer be eligible for making the registration request. If you are willing to make another request in the future, we are happy to review it again for approval. We thank you again for your interest in the BioHub System.</p><p></p><p>Best regards,</p><p>The WHO BioHub System Secretariat</p>",
            IsResponseMessage = true,
            Status = UserRegistrationStatus.Rejected
        },

    };

    internal static Guid UserRequestStatusId1 => Guid.Parse("8a6e7ad2-da3d-4078-b755-6b7e5c683820");
    internal static Guid UserRequestStatusId2 => Guid.Parse("46e8aa2a-67cb-4140-ae3c-1e147ebe65dd");
    internal static Guid UserRequestStatusId3 => Guid.Parse("651b49ba-bf5b-4c5e-bacc-3ba9d421ae7b");

        

    private async Task AddOrUpdateUserRequestStatuses(CancellationToken cancellationToken)
    {
        foreach (var userRequestStatus in UserRequestStatuses)
        {
            if (await _db.UserRequestStatuses.Where(x => x.Id == userRequestStatus.Id).AnyAsync(cancellationToken))
            {
                _db.Update(userRequestStatus);
            }
            else
            {
                await _db.AddAsync(userRequestStatus);
            }
        }
    }
}