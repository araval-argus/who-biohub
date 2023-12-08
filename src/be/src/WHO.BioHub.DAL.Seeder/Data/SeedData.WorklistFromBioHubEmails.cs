using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static WorklistFromBioHubEmail[] WorklistFromBioHubEmails = new WorklistFromBioHubEmail[]
    {       
        //Status: S3 => S4
        new() // 10
        {
            Id = EmailForSubmitAnnex2OfSMTA2ApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of Annex 2 of SMTA 2",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted the Annex 2 of SMTA 2 to the WHO BioHub System. You will receive a further notification as our review process advances.    </p>    <p>        Thank you for your interest in the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 10
        {
            Id = EmailForSubmitAnnex2OfSMTA2ApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of Annex 2 of SMTA 2 Submission for Your Approval",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Annex 2 of SMTA 2 in the BioHub Operational IT Tool for your review and approval.<br>        You can find the submitted Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page </li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 10
        {
            Id = EmailForSubmitAnnex2OfSMTA2ApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Annex 2 of SMTA 2 Submission for BioHub Secretariat's Approval",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Annex 2 of SMTA 2 in the BioHub Operational Platform for the review and approval of the BioHub Secretariat.<br>        You can find the submitted Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Opeartaional Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },
        

        //Status: S4 => S5 (Approved)
        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of Annex 2 of SMTA 2",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have approved the Annex 2 of SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the WHO BioHub System.<br>        You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Oplerational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 2",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the Annex 2 of SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 2",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the Annex 2 of SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat  </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 2",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the Annex 2 of SMTA 2 and you can further proceed with the shipment request process, where you can also find the Annex 2 of SMTA 2 signed by WHO, in the Operational Platform.<br>        You can proceed with the shipment request by either:        <ul>               <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Opeational Platform is not yet expired)</i></li>                     <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page </li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>       <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToWHOBiosafetyBiosecurityFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 2",
            EmailBody = "<p>        Dear BioHub Biosafety & Biosecurity Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the Annex 2 of SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br> You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page on your personal page</li>        </ul></p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId
        },
        
      
        //Status: S4 => S3 (Rejected)
        new() // 15
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalRejectedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of Annex 2 of SMTA 2 with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have returned with comment the Annex 2 of SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification when the User resubmits Annex 2.</p> <p>    Sincerely,<br>        BioHub Operational Platform</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },


        new() // 15
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Returning of Annex 2 of SMTA 2 with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned the Annex 2 of SMTA 2 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the returned Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>       WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 15
        {
            Id = EmailForWaitingForAnnex2OfSMTA2SECsApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Secretariat's Returning of Annex 2 of SMTA 2 with Comment",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the Annex 2 of SMTA 2. <br>        You can view the comment to resubmit Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        
        //Status: S5 => S6
        new() // 30
        {
            Id = EmailForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of QE Requirement (Biosafety & Biosecurity) Checklist Submission",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist in the Operational Platform for the review and approval of the BioHub Biosafety & Biosecurity Focal Point.<br>        You can find the submitted Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 30
        {
            Id = EmailForWaitingForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToWHOBiosafetyBiosecurityFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of QE Requirement (Biosafety & Biosecurity) Checklist Submission",
            EmailBody = "<p>        Dear BioHub Biosafety & Biosecurity Focal Point,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist in the BioHub Operational Platform for your review and approval.<br>        You can find the submitted Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>clicking the related item in the Worklist as part of the Dashboard on your personal page</li>                    <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId
        },

        new() // 30
        {
            Id = EmailForWaitingForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of QE Requirement (Biosafety & Biosecurity) Checklist Submission",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist in the BioHub Operational Platform for the review and approval of the BioHub Biosafety & Biosecurity Focal Point. <br>        You can find the submitted Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 30
        {
            Id = EmailForWaitingForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of QE Requirement Checklist (Biosafety & Biosecurity)",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist to the WHO BioHub System. You will receive a further notification as our review process advances.<br>        <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },
        

        //Status: S6 => S7 (Approved)
        new() // 40
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Biosafety and Biosecurity Focal Point's Approval of QE Requirements (Biosafety & Biosecurity) Checklist",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has approved the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Biosafety and Biosecurity Focal Point's Approval of QE Requirements (Biosafety & Biosecurity) Checklist",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has approved the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Biosafety and Biosecurity Focal Point's Approval of QE Requirements (Biosafety & Biosecurity) Checklist",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has approved the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Biosafety and Biosecurity Focal Point's Approval of QE Requirements (Biosafety & Biosecurity) Checklist",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has approved the Qualified Entity Requirements (Biosafety & Biosecurity). <br>        You can find the approved Checklist by either:        <ul>               <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your session in the Operational Platform is not yet expired)</i></li>                     <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        You will receive a further notification as the shipment process advances.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToWHOBiosafetyBiosecurityFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of QE Requirements (Biosafety & Biosecurity) Checklist",
            EmailBody = "<p>        Dear BioHub Biosafety & Biosecurity Focal Point,    </p>    <p>        This is to confirm that you have approved the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@. Upon this approval, the shipment request process will further proceed.<br>        You can find the approved Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat  </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId
        },
        

        //Status: S6 => S5 (Rejected)
        new() // 35
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Biosafety & Biosecurity Focal Point's Returning of QE Requirements (Biosafety & Biosecurity) Checklist with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has returned with comment the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification when the User resubmits the Checklist.</p>    <p>        Sincerely,<br>        BioHub Operational Platform   </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 35
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Biosafety & Biosecurity Focal Point's Returning of QE Requirements (Biosafety & Biosecurity) Checklist with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has returned with comment the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification when the User resubmits the Checklist.</p>    <p>        Sincerely,<br>       WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 35
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of Biosafety & Biosecurity Focal Point's Returning of QE Requirements (Biosafety & Biosecurity) Checklist with Comment",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Biosafety & Biosecurity Focal Point has returned with comment the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist.  <br>        You can view the comment to resubmit the Checklist by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 35
        {
            Id = EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToWHOBiosafetyBiosecurityFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of QE Requirements (Biosafety & Biosecurity) Checklist with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Biosafety & Biosecurity Focal Point,    </p>    <p>        This is to confirm that you have returned with comment the Qualified Entity Requirements (Biosafety & Biosecurity) Checklist submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the User.<br>        You will receive a further notification when the User resubmits the Checklist.            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId
        },
        

        //Status: S7 => S8
        new() // 50
        {
            Id = EmailForSubmitBookingFormOfSMTA2ApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of Booking Form",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that you have successfully submitted a Booking Form(s) to the WHO BioHub System. You will receive a further notification as our review process advances.</p><p>       Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 50
        {
            Id = EmailForSubmitBookingFormOfSMTA2ApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Facility's Submission of Booking Form for Your Approval",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Facility, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, has submitted the Booking Form(s) for the shipment requested by @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ in the BioHub Operational IT Tool for your review and approval.<br>        You can find the submitted Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat  </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 50
        {
            Id = EmailForSubmitBookingFormOfSMTA2ApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ToStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Facility's Submission of Booking Form",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Facility, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, has submitted a Booking Form(s) for the shipment requested by @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ in the BioHub Operational Platform for the review and approval of the BioHub Operational Focal Point.<br>        You can find the submitted Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        

        //Status: S8 => S11 (Approved)
        new() // 60
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of Booking Form",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have approved the Booking Form(s) submitted by the BioHub Facility, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ for the shipment requested by @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>      WHO BioHub Secretariat   </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 60
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has approved the Booking Form(s). <br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>     You will receive a further notification as the shipment process advances.    </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 60
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has approved the Booking Form(s) submitted by the BioHub Facility colleague, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ for the shipment requested by @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>You will receive a further notification as the shipment process advances.</p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 60
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToWHOOperationalFocalPointCourierId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required, WHO BioHub System] Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form and Starting Shipment Coordination ",
            
            //# 54317
            //EmailBody = "<p>        Dear Colleague,    </p>    <p>        This is to confirm by the WHO BioHub Secretariat that you can start coordination of a shipment requested by @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ upon approval of the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@, on their submitted Booking Form(s). This shipment is from BioHub Facility @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, to the requester's facility.<br>        To proceed with the shipment coordination, all the detailed information is shown below<br>        Please stay in touch with the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@ (<a href='mailto:@@@WHOOperationalFocalPointEmail@@@'>@@@WHOOperationalFocalPointEmail@@@</a>), while the shipment process advances.            </p>    <p>        <h3>WHO Booking Form(s) for Shipment of Biological Materials with Epidemic or Pandemic Potential to a WHO BioHub Facility</h3>        <h4>1. Pick-up and delivery addresses and contact persons</h4>        <p><strong>Information from the sending laboratory</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Request Date:</strong></td>                    <td>@@@RequestDate@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>From:</strong></td>                    <td>@@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Name:</strong></td>                    <td>@@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Mobile:</strong></td>                    <td>@@@BioHubFacilityUserMobilePhone@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Business (landline):</strong></td>                    <td>@@@BioHubFacilityUserLandline@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Email:</strong></td>                    <td><a href='mailto:@@@BioHubFacilityUserEmail@@@'>@@@BioHubFacilityUserEmail@@@</a></td>                </tr>            </table>        </p>        <p><strong>Requested date of pick-up: </strong>@@@RequestDateOfPickUp@@@</p>        <p>            <strong>Person(s) to be contacted for the pick-up</strong><br>            <ul>                @@@PickupUsers@@@            </ul>        </p>                <p><strong>Place of pick-up</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubFacilityName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubFacilityAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubFacilityCountryName@@@</td>                </tr>            </table>        </p>				<p><strong>Place of delivery</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubUserInstituteName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubUserInstituteAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubUserCountryName@@@</td>                </tr>                            </table>        </p>        <p>            <strong>Person(s) to be contacted for the delivery</strong><br>            <ul>                @@@DeliveryUsers@@@            </ul>        </p>		        <h4>2. Details of shipment (WHO Account: @@@WHOAccountNumber@@@)</h4>        <p><strong>Substance Category</strong></p>        <p>@@@SubstanceCategory@@@</p>        <p><strong>Temperature Transport Conditions</strong></p>        <p>@@@TemperatureTransportConditions@@@</p>        <p><strong>Number of vials</strong></p>        <p>@@@NumberOfVials@@@</p> <p><strong>Total Amount (in ml)</strong></p>        <p>@@@TotalAmount@@@</p>         <p><strong>Number of inner packaging and size (if available)</strong></p>        <p>@@@NumberOfInnerPackagingAndSize@@@</p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Name:</strong></td>                    <td>@@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Function:</strong></td>                    <td>@@@BioHubFacilityUserJobTitle@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Signature:</strong></td>                    <td><img src='data:image/jpeg;base64,@@@BioHubFacilityUserSignature@@@' width='500px'></td>                </tr>            </table>        </p>    </p>    <p>        We look forward to continuing to work with you through this shipment process.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            EmailBody = "<p>        Dear Courier Team,    </p>    <p>        This is to confirm by the WHO BioHub Secretariat that you can start coordination of a shipment requested by @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ upon the Booking Form approval of the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@. This shipment is from BioHub Facility @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, to the requester's facility.<br>        To proceed with the shipment coordination, all the detailed information is shown below<br>        Please stay in touch with the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@ (<a href='mailto:@@@WHOOperationalFocalPointEmail@@@'>@@@WHOOperationalFocalPointEmail@@@</a>), while the shipment process advances.            </p>    <p>        <h3>WHO Booking Form(s) for Shipment of Biological Materials with Epidemic or Pandemic Potential to a WHO BioHub Facility</h3>        <h4>1. Pick-up and delivery addresses and contact persons</h4>        <p><strong>Information from the sending laboratory</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Request Date:</strong></td>                    <td>@@@RequestDate@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>From:</strong></td>                    <td>@@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Name:</strong></td>                    <td>@@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Mobile:</strong></td>                    <td>@@@BioHubFacilityUserMobilePhone@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Business (landline):</strong></td>                    <td>@@@BioHubFacilityUserLandline@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Email:</strong></td>                    <td><a href='mailto:@@@BioHubFacilityUserEmail@@@'>@@@BioHubFacilityUserEmail@@@</a></td>                </tr>            </table>        </p>        <p><strong>Requested date of pick-up: </strong>@@@RequestDateOfPickUp@@@</p>        <p>            <strong>Person(s) to be contacted for the pick-up</strong><br>            <ul>                @@@PickupUsers@@@            </ul>        </p>                <p><strong>Place of pick-up</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubFacilityName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubFacilityAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubFacilityCountryName@@@</td>                </tr>            </table>        </p>Sincerely,<br>      WHO BioHub Secretariat",
            //////////////////////
            
            RoleId = WHOOperationalFocalPointRoleId,
            IsCourier = true
        },

        new() // 60
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form",
            EmailBody = "<p>        BioHub System User,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has approved the Booking Form(s) submitted by the BioHub Facility @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, for initiating the shipment coordination.<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>You will receive a further notification as the shipment process advances.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        
        //Status: S8 => S7 (Rejected)
        new() // 55
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalRejectedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Returning of Booking Form with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has returned with comment the Booking Form(s) submitted by the BioHub Facility, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ to the BioHub Facility. You will receive a further notification when the BioHub Facility resubmits the Booking Form(s).    </p>    <p>        Sincerely,<br>        BioHub Operational Platform  </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 55
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of Booking Form with Comment to BioHub Facility",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have returned with comment the Booking Form(s) submitted by the BioHub Facility, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ to the BioHub Facility. You will receive a further notification when the BioHub Facility resubmits the Booking Form(s).    </p>    <p>        Sincerely,<br>       WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 55
        {
            Id = EmailForWaitForBookingFormSMTA2OPSsApprovalRejectedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
            ToStatus = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Operatioanl Focal Point's Returning of Booking Form with Comment",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has returned with comment the Booking Form(s). <br>        You can view the comment to resubmit the Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },


        //Status: S9 => S11
        //new()
        //{
        //    Id = EmailForSubmitBHFSMTA2ShipmentDocumentsApprovedToBioHubFacilityITToolFocalPointId,
        //    FromStatus = WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments,
        //    ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission",
        //    EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that you have successfully submitted a shipment-related document(s) to the WHO BioHub System. You will receive a further notification as shipment process advances.            </p>    <p>        List of submitted documents:        <ul>            @@@ShipmentRelatedDocumentItem@@@        </ul>    </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = BioHubFacilityITToolFocalPointRoleId
        //},
        //new()
        //{
        //    Id = EmailForSubmitBHFSMTA2ShipmentDocumentsApprovedToWHOOperationalFocalPointId,
        //    FromStatus = WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments,
        //    ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission for Your Approval",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that a BioHub Facility colleague, @@@BioHubFacilityUserFirstName@@@ @@@BioHubFacilityUserLastName@@@ at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, has submitted a Shipment-related Document(s) in the BioHub Operational IT Tool for the shipment requested by @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.    </p>    <p>        List of submitted documents:        <ul>            @@@ShipmentRelatedDocumentItem@@@        </ul>    </p>    <p>                You can find the submitted Document(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the related item in Worklist as part of the Dashboard in your personal page within the IT Tool</li>            <li>Clicking the relevant shipment request on the Shipment Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},

        //new()
        //{
        //    Id = EmailForWaitForPickUpCompletedRejectedToLaboratoryITToolFocalPointId,
        //    FromStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
        //    ToStatus = WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission",
        //    EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to inform you that the BioHub Operational Focal Point has requested you to add shipment-related document(s) to the WHO BioHub System (if any).<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Overview page on your personal page</li>        </ul>            </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = LaboratoryITToolFocalPointRoleId
        //},
        //new()
        //{
        //    Id = EmailForWaitForPickUpCompletedRejectedToWHOOperationalFocalPoinId,
        //    FromStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
        //    ToStatus = WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission for Your Approval",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that your request to add shipment-related document(s) to the WHO BioHub System was sent to @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.    </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},   
        
         
        
        //Status: S10 => S11
        //new()
        //{
        //    Id = EmailForSubmitQESMTA2ShipmentDocumentsApprovedToLaboratoryITToolFocalPointId,
        //    FromStatus = WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
        //    ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission",
        //    EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted a shipment-related document(s) to the WHO BioHub System. You will receive a further notification as shipment process advances.            </p>    <p>        List of submitted documents:        <ul>            @@@ShipmentRelatedDocumentItem@@@        </ul>    </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = LaboratoryITToolFocalPointRoleId
        //},
        //new()
        //{
        //    Id = EmailForSubmitQESMTA2ShipmentDocumentsApprovedToWHOOperationalFocalPointId,
        //    FromStatus = WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
        //    ToStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission for Your Approval",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted a Shipment-related Document(s) in the BioHub Operational IT Tool for your review and approval.    </p>    <p>        List of submitted documents:        <ul>            @@@ShipmentRelatedDocumentItem@@@        </ul>    </p>    <p>                You can find the submitted Document(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the related item in Worklist as part of the Dashboard in your personal page within the IT Tool</li>            <li>Clicking the relevant shipment request on the Shipment Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},   

        
        //Status: S11 => S12 [Pick-up completed]
        new() // 70
        {
            Id = EmailForWaitForSMTA2PickUpCompletedApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Reporting of Courier's Pick-up of BMEPP at BioHub Facility",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have successfully reported that the courier has picked up the BMEPP at the BioHub Facility @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, @@@BioHubUserInstituteName@@@, to deliver to the BioHub System User's facility (QE), @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.    </p>    <p>        Pickup details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndPickupDate@@@        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 70
        {
            Id = EmailForWaitForSMTA2PickUpCompletedApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Pick-up of BMEPP(s) at BioHub Facility",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the courier has picked up the BMEPP at @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, to deliver to your facility. Upon the BMEPP's arrival at your facility, you will be directly contacted by the courier.    </p>    <p>        Pickup details:        <ul>            @@@CategoryItemWithPickupDate@@@        </ul></p>    <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 70
        {
            Id = EmailForWaitForSMTA2PickUpCompletedApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Pick-up of BMEPP(s) at BioHub Facility",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the courier has picked up the BMEPP at the BioHub Facility @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, to deliver to the BioHub System User's facility (QE), @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.    </p>    <p>        Pickup details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndPickupDate@@@        </ul>    </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },


        new()
        {
            Id = EmailForNumberOfVialsWarningToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Current Number of Vials Warning",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to warn you that the residual number of vials is under the threshold for the following materials: </p>    <p>        <ul>            @@@WarningMaterials@@@        </ul>    </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId,
            IsNumberOfVialsWarning = true
        },

        
        //Status: S12 => S13 [Delivery completed]
        new() // 80
        {
            Id = EmailForWaitForSMTA2DeliveryCompletedApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Reporting of Courier's Delivery Completion of BMEPP to BioHub System User's Facility (QE)",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have successfully reported the courier has delivered the BMEPP to the BioHub System User's facility (QE), @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@. You will be then notified with the arrival condition and comment, if necessary, from the facility.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndDeliveryDate@@@        </ul>    </p>    <p>        Sincerely,<br>       WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 80
        {
            Id = EmailForWaitForSMTA2DeliveryCompletedApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of Courier's Delivery Completion of BMEPP to Your Facility for Your Reporting of Arrival Condition and Confirmation of Shipment Completion",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the courier has delivered the BMEPP to your facility for you to report the arrival condition of the BMEPP and confirm the shipment process completion.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithDeliveryDate@@@        </ul>    <br>        You can do so by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page </li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>  </p>    <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 80
        {
            Id = EmailForWaitForSMTA2DeliveryCompletedApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Delivery Completion of BMEPP to BioHub System User's Facility (QE)",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the courier has delivered the BMEPP to the BioHub System User's facility (QE), @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@. You will be then notified with the arrival condition and comment, if necessary, from the facility.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndDeliveryDate@@@        </ul>    </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 80
        {
            Id = EmailForWaitForSMTA2DeliveryCompletedApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Delivery of BMEPP to BioHub System User's Facility (QE)",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the courier has delivered the BMEPP to the BioHub System User's facility (QE), @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@. You will be then notified with the arrival condition and comment, if necessary, from the facility.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndDeliveryDate@@@        </ul>    </p>    <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        
        //Status: S13 => S16 (Approved)
        new() // 110
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the entire shipment process for the BMEPP to the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has been completed </p>    <p>  To check the arrival condition of the BMEPP at the BioHub System User's facility (QE), please click this <a href='@@@url@@@'>link</a>.</p><p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 110
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the entire shipment process for the BMEPP to the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has been completed </p>    <p>  To check the arrival condition of the BMEPP at the BioHub System User's facility (QE), please click this <a href='@@@url@@@'>link</a>.</p><p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 110
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the entire shipment process for the BMEPP to the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has been completed </p>    <p>  To check the arrival condition of the BMEPP at the BioHub System User's facility (QE), please click this <a href='@@@url@@@'>link</a>.</p><p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 110
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have confirmed that the entire shipment process for the BMEPP received from the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, has been completed and reported the BMEPP arrival condition to the BioHub Facility. To check the arrival condition of the BMEPP with comment at the BioHub Facility, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S13 => S14 (Rejected)
        new() // 85
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckRejectedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub System User (QE)'s Comment about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, sent comment on the arrival condition of the BMEPP you shipped. Please respond to the comment following the guidance below.</p><p><ul>       You can view the comment to respond by either:             <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>clicking the related item in the Worklist as part of the Dashboard on your personal page</li>                    <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li></ul></p>    <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 85
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckRejectedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub System User (QE)'s Sending of Comment to BioHub Facility about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub System User (QE), @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, sent comment on the arrival condition of BMEPP shipped from the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ to the User. You will receive a further notification when the User responds to it.<br> You can view the comment by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>   </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 85
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub System User (QE)'s Sending of Comment to BioHub Facility about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub System User (QE), @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, sent comment on the arrival condition of BMEPP shipped from the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ to the User. You will receive a further notification when the User responds to it.<br> You can view the comment by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>   </p>    <p>        Sincerely,<br>       WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 85
        {
            Id = EmailForWaitForSMTA2ArrivalConditionCheckRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Sending of Comment to BioHub Facility about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that your comment on the arrival condition of the BMEPP shipped from the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@, was sent to the Facility. You will receive a further notification when the Facility responds to it.</p>    <br>        You can view the comment by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        
        //Status: S14 => S15 (Comment)      
        new() // 90
        {
            Id = EmailForWaitForCommentQESendFeedbackApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ToStatus = WorklistFromBioHubStatus.WaitForFinalApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Comment and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ have reported about the comment that you sent related to the Completion of Shipment Process.        <h4>Comments</h4>        @@@Feedbacks@@@    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 90
        {
            Id = EmailForWaitForCommentQESendFeedbackApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ToStatus = WorklistFromBioHubStatus.WaitForFinalApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of Your Receiving of BioHub Facility's Comment about Arrival Condition of BMEPP for Your Confirmation of Shipment Completion",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have received a comment from the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ about the arrival condition of the BMEPP received from the Facility.   To view the comment and confirm the shipment completion in the Operational Platform, please click this <a href='@@@url@@@'>link</a>.</p>    <p>Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        
        //Status: S15 => S14 (Rejected)
        new() // 95
        {
            Id = EmailForWaitForSMTA2FinalApprovalRejectedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Feedback and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ have reported some feedback related to the Completion of Shipment Process.        <h4>Feedback</h4>        @@@Feedbacks@@@    </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 95
        {
            Id = EmailForWaitForSMTA2FinalApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Feedback and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have sent feedback to @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ related to the Completion of Shipment Process.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        //Status: S15 => S16 (Completed)
        new() // 100
        {
            Id = EmailForWaitForSMTA2FinalApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistFromBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistFromBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Completion of Shipment Process",
            EmailBody = "<p>        Dear all,    </p>    <p>        This is to confirm that the shipment process has been completed.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 100
        {
            Id = EmailForWaitForSMTA2FinalApprovalApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistFromBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistFromBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Comment and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the shipment process has been completed.    </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

    };

    internal static Guid EmailForSubmitAnnex2OfSMTA2ApprovedToWHOSecretariatId => Guid.Parse("3b32ae93-7e58-4cba-b5c0-42d05965e9ab");
    internal static Guid EmailForSubmitAnnex2OfSMTA2ApprovedToLaboratoryITToolFocalPointId => Guid.Parse("6b343715-93be-48c4-ab9e-43b126c7bfde");
    internal static Guid EmailForSubmitAnnex2OfSMTA2ApprovedToWHOOperationalFocalPointId => Guid.Parse("75f21aac-480a-4291-ac71-fe1389e796f2");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToWHOSecretariatId => Guid.Parse("ea338878-7ee0-45f8-9586-b11c9b68f1e4");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("06552e72-b0a3-4337-92ed-b19d8df0666e");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("4f31f1f0-6894-4cdf-9e95-c094e313e6bc");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToWHOBiosafetyBiosecurityFocalPointId => Guid.Parse("1779c30e-8678-4bae-a0be-233807d48eb2");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("45072879-7bf9-4804-ad65-f97d02749961");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("97afe922-5418-48c5-87c3-e63272ca70e1");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("ba049251-d257-4e4c-9ac8-0a3c6f606cbb");
    internal static Guid EmailForWaitingForAnnex2OfSMTA2SECsApprovalRejectedToWHOSecretariatId => Guid.Parse("5efa9f72-abbe-4350-bc67-a4b572cbb713");
    internal static Guid EmailForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToWHOSecretariatId => Guid.Parse("06d390a8-9909-4e65-9457-eea88aef7997");
    internal static Guid EmailForWaitingForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToWHOBiosafetyBiosecurityFocalPointId => Guid.Parse("ae43a802-7fc8-4cd7-a043-22a284895ee1");
    internal static Guid EmailForWaitingForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToWHOOperationalFocalPointId => Guid.Parse("8fd372a9-0272-48c5-bef9-57650721e23a");
    internal static Guid EmailForWaitingForSubmitBiosafetyChecklistFormOfSMTA2ApprovedToLaboratoryITToolFocalPointId => Guid.Parse("7820524e-b252-4c3f-9ce7-8ae21d863822");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToWHOSecretariatId => Guid.Parse("05b58ded-9d71-4812-8956-d0487eea1eac");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToWHOBiosafetyBiosecurityFocalPointId => Guid.Parse("800da966-a621-407d-9f34-f5f210834a12");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("a5114d45-9135-4e0d-9210-381bddee7693");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("66d35fde-6152-40e2-a438-69f67398e942");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("9081be0e-3b85-4f7e-9639-1c3f988c7f1f");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("eeef889a-7466-471e-831b-463b28bcfa01");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("4f443839-c0a1-4b10-b9f0-211c7f9eb91b");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToWHOSecretariatId => Guid.Parse("06f33b9e-6b28-40a8-83b5-c9e46e687af5");
    internal static Guid EmailForWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedToWHOBiosafetyBiosecurityFocalPointId => Guid.Parse("a72ee308-e0ac-4d74-8874-b2a64ab08f52");
    internal static Guid EmailForSubmitBookingFormOfSMTA2ApprovedToWHOOperationalFocalPointId => Guid.Parse("4ad670ad-d463-42c4-b315-6bf5aecdf4cb");
    internal static Guid EmailForSubmitBookingFormOfSMTA2ApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("7e9bdfe1-88b4-423f-aa45-8174fef67a67");
    internal static Guid EmailForSubmitBookingFormOfSMTA2ApprovedToWHOSecretariatId => Guid.Parse("2f65c687-d331-40c1-ba91-fda007e6d1f0");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("07fbb320-64be-4bac-bf48-a0fd0e596e80");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("a623657f-5ea7-453a-842c-a3e6794fcb33");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToWHOSecretariatId => Guid.Parse("36313bc4-cf8a-44dd-a232-ce628b9cf300");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("88ed881c-ce9c-4c90-b58a-e145f6f5b40e");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("3ababd37-dfae-4d1f-8d6d-b4b77bf0e213");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("9b55c64d-694d-4471-ba04-9e462120d0eb");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalRejectedToWHOSecretariatId => Guid.Parse("eedc8b9c-18a6-4744-b3c1-1ddfa03b91e0");
    internal static Guid EmailForSubmitBHFSMTA2ShipmentDocumentsApprovedToWHOOperationalFocalPointId => Guid.Parse("0376a6b2-7cd5-44d4-850f-6b7bbf5652d7");
    internal static Guid EmailForSubmitBHFSMTA2ShipmentDocumentsApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("b6b4a1ad-332a-4f89-9d7e-8c909e51b0ed");
    internal static Guid EmailForSubmitQESMTA2ShipmentDocumentsApprovedToWHOOperationalFocalPointId => Guid.Parse("33624b8b-89ab-4f0d-afff-0f631fcecabe");
    internal static Guid EmailForSubmitQESMTA2ShipmentDocumentsApprovedToLaboratoryITToolFocalPointId => Guid.Parse("d1aedd15-deb0-4178-abd6-9d91852b1403");
    internal static Guid EmailForWaitForCommentQESendFeedbackApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("7d109dfd-33b9-4a7b-abcb-16c5a959c14f");
    internal static Guid EmailForWaitForCommentQESendFeedbackApprovedToLaboratoryITToolFocalPointId => Guid.Parse("6564fd63-4cbd-43b6-b18b-9a9ff08d44c4");
    internal static Guid EmailForWaitForBookingFormSMTA2OPSsApprovalApprovedToWHOOperationalFocalPointCourierId => Guid.Parse("2fc5f0df-3d6f-409a-a295-c7dfce85fd00");
    internal static Guid EmailForWaitForSMTA2PickUpCompletedApprovedToLaboratoryITToolFocalPointId => Guid.Parse("18f95c0e-313f-4793-ae30-7b3443e631ce");
    internal static Guid EmailForWaitForSMTA2PickUpCompletedApprovedToWHOSecretariatId => Guid.Parse("82bc7fe8-3bab-480e-84bc-0c925edc135b");
    internal static Guid EmailForWaitForSMTA2PickUpCompletedApprovedToWHOOperationalFocalPointId => Guid.Parse("e04c0650-4095-49d0-88f8-8955393ce483");
    internal static Guid EmailForWaitForSMTA2DeliveryCompletedApprovedToLaboratoryITToolFocalPointId => Guid.Parse("6c679482-e4a2-4eac-b32b-4d55be1d56f1");
    internal static Guid EmailForWaitForSMTA2DeliveryCompletedApprovedToWHOSecretariatId => Guid.Parse("34df6c2c-9cf0-401a-ad19-f9aaaa635184");
    internal static Guid EmailForWaitForSMTA2DeliveryCompletedApprovedToWHOOperationalFocalPointId => Guid.Parse("3316366b-06fe-498c-8a3e-f6dcb40f4cba");
    internal static Guid EmailForWaitForSMTA2DeliveryCompletedApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("498624bf-1697-4ca4-8fd0-84c5a621d37e");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckApprovedToLaboratoryITToolFocalPointId => Guid.Parse("2ac297ec-436d-420c-873c-f41abfd7bbcb");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("0c0d56b4-5438-48e8-b8b1-1e93b1d81909");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckApprovedToWHOSecretariatId => Guid.Parse("fd92773f-802b-44fe-963d-8d0a8d668b65");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckApprovedToWHOOperationalFocalPointId => Guid.Parse("066ba9ad-c1d5-4908-91b6-90d3222762cf");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("d3d3b8da-703c-4da5-90eb-6f3b2b7aec4d");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckRejectedToLaboratoryITToolFocalPointId => Guid.Parse("39cc6ab5-bd2d-486c-af72-03b5419774bf");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckRejectedToWHOSecretariatId => Guid.Parse("b1b18312-72e7-40ad-9227-472d98c2485f");
    internal static Guid EmailForWaitForSMTA2ArrivalConditionCheckRejectedToWHOOperationalFocalPointId => Guid.Parse("09fb2b8b-3c78-4ca6-b838-9c6694139d6d");
    internal static Guid EmailForWaitForSMTA2FinalApprovalRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("c769cdde-cbf0-4232-8bf0-0a9da75d8b7c");
    internal static Guid EmailForWaitForSMTA2FinalApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("7f847b03-d752-4966-8ce1-c7c7627b1665");
    internal static Guid EmailForWaitForSMTA2FinalApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("42fef979-f252-46de-947e-4c6f660a6060");
    internal static Guid EmailForWaitForSMTA2FinalApprovalApprovedToWHOSecretariatId => Guid.Parse("6d5e4cbf-cee4-45f8-b9cf-b8fc0fdf67c5");
    internal static Guid EmailForWaitForPickUpCompletedRejectedToLaboratoryITToolFocalPointId => Guid.Parse("cc9f6c78-b879-4111-9010-c2a911756bd1");
    internal static Guid EmailForWaitForPickUpCompletedRejectedToWHOOperationalFocalPoinId => Guid.Parse("14757e56-6a42-4822-85ff-5996bb435fa2");
    internal static Guid EmailForWaitForPickUpCompletedRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("1d7556d8-738b-46f1-8fb6-5168fe88071d");
    internal static Guid EmailForNumberOfVialsWarningToBioHubFacilityITToolFocalPointId => Guid.Parse("34271af0-445e-4e6b-8a03-39108fec32a2");

    private async Task AddOrUpdateWorklistFromBioHubEmails(CancellationToken cancellationToken)
    {
        var rows = from o in _db.WorklistFromBioHubEmails
                   select o;

        foreach (var row in rows)
        {
            _db.WorklistFromBioHubEmails.Remove(row);
        }

        foreach (var worklistFromBioHubEmail in WorklistFromBioHubEmails)
        {
            if (await _db.WorklistFromBioHubEmails.Where(x => x.Id == worklistFromBioHubEmail.Id).AnyAsync(cancellationToken))
            {
                _db.Update(worklistFromBioHubEmail);
            }
            else
            {
                await _db.AddAsync(worklistFromBioHubEmail);
            }
        }
    }
}