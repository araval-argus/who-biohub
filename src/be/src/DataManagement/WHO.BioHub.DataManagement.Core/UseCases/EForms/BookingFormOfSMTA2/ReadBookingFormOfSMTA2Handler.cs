using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA2;

public interface IReadBookingFormOfSMTA2Handler
{
    Task<Either<ReadBookingFormOfSMTA2QueryResponse, Errors>> Handle(ReadBookingFormOfSMTA2Query query, CancellationToken cancellationToken);
}

public class ReadBookingFormOfSMTA2Handler : IReadBookingFormOfSMTA2Handler
{
    private readonly ILogger<ReadBookingFormOfSMTA2Handler> _logger;
    private readonly ReadBookingFormOfSMTA2QueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly ILaboratoryReadRepository _readLaboratoryRepository;
    private readonly IBioHubFacilityReadRepository _readBioHubFacilityRepository;
    private readonly IUserReadRepository _readUserRepository;
    private readonly ICourierReadRepository _readCourierRepository;
    private readonly IMaterialReadRepository _readMaterialRepository;

    public ReadBookingFormOfSMTA2Handler(
        ILogger<ReadBookingFormOfSMTA2Handler> logger,
        ReadBookingFormOfSMTA2QueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ILaboratoryReadRepository readLaboratoryRepository,
        IBioHubFacilityReadRepository readBioHubFacilityRepository,
        IUserReadRepository readUserRepository,
        ICourierReadRepository readCourierRepository,
        IMaterialReadRepository readMaterialRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readLaboratoryRepository = readLaboratoryRepository;
        _readBioHubFacilityRepository = readBioHubFacilityRepository;
        _readUserRepository = readUserRepository;
        _readCourierRepository = readCourierRepository;
        _readMaterialRepository = readMaterialRepository;
    }

