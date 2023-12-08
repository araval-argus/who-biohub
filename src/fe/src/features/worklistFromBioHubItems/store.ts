import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubItemsService } from "@/services/worklistFromBioHubItems/WorklistFromBioHubItemsService";
import { WorklistFromBioHubHistoryItemsService } from "@/services/worklistFromBioHubHistoryItems/WorklistFromBioHubHistoryItemsService";
import { RoleType } from "@/models/enums/RoleType";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { worklistFromBioHubItems } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListWorklistFromBioHubItemResponse } from "@/services/worklistFromBioHubItems/models/ListWorklistFromBioHubItem";
import { ListWorklistFromBioHubHistoryItemResponse } from "@/services/worklistFromBioHubHistoryItems/models/ListWorklistFromBioHubHistoryItem";
import { ListDashboardWorklistFromBioHubItemResponse } from "@/services/worklistFromBioHubItems/models/ListDashboardWorklistFromBioHubItem";

import { CreateWorklistFromBioHubItemResponse } from "@/services/worklistFromBioHubItems/models/CreateWorklistFromBioHubItem";
import { DeleteWorklistFromBioHubItemResponse } from "@/services/worklistFromBioHubItems/models/DeleteWorklistFromBioHubItem";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadWorklistFromBioHubItemQuery } from "@/services/worklistFromBioHubItems/models/ReadWorklistFromBioHubItem";
import { ListWorklistFromBioHubHistoryItemQuery } from "@/services/worklistFromBioHubHistoryItems/models/ListWorklistFromBioHubHistoryItem";

import { ReadWorklistFromBioHubItemResponse } from "@/services/worklistFromBioHubItems/models/ReadWorklistFromBioHubItem";
import { AppModule } from "../../store/MainStore";
import { WorklistFromBioHubItemFileInfo } from "@/models/WorklistFromBioHubItemFileInfo";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
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
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import { WorklistFromBioHubItemAnnex2OfSMTA2Condition } from "@/models/WorklistFromBioHubItemAnnex2OfSMTA2Condition";
import { WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 } from "@/models/WorklistFromBioHubItemBiosafetyChecklistOfSMTA2";

import { WorklistFromBioHubItemEventsService } from "@/services/worklistFromBioHubItemEvents/WorklistFromBioHubItemEventsService";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import {
  ListWorklistFromBioHubItemEventQuery,
  ListWorklistFromBioHubItemEventResponse,
} from "@/services/worklistFromBioHubItemEvents/models/ListWorklistFromBioHubItemEvent";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";

declare global {
  interface Crypto {
    randomUUID: () => string;
  }
}

