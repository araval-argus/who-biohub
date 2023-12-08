using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA1;

public interface IReadBookingFormOfSMTA1Handler
{
    Task<Either<ReadBookingFormOfSMTA1QueryResponse, Errors>> Handle(ReadBookingFormOfSMTA1Query query, CancellationToken cancellationToken);
}

public class ReadBookingFormOfSMTA1Handler : IReadBookingFormOfSMTA1Handler
{
    private readonly ILogger<ReadBookingFormOfSMTA1Handler> _logger;
    private readonly ReadBookingFormOfSMTA1QueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly ILaboratoryReadRepository _readLaboratoryRepository;
    private readonly IBioHubFacilityReadRepository _readBioHubFacilityRepository;
    private readonly IUserReadRepository _readUserRepository;
    private readonly ICourierReadRepository _readCourierRepository;

    public ReadBookingFormOfSMTA1Handler(
        ILogger<ReadBookingFormOfSMTA1Handler> logger,
        ReadBookingFormOfSMTA1QueryValidator validator,
        IWorklistToBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ILaboratoryReadRepository readLaboratoryRepository,
        IBioHubFacilityReadRepository readBioHubFacilityRepository,
        IUserReadRepository readUserRepository,
        ICourierReadRepository readCourierRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readLaboratoryRepository = readLaboratoryRepository;
        _readBioHubFacilityRepository = readBioHubFacilityRepository;
        _readUserRepository = readUserRepository;
        _readCourierRepository = readCourierRepository;
    }

