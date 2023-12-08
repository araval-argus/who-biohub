using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static SMTA2WorkflowEmail[] SMTA2WorkflowEmails = new SMTA2WorkflowEmail[]
    {
        //Status: S1 => S2      
        new() // 10
        {
            Id = EmailForSubmitSMTA2ApprovedToLaboratoryITToolFocalPointId,
            FromStatus = SMTA2WorkflowStatus.SubmitSMTA2,
            ToStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of SMTA 2",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted the Standard Materials Transfer Agreement (SMTA) 2 to the WHO BioHub System. You will receive a further notification as our review process advances.    </p>    <p>        Thank you for your interest in the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 10
        {
            Id = EmailForSubmitSMTA2ApprovedToWHOSecretariatId,
            FromStatus = SMTA2WorkflowStatus.SubmitSMTA2,
            ToStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of SMTA 2 Submission for Your Approval",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the SMTA 2 in the BioHub Operational Platform for your review and approval.<br>        To review and approve SMTA 2, you can find the submitted SMTA 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant SMTA request on the SMTA Requests page of your personal page</li>        </ul>    </p>    <p>       </p>    <p>        Sincerely,<br>        BioHub Operatioanl Platform    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        //new()
        //{
        //    Id = EmailForSubmitSMTA2ApprovedToWHOOperationalFocalPointId,
        //    FromStatus = SMTA2WorkflowStatus.SubmitSMTA2,
        //    ToStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of SMTA 2 Submission for WHO BioHub Secretariat's Approval",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the SMTA 2 in the BioHub Operational IT Tool for the review and approval of the WHO BioHub Secretariat. Once the Secretariat has approved the STMA 2, you will be again notified.<br>        You can find the submitted SMTA 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},
                

        //Status: S2 => S3 (Approved)
        new() // 20
        {
            Id = EmailForWaitingForSMTA2SECsApprovalApprovedToWHOSecretariatId,
            FromStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
            ToStatus = SMTA2WorkflowStatus.SMTA2WorkflowComplete,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of SMTA 2",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have approved the SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the WHO BioHub System.<br>        You can find the approved SMTA 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        //new()
        //{
        //    Id = EmailForWaitingForSMTA2SECsApprovalApprovedToWHOOperationalFocalPointId,
        //    FromStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
        //    ToStatus = SMTA2WorkflowStatus.SMTA2WorkflowComplete,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of SMTA 2 Approval by BioHub Secretariat",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved SMTA 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},

        new() // 20
        {
            Id = EmailForWaitingForSMTA2SECsApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
            ToStatus = SMTA2WorkflowStatus.SMTA2WorkflowComplete,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of SMTA 2",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the SMTA 2.<br>        You can find the approved SMTA 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant SMTA request on the SMTA Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },
              
        //Status: S2 => S1 (Rejected)
        new() // 15
        {
            Id = EmailForWaitingForSMTA2SECsApprovalRejectedToWHOSecretariatId,
            FromStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
            ToStatus = SMTA2WorkflowStatus.SubmitSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of SMTA 1 with Comment to BioHub System User",
            EmailBody = "<p> Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have returned with comment the SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification if the User resubmits SMTA 2.    </p>    <p>        Sincerely,<br>        BioHub Operational Platform    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        //new()
        //{
        //    Id = EmailForWaitingForSMTA2SECsApprovalRejectedToWHOOperationalFocalPointId,
        //    FromStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
        //    ToStatus = SMTA2WorkflowStatus.SubmitSMTA2,
        //    ApprovedSubmission = false,
        //    EmailSubject = "Confirmation E-mail of BioHub Secretariat's Returning of SMTA 2 with Comment to BioHub System User",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the SMTA 2 submitted by a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the User. You will receive a further notification if the User resubmits SMTA 2.    </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},

        new() // 15
        {
            Id = EmailForWaitingForSMTA2SECsApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
            ToStatus = SMTA2WorkflowStatus.SubmitSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Secretariat's Returning of SMTA 1 with Comment",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the SMTA 2.<br>        You can view the comment by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant SMTA request on the SMTA Requests page on your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

    };

    internal static Guid EmailForSubmitSMTA2ApprovedToLaboratoryITToolFocalPointId => Guid.Parse("9d03f0ae-5b90-4f17-9a22-b1ba6542a23e");
    internal static Guid EmailForWaitingForSMTA2SECsApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("b2c386f9-4254-409e-8ff1-5703e27d9b24");
    internal static Guid EmailForWaitingForSMTA2SECsApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("7e245b6d-f0d6-4111-acce-e3f7fb2d8e21");
    internal static Guid EmailForSubmitSMTA2ApprovedToWHOSecretariatId => Guid.Parse("7a4a2c55-f64f-43d8-bd7a-63088f11e545");
    internal static Guid EmailForWaitingForSMTA2SECsApprovalApprovedToWHOSecretariatId => Guid.Parse("2d3a1513-b879-4ac5-abdd-9dd421e1e101");
    internal static Guid EmailForWaitingForSMTA2SECsApprovalRejectedToWHOSecretariatId => Guid.Parse("d56922aa-e7c7-4b0b-9f65-d978d473612d");
    internal static Guid EmailForSubmitSMTA2ApprovedToWHOOperationalFocalPointId => Guid.Parse("cd0d33ee-a2a1-4912-8d18-c909f22420aa");
    internal static Guid EmailForWaitingForSMTA2SECsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("10e63826-3e28-48fc-bbbb-bfe111bc4678");
    internal static Guid EmailForWaitingForSMTA2SECsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("5e919ef3-f20c-450e-8116-13d069abd38d");


    private async Task AddOrUpdateSMTA2WorkflowEmails(CancellationToken cancellationToken)
    {
        var rows = from o in _db.SMTA2WorkflowEmails
                   select o;

        foreach (var row in rows)
        {
            _db.SMTA2WorkflowEmails.Remove(row);
        }

        foreach (var SMTA2WorkflowEmail in SMTA2WorkflowEmails)
        {
            if (await _db.SMTA2WorkflowEmails.Where(x => x.Id == SMTA2WorkflowEmail.Id).AnyAsync(cancellationToken))
            {
                _db.Update(SMTA2WorkflowEmail);
            }
            else
            {
                await _db.AddAsync(SMTA2WorkflowEmail);
            }
        }
    }
}