export interface WorklistFromBioHubItemState {
  WorklistFromBioHubItemCreate: WorklistFromBioHubItem | undefined;
  WorklistFromBioHubItem: WorklistFromBioHubItem | undefined;
  WorklistFromBioHubItems: Array<WorklistFromBioHubItem>;
  WorklistFromBioHubHistoryItems: Array<WorklistFromBioHubItem>;
  WorklistFromBioHubItemDocument: File | undefined;
  WorklistFromBioHubItemSignature: File | undefined;
  WorklistTimelines: Array<WorklistTimeline>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "worklistFromBioHubItems",
  store: store,
})
class WorklistFromBioHubItemStore
  extends VuexModule
  implements WorklistFromBioHubItemState
{
  // Private variables
  private worklistFromBioHubItemCreate: { value: WorklistFromBioHubItem } = {
    value: this.emptyWorklistFromBioHubItem,
  };

  private worklistFromBioHubItem: {
    value: WorklistFromBioHubItem | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubItemDocumentInfo: {
    value: WorklistFromBioHubItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubItemSignatureInfo: {
    value: WorklistFromBioHubItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubItemDownloadFileInfo: {
    value: WorklistFromBioHubItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubItems: { value: Array<WorklistFromBioHubItem> } = {
    value: worklistFromBioHubItems,
  };

  private worklistFromBioHubItemDocument: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubItemSignature: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubItemDownloadFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private worklistFromBioHubHistoryItems: {
    value: Array<WorklistFromBioHubItem>;
  } = {
    value: worklistFromBioHubItems,
  };

  private attachmentType: {
    value: AttachmentType | undefined;
  } = {
    value: AttachmentType.Document,
  };

  private worklistTimelines: { value: Array<WorklistTimeline> } = {
    value: [],
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
  public SET_WORKLISTFROMBIOHUBITEM_CREATE(
    worklistFromBioHubItem: WorklistFromBioHubItem
  ): void {
    this.worklistFromBioHubItemCreate.value = worklistFromBioHubItem;
  }

  // Details - Edit
  @Mutation
  public SET_WORKLISTFROMBIOHUBITEM(
    worklistFromBioHubItem: WorklistFromBioHubItem | undefined
  ): void {
    this.worklistFromBioHubItem.value = worklistFromBioHubItem;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMDOWNLOADFILEINFO(
    worklistFromBioHubItemDownloadFileInfo:
      | WorklistFromBioHubItemFileInfo
      | undefined
  ): void {
    this.worklistFromBioHubItemDownloadFileInfo.value =
      worklistFromBioHubItemDownloadFileInfo;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEM_DOCUMENT(file: File | undefined): void {
    this.worklistFromBioHubItemDocument.value = file;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEM_SIGNATURE(file: File | undefined): void {
    this.worklistFromBioHubItemSignature.value = file;
  }

  @Mutation
  public CLEAR_WORKLISTFROMBIOHUBITEM(): void {
    this.worklistFromBioHubItem.value = undefined;
    this.worklistFromBioHubItemDocument.value = undefined;
    this.worklistFromBioHubItemSignature.value = undefined;
    this.worklistFromBioHubItemDownloadFile.value = undefined;
  }

  @Mutation
  public CLEAR_WORKLISTFROMBIOHUBITEM_CREATE(): void {
    this.worklistFromBioHubItem.value = Object.create({
      CurrentStatus: WorklistFromBioHubStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: WorklistFromBioHubStatus.RequestInitiation,
      WorklistItemTitle: "",
      LastSubmissionApproved: "",
      LaboratoryName: "",
      BioHubFacilityName: "",
      OperationDate: "",
      UserName: "",
      Comment: "",
      SMTA2DocumentId: "",
      SMTA2DocumentName: "",
      LaboratoryId: "",
      BioHubFacilityId: "",
      OriginalDocumentTemplateSMTA2DocumentId: "",
      DocumentTemplateFileType: DocumentFileType.SMTA2,
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
      IsPast: false,
      AssignedOperationDate: null,
    });
    this.worklistFromBioHubItemDocument.value = undefined;
    this.worklistFromBioHubItemSignature.value = undefined;
    this.worklistFromBioHubItemDownloadFile.value = undefined;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEM_CREATE_ISPAST(isPast: boolean): void {
    if (this.worklistFromBioHubItemCreate?.value != undefined) {
      this.worklistFromBioHubItemCreate!.value.IsPast = isPast;
    }
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEM_CREATE_ASSIGNED_OPERATION_DATE(
    assignedOperationDate: Date | null
  ): void {
    if (this.worklistFromBioHubItemCreate?.value != undefined) {
      this.worklistFromBioHubItemCreate!.value.AssignedOperationDate =
        assignedOperationDate;
    }
  }

  // List
  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMS(
    worklistFromBioHubItems: Array<WorklistFromBioHubItem>
  ): void {
    this.worklistFromBioHubItems.value = worklistFromBioHubItems;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBHISTORYITEMS(
    worklistFromBioHubHistoryItems: Array<WorklistFromBioHubItem>
  ): void {
    this.worklistFromBioHubHistoryItems.value = worklistFromBioHubHistoryItems;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMEVENTS(
    worklistTimelines: Array<WorklistTimeline>
  ): void {
    this.worklistTimelines.value = worklistTimelines;
  }

  @Mutation
  public RESET_ATTACHMENT_TYPE(): void {
    this.attachmentType.value = AttachmentType.Document;
  }

  @Mutation
  public SET_ATTACHMENT_TYPE(type: AttachmentType): void {
    this.attachmentType.value = type;
  }

  @Mutation
  public ADD_LABORATORY_FOCAL_POINT(item: WorklistItemUser): void {
    item.Other = "";
    this.worklistFromBioHubItem.value?.LaboratoryFocalPoints.push(item);
  }

  @Mutation
  public UPDATE_OTHER_FIELD_LABORATORY_FOCAL_POINT(
    item: WorklistItemUser
  ): void {
    const array = this.worklistFromBioHubItem.value?.LaboratoryFocalPoints;
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
    const array = this.worklistFromBioHubItem.value?.LaboratoryFocalPoints;
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
  public ADD_MATERIAL(item: WorklistFromBioHubItemMaterial): void {
    item.Amount = 0;
    item.Quantity = 0;

    this.worklistFromBioHubItem.value?.WorklistFromBioHubItemMaterials.push(
      item
    );
  }

  @Mutation
  public UPDATE_MATERIAL(item: WorklistFromBioHubItemMaterial): void {
    const array =
      this.worklistFromBioHubItem.value?.WorklistFromBioHubItemMaterials;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].Amount = item.Amount;
        array[index].Quantity = item.Quantity;
      }
    }
  }

  @Mutation
  public REMOVE_MATERIAL(id: string): void {
    const array =
      this.worklistFromBioHubItem.value?.WorklistFromBioHubItemMaterials;
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
  public REMOVE_ALL_MATERIALS(): void {
    this.worklistFromBioHubItem.value?.WorklistFromBioHubItemMaterials.splice(
      0
    );
  }

  @Mutation
  public UPDATE_ANNEX2OFSMTA2CONDITION(
    item: WorklistFromBioHubItemAnnex2OfSMTA2Condition
  ): void {
    const array =
      this.worklistFromBioHubItem.value
        ?.WorklistFromBioHubItemAnnex2OfSMTA2Conditions;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].Flag = item.Flag;
        array[index].Comment = item.Comment;
      }
    }
  }

  @Mutation
  public UPDATE_BIOSAFETYCHECKLIST(
    item: WorklistFromBioHubItemBiosafetyChecklistOfSMTA2
  ): void {
    const array =
      this.worklistFromBioHubItem.value
        ?.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(item.Id);
      if (index !== -1) {
        array[index].Flag = item.Flag;
        if (array[index].IsParentCondition == true) {
          array.forEach((elem) => {
            if (elem.ParentConditionId == array[index].BiosafetyChecklistId) {
              elem.IsVisible = array[index].Flag == elem.ShowOnParentValue;
            }
          });
        }
      }
    }
  }

  @Mutation
  public ADD_BOOKINGFORM_PICKUP_USER(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;
    const item = info["Item"] as WorklistItemUser;

    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

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
    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

    const bookingFormId = info.get("BookingFormId") ?? "";
    const id = info.get("Id") ?? "";

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
    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

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
  public ADD_BOOKINGFORM_COURIER_USER(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;

    const item = info["Item"] as WorklistItemUser;

    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

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
    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

    const bookingFormId = info.get("BookingFormId") ?? "";
    const id = info.get("Id") ?? "";

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
    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

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
    const bookingFormArray = this.worklistFromBioHubItem.value?.BookingForms;

    if (bookingFormArray !== undefined && bookingFormId !== undefined) {
      const bookingForm = bookingFormArray.find((x) => x.Id === bookingFormId);
      if (bookingForm !== undefined) {
        bookingForm.BookingFormCourierUsers = new Array<WorklistItemUser>();
      }
    }
  }

  @Mutation
  public UPDATE_BOOKING_FORM_TOTALS(info: any): void {
    const bookingFormId = info["BookingFormId"] as string;
    const totalNumberOfVials = info["TotalNumberOfVials"] as number;
    const totalAmount = info["TotalAmount"] as number;

    const array = this.worklistFromBioHubItem.value?.BookingForms;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(bookingFormId);
      if (index !== -1) {
        array[index].TotalNumberOfVials = totalNumberOfVials;
        array[index].TotalAmount = totalAmount;
      }
    }
  }

  @Mutation
  public UPDATE_BOOKING_FORM(item: BookingFormOfSMTA): void {
    const array = this.worklistFromBioHubItem.value?.BookingForms;
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
    const array = this.worklistFromBioHubItem.value?.BookingForms;
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
    const array = this.worklistFromBioHubItem.value?.BookingForms;
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
    const array = this.worklistFromBioHubItem.value?.BookingForms;
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
    const array = this.worklistFromBioHubItem.value?.BookingForms;
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
  public SET_WORKLISTFROMBIOHUBITEM_BHFSHIPMENTDOCUMENTS(
    shipmentDocuments: Array<ShipmentDocument>
  ): void {
    this.worklistFromBioHubItem.value!.BHFShipmentDocuments = shipmentDocuments;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEM_QESHIPMENTDOCUMENTS(
    shipmentDocuments: Array<ShipmentDocument>
  ): void {
    this.worklistFromBioHubItem.value!.QEShipmentDocuments = shipmentDocuments;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateWorklistFromBioHubItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistFromBioHubItemsService();
    const worklistFromBioHubItem = this.worklistFromBioHubItemCreate.value;
    if (worklistFromBioHubItem === undefined) {
      this.SET_ERROR({
        message:
          "WorklistFromBioHubItemsStore: not expecting worklistFromBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(worklistFromBioHubItem);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateWorklistFromBioHubItemResponse =
        response.value;
      worklistFromBioHubItem.Id = createResponse.Id;
      this.SET_WORKLISTFROMBIOHUBITEM(worklistFromBioHubItem);
      this.SET_WORKLISTFROMBIOHUBITEM_CREATE(this.emptyWorklistFromBioHubItem);
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
  public async UpdateWorklistFromBioHubItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistFromBioHubItemsService();
    const worklistFromBioHubItem: WorklistFromBioHubItem | undefined =
      this.WorklistFromBioHubItem;
    const file =
      this.AttachmentType == AttachmentType.Document
        ? this.worklistFromBioHubItemDocument.value
        : this.worklistFromBioHubItemSignature.value;
    if (!worklistFromBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistFromBioHubItemsStore: not expecting worklistFromBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(worklistFromBioHubItem, file);
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
      this.SET_WORKLISTFROMBIOHUBITEM(worklistFromBioHubItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async UpdateWorklistFromBioHubItemBHFShipmentDocuments(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistFromBioHubItemsService();
    const worklistFromBioHubItem: WorklistFromBioHubItem | undefined =
      this.WorklistFromBioHubItem;
    const file =
      this.AttachmentType == AttachmentType.Document
        ? this.worklistFromBioHubItemDocument.value
        : this.worklistFromBioHubItemSignature.value;
    if (!worklistFromBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistFromBioHubItemsStore: not expecting worklistFromBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updateBHFShipmentDocuments(
      worklistFromBioHubItem,
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
      this.SET_WORKLISTFROMBIOHUBITEM(worklistFromBioHubItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async UpdateWorklistFromBioHubItemQEShipmentDocuments(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistFromBioHubItemsService();
    const worklistFromBioHubItem: WorklistFromBioHubItem | undefined =
      this.WorklistFromBioHubItem;
    const file =
      this.AttachmentType == AttachmentType.Document
        ? this.worklistFromBioHubItemDocument.value
        : this.worklistFromBioHubItemSignature.value;
    if (!worklistFromBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistFromBioHubItemsStore: not expecting worklistFromBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updateQEShipmentDocuments(
      worklistFromBioHubItem,
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
      this.SET_WORKLISTFROMBIOHUBITEM(worklistFromBioHubItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListWorklistFromBioHubItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new WorklistFromBioHubItemsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListWorklistFromBioHubItemResponse = response.value;
      this.SET_WORKLISTFROMBIOHUBITEMS(listResponse.WorklistFromBioHubItems);
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
  public async ListDashboardWorklistFromBioHubItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new WorklistFromBioHubItemsService();
    const response = await service.listForDashboard({});
    if (isLeft(response)) {
      const listResponse: ListDashboardWorklistFromBioHubItemResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBITEMS(listResponse.WorklistFromBioHubItems);
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
  public async ListWorklistFromBioHubHistoryItems(
    worklistFromBioHubItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListWorklistFromBioHubHistoryItemQuery = {
      WorklistFromBioHubItemId: worklistFromBioHubItemId,
    };
    const service = new WorklistFromBioHubHistoryItemsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListWorklistFromBioHubHistoryItemResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBHISTORYITEMS(
        listResponse.WorklistFromBioHubHistoryItems
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
  public async ListWorklistFromBioHubItemEvents(
    worklistFromBioHubItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListWorklistFromBioHubItemEventQuery = {
      WorklistFromBioHubItemId: worklistFromBioHubItemId,
    };
    const service = new WorklistFromBioHubItemEventsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListWorklistFromBioHubItemEventResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBITEMEVENTS(listResponse.WorklistTimelines);
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
  public async ReadWorklistFromBioHubItem(id: string): Promise<void> {
    const service = new WorklistFromBioHubItemsService();
    const query: ReadWorklistFromBioHubItemQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadWorklistFromBioHubItemResponse = response.value;
      this.SET_WORKLISTFROMBIOHUBITEM(readResponse.WorklistFromBioHubItemDto);
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
    const service = new WorklistFromBioHubItemsService();
    const query: ReadWorklistFromBioHubItemQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadWorklistFromBioHubItemResponse = response.value;
      this.SET_WORKLISTFROMBIOHUBITEM_BHFSHIPMENTDOCUMENTS(
        readResponse.WorklistFromBioHubItemDto.BHFShipmentDocuments
      );
      this.SET_WORKLISTFROMBIOHUBITEM_QESHIPMENTDOCUMENTS(
        readResponse.WorklistFromBioHubItemDto.QEShipmentDocuments
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
    const id = this.worklistFromBioHubItemDownloadFileInfo.value?.Id;
    const name =
      this.worklistFromBioHubItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId =
      this.worklistFromBioHubItemDownloadFileInfo.value?.WorklistId;

    const service = new WorklistFromBioHubItemsService();

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
    const id = this.worklistFromBioHubItemDownloadFileInfo.value?.Id;
    const name =
      this.worklistFromBioHubItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId =
      this.worklistFromBioHubItemDownloadFileInfo.value?.WorklistId;

    const service = new WorklistFromBioHubHistoryItemsService();
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
  public async DeleteWorklistFromBioHubItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new WorklistFromBioHubItemsService();
    const worklistFromBioHubItem: WorklistFromBioHubItem | undefined =
      this.WorklistFromBioHubItem;
    if (!worklistFromBioHubItem) {
      this.SET_ERROR({
        message:
          "WorklistFromBioHubItemsStore: not expecting worklistFromBioHubItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(worklistFromBioHubItem);
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
      const deleteWorklistFromBioHubItemResponse: DeleteWorklistFromBioHubItemResponse =
        response.value;

      this.SET_WORKLISTFROMBIOHUBITEM(undefined);
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

  get WorklistFromBioHubItem(): WorklistFromBioHubItem | undefined {
    return this.worklistFromBioHubItem.value;
  }

  get WorklistFromBioHubItemDocument(): File | undefined {
    return this.worklistFromBioHubItemDocument.value;
  }

  get WorklistFromBioHubItemSignature(): File | undefined {
    return this.worklistFromBioHubItemSignature.value;
  }

  get WorklistFromBioHubItems(): WorklistFromBioHubItem[] {
    return (
      this.worklistFromBioHubItems.value ?? new Array<WorklistFromBioHubItem>()
    );
  }

  get WorklistFromBioHubHistoryItems(): WorklistFromBioHubItem[] {
    return (
      this.worklistFromBioHubHistoryItems.value ??
      new Array<WorklistFromBioHubItem>()
    );
  }

  get WorklistTimelines(): WorklistTimeline[] {
    return this.worklistTimelines.value ?? new Array<WorklistTimeline>();
  }

  get WorklistFromBioHubItemCreate(): WorklistFromBioHubItem {
    return this.worklistFromBioHubItemCreate.value;
  }

  get WorklistFromBioHubItemMaterials(): Array<WorklistFromBioHubItemMaterial> {
    return (
      this.worklistFromBioHubItem.value?.WorklistFromBioHubItemMaterials ??
      new Array<WorklistFromBioHubItemMaterial>()
    );
  }

  get LaboratoryFocalPoints(): Array<WorklistItemUser> {
    return (
      this.worklistFromBioHubItem.value?.LaboratoryFocalPoints ??
      new Array<WorklistItemUser>()
    );
  }

  get WorklistFromBioHubItemAnnex2OfSMTA2Conditions(): Array<WorklistFromBioHubItemAnnex2OfSMTA2Condition> {
    return (
      this.worklistFromBioHubItem.value
        ?.WorklistFromBioHubItemAnnex2OfSMTA2Conditions ??
      new Array<WorklistFromBioHubItemAnnex2OfSMTA2Condition>()
    );
  }

  get WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s(): Array<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> {
    return (
      this.worklistFromBioHubItem.value
        ?.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s ??
      new Array<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2>()
    );
  }

  get BHFShipmentDocuments(): Array<ShipmentDocument> {
    return (
      this.worklistFromBioHubItem.value?.BHFShipmentDocuments ??
      new Array<ShipmentDocument>()
    );
  }

  get QEShipmentDocuments(): Array<ShipmentDocument> {
    return (
      this.worklistFromBioHubItem.value?.QEShipmentDocuments ??
      new Array<ShipmentDocument>()
    );
  }

  get Status(): WorklistFromBioHubStatus {
    return (
      this.worklistFromBioHubItem.value?.CurrentStatus ??
      WorklistFromBioHubStatus.RequestInitiation
    );
  }

  get BookingFormFillingOption(): WorklistFillingOption {
    return (
      this.worklistFromBioHubItem.value?.BookingFormFillingOption ??
      WorklistFillingOption.ElectronicallyFill
    );
  }

  get LaboratoryId(): string {
    return this.worklistFromBioHubItem.value?.LaboratoryId ?? "";
  }

  get BioHubFacilityId(): string {
    return this.worklistFromBioHubItem.value?.BioHubFacilityId ?? "";
  }

  get AttachmentType(): AttachmentType | undefined {
    return this.attachmentType?.value;
  }

  get SignatureFile(): File | undefined {
    return this.worklistFromBioHubItemSignature.value;
  }

  get BookingForms(): Array<BookingFormOfSMTA> {
    return this.worklistFromBioHubItem.value?.BookingForms ?? [];
  }

  get emptyWorklistFromBioHubItem(): WorklistFromBioHubItem {
    return Object.create({
      CurrentStatus: WorklistFromBioHubStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: WorklistFromBioHubStatus.RequestInitiation,
      WorklistItemTitle: "",
      LastSubmissionApproved: "",
      LaboratoryName: "",
      BioHubFacilityName: "",
      OperationDate: "",
      UserName: "",
      Comment: "",
      SMTA2DocumentId: "",
      SMTA2DocumentName: "",
      LaboratoryId: "",
      BioHubFacilityId: "",
      OriginalDocumentTemplateSMTA2DocumentId: "",
      DocumentTemplateFileType: DocumentFileType.SMTA2,
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
    });
  }
}

export const WorklistFromBioHubItemModule = getModule(
  WorklistFromBioHubItemStore,
  store
);
