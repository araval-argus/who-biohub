using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static WorklistToBioHubEmail[] WorklistToBioHubEmails = new WorklistToBioHubEmail[]
    {
        
        //Status: S4 => S5
        new() // 10
        {
            Id = EmailForSubmitAnnex2OfSMTA1ApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
            ToStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of Annex 2 of SMTA 1",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted the Annex 2 of SMTA 1 to the WHO BioHub System. You will receive a further notification as our review process advances.    </p>    <p>        Thank you for your interest in the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 10
        {
            Id = EmailForSubmitAnnex2OfSMTA1ApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
            ToStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of Annex 2 of SMTA 1 Submission for Your Approval",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Annex 2 of SMTA 1 in the BioHub Operational Platform for your review and approval.<br>        You can find the submitted Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platoform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 10
        {
            Id = EmailForSubmitAnnex2OfSMTA1ApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
            ToStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Annex 2 of SMTA 1 Submission for BioHub Secretariat's Approval",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted the Annex 2 of SMTA 1 in the BioHub Operational Platform for the review and approval of the BioHub Secretariat.<br>        You can find the submitted Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },


        //Status: S5 => S6 (Approved)
        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of Annex 2 of SMTA 1",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have approved the Annex 2 of SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the WHO BioHub System.<br>        You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 1",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved Annex 2 of SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Opearational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 1",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the Annex 2 of SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 20
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Secretariat's Approval of Annex 2 of SMTA 1",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has approved the Annex 2 of SMTA 1 and you can further proceed with the shipment request process, where you can also find the Annex 2 of SMTA 1 signed by WHO, in the Operational Platform.<br>        You can proceed with the shipment request by either:        <ul>               <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>                     <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page </li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S5 => S4 (Rejected)
        new() // 15
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalRejectedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of Annex 2 of SMTA 1 with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that you have returned with comment the Annex 2 of SMTA 1 submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification when the User resubmits Annex 2.    </p>    <p>        Sincerely,<br>        BioHub Operational Platform</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 15
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of BioHub Secretariat's Returning of Annex 2 of SMTA 1 with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the Annex 2 of SMTA 1 to the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find updates:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 15
        {
            Id = EmailForWaitingForAnnex2OfSMTA1SECsApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
            ToStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Secretariat's Returning of Annex 2 of SMTA 1 with Comment",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Secretariat has returned with comment the Annex 2 of SMTA 1.<br>        You can view the comment to resubmit the Annex 2 by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page </li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S6 => S7
        new() // 30
        {
            Id = EmailForSubmitBookingFormOfSMTA1ApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ToStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Submission of Booking Form",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted a Booking Form(s) to the WHO BioHub System. You will receive a further notification as our review process advances.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 30
        {
            Id = EmailForSubmitBookingFormOfSMTA1ApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ToStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of Booking Form Submission for Your Approval",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted a Booking Form(s) in the BioHub Operational Platform for your review and approval.<br>        You can find the submitted Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page within the IT Tool</li>            <li>Clicking the relevant shipment request on the Shipment Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat  </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 30
        {
            Id = EmailForSubmitBookingFormOfSMTA1ApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ToStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Booking Form Submission for BioHub Operational Focal Point's Approval",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted a Booking Form(s) in the BioHub Operational Platform for the review and approval by the BioHub Operational Focal Point.<br>        You can find the submitted Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>     <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },


        //Status: S7 => S9 (Approved)
        new() // 40
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Approval of Booking Form",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have approved the Booking Form(s) submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the WHO BioHub System.<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the BioHub Operational Foal Point has approved the Booking Form(s) submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has approved the Booking Form(s) submitted by the BioHub System User,  @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@.<br>        You can find the approved Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platoform is not yet expired)</i></li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 40
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToWHOOperationalFocalPointCourierId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required, WHO BioHub System] Confirmation E-mail of BioHub Operational Focal Point's Approval of Booking Form and Starting Shipment Coordination",
            
            //# 54317
            //EmailBody = "<p>        Dear Colleague,    </p>    <p>        This is to confirm by the WHO BioHub Secretariat that you can start coordination of a shipment requested by @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ from their facility to the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ upon approval of the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@, on their submitted Booking Form(s).<br>        To proceed with the shipment coordination, all the detailed information is shown below<br>        Please stay in touch with the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@ (<a href='mailto:@@@WHOOperationalFocalPointEmail@@@'>@@@WHOOperationalFocalPointEmail@@@</a>), while the shipment process advances.            </p>    <p>        <h3>WHO Booking Form(s) for Shipment of Biological Materials with Epidemic or Pandemic Potential to a WHO BioHub Facility</h3>        <h4>1. Pick-up and delivery addresses and contact persons</h4>        <p><strong>Information from the sending laboratory</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Request Date:</strong></td>                    <td>@@@RequestDate@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>From:</strong></td>                    <td>@@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Name:</strong></td>                    <td>@@@BioHubUserName@@@ @@@BioHubUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Mobile:</strong></td>                    <td>@@@BioHubUserMobilePhone@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Business (landline):</strong></td>                    <td>@@@BioHubUserLandline@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Email:</strong></td>                    <td><a href='mailto:@@@BioHubUserEmail@@@'>@@@BioHubUserEmail@@@</a></td>                </tr>            </table>        </p>        <p><strong>Requested date of pick-up: </strong>@@@RequestDateOfPickUp@@@</p>        <p>            <strong>Person(s) to be contacted for the pick-up</strong><br>            <ul>                @@@PickupUsers@@@            </ul>        </p>        <p><strong>Place of pick-up</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubUserInstituteName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubUserInstituteAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubUserCountryName@@@</td>                </tr>                            </table>        </p>        <p>            <strong>Person(s) to be contacted for the delivery</strong><br>            <ul>                @@@DeliveryUsers@@@            </ul>        </p>        <p><strong>Place of delivery</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubFacilityName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubFacilityAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubFacilityCountryName@@@</td>                </tr>            </table>        </p>        <h4>2. Details of shipment (WHO Account: @@@WHOAccountNumber@@@)</h4>        <p><strong>Substance Category</strong></p>        <p>@@@SubstanceCategory@@@</p>        <p><strong>Temperature Transport Conditions</strong></p>        <p>@@@TemperatureTransportConditions@@@</p>        <p><strong>Number of vials</strong></p>        <p>@@@NumberOfVials@@@</p> <p><strong>Total Amount (in ml)</strong></p>        <p>@@@TotalAmount@@@</p>         <p><strong>Number of inner packaging and size (if available)</strong></p>        <p>@@@NumberOfInnerPackagingAndSize@@@</p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Name:</strong></td>                    <td>@@@BioHubUserName@@@ @@@BioHubUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Function:</strong></td>                    <td>@@@BioHubUserJobTitle@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Signature:</strong></td>                    <td><img src='data:image/jpeg;base64,@@@BioHubUserSignature@@@' width='500px'></td>                </tr>            </table>        </p>    </p>    <p>        We look forward to continuing to work with you through this shipment process.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            EmailBody = "<p>        Dear Courier Team,    </p>    <p>        This is to confirm by the WHO BioHub Secretariat that you can start coordination of a shipment requested by @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ from their facility to the BioHub Facility, @@@BioHubFacilityName@@@ in @@@BioHubFacilityCountryName@@@ upon the Booking Form approval by the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@.<br>        To proceed with the shipment coordination, all the detailed information is shown below<br>        Please stay in touch with the BioHub Operational Focal Point, @@@WHOOperationalFocalPointName@@@ @@@WHOOperationalFocalPointLastname@@@ (<a href='mailto:@@@WHOOperationalFocalPointEmail@@@'>@@@WHOOperationalFocalPointEmail@@@</a>), while the shipment process advances.            </p>    <p>        <h3>WHO Booking Form(s) for Shipment of Biological Materials with Epidemic or Pandemic Potential to a WHO BioHub Facility</h3>        <h4>1. Pick-up and delivery addresses and contact persons</h4>        <p><strong>Information from the sending laboratory</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Request Date:</strong></td>                    <td>@@@RequestDate@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>From:</strong></td>                    <td>@@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Name:</strong></td>                    <td>@@@BioHubUserName@@@ @@@BioHubUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Mobile:</strong></td>                    <td>@@@BioHubUserMobilePhone@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Business (landline):</strong></td>                    <td>@@@BioHubUserLandline@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Email:</strong></td>                    <td><a href='mailto:@@@BioHubUserEmail@@@'>@@@BioHubUserEmail@@@</a></td>                </tr>            </table>        </p>        <p><strong>Requested date of pick-up: </strong>@@@RequestDateOfPickUp@@@</p>        <p>            <strong>Person(s) to be contacted for the pick-up</strong><br>            <ul>                @@@PickupUsers@@@            </ul>        </p>        <p><strong>Place of pick-up</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubUserInstituteName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubUserInstituteAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubUserCountryName@@@</td>                </tr>                            </table>        </p>        <p>            <strong>Person(s) to be contacted for the delivery</strong><br>            <ul>                @@@DeliveryUsers@@@            </ul>        </p>        <p><strong>Place of delivery</strong></p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Laboratory name:</strong></td>                    <td>@@@BioHubFacilityName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Address:</strong></td>                    <td>@@@BioHubFacilityAddress@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 150px;'><strong>Country:</strong></td>                    <td>@@@BioHubFacilityCountryName@@@</td>                </tr>            </table>        </p>        <h4>2. Details of shipment (WHO Account: @@@WHOAccountNumber@@@)</h4>        <p><strong>Substance Category</strong></p>        <p>@@@SubstanceCategory@@@</p>        <p><strong>Temperature Transport Conditions</strong></p>        <p>@@@TemperatureTransportConditions@@@</p>        <p><strong>Number of vials</strong></p>        <p>@@@NumberOfVials@@@</p> <p><strong>Total Amount (in ml)</strong></p>        <p>@@@TotalAmount@@@</p>         <p><strong>Number of inner packaging and size (if available)</strong></p>        <p>@@@NumberOfInnerPackagingAndSize@@@</p>        <p>            <table border='0'>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Name:</strong></td>                    <td>@@@BioHubUserName@@@ @@@BioHubUserLastName@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Function:</strong></td>                    <td>@@@BioHubUserJobTitle@@@</td>                </tr>                <tr>                    <td style='text-align: right; width: 50%;'><strong>Signature:</strong></td>                    <td><p width='500px'>@@@BioHubUserSignature@@@</p></td>                </tr>            </table>        </p>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            //////////////////////
            
            RoleId = WHOOperationalFocalPointRoleId,
            IsCourier = true
        },

        //Status: S7 => S6 (Rejected)
        new() // 35
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of Your Returning of Booking Form with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have returned with comment the Booking Form(s) submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User. You will receive a further notification when the User resubmits Booking Form.    </p>      <p>        Sincerely,<br>        WHO BioHub Secretariat  </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 35
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalRejectedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "Confirmation E-mail of BioHub Operational Focal Point's Returning of Booking Form with Comment to BioHub System User",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has returned with comment the Booking Form(s) submitted by the BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the User. You will receive a further notification when the User resubmits Booking Form(s).    </p>    <p>        Sincerely,<br>        BioHub Operational Platform</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 35
        {
            Id = EmailForWaitForBookingFormSMTA1OPSApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
            ToStatus = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
            ApprovedSubmission = false,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Operational Focal Point's Returning of Booking Form with Comment",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Operational Focal Point has returned with comment the Booking Form(s).<br>        You can view the comment to resubmit the Booking Form(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        //Status: S8 => S9
        //new()
        //{
        //    Id = EmailForSubmitSMTA1ShipmentDocumentsApprovedToLaboratoryITToolFocalPointId,
        //    FromStatus = WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
        //    ToStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission",
        //    EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have successfully submitted a shipment-related document(s) to the WHO BioHub System. You will receive a further notification as shipment process advances.            </p>    <p>        List of submitted documents:        <ul>            @@@ShipmentRelatedDocumentItem@@@        </ul>    </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = LaboratoryITToolFocalPointRoleId
        //},
        //new()
        //{
        //    Id = EmailForSubmitSMTA1ShipmentDocumentsApprovedToWHOOperationalFocalPointId,
        //    FromStatus = WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
        //    ToStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
        //    ApprovedSubmission = true,
        //    EmailSubject = "Confirmation E-mail of Shipment-related Document Submission for Your Approval",
        //    EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that a BioHub System User, @@@BioHubUserName@@@ @@@BioHubUserLastName@@@ at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, has submitted a Shipment-related Document(s) in the BioHub Operational IT Tool for your review and approval.    </p>    <p>        List of submitted documents:        <ul>            @@@ShipmentRelatedDocumentItem@@@        </ul>    </p>    <p>                You can find the submitted Document(s) by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address to the specific item only if your IT Tool session is not yet expired)</i></li>            <li>Clicking the related item in Worklist as part of the Dashboard in your personal page within the IT Tool</li>            <li>Clicking the relevant shipment request on the Shipment Overview page on your personal page</li>        </ul>            </p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
        //    RoleId = WHOOperationalFocalPointRoleId
        //},                       


        //Status: S9 => S10 [Pick-up completed]
        new() // 50
        {
            Id = EmailForWaitForPickUpCompletedApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Reporting of Courier's Pick-up of BMEPP at BioHub System User's Facility",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have successfully reported that the courier has picked up the BMEPP at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to deliver to the BioHub Facility, @@@BioHubFacilityName@@@.    </p>    <p>        Pickup details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndPickupDate@@@        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 50
        {
            Id = EmailForWaitForPickUpCompletedApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Pick-up of BMEPP at BioHub System User's Facility",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the courier has picked up the BMEPPs(s) at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to deliver to your facility. Upon the BMEPP's arrival at your facility, you will be directly contacted by the courier.    </p>    <p>        Pickup details:        <ul>            @@@CategoryItemWithPickupDate@@@        </ul>  </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 50
        {
            Id = EmailForWaitForPickUpCompletedApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForPickUpCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Pick-up of BMEPP at BioHub System User's Facility",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the courier has picked up the BMEPP at @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to deliver to the BioHub Facility, @@@BioHubFacilityName@@@. Upon the BMEPP's arrival at the BioHub Facility, you will be notified.    </p>    <p>        Pickup details:        <ul>            @@@CategoryItemWithPickupDate@@@        </ul>    </p>    <p>        Sincerely,<br>        BioHub Operational Platform    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },                


        //Status: S10 => S11 [Delivery completed]
        new() // 60
        {
            Id = EmailForWaitForDeliveryCompletedApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Reporting of Courier's Delivery Completion of BMEPP to BioHub Facility",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that you have successfully reported the courier has delivered the BMEPP to @@@BioHubFacilityName@@@. You will be then notified with the arrival condition and comment, if necessary, from the Facility.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithShipmentReferenceNumberAndDeliveryDate@@@        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 60
        {
            Id = EmailForWaitForDeliveryCompletedApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Delivery Completion of BMEPP to BioHub Facility",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the courier has delivered the BMEPP to the BioHub Facility, @@@BioHubFacilityName@@@. You will be then notified with the arrival condition and comment, if necessary, from the Facility.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithDeliveryDate@@@        </ul>  </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        new() // 60
        {
            Id = EmailForWaitForDeliveryCompletedApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Courier's Delivery Completion of BMEPP to BioHub Facility",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the courier has delivered BMEPP shipped from the BioHub System User, @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, to the BioHub Facility, @@@BioHubFacilityName@@@. You will be then notified with the arrival condition and comment, if necessary, from the Facility to the BioHub System User.    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithDeliveryDate@@@        </ul>    </p>    <p>        Sincerely,<br>        BioHub Operational Platform <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 60
        {
            Id = EmailForWaitForDeliveryCompletedApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForDeliveryCompleted,
            ToStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of Courier's Delivery Completion of BMEPP to Your Facility for Your Reporting of Arrival Condition and Confirmation of Shipment Completion",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that the courier has delivered the BMEPP to your facility for you to report the arrival condition of the BMEPP and confirm the shipment process completion .    </p>    <p>        Delivery details:        <ul>            @@@CategoryItemWithDeliveryDate@@@        </ul>    <br>        You can do so by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page </li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>  </p>    <p>   Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        //Status: S11 => S14 (Approved)
        new() // 110
        {
            Id = EmailForWaitForArrivalConditionCheckApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your  Reporting of Shipment Process Completion and Arrival Condition of BMEPP (Shipment Completed)",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that you have confirmed the entire shipment process for the BMEPP sent from @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ has been completed and reported the BMEPP arrival condition to the User.     To check the arrival condition, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 110
        {
            Id = EmailForWaitForArrivalConditionCheckApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that entire shipment process of the BMEPP to the BioHub Facility, @@@BioHubFacilityName@@@.  has been completed. To check the arrival condition of the BMEPP  at the BioHub Facility, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Sincerely,<br>        BioHub Operational IT Tool    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 110
        {
            Id = EmailForWaitForArrivalConditionCheckApprovedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that entire shipment process of the BMEPP to the BioHub Facility, @@@BioHubFacilityName@@@.  has been completed.   To check the arrival condition of the BMEPP at the BioHub Facility, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 110
        {
            Id = EmailForWaitForArrivalConditionCheckApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that entire shipment process of the BMEPP to the BioHub Facility, @@@BioHubFacilityName@@@.  has been completed. To check the arrival condition of the BMEPP at the BioHub Facility, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S11 => S12 (Rejected)
        new() // 65
        {
            Id = EmailForWaitForArrivalConditionCheckRejectedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Your Sending of Comment to BioHub System User about Arrival Condtion of BMEPP",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that your comment on the arrival condition of the BMEPP shipped from the BioHub System User, @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@, was sent to the User. You will receive a further notification when the User responds to it. To check the arrival condition of the BMEPP with comment at the BioHub Facility, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 65
        {
            Id = EmailForWaitForArrivalConditionCheckRejectedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Facility's Sending Comment to BioHub System User about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the BioHub Facility, @@@BioHubFacilityName@@@, sent comment on the arrival condition of BMEPP shipped from the BioHub System User, @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User, and thus the shipment is not been completed yet. You will receive a further notification when the User responds to it.    To check the comment, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Sincerely,<br>        BioHub Operational Platform </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 65
        {
            Id = EmailForWaitForArrivalConditionCheckRejectedToWHOOperationalFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of BioHub Facility's Sending Comment to BioHub System User about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub Operational Focal Point,    </p>    <p>        This is to confirm that the BioHub Facility, @@@BioHubFacilityName@@@, sent comment on the arrival condition of BMEPP shipped from the BioHub System User, @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ to the User, and thus the shipment is not been completed yet. You will receive a further notification when the User responds to it.    To check the comment, please click this <a href='@@@url@@@'>link</a>.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat</p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOOperationalFocalPointRoleId
        },

        new() // 65
        {
            Id = EmailForWaitForArrivalConditionCheckRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForArrivalConditionCheck,
            ToStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "[Action Required] Confirmation E-mail of BioHub Facility's Comment about Arrival Condition of BMEPP (Shipment NOT Completed)",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Facility, @@@BioHubFacilityName@@@, sent comment on the arrival condition of the BMEPP you shipped.</p>    <p>Please respond to the comment following the guidance below.<br>        You can view the comment to respond by either:        <ul>            <li>Clicking this <a href='@@@url@@@'>link</a> <i style='font-size: 10pt; color: #C0C0C0'>(this will address the specific item only if your session in the Operational Platform is not yet expired)</i></li>            <li>Clicking the related item in the Worklist as part of the Dashboard on your personal page</li>            <li>Clicking the relevant shipment request on the Shipment Requests page of your personal page</li>        </ul>    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S12 => S13 (Comment)
        new() // 70
        {
            Id = EmailForWaitForCommentBHFSendFeedbackApprovedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ToStatus = WorklistToBioHubStatus.WaitForFinalApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Comment and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that you have received a comment from @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ related to the Completion of Shipment Process.</p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 70
        {
            Id = EmailForWaitForCommentBHFSendFeedbackApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ToStatus = WorklistToBioHubStatus.WaitForFinalApproval,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Comment and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the BioHub Facility, @@@BioHubFacilityName@@@, have reported about the comment that you sent related to the Completion of Shipment Process.</p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },


        //Status: S13 => S12 (Rejected)
        new() // 75
        {
            Id = EmailForWaitForFinalApprovalRejectedToBioHubFacilityITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Feedback and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub Facility,    </p>    <p>        This is to confirm that you have sent feedback to @@@BioHubUserInstituteName@@@ in @@@BioHubUserCountryName@@@ related to the Completion of Shipment Process.</p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = BioHubFacilityITToolFocalPointRoleId
        },

        new() // 75
        {
            Id = EmailForWaitForFinalApprovalRejectedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Feedback and Completion of Shipment Process",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that you have received feedback from the BioHub Facility, @@@BioHubFacilityName@@@.</p>    <p>To check the feedback from the BioHub Facility, please click this <a href='@@@url@@@'>link</a>. Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

        //Status: S13 => S14 (Completed)
        new() // 100

        {
            Id = EmailForWaitForFinalApprovalApprovedToWHOSecretariatId,
            FromStatus = WorklistToBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistToBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub Secretariat,    </p>    <p>        This is to confirm that the entire shipment process has been completed.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = WHOSecretariatRoleId
        },

        new() // 100
        {
            Id = EmailForWaitForFinalApprovalApprovedToLaboratoryITToolFocalPointId,
            FromStatus = WorklistToBioHubStatus.WaitForFinalApproval,
            ToStatus = WorklistToBioHubStatus.ShipmentCompleted,
            ApprovedSubmission = true,
            EmailSubject = "Confirmation E-mail of Shipment Process Completion",
            EmailBody = "<p>        Dear BioHub System User,    </p>    <p>        This is to confirm that the entire shipment process has been completed.    </p>    <p>        Thank you for your contribution to the BioHub System.    </p>    <p>        Sincerely,<br>        WHO BioHub Secretariat    </p>    <p style='font-size: 10pt; color: #C0C0C0'>        ** This is an automatically generated email. Do not reply to this message**    </p>",
            RoleId = LaboratoryITToolFocalPointRoleId
        },

    };

    internal static Guid EmailForSubmitAnnex2OfSMTA1ApprovedToLaboratoryITToolFocalPointId => Guid.Parse("f5ba4c7c-4de6-4233-a555-66505a1d2697");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("2a89d6a6-872e-4305-96a2-a0c0f6b3365c");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("625403c4-4a9b-41f6-8c25-8bafaf3c7ec7");
    internal static Guid EmailForSubmitBookingFormOfSMTA1ApprovedToLaboratoryITToolFocalPointId => Guid.Parse("01d1fba1-4e76-4e53-b471-69a445b6930e");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("d5e642ef-fe11-4248-9ec4-f085221332e6");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("d07ef8cb-9ccf-446a-bd81-ccf33ae3f5c2");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("b37af424-ad0a-4f69-b43f-0cc9a8218994");
    internal static Guid EmailForSubmitAnnex2OfSMTA1ApprovedToWHOSecretariatId => Guid.Parse("eea21313-dee0-4165-9bf8-18ebc9adb790");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToWHOSecretariatId => Guid.Parse("087f8520-1efa-4404-ab35-e488efe4d2ee");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalRejectedToWHOSecretariatId => Guid.Parse("7b80d06b-2a41-4cd9-adcd-aecb2713c5bb");
    internal static Guid EmailForSubmitBookingFormOfSMTA1ApprovedToWHOSecretariatId => Guid.Parse("2d5a8455-efa4-4d8e-b440-642f0f596480");
    internal static Guid EmailForSubmitBookingFormOfSMTA1ApprovedToWHOOperationalFocalPointId => Guid.Parse("b96a3785-bbb8-4b3f-b171-5f203b9d544b");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToWHOSecretariatId => Guid.Parse("ce73006d-7344-41ae-9b6a-aa57eee25a43");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalRejectedToWHOSecretariatId => Guid.Parse("324f2b8b-4053-43a4-a42a-d5874f4c6fda");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("73c227ba-08dc-469a-ae80-de7c1855eb93");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("a5f735ef-be83-4026-9978-1881e4bcf885");
    internal static Guid EmailForWaitForPickUpCompletedApprovedToWHOSecretariatId => Guid.Parse("2a457bda-433b-4e25-a4ce-5351d13dbf4b");
    internal static Guid EmailForWaitForPickUpCompletedApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("486fc3e7-e9e0-4195-9e6c-95c2c10d7251");
    internal static Guid EmailForWaitForDeliveryCompletedApprovedToWHOSecretariatId => Guid.Parse("f62a6546-d3e5-492d-bae0-0da98c468c36");
    internal static Guid EmailForWaitForDeliveryCompletedApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("6fc92667-ebd4-453d-b387-4513f4e3620a");
    internal static Guid EmailForWaitForDeliveryCompletedApprovedToLaboratoryITToolFocalPointId => Guid.Parse("a9e08021-2c0b-410d-82c9-92f1743f549c");
    internal static Guid EmailForWaitForArrivalConditionCheckApprovedToWHOSecretariatId => Guid.Parse("2de86fb8-9671-405a-ac11-a48e4642a059");
    internal static Guid EmailForWaitForArrivalConditionCheckApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("8b9fe1cd-13cd-4ee2-b245-48a5add2ba39");
    internal static Guid EmailForWaitForArrivalConditionCheckApprovedToLaboratoryITToolFocalPointId => Guid.Parse("344772be-e08a-4d2f-a3c3-e1d9b5c9cdd0");
    internal static Guid EmailForWaitForCommentBHFSendFeedbackApprovedToBioHubFacilityITToolFocalPointId => Guid.Parse("6831f1d1-3744-4976-bcb0-8c9a3642e3ac");
    internal static Guid EmailForWaitForCommentBHFSendFeedbackApprovedToLaboratoryITToolFocalPointId => Guid.Parse("58832cbe-43e3-486c-8cda-63172169e6fa");
    internal static Guid EmailForWaitForFinalApprovalApprovedToWHOSecretariatId => Guid.Parse("ca726d95-1a41-4f4f-a02f-6aedc203e883");
    internal static Guid EmailForWaitForFinalApprovalApprovedToLaboratoryITToolFocalPointId => Guid.Parse("51002f80-0f42-41bf-bee9-ccbc7c618ed4");
    internal static Guid EmailForWaitForArrivalConditionCheckRejectedToWHOSecretariatId => Guid.Parse("633e7838-8899-4517-ac89-a2e4ef5b3470");
    internal static Guid EmailForWaitForArrivalConditionCheckRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("9d0a855f-55b0-44a1-bc4e-180c0218c2a4");
    internal static Guid EmailForWaitForArrivalConditionCheckRejectedToLaboratoryITToolFocalPointId => Guid.Parse("a5d33bf5-ad6d-4666-bd27-c2e2bd32e13c");
    internal static Guid EmailForWaitForFinalApprovalRejectedToBioHubFacilityITToolFocalPointId => Guid.Parse("f95fe6ff-e947-4dcd-a8f0-b72352140a03");
    internal static Guid EmailForWaitForFinalApprovalRejectedToLaboratoryITToolFocalPointId => Guid.Parse("3e86b89f-74fb-433d-af2b-473e6e4e5bd1");
    internal static Guid EmailForSubmitAnnex2OfSMTA1ApprovedToWHOOperationalFocalPointId => Guid.Parse("fac863db-889d-40c8-b95c-97071cc842af");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalApprovedToWHOOperationalFocalPointId => Guid.Parse("a3025bb7-9741-461b-b5bd-d8ab165e774c");
    internal static Guid EmailForWaitForBookingFormSMTA1OPSApprovalApprovedToWHOOperationalFocalPointCourierId => Guid.Parse("0c62e41e-728d-4e64-aa38-5b074d7f2a32");
    internal static Guid EmailForSubmitSMTA1ShipmentDocumentsApprovedToLaboratoryITToolFocalPointId => Guid.Parse("57da2d6a-1c36-4cbc-9f71-148eb9e44ff9");
    internal static Guid EmailForSubmitSMTA1ShipmentDocumentsApprovedToWHOOperationalFocalPointId => Guid.Parse("b4e01ce7-1a5f-4b24-b520-41b4e5a22ee7");
    internal static Guid EmailForWaitForPickUpCompletedApprovedToWHOOperationalFocalPointId => Guid.Parse("df67f13f-15dc-420f-90d3-f3c5f2dae90f");
    internal static Guid EmailForWaitForDeliveryCompletedApprovedToWHOOperationalFocalPointId => Guid.Parse("9a391c07-e003-4797-a1ed-8a159c0bcab4");
    internal static Guid EmailForWaitForArrivalConditionCheckApprovedToWHOOperationalFocalPointId => Guid.Parse("8723dfe0-3720-4949-ae39-c4ed5baa3f2b");
    internal static Guid EmailForWaitForArrivalConditionCheckRejectedToWHOOperationalFocalPointId => Guid.Parse("996c40d3-b243-4a39-b71c-66a4b90f0829");
    internal static Guid EmailForWaitingForAnnex2OfSMTA1SECsApprovalRejectedToWHOOperationalFocalPointId => Guid.Parse("ebafda94-6d6a-4c2d-9671-6aa432025c3e");

    private async Task AddOrUpdateWorklistToBioHubEmails(CancellationToken cancellationToken)
    {
        var rows = from o in _db.WorklistToBioHubEmails
                   select o;

        foreach (var row in rows)
        {
            _db.WorklistToBioHubEmails.Remove(row);
        }

        foreach (var worklistToBioHubEmail in WorklistToBioHubEmails)
        {
            if (await _db.WorklistToBioHubEmails.Where(x => x.Id == worklistToBioHubEmail.Id).AnyAsync(cancellationToken))
            {
                _db.Update(worklistToBioHubEmail);
            }
            else
            {
                await _db.AddAsync(worklistToBioHubEmail);
            }
        }
    }
}