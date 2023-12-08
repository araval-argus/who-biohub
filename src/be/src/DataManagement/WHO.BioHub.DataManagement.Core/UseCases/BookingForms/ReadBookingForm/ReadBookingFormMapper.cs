using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ReadBookingForm;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;

public interface IReadBookingFormMapper
{
    CourierBookingFormDto Map(BookingForm entity);
}

public class ReadBookingFormMapper : IReadBookingFormMapper
{
    public CourierBookingFormDto Map(BookingForm bookingForm)
    {        

        CourierBookingFormDto bookingformDto = new()
        {
            Id = bookingForm.Id,
            Date = bookingForm.Date,
            RequestDateOfPickup = bookingForm.RequestDateOfPickup,
            NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize,
            TemperatureTransportCondition = bookingForm.TemperatureTransportCondition,
            TransportCategoryId = bookingForm.TransportCategoryId,
            TransportCategoryName = bookingForm.TransportCategory.Name,
            TransportCategoryDescription = bookingForm.TransportCategory.Description,
            WorklistToBioHubItemId = bookingForm.WorklistToBioHubItemId,
            WorklistFromBioHubItemId = bookingForm.WorklistFromBioHubItemId,
            RequestingUserFirstName = GetRequestingUser(bookingForm) == null ? String.Empty : GetRequestingUser(bookingForm).FirstName,
            RequestingUserLastName = GetRequestingUser(bookingForm) == null ? String.Empty : GetRequestingUser(bookingForm).LastName,
            RequestingUserBusinessPhone = GetRequestingUser(bookingForm) == null ? String.Empty : GetRequestingUser(bookingForm).BusinessPhone,
            RequestingUserMobilePhone = GetRequestingUser(bookingForm) == null ? String.Empty : GetRequestingUser(bookingForm).MobilePhone,
            RequestingUserEmail = GetRequestingUser(bookingForm) == null ? String.Empty : GetRequestingUser(bookingForm).Email,
            RequestingUserJobTitle = GetRequestingUser(bookingForm) == null ? String.Empty : GetRequestingUser(bookingForm).JobTitle,
            ShipmentDirection = bookingForm.WorklistToBioHubItemId != null ? "Into BioHub (SMTA 1)" : "Out of BioHub (SMTA 2)",
            CourierId = bookingForm.CourierId,
            ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber,
            EstimateDateOfPickup = bookingForm.EstimateDateOfPickup,
            DateOfPickup = bookingForm.DateOfPickup,
            DateOfDelivery = bookingForm.DateOfDelivery,
            BookingFormPickupUsers = GetPickupUsers(bookingForm),
            BookingFormCourierUsers = GetCourierUsers(bookingForm),
            WorklistReferenceNumber = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.ReferenceNumber : bookingForm.WorklistToBioHubItem.ReferenceNumber,
            LaboratoryName = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationToLaboratory.Name : bookingForm.WorklistToBioHubItem.RequestInitiationFromLaboratory.Name,
            LaboratoryAbbreviation = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationToLaboratory.Abbreviation : bookingForm.WorklistToBioHubItem.RequestInitiationFromLaboratory.Abbreviation,
            LaboratoryAddress = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationToLaboratory.Address : bookingForm.WorklistToBioHubItem.RequestInitiationFromLaboratory.Address,
            LaboratoryCountry = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationToLaboratory.Country.Name : bookingForm.WorklistToBioHubItem.RequestInitiationFromLaboratory.Country.Name,
            BioHubFacilityName = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Name : bookingForm.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Name,
            BioHubFacilityAddress = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Address : bookingForm.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Address,
            BioHubFacilityCountry = bookingForm.WorklistFromBioHubItem != null ? bookingForm.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Country.Name : bookingForm.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Country.Name,
            BookingFormLaboratoryFocalPoints = GetLaboratoryFocalPoints(bookingForm),
            BookingFormBioHubFacilityFocalPoints = GetBioHubFacilityFocalPoints(bookingForm),
            WorklistFromBioHubItemMaterials = GetWorklistFromBioHubItemMaterials(bookingForm),
            MaterialShippingInformations = GetMaterialShippingInformations(bookingForm),
            TotalAmount = bookingForm.TotalAmount,
            TotalNumberOfVials = bookingForm.TotalNumberOfVials,
            TransportMode = bookingForm.TransportMode?.Name ?? string.Empty,
    };



        return bookingformDto;
    }

    public User? GetRequestingUser(BookingForm bookingForm)
    {
        if (bookingForm.WorklistFromBioHubItemId != null)
        {
            var worklistFromBioHubHistoryItem = bookingForm.WorklistFromBioHubItem.WorklistFromBioHubHistoryItems
                .Where(x => x.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval)
                .OrderByDescending(x => x.OperationDate)
                .FirstOrDefault();

            if (worklistFromBioHubHistoryItem != null)
            {
                return worklistFromBioHubHistoryItem.LastOperationUser;
            }
        }
        else if (bookingForm.WorklistToBioHubItemId != null)
        {
            var worklistToBioHubHistoryItem = bookingForm.WorklistToBioHubItem.WorklistToBioHubHistoryItems
                .Where(x => x.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
                .OrderByDescending(x => x.OperationDate)
                .FirstOrDefault();

            if (worklistToBioHubHistoryItem != null)
            {
                return worklistToBioHubHistoryItem.LastOperationUser;
            }
        }

        return null;
    }

    private IEnumerable<CourierBookingFormUserDto> GetCourierUsers(BookingForm bookingForm)
    {
        List<CourierBookingFormUserDto> courierUsersDto = new List<CourierBookingFormUserDto>();


        foreach (var courierUser in bookingForm.BookingFormCourierUsers)
        {
            CourierBookingFormUserDto courierUserDto = new CourierBookingFormUserDto();
            courierUserDto.Id = courierUser.Id;
            courierUserDto.UserId = courierUser.UserId;
            courierUserDto.BookingFormId = courierUser.BookingFormId;
            courierUserDto.Other = courierUser.Other;
            courierUserDto.Country = courierUser.User.Courier.Country.Name;
            courierUserDto.Laboratory = courierUser.User.Courier.Name;
            courierUserDto.UserName = courierUser.User.FirstName + " " + courierUser.User.LastName;
            courierUserDto.Email = courierUser.User.Email;
            courierUserDto.JobTitle = courierUser.User.JobTitle;
            courierUserDto.MobilePhone = courierUser.User.MobilePhone;
            courierUserDto.BusinessPhone = courierUser.User.BusinessPhone;


            courierUsersDto.Add(courierUserDto);
        }
        return courierUsersDto;
    }

    private IEnumerable<CourierBookingFormUserDto> GetPickupUsers(BookingForm bookingForm)
    {
        List<CourierBookingFormUserDto> pickupUsersDto = new List<CourierBookingFormUserDto>();


        foreach (var pickupUser in bookingForm.BookingFormPickupUsers)
        {
            CourierBookingFormUserDto pickupUserDto = new CourierBookingFormUserDto();
            pickupUserDto.Id = pickupUser.Id;
            pickupUserDto.UserId = pickupUser.UserId;
            pickupUserDto.BookingFormId = pickupUser.BookingFormId;
            pickupUserDto.Other = pickupUser.Other;
            pickupUserDto.Country = pickupUser.User.Laboratory == null ? pickupUser.User.BioHubFacility.Country.Name : pickupUser.User.Laboratory.Country.Name;
            pickupUserDto.Laboratory = pickupUser.User.Laboratory == null ? pickupUser.User.BioHubFacility.Name : pickupUser.User.Laboratory.Name;
            pickupUserDto.UserName = pickupUser.User.FirstName + " " + pickupUser.User.LastName;
            pickupUserDto.Email = pickupUser.User.Email;
            pickupUserDto.JobTitle = pickupUser.User.JobTitle;
            pickupUserDto.MobilePhone = pickupUser.User.MobilePhone;
            pickupUserDto.BusinessPhone = pickupUser.User.BusinessPhone;

            pickupUsersDto.Add(pickupUserDto);
        }
        return pickupUsersDto;
    }

    private IEnumerable<CourierBookingFormUserDto> GetLaboratoryFocalPoints(BookingForm bookingForm)
    {
        List<CourierBookingFormUserDto> laboratoryFocalPoints = new List<CourierBookingFormUserDto>();

        if (bookingForm.WorklistFromBioHubItemId == null)
        {
            foreach (var user in bookingForm.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Users)
            {
                if (user.DeletedOn == null && user.OperationalFocalPoint == true)
                {
                    CourierBookingFormUserDto laboratoryFocalPointDto = new CourierBookingFormUserDto();
                    laboratoryFocalPointDto.UserId = user.Id;
                    laboratoryFocalPointDto.UserName = user.FirstName + " " + user.LastName;

                    laboratoryFocalPointDto.Email = user.Email;
                    laboratoryFocalPointDto.BioHubFacilityId = user.BioHubFacilityId;
                    laboratoryFocalPointDto.JobTitle = user.JobTitle;
                    laboratoryFocalPointDto.MobilePhone = user.MobilePhone;
                    laboratoryFocalPointDto.BusinessPhone = user.BusinessPhone;
                    laboratoryFocalPointDto.Laboratory = user.BioHubFacility.Name;

                    laboratoryFocalPoints.Add(laboratoryFocalPointDto);
                }
            }
        }
        else
        {
            foreach (var laboratoryFocalPoint in bookingForm.WorklistFromBioHubItem.WorklistFromBioHubItemLaboratoryFocalPoints)
            {

                CourierBookingFormUserDto laboratoryFocalPointDto = new CourierBookingFormUserDto();
                laboratoryFocalPointDto.Id = laboratoryFocalPoint.Id;
                laboratoryFocalPointDto.UserId = laboratoryFocalPoint.UserId;
                laboratoryFocalPointDto.UserName = laboratoryFocalPoint.User.FirstName + " " + laboratoryFocalPoint.User.LastName;
                laboratoryFocalPointDto.Country = laboratoryFocalPoint.User.Laboratory.Country.Name;
                laboratoryFocalPointDto.Email = laboratoryFocalPoint.User.Email;
                laboratoryFocalPointDto.LaboratoryId = bookingForm.WorklistFromBioHubItem.RequestInitiationToLaboratoryId;
                laboratoryFocalPointDto.JobTitle = laboratoryFocalPoint.User.JobTitle;
                laboratoryFocalPointDto.MobilePhone = laboratoryFocalPoint.User.MobilePhone;
                laboratoryFocalPointDto.BusinessPhone = laboratoryFocalPoint.User.BusinessPhone;
                laboratoryFocalPointDto.Laboratory = laboratoryFocalPoint.User.Laboratory.Name;
                laboratoryFocalPointDto.Other = laboratoryFocalPoint.Other;
                laboratoryFocalPointDto.WorklistFromBioHubItemId = laboratoryFocalPoint.WorklistFromBioHubItemId;

                laboratoryFocalPoints.Add(laboratoryFocalPointDto);

            }
        }
        return laboratoryFocalPoints;
    }


    private IEnumerable<CourierBookingFormUserDto> GetBioHubFacilityFocalPoints(BookingForm bookingForm)
    {
        List<CourierBookingFormUserDto> bioHubFacilityFocalPointsDto = new List<CourierBookingFormUserDto>();

        if (bookingForm.WorklistToBioHubItem != null)
        {

            var bioHubFacilityFocalPoints = bookingForm.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Users
                .Where(x => x.DeletedOn == null)
                .Where(x => x.OperationalFocalPoint == true)
                .ToList();

            foreach (var bioHubFacilityFocalPoint in bioHubFacilityFocalPoints)
            {

                CourierBookingFormUserDto bioHubFacilityFocalPointDto = new CourierBookingFormUserDto();
                bioHubFacilityFocalPointDto.Id = Guid.NewGuid();
                bioHubFacilityFocalPointDto.UserId = bioHubFacilityFocalPoint.Id;
                bioHubFacilityFocalPointDto.UserName = bioHubFacilityFocalPoint.FirstName + " " + bioHubFacilityFocalPoint.LastName;
                bioHubFacilityFocalPointDto.BioHubFacilityId = bookingForm.WorklistToBioHubItem.RequestInitiationToBioHubFacilityId;
                bioHubFacilityFocalPointDto.JobTitle = bioHubFacilityFocalPoint.JobTitle;
                bioHubFacilityFocalPointDto.MobilePhone = bioHubFacilityFocalPoint.MobilePhone;
                bioHubFacilityFocalPointDto.BusinessPhone = bioHubFacilityFocalPoint.BusinessPhone;
                bioHubFacilityFocalPointDto.WorklistToBioHubItemId = bookingForm.WorklistToBioHubItemId;

                bioHubFacilityFocalPointsDto.Add(bioHubFacilityFocalPointDto);

            }
        }
        return bioHubFacilityFocalPointsDto;
    }

      


    private List<WorklistFromBioHubItemMaterialDto> GetWorklistFromBioHubItemMaterials(BookingForm bookingForm)
    {

        var worklistFromBioHubItemMaterials = new List<WorklistFromBioHubItemMaterialDto>();
        if (bookingForm.WorklistFromBioHubItemId != null)
        {

            foreach (var material in bookingForm.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Where(x => x.Material.TransportCategoryId == bookingForm.TransportCategoryId))
            {
                WorklistFromBioHubItemMaterialDto worklistFromBioHubItemMaterialDto = new WorklistFromBioHubItemMaterialDto();
                worklistFromBioHubItemMaterialDto.MaterialName = material.Material.Name;
                worklistFromBioHubItemMaterialDto.Amount = material.Amount;
                worklistFromBioHubItemMaterialDto.Condition = material.Condition;
                worklistFromBioHubItemMaterialDto.Id = material.Id;
                worklistFromBioHubItemMaterialDto.MaterialNumber = material.Material.ReferenceNumber;
                worklistFromBioHubItemMaterialDto.TransportCategoryId = material.Material.TransportCategoryId;
                worklistFromBioHubItemMaterialDto.Quantity = material.Quantity;
                worklistFromBioHubItemMaterials.Add(worklistFromBioHubItemMaterialDto);
            }
        }
        return worklistFromBioHubItemMaterials;
    }

    private List<MaterialShippingInformationDto> GetMaterialShippingInformations(BookingForm bookingForm)
    {

        var materialShippingInformations = new List<MaterialShippingInformationDto>();
        if (bookingForm.WorklistToBioHubItemId != null)
        {            
            foreach (var material in bookingForm.WorklistToBioHubItem.MaterialShippingInformations.Where(x => x.TransportCategoryId == bookingForm.TransportCategoryId))
            {
                MaterialShippingInformationDto materialShippingInformationDto = new MaterialShippingInformationDto();
                materialShippingInformationDto.AdditionalInformation = material.AdditionalInformation;
                materialShippingInformationDto.Amount = material.Amount;
                materialShippingInformationDto.Condition = material.Condition;
                materialShippingInformationDto.Id = material.Id;
                materialShippingInformationDto.MaterialNumber = material.MaterialNumber;
                materialShippingInformationDto.MaterialProductId = material.MaterialProductId;
                materialShippingInformationDto.TransportCategoryId = material.TransportCategoryId;
                materialShippingInformationDto.Quantity = material.Quantity;
                materialShippingInformations.Add(materialShippingInformationDto);
            }

        }
        return materialShippingInformations;
    }

}
