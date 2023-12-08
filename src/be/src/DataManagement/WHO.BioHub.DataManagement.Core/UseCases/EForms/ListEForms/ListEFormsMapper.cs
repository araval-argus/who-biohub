using System.Linq;
using WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;

namespace WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForm;

public interface IListEFormsMapper
{
    IEnumerable<EFormItemDto> Map(List<WorklistToBioHubItem> worklistToBioHubItems, List<WorklistFromBioHubItem> worklistFromBioHubItems, RoleType roleType);
}

public class ListEFormsMapper : IListEFormsMapper
{
    private readonly IWorkflowEngineUtility _utils;

    public ListEFormsMapper(IWorkflowEngineUtility utils)
    {
        _utils = utils;
    }

    public IEnumerable<EFormItemDto> Map(List<WorklistToBioHubItem> worklistToBioHubItems, List<WorklistFromBioHubItem> worklistFromBioHubItems, RoleType roleType)
    {

        List<EFormItemDto> listItems = new List<EFormItemDto>();

        Dictionary<string, Guid> folderIds = new Dictionary<string, Guid>();

        List<EFormType> intoBioHubEFormTypes = new List<EFormType>();
        List<EFormType> fromBioHubEFormTypes = new List<EFormType>();

        intoBioHubEFormTypes.Add(EFormType.Annex2OfSMTA1);
        intoBioHubEFormTypes.Add(EFormType.BookingFormOfSMTA1);

        fromBioHubEFormTypes.Add(EFormType.Annex2OfSMTA2);
        fromBioHubEFormTypes.Add(EFormType.BiosafetyChecklistOfSMTA2);
        fromBioHubEFormTypes.Add(EFormType.BookingFormOfSMTA2);



        var toBioHubLaboratoryIds = worklistToBioHubItems.Where(x => x.RequestInitiationFromLaboratoryId != null && x.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval).Select(x => x.RequestInitiationFromLaboratoryId).Distinct().ToList();

        foreach (var laboratoryId in toBioHubLaboratoryIds)
        {

            Guid ShipmentsFolderId = Guid.NewGuid();
            if (!listItems.Select(x => x.Id).Contains(laboratoryId.GetValueOrDefault()))
            {
                var laboratory = worklistToBioHubItems.FirstOrDefault(x => x.RequestInitiationFromLaboratoryId == laboratoryId)?.RequestInitiationFromLaboratory;

                AddFolder(listItems, laboratoryId.GetValueOrDefault(), laboratory?.Name, null);
            }

            else
            {
                ShipmentsFolderId = listItems.Where(x => x.ParentId == laboratoryId && x.Name == "Shipments").FirstOrDefault().Id;
            }

            
            Guid IntoBioHubFolderId = Guid.NewGuid();           


            if (!ContainsFolder(listItems, laboratoryId, "Shipments"))
            {
                AddFolder(listItems, ShipmentsFolderId, "Shipments", laboratoryId);
            }


            if (!ContainsFolder(listItems, ShipmentsFolderId, "Into BioHub"))
            {
                AddFolder(listItems, IntoBioHubFolderId, "Into BioHub", ShipmentsFolderId);
            }

            foreach (var worklistToBioHubItem in worklistToBioHubItems.Where(x => x.RequestInitiationFromLaboratoryId == laboratoryId))
            {
                Guid ShipmentReferenceFolderId = Guid.NewGuid();

                if (!ContainsFolder(listItems, IntoBioHubFolderId, worklistToBioHubItem.ReferenceNumber))
                {
                    AddFolder(listItems, ShipmentReferenceFolderId, worklistToBioHubItem.ReferenceNumber, IntoBioHubFolderId);
                }



                foreach (EFormType type in intoBioHubEFormTypes)
                {
                    Guid folderId = Guid.NewGuid();

                    if (ContainToBioHubEForms(worklistToBioHubItem, type, Shared.Enums.ShipmentDirection.ToBioHub))
                    {
                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(type.ToString()))
                        {
                            AddFolder(listItems, folderId, type.ToString(), ShipmentReferenceFolderId);
                        }

                        AddToBioHubEForm(worklistToBioHubItem, listItems, folderId, roleType, type);
                    }
                }
            }
        }