    public async Task<Either<ReadBookingFormOfSMTA1QueryResponse, Errors>> Handle(
        ReadBookingFormOfSMTA1Query query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {

            WorklistToBioHubItem worklistToBioHubItem;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                    worklistToBioHubItem = await _readRepository.ReadBookingFormOfSMTA1Info(query.WorklistId, cancellationToken);
                    break;
                case RoleType.BioHubFacility:
                    worklistToBioHubItem = await _readRepository.ReadBookingFormOfSMTA1InfoForBioHubFacility(query.WorklistId, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);

                    break;
                case RoleType.Laboratory:
                    worklistToBioHubItem = await _readRepository.ReadBookingFormOfSMTA1InfoForLaboratory(query.WorklistId, query.LaboratoryId.GetValueOrDefault(), cancellationToken);

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }


            if (worklistToBioHubItem == null)
                return new(new Errors(ErrorType.NotFound, $"Booking Form Of SMTA 1 data with Id {query.WorklistId} not found"));

            DocumentTemplate bookingFormOfSmta1Document = null;

            if (worklistToBioHubItem.OriginalBookingFormOfSMTA1DocumentTemplateId != null)
            {
                bookingFormOfSmta1Document = await _readDocumentTemplateRepository.Read(worklistToBioHubItem.OriginalBookingFormOfSMTA1DocumentTemplateId.GetValueOrDefault(), cancellationToken);
            }

            var smta1Document = worklistToBioHubItem.WorklistToBioHubItemDocuments.FirstOrDefault(x => x.Type == DocumentFileType.SMTA1)?.Document;

            DateTime? approvalDate = worklistToBioHubItem.BookingFormOfSMTA1ApprovalDate; 

            var laboratory = worklistToBioHubItem.RequestInitiationFromLaboratory;
            var bioHubFacility = worklistToBioHubItem.RequestInitiationToBioHubFacility;
            bool isPast = worklistToBioHubItem.IsPast == true;

            BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel = new BookingFormOfSMTA1DataViewModel();

            bookingFormOfSMTA1DataViewModel.ShipmentRequestNumber = worklistToBioHubItem.ReferenceNumber;
            bookingFormOfSMTA1DataViewModel.BookingFormOfSMTA1SignatureText = worklistToBioHubItem.BookingFormOfSMTA1SignatureText;
            bookingFormOfSMTA1DataViewModel.SMTA1DocumentId = smta1Document?.Id;
            bookingFormOfSMTA1DataViewModel.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId = bookingFormOfSmta1Document?.Id;
            bookingFormOfSMTA1DataViewModel.SMTA1DocumentName = $"{smta1Document?.Name}.{smta1Document?.Extension.ToLower()}";
            bookingFormOfSMTA1DataViewModel.OriginalDocumentTemplateBookingFormOfSMTA1DocumentName = $"{bookingFormOfSmta1Document?.Name}.{bookingFormOfSmta1Document?.Extension.ToLower()}";
            bookingFormOfSMTA1DataViewModel.ApprovalDate = approvalDate;

            await SetLaboratoryInformation(bookingFormOfSMTA1DataViewModel, laboratory, approvalDate.GetValueOrDefault(), isPast,cancellationToken);
            await SetBioHubFacilityInformation(bookingFormOfSMTA1DataViewModel, bioHubFacility, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            
            bookingFormOfSMTA1DataViewModel.BookingForms = await GetBookingForms(worklistToBioHubItem, query.UserPermissions, approvalDate.GetValueOrDefault(), bookingFormOfSMTA1DataViewModel.LaboratoryName, bookingFormOfSMTA1DataViewModel.LaboratoryCountry, isPast, cancellationToken);
            
            bookingFormOfSMTA1DataViewModel.MaterialShippingInformations = GetMaterialShippingInformations(worklistToBioHubItem);
            bookingFormOfSMTA1DataViewModel.BioHubFacilityFocalPoints = await GetBioHubFacilityFocalPoints(worklistToBioHubItem, approvalDate.GetValueOrDefault(), isPast, cancellationToken);


            var requestUser = worklistToBioHubItem.WorklistToBioHubHistoryItems.Where(x => x.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval).OrderByDescending(x => x.OperationDate).FirstOrDefault()?.LastOperationUser;

            await SetRequestingUserInformationInformation(bookingFormOfSMTA1DataViewModel, requestUser, approvalDate.GetValueOrDefault(), isPast, cancellationToken);

            var couriers = worklistToBioHubItem.BookingForms.Select(x => x.Courier).ToList();

            await SetCouriers(bookingFormOfSMTA1DataViewModel, couriers, approvalDate.GetValueOrDefault(), query.UserPermissions, isPast, cancellationToken);

            return new(new ReadBookingFormOfSMTA1QueryResponse(bookingFormOfSMTA1DataViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Kpi");
            throw;
        }
    }


    private async Task SetLaboratoryInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, Laboratory? laboratory, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (laboratory != null)
        {
            if (laboratory.LastOperationDate != null && laboratory.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastLaboratoryInformation(bookingFormOfSMTA1DataViewModel, laboratory, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentLaboratoryInformation(bookingFormOfSMTA1DataViewModel, laboratory);
            }
        }
    }

    private void SetCurrentLaboratoryInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, Laboratory? laboratory)
    {
        bookingFormOfSMTA1DataViewModel.LaboratoryName = laboratory != null ? laboratory.Name : string.Empty;
        bookingFormOfSMTA1DataViewModel.LaboratoryAddress = laboratory != null ? laboratory.Address : string.Empty;
        bookingFormOfSMTA1DataViewModel.LaboratoryCountry = laboratory != null ? laboratory.Country.Name : string.Empty;
    }

    private async Task SetPastLaboratoryInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, Laboratory? laboratory, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var laboratoryHistory = await _readLaboratoryRepository.ReadPastInformation(laboratory.Id, approvalDate, cancellationToken);
        if (laboratoryHistory != null)
        {
            bookingFormOfSMTA1DataViewModel.LaboratoryName = laboratoryHistory != null ? laboratoryHistory.Name : string.Empty;
            bookingFormOfSMTA1DataViewModel.LaboratoryAddress = laboratoryHistory != null ? laboratoryHistory.Address : string.Empty;
            bookingFormOfSMTA1DataViewModel.LaboratoryCountry = laboratoryHistory != null ? laboratoryHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentLaboratoryInformation(bookingFormOfSMTA1DataViewModel, laboratory);
        }
    }



