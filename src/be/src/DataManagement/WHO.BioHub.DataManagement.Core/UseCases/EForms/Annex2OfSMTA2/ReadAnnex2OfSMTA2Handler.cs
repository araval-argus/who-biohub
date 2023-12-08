using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA2;

public interface IReadAnnex2OfSMTA2Handler
{
    Task<Either<ReadAnnex2OfSMTA2QueryResponse, Errors>> Handle(ReadAnnex2OfSMTA2Query query, CancellationToken cancellationToken);
}

public class ReadAnnex2OfSMTA2Handler : IReadAnnex2OfSMTA2Handler
{
    private readonly ILogger<ReadAnnex2OfSMTA2Handler> _logger;
    private readonly ReadAnnex2OfSMTA2QueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly ILaboratoryReadRepository _readLaboratoryRepository;
    private readonly IBioHubFacilityReadRepository _readBioHubFacilityRepository;
    private readonly IUserReadRepository _readUserRepository;
    private readonly IMaterialReadRepository _readMaterialRepository;

    public ReadAnnex2OfSMTA2Handler(
        ILogger<ReadAnnex2OfSMTA2Handler> logger,
        ReadAnnex2OfSMTA2QueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ILaboratoryReadRepository readLaboratoryRepository,
        IBioHubFacilityReadRepository readBioHubFacilityRepository,
        IUserReadRepository readUserRepository,
        IMaterialReadRepository readMaterialRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readLaboratoryRepository = readLaboratoryRepository;
        _readBioHubFacilityRepository = readBioHubFacilityRepository;
        _readUserRepository = readUserRepository;
        _readMaterialRepository = readMaterialRepository;
    }

    public async Task<Either<ReadAnnex2OfSMTA2QueryResponse, Errors>> Handle(
        ReadAnnex2OfSMTA2Query query,
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
                    worklistFromBioHubItem = await _readRepository.ReadAnnex2OfSMTA2Info(query.WorklistId, cancellationToken);
                    break;
                case RoleType.BioHubFacility:
                    worklistFromBioHubItem = await _readRepository.ReadAnnex2OfSMTA2InfoForBioHubFacility(query.WorklistId, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);

                    break;
                case RoleType.Laboratory:
                    worklistFromBioHubItem = await _readRepository.ReadAnnex2OfSMTA2InfoForLaboratory(query.WorklistId, query.LaboratoryId.GetValueOrDefault(), cancellationToken);

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }


            if (worklistFromBioHubItem == null)
                return new(new Errors(ErrorType.NotFound, $"Annex 2 Of SMTA 2 data with Id {query.WorklistId} not found"));

            var annex2OfSMTA2Conditions = (await _readRepository.GetAllAnnex2OfSMTA2ConditionList(cancellationToken)).ToList();

            DocumentTemplate annex2OfSmta2Document = null;
            
            if (worklistFromBioHubItem.OriginalAnnex2OfSMTA2DocumentTemplateId != null)
            {
                annex2OfSmta2Document = await _readDocumentTemplateRepository.Read(worklistFromBioHubItem.OriginalAnnex2OfSMTA2DocumentTemplateId.GetValueOrDefault(), cancellationToken);
            }

            Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel = new Annex2OfSMTA2DataViewModel();

            var smta2Document = worklistFromBioHubItem.WorklistFromBioHubItemDocuments.FirstOrDefault(x => x.Type == DocumentFileType.SMTA2)?.Document;

            DateTime? approvalDate = worklistFromBioHubItem.Annex2OfSMTA2ApprovalDate; 

            var laboratory = worklistFromBioHubItem.RequestInitiationToLaboratory;
            var bioHubFacility = worklistFromBioHubItem.RequestInitiationFromBioHubFacility;
            var isPast = worklistFromBioHubItem.IsPast == true;

