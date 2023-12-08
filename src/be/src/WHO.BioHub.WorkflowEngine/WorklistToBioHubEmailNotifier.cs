using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public class WorklistToBioHubEmailNotifier : IWorklistToBioHubEmailNotifier
    {
        private readonly IWorklistToBioHubItemReadRepository _worklistToBioHubItemReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IWorklistToBioHubEmailReadRepository _worklistToBioHubEmailReadRepository;


        private readonly IWorkflowEngineUtility _workflowEngineUtility;
        private readonly ISendNotification _sendNotification;

        public WorklistToBioHubEmailNotifier(            IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepository,            IUserReadRepository userReadRepository,            IWorklistToBioHubEmailReadRepository worklistToBioHubEmailReadRepository,            IWorkflowEngineUtility workflowEngineUtility,
            ISendNotification sendNotification
        )
        {
            _worklistToBioHubItemReadRepository = worklistToBioHubItemReadRepository;
            _userReadRepository = userReadRepository;
            _worklistToBioHubEmailReadRepository = worklistToBioHubEmailReadRepository;
            _workflowEngineUtility = workflowEngineUtility;
            _sendNotification = sendNotification;
        }



        public async Task<Errors?> NotifyUsers(WorklistToBioHubItem worklistToBioHubItem, CancellationToken cancellationToken)
        {

            string permissionName = GetEmailPermissionName(worklistToBioHubItem);
            var fromStatus = worklistToBioHubItem.PreviousStatus;
            var toStatus = worklistToBioHubItem.Status;
            var bioHubFacilityId = worklistToBioHubItem.RequestInitiationToBioHubFacilityId;
            var laboratoryId = worklistToBioHubItem.RequestInitiationFromLaboratoryId;
            var approvedSubmission = worklistToBioHubItem.LastSubmissionApproved == true;
            WorklistToBioHubEmail worklistToBioHubEmail;
            Errors? errors;
            List<User> allUsersToBeNotified = new List<User>();
            List<User> whoAndBioHubUsersToBeNotified = new List<User>();
            string entityUrl = "worklistToBioHubItems";

            try
            {
                var laboratoryUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, laboratoryId, null, cancellationToken);
                var bioHubFacilityUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, null, bioHubFacilityId, cancellationToken);
                var whoUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, null, null, cancellationToken);

                allUsersToBeNotified.AddRange(laboratoryUsersToBeNotified);
                allUsersToBeNotified.AddRange(bioHubFacilityUsersToBeNotified);
                allUsersToBeNotified.AddRange(whoUsersToBeNotified);

                var emailCustomInfo = await _worklistToBioHubItemReadRepository.ReadInfoForEmail(worklistToBioHubItem.Id, worklistToBioHubItem.Status, worklistToBioHubItem.PreviousStatus, cancellationToken);


                //In this case, i.e. after the booking form approval, besides the common mail it must be sent a custom mail to the courier and courier users with the WHO OPS in cc
                if (worklistToBioHubItem.Status == WorklistToBioHubStatus.WaitForPickUpCompleted && worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
                {
                    if (allUsersToBeNotified.Any())
                    {
                        var roleIds = allUsersToBeNotified.Select(x => x.RoleId).Distinct();
                        foreach (var roleId in roleIds)
                        {
                            worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                            if (worklistToBioHubEmail != null)
                            {
                                worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);

                                var toEmails = allUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                                await _sendNotification.SendEmail(toEmails, null, null, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);

                            }
                        }
                    }

                    foreach (var bookingForm in emailCustomInfo.BookingForms)
                    {
                        worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, emailCustomInfo.WHOOperationalFocalPointRoleId, cancellationToken, true);
                        if (worklistToBioHubEmail != null)
                        {
                            worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);
                            worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyBookingFormInformation(worklistToBioHubEmail.EmailBody, bookingForm);

                            var courierEmail = bookingForm.CourierEmail;

                            var courierUserEmails = bookingForm.BookingFormCourierUsers.Select(x => x.Email).ToList();

                            var toEmails = courierUserEmails;

                            toEmails.Add(courierEmail);

                            var ccEmails = new List<string>() { emailCustomInfo.WHOOperationalFocalPointEmail };

                            await _sendNotification.SendEmail(toEmails, ccEmails, null, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);
                        }
                    }
                }


                //In this case, i.e. after the shipment approval upon successful arrival condition check, a custom unique mail must be sent to who and biohub facility users, then the email to the laboratory administrators
                else if (worklistToBioHubItem.Status == WorklistToBioHubStatus.ShipmentCompleted && worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForFinalApproval)
                {
                    whoAndBioHubUsersToBeNotified.AddRange(whoUsersToBeNotified);
                    whoAndBioHubUsersToBeNotified.AddRange(bioHubFacilityUsersToBeNotified);

                    if (whoAndBioHubUsersToBeNotified.Any())
                    {
                        var roleIds = whoAndBioHubUsersToBeNotified.Select(x => x.RoleId).Distinct();

                        foreach (var roleId in roleIds)
                        {
                            worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                            if (worklistToBioHubEmail != null)
                            {
                                worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);
                                var toEmails = whoAndBioHubUsersToBeNotified.Select(x => x.Email).ToList();
                               
                                await _sendNotification.SendEmail(toEmails, null, null, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);
                            }
                        }
                    }

                    if (laboratoryUsersToBeNotified.Any())
                    {
                        var roleIds = laboratoryUsersToBeNotified.Select(x => x.RoleId).Distinct();

                        foreach (var roleId in roleIds)
                        {
                            worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                            if (worklistToBioHubEmail != null)
                            {
                                worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);
                                var toEmails = laboratoryUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();
                                                               
                                await _sendNotification.SendEmail(toEmails, null, null, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);
                            }
                        }
                    }
                }

                //In this case, i.e. after the shipment approval or reject upon feedback receive, a custom mail to biohub facility users with who users in cc must be sent, then the email to the laboratory administrators
                else if (

                    (worklistToBioHubItem.Status == WorklistToBioHubStatus.WaitForCommentBHFSendFeedback && worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForFinalApproval) ||
                    (worklistToBioHubItem.Status == WorklistToBioHubStatus.WaitForFinalApproval && worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForCommentBHFSendFeedback))
                {

                    if (bioHubFacilityUsersToBeNotified.Any())
                    {
                        var roleIds = bioHubFacilityUsersToBeNotified.Select(x => x.RoleId).Distinct();

                        foreach (var roleId in roleIds)
                        {
                            worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                            if (worklistToBioHubEmail != null)
                            {
                                worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);
                                var toEmails = bioHubFacilityUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                                var bccEmails = whoUsersToBeNotified.Select(x => x.Email).ToList();

                                await _sendNotification.SendEmail(toEmails, null, bccEmails, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);

                            }
                        }
                    }

                    if (laboratoryUsersToBeNotified.Any())
                    {
                        var roleIds = laboratoryUsersToBeNotified.Select(x => x.RoleId).Distinct();

                        foreach (var roleId in roleIds)
                        {
                            worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                            if (worklistToBioHubEmail != null)
                            {
                                worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);
                                var toEmails = laboratoryUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();
                                                                
                                await _sendNotification.SendEmail(toEmails, null, null, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);
                            }
                        }
                    }
                }


                //in all other case, sent common email
                else
                {
                    if (allUsersToBeNotified.Any())
                    {
                        var roleIds = allUsersToBeNotified.Select(x => x.RoleId).Distinct();
                        foreach (var roleId in roleIds)
                        {
                            worklistToBioHubEmail = await _worklistToBioHubEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                            if (worklistToBioHubEmail != null)
                            {
                                worklistToBioHubEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(worklistToBioHubEmail.EmailBody, emailCustomInfo, worklistToBioHubEmail.Role.RoleType, entityUrl);
                                var toEmails = allUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                                
                                await _sendNotification.SendEmail(toEmails, null, null, worklistToBioHubEmail.EmailBody, string.Empty, worklistToBioHubEmail.EmailSubject);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        private string GetEmailPermissionName(WorklistToBioHubItem worklistToBioHubItem)
        {
            switch (worklistToBioHubItem.Status)
            {
                case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:

                    if (worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApproval;
                    }

                case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:
                    return PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA1Approval;


                case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:
                    if (worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApproval;
                    }

                case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:
                    return PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA1Approval;


                //case WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments:
                //    return PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApproval;

                case WorklistToBioHubStatus.WaitForPickUpCompleted:
                    return PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApproval;


                case WorklistToBioHubStatus.WaitForDeliveryCompleted:

                    return PermissionNames.CanReceiveEmailsOnWaitForPickUpCompletedApproval;


                case WorklistToBioHubStatus.WaitForArrivalConditionCheck:
                    return PermissionNames.CanReceiveEmailsOnWaitForDeliveryCompletedApproval;


                case WorklistToBioHubStatus.WaitForCommentBHFSendFeedback:
                    if (worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForArrivalConditionCheck)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForArrivalConditionCheckReject;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalReject;
                    }

                case WorklistToBioHubStatus.WaitForFinalApproval:
                    return PermissionNames.CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApproval;

                case WorklistToBioHubStatus.ShipmentCompleted:
                    if (worklistToBioHubItem.PreviousStatus == WorklistToBioHubStatus.WaitForArrivalConditionCheck)
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForArrivalConditionCheckApproval;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalApproval;
                    }

                default:
                    return null;

            }
        }



        private async Task<Errors?> SendEmail(string body, string subject, List<string>? toEmails, List<string>? ccEmails, List<string>? bccEmails, string? fromEmail = null)
        {
            await _sendNotification.SendEmail(toEmails, ccEmails, bccEmails, body, "", subject);
            return null;
        }

    }
}