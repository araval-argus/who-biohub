using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowEmails;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public class SMTA2WorkflowEmailNotifier : ISMTA2WorkflowEmailNotifier
    {

        private readonly ISMTA2WorkflowItemReadRepository _SMTA2WorkflowItemReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ISMTA2WorkflowEmailReadRepository _SMTA2WorkflowEmailReadRepository;
        private readonly IWorkflowEngineUtility _workflowEngineUtility;
        private readonly ISendNotification _sendNotification;

        public SMTA2WorkflowEmailNotifier(
            ISMTA2WorkflowItemReadRepository SMTA2WorkflowItemReadRepository,
            IUserReadRepository userReadRepository,
            ISMTA2WorkflowEmailReadRepository SMTA2WorkflowEmailReadRepository,
            IWorkflowEngineUtility workflowEngineUtility,
            ISendNotification sendNotification
        )
        {
            _SMTA2WorkflowItemReadRepository = SMTA2WorkflowItemReadRepository;
            _userReadRepository = userReadRepository;
            _SMTA2WorkflowEmailReadRepository = SMTA2WorkflowEmailReadRepository;
            _workflowEngineUtility = workflowEngineUtility;
            _sendNotification = sendNotification;
        }


        public async Task<Errors?> NotifyUsers(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken)
        {

            string permissionName = GetEmailPermissionName(SMTA2WorkflowItem);
            var fromStatus = SMTA2WorkflowItem.PreviousStatus;
            var toStatus = SMTA2WorkflowItem.Status;
            var laboratoryId = SMTA2WorkflowItem.LaboratoryId;
            var approvedSubmission = SMTA2WorkflowItem.LastSubmissionApproved == true;
            SMTA2WorkflowEmail SMTA2WorkflowEmail;
            Errors? errors;
            List<User> allUsersToBeNotified = new List<User>();
            List<User> whoAndBioHubUsersToBeNotified = new List<User>();
            string entityUrl = "SMTA2WorkflowItems";

            try
            {
                var laboratoryUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, laboratoryId, null, cancellationToken);
                var whoUsersToBeNotified = await _userReadRepository.ListByPermissionName(permissionName, null, null, cancellationToken);

                allUsersToBeNotified.AddRange(laboratoryUsersToBeNotified);
                allUsersToBeNotified.AddRange(whoUsersToBeNotified);

                var emailCustomInfo = await _SMTA2WorkflowItemReadRepository.ReadInfoForEmail(SMTA2WorkflowItem.Id, SMTA2WorkflowItem.Status, SMTA2WorkflowItem.PreviousStatus, cancellationToken);

                if (allUsersToBeNotified.Any())
                {
                    var roleIds = allUsersToBeNotified.Select(x => x.RoleId).Distinct();
                    foreach (var roleId in roleIds)
                    {
                        SMTA2WorkflowEmail = await _SMTA2WorkflowEmailReadRepository.ReadByStatusRoleApproved(fromStatus, toStatus, approvedSubmission, roleId.GetValueOrDefault(), cancellationToken);

                        if (SMTA2WorkflowEmail != null)
                        {
                            SMTA2WorkflowEmail.EmailBody = _workflowEngineUtility.FormatEmailBodyGeneralInformation(SMTA2WorkflowEmail.EmailBody, emailCustomInfo, SMTA2WorkflowEmail.Role.RoleType, entityUrl);

                            var toEmails = allUsersToBeNotified.Where(x => x.RoleId == roleId).Select(x => x.Email).ToList();

                            await _sendNotification.SendEmail(toEmails, null, null, SMTA2WorkflowEmail.EmailBody, string.Empty, SMTA2WorkflowEmail.EmailSubject);
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



        private string GetEmailPermissionName(SMTA2WorkflowItem SMTA2WorkflowItem)
        {
            switch (SMTA2WorkflowItem.Status)
            {
                case SMTA2WorkflowStatus.SubmitSMTA2:
                    if (SMTA2WorkflowItem.PreviousStatus == SMTA2WorkflowStatus.RequestInitiation)
                    {
                        return PermissionNames.CanReceiveEmailsOnRequestInitiation;
                    }
                    else
                    {
                        return PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalReject;
                    }


                case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:
                    return PermissionNames.CanReceiveEmailsOnSubmitSMTA2;



                case SMTA2WorkflowStatus.SMTA2WorkflowComplete:
                    return PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApproval;


                default:
                    return null;

            }
        }
    }
}