            annex2OfSMTA2DataViewModel.ShipmentRequestNumber = worklistFromBioHubItem.ReferenceNumber;
            annex2OfSMTA2DataViewModel.Annex2Comment = worklistFromBioHubItem.Annex2Comment;
            annex2OfSMTA2DataViewModel.Annex2OfSMTA2SignatureText = worklistFromBioHubItem.Annex2OfSMTA2SignatureText;
            annex2OfSMTA2DataViewModel.Annex2TermsAndConditions = worklistFromBioHubItem.Annex2TermsAndConditions ?? true;
            annex2OfSMTA2DataViewModel.WHODocumentRegistrationNumber = worklistFromBioHubItem.WHODocumentRegistrationNumber;
            annex2OfSMTA2DataViewModel.ApprovalDate = approvalDate;
            annex2OfSMTA2DataViewModel.Annex2ApprovalComment = worklistFromBioHubItem.Annex2ApprovalComment;
            annex2OfSMTA2DataViewModel.SMTA2DocumentId = smta2Document?.Id;
            annex2OfSMTA2DataViewModel.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId = annex2OfSmta2Document?.Id;
            annex2OfSMTA2DataViewModel.SMTA2DocumentName = $"{smta2Document?.Name}.{smta2Document?.Extension.ToLower()}";
            annex2OfSMTA2DataViewModel.OriginalDocumentTemplateAnnex2OfSMTA2DocumentName = $"{annex2OfSmta2Document?.Name}.{annex2OfSmta2Document?.Extension.ToLower()}";