    private async Task SetBioHubFacilityInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (bioHubFacility != null)
        {
            if (bioHubFacility.LastOperationDate != null && bioHubFacility.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastBioHubFacilityInformation(bookingFormOfSMTA1DataViewModel, bioHubFacility, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentBioHubFacilityInformation(bookingFormOfSMTA1DataViewModel, bioHubFacility);
            }
        }
    }

    private void SetCurrentBioHubFacilityInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, BioHubFacility? bioHubFacility)
    {
        bookingFormOfSMTA1DataViewModel.BioHubFacilityName = bioHubFacility != null ? bioHubFacility.Name : string.Empty;
        bookingFormOfSMTA1DataViewModel.BioHubFacilityAddress = bioHubFacility != null ? bioHubFacility.Address : string.Empty;
        bookingFormOfSMTA1DataViewModel.BioHubFacilityCountry = bioHubFacility != null ? bioHubFacility.Country.Name : string.Empty;
    }

    private async Task SetPastBioHubFacilityInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var bioHubFacilityHistory = await _readBioHubFacilityRepository.ReadPastInformation(bioHubFacility.Id, approvalDate, cancellationToken);
        if (bioHubFacilityHistory != null)
        {
            bookingFormOfSMTA1DataViewModel.BioHubFacilityName = bioHubFacilityHistory != null ? bioHubFacilityHistory.Name : string.Empty;
            bookingFormOfSMTA1DataViewModel.BioHubFacilityAddress = bioHubFacilityHistory != null ? bioHubFacilityHistory.Address : string.Empty;
            bookingFormOfSMTA1DataViewModel.BioHubFacilityCountry = bioHubFacilityHistory != null ? bioHubFacilityHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentBioHubFacilityInformation(bookingFormOfSMTA1DataViewModel, bioHubFacility);
        }
    }



    private async Task SetRequestingUserInformationInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, User? user, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (user != null)
        {
            if (user.LastOperationDate != null && user.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastRequestingUserInformation(bookingFormOfSMTA1DataViewModel, user, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentRequestingUserInformation(bookingFormOfSMTA1DataViewModel, user);
            }
        }
    }

    private void SetCurrentRequestingUserInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, User? user)
    {
        bookingFormOfSMTA1DataViewModel.RequestUserFirstName = user?.FirstName ?? string.Empty;
        bookingFormOfSMTA1DataViewModel.RequestUserLastName = user?.LastName ?? string.Empty;
        bookingFormOfSMTA1DataViewModel.RequestUserEmail = user?.Email ?? string.Empty;
        bookingFormOfSMTA1DataViewModel.RequestUserJobTitle = user?.JobTitle ?? string.Empty;
        bookingFormOfSMTA1DataViewModel.RequestUserBusinessPhone = user?.BusinessPhone ?? string.Empty;
        bookingFormOfSMTA1DataViewModel.RequestUserMobilePhone = user?.MobilePhone ?? string.Empty;
    }

    private async Task SetPastRequestingUserInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, User? user, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var userHistory = await _readUserRepository.ReadPastInformation(user.Id, approvalDate, cancellationToken);
        if (userHistory != null)
        {
            bookingFormOfSMTA1DataViewModel.RequestUserFirstName = userHistory?.FirstName ?? string.Empty;
            bookingFormOfSMTA1DataViewModel.RequestUserLastName = userHistory?.LastName ?? string.Empty;
            bookingFormOfSMTA1DataViewModel.RequestUserEmail = userHistory?.Email ?? string.Empty;
            bookingFormOfSMTA1DataViewModel.RequestUserJobTitle = userHistory?.JobTitle ?? string.Empty;
            bookingFormOfSMTA1DataViewModel.RequestUserBusinessPhone = userHistory?.BusinessPhone ?? string.Empty;
            bookingFormOfSMTA1DataViewModel.RequestUserMobilePhone = userHistory?.MobilePhone ?? string.Empty;
        }
        else
        {
            SetCurrentRequestingUserInformation(bookingFormOfSMTA1DataViewModel, user);
        }
    }



    private async Task<IEnumerable<WorklistItemUserDto>> GetBioHubFacilityFocalPoints(WorklistToBioHubItem entity, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        List<WorklistItemUserDto> bioHubFacilityFocalPointsDto = new List<WorklistItemUserDto>();

        var bioHubFacilityFocalPoints = entity.RequestInitiationToBioHubFacility.Users
            .Where(x => x.DeletedOn == null)
            .Where(x => x.OperationalFocalPoint == true)
            .ToList();

        foreach (var bioHubFacilityFocalPoint in bioHubFacilityFocalPoints)
        {

            WorklistItemUserDto bioHubFacilityFocalPointDto = new WorklistItemUserDto();
            bioHubFacilityFocalPointDto.Id = Guid.NewGuid();
            
            bioHubFacilityFocalPointDto.BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId;
            bioHubFacilityFocalPointDto.WorklistItemId = entity.Id;
            bioHubFacilityFocalPointDto.UserId = bioHubFacilityFocalPoint.Id;                       

            await SetUserInformation(bioHubFacilityFocalPointDto, bioHubFacilityFocalPoint, approvalDate, isPast, cancellationToken);

            bioHubFacilityFocalPointsDto.Add(bioHubFacilityFocalPointDto);

        }
        return bioHubFacilityFocalPointsDto;
    }



    private async Task SetUserInformation(WorklistItemUserDto userDto, User? user, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (user != null)
        {
            if (user.LastOperationDate != null && user.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastUserInformation(userDto, user, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentUserInformation(userDto, user);
            }
        }
    }

    private void SetCurrentUserInformation(WorklistItemUserDto userDto, User? user)
    {
        userDto.UserName = user.FirstName + " " + user.LastName;
        userDto.Email = user.Email;
        userDto.JobTitle = user.JobTitle;
        userDto.MobilePhone = user.MobilePhone;
        userDto.BusinessPhone = user.BusinessPhone;
    }

    private async Task SetPastUserInformation(WorklistItemUserDto userDto, User? user, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var userHistory = await _readUserRepository.ReadPastInformation(user.Id, approvalDate, cancellationToken);
        if (userHistory != null)
        {
            userDto.UserName = userHistory.FirstName + " " + userHistory.LastName;
            userDto.Email = userHistory.Email;
            userDto.JobTitle = userHistory.JobTitle;
            userDto.MobilePhone = userHistory.MobilePhone;
            userDto.BusinessPhone = userHistory.BusinessPhone;
        }
        else
        {
            SetCurrentUserInformation(userDto, user);
        }
    }

    private async Task<IEnumerable<BookingFormOfSMTADto>> GetBookingForms(WorklistToBioHubItem entity, IEnumerable<string> userPermissions, DateTime approvalDate, string laboratoryName, string laboratoryCountry, bool isPast, CancellationToken cancellationToken)
    {
        List<BookingFormOfSMTADto> bookingForms = new List<BookingFormOfSMTADto>();
        var orderedList = entity.BookingForms.OrderBy(x => x.TransportCategory.Description);
        foreach (var bookingForm in orderedList)
        {

            BookingFormOfSMTADto bookingFormDto = new BookingFormOfSMTADto();
            bookingFormDto.Id = bookingForm.Id;
            bookingFormDto.Date = bookingForm.Date;
            bookingFormDto.RequestDateOfPickup = bookingForm.RequestDateOfPickup;
            bookingFormDto.TotalAmount = bookingForm.TotalAmount;
            bookingFormDto.TotalNumberOfVials = bookingForm.TotalNumberOfVials;
            bookingFormDto.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
            bookingFormDto.TemperatureTransportCondition = bookingForm.TemperatureTransportCondition;
            bookingFormDto.TransportCategoryId = bookingForm.TransportCategoryId;
            bookingFormDto.TransportCategoryName = bookingForm.TransportCategory.Name;
            bookingFormDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
            bookingFormDto.TransportModeId = bookingForm.TransportModeId;
            bookingFormDto.TransportModeName = bookingForm.TransportMode?.Name ?? string.Empty;
            bookingFormDto.TransportModeDescription = bookingForm.TransportMode?.Description ?? string.Empty;
            bookingFormDto.WorklistItemId = bookingForm.WorklistToBioHubItemId;            

            bookingFormDto.BookingFormPickupUsers = new List<WorklistItemUserDto>();
            foreach (var bookingFormPickupUser in bookingForm.BookingFormPickupUsers)
            {
                WorklistItemUserDto bookingFormPickupUserDto = new WorklistItemUserDto();
                bookingFormPickupUserDto.Id = bookingFormPickupUser.Id;
                bookingFormPickupUserDto.UserId = bookingFormPickupUser.UserId;
                bookingFormPickupUserDto.BookingFormId = bookingFormPickupUser.BookingFormId;
                bookingFormPickupUserDto.Other = bookingFormPickupUser.Other;
                bookingFormPickupUserDto.Country = laboratoryName;
                bookingFormPickupUserDto.Laboratory = laboratoryCountry;

                var user = bookingFormPickupUser.User;
                await SetUserInformation(bookingFormPickupUserDto, user, approvalDate, isPast, cancellationToken);                

                bookingFormDto.BookingFormPickupUsers.Add(bookingFormPickupUserDto);
            }

            
            bookingFormDto.EstimateDateOfPickup = bookingForm.EstimateDateOfPickup;

            
            bookingFormDto.BookingFormCourierUsers = new List<WorklistItemUserDto>();

            if (userPermissions.Contains(PermissionNames.CanReadCourier))
            {
                bookingFormDto.CourierId = bookingForm.CourierId;                

                foreach (var bookingFormCourierUser in bookingForm.BookingFormCourierUsers)
                {
                    WorklistItemUserDto bookingFormCourierUserDto = new WorklistItemUserDto();

                    bookingFormCourierUserDto.Id = bookingFormCourierUser.Id;
                    bookingFormCourierUserDto.UserId = bookingFormCourierUser.UserId;
                    bookingFormCourierUserDto.BookingFormId = bookingFormCourierUser.BookingFormId;
                    bookingFormCourierUserDto.Country = bookingFormCourierUser.User.Courier.Country.Name;
                    bookingFormCourierUserDto.CourierId = bookingForm.CourierId;
                    bookingFormCourierUserDto.Other = bookingFormCourierUser.Other;

                    var user = bookingFormCourierUser.User;
                    
                    await SetUserInformation(bookingFormCourierUserDto, user, approvalDate, isPast, cancellationToken);


                    bookingFormDto.BookingFormCourierUsers.Add(bookingFormCourierUserDto);
                }


                bookingFormDto.DateOfPickup = bookingForm.DateOfPickup;
                bookingFormDto.DateOfDelivery = bookingForm.DateOfDelivery;
                bookingFormDto.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;
                bookingFormDto.TransportModeId = bookingForm.TransportModeId;
            }

            bookingForms.Add(bookingFormDto);

        }
        return bookingForms;
    }

    private async Task SetCouriers(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, List<Courier> couriers, DateTime approvalDate, IEnumerable<string> userPermissions, bool isPast, CancellationToken cancellationToken)
    {
        bookingFormOfSMTA1DataViewModel.Couriers = new List<CourierViewModel>();

        if (userPermissions.Contains(PermissionNames.CanReadCourier) && couriers != null)
        {
            foreach (var courier in couriers)
            {
                if (courier != null)
                {
                    if (courier.LastOperationDate != null && courier.LastOperationDate > approvalDate && !isPast)
                    {
                        await SetPastCourierInformation(bookingFormOfSMTA1DataViewModel, courier, approvalDate, cancellationToken);
                    }
                    else
                    {
                        SetCurrentCourierInformation(bookingFormOfSMTA1DataViewModel, courier);
                    }
                }
            }
        }
    }

    private void SetCurrentCourierInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, Courier courier)
    {
        CourierViewModel courierViewModel = new CourierViewModel();
        courierViewModel.Id = courier.Id;
        courierViewModel.Address = courier.Address;
        courierViewModel.BusinessPhone = courier.BusinessPhone;
        courierViewModel.CountryId = courier.CountryId;
        courierViewModel.Description = courier.Description;
        courierViewModel.Email = courier.Email;
        courierViewModel.IsActive = courier.IsActive;
        courierViewModel.Latitude = courier.Latitude;
        courierViewModel.Longitude = courier.Longitude;
        courierViewModel.Name = courier.Name;
        courierViewModel.WHOAccountNumber = courier.WHOAccountNumber;

        bookingFormOfSMTA1DataViewModel.Couriers.Add(courierViewModel);
    }

    private async Task SetPastCourierInformation(BookingFormOfSMTA1DataViewModel bookingFormOfSMTA1DataViewModel, Courier courier, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var courierHistory = await _readCourierRepository.ReadPastInformation(courier.Id, approvalDate, cancellationToken);
        if (courierHistory != null)
        {
            CourierViewModel courierViewModel = new CourierViewModel();
            courierViewModel.Id = courier.Id;
            courierViewModel.Address = courierHistory.Address;
            courierViewModel.BusinessPhone = courierHistory.BusinessPhone;
            courierViewModel.CountryId = courierHistory.CountryId;
            courierViewModel.Description = courierHistory.Description;
            courierViewModel.Email = courierHistory.Email;
            courierViewModel.IsActive = courierHistory.IsActive;
            courierViewModel.Latitude = courierHistory.Latitude;
            courierViewModel.Longitude = courierHistory.Longitude;
            courierViewModel.Name = courierHistory.Name;
            courierViewModel.WHOAccountNumber = courierHistory.WHOAccountNumber;

            bookingFormOfSMTA1DataViewModel.Couriers.Add(courierViewModel);
        }
        else
        {
            SetCurrentCourierInformation(bookingFormOfSMTA1DataViewModel, courier);
        }

    }





    private IEnumerable<MaterialShippingInformationDto> GetMaterialShippingInformations(WorklistToBioHubItem worklistToBioHubItem)
    {
        List<MaterialShippingInformationDto> materialShippingInformations = new List<MaterialShippingInformationDto>();
        foreach (var materialShippingInformation in worklistToBioHubItem.MaterialShippingInformations)
        {

            MaterialShippingInformationDto materialShippingInformationDto = new MaterialShippingInformationDto();
            materialShippingInformationDto.Id = materialShippingInformation.Id;
            materialShippingInformationDto.MaterialNumber = materialShippingInformation.MaterialNumber;
            materialShippingInformationDto.AdditionalInformation = materialShippingInformation.AdditionalInformation;
            materialShippingInformationDto.Condition = materialShippingInformation.Condition;
            materialShippingInformationDto.Quantity = materialShippingInformation.Quantity;
            materialShippingInformationDto.MaterialProductId = materialShippingInformation.MaterialProductId;
            materialShippingInformationDto.TransportCategoryId = materialShippingInformation.TransportCategoryId;
            materialShippingInformationDto.Amount = materialShippingInformation.Amount;
            materialShippingInformationDto.MaterialClinicalDetails = new List<MaterialClinicalDetailDto>();
            if (materialShippingInformation.MaterialClinicalDetails != null)
            {
                var materialClinicalDetails = materialShippingInformation.MaterialClinicalDetails;


                foreach (var materialClinicalDetail in materialClinicalDetails)
                {
                    MaterialClinicalDetailDto materialClinicalDetailDto = new MaterialClinicalDetailDto();
                    materialClinicalDetailDto.Id = materialClinicalDetail.Id;
                    materialClinicalDetailDto.MaterialShippingInformationId = materialShippingInformationDto.Id;
                    materialClinicalDetailDto.MaterialNumber = materialClinicalDetail.MaterialNumber;
                    materialClinicalDetailDto.MaterialProductId = materialShippingInformation.MaterialProductId;
                    materialClinicalDetailDto.TransportCategoryId = materialShippingInformation.TransportCategoryId;
                    materialClinicalDetailDto.Gender = materialClinicalDetail.Gender;
                    materialClinicalDetailDto.PatientStatus = materialClinicalDetail.PatientStatus;
                    materialClinicalDetailDto.CollectionDate = materialClinicalDetail.CollectionDate;
                    materialClinicalDetailDto.Age = materialClinicalDetail.Age;
                    materialClinicalDetailDto.IsolationHostTypeId = materialClinicalDetail.IsolationHostTypeId;
                    materialClinicalDetailDto.Location = materialClinicalDetail.Location;
                    materialClinicalDetailDto.Note = materialClinicalDetail.Note;
                    materialClinicalDetailDto.Condition = materialClinicalDetail.Condition;
                    materialShippingInformationDto.MaterialClinicalDetails.Add(materialClinicalDetailDto);
                }
            }
            materialShippingInformationDto.MaterialLaboratoryAnalysisInformation = new List<MaterialLaboratoryAnalysisInformationDto>();
            if (materialShippingInformation.MaterialLaboratoryAnalysisInformation != null)
            {
                var materialLaboratoryAnalysisInformationElements = materialShippingInformation.MaterialLaboratoryAnalysisInformation;

                foreach (var materialLaboratoryAnalysisInformationElement in materialLaboratoryAnalysisInformationElements)
                {
                    MaterialLaboratoryAnalysisInformationDto materialLaboratoryAnalysisInformationDto = new MaterialLaboratoryAnalysisInformationDto();
                    materialLaboratoryAnalysisInformationDto.Id = materialLaboratoryAnalysisInformationElement.Id;
                    materialLaboratoryAnalysisInformationDto.MaterialShippingInformationId = materialShippingInformationDto.Id;
                    materialLaboratoryAnalysisInformationDto.MaterialNumber = materialLaboratoryAnalysisInformationElement.MaterialNumber;
                    materialLaboratoryAnalysisInformationDto.UnitOfMeasureId = materialLaboratoryAnalysisInformationElement.UnitOfMeasureId;
                    materialLaboratoryAnalysisInformationDto.Temperature = materialLaboratoryAnalysisInformationElement.Temperature;
                    materialLaboratoryAnalysisInformationDto.VirusConcentration = materialLaboratoryAnalysisInformationElement.VirusConcentration;
                    materialLaboratoryAnalysisInformationDto.MaterialNumber = materialLaboratoryAnalysisInformationElement.MaterialNumber;
                    materialLaboratoryAnalysisInformationDto.CulturingPassagesNumber = materialLaboratoryAnalysisInformationElement.CulturingPassagesNumber;
                    materialLaboratoryAnalysisInformationDto.BrandOfTransportMedium = materialLaboratoryAnalysisInformationElement.BrandOfTransportMedium;
                    materialLaboratoryAnalysisInformationDto.TypeOfTransportMedium = materialLaboratoryAnalysisInformationElement.TypeOfTransportMedium;
                    materialLaboratoryAnalysisInformationDto.CulturingCellLine = materialLaboratoryAnalysisInformationElement.CulturingCellLine;
                    materialLaboratoryAnalysisInformationDto.FreezingDate = materialLaboratoryAnalysisInformationElement.FreezingDate;
                    materialLaboratoryAnalysisInformationDto.GSDUploadedToDatabase = materialLaboratoryAnalysisInformationElement.GSDUploadedToDatabase;
                    materialLaboratoryAnalysisInformationDto.AccessionNumberInGSDDatabase = materialLaboratoryAnalysisInformationElement.AccessionNumberInGSDDatabase;
                    materialLaboratoryAnalysisInformationDto.DatabaseUsedForGSDUploadingId = materialLaboratoryAnalysisInformationElement.DatabaseUsedForGSDUploadingId;
                    materialLaboratoryAnalysisInformationDto.CollectedSpecimenTypes = new List<Guid?>();
                    foreach (var elem in materialLaboratoryAnalysisInformationElement.CollectedSpecimenTypes)
                    {
                        materialLaboratoryAnalysisInformationDto.CollectedSpecimenTypes.Add(elem.SpecimenTypeId);
                    }

                    materialShippingInformationDto.MaterialLaboratoryAnalysisInformation.Add(materialLaboratoryAnalysisInformationDto);
                }
            }


            materialShippingInformations.Add(materialShippingInformationDto);

        }
        return materialShippingInformations;
    }
}