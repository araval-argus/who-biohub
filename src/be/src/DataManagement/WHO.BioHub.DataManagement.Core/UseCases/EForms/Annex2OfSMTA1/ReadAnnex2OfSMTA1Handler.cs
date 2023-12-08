using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA1;

public interface IReadAnnex2OfSMTA1Handler
{
    Task<Either<ReadAnnex2OfSMTA1QueryResponse, Errors>> Handle(ReadAnnex2OfSMTA1Query query, CancellationToken cancellationToken);
}

public class ReadAnnex2OfSMTA1Handler : IReadAnnex2OfSMTA1Handler
{
    private readonly ILogger<ReadAnnex2OfSMTA1Handler> _logger;
    private readonly ReadAnnex2OfSMTA1QueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly ILaboratoryReadRepository _readLaboratoryRepository;
    private readonly IBioHubFacilityReadRepository _readBioHubFacilityRepository;
    private readonly IUserReadRepository _readUserRepository;

    public ReadAnnex2OfSMTA1Handler(
        ILogger<ReadAnnex2OfSMTA1Handler> logger,
        ReadAnnex2OfSMTA1QueryValidator validator,
        IWorklistToBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ILaboratoryReadRepository readLaboratoryRepository,
        IBioHubFacilityReadRepository readBioHubFacilityRepository,
        IUserReadRepository readUserRepository
        )
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readLaboratoryRepository = readLaboratoryRepository;
        _readBioHubFacilityRepository = readBioHubFacilityRepository;
        _readUserRepository = readUserRepository;
    }

    public async Task<Either<ReadAnnex2OfSMTA1QueryResponse, Errors>> Handle(
        ReadAnnex2OfSMTA1Query query,
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
                    worklistToBioHubItem = await _readRepository.ReadAnnex2OfSMTA1Info(query.WorklistId, cancellationToken);
                    break;
                case RoleType.BioHubFacility:
                    worklistToBioHubItem = await _readRepository.ReadAnnex2OfSMTA1InfoForBioHubFacility(query.WorklistId, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);

                    break;
                case RoleType.Laboratory:
                    worklistToBioHubItem = await _readRepository.ReadAnnex2OfSMTA1InfoForLaboratory(query.WorklistId, query.LaboratoryId.GetValueOrDefault(), cancellationToken);

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }


            if (worklistToBioHubItem == null)
                return new(new Errors(ErrorType.NotFound, $"Annex 2 Of SMTA 1 data with Id {query.WorklistId} not found"));


            DocumentTemplate annex2OfSmta1Document = null;

            if (worklistToBioHubItem.OriginalAnnex2OfSMTA1DocumentTemplateId != null)
            {
                annex2OfSmta1Document = await _readDocumentTemplateRepository.Read(worklistToBioHubItem.OriginalAnnex2OfSMTA1DocumentTemplateId.GetValueOrDefault(), cancellationToken);
            }

            Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel = new Annex2OfSMTA1DataViewModel();

            var smta1Document = worklistToBioHubItem.WorklistToBioHubItemDocuments.FirstOrDefault(x => x.Type == DocumentFileType.SMTA1)?.Document;

            DateTime? approvalDate = worklistToBioHubItem.Annex2OfSMTA1ApprovalDate;

            var laboratory = worklistToBioHubItem.RequestInitiationFromLaboratory;
            var bioHubFacility = worklistToBioHubItem.RequestInitiationToBioHubFacility;
            var isPast = worklistToBioHubItem.IsPast == true;

            annex2OfSMTA1DataViewModel.ApprovalDate = approvalDate;
            annex2OfSMTA1DataViewModel.ShipmentRequestNumber = worklistToBioHubItem.ReferenceNumber;
            annex2OfSMTA1DataViewModel.Annex2Comment = worklistToBioHubItem.Annex2Comment;
            annex2OfSMTA1DataViewModel.Annex2OfSMTA1SignatureText = worklistToBioHubItem.Annex2OfSMTA1SignatureText;
            annex2OfSMTA1DataViewModel.Annex2TermsAndConditions = worklistToBioHubItem.Annex2TermsAndConditions ?? true;
            annex2OfSMTA1DataViewModel.WHODocumentRegistrationNumber = worklistToBioHubItem.WHODocumentRegistrationNumber;
            annex2OfSMTA1DataViewModel.Annex2ApprovalComment = worklistToBioHubItem.Annex2ApprovalComment;
            annex2OfSMTA1DataViewModel.SMTA1DocumentId = smta1Document?.Id;
            annex2OfSMTA1DataViewModel.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId = annex2OfSmta1Document?.Id;
            annex2OfSMTA1DataViewModel.SMTA1DocumentName = $"{smta1Document?.Name}.{smta1Document?.Extension.ToLower()}";
            annex2OfSMTA1DataViewModel.OriginalDocumentTemplateAnnex2OfSMTA1DocumentName = $"{annex2OfSmta1Document?.Name}.{annex2OfSmta1Document?.Extension.ToLower()}";
            annex2OfSMTA1DataViewModel.MaterialShippingInformations = GetMaterialShippingInformations(worklistToBioHubItem);

            await SetLaboratoryInformation(annex2OfSMTA1DataViewModel, laboratory, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            await SetBioHubFacilityInformation(annex2OfSMTA1DataViewModel, bioHubFacility, approvalDate.GetValueOrDefault(), isPast, cancellationToken);

            annex2OfSMTA1DataViewModel.LaboratoryFocalPoints = await GetLaboratoryFocalPoints(worklistToBioHubItem, annex2OfSMTA1DataViewModel.LaboratoryName, annex2OfSMTA1DataViewModel.LaboratoryCountry, approvalDate.GetValueOrDefault(), isPast, cancellationToken);


            return new(new ReadAnnex2OfSMTA1QueryResponse(annex2OfSMTA1DataViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Kpi");
            throw;
        }
    }

    private async Task SetLaboratoryInformation(Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel, Laboratory? laboratory, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (laboratory != null)
        {
            if (laboratory.LastOperationDate != null && laboratory.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastLaboratoryInformation(annex2OfSMTA1DataViewModel, laboratory, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentLaboratoryInformation(annex2OfSMTA1DataViewModel, laboratory);
            }
        }
    }

    private void SetCurrentLaboratoryInformation(Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel, Laboratory? laboratory)
    {
        annex2OfSMTA1DataViewModel.LaboratoryName = laboratory != null ? laboratory.Name : string.Empty;
        annex2OfSMTA1DataViewModel.LaboratoryAddress = laboratory != null ? laboratory.Address : string.Empty;
        annex2OfSMTA1DataViewModel.LaboratoryCountry = laboratory != null ? laboratory.Country.Name : string.Empty;
    }

    private async Task SetPastLaboratoryInformation(Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel, Laboratory? laboratory, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var laboratoryHistory = await _readLaboratoryRepository.ReadPastInformation(laboratory.Id, approvalDate, cancellationToken);
        if (laboratoryHistory != null)
        {
            annex2OfSMTA1DataViewModel.LaboratoryName = laboratoryHistory != null ? laboratoryHistory.Name : string.Empty;
            annex2OfSMTA1DataViewModel.LaboratoryAddress = laboratoryHistory != null ? laboratoryHistory.Address : string.Empty;
            annex2OfSMTA1DataViewModel.LaboratoryCountry = laboratoryHistory != null ? laboratoryHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentLaboratoryInformation(annex2OfSMTA1DataViewModel, laboratory);
        }
    }



    private async Task SetBioHubFacilityInformation(Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (bioHubFacility != null)
        {
            if (bioHubFacility.LastOperationDate != null && bioHubFacility.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastBioHubFacilityInformation(annex2OfSMTA1DataViewModel, bioHubFacility, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentBioHubFacilityInformation(annex2OfSMTA1DataViewModel, bioHubFacility);
            }
        }
    }

    private void SetCurrentBioHubFacilityInformation(Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel, BioHubFacility? bioHubFacility)
    {
        annex2OfSMTA1DataViewModel.BioHubFacilityName = bioHubFacility != null ? bioHubFacility.Name : string.Empty;
        annex2OfSMTA1DataViewModel.BioHubFacilityAddress = bioHubFacility != null ? bioHubFacility.Address : string.Empty;
        annex2OfSMTA1DataViewModel.BioHubFacilityCountry = bioHubFacility != null ? bioHubFacility.Country.Name : string.Empty;
    }

    private async Task SetPastBioHubFacilityInformation(Annex2OfSMTA1DataViewModel annex2OfSMTA1DataViewModel, BioHubFacility bioHubFacility, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var bioHubFacilityHistory = await _readBioHubFacilityRepository.ReadPastInformation(bioHubFacility.Id, approvalDate, cancellationToken);
        if (bioHubFacilityHistory != null)
        {
            annex2OfSMTA1DataViewModel.BioHubFacilityName = bioHubFacilityHistory != null ? bioHubFacilityHistory.Name : string.Empty;
            annex2OfSMTA1DataViewModel.BioHubFacilityAddress = bioHubFacilityHistory != null ? bioHubFacilityHistory.Address : string.Empty;
            annex2OfSMTA1DataViewModel.BioHubFacilityCountry = bioHubFacilityHistory != null ? bioHubFacilityHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentBioHubFacilityInformation(annex2OfSMTA1DataViewModel, bioHubFacility);
        }
    }






    private async Task<IEnumerable<WorklistItemUserDto>> GetLaboratoryFocalPoints(WorklistToBioHubItem entity, string laboratoryName, string laboratoryCountry, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        List<WorklistItemUserDto> laboratoryFocalPoints = new List<WorklistItemUserDto>();

        foreach (var laboratoryFocalPoint in entity.WorklistToBioHubItemLaboratoryFocalPoints)
        {
            WorklistItemUserDto laboratoryFocalPointDto = new WorklistItemUserDto();
            laboratoryFocalPointDto.Country = laboratoryCountry;
            laboratoryFocalPointDto.Laboratory = laboratoryName;
            laboratoryFocalPointDto.Id = laboratoryFocalPoint.Id;
            laboratoryFocalPointDto.UserId = laboratoryFocalPoint.UserId;
            laboratoryFocalPointDto.LaboratoryId = entity.RequestInitiationFromLaboratoryId;
            laboratoryFocalPointDto.Other = laboratoryFocalPoint.Other;
            laboratoryFocalPointDto.WorklistItemId = laboratoryFocalPoint.WorklistToBioHubItemId;

            var user = laboratoryFocalPoint.User;

            await SetFocalPointUserInformation(laboratoryFocalPointDto, user, approvalDate, isPast, cancellationToken);


            laboratoryFocalPoints.Add(laboratoryFocalPointDto);

        }
        return laboratoryFocalPoints;
    }


    private async Task SetFocalPointUserInformation(WorklistItemUserDto laboratoryFocalPointDto, User? user, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (user != null)
        {
            if (user.LastOperationDate != null && user.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastUserInformation(laboratoryFocalPointDto, user, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentUserInformation(laboratoryFocalPointDto, user);
            }
        }
    }

    private void SetCurrentUserInformation(WorklistItemUserDto laboratoryFocalPointDto, User? user)
    {
        laboratoryFocalPointDto.UserName = user.FirstName + " " + user.LastName;
        laboratoryFocalPointDto.Email = user.Email;
        laboratoryFocalPointDto.JobTitle = user.JobTitle;
        laboratoryFocalPointDto.MobilePhone = user.MobilePhone;
        laboratoryFocalPointDto.BusinessPhone = user.BusinessPhone;
    }

    private async Task SetPastUserInformation(WorklistItemUserDto laboratoryFocalPointDto, User? user, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var userHistory = await _readUserRepository.ReadPastInformation(user.Id, approvalDate, cancellationToken);
        if (userHistory != null)
        {
            laboratoryFocalPointDto.UserName = userHistory.FirstName + " " + userHistory.LastName;
            laboratoryFocalPointDto.Email = userHistory.Email;
            laboratoryFocalPointDto.JobTitle = userHistory.JobTitle;
            laboratoryFocalPointDto.MobilePhone = userHistory.MobilePhone;
            laboratoryFocalPointDto.BusinessPhone = userHistory.BusinessPhone;
        }
        else
        {
            SetCurrentUserInformation(laboratoryFocalPointDto, user);
        }
    }

    private IEnumerable<MaterialShippingInformationDto> GetMaterialShippingInformations(WorklistToBioHubItem entity)
    {
        List<MaterialShippingInformationDto> materialShippingInformations = new List<MaterialShippingInformationDto>();
        foreach (var materialShippingInformation in entity.MaterialShippingInformations)
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