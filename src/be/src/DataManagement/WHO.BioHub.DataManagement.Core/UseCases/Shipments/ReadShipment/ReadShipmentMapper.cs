using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.WorkflowEngine;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;

public interface IReadShipmentMapper
{
    ShipmentViewModel Map(Shipment shipment, RoleType roleType, IEnumerable<string> userPermissions);
    ShipmentViewModel MapForBioHubUser(Shipment shipment, Guid bioHubFacilityId, RoleType roleType, IEnumerable<string> userPermissions);
    ShipmentViewModel MapForLaboratoryUser(Shipment shipment, Guid laboratoryId, RoleType roleType, IEnumerable<string> userPermissions);
}

public class ReadShipmentMapper : IReadShipmentMapper
{
    private readonly IWorkflowEngineUtility _utils;

    public ReadShipmentMapper(IWorkflowEngineUtility utils)
    {
        _utils = utils;
    }

    public ShipmentViewModel Map(Shipment shipment, RoleType roleType, IEnumerable<string> userPermissions)
    {


        ShipmentViewModel shipmentViewModel = UpdateGeneralPart(shipment, userPermissions);

        if (shipment.WorklistFromBioHubItem != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any()
            )
        {
            UpdateWorklistFromBioHubItemMaterials(shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials, shipmentViewModel);
        }

        if (shipment.WorklistToBioHubItem != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any()
            )
        {
            UpdateWorklistToBioHubItemMaterials(shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials, shipmentViewModel);
        }

        SetDocuments(shipment, shipmentViewModel);
        SetEForms(shipment, shipmentViewModel, roleType);

        return shipmentViewModel;
    }



    public ShipmentViewModel MapForLaboratoryUser(Shipment shipment, Guid laboratoryId, RoleType roleType, IEnumerable<string> userPermissions)
    {

        ShipmentViewModel shipmentViewModel = UpdateGeneralPart(shipment, userPermissions);

        if (shipment.WorklistFromBioHubItem != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any()
            )
        {
            var worklistFromBioHubItemMaterials = shipment
                .WorklistFromBioHubItem
                .WorklistFromBioHubItemMaterials
                .Where(x => x.Material.DeletedOn == null && (x.Material.ProviderLaboratoryId == laboratoryId || (x.Material.PublicShare == YesNoOption.Yes && x.Material.Status == MaterialStatus.Completed)));


            UpdateWorklistFromBioHubItemMaterials(worklistFromBioHubItemMaterials, shipmentViewModel);
        }

        if (shipment.WorklistToBioHubItem != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any()
            )
        {

            var worklistToBioHubItemMaterials = shipment
               .WorklistToBioHubItem
               .WorklistToBioHubItemMaterials
               .Where(x => x.Material.DeletedOn == null && (x.Material.ProviderLaboratoryId == laboratoryId || (x.Material.PublicShare == YesNoOption.Yes && x.Material.Status == MaterialStatus.Completed)));


            UpdateWorklistToBioHubItemMaterials(worklistToBioHubItemMaterials, shipmentViewModel);
        }

        SetDocuments(shipment, shipmentViewModel);
        SetEForms(shipment, shipmentViewModel, roleType);

        return shipmentViewModel;
    }

    public ShipmentViewModel MapForBioHubUser(Shipment shipment, Guid bioHubFacilityId, RoleType roleType, IEnumerable<string> userPermissions)
    {

        ShipmentViewModel shipmentViewModel = UpdateGeneralPart(shipment, userPermissions);

        if (shipment.WorklistFromBioHubItem != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any()
            )
        {
            var worklistFromBioHubItemMaterials = shipment
                .WorklistFromBioHubItem
                .WorklistFromBioHubItemMaterials
                .Where(x => x.Material.DeletedOn == null && (x.Material.OwnerBioHubFacilityId == bioHubFacilityId || (x.Material.Status == MaterialStatus.Completed)));


            UpdateWorklistFromBioHubItemMaterials(worklistFromBioHubItemMaterials, shipmentViewModel);
        }

        if (shipment.WorklistToBioHubItem != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any()
            )
        {

            var worklistToBioHubItemMaterials = shipment
               .WorklistToBioHubItem
               .WorklistToBioHubItemMaterials
               .Where(x => x.Material.DeletedOn == null && (x.Material.OwnerBioHubFacilityId == bioHubFacilityId || (x.Material.Status == MaterialStatus.Completed)));


            UpdateWorklistToBioHubItemMaterials(worklistToBioHubItemMaterials, shipmentViewModel);
        }

        SetDocuments(shipment, shipmentViewModel);
        SetEForms(shipment, shipmentViewModel, roleType);

        return shipmentViewModel;
    }

