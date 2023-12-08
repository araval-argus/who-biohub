using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowEmails;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public class SMTA1WorkflowEmailNotifier : ISMTA1WorkflowEmailNotifier
    {

        private readonly ISMTA1WorkflowItemReadRepository _SMTA1WorkflowItemReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ISMTA1WorkflowEmailReadRepository _SMTA1WorkflowEmailReadRepository;
        private readonly IWorkflowEngineUtility _workflowEngineUtility;
        private readonly ISendNotification _sendNotification;

        public SMTA1WorkflowEmailNotifier(
            ISMTA1WorkflowItemReadRepository SMTA1WorkflowItemReadRepository,
            IUserReadRepository userReadRepository,
            ISMTA1WorkflowEmailReadRepository SMTA1WorkflowEmailReadRepository,
            IWorkflowEngineUtility workflowEngineUtility,
            ISendNotification sendNotification        )
        {
            _SMTA1WorkflowItemReadRepository = SMTA1WorkflowItemReadRepository;
            _userReadRepository = userReadRepository;
            _SMTA1WorkflowEmailReadRepository = SMTA1WorkflowEmailReadRepository;
            _workflowEngineUtility = workflowEngineUtility;
            _sendNotification = sendNotification;
        }


        public async Task<Errors?> NotifyUsers(SMTA1WorkflowItem SMTA1WorkflowItem, CancellationToken cancellationToken)
        {

            string permissionName = GetEmailPermissionName(SMTA1WorkflowItem);
            var fromStatus = SMTA1WorkflowItem.PreviousStatus;
            var toStatus = SMTA1WorkflowItem.Status;
            var laboratoryId = SMTA1WorkflowItem.LaboratoryId;
            var approvedSubmission = SMTA1WorkflowItem.LastSubmissionApproved == true;
            SMTA1WorkflowEmail SMTA1WorkflowEmail;
            Errors? errors;
            List<User> allUsersToBeNotified = new List<User>();
            List<User> whoAndBioHubUsersToBeNotified = new List<User>();
            string entityUrl = "SMTA1WorkflowItems";

            try
            {
                var laboratoryUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, laboratoryId, null, cancellationToken);
                var whoUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, null, null, cancellationToken);

                allUsersToBeNotified.AddRange(laboratoryUsersToBeNotified);
                allUsersToBeNotified.AddRange(whoUsersToBeNotified);

                var emailCustomInfo = await _SMTA1WorkflowItemReadRepository.ReadInfoForEmail(SMTA1WorkflowItem.Id, SMTA1WorkflowItem.Status, SMTA1WorkflowItem.PreviousStatus, cancellationToken);

                if (allUsersToBeNotified.Any())
                {
                    var roleIds = allUsersToBeNotified.Select(x => x.RoleId).Distinct();
                    foreach (var roleId in roleIds)
                    {
                        SMTA1WorkflowEmail = await _SMTA1WorkflowEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                        if (SMTA1WorkflowEmail != null)
                        {
                            SMTA1WorkflowEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(SMTA1WorkflowEmail.EmailBody, emailCustomInfo, SMTA1WorkflowEmail.Role.RoleType, entityUrl);

                            var toEmails = allUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                            await _sendNotification.SendEmail(toEmails, null, null, SMTA1WorkflowEmail.EmailBody, string.Empty, SMTA1WorkflowEmail.EmailSubject);
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



        private string GetEmailPermissionName(SMTA1WorkflowItem SMTA1WorkflowItem)
        {
            switch (SMTA1WorkflowItem.Status)
            {
                case SMTA1WorkflowStatus.SubmitSMTA1:
                    if (SMTA1WorkflowItem.PreviousStatus == SMTA1WorkflowStatus.RequestInitiation)
                    {
                        return PermissionNames.CanReceiveEmailsOnRequestInitiation;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalReject;
                    }


                case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:
                    return PermissionNames.CanReceiveEmailsOnSubmitSMTA1;



                case SMTA1WorkflowStatus.SMTA1WorkflowComplete:
                    return PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApproval;


                default:
                    return null;

            }
        }
    }
}