    public async Task<Either<ReadBookingFormOfSMTA2QueryResponse, Errors>> Handle(
        ReadBookingFormOfSMTA2Query query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {

            WorklistFromBioHubItem worklistFromBioHubItem;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                    worklistFromBioHubItem = await _readRepository.ReadBookingFormOfSMTA2Info(query.WorklistId, cancellationToken);
                    break;
                case RoleType.BioHubFacility:
                    worklistFromBioHubItem = await _readRepository.ReadBookingFormOfSMTA2InfoForBioHubFacility(query.WorklistId, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);

                    break;
                case RoleType.Laboratory:
                    worklistFromBioHubItem = await _readRepository.ReadBookingFormOfSMTA2InfoForLaboratory(query.WorklistId, query.LaboratoryId.GetValueOrDefault(), cancellationToken);

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }


            if (worklistFromBioHubItem == null)
                return new(new Errors(ErrorType.NotFound, $"Annex 2 Of SMTA 2 data with Id {query.WorklistId} not found"));

            DocumentTemplate bookingFormOfSmta2Document = null;

            if (worklistFromBioHubItem.OriginalBookingFormOfSMTA2DocumentTemplateId != null)
            {
                bookingFormOfSmta2Document = await _readDocumentTemplateRepository.Read(worklistFromBioHubItem.OriginalBookingFormOfSMTA2DocumentTemplateId.GetValueOrDefault(), cancellationToken);
            }
            
            var smta2Document = worklistFromBioHubItem.WorklistFromBioHubItemDocuments.FirstOrDefault(x => x.Type == DocumentFileType.SMTA2)?.Document;

            DateTime? approvalDate = worklistFromBioHubItem.BookingFormOfSMTA2ApprovalDate;

            var laboratory = worklistFromBioHubItem.RequestInitiationToLaboratory;
            var bioHubFacility = worklistFromBioHubItem.RequestInitiationFromBioHubFacility;
            bool isPast = worklistFromBioHubItem.IsPast == true;

            BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel = new BookingFormOfSMTA2DataViewModel();

            bookingFormOfSMTA2DataViewModel.ShipmentRequestNumber = worklistFromBioHubItem.ReferenceNumber;
            bookingFormOfSMTA2DataViewModel.BookingFormOfSMTA2SignatureText = worklistFromBioHubItem.BookingFormOfSMTA2SignatureText;
            bookingFormOfSMTA2DataViewModel.SMTA2DocumentId = smta2Document?.Id;
            bookingFormOfSMTA2DataViewModel.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId = bookingFormOfSmta2Document?.Id;
            bookingFormOfSMTA2DataViewModel.SMTA2DocumentName = $"{smta2Document?.Name}.{smta2Document?.Extension.ToLower()}";
            bookingFormOfSMTA2DataViewModel.OriginalDocumentTemplateBookingFormOfSMTA2DocumentName = $"{bookingFormOfSmta2Document?.Name}.{bookingFormOfSmta2Document?.Extension.ToLower()}";

                       
           
            bookingFormOfSMTA2DataViewModel.ApprovalDate = approvalDate;

            await SetLaboratoryInformation(bookingFormOfSMTA2DataViewModel, laboratory, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            await SetBioHubFacilityInformation(bookingFormOfSMTA2DataViewModel, bioHubFacility, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
                                                 
            
            
            
            bookingFormOfSMTA2DataViewModel.WorklistFromBioHubItemMaterials = await GetMaterials(worklistFromBioHubItem, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            bookingFormOfSMTA2DataViewModel.LaboratoryFocalPoints = await GetLaboratoryFocalPoints(worklistFromBioHubItem, bookingFormOfSMTA2DataViewModel.LaboratoryName, bookingFormOfSMTA2DataViewModel.LaboratoryCountry, approvalDate.GetValueOrDefault(), isPast, cancellationToken);

            var requestUser = worklistFromBioHubItem.WorklistFromBioHubHistoryItems.Where(x => x.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval).OrderByDescending(x => x.OperationDate).FirstOrDefault()?.LastOperationUser;

                        
            await SetRequestingUserInformationInformation(bookingFormOfSMTA2DataViewModel, requestUser, approvalDate.GetValueOrDefault(), isPast, cancellationToken);

            bookingFormOfSMTA2DataViewModel.BookingForms = await GetBookingForms(worklistFromBioHubItem, query.UserPermissions, approvalDate.GetValueOrDefault(), bookingFormOfSMTA2DataViewModel.BioHubFacilityName, bookingFormOfSMTA2DataViewModel.BioHubFacilityCountry, isPast, cancellationToken);

            var couriers = worklistFromBioHubItem.BookingForms.Select(x => x.Courier).ToList();

            await SetCouriers(bookingFormOfSMTA2DataViewModel, couriers, approvalDate.GetValueOrDefault(), query.UserPermissions, isPast, cancellationToken);


            return new(new ReadBookingFormOfSMTA2QueryResponse(bookingFormOfSMTA2DataViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Kpi");
            throw;
        }
    }


    private async Task SetLaboratoryInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, Laboratory? laboratory, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (laboratory != null)
        {
            if (laboratory.LastOperationDate != null && laboratory.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastLaboratoryInformation(bookingFormOfSMTA2DataViewModel, laboratory, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentLaboratoryInformation(bookingFormOfSMTA2DataViewModel, laboratory);
            }
        }
    }

    private void SetCurrentLaboratoryInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, Laboratory? laboratory)
    {
        bookingFormOfSMTA2DataViewModel.LaboratoryName = laboratory != null ? laboratory.Name : string.Empty;
        bookingFormOfSMTA2DataViewModel.LaboratoryAddress = laboratory != null ? laboratory.Address : string.Empty;
        bookingFormOfSMTA2DataViewModel.LaboratoryCountry = laboratory != null ? laboratory.Country.Name : string.Empty;
    }

    private async Task SetPastLaboratoryInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, Laboratory? laboratory, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var laboratoryHistory = await _readLaboratoryRepository.ReadPastInformation(laboratory.Id, approvalDate, cancellationToken);
        if (laboratoryHistory != null)
        {
            bookingFormOfSMTA2DataViewModel.LaboratoryName = laboratoryHistory != null ? laboratoryHistory.Name : string.Empty;
            bookingFormOfSMTA2DataViewModel.LaboratoryAddress = laboratoryHistory != null ? laboratoryHistory.Address : string.Empty;
            bookingFormOfSMTA2DataViewModel.LaboratoryCountry = laboratoryHistory != null ? laboratoryHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentLaboratoryInformation(bookingFormOfSMTA2DataViewModel, laboratory);
        }
    }



    private async Task SetBioHubFacilityInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (bioHubFacility != null)
        {
            if (bioHubFacility.LastOperationDate != null && bioHubFacility.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastBioHubFacilityInformation(bookingFormOfSMTA2DataViewModel, bioHubFacility, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentBioHubFacilityInformation(bookingFormOfSMTA2DataViewModel, bioHubFacility);
            }
        }
    }

    private void SetCurrentBioHubFacilityInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, BioHubFacility? bioHubFacility)
    {
        bookingFormOfSMTA2DataViewModel.BioHubFacilityName = bioHubFacility != null ? bioHubFacility.Name : string.Empty;
        bookingFormOfSMTA2DataViewModel.BioHubFacilityAddress = bioHubFacility != null ? bioHubFacility.Address : string.Empty;
        bookingFormOfSMTA2DataViewModel.BioHubFacilityCountry = bioHubFacility != null ? bioHubFacility.Country.Name : string.Empty;
    }

    private async Task SetPastBioHubFacilityInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var bioHubFacilityHistory = await _readBioHubFacilityRepository.ReadPastInformation(bioHubFacility.Id, approvalDate, cancellationToken);
        if (bioHubFacilityHistory != null)
        {
            bookingFormOfSMTA2DataViewModel.BioHubFacilityName = bioHubFacilityHistory != null ? bioHubFacilityHistory.Name : string.Empty;
            bookingFormOfSMTA2DataViewModel.BioHubFacilityAddress = bioHubFacilityHistory != null ? bioHubFacilityHistory.Address : string.Empty;
            bookingFormOfSMTA2DataViewModel.BioHubFacilityCountry = bioHubFacilityHistory != null ? bioHubFacilityHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentBioHubFacilityInformation(bookingFormOfSMTA2DataViewModel, bioHubFacility);
        }
    }



