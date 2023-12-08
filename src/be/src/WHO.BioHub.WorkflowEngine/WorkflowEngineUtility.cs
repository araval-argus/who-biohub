using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public class WorkflowEngineUtility : IWorkflowEngineUtility
    {
        private readonly ApplicationConfiguration _applicationConfiguration;
        //# 54317
        //private const string EmptySignature = "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";
        private string EmptySignature = string.Empty;
        //////////////////////

        public WorkflowEngineUtility(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public string BaseUrl()
        {
            return _applicationConfiguration.BaseUrl;
        }

        public string FormatEmailBodyGeneralInformation(string body, WorkflowEmailInfoDto emailCustomInfo, RoleType roleType, string entityUrl)
        {
            string listCategoryTypeWithPickupDate = string.Empty;
            string listCategoryTypeWithShipmentReferenceNumberAndPickupDate = string.Empty;

            string listCategoryTypeWithDeliveryDate = string.Empty;
            string listCategoryTypeWithShipmentReferenceNumberAndDeliveryDate = string.Empty;

            body = body.Replace("@@@BioHubUserName@@@", emailCustomInfo.LaboratoryUserFirstName);
            body = body.Replace("@@@BioHubUserLastName@@@", emailCustomInfo.LaboratoryUserLastName);
            body = body.Replace("@@@BioHubUserInstituteName@@@", emailCustomInfo.LaboratoryName);
            body = body.Replace("@@@BioHubUserCountryName@@@", emailCustomInfo.LaboratoryCountry);
            body = body.Replace("@@@BioHubFacilityName@@@", emailCustomInfo.BioHubFacilityName);
            body = body.Replace("@@@BioHubUserMobilePhone@@@", emailCustomInfo.LaboratoryUserMobilePhone);
            body = body.Replace("@@@BioHubUserLandline@@@", emailCustomInfo.LaboratoryUserBusinessPhone);
            body = body.Replace("@@@BioHubUserEmail@@@", emailCustomInfo.LaboratoryUserEmail);
            body = body.Replace("@@@BioHubUserInstituteAddress@@@", emailCustomInfo.LaboratoryAddress);
            body = body.Replace("@@@BioHubFacilityAddress@@@", emailCustomInfo.BioHubFacilityAddress);
            body = body.Replace("@@@BioHubFacilityCountryName@@@", emailCustomInfo.BioHubFacilityCountry);
            body = body.Replace("@@@WHOOperationalFocalPointName@@@", emailCustomInfo.WHOOperationalFocalPointName);
            body = body.Replace("@@@WHOOperationalFocalPointLastname@@@", emailCustomInfo.WHOOperationalFocalPointLastname);
            body = body.Replace("@@@WHOOperationalFocalPointEmail@@@", emailCustomInfo.WHOOperationalFocalPointEmail);
            body = body.Replace("@@@BioHubUserJobTitle@@@", emailCustomInfo.LaboratoryUserJobTitle);
            body = body.Replace("@@@BioHubUserSignature@@@", string.IsNullOrEmpty(emailCustomInfo.LaboratoryUserSignature) ? EmptySignature : emailCustomInfo.LaboratoryUserSignature);
            body = body.Replace("@@@WaitForArrivalConditionCheckApprovalComment@@@", string.IsNullOrEmpty(emailCustomInfo.WaitForArrivalConditionCheckApprovalComment) ? string.Empty : emailCustomInfo.WaitForArrivalConditionCheckApprovalComment);
            body = body.Replace("@@@WHOAccountNumber@@@", string.IsNullOrEmpty(emailCustomInfo.WHOAccountNumber) ? string.Empty : emailCustomInfo.WHOAccountNumber);

            body = body.Replace("@@@BioHubFacilityUserFirstName@@@", emailCustomInfo.BioHubFacilityUserFirstName);
            body = body.Replace("@@@BioHubFacilityUserLastName@@@", emailCustomInfo.BioHubFacilityUserLastName);
            body = body.Replace("@@@BioHubFacilityUserSignature@@@", string.IsNullOrEmpty(emailCustomInfo.BioHubFacilityUserSignature) ? EmptySignature : emailCustomInfo.BioHubFacilityUserSignature);
            body = body.Replace("@@@BioHubFacilityUserMobilePhone@@@", emailCustomInfo.BioHubFacilityUserMobilePhone);
            body = body.Replace("@@@BioHubFacilityUserLandline@@@", emailCustomInfo.BioHubFacilityUserBusinessPhone);
            body = body.Replace("@@@BioHubFacilityUserEmail@@@", emailCustomInfo.BioHubFacilityUserEmail);
            body = body.Replace("@@@BioHubFacilityUserJobTitle@@@", emailCustomInfo.BioHubFacilityUserJobTitle);



            if (emailCustomInfo.BookingForms != null)
            {
                foreach (var emailCustomBookingFormInfo in emailCustomInfo.BookingForms)
                {
                    string categoryType = emailCustomBookingFormInfo.TransportCategoryName;
                    string dateOfPickup = FormatDate(emailCustomBookingFormInfo.DateOfPickup);
                    string dateOfDelivery = FormatDate(emailCustomBookingFormInfo.DateOfDelivery);


                    listCategoryTypeWithPickupDate += $"<li> {categoryType} - {dateOfPickup} </li>";
                    listCategoryTypeWithShipmentReferenceNumberAndPickupDate += $"<li>{categoryType} - {emailCustomBookingFormInfo.ShipmentReferenceNumber} - {dateOfPickup}</li>";
                    listCategoryTypeWithDeliveryDate += $"<li> {categoryType} - {dateOfDelivery} </li>";
                    listCategoryTypeWithShipmentReferenceNumberAndDeliveryDate += $"<li>{categoryType} - {emailCustomBookingFormInfo.ShipmentReferenceNumber} - {dateOfDelivery}</li>";

                }
            }

            body = body.Replace("@@@CategoryItemWithPickupDate@@@", listCategoryTypeWithPickupDate);
            body = body.Replace("@@@CategoryItemWithShipmentReferenceNumberAndPickupDate@@@", listCategoryTypeWithShipmentReferenceNumberAndPickupDate);

            body = body.Replace("@@@CategoryItemWithDeliveryDate@@@", listCategoryTypeWithDeliveryDate);
            body = body.Replace("@@@CategoryItemWithShipmentReferenceNumberAndDeliveryDate@@@", listCategoryTypeWithShipmentReferenceNumberAndDeliveryDate);


            if (emailCustomInfo.ShipmentDocuments != null)
            {
                string listDocuments = string.Empty;
                foreach (var document in emailCustomInfo.ShipmentDocuments)
                {
                    listDocuments += $"<li>{document.DocumentName}.{document.DocumentExtension}    {document.DocumentType}</li>";
                }
                body = body.Replace("@@@ShipmentRelatedDocumentItem@@@", listDocuments);

            }


            if (emailCustomInfo.LaboratoryFocalPoints != null)
            {
                string listLaboratoryFocalPoints = string.Empty;
                foreach (var laboratoryFocalPoint in emailCustomInfo.LaboratoryFocalPoints)
                {
                    listLaboratoryFocalPoints += $"<li>{laboratoryFocalPoint.Name} <a href='mailto:{laboratoryFocalPoint.Email}'>{laboratoryFocalPoint.Email}</a> {laboratoryFocalPoint.Phone}</li>";
                }
                body = body.Replace("@@@BioHubUserToBeContactedItem_Name_Email_Phones@@@", listLaboratoryFocalPoints);

            }

            if (emailCustomInfo.Feedback != null)
            {
                string feedback = string.Empty;
                if (emailCustomInfo.Feedback != null)
                {

                    feedback += "<p>" + emailCustomInfo.Feedback.PostedBy + "</p>";
                    feedback += "<p>" + emailCustomInfo.Feedback.Text + "</p>";
                }
                body = body.Replace("@@@Feedbacks@@@", feedback);

            }


            string baseUrl = BaseUrl();
            switch (roleType)
            {

                case RoleType.Laboratory:
                    body = body.Replace("@@@url@@@", $"{baseUrl}laboratoryarea/{entityUrl}/{emailCustomInfo.Id}/detail");
                    break;

                case RoleType.WHO:
                    body = body.Replace("@@@url@@@", $"{baseUrl}whoarea/{entityUrl}/{emailCustomInfo.Id}/detail");
                    break;

                case RoleType.BioHubFacility:
                    body = body.Replace("@@@url@@@", $"{baseUrl}biohubfacilityarea/{entityUrl}/{emailCustomInfo.Id}/detail");
                    break;

                default:
                    break;
            }

            return body;
        }

        public string FormatEmailBodyBookingFormInformation(string body, BookingFormEmailInfoDto emailCustomBookingFormInfo)
        {
            string categoryType = emailCustomBookingFormInfo.TransportCategoryName;

            string date = FormatDate(emailCustomBookingFormInfo.Date);

            body = body.Replace("@@@RequestDate@@@", date);

            string requestDateOfPickup = FormatDate(emailCustomBookingFormInfo.RequestDateOfPickup);

            body = body.Replace("@@@RequestDateOfPickUp@@@", requestDateOfPickup);

            string substanceCategory = emailCustomBookingFormInfo.TransportCategoryName;

            body = body.Replace("@@@SubstanceCategory@@@", substanceCategory);
            body = body.Replace("@@@TemperatureTransportConditions@@@", emailCustomBookingFormInfo.TemperatureTransportCondition);
            body = body.Replace("@@@NumberOfVials@@@", emailCustomBookingFormInfo.Quantity.ToString());
            body = body.Replace("@@@TotalAmount@@@", emailCustomBookingFormInfo.Amount.ToString());

            body = body.Replace("@@@NumberOfInnerPackagingAndSize@@@", emailCustomBookingFormInfo.NumberOfInnerPackagingAndSize);
            body = body.Replace("@@@CategoryType@@@", categoryType);

            if (emailCustomBookingFormInfo.BookingFormPickupUsers != null)
            {
                string listPickupUsers = string.Empty;
                foreach (var pickupUser in emailCustomBookingFormInfo.BookingFormPickupUsers)
                {
                    listPickupUsers += $"<li> {pickupUser.Name} <a href='mailto:{pickupUser.Email}'>{pickupUser.Email}</a> {pickupUser.Phone}</li>";
                }
                body = body.Replace("@@@PickupUsers@@@", listPickupUsers);

            }

            if (emailCustomBookingFormInfo.BookingFormDeliveryUsers != null)
            {
                string listDeliveryUsers = string.Empty;
                foreach (var deliveryUser in emailCustomBookingFormInfo.BookingFormDeliveryUsers)
                {
                    listDeliveryUsers += $"<li> {deliveryUser.Name} <a href='mailto:{deliveryUser.Email}'>{deliveryUser.Email}</a> {deliveryUser.Phone}</li>";
                }
                body = body.Replace("@@@DeliveryUsers@@@", listDeliveryUsers);

            }
            return body;

        }


        public string FormatEmailBodyWarningCurrentNumberOfVialsInformation(string body, IEnumerable<MaterialsCurrentNumberOfVialsInfo> materialsCurrentNumberOfVialsInfo)
        {                        
            if (materialsCurrentNumberOfVialsInfo != null)
            {
                string listMaterialInfo = string.Empty;
                foreach (var numberOfVialsInfo in materialsCurrentNumberOfVialsInfo)
                {
                    listMaterialInfo += $"<li> {numberOfVialsInfo.ReferenceNumber} - Current Number of Vials: {numberOfVialsInfo.NewNumberOfVials} - Warning Threshold:{numberOfVialsInfo.WarningEmailCurrentNumberOfVialsThreshold} </li>";
                }
                body = body.Replace("@@@WarningMaterials@@@", listMaterialInfo);

            }
            
            return body;

        }

        private string FormatDate(DateTime? date)
        {
            if (date != null)
            {
                return $"{date.Value.Day.ToString("D2")}/{date.Value.Month.ToString("D2")}/{date.Value.Year.ToString("D4")}";
            }
            return String.Empty;
        }
    }

}