    private ShipmentViewModel UpdateGeneralPart(Shipment shipment, IEnumerable<string> userPermissions)
    {
        ShipmentViewModel shipmentViewModel = new ShipmentViewModel();

        shipmentViewModel.Id = shipment.Id;
        shipmentViewModel.ReferenceNumber = shipment.ReferenceNumber;
        shipmentViewModel.From = shipment.WorklistFromBioHubItem != null ? shipment.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Name : (shipment.WorklistToBioHubItem != null ? shipment.WorklistToBioHubItem.RequestInitiationFromLaboratory.Name : String.Empty);
        shipmentViewModel.To = shipment.WorklistFromBioHubItem != null ? shipment.WorklistFromBioHubItem.RequestInitiationToLaboratory.Name : (shipment.WorklistToBioHubItem != null ? shipment.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Name : String.Empty);
        shipmentViewModel.CompletedOn = shipment.CreationDate;
        shipmentViewModel.StatusOfRequest = shipment.StatusOfRequest;
        shipmentViewModel.BioHubFacilityId = shipment.BioHubFacilityId;
        shipmentViewModel.LaboratoryId = shipment.QELaboratoryId;
        shipmentViewModel.WorklistFromBioHubItemId = shipment.WorklistFromBioHubItemId;
        shipmentViewModel.WorklistToBioHubItemId = shipment.WorklistToBioHubItemId;
        shipmentViewModel.CanEditReferenceNumber = CanEditReferenceNumber(shipment, userPermissions);

        shipmentViewModel.ShipmentMaterials = new List<ShipmentMaterialViewModel>();

        shipmentViewModel.BookingForms = GetBookingForms(shipment);

        return shipmentViewModel;
    }

    private List<BookingFormOfSMTADto> GetBookingForms(Shipment shipment)
    {
        List<BookingForm> shipmentBookingForms;
        if (shipment.WorklistFromBioHubItem != null)
        {
            shipmentBookingForms = shipment.WorklistFromBioHubItem.BookingForms.ToList();
        }
        else
        {
            shipmentBookingForms = shipment.WorklistToBioHubItem.BookingForms.ToList();
        }

        List<BookingFormOfSMTADto> bookingForms = new List<BookingFormOfSMTADto>();
        var orderedList = shipmentBookingForms.OrderBy(x => x.TransportCategory.Description);
        foreach (var bookingForm in orderedList)
        {

            BookingFormOfSMTADto bookingFormDto = new BookingFormOfSMTADto();
            bookingFormDto.Id = bookingForm.Id;

            bookingFormDto.TransportCategoryName = bookingForm.TransportCategory.Name;
            bookingFormDto.TransportModeName = bookingForm.TransportMode.Name;
            bookingFormDto.TransportModeDescription = bookingForm.TransportMode.Description;
            bookingFormDto.DateOfPickup = bookingForm.DateOfPickup;
            bookingFormDto.DateOfDelivery = bookingForm.DateOfDelivery;
            bookingFormDto.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;
            bookingFormDto.TransportModeId = bookingForm.TransportModeId;
            bookingFormDto.TransportCategoryId = bookingForm.TransportCategoryId;

            bookingForms.Add(bookingFormDto);

        }
        return bookingForms;
    }


    private bool CanEditReferenceNumber(Shipment shipment, IEnumerable<string> userPermissions)
    {
        if (!userPermissions.Contains(PermissionNames.CanEditShipment))
        {
            return false;
        }
        if (shipment.WorklistFromBioHubItem != null && shipment.WorklistFromBioHubItem.IsPast == true)
        {
            return true;
        }

        if (shipment.WorklistToBioHubItem != null && shipment.WorklistToBioHubItem.IsPast == true)
        {
            return true;
        }

        return false;


    }

