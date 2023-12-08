import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubItemsService } from "@/services/worklistToBioHubItems/WorklistToBioHubItemsService";
import { WorklistToBioHubHistoryItemsService } from "@/services/worklistToBioHubHistoryItems/WorklistToBioHubHistoryItemsService";
import { RoleType } from "@/models/enums/RoleType";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { worklistToBioHubItems } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListWorklistToBioHubItemResponse } from "@/services/worklistToBioHubItems/models/ListWorklistToBioHubItem";
import { ListWorklistToBioHubHistoryItemResponse } from "@/services/worklistToBioHubHistoryItems/models/ListWorklistToBioHubHistoryItem";
import { ListDashboardWorklistToBioHubItemResponse } from "@/services/worklistToBioHubItems/models/ListDashboardWorklistToBioHubItem";

import { CreateWorklistToBioHubItemResponse } from "@/services/worklistToBioHubItems/models/CreateWorklistToBioHubItem";
import { DeleteWorklistToBioHubItemResponse } from "@/services/worklistToBioHubItems/models/DeleteWorklistToBioHubItem";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadWorklistToBioHubItemQuery } from "@/services/worklistToBioHubItems/models/ReadWorklistToBioHubItem";
import { ListWorklistToBioHubHistoryItemQuery } from "@/services/worklistToBioHubHistoryItems/models/ListWorklistToBioHubHistoryItem";

import { ReadWorklistToBioHubItemResponse } from "@/services/worklistToBioHubItems/models/ReadWorklistToBioHubItem";
import { AppModule } from "../../store/MainStore";
import { WorklistToBioHubItemFileInfo } from "@/models/WorklistToBioHubItemFileInfo";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import { DocumentFileType } from "@/models/enums/DocumentFileType";

import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { AttachmentType } from "@/models/enums/AttachmentType";
import { MaterialClinicalDetailGridItem } from "@/models/MaterialClinicalDetailGridItem";

import { ShipmentDocument } from "@/models/ShipmentDocument";

import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { WorklistToBioHubItemMaterial } from "@/models/WorklistToBioHubItemMaterial";

import { WorklistToBioHubItemEventsService } from "@/services/worklistToBioHubItemEvents/WorklistToBioHubItemEventsService";
import { WorklistTimelineEventsDay } from "@/models/WorklistTimelineEventsDay";
import {
  ListWorklistToBioHubItemEventQuery,
  ListWorklistToBioHubItemEventResponse,
} from "@/services/worklistToBioHubItemEvents/models/ListWorklistToBioHubItemEvent";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import { MaterialLaboratoryAnalysisInformation } from "@/models/MaterialLaboratoryAnalysisInformation";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { SeedData } from "@/models/constants/SeedData";

declare global {
  interface Crypto {
    randomUUID: () => string;
  }
}

