using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{

    internal static SMTA1WorkflowEmail[] SMTA1WorkflowEmails = new SMTA1WorkflowEmail[]
    {
        //Status: S1 => S2      
        new() // 10
        {
            Id = EmailForSubmitSMTA1ApprovedToLaboratoryITToolFocalPointId,
            FromStatus = SMTA1WorkflowStatus.SubmitSMTA1,
            ToStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of SMTA 1",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted the Standard Material Transfer Agreement (SMTA) 1 to the WHO BioHub System. You will receive a further notification as our review process advances.    </p>    <p>        Thank you for your interest in the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 10
        {
            Id = EmailForSubmitSMTA1ApprovedToWHOSecretariatId,
            FromStatus = SMTA1WorkflowStatus.SubmitSMTA1,
            ToStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of SMTA 1 Submission for Your Approval",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the SMTA 1 in the BioHub Operational Platform for your review and approval.<br>        To review and approve SMTA 1, you can find the submitted SMTA 1 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant SMTA request on the SMTA Requests page of your personal page</li>        </ul>    </p>    <p>       </p>    <p>        Sincerely,<br>        BioHub Operational Platform    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        //new()
        //{
        //    Id = EmailForSubmitSMTA1ApprovedToWHOOperationalFocalPointId,
        //    FromStatus = SMTA1WorkflowStatus.SubmitSMTA1,
        //    ToStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of SMTA 1 Submission for WHO BioHub Secretariat's Approval",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the SMTA 1 in the BioHub Operational IT Tool for the review and approval of the WHO BioHub Secretariat. Once the Secretariat has approved the STMA 2, you will be again notified.<br>        You can find the submitted SMTA 1 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},


        //Status: S2 => S3 (Approved)
        new() // 20
        {
            Id = EmailForWaitingForSMTA1SECsApprovalApprovedToWHOSecretariatId,
            FromStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
            ToStatus = SMTA1WorkflowStatus.SMTA1WorkflowComplete,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of SMTA 1",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have approved the SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the WHO BioHub System.<br>        You can find the approved SMTA 1 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        //new()
        //{
        //    Id = EmailForWaitingForSMTA1SECsApprovalApprovedToWHOOperationalFocalPointId,
        //    FromStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
        //    ToStatus = SMTA1WorkflowStatus.SMTA1WorkflowComplete,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of SMTA 1 Approval by BioHub Secretariat",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved SMTA 1 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},

        new() // 20
        {
            Id = EmailForWaitingForSMTA1SECsApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
            ToStatus = SMTA1WorkflowStatus.SMTA1WorkflowComplete,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of SMTA 1",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the SMTA 1.<br>        You can find the approved SMTA 1 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S2 => S1 (Rejected)
        new() // 15
        {
            Id = EmailForWaitingForSMTA1SECsApprovalRejectedToWHOSecretariatId,
            FromStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
            ToStatus = SMTA1WorkflowStatus.SubmitSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of SMTA 1 with Comment to BioHub System User",
            EmailBody = "<p>        BioHub Secretariat,    </p>    <p>        This is to confirm that you have returned with comment the SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification when the User resubmits SMTA 1.    </p>    <p>        Sincerely,<br>        BioHub Operational Platform    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        //new()
        //{
        //    Id = EmailForWaitingForSMTA1SECsApprovalRejectedToWHOOperationalFocalPointId,
        //    FromStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
        //    ToStatus = SMTA1WorkflowStatus.SubmitSMTA1,
        //    ApprovedSubmission = false,
        //    EmailSubject = "Confirmation E-mail of BioHub Secretariat's Returning of SMTA 1 with Comment to BioHub System User",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the SMTA 1 submitted by a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the User. You will receive a further notification if the User resubmits SMTA 1.    </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},

        new() // 15
        {
            Id = EmailForWaitingForSMTA1SECsApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
            ToStatus = SMTA1WorkflowStatus.SubmitSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Secretariat's Returning of SMTA 1 with Comment",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the SMTA 1.<br>        You can view the comment by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant SMTA request on the SMTA Requests page on your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

    };

    internal static Guid EmailForWaitingForSMTA1BHFsApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("2ae38f0b-0c61-4d23-871b-77759c3b4028");
    internal static Guid EmailForSubmitSMTA1ApprovedToLaboratoryITToolFocalPointId => Guid.Parse("9d03f0ae-5b90-4f17-9a22-b1ba6542a23e");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("b2c386f9-4254-409e-8ff1-5703e27d9b24");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("7e245b6d-f0d6-4111-acce-e3f7fb2d8e21");
    internal static Guid EmailForSubmitSMTA1ApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("2be66df9-0dfa-4e46-87be-e38153200205");
    internal static Guid EmailForWaitingForSMTA1BHFsApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("de22b772-169f-4ae8-ba5e-51d122e5e431");
    internal static Guid EmailForWaitingForSMTA1BHFsApprovalRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("047469a8-7fb6-42e5-abd6-23f7ec2752b9");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("50ccecab-a172-4045-9e2c-3aeda5b74b14");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("b7ed0c47-d8a7-43f1-9aaf-db3d3a4d3d2a");
    internal static Guid EmailForSubmitSMTA1ApprovedToWHOSecretariatId => Guid.Parse("7a4a2c55-f64f-43d8-bd7a-63088f11e545");
    internal static Guid EmailForWaitingForSMTA1BHFsApprovalApprovedToWHOSecretariatId => Guid.Parse("110c50b3-d20c-4af4-88b2-ed6c64bf6d93");
    internal static Guid EmailForWaitingForSMTA1BHFsApprovalRejectedToWHOSecretariatId => Guid.Parse("f59eb3b6-5ba4-41cb-83ba-52e4b7ed68b2");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalApprovedToWHOSecretariatId => Guid.Parse("2d3a1513-b879-4ac5-abdd-9dd421e1e101");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalRejectedToWHOSecretariatId => Guid.Parse("d56922aa-e7c7-4b0b-9f65-d978d473612d");
    internal static Guid EmailForSubmitSMTA1ApprovedToWHOOperationalFocalPointId => Guid.Parse("cd0d33ee-a2a1-4912-8d18-c909f22420aa");
    internal static Guid EmailForWaitingForSMTA1BHFsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("72c8d567-ced0-4e68-b307-bebdb1e0a9bc");
    internal static Guid EmailForWaitingForSMTA1BHFsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("2fed7dea-dd5c-4e65-9af2-7fd3dd6f3edc");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("10e63826-3e28-48fc-bbbb-bfe111bc4678");
    internal static Guid EmailForWaitingForSMTA1SECsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("5e919ef3-f20c-450e-8116-13d069abd38d");


    private async Task AddOrUpdateSMTA1WorkflowEmails(CancellationToken cancellationToken)
    {
        var rows = from o in _db.SMTA1WorkflowEmails
                   select o;

        foreach (var row in rows)
        {
            _db.SMTA1WorkflowEmails.Remove(row);
        }

        foreach (var SMTA1WorkflowEmail in SMTA1WorkflowEmails)
        {
            if (await _db.SMTA1WorkflowEmails.Where(x => x.Id == SMTA1WorkflowEmail.Id).AnyAsync(cancellationToken))
            {
                _db.Update(SMTA1WorkflowEmail);
            }
            else
            {
                await _db.AddAsync(SMTA1WorkflowEmail);
            }
        }
    }
}