    private void UpdateWorklistFromBioHubItemMaterials(IEnumerable<WorklistFromBioHubItemMaterial> worklistFromBioHubItemMaterials, ShipmentViewModel shipmentViewModel)
    {
        foreach (var worklistFromBioHubItemMaterial in worklistFromBioHubItemMaterials)
        {
            ShipmentMaterialViewModel shipmentMaterialViewModel = new ShipmentMaterialViewModel();
            shipmentMaterialViewModel.Id = worklistFromBioHubItemMaterial.Id;
            shipmentMaterialViewModel.MaterialProductId = worklistFromBioHubItemMaterial.Material.OriginalProductTypeId;
            shipmentMaterialViewModel.MaterialNumber = worklistFromBioHubItemMaterial.Material.ReferenceNumber;
            shipmentMaterialViewModel.MaterialId = worklistFromBioHubItemMaterial.Material.Id;
            shipmentMaterialViewModel.MaterialName = worklistFromBioHubItemMaterial.Material.Name;
            shipmentMaterialViewModel.Gender = worklistFromBioHubItemMaterial.Material.Gender;
            shipmentMaterialViewModel.ShipmentMaterialCondition = ShipmentMaterialCondition.Intact;
            shipmentMaterialViewModel.CollectionDate = worklistFromBioHubItemMaterial.Material.CollectionDate;
            shipmentMaterialViewModel.Age = worklistFromBioHubItemMaterial.Material.Age;
            shipmentMaterialViewModel.IsolationHostTypeId = worklistFromBioHubItemMaterial.Material.IsolationHostTypeId;
            shipmentMaterialViewModel.Location = worklistFromBioHubItemMaterial.Material.Location;
            shipmentMaterialViewModel.Status = worklistFromBioHubItemMaterial.Material.Status;
            shipmentViewModel.ShipmentMaterials.Add(shipmentMaterialViewModel);
        }
    }

    private void UpdateWorklistToBioHubItemMaterials(IEnumerable<WorklistToBioHubItemMaterial> worklistToBioHubItemMaterials, ShipmentViewModel shipmentViewModel)
    {
        foreach (var worklistToBioHubItemMaterial in worklistToBioHubItemMaterials)
        {
            ShipmentMaterialViewModel shipmentMaterialViewModel = new ShipmentMaterialViewModel();
            shipmentMaterialViewModel.Id = worklistToBioHubItemMaterial.Id;
            shipmentMaterialViewModel.MaterialProductId = worklistToBioHubItemMaterial.Material.OriginalProductTypeId;
            shipmentMaterialViewModel.MaterialNumber = worklistToBioHubItemMaterial.Material.ReferenceNumber;
            shipmentMaterialViewModel.MaterialId = worklistToBioHubItemMaterial.Material.Id;
            shipmentMaterialViewModel.MaterialName = worklistToBioHubItemMaterial.Material.Name;
            shipmentMaterialViewModel.Gender = worklistToBioHubItemMaterial.Material.Gender;
            shipmentMaterialViewModel.CollectionDate = worklistToBioHubItemMaterial.Material.CollectionDate;
            shipmentMaterialViewModel.Age = worklistToBioHubItemMaterial.Material.Age;
            shipmentMaterialViewModel.IsolationHostTypeId = worklistToBioHubItemMaterial.Material.IsolationHostTypeId;
            shipmentMaterialViewModel.Location = worklistToBioHubItemMaterial.Material.Location;
            shipmentMaterialViewModel.Status = worklistToBioHubItemMaterial.Material.Status;
            shipmentMaterialViewModel.ShipmentMaterialCondition = worklistToBioHubItemMaterial.Material.ShipmentMaterialCondition ?? ShipmentMaterialCondition.Intact;
            shipmentViewModel.ShipmentMaterials.Add(shipmentMaterialViewModel);
        }
    }

    private void SetDocuments(Shipment shipment, ShipmentViewModel shipmentViewModel)
    {
        shipmentViewModel.Documents = new List<DocumentItemDto>();

        Dictionary<string, Guid> folderIds = new Dictionary<string, Guid>();

        if (shipment.WorklistToBioHubItem != null)
        {
            List<DocumentFileType> intoBioHubDocumentTypes = new List<DocumentFileType>();

            intoBioHubDocumentTypes.Add(DocumentFileType.Annex2OfSMTA1);
            intoBioHubDocumentTypes.Add(DocumentFileType.BookingFormOfSMTA1);
            intoBioHubDocumentTypes.Add(DocumentFileType.PackagingList);
            intoBioHubDocumentTypes.Add(DocumentFileType.DangerousGoodsDeclaration);
            intoBioHubDocumentTypes.Add(DocumentFileType.ExportPermit);
            intoBioHubDocumentTypes.Add(DocumentFileType.ImportPermit);
            intoBioHubDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatA);
            intoBioHubDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatB);
            intoBioHubDocumentTypes.Add(DocumentFileType.Other);