export interface WorklistToBioHubItemState {
  WorklistToBioHubItemCreate: WorklistToBioHubItem | undefined;
  WorklistToBioHubItem: WorklistToBioHubItem | undefined;
  WorklistToBioHubItems: Array<WorklistToBioHubItem>;
  WorklistToBioHubHistoryItems: Array<WorklistToBioHubItem>;
  WorklistToBioHubItemDocument: File | undefined;
  WorklistToBioHubItemSignature: File | undefined;
  WorklistTimelines: Array<WorklistTimeline>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "worklistToBioHubItems",
  store: store,
})
class WorklistToBioHubItemStore
  extends VuexModule
  implements WorklistToBioHubItemState
{
  // Private variables
  private worklistToBioHubItemCreate: { value: WorklistToBioHubItem } = {
    value: this.emptyWorklistToBioHubItem,
  };

  private worklistToBioHubItem: { value: WorklistToBioHubItem | undefined } = {
    value: undefined,
  };

  private worklistToBioHubItemDocumentInfo: {
    value: WorklistToBioHubItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private worklistToBioHubItemSignatureInfo: {
    value: WorklistToBioHubItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private worklistToBioHubItemDownloadFileInfo: {
    value: WorklistToBioHubItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private worklistToBioHubItems: { value: Array<WorklistToBioHubItem> } = {
    value: worklistToBioHubItems,
  };

  private worklistToBioHubItemDocument: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private worklistToBioHubItemSignature: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private worklistToBioHubItemDownloadFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private worklistToBioHubHistoryItems: { value: Array<WorklistToBioHubItem> } =
    {
      value: worklistToBioHubItems,
    };

  private worklistTimelines: { value: Array<WorklistTimeline> } = {
    value: [],
  };

  private temporaryMaterialShippingInformation: {
    value: MaterialShippingInformation | undefined;
  } = {
    value: undefined,
  };

  private temporaryShipmentMaterial: {
    value: MaterialClinicalDetail | undefined;
  } = {
    value: undefined,
  };

  private attachmentType: {
    value: AttachmentType | undefined;
  } = {
    value: AttachmentType.Document,
  };

  private currentBookingFormId: {
    value: string | undefined;
  } = {
    value: undefined,
  };

  private currentCourierId: {
    value: string | undefined;
  } = {
    value: undefined,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_WORKLISTTOBIOHUBITEM_CREATE(
    worklistToBioHubItem: WorklistToBioHubItem
  ): void {
    this.worklistToBioHubItemCreate.value = worklistToBioHubItem;
  }

  // Details - Edit
  @Mutation
  public SET_WORKLISTTOBIOHUBITEM(
    worklistToBioHubItem: WorklistToBioHubItem | undefined
  ): void {
    this.worklistToBioHubItem.value = worklistToBioHubItem;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEMDOWNLOADFILEINFO(
    worklistToBioHubItemDownloadFileInfo:
      | WorklistToBioHubItemFileInfo
      | undefined
  ): void {
    this.worklistToBioHubItemDownloadFileInfo.value =
      worklistToBioHubItemDownloadFileInfo;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEM_DOCUMENT(file: File | undefined): void {
    this.worklistToBioHubItemDocument.value = file;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEM_SIGNATURE(file: File | undefined): void {
    this.worklistToBioHubItemSignature.value = file;
  }

  @Mutation
  public CLEAR_WORKLISTTOBIOHUBITEM(): void {
    this.worklistToBioHubItem.value = undefined;
    this.worklistToBioHubItemDocument.value = undefined;
    this.worklistToBioHubItemSignature.value = undefined;
    this.worklistToBioHubItemDownloadFile.value = undefined;
  }

  @Mutation
  public CLEAR_WORKLISTTOBIOHUBITEM_CREATE(): void {
    this.worklistToBioHubItem.value = Object.create({
      CurrentStatus: WorklistToBioHubStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: WorklistToBioHubStatus.RequestInitiation,
      WorklistItemTitle: "",
      LastSubmissionApproved: "",
      LaboratoryName: "",
      BioHubFacilityName: "",
      OperationDate: "",
      UserName: "",
      Comment: "",
      SMTA1DocumentId: "",
      SMTA1DocumentName: "",
      LaboratoryId: "",
      BioHubFacilityId: "",
      OriginalDocumentTemplateSMTA1DocumentId: "",
      DocumentTemplateFileType: DocumentFileType.Annex2OfSMTA1,
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
      IsPast: false,
      AssignedOperationDate: null,
    });
    this.worklistToBioHubItemDocument.value = undefined;
    this.worklistToBioHubItemSignature.value = undefined;
    this.worklistToBioHubItemDownloadFile.value = undefined;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEM_CREATE_ISPAST(isPast: boolean): void {
    if (this.worklistToBioHubItemCreate?.value != undefined) {
      this.worklistToBioHubItemCreate!.value.IsPast = isPast;
    }
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEM_CREATE_ASSIGNED_OPERATION_DATE(
    assignedOperationDate: Date | null
  ): void {
    if (this.worklistToBioHubItemCreate?.value != undefined) {
      this.worklistToBioHubItemCreate!.value.AssignedOperationDate =
        assignedOperationDate;
    }
  }

  // List
  @Mutation
  public SET_WORKLISTTOBIOHUBITEMS(
    worklistToBioHubItems: Array<WorklistToBioHubItem>
  ): void {
    this.worklistToBioHubItems.value = worklistToBioHubItems;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBHISTORYITEMS(
    worklistToBioHubHistoryItems: Array<WorklistToBioHubItem>
  ): void {
    this.worklistToBioHubHistoryItems.value = worklistToBioHubHistoryItems;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEMEVENTS(
    worklistTimelines: Array<WorklistTimeline>
  ): void {
    this.worklistTimelines.value = worklistTimelines;
  }

  @Mutation
  public CLEAR_TEMPORARY_MATERIAL_SHIPPING_INFORMATION(): void {
    this.temporaryMaterialShippingInformation.value = Object.create({
      Id: "",
      MaterialProductId: "",
      Condition: "",
      AdditionalInformation: "",
      Quantity: 0,
      PatientStatus: "",
      MaterialClinicalDetails: [],
      MaterialLaboratoryAnalysisInformation: [],
      Amount: 0,
    }) as MaterialShippingInformation;
  }

  @Mutation
  public RESET_ATTACHMENT_TYPE(): void {
    this.attachmentType.value = AttachmentType.Document;
  }

  @Mutation
  public SET_NEW_TEMPORARY_MATERIAL_SHIPPING_INFORMATION(): void {
    this.temporaryMaterialShippingInformation.value = Object.create({
      Id: crypto.randomUUID(),
      MaterialProductId: "",
      TransportCategoryId: "",
      Condition: "",
      AdditionalInformation: "",
      Quantity: 0,
      MaterialClinicalDetails: [],
      MaterialLaboratoryAnalysisInformation: [],
      Amount: 0,
    }) as MaterialShippingInformation;
  }

  @Mutation
  public SET_TEMPORARY_MATERIAL_SHIPPING_INFORMATION(
    item: MaterialShippingInformation | undefined
  ): void {
    this.temporaryMaterialShippingInformation.value = item;
  }

  @Mutation
  public SET_ATTACHMENT_TYPE(attachmentType: AttachmentType | undefined): void {
    this.attachmentType.value = attachmentType;
  }

  @Mutation
  public INITIALIZE_TEMPORARY_MATERIAL_CLINICAL_DETAILS(
    info: Map<string, string>
  ): void {
    const quantity = parseInt(info.get("Quantity") ?? "0");

    const materialNumber = info.get("MaterialNumber");
    for (let i = 0; i < quantity; i++) {
      const temporaryMaterialClinicalDetail = Object.create({
        Id: crypto.randomUUID(),
        MaterialNumber: materialNumber,
        CollectionDate: null,
        Location: "",
        IsolationHostTypeId: "",
        Gender: null,
        Age: 0,
        PatientStatus: "",
        Condition: null,
        Note: "",
      }) as MaterialClinicalDetail;

      this.temporaryMaterialShippingInformation.value?.MaterialClinicalDetails.push(
        temporaryMaterialClinicalDetail
      );
    }
  }

  @Mutation
  public INITIALIZE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
    info: Map<string, string>
  ): void {
    const quantity = parseInt(info.get("Quantity") ?? "0");

    const materialNumber = info.get("MaterialNumber");
    for (let i = 0; i < quantity; i++) {
      const temporaryMaterialLaboratoryAnalysisInformation = Object.create({
        Id: crypto.randomUUID(),
        MaterialNumber: materialNumber,
        FreezingDate: null,
        Temperature: null,
        UnitOfMeasureId: "",
        VirusConcentration: "",
        CulturingCellLine: "",
        CulturingPassagesNumber: "",
        CollectedSpecimenTypes: [],
        TypeOfTransportMedium: "",
        BrandOfTransportMedium: "",
        GSDUploadedToDatabase: null,
        DatabaseUsedForGSDUploadingId: "",
        AccessionNumberInGSDDatabase: "",
      }) as MaterialLaboratoryAnalysisInformation;

      this.temporaryMaterialShippingInformation.value?.MaterialLaboratoryAnalysisInformation.push(
        temporaryMaterialLaboratoryAnalysisInformation
      );
    }
  }

  @Mutation
  public ADD_TEMPORARY_MATERIAL_SHIPPING_INFORMATION(): void {
    const temporaryMaterialShippingInformation =
      this.temporaryMaterialShippingInformation.value;

    if (temporaryMaterialShippingInformation !== undefined) {
      const newMaterialShippingInformation = Object.assign({
        Id: temporaryMaterialShippingInformation.Id,
        MaterialNumber: temporaryMaterialShippingInformation.MaterialNumber,
        MaterialProductId:
          temporaryMaterialShippingInformation.MaterialProductId,
        TransportCategoryId:
          temporaryMaterialShippingInformation.TransportCategoryId,
        Condition: temporaryMaterialShippingInformation.Condition,
        AdditionalInformation:
          temporaryMaterialShippingInformation.AdditionalInformation,
        Quantity: temporaryMaterialShippingInformation.Quantity,
        Amount: temporaryMaterialShippingInformation.Amount,
        MaterialClinicalDetails: [] as Array<MaterialClinicalDetail>,
        MaterialLaboratoryAnalysisInformation:
          [] as Array<MaterialLaboratoryAnalysisInformation>,
      }) as MaterialShippingInformation;

      temporaryMaterialShippingInformation.MaterialClinicalDetails.forEach(
        (element) => {
          const newMaterialClinicalDetail = Object.assign({
            Id: element.Id,
            MaterialShippingInformationId:
              temporaryMaterialShippingInformation.Id,
            MaterialProductId:
              temporaryMaterialShippingInformation.MaterialProductId,
            TransportCategoryId:
              temporaryMaterialShippingInformation.TransportCategoryId,
            MaterialNumber: element.MaterialNumber,
            CollectionDate: element.CollectionDate,
            Location: element.Location,
            IsolationHostTypeId: element.IsolationHostTypeId,
            Gender: element.Gender,
            Age: element.Age,
            PatientStatus: element.PatientStatus,
          }) as MaterialClinicalDetail;

          newMaterialShippingInformation.MaterialClinicalDetails.push(
            newMaterialClinicalDetail
          );
        }
      );

      temporaryMaterialShippingInformation.MaterialLaboratoryAnalysisInformation.forEach(
        (element) => {
          let CollectedSpecimenTypes = [] as Array<string>;
          let TypeOfTransportMedium = "";
          let BrandOfTransportMedium = "";

          let CulturingCellLine = "";
          let CulturingPassagesNumber = null;

          let DatabaseUsedForGSDUploadingId = "";
          let AccessionNumberInGSDDatabase = "";

          if (
            temporaryMaterialShippingInformation.MaterialProductId ==
            SeedData.ClinicalSpecimenProductId
          ) {
            CollectedSpecimenTypes = element.CollectedSpecimenTypes;
            TypeOfTransportMedium = element.TypeOfTransportMedium;
            BrandOfTransportMedium = element.BrandOfTransportMedium;
          } else if (
            temporaryMaterialShippingInformation.MaterialProductId ==
            SeedData.CulturedIsolateProductId
          ) {
            CulturingCellLine = element.CulturingCellLine;
            CulturingPassagesNumber = element.CulturingPassagesNumber;
          }

          if (element.GSDUploadedToDatabase == YesNoOption.Yes) {
            DatabaseUsedForGSDUploadingId =
              element.DatabaseUsedForGSDUploadingId;
            AccessionNumberInGSDDatabase = element.AccessionNumberInGSDDatabase;
          }

          const newMaterialLaboratoryAnalysisInformation = Object.assign({
            Id: element.Id,
            MaterialShippingInformationId:
              temporaryMaterialShippingInformation.Id,
            MaterialProductId:
              temporaryMaterialShippingInformation.MaterialProductId,
            TransportCategoryId:
              temporaryMaterialShippingInformation.TransportCategoryId,
            MaterialNumber: element.MaterialNumber,
            FreezingDate: element.FreezingDate,
            Temperature: element.Temperature,
            UnitOfMeasureId: element.UnitOfMeasureId,
            VirusConcentration: element.VirusConcentration,
            CulturingCellLine: CulturingCellLine,
            CulturingPassagesNumber: CulturingPassagesNumber,
            CollectedSpecimenTypes: CollectedSpecimenTypes,
            TypeOfTransportMedium: TypeOfTransportMedium,
            BrandOfTransportMedium: BrandOfTransportMedium,
            GSDUploadedToDatabase: element.GSDUploadedToDatabase,
            DatabaseUsedForGSDUploadingId: DatabaseUsedForGSDUploadingId,
            AccessionNumberInGSDDatabase: AccessionNumberInGSDDatabase,
          }) as MaterialLaboratoryAnalysisInformation;

          newMaterialShippingInformation.MaterialLaboratoryAnalysisInformation.push(
            newMaterialLaboratoryAnalysisInformation
          );
        }
      );

      this.worklistToBioHubItem.value?.MaterialShippingInformations.push(
        newMaterialShippingInformation
      );

      this.temporaryMaterialShippingInformation.value = undefined;
    }
  }

  @Mutation
  public UPDATE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
    item: MaterialLaboratoryAnalysisInformation
  ): void {
    if (this.temporaryMaterialShippingInformation.value) {
      const materialLaboratoryAnalysisInformationIndex =
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation.map(
          function (e) {
            return e.Id;
          }
        ).indexOf(item.Id);

      if (materialLaboratoryAnalysisInformationIndex !== -1) {
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].FreezingDate = item.FreezingDate;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].Temperature = item.Temperature;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].UnitOfMeasureId = item.UnitOfMeasureId;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].VirusConcentration = item.VirusConcentration;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].CulturingCellLine = item.CulturingCellLine;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].CulturingPassagesNumber = item.CulturingPassagesNumber;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].CollectedSpecimenTypes = item.CollectedSpecimenTypes;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].TypeOfTransportMedium = item.TypeOfTransportMedium;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].BrandOfTransportMedium = item.BrandOfTransportMedium;
        this.temporaryMaterialShippingInformation.value.MaterialLaboratoryAnalysisInformation[
          materialLaboratoryAnalysisInformationIndex
        ].GSDUploadedToDatabase = item.GSDUploadedToDatabase;
      }
    }
  }

  @Mutation
  public UPDATE_TEMPORARY_MATERIAL_CLINICAL_DETAIL(
    item: MaterialClinicalDetail
  ): void {
    if (this.temporaryMaterialShippingInformation.value) {
      const materialClinicalDetailIndex =
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails.map(
          function (e) {
            return e.Id;
          }
        ).indexOf(item.Id);

      if (materialClinicalDetailIndex !== -1) {
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails[
          materialClinicalDetailIndex
        ].CollectionDate = item.CollectionDate;
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails[
          materialClinicalDetailIndex
        ].Location = item.Location;
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails[
          materialClinicalDetailIndex
        ].IsolationHostTypeId = item.IsolationHostTypeId;
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails[
          materialClinicalDetailIndex
        ].Gender = item.Gender;
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails[
          materialClinicalDetailIndex
        ].Age = item.Age;
        this.temporaryMaterialShippingInformation.value.MaterialClinicalDetails[
          materialClinicalDetailIndex
        ].PatientStatus = item.PatientStatus;
      }
    }
  }

  @Mutation
  public REMOVE_MATERIAL_CLINICAL_DETAIL(info: Map<string, string>): void {
    const materialShippingInformationArray =
      this.worklistToBioHubItem.value?.MaterialShippingInformations;
    if (materialShippingInformationArray !== undefined) {
      const materialShippingInformationId = info.get(
        "MaterialShippingInformationId"
      );
      const materialClinicalDetailId = info.get("MaterialClinicalDetailId");
      if (
        materialShippingInformationId !== undefined &&
        materialClinicalDetailId !== undefined
      ) {
        const materialShippingInformation =
          materialShippingInformationArray.find(
            (x) => x.Id === materialShippingInformationId
          );
        if (materialShippingInformation !== undefined) {
          const materialClinicalDetailArray =
            materialShippingInformation.MaterialClinicalDetails;

          const index = materialClinicalDetailArray
            .map(function (e) {
              return e.Id;
            })
            .indexOf(materialClinicalDetailId);
          if (index !== -1) {
            materialClinicalDetailArray.splice(index, 1);
          }
        }
      }
    }
  }

  @Mutation
  public REMOVE_MATERIAL_SHIPPING_INFORMATION(id: string): void {
    const array = this.worklistToBioHubItem.value?.MaterialShippingInformations;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(id);
      if (index !== -1) {
        array.splice(index, 1);
      }
    }
  }

  @Mutation
  public UPDATE_MATERIAL_CLINICAL_DETAIL(item: MaterialClinicalDetail): void {
    if (this.worklistToBioHubItem.value !== undefined) {
      const materialShippingInformationIndex =
        this.worklistToBioHubItem.value?.MaterialShippingInformations.map(
          function (e) {
            return e.Id;
          }
        ).indexOf(item.MaterialShippingInformationId);

      if (materialShippingInformationIndex !== -1) {
        const materialShippingInformation =
          this.worklistToBioHubItem.value?.MaterialShippingInformations[
            materialShippingInformationIndex
          ];

        const materialClinicalDetailIndex =
          materialShippingInformation.MaterialClinicalDetails.map(function (e) {
            return e.Id;
          }).indexOf(item.Id);

        if (materialClinicalDetailIndex !== -1) {
          materialShippingInformation.MaterialClinicalDetails[
            materialClinicalDetailIndex
          ].Note = item.Note;
          materialShippingInformation.MaterialClinicalDetails[
            materialClinicalDetailIndex
          ].Condition = item.Condition;
        }
      }
    }
  }

  @Mutation
  public ADD_LABORATORY_FOCAL_POINT(item: WorklistItemUser): void {
    item.Other = "";
    this.worklistToBioHubItem.value?.LaboratoryFocalPoints.push(item);
  }

  @Mutation
  public UPDATE_OTHER_FIELD_LABORATORY_FOCAL_POINT(
    item: WorklistItemUser
  ): void {
    const array = this.worklistToBioHubItem.value?.LaboratoryFocalPoints;

    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].Other = item.Other;
      }
    }
  }

  @Mutation
  public REMOVE_LABORATORY_FOCAL_POINT(id: string): void {
    const array = this.worklistToBioHubItem.value?.LaboratoryFocalPoints;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(id);
      if (index !== -1) {
        array.splice(index, 1);
      }
    }
  }

  @Mutation
  public ADD_BOOKINGFORM_PICKUP_USER(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;

    const item = info["Item"] as WorklistItemUser;

    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);
      if (bookingForm !== undefined) {
        const pickupUserArray = bookingForm.BookingFormPickupUsers;
        pickupUserArray.push(item);
      }
    }
  }

  @Mutation
  public REMOVE_BOOKINGFORM_PICKUP_USER(info: Map<string, string>): void {
    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;

    const bookingFormId = info.get("BookingFormId") ?? "";
    const id = info.get("Id") ?? "";
    //const bookingFormId = this.currentBookingFormId;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);

      if (bookingForm !== undefined) {
        const pickupUserArray = bookingForm.BookingFormPickupUsers;

        if (pickupUserArray !== undefined) {
          const index = pickupUserArray
            .map(function (e) {
              return e.Id;
            })
            .indexOf(id);
          if (index !== -1) {
            pickupUserArray.splice(index, 1);
          }
        }
      }
    }
  }

  @Mutation
  public UPDATE_BOOKINGFORM_PICKUP_USER(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;

    const item = info["Item"] as WorklistItemUser;
    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;
    //const bookingFormId = this.currentBookingFormId;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);
      if (bookingForm !== undefined) {
        const pickupUserArray = bookingForm.BookingFormPickupUsers;
        if (pickupUserArray !== undefined) {
          const index = pickupUserArray
            .map(function (e) {
              return e.Id;
            })
            .indexOf(item.Id);
          if (index !== -1) {
            pickupUserArray[index].Other = item.Other;
          }
        }
      }
    }
  }

  @Mutation
  public ADD_MATERIAL(item: WorklistToBioHubItemMaterial): void {
    this.worklistToBioHubItem.value?.WorklistToBioHubItemMaterials.push(item);
  }

  @Mutation
  public REMOVE_MATERIAL(id: string): void {
    const array =
      this.worklistToBioHubItem.value?.WorklistToBioHubItemMaterials;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(id);
      if (index !== -1) {
        array.splice(index, 1);
      }
    }
  }

  @Mutation
  public ADD_BOOKINGFORM_COURIER_USER(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;

    const item = info["Item"] as WorklistItemUser;

    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;
    //const bookingFormId = this.currentBookingFormId;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);

      if (bookingForm !== undefined) {
        const courierUserArray = bookingForm.BookingFormCourierUsers;
        courierUserArray.push(item);
      }
    }
  }

  @Mutation
  public REMOVE_BOOKINGFORM_COURIER_USER(info: Map<string, string>): void {
    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;

    const bookingFormId = info.get("BookingFormId") ?? "";
    const id = info.get("Id") ?? "";
    //const bookingFormId = this.currentBookingFormId;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);

      if (bookingForm !== undefined) {
        const courierUserArray = bookingForm.BookingFormCourierUsers;

        if (courierUserArray !== undefined) {
          const index = courierUserArray
            .map(function (e) {
              return e.Id;
            })
            .indexOf(id);
          if (index !== -1) {
            courierUserArray.splice(index, 1);
          }
        }
      }
    }
  }

  @Mutation
  public UPDATE_BOOKINGFORM_COURIER_USER(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;

    const item = info["Item"] as WorklistItemUser;
    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;
    //const bookingFormId = this.currentBookingFormId;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);
      if (bookingForm !== undefined) {
        const courierUserArray = bookingForm.BookingFormCourierUsers;
        if (courierUserArray !== undefined) {
          const index = courierUserArray
            .map(function (e) {
              return e.Id;
            })
            .indexOf(item.Id);
          if (index !== -1) {
            courierUserArray[index].Other = item.Other;
          }
        }
      }
    }
  }

  @Mutation
  public CLEAR_BOOKINGFORM_COURIER_USER(bookingFormId: string): void {
    const bookingFormArray = this.worklistToBioHubItem.value?.BookingForms;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);
      if (bookingForm !== undefined) {
        bookingForm.BookingFormCourierUsers = new Array<WorklistItemUser>();
      }
    }
  }

  @Mutation
  public UPDATE_BOOKING_FORM(item: BookingFormOfSMTA): void {
    const array = this.worklistToBioHubItem.value?.BookingForms;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index] = item;
      }
    }
  }

  @Mutation
  public UPDATE_SHIPMENTREFERENCENUMBER_FIELD_BOOKING_FORM(
    item: BookingFormOfSMTA
  ): void {
    const array = this.worklistToBioHubItem.value?.BookingForms;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].ShipmentReferenceNumber = item.ShipmentReferenceNumber;
      }
    }
  }

  @Mutation
  public UPDATE_DATEOFPICKUP_FIELD_BOOKING_FORM(item: BookingFormOfSMTA): void {
    const array = this.worklistToBioHubItem.value?.BookingForms;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].DateOfPickup = item.DateOfPickup;
      }
    }
  }

  @Mutation
  public UPDATE_DATEOFDELIVERY_FIELD_BOOKING_FORM(
    item: BookingFormOfSMTA
  ): void {
    const array = this.worklistToBioHubItem.value?.BookingForms;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].DateOfDelivery = item.DateOfDelivery;
      }
    }
  }

  @Mutation
  public UPDATE_TRANSPORTMODE_FIELD_BOOKING_FORM(
    item: BookingFormOfSMTA
  ): void {
    const array = this.worklistToBioHubItem.value?.BookingForms;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].TransportModeId = item.TransportModeId;
      }
    }
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEM_SHIPMENTDOCUMENTS(
    shipmentDocuments: Array<ShipmentDocument>
  ): void {
    this.worklistToBioHubItem.value!.ShipmentDocuments = shipmentDocuments;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateWorklistToBioHubItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistToBioHubItemsService();
    const worklistToBioHubItem = this.worklistToBioHubItemCreate.value;
    if (worklistToBioHubItem === undefined) {
      this.SET_ERROR({
        message:
          "WorklistToBioHubItemsStore: not expecting worklistToBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(worklistToBioHubItem);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateWorklistToBioHubItemResponse = response.value;
      worklistToBioHubItem.Id = createResponse.Id;
      this.SET_WORKLISTTOBIOHUBITEM(worklistToBioHubItem);
      this.SET_WORKLISTTOBIOHUBITEM_CREATE(this.emptyWorklistToBioHubItem);
      AppModule.SetSuccessNotifications("Successfully created");
      AppModule.HideLoading();
      return;
    }
    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    AppModule.HideLoading();
    throw response.value;
  }

  @Action({ rawError: true })
  public async UpdateWorklistToBioHubItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistToBioHubItemsService();
    const worklistToBioHubItem: WorklistToBioHubItem | undefined =
      this.WorklistToBioHubItem;
    const file =
      this.AttachmentType == AttachmentType.Document
        ? this.worklistToBioHubItemDocument.value
        : this.worklistToBioHubItemSignature.value;
    if (!worklistToBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistToBioHubItemsStore: not expecting worklistToBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(worklistToBioHubItem, file);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_WORKLISTTOBIOHUBITEM(worklistToBioHubItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async UpdateWorklistToBioHubItemShipmentDocuments(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistToBioHubItemsService();
    const worklistToBioHubItem: WorklistToBioHubItem | undefined =
      this.WorklistToBioHubItem;
    const file =
      this.AttachmentType == AttachmentType.Document
        ? this.worklistToBioHubItemDocument.value
        : this.worklistToBioHubItemSignature.value;
    if (!worklistToBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistToBioHubItemsStore: not expecting worklistToBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updateShipmentDocuments(
      worklistToBioHubItem,
      file
    );
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_WORKLISTTOBIOHUBITEM(worklistToBioHubItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListWorklistToBioHubItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new WorklistToBioHubItemsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListWorklistToBioHubItemResponse = response.value;
      this.SET_WORKLISTTOBIOHUBITEMS(listResponse.WorklistToBioHubItems);
      return;
    }

    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListDashboardWorklistToBioHubItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new WorklistToBioHubItemsService();
    const response = await service.listForDashboard({});
    if (isLeft(response)) {
      const listResponse: ListDashboardWorklistToBioHubItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEMS(listResponse.WorklistToBioHubItems);
      return;
    }

    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListWorklistToBioHubHistoryItems(
    worklistToBioHubItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListWorklistToBioHubHistoryItemQuery = {
      WorklistToBioHubItemId: worklistToBioHubItemId,
    };
    const service = new WorklistToBioHubHistoryItemsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListWorklistToBioHubHistoryItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBHISTORYITEMS(
        listResponse.WorklistToBioHubHistoryItems
      );
      return;
    }

    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListWorklistToBioHubItemEvents(
    worklistToBioHubItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListWorklistToBioHubItemEventQuery = {
      WorklistToBioHubItemId: worklistToBioHubItemId,
    };
    const service = new WorklistToBioHubItemEventsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListWorklistToBioHubItemEventResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEMEVENTS(listResponse.WorklistTimelines);
      return;
    }

    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ReadWorklistToBioHubItem(id: string): Promise<void> {
    const service = new WorklistToBioHubItemsService();
    const query: ReadWorklistToBioHubItemQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadWorklistToBioHubItemResponse = response.value;
      this.SET_WORKLISTTOBIOHUBITEM(readResponse.WorklistToBioHubItemDto);
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ReloadShipmentDocuments(id: string): Promise<void> {
    const service = new WorklistToBioHubItemsService();
    const query: ReadWorklistToBioHubItemQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadWorklistToBioHubItemResponse = response.value;
      this.SET_WORKLISTTOBIOHUBITEM_SHIPMENTDOCUMENTS(
        readResponse.WorklistToBioHubItemDto.ShipmentDocuments
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async DownloadDocument(): Promise<void> {
    const id = this.worklistToBioHubItemDownloadFileInfo.value?.Id;
    const name =
      this.worklistToBioHubItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId =
      this.worklistToBioHubItemDownloadFileInfo.value?.WorklistId;

    const service = new WorklistToBioHubItemsService();

    if (id !== undefined && name !== undefined && worklistId !== undefined) {
      const response = await service.downloadFile({
        Id: id,
        Name: name,
        WorklistId: worklistId,
      });
      if (isRight(response)) {
        if (response.value.code == 404) {
          AppModule.SetErrorNotifications("File not found");
        } else {
          AppModule.SetErrorNotifications("Internal Server Error");
        }
        throw response;
      }
      this.SET_ERROR(undefined);
      return;
    }
  }

  @Action({ rawError: true })
  public async DownloadHistoryDocument(): Promise<void> {
    const id = this.worklistToBioHubItemDownloadFileInfo.value?.Id;
    const name =
      this.worklistToBioHubItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId =
      this.worklistToBioHubItemDownloadFileInfo.value?.WorklistId;

    const service = new WorklistToBioHubHistoryItemsService();
    if (id !== undefined && name !== undefined && worklistId !== undefined) {
      const response = await service.downloadFile({
        Id: id,
        Name: name,
        WorklistId: worklistId,
      });
      if (isRight(response)) {
        if (response.value.code == 404) {
          AppModule.SetErrorNotifications("File not found");
        } else {
          AppModule.SetErrorNotifications("Internal Server Error");
        }
        throw response;
      }
      this.SET_ERROR(undefined);
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteWorklistToBioHubItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistToBioHubItemsService();
    const worklistToBioHubItem: WorklistToBioHubItem | undefined =
      this.WorklistToBioHubItem;
    if (!worklistToBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistToBioHubItemsStore: not expecting worklistToBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(worklistToBioHubItem);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      const deleteWorklistToBioHubItemResponse: DeleteWorklistToBioHubItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEM(undefined);
      AppModule.SetSuccessNotifications("Successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get WorklistToBioHubItem(): WorklistToBioHubItem | undefined {
    return this.worklistToBioHubItem.value;
  }

  get WorklistToBioHubItemDocument(): File | undefined {
    return this.worklistToBioHubItemDocument.value;
  }

  get WorklistToBioHubItemSignature(): File | undefined {
    return this.worklistToBioHubItemSignature.value;
  }

  get WorklistToBioHubItems(): WorklistToBioHubItem[] {
    return (
      this.worklistToBioHubItems.value ?? new Array<WorklistToBioHubItem>()
    );
  }

  get WorklistToBioHubHistoryItems(): WorklistToBioHubItem[] {
    return (
      this.worklistToBioHubHistoryItems.value ??
      new Array<WorklistToBioHubItem>()
    );
  }

  get WorklistTimelines(): WorklistTimeline[] {
    return this.worklistTimelines.value ?? new Array<WorklistTimeline>();
  }

  get WorklistToBioHubItemCreate(): WorklistToBioHubItem {
    return this.worklistToBioHubItemCreate.value;
  }

  get MaterialShippingInformations(): Array<MaterialShippingInformation> {
    return (
      this.worklistToBioHubItem.value?.MaterialShippingInformations ??
      new Array<MaterialShippingInformation>()
    );
  }

  get TemporaryMaterialShippingInformation():
    | MaterialShippingInformation
    | undefined {
    return this.temporaryMaterialShippingInformation?.value;
  }

  get TemporaryShipmentMaterial(): MaterialClinicalDetail | undefined {
    return this.temporaryShipmentMaterial?.value;
  }

  get LaboratoryFocalPoints(): Array<WorklistItemUser> {
    return (
      this.worklistToBioHubItem.value?.LaboratoryFocalPoints ??
      new Array<WorklistItemUser>()
    );
  }

  get WorklistToBioHubItemBioHubFacilityFocalPoints(): Array<WorklistItemUser> {
    return (
      this.worklistToBioHubItem.value
        ?.WorklistToBioHubItemBioHubFacilityFocalPoints ??
      new Array<WorklistItemUser>()
    );
  }

  get ShipmentDocuments(): Array<ShipmentDocument> {
    return (
      this.worklistToBioHubItem.value?.ShipmentDocuments ??
      new Array<ShipmentDocument>()
    );
  }

  get Status(): WorklistToBioHubStatus {
    return (
      this.worklistToBioHubItem.value?.CurrentStatus ??
      WorklistToBioHubStatus.RequestInitiation
    );
  }

  get LaboratoryId(): string {
    return this.worklistToBioHubItem.value?.LaboratoryId ?? "";
  }

  get BioHubFacilityId(): string {
    return this.worklistToBioHubItem.value?.BioHubFacilityId ?? "";
  }

  get AttachmentType(): AttachmentType | undefined {
    return this.attachmentType?.value;
  }

  get SignatureFile(): File | undefined {
    return this.worklistToBioHubItemSignature.value;
  }

  get BookingForms(): Array<BookingFormOfSMTA> {
    return this.worklistToBioHubItem.value?.BookingForms ?? [];
  }

  get WorklistToBioHubItemMaterials(): Array<WorklistToBioHubItemMaterial> {
    return (
      this.worklistToBioHubItem.value?.WorklistToBioHubItemMaterials ??
      new Array<WorklistToBioHubItemMaterial>()
    );
  }

  get emptyWorklistToBioHubItem(): WorklistToBioHubItem {
    return Object.create({
      CurrentStatus: WorklistToBioHubStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: WorklistToBioHubStatus.RequestInitiation,
      WorklistItemTitle: "",
      LastSubmissionApproved: "",
      LaboratoryName: "",
      BioHubFacilityName: "",
      OperationDate: "",
      UserName: "",
      Comment: "",
      SMTA1DocumentId: "",
      SMTA1DocumentName: "",
      LaboratoryId: "",
      BioHubFacilityId: "",
      OriginalDocumentTemplateSMTA1DocumentId: "",
      DocumentTemplateFileType: DocumentFileType.Annex2OfSMTA1,
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
    });
  }
}

export const WorklistToBioHubItemModule = getModule(
  WorklistToBioHubItemStore,
  store
);
