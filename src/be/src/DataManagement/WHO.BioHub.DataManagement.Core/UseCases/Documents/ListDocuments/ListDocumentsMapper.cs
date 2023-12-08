using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocument;

public interface IListDocumentsMapper
{
    IEnumerable<DocumentItemDto> Map(List<Document> entities,
        List<WorklistToBioHubItem> worklistToBioHubItems,
        List<WorklistFromBioHubItem> worklistFromBioHubItems);
}

public class ListDocumentsMapper : IListDocumentsMapper
{   
  
    public IEnumerable<DocumentItemDto> Map(List<Document> entities,
        List<WorklistToBioHubItem> worklistToBioHubItems,
        List<WorklistFromBioHubItem> worklistFromBioHubItems)
    {

        List<DocumentItemDto> listItems = new List<DocumentItemDto>();

        Dictionary<string, Guid> folderIds = new Dictionary<string, Guid>();

        List<DocumentFileType> intoBioHubDocumentTypes = new List<DocumentFileType>();
        List<DocumentFileType> fromBioHubDocumentTypes = new List<DocumentFileType>();

        intoBioHubDocumentTypes.Add(DocumentFileType.Annex2OfSMTA1);
        intoBioHubDocumentTypes.Add(DocumentFileType.BookingFormOfSMTA1);
        intoBioHubDocumentTypes.Add(DocumentFileType.PackagingList);
        intoBioHubDocumentTypes.Add(DocumentFileType.DangerousGoodsDeclaration);
        intoBioHubDocumentTypes.Add(DocumentFileType.ExportPermit);
        intoBioHubDocumentTypes.Add(DocumentFileType.ImportPermit);
        intoBioHubDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatA);
        intoBioHubDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatB);
        intoBioHubDocumentTypes.Add(DocumentFileType.Other);

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

        var availableDocuments = entities.Where(x => x.IsDocumentFile != false && x.IsDeleted == false && x.DeletedOn == null && x.Approved == true);

        var laboratoryIds = availableDocuments.Where(x => x.LaboratoryId != null).Select(x => x.LaboratoryId).Distinct().ToList();

        foreach (var laboratoryId in laboratoryIds)
        {
            if (!listItems.Select(x => x.Id).Contains(laboratoryId.GetValueOrDefault()))
            {
                var laboratory = entities.FirstOrDefault(x => x.LaboratoryId == laboratoryId)?.Laboratory;

                AddFolder(listItems, laboratoryId.GetValueOrDefault(), laboratory?.Name, null);
            }

            var laboratoryDocuments = availableDocuments.Where(x => x.LaboratoryId == laboratoryId).ToList();


            Guid SMTAFolderId = Guid.NewGuid();
            Guid ShipmentsFolderId = Guid.NewGuid();
            Guid SMTA1FolderId = Guid.NewGuid();
            Guid SMTA2FolderId = Guid.NewGuid();
            Guid IntoBioHubFolderId = Guid.NewGuid();
            Guid FromBioHubFolderId = Guid.NewGuid();


            if (ContainsSMTADocuments(laboratoryDocuments) && !ContainsFolder(listItems, laboratoryId, "SMTA"))
            {
                AddFolder(listItems, SMTAFolderId, "SMTA", laboratoryId);
            }
            if (ContainsShipmentDocuments(laboratoryDocuments) && !ContainsFolder(listItems, laboratoryId, "Shipments"))
            {
                AddFolder(listItems, ShipmentsFolderId, "Shipments", laboratoryId);
            }


            if (ContainsSMTA1Documents(laboratoryDocuments))
            {
                if (!ContainsFolder(listItems, SMTAFolderId, "SMTA1"))
                {
                    AddFolder(listItems, SMTA1FolderId, "SMTA1", SMTAFolderId);
                }
                AddSMTADocumentFiles(laboratoryDocuments, listItems, SMTA1FolderId, DocumentFileType.SMTA1);
            }


            if (ContainsSMTA2Documents(laboratoryDocuments))
            {

                if (!ContainsFolder(listItems, SMTAFolderId, "SMTA2"))
                {
                    AddFolder(listItems, SMTA2FolderId, "SMTA2", SMTAFolderId);
                }

                AddSMTADocumentFiles(laboratoryDocuments, listItems, SMTA2FolderId, DocumentFileType.SMTA2);

            }

            if (ContainsIntoBioHubDocuments(laboratoryDocuments))
            {
                var intoBioHubDocuments = laboratoryDocuments.Where(x => intoBioHubDocumentTypes.Contains(x.Type.GetValueOrDefault())).ToList();
                                
                if (!ContainsFolder(listItems, ShipmentsFolderId, "Into BioHub"))
                {
                    AddFolder(listItems, IntoBioHubFolderId, "Into BioHub", ShipmentsFolderId);
                }

              

                foreach (var document in intoBioHubDocuments)
                {
                    var documentWorklistToBioHubItemIds = document.WorklistToBioHubItemDocuments.Select(x => x.WorklistToBioHubItemId);

                    foreach (var worklistToBioHubItem in worklistToBioHubItems.Where(x => documentWorklistToBioHubItemIds.Contains(x.Id)))
                    {

                        Guid ShipmentReferenceFolderId = Guid.NewGuid();

                        if (!ContainsFolder(listItems, IntoBioHubFolderId, worklistToBioHubItem.ReferenceNumber))
                        {
                            AddFolder(listItems, ShipmentReferenceFolderId, worklistToBioHubItem.ReferenceNumber, IntoBioHubFolderId);
                        }
                        else
                        {
                            ShipmentReferenceFolderId = listItems.Where(x => x.ParentId == IntoBioHubFolderId && x.Name == worklistToBioHubItem.ReferenceNumber).FirstOrDefault().Id;
                        }


                        Guid folderId = Guid.NewGuid();


                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(document.Type.GetValueOrDefault().ToString()))
                        {
                            AddFolder(listItems, folderId, document.Type.ToString(), ShipmentReferenceFolderId);
                        }

                        else
                        {
                            folderId = listItems.Where(x => x.ParentId == ShipmentReferenceFolderId && x.Name == document.Type.GetValueOrDefault().ToString()).FirstOrDefault().Id;
                        }

                        AddDocumentFile(document, listItems, folderId, Shared.Enums.ShipmentDirection.ToBioHub, document.Type.GetValueOrDefault());


                    }

                }
            }

            if (ContainsFromBioHubDocuments(laboratoryDocuments))
            {

                if (!ContainsFolder(listItems, ShipmentsFolderId, "From BioHub"))
                {
                    AddFolder(listItems, FromBioHubFolderId, "From BioHub", ShipmentsFolderId);
                }
                var fromBioHubDocuments = laboratoryDocuments.Where(x => fromBioHubDocumentTypes.Contains(x.Type.GetValueOrDefault())).ToList();

                foreach (var document in fromBioHubDocuments)
                {
                    var documentWorklistFromBioHubItemIds = document.WorklistFromBioHubItemDocuments.Select(x => x.WorklistFromBioHubItemId);

                    foreach (var worklistFromBioHubItem in worklistFromBioHubItems.Where(x => documentWorklistFromBioHubItemIds.Contains(x.Id)))
                    {

                        Guid ShipmentReferenceFolderId = Guid.NewGuid();

                        if (!ContainsFolder(listItems, FromBioHubFolderId, worklistFromBioHubItem.ReferenceNumber))
                        {
                            AddFolder(listItems, ShipmentReferenceFolderId, worklistFromBioHubItem.ReferenceNumber, FromBioHubFolderId);
                        }
                        else
                        {
                            ShipmentReferenceFolderId = listItems.Where(x => x.ParentId == FromBioHubFolderId && x.Name == worklistFromBioHubItem.ReferenceNumber).FirstOrDefault().Id;
                        }


                        Guid folderId = Guid.NewGuid();


                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(document.Type.GetValueOrDefault().ToString()))
                        {
                            AddFolder(listItems, folderId, document.Type.ToString(), ShipmentReferenceFolderId);
                        }

                        else
                        {
                            folderId = listItems.Where(x => x.ParentId == ShipmentReferenceFolderId && x.Name == document.Type.GetValueOrDefault().ToString()).FirstOrDefault().Id;
                        }

                        AddDocumentFile(document, listItems, folderId, Shared.Enums.ShipmentDirection.FromBioHub, document.Type.GetValueOrDefault());

                    }

                }
            }
        }

        var bioHubFacilityIds = availableDocuments.Where(x => x.BioHubFacilityId != null).Select(x => x.BioHubFacilityId).Distinct().ToList();

        foreach (var bioHubFacilityId in bioHubFacilityIds)
        {
            if (!listItems.Select(x => x.Id).Contains(bioHubFacilityId.GetValueOrDefault()))
            {
                var bioHubFacility = entities.FirstOrDefault(x => x.BioHubFacilityId == bioHubFacilityId)?.BioHubFacility;

                AddFolder(listItems, bioHubFacilityId.GetValueOrDefault(), bioHubFacility?.Name, null);
            }

            var bioHubFacilityDocuments = availableDocuments.Where(x => x.BioHubFacilityId == bioHubFacilityId).ToList();


            Guid SMTAFolderId = Guid.NewGuid();
            Guid ShipmentsFolderId = Guid.NewGuid();
            Guid SMTA1FolderId = Guid.NewGuid();
            Guid SMTA2FolderId = Guid.NewGuid();
            Guid IntoBioHubFolderId = Guid.NewGuid();
            Guid FromBioHubFolderId = Guid.NewGuid();


            if (ContainsSMTADocuments(bioHubFacilityDocuments) && !ContainsFolder(listItems, bioHubFacilityId, "SMTA"))
            {
                AddFolder(listItems, SMTAFolderId, "SMTA", bioHubFacilityId);
            }
            if (ContainsShipmentDocuments(bioHubFacilityDocuments) && !ContainsFolder(listItems, bioHubFacilityId, "Shipments"))
            {
                AddFolder(listItems, ShipmentsFolderId, "Shipments", bioHubFacilityId);
            }


            if (ContainsSMTA1Documents(bioHubFacilityDocuments))
            {
                if (!ContainsFolder(listItems, SMTAFolderId, "SMTA1"))
                {
                    AddFolder(listItems, SMTA1FolderId, "SMTA1", SMTAFolderId);
                }
                AddSMTADocumentFiles(bioHubFacilityDocuments, listItems, SMTA1FolderId, DocumentFileType.SMTA1);
            }


            if (ContainsSMTA2Documents(bioHubFacilityDocuments))
            {

                if (!ContainsFolder(listItems, SMTAFolderId, "SMTA2"))
                {
                    AddFolder(listItems, SMTA2FolderId, "SMTA2", SMTAFolderId);
                }

                AddSMTADocumentFiles(bioHubFacilityDocuments, listItems, SMTA2FolderId, DocumentFileType.SMTA2);

            }

            if (ContainsIntoBioHubDocuments(bioHubFacilityDocuments))
            {
                var intoBioHubDocuments = bioHubFacilityDocuments.Where(x => intoBioHubDocumentTypes.Contains(x.Type.GetValueOrDefault())).ToList();

                if (!ContainsFolder(listItems, ShipmentsFolderId, "Into BioHub"))
                {
                    AddFolder(listItems, IntoBioHubFolderId, "Into BioHub", ShipmentsFolderId);
                }



                foreach (var document in intoBioHubDocuments)
                {
                    var documentWorklistToBioHubItemIds = document.WorklistToBioHubItemDocuments.Select(x => x.WorklistToBioHubItemId);

                    foreach (var worklistToBioHubItem in worklistToBioHubItems.Where(x => documentWorklistToBioHubItemIds.Contains(x.Id)))
                    {

                        Guid ShipmentReferenceFolderId = Guid.NewGuid();

                        if (!ContainsFolder(listItems, IntoBioHubFolderId, worklistToBioHubItem.ReferenceNumber))
                        {
                            AddFolder(listItems, ShipmentReferenceFolderId, worklistToBioHubItem.ReferenceNumber, IntoBioHubFolderId);
                        }
                        else
                        {
                            ShipmentReferenceFolderId = listItems.Where(x => x.ParentId == IntoBioHubFolderId && x.Name == worklistToBioHubItem.ReferenceNumber).FirstOrDefault().Id;
                        }


                        Guid folderId = Guid.NewGuid();


                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(document.Type.GetValueOrDefault().ToString()))
                        {
                            AddFolder(listItems, folderId, document.Type.ToString(), ShipmentReferenceFolderId);
                        }

                        else
                        {
                            folderId = listItems.Where(x => x.ParentId == ShipmentReferenceFolderId && x.Name == document.Type.GetValueOrDefault().ToString()).FirstOrDefault().Id;
                        }

                        AddDocumentFile(document, listItems, folderId, Shared.Enums.ShipmentDirection.ToBioHub, document.Type.GetValueOrDefault());


                    }

                }
            }

            if (ContainsFromBioHubDocuments(bioHubFacilityDocuments))
            {

                if (!ContainsFolder(listItems, ShipmentsFolderId, "From BioHub"))
                {
                    AddFolder(listItems, FromBioHubFolderId, "From BioHub", ShipmentsFolderId);
                }
                var fromBioHubDocuments = bioHubFacilityDocuments.Where(x => fromBioHubDocumentTypes.Contains(x.Type.GetValueOrDefault())).ToList();

                foreach (var document in fromBioHubDocuments)
                {
                    var documentWorklistFromBioHubItemIds = document.WorklistFromBioHubItemDocuments.Select(x => x.WorklistFromBioHubItemId);

                    foreach (var worklistFromBioHubItem in worklistFromBioHubItems.Where(x => documentWorklistFromBioHubItemIds.Contains(x.Id)))
                    {

                        Guid ShipmentReferenceFolderId = Guid.NewGuid();

                        if (!ContainsFolder(listItems, FromBioHubFolderId, worklistFromBioHubItem.ReferenceNumber))
                        {
                            AddFolder(listItems, ShipmentReferenceFolderId, worklistFromBioHubItem.ReferenceNumber, FromBioHubFolderId);
                        }
                        else
                        {
                            ShipmentReferenceFolderId = listItems.Where(x => x.ParentId == FromBioHubFolderId && x.Name == worklistFromBioHubItem.ReferenceNumber).FirstOrDefault().Id;
                        }


                        Guid folderId = Guid.NewGuid();


                        if (!listItems.Where(x => x.ParentId == ShipmentReferenceFolderId).Select(x => x.Name).Contains(document.Type.GetValueOrDefault().ToString()))
                        {
                            AddFolder(listItems, folderId, document.Type.ToString(), ShipmentReferenceFolderId);
                        }

                        else
                        {
                            folderId = listItems.Where(x => x.ParentId == ShipmentReferenceFolderId && x.Name == document.Type.GetValueOrDefault().ToString()).FirstOrDefault().Id;
                        }

                        AddDocumentFile(document, listItems, folderId, Shared.Enums.ShipmentDirection.FromBioHub, document.Type.GetValueOrDefault());

                    }

                }
            }
        }
        return listItems.OrderBy(x => x.Name);
    }

    private bool ContainsFolder(List<DocumentItemDto> listItems, Guid? parentId, string folderName)
    {
        var filteredElements = listItems.Where(x => x.ParentId == parentId);

        if (filteredElements.Select(x => x.Name).Contains(folderName))
        {
            return true;
        }
        return false;
    }

    private void AddFolder(List<DocumentItemDto> listItems, Guid folderId, string folderName, Guid? parentFolderId)
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

    private void AddSMTADocumentFiles(List<Document> entities, List<DocumentItemDto> listItems, Guid parentFolderId, DocumentFileType documentFileType)
    {
        List<Document>? filteredDocuments;

        if (documentFileType == DocumentFileType.SMTA1)
        {

            filteredDocuments = entities.Where(x => x.SMTA1WorkflowItemDocuments.Any()).Where(x => x.Type == documentFileType).ToList();
        }
        else
        {
            filteredDocuments = entities.Where(x => x.SMTA2WorkflowItemDocuments.Any()).Where(x => x.Type == documentFileType).ToList();

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

    private void AddDocumentFile(Document entity, List<DocumentItemDto> listItems, Guid parentFolderId, Shared.Enums.ShipmentDirection shipmentDirection, DocumentFileType documentFileType)
    {

        DocumentItemDto documentItemDto = new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Extension = entity.Extension,
            Type = DocumentItemType.File,
            FileType = entity.Type,
            UploadTime = entity.OperationDate != null ? entity.OperationDate : entity.CreationDate,
            UploadedById = entity.UploadedById,
            UploadedBy = entity.UploadedBy.FirstName + " " + entity.UploadedBy.LastName,
            ParentId = parentFolderId,
        };
        listItems.Add(documentItemDto);

    }

    private bool ContainsSMTADocuments(List<Document> entities)
    {
        if (
            entities.Select(x => x.Type).Contains(DocumentFileType.SMTA1) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.SMTA2)
        )
        {
            return true;
        };
        return false;
    }

    private bool ContainsShipmentDocuments(List<Document> entities)
    {
        if (
            entities.Select(x => x.Type).Contains(DocumentFileType.Annex2OfSMTA1) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.Annex2OfSMTA2) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.BookingFormOfSMTA1) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.BookingFormOfSMTA2) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.BiosafetyChecklist) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.NonCommercialInvoiceCatA) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.NonCommercialInvoiceCatB) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.PackagingList) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.DangerousGoodsDeclaration) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.ExportPermit) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.ImportPermit) ||
            entities.Select(x => x.Type).Contains(DocumentFileType.Other)
        )
        {
            return true;
        };
        return false;
    }

    private bool ContainsSMTA1Documents(List<Document> entities)
    {
        if (
            entities.Select(x => x.Type).Contains(DocumentFileType.SMTA1)
        )
        {
            return true;
        };
        return false;
    }

    private bool ContainsSMTA2Documents(List<Document> entities)
    {
        if (
            entities.Select(x => x.Type).Contains(DocumentFileType.SMTA2)
        )
        {
            return true;
        };
        return false;
    }

    private bool ContainsIntoBioHubDocuments(List<Document> documents)
    {
        if (
            documents.Select(x => x.Type).Contains(DocumentFileType.Annex2OfSMTA1) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.BookingFormOfSMTA1) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.NonCommercialInvoiceCatA) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.NonCommercialInvoiceCatB) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.PackagingList) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.DangerousGoodsDeclaration) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.ExportPermit) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.ImportPermit) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.Other)
        )
        {
            return true;
        };
        return false;
    }

    private bool ContainsFromBioHubDocuments(List<Document> documents)
    {

        if (
            documents.Select(x => x.Type).Contains(DocumentFileType.Annex2OfSMTA2) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.BiosafetyChecklist) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.BookingFormOfSMTA2) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.NonCommercialInvoiceCatA) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.NonCommercialInvoiceCatB) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.PackagingList) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.DangerousGoodsDeclaration) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.ExportPermit) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.ImportPermit) ||
            documents.Select(x => x.Type).Contains(DocumentFileType.Other)
        )
        {
            return true;
        };
        return false;
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
}