    private async Task SetRequestingUserInformationInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, User? user, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (user != null)
        {
            if (user.LastOperationDate != null && user.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastRequestingUserInformation(bookingFormOfSMTA2DataViewModel, user, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentRequestingUserInformation(bookingFormOfSMTA2DataViewModel, user);
            }
        }
    }

    private void SetCurrentRequestingUserInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, User? user)
    {
        bookingFormOfSMTA2DataViewModel.RequestUserFirstName = user?.FirstName ?? string.Empty;
        bookingFormOfSMTA2DataViewModel.RequestUserLastName = user?.LastName ?? string.Empty;
        bookingFormOfSMTA2DataViewModel.RequestUserEmail = user?.Email ?? string.Empty;
        bookingFormOfSMTA2DataViewModel.RequestUserJobTitle = user?.JobTitle ?? string.Empty;
        bookingFormOfSMTA2DataViewModel.RequestUserBusinessPhone = user?.BusinessPhone ?? string.Empty;
        bookingFormOfSMTA2DataViewModel.RequestUserMobilePhone = user?.MobilePhone ?? string.Empty;
    }

    private async Task SetPastRequestingUserInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, User? user, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var userHistory = await _readUserRepository.ReadPastInformation(user.Id, approvalDate, cancellationToken);
        if (userHistory != null)
        {
            bookingFormOfSMTA2DataViewModel.RequestUserFirstName = userHistory?.FirstName ?? string.Empty;
            bookingFormOfSMTA2DataViewModel.RequestUserLastName = userHistory?.LastName ?? string.Empty;
            bookingFormOfSMTA2DataViewModel.RequestUserEmail = userHistory?.Email ?? string.Empty;
            bookingFormOfSMTA2DataViewModel.RequestUserJobTitle = userHistory?.JobTitle ?? string.Empty;
            bookingFormOfSMTA2DataViewModel.RequestUserBusinessPhone = userHistory?.BusinessPhone ?? string.Empty;
            bookingFormOfSMTA2DataViewModel.RequestUserMobilePhone = userHistory?.MobilePhone ?? string.Empty;
        }
        else
        {
            SetCurrentRequestingUserInformation(bookingFormOfSMTA2DataViewModel, user);
        }
    }


    private async Task<IEnumerable<WorklistItemUserDto>> GetLaboratoryFocalPoints(WorklistFromBioHubItem entity, string laboratoryName, string laboratoryCountry, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        List<WorklistItemUserDto> laboratoryFocalPoints = new List<WorklistItemUserDto>();

        foreach (var laboratoryFocalPoint in entity.WorklistFromBioHubItemLaboratoryFocalPoints)
        {
            WorklistItemUserDto laboratoryFocalPointDto = new WorklistItemUserDto();
            laboratoryFocalPointDto.Country = laboratoryCountry;
            laboratoryFocalPointDto.Laboratory = laboratoryName;
            laboratoryFocalPointDto.Id = laboratoryFocalPoint.Id;
            laboratoryFocalPointDto.UserId = laboratoryFocalPoint.UserId;
            laboratoryFocalPointDto.LaboratoryId = entity.RequestInitiationToLaboratoryId;
            laboratoryFocalPointDto.Other = laboratoryFocalPoint.Other;
            laboratoryFocalPointDto.WorklistItemId = laboratoryFocalPoint.WorklistFromBioHubItemId;

            var user = laboratoryFocalPoint.User;

            await SetUserInformation(laboratoryFocalPointDto, user, approvalDate, isPast, cancellationToken);


            laboratoryFocalPoints.Add(laboratoryFocalPointDto);

        }
        return laboratoryFocalPoints;
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

    private async Task<IEnumerable<BookingFormOfSMTADto>> GetBookingForms(WorklistFromBioHubItem entity, IEnumerable<string> userPermissions, DateTime approvalDate, string bioHubFacilityName, string bioHubFacilityCountry, bool isPast, CancellationToken cancellationToken)
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
                bookingFormPickupUserDto.Country = bioHubFacilityCountry;
                bookingFormPickupUserDto.Laboratory = bioHubFacilityName;

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

    private async Task SetCouriers(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, List<Courier> couriers, DateTime approvalDate, IEnumerable<string> userPermissions, bool isPast, CancellationToken cancellationToken)
    {
        bookingFormOfSMTA2DataViewModel.Couriers = new List<CourierViewModel>();

        if (userPermissions.Contains(PermissionNames.CanReadCourier) && couriers != null)
        {
            foreach (var courier in couriers)
            {
                if (courier != null)
                {
                    if (courier.LastOperationDate != null && courier.LastOperationDate > approvalDate && !isPast)
                    {
                        await SetPastCourierInformation(bookingFormOfSMTA2DataViewModel, courier, approvalDate, cancellationToken);
                    }
                    else
                    {
                        SetCurrentCourierInformation(bookingFormOfSMTA2DataViewModel, courier);
                    }
                }
            }
        }
    }

    private void SetCurrentCourierInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, Courier courier)
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

        bookingFormOfSMTA2DataViewModel.Couriers.Add(courierViewModel);
    }

    private async Task SetPastCourierInformation(BookingFormOfSMTA2DataViewModel bookingFormOfSMTA2DataViewModel, Courier courier, DateTime approvalDate, CancellationToken cancellationToken)
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

            bookingFormOfSMTA2DataViewModel.Couriers.Add(courierViewModel);
        }
        else
        {
            SetCurrentCourierInformation(bookingFormOfSMTA2DataViewModel, courier);
        }

    }

    private async Task<IEnumerable<WorklistFromBioHubItemMaterialDto>> GetMaterials(WorklistFromBioHubItem entity, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        List<WorklistFromBioHubItemMaterialDto> materials = new List<WorklistFromBioHubItemMaterialDto>();
        foreach (var worklistFromBioHubItemMaterial in entity.WorklistFromBioHubItemMaterials)
        {

            WorklistFromBioHubItemMaterialDto materialDto = new WorklistFromBioHubItemMaterialDto();
            materialDto.Id = worklistFromBioHubItemMaterial.Id;
            materialDto.Condition = worklistFromBioHubItemMaterial.Condition;
            materialDto.Quantity = worklistFromBioHubItemMaterial.Quantity;
            materialDto.Amount = worklistFromBioHubItemMaterial.Amount;
            materialDto.Note = worklistFromBioHubItemMaterial.Note;
            materialDto.Condition = worklistFromBioHubItemMaterial.Condition;

            var material = worklistFromBioHubItemMaterial.Material;
            await SetMaterialInformation(materialDto, material, approvalDate, isPast, cancellationToken);

            materials.Add(materialDto);

        }
        return materials;
    }


    private async Task SetMaterialInformation(WorklistFromBioHubItemMaterialDto materialDto, Material? material, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (material != null)
        {
            if (material.LastOperationDate != null && material.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastMaterialInformation(materialDto, material, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentMaterialInformation(materialDto, material);
            }
        }
    }

    private void SetCurrentMaterialInformation(WorklistFromBioHubItemMaterialDto materialDto, Material? material)
    {
        materialDto.MaterialProductId = material.OriginalProductTypeId;
        materialDto.TransportCategoryId = material.TransportCategoryId;
        materialDto.AvailableQuantity = material.CurrentNumberOfVials ?? 0;
        materialDto.MaterialNumber = material.ReferenceNumber;
        materialDto.MaterialId = material.Id;
        materialDto.MaterialName = material.Name;
        materialDto.Gender = material.Gender;
        materialDto.CollectionDate = material.CollectionDate;
        materialDto.Age = material.Age;
        materialDto.IsolationHostTypeId = material.IsolationHostTypeId;
        materialDto.Location = material.Location;
        materialDto.Status = material.Status;
    }

    private async Task SetPastMaterialInformation(WorklistFromBioHubItemMaterialDto materialDto, Material? material, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var materialHistory = await _readMaterialRepository.ReadPastInformation(material.Id, approvalDate, cancellationToken);
        if (materialHistory != null)
        {
            materialDto.MaterialProductId = materialHistory.OriginalProductTypeId;
            materialDto.TransportCategoryId = materialHistory.TransportCategoryId;
            materialDto.AvailableQuantity = materialHistory.CurrentNumberOfVials ?? 0;
            materialDto.MaterialNumber = materialHistory.ReferenceNumber;
            materialDto.MaterialId = materialHistory.Id;
            materialDto.MaterialName = materialHistory.Name;
            materialDto.Gender = materialHistory.Gender;
            materialDto.CollectionDate = materialHistory.CollectionDate;
            materialDto.Age = materialHistory.Age;
            materialDto.IsolationHostTypeId = materialHistory.IsolationHostTypeId;
            materialDto.Location = materialHistory.Location;
            materialDto.Status = materialHistory.Status;
        }
        else
        {
            SetCurrentMaterialInformation(materialDto, material);
        }
    }
}