using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubEmails;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public class WorklistFromBioHubEmailNotifier : IWorklistFromBioHubEmailNotifier
    {

        private readonly IWorklistFromBioHubItemReadRepository _worklistFromBioHubItemReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IWorklistFromBioHubEmailReadRepository _worklistFromBioHubEmailReadRepository;

        private readonly IWorkflowEngineUtility _workflowEngineUtility;
        private readonly ISendNotification _sendNotification;

        public WorklistFromBioHubEmailNotifier(            IWorklistFromBioHubItemReadRepository worklistFromBioHubItemReadRepository,            IUserReadRepository userReadRepository,            IWorklistFromBioHubEmailReadRepository worklistFromBioHubEmailReadRepository,            IWorkflowEngineUtility workflowEngineUtility,
            ISendNotification sendNotification        )
        {
            _worklistFromBioHubItemReadRepository = worklistFromBioHubItemReadRepository;
            _userReadRepository = userReadRepository;
            _worklistFromBioHubEmailReadRepository = worklistFromBioHubEmailReadRepository;
            _workflowEngineUtility = workflowEngineUtility;
            _sendNotification = sendNotification;
        }


        public async Task<Errors?> NotifyUsers(WorklistFromBioHubItem worklistFromBioHubItem, CancellationToken cancellationToken)
        {

            string permissionName = GetEmailPermissionName(worklistFromBioHubItem);
            var fromStatus = worklistFromBioHubItem.PreviousStatus;
            var toStatus = worklistFromBioHubItem.Status;
            var bioHubFacilityId = worklistFromBioHubItem.RequestInitiationFromBioHubFacilityId;
            var laboratoryId = worklistFromBioHubItem.RequestInitiationToLaboratoryId;
            var approvedSubmission = worklistFromBioHubItem.LastSubmissionApproved == true;
            WorklistFromBioHubEmail worklistFromBioHubEmail;
            Errors? errors;
            List<User> allUsersToBeNotified = new List<User>();
            List<User> whoAndBioHubUsersToBeNotified = new List<User>();
            string entityUrl = "worklistFromBioHubItems";

            try
            {
                var laboratoryUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, laboratoryId, null, cancellationToken);
                var bioHubFacilityUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, null, bioHubFacilityId, cancellationToken);
                var whoUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, null, null, cancellationToken);

                allUsersToBeNotified.AddRange(laboratoryUsersToBeNotified);
                allUsersToBeNotified.AddRange(bioHubFacilityUsersToBeNotified);
                allUsersToBeNotified.AddRange(whoUsersToBeNotified);

                var emailCustomInfo = await _worklistFromBioHubItemReadRepository.ReadInfoForEmail(worklistFromBioHubItem.Id, worklistFromBioHubItem.Status, worklistFromBioHubItem.PreviousStatus, cancellationToken);


                //In this case, i.e. after the booking form approval, besides the common mail it must be sent a custom mail to the courier and courier users with the WHO OPS in cc
                if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval && worklistFromBioHubItem.LastSubmissionApproved == true)
                {
                    await Notify(fromStatus, toStatus, approvedSubmission, allUsersToBeNotified, entityUrl, emailCustomInfo, cancellationToken);


                    foreach (var bookingForm in emailCustomInfo.BookingForms)
                    {
                        worklistFromBioHubEmail = await _worklistFromBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, emailCustomInfo.WHOOperationalFocalPointRoleId, cancellationToken, true);
                        if (worklistFromBioHubEmail != null)
                        {
                            worklistFromBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyBookingFormInformation(worklistFromBioHubEmail.EmailBody, bookingForm);

                            var courierEmail = bookingForm.CourierEmail;

                            var courierUserEmails = bookingForm.BookingFormCourierUsers.Select(x => x.Email).ToList();

                            var toEmails = courierUserEmails;

                            toEmails.Add(courierEmail);

                            var ccEmails = new List<string>() { emailCustomInfo.WHOOperationalFocalPointEmail };

                            await _sendNotification.SendEmail(toEmails, ccEmails, null, worklistFromBioHubEmail.EmailBody, string.Empty, worklistFromBioHubEmail.EmailSubject);

                        }
                    }
                }


                else if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForPickUpCompleted && worklistFromBioHubItem.LastSubmissionApproved == true)
                {
                    await Notify(fromStatus, toStatus, approvedSubmission, allUsersToBeNotified, entityUrl, emailCustomInfo, cancellationToken);

                    var selectedMaterialsCurrentNumberOfInfo = emailCustomInfo.MaterialsCurrentNumberOfInfo.Where(x => x.NewNumberOfVials < x.WarningEmailCurrentNumberOfVialsThreshold).ToList();

                    if (selectedMaterialsCurrentNumberOfInfo.Any())
                    {
                        bioHubFacilityUsersToBeNotified = await _userReadRepository.ListByPermissionName(PermissionNames.CanReceiveEmailOnNumberOfVialsWarning, null, bioHubFacilityId, cancellationToken);

                        var roleIds = bioHubFacilityUsersToBeNotified.Select(x => x.RoleId).Distinct();

                        foreach (var roleId in roleIds)
                        {
                            worklistFromBioHubEmail = await _worklistFromBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken, false, true);

                            if (worklistFromBioHubEmail != null)
                            {
                                worklistFromBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyWarningCurrentNumberOfVialsInformation(worklistFromBioHubEmail.EmailBody, emailCustomInfo.MaterialsCurrentNumberOfInfo);

                                var toEmails = bioHubFacilityUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                                await _sendNotification.SendEmail(toEmails, null, null, worklistFromBioHubEmail.EmailBody, string.Empty, worklistFromBioHubEmail.EmailSubject);

                            }

                        }
                    }
                }


                //In this case, i.e. after the shipment approval upon successful arrival condition check, a custom unique mail must be sent to who and biohub facility users, then the email to the laboratory administrators
                else if (worklistFromBioHubItem.Status == WorklistFromBioHubStatus.ShipmentCompleted && worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForFinalApproval)
                {
                    whoAndBioHubUsersToBeNotified.AddRange(whoUsersToBeNotified);
                    whoAndBioHubUsersToBeNotified.AddRange(bioHubFacilityUsersToBeNotified);

                    await Notify(fromStatus, toStatus, approvedSubmission, whoAndBioHubUsersToBeNotified, entityUrl, emailCustomInfo, cancellationToken);

                    await Notify(fromStatus, toStatus, approvedSubmission, laboratoryUsersToBeNotified.ToList(), entityUrl, emailCustomInfo, cancellationToken);

                                        
                }

                //In this case, i.e. after the shipment approval or reject upon feedback receive, a custom mail to biohub facility users with who users in cc must be sent, then the email to the laboratory administrators
                else if (

                    (worklistFromBioHubItem.Status == WorklistFromBioHubStatus.WaitForCommentQESendFeedback && worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForFinalApproval) ||
                    (worklistFromBioHubItem.Status == WorklistFromBioHubStatus.WaitForFinalApproval && worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForCommentQESendFeedback))
                {

                    var bccEmails = whoUsersToBeNotified.Select(x => x.Email).ToList();
                    
                    await Notify(fromStatus, toStatus, approvedSubmission, bioHubFacilityUsersToBeNotified.ToList(), entityUrl, emailCustomInfo, cancellationToken, bccEmails);
                                        
                    await Notify(fromStatus, toStatus, approvedSubmission, laboratoryUsersToBeNotified.ToList(), entityUrl, emailCustomInfo, cancellationToken);
                                        
                }


                //in all other case, sent common email
                else
                {
                    await Notify(fromStatus, toStatus, approvedSubmission, allUsersToBeNotified, entityUrl, emailCustomInfo, cancellationToken);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task Notify(WorklistFromBioHubStatus fromStatus, WorklistFromBioHubStatus toStatus, bool approvedSubmission, List<User>? usersToBeNotified, string entityUrl, WorkflowEmailInfoDto emailCustomInfo, CancellationToken cancellationToken, List<string>? bccEmails = null)
        {
            if (usersToBeNotified != null && usersToBeNotified.Any())
            {
                var roleIds = usersToBeNotified.Select(x => x.RoleId).Distinct();
                foreach (var roleId in roleIds)
                {
                    var worklistFromBioHubEmail = await _worklistFromBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                    if (worklistFromBioHubEmail != null)
                    {
                        worklistFromBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistFromBioHubEmail.EmailBody, emailCustomInfo, worklistFromBioHubEmail.Role.RoleType, entityUrl);
                        var toEmails = usersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                        await _sendNotification.SendEmail(toEmails, null, bccEmails, worklistFromBioHubEmail.EmailBody, string.Empty, worklistFromBioHubEmail.EmailSubject);
                    }
                }
            }            
        }

        private string GetEmailPermissionName(WorklistFromBioHubItem worklistFromBioHubItem)
        {
            switch (worklistFromBioHubItem.Status)
            {

                case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:

                    if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApproval;
                    }


                case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:
                    return PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA2Approval;


                case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:

                    if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApproval;
                    }


                case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:
                    return PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApproval;


                case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:
                    if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApproval;
                    }

                case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:
                    return PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA2Approval;
               

                case WorklistFromBioHubStatus.WaitForPickUpCompleted:                    
                    return PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApproval;


                case WorklistFromBioHubStatus.WaitForDeliveryCompleted:

                    return PermissionNames.CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApproval;


                case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:
                    return PermissionNames.CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApproval;


                case WorklistFromBioHubStatus.WaitForCommentQESendFeedback:
                    if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForArrivalConditionCheck)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForSMTA2FinalApprovalReject;
                    }

                case WorklistFromBioHubStatus.WaitForFinalApproval:
                    return PermissionNames.CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApproval;

                case WorklistFromBioHubStatus.ShipmentCompleted:
                    if (worklistFromBioHubItem.PreviousStatus == WorklistFromBioHubStatus.WaitForArrivalConditionCheck)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApproval;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForSMTA2FinalApprovalApproval;
                    }

                default:
                    return null;

            }
        }

    }
}