        var fromBioHubLaboratoryIds = worklistFromBioHubItems.Where(x => x.RequestInitiationToLaboratoryId != null && x.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval).Select(x => x.RequestInitiationToLaboratoryId).Distinct().ToList();

        foreach (var laboratoryId in fromBioHubLaboratoryIds)
        {

            Guid ShipmentsFolderId = Guid.NewGuid();
            
            if (!listItems.Select(x => x.Id).Contains(laboratoryId.GetValueOrDefault()))
            {
                var laboratory = worklistFromBioHubItems.FirstOrDefault(x => x.RequestInitiationToLaboratoryId == laboratoryId)?.RequestInitiationToLaboratory;

                AddFolder(listItems, laboratoryId.GetValueOrDefault(), laboratory?.Name, null);
            }
            else
            {
                ShipmentsFolderId = listItems.Where(x => x.ParentId == laboratoryId && x.Name == "Shipments").FirstOrDefault().Id;
            }


                    
            Guid FromBioHubFolderId = Guid.NewGuid();



            if (!ContainsFolder(listItems, laboratoryId, "Shipments"))
            {
                AddFolder(listItems, ShipmentsFolderId, "Shipments", laboratoryId);
            }


            if (!ContainsFolder(listItems, ShipmentsFolderId, "From BioHub"))
            {
                AddFolder(listItems, FromBioHubFolderId, "From BioHub", ShipmentsFolderId);
            }

            foreach (var worklistFromBioHubItem in worklistFromBioHubItems.Where(x => x.RequestInitiationToLaboratoryId == laboratoryId))
            {
                Guid ShipmentReferenceFolderId = Guid.NewGuid();

                if (!ContainsFolder(listItems, FromBioHubFolderId, worklistFromBioHubItem.ReferenceNumber))
                {
                    AddFolder(listItems, ShipmentReferenceFolderId, worklistFromBioHubItem.ReferenceNumber, FromBioHubFolderId);
                }



                foreach (EFormType type in fromBioHubEFormTypes)
                {
                    Guid folderId = Guid.NewGuid();

                    if (ContainFromBioHubEForms(worklistFromBioHubItem, type, Shared.Enums.ShipmentDirection.FromBioHub))
                    {
                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(type.ToString()))
                        {
                            AddFolder(listItems, folderId, type.ToString(), ShipmentReferenceFolderId);
                        }

                        AddFromBioHubEForm(worklistFromBioHubItem, listItems, folderId, roleType, type);
                    }
                }
            }


        }


        var toBioHubBioHubFacilityIds = worklistToBioHubItems.Where(x => x.RequestInitiationToBioHubFacilityId != null && x.Status > WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval).Select(x => x.RequestInitiationToBioHubFacilityId).Distinct().ToList();

        foreach (var bioHubFacilityId in toBioHubBioHubFacilityIds)
        {

            Guid ShipmentsFolderId = Guid.NewGuid();
            if (!listItems.Select(x => x.Id).Contains(bioHubFacilityId.GetValueOrDefault()))
            {
                var bioHubFacility = worklistToBioHubItems.FirstOrDefault(x => x.RequestInitiationToBioHubFacilityId == bioHubFacilityId)?.RequestInitiationToBioHubFacility;

                AddFolder(listItems, bioHubFacilityId.GetValueOrDefault(), bioHubFacility?.Name, null);
            }

            else
            {
                ShipmentsFolderId = listItems.Where(x => x.ParentId == bioHubFacilityId && x.Name == "Shipments").FirstOrDefault().Id;
            }


            Guid IntoBioHubFolderId = Guid.NewGuid();


            if (!ContainsFolder(listItems, bioHubFacilityId, "Shipments"))
            {
                AddFolder(listItems, ShipmentsFolderId, "Shipments", bioHubFacilityId);
            }


            if (!ContainsFolder(listItems, ShipmentsFolderId, "Into BioHub"))
            {
                AddFolder(listItems, IntoBioHubFolderId, "Into BioHub", ShipmentsFolderId);
            }

            foreach (var worklistToBioHubItem in worklistToBioHubItems.Where(x => x.RequestInitiationToBioHubFacilityId == bioHubFacilityId))
            {
                Guid ShipmentReferenceFolderId = Guid.NewGuid();

                if (!ContainsFolder(listItems, IntoBioHubFolderId, worklistToBioHubItem.ReferenceNumber))
                {
                    AddFolder(listItems, ShipmentReferenceFolderId, worklistToBioHubItem.ReferenceNumber, IntoBioHubFolderId);
                }



                foreach (EFormType type in intoBioHubEFormTypes)
                {
                    Guid folderId = Guid.NewGuid();

                    if (ContainToBioHubEForms(worklistToBioHubItem, type, Shared.Enums.ShipmentDirection.ToBioHub))
                    {
                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(type.ToString()))
                        {
                            AddFolder(listItems, folderId, type.ToString(), ShipmentReferenceFolderId);
                        }

                        AddToBioHubEForm(worklistToBioHubItem, listItems, folderId, roleType, type);
                    }
                }
            }
        }


        var fromBioHubBioHubFacilityIds = worklistFromBioHubItems.Where(x => x.RequestInitiationFromBioHubFacilityId != null && x.Status > WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval).Select(x => x.RequestInitiationFromBioHubFacilityId).Distinct().ToList();

        foreach (var bioHubFacilityId in fromBioHubBioHubFacilityIds)
        {

            Guid ShipmentsFolderId = Guid.NewGuid();

            if (!listItems.Select(x => x.Id).Contains(bioHubFacilityId.GetValueOrDefault()))
            {
                var bioHubFacility = worklistFromBioHubItems.FirstOrDefault(x => x.RequestInitiationFromBioHubFacilityId == bioHubFacilityId)?.RequestInitiationFromBioHubFacility;

                AddFolder(listItems, bioHubFacilityId.GetValueOrDefault(), bioHubFacility?.Name, null);
            }
            else
            {
                ShipmentsFolderId = listItems.Where(x => x.ParentId == bioHubFacilityId && x.Name == "Shipments").FirstOrDefault().Id;
            }



            Guid FromBioHubFolderId = Guid.NewGuid();



            if (!ContainsFolder(listItems, bioHubFacilityId, "Shipments"))
            {
                AddFolder(listItems, ShipmentsFolderId, "Shipments", bioHubFacilityId);
            }


            if (!ContainsFolder(listItems, ShipmentsFolderId, "From BioHub"))
            {
                AddFolder(listItems, FromBioHubFolderId, "From BioHub", ShipmentsFolderId);
            }

            foreach (var worklistFromBioHubItem in worklistFromBioHubItems.Where(x => x.RequestInitiationFromBioHubFacilityId == bioHubFacilityId))
            {
                Guid ShipmentReferenceFolderId = Guid.NewGuid();

                if (!ContainsFolder(listItems, FromBioHubFolderId, worklistFromBioHubItem.ReferenceNumber))
                {
                    AddFolder(listItems, ShipmentReferenceFolderId, worklistFromBioHubItem.ReferenceNumber, FromBioHubFolderId);
                }



                foreach (EFormType type in fromBioHubEFormTypes)
                {
                    Guid folderId = Guid.NewGuid();

                    if (ContainFromBioHubEForms(worklistFromBioHubItem, type, Shared.Enums.ShipmentDirection.FromBioHub))
                    {
                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(type.ToString()))
                        {
                            AddFolder(listItems, folderId, type.ToString(), ShipmentReferenceFolderId);
                        }

                        AddFromBioHubEForm(worklistFromBioHubItem, listItems, folderId, roleType, type);
                    }
                }
            }


        }



        return listItems.OrderBy(x => x.Name);
    }

    private bool ContainsFolder(List<EFormItemDto> listItems, Guid? parentId, string folderName)
    {
        var filteredElements = listItems.Where(x => x.ParentId == parentId);

        if (filteredElements.Select(x => x.Name).Contains(folderName))
        {
            return true;
        }
        return false;
    }

    private void AddFolder(List<EFormItemDto> listItems, Guid folderId, string folderName, Guid? parentFolderId)
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