            await SetLaboratoryInformation(annex2OfSMTA2DataViewModel, laboratory, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            await SetBioHubFacilityInformation(annex2OfSMTA2DataViewModel, bioHubFacility, approvalDate.GetValueOrDefault(), isPast, cancellationToken);

            annex2OfSMTA2DataViewModel.WorklistFromBioHubItemMaterials = await GetMaterials(worklistFromBioHubItem, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            annex2OfSMTA2DataViewModel.LaboratoryFocalPoints = await GetLaboratoryFocalPoints(worklistFromBioHubItem, annex2OfSMTA2DataViewModel.LaboratoryName, annex2OfSMTA2DataViewModel.LaboratoryCountry, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            annex2OfSMTA2DataViewModel.WorklistFromBioHubItemAnnex2OfSMTA2Conditions = GetAnnex2OfSMTA2Conditions(worklistFromBioHubItem);


            return new(new ReadAnnex2OfSMTA2QueryResponse(annex2OfSMTA2DataViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Annex 2 Of SMTA 2 data");
            throw;
        }
    }


    private async Task SetLaboratoryInformation(Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel, Laboratory? laboratory, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (laboratory != null)
        {
            if (laboratory.LastOperationDate != null && laboratory.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastLaboratoryInformation(annex2OfSMTA2DataViewModel, laboratory, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentLaboratoryInformation(annex2OfSMTA2DataViewModel, laboratory);
            }
        }
    }

    private void SetCurrentLaboratoryInformation(Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel, Laboratory? laboratory)
    {
        annex2OfSMTA2DataViewModel.LaboratoryName = laboratory != null ? laboratory.Name : string.Empty;
        annex2OfSMTA2DataViewModel.LaboratoryAddress = laboratory != null ? laboratory.Address : string.Empty;
        annex2OfSMTA2DataViewModel.LaboratoryCountry = laboratory != null ? laboratory.Country.Name : string.Empty;
    }

    private async Task SetPastLaboratoryInformation(Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel, Laboratory? laboratory, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var laboratoryHistory = await _readLaboratoryRepository.ReadPastInformation(laboratory.Id, approvalDate, cancellationToken);
        if (laboratoryHistory != null)
        {
            annex2OfSMTA2DataViewModel.LaboratoryName = laboratoryHistory != null ? laboratoryHistory.Name : string.Empty;
            annex2OfSMTA2DataViewModel.LaboratoryAddress = laboratoryHistory != null ? laboratoryHistory.Address : string.Empty;
            annex2OfSMTA2DataViewModel.LaboratoryCountry = laboratoryHistory != null ? laboratoryHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentLaboratoryInformation(annex2OfSMTA2DataViewModel, laboratory);
        }
    }



    private async Task SetBioHubFacilityInformation(Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (bioHubFacility != null)
        {
            if (bioHubFacility.LastOperationDate != null && bioHubFacility.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastBioHubFacilityInformation(annex2OfSMTA2DataViewModel, bioHubFacility, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentBioHubFacilityInformation(annex2OfSMTA2DataViewModel, bioHubFacility);
            }
        }
    }

    private void SetCurrentBioHubFacilityInformation(Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel, BioHubFacility? bioHubFacility)
    {
        annex2OfSMTA2DataViewModel.BioHubFacilityName = bioHubFacility != null ? bioHubFacility.Name : string.Empty;
        annex2OfSMTA2DataViewModel.BioHubFacilityAddress = bioHubFacility != null ? bioHubFacility.Address : string.Empty;
        annex2OfSMTA2DataViewModel.BioHubFacilityCountry = bioHubFacility != null ? bioHubFacility.Country.Name : string.Empty;
    }

    private async Task SetPastBioHubFacilityInformation(Annex2OfSMTA2DataViewModel annex2OfSMTA2DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var bioHubFacilityHistory = await _readBioHubFacilityRepository.ReadPastInformation(bioHubFacility.Id, approvalDate, cancellationToken);
        if (bioHubFacilityHistory != null)
        {
            annex2OfSMTA2DataViewModel.BioHubFacilityName = bioHubFacilityHistory != null ? bioHubFacilityHistory.Name : string.Empty;
            annex2OfSMTA2DataViewModel.BioHubFacilityAddress = bioHubFacilityHistory != null ? bioHubFacilityHistory.Address : string.Empty;
            annex2OfSMTA2DataViewModel.BioHubFacilityCountry = bioHubFacilityHistory != null ? bioHubFacilityHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentBioHubFacilityInformation(annex2OfSMTA2DataViewModel, bioHubFacility);
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

    private IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> GetAnnex2OfSMTA2Conditions(WorklistFromBioHubItem entity)
    {
        List<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> conditions = new List<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto>();

        foreach (var condition in entity.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
        {

            WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto annex2OfSMTA2ConditionDto = new WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto();
            annex2OfSMTA2ConditionDto.Id = condition != null ? condition.Id : Guid.NewGuid();
            annex2OfSMTA2ConditionDto.Annex2OfSMTA2ConditionId = condition.Id;
            annex2OfSMTA2ConditionDto.Comment = condition != null ? condition.Comment : String.Empty;
            annex2OfSMTA2ConditionDto.Flag = condition != null ? condition.Flag : (condition.Annex2OfSMTA2Condition.FlagPresetValue != null ? condition.Annex2OfSMTA2Condition.FlagPresetValue : false);
            annex2OfSMTA2ConditionDto.Order = condition.Annex2OfSMTA2Condition.Order;
            annex2OfSMTA2ConditionDto.PointNumber = condition.Annex2OfSMTA2Condition.PointNumber;
            annex2OfSMTA2ConditionDto.Condition = condition.Annex2OfSMTA2Condition.Condition;
            annex2OfSMTA2ConditionDto.Mandatory = condition.Annex2OfSMTA2Condition.Mandatory;
            annex2OfSMTA2ConditionDto.Selectable = condition.Annex2OfSMTA2Condition.Selectable;


            conditions.Add(annex2OfSMTA2ConditionDto);

        }

        conditions = conditions.OrderBy(x => x.Order).ToList();

        return conditions;
    }

    private async Task<IEnumerable<WorklistFromBioHubItemMaterialDto>> GetMaterials(WorklistFromBioHubItem entity, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        List<WorklistFromBioHubItemMaterialDto> materials = new List<WorklistFromBioHubItemMaterialDto>();

        var historyEntity = entity.WorklistFromBioHubHistoryItems.Where(x => x.PreviousStatus == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval && x.LastSubmissionApproved == true).FirstOrDefault();

        if (historyEntity != null)
        {

            foreach (var worklistFromBioHubItemMaterial in historyEntity.WorklistFromBioHubHistoryItemMaterials)
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
        }
        else
        {
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