            var allDocuments = shipment.WorklistToBioHubItem.WorklistToBioHubItemDocuments.Select(x => x.Document);

            var documents = allDocuments.Where(x => x.IsDocumentFile != false && x.IsDeleted == false && x.DeletedOn == null && x.Approved == true).ToList();

            if (documents.Any())
            {
                foreach (DocumentFileType type in intoBioHubDocumentTypes)
                {
                    Guid folderId = Guid.NewGuid();

                    if (ContainDocuments(documents.ToList(), type, Shared.Enums.ShipmentDirection.ToBioHub))
                    {

                        AddDocumentFolder(shipmentViewModel.Documents, folderId, type.ToString(), null);


                        AddDocumentFiles(documents, shipmentViewModel.Documents, folderId, Shared.Enums.ShipmentDirection.ToBioHub, type);
                    }
                }
            }
        }

        else
        {

            List<DocumentFileType> fromBioHubDocumentTypes = new List<DocumentFileType>();

            fromBioHubDocumentTypes.Add(DocumentFileType.Annex2OfSMTA2);
            fromBioHubDocumentTypes.Add(DocumentFileType.BiosafetyChecklist);
            fromBioHubDocumentTypes.Add(DocumentFileType.BookingFormOfSMTA2);
            fromBioHubDocumentTypes.Add(DocumentFileType.PackagingList);
            fromBioHubDocumentTypes.Add(DocumentFileType.DangerousGoodsDeclaration);
            fromBioHubDocumentTypes.Add(DocumentFileType.ExportPermit);
            fromBioHubDocumentTypes.Add(DocumentFileType.ImportPermit);
            fromBioHubDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatA);
            fromBioHubDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatB);
            fromBioHubDocumentTypes.Add(DocumentFileType.Other);

            var allDocuments = shipment.WorklistFromBioHubItem.WorklistFromBioHubItemDocuments.Select(x => x.Document);

            var documents = allDocuments.Where(x => x.IsDocumentFile != false && x.IsDeleted == false && x.DeletedOn == null && x.Approved == true).ToList();

            if (documents.Any())
            {
                foreach (DocumentFileType type in fromBioHubDocumentTypes)
                {
                    Guid folderId = Guid.NewGuid();

                    if (ContainDocuments(documents.ToList(), type, Shared.Enums.ShipmentDirection.FromBioHub))
                    {

                        AddDocumentFolder(shipmentViewModel.Documents, folderId, type.ToString(), null);


                        AddDocumentFiles(documents, shipmentViewModel.Documents, folderId, Shared.Enums.ShipmentDirection.FromBioHub, type);
                    }
                }
            }

        }
        shipmentViewModel.Documents.OrderBy(x => x.Name);
    }


    private void SetEForms(Shipment shipment, ShipmentViewModel shipmentViewModel, RoleType roleType)
    {
        shipmentViewModel.EForms = new List<EFormItemDto>();

        Dictionary<string, Guid> folderIds = new Dictionary<string, Guid>();

        if (shipment.WorklistToBioHubItem != null)
        {
            List<EFormType> intoBioHubEFormTypes = new List<EFormType>();

            intoBioHubEFormTypes.Add(EFormType.Annex2OfSMTA1);
            intoBioHubEFormTypes.Add(EFormType.BookingFormOfSMTA1);

            var worklistToBioHubItem = shipment.WorklistToBioHubItem;

            foreach (EFormType type in intoBioHubEFormTypes)
            {
                Guid folderId = Guid.NewGuid();

                if (ContainToBioHubEForms(worklistToBioHubItem, type, Shared.Enums.ShipmentDirection.ToBioHub))
                {

                    AddEFormFolder(shipmentViewModel.EForms, folderId, type.ToString(), null);

                    AddToBioHubEForm(worklistToBioHubItem, shipmentViewModel.EForms, folderId, roleType, type);
                }
            }
        }

        else
        {

            List<EFormType> fromBioHubEFormTypes = new List<EFormType>();

            fromBioHubEFormTypes.Add(EFormType.Annex2OfSMTA2);
            fromBioHubEFormTypes.Add(EFormType.BiosafetyChecklistOfSMTA2);
            fromBioHubEFormTypes.Add(EFormType.BookingFormOfSMTA2);

            var worklistFromBioHubItem = shipment.WorklistFromBioHubItem;

            foreach (EFormType type in fromBioHubEFormTypes)
            {
                Guid folderId = Guid.NewGuid();

                if (ContainFromBioHubEForms(worklistFromBioHubItem, type, Shared.Enums.ShipmentDirection.FromBioHub))
                {
                    AddEFormFolder(shipmentViewModel.EForms, folderId, type.ToString(), null);
                    AddFromBioHubEForm(worklistFromBioHubItem, shipmentViewModel.EForms, folderId, roleType, type);
                }
            }

        }
        shipmentViewModel.EForms.OrderBy(x => x.Name);
    }

    private bool ContainDocuments(List<Document> documents, DocumentFileType fileType, Shared.Enums.ShipmentDirection shipmentDirection)
    {
        List<Document> filteredDocuments = null;
        if (shipmentDirection == Shared.Enums.ShipmentDirection.ToBioHub)
        {
            filteredDocuments = documents.Where(x => x.WorklistToBioHubItemDocuments.Any()).ToList();
        }
        else
        {
            filteredDocuments = documents.Where(x => x.WorklistFromBioHubItemDocuments.Any()).ToList();
        }

        if (filteredDocuments.Select(x => x.Type).Contains(fileType))
        {
            return true;
        }
        return false;
    }

    private void AddDocumentFolder(List<DocumentItemDto> listItems, Guid folderId, string folderName, Guid? parentFolderId)
    {
        DocumentItemDto documentItemDto = new()
        {
            Id = folderId,
            Name = folderName,
            Extension = String.Empty,
            Type = DocumentItemType.Folder,
            FileType = null,
            UploadTime = null,
            UploadedById = null,
            UploadedBy = String.Empty,
            ParentId = parentFolderId,
        };
        listItems.Add(documentItemDto);
    }

    private void AddDocumentFiles(List<Document> entities, List<DocumentItemDto> listItems, Guid parentFolderId, Shared.Enums.ShipmentDirection shipmentDirection, DocumentFileType documentFileType)
    {
        List<Document>? filteredDocuments;

        if (shipmentDirection == Shared.Enums.ShipmentDirection.FromBioHub)
        {

            filteredDocuments = entities.Where(x => x.WorklistFromBioHubItemDocuments.Any()).Where(x => x.Type == documentFileType).ToList();
        }
        else
        {
            filteredDocuments = entities.Where(x => x.WorklistToBioHubItemDocuments.Any()).Where(x => x.Type == documentFileType).ToList();

        }
        foreach (var filteredDocument in filteredDocuments)
        {
            DocumentItemDto documentItemDto = new()
            {
                Id = filteredDocument.Id,
                Name = filteredDocument.Name,
                Extension = filteredDocument.Extension,
                Type = DocumentItemType.File,
                FileType = filteredDocument.Type,
                UploadTime = filteredDocument.OperationDate != null ? filteredDocument.OperationDate : filteredDocument.CreationDate,
                UploadedById = filteredDocument.UploadedById,
                UploadedBy = filteredDocument.UploadedBy.FirstName + " " + filteredDocument.UploadedBy.LastName,
                ParentId = parentFolderId,
            };
            listItems.Add(documentItemDto);
        }
    }


    private bool ContainsEFormFolder(List<EFormItemDto> listItems, Guid? parentId, string folderName)
    {
        var filteredElements = listItems.Where(x => x.ParentId == parentId);

        if (filteredElements.Select(x => x.Name).Contains(folderName))
        {
            return true;
        }
        return false;
    }

    private void AddEFormFolder(List<EFormItemDto> listItems, Guid folderId, string folderName, Guid? parentFolderId)
    {
        EFormItemDto documentItemDto = new()
        {
            Id = folderId,
            Name = folderName,
            Type = EFormItemType.Folder,
            EFormType = null,
            ParentId = parentFolderId,
        };
        listItems.Add(documentItemDto);
    }



    private void AddToBioHubEForm(WorklistToBioHubItem worklistToBioHubItem, List<EFormItemDto> listItems, Guid parentFolderId, RoleType roleType, EFormType eFormType)
    {
        string baseUrl = _utils.BaseUrl();
        string area = string.Empty;
        string url = string.Empty;

        switch (roleType)
        {
            case RoleType.WHO:
                area = $"whoarea";
                break;

            case RoleType.BioHubFacility:
                area = $"biohubfacilityarea";
                break;

            case RoleType.Laboratory:
                area = $"laboratoryarea";
                break;

            default:
                break;
        }

        DateTime? approvedTime = null;

        switch (eFormType)
        {
            case EFormType.Annex2OfSMTA1:
                approvedTime = worklistToBioHubItem.Annex2OfSMTA1ApprovalDate;

                break;

            case EFormType.BookingFormOfSMTA1:
                approvedTime = worklistToBioHubItem.BookingFormOfSMTA1ApprovalDate;
                break;

            default:
                break;
        }

        url = $"{baseUrl}{area}/eforms/{worklistToBioHubItem.Id}/{eFormType.ToString().ToLower()}";

        EFormItemDto documentItemDto = new()
        {
            Id = Guid.NewGuid(),
            Name = eFormType.ToString(),
            ApprovedTime = approvedTime,
            Type = EFormItemType.EForm,
            EFormType = eFormType,
            ParentId = parentFolderId,
            Url = url,
        };
        listItems.Add(documentItemDto);

    }

    private bool ContainToBioHubEForms(WorklistToBioHubItem worklistToBioHubItem, EFormType eFormType, Shared.Enums.ShipmentDirection shipmentDirection)
    {
        switch (eFormType)
        {
            case EFormType.Annex2OfSMTA1:
                return worklistToBioHubItem.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval;

            case EFormType.BookingFormOfSMTA1:
                return worklistToBioHubItem.Status > WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval;

            default:
                return false;
        }

    }


    private bool ContainFromBioHubEForms(WorklistFromBioHubItem worklistFromBioHubItem, EFormType eFormType, Shared.Enums.ShipmentDirection shipmentDirection)
    {
        switch (eFormType)
        {
            case EFormType.Annex2OfSMTA2:
                return worklistFromBioHubItem.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval;

            case EFormType.BiosafetyChecklistOfSMTA2:
                return worklistFromBioHubItem.Status > WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval;

            case EFormType.BookingFormOfSMTA2:
                return worklistFromBioHubItem.Status > WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval;

            default:
                return false;
        }

    }

    private void AddFromBioHubEForm(WorklistFromBioHubItem worklistFromBioHubItem, List<EFormItemDto> listItems, Guid parentFolderId, RoleType roleType, EFormType eFormType)
    {
        string baseUrl = _utils.BaseUrl();
        string area = string.Empty;
        string url = string.Empty;

        switch (roleType)
        {
            case RoleType.WHO:
                area = $"whoarea";
                break;

            case RoleType.BioHubFacility:
                area = $"biohubfacilityarea";
                break;

            case RoleType.Laboratory:
                area = $"laboratoryarea";
                break;

            default:
                break;
        }

        DateTime? approvedTime = null;

        switch (eFormType)
        {
            case EFormType.Annex2OfSMTA2:
                approvedTime = worklistFromBioHubItem.Annex2OfSMTA2ApprovalDate;

                break;

            case EFormType.BiosafetyChecklistOfSMTA2:
                approvedTime = worklistFromBioHubItem.BiosafetyChecklistApprovalDate;
                break;

            case EFormType.BookingFormOfSMTA1:
                approvedTime = worklistFromBioHubItem.BookingFormOfSMTA2ApprovalDate;
                break;

            default:
                break;
        }

        url = $"{baseUrl}{area}/eforms/{worklistFromBioHubItem.Id}/{eFormType.ToString().ToLower()}";

        EFormItemDto documentItemDto = new()
        {
            Id = Guid.NewGuid(),
            Name = eFormType.ToString(),
            ApprovedTime = approvedTime,
            Type = EFormItemType.EForm,
            EFormType = eFormType,
            ParentId = parentFolderId,
            Url = url,
        };
        listItems.Add(documentItemDto);

    }
}