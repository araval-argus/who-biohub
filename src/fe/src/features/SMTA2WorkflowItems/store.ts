import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";
import { SMTA2WorkflowItemsService } from "@/services/SMTA2WorkflowItems/SMTA2WorkflowItemsService";
import { SMTA2WorkflowHistoryItemsService } from "@/services/SMTA2WorkflowHistoryItems/SMTA2WorkflowHistoryItemsService";
import { RoleType } from "@/models/enums/RoleType";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { SMTA2WorkflowItems } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListSMTA2WorkflowItemResponse } from "@/services/SMTA2WorkflowItems/models/ListSMTA2WorkflowItem";
import { ListSMTA2WorkflowHistoryItemResponse } from "@/services/SMTA2WorkflowHistoryItems/models/ListSMTA2WorkflowHistoryItem";
import { ListDashboardSMTA2WorkflowItemResponse } from "@/services/SMTA2WorkflowItems/models/ListDashboardSMTA2WorkflowItem";

import { CreateSMTA2WorkflowItemResponse } from "@/services/SMTA2WorkflowItems/models/CreateSMTA2WorkflowItem";
import { DeleteSMTA2WorkflowItemResponse } from "@/services/SMTA2WorkflowItems/models/DeleteSMTA2WorkflowItem";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadSMTA2WorkflowItemQuery } from "@/services/SMTA2WorkflowItems/models/ReadSMTA2WorkflowItem";
import { ListSMTA2WorkflowHistoryItemQuery } from "@/services/SMTA2WorkflowHistoryItems/models/ListSMTA2WorkflowHistoryItem";

import { ReadSMTA2WorkflowItemResponse } from "@/services/SMTA2WorkflowItems/models/ReadSMTA2WorkflowItem";
import { AppModule } from "../../store/MainStore";
import { SMTA2WorkflowItemFileInfo } from "@/models/SMTA2WorkflowItemFileInfo";
import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";
import { DocumentFileType } from "@/models/enums/DocumentFileType";

import { AttachmentType } from "@/models/enums/AttachmentType";

import { SMTA2WorkflowItemEventsService } from "@/services/SMTA2WorkflowItemEvents/SMTA2WorkflowItemEventsService";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import {
  ListSMTA2WorkflowItemEventQuery,
  ListSMTA2WorkflowItemEventResponse,
} from "@/services/SMTA2WorkflowItemEvents/models/ListSMTA2WorkflowItemEvent";

declare global {
  interface Crypto {
    randomUUID: () => string;
  }
}

export interface SMTA2WorkflowItemState {
  SMTA2WorkflowItemCreate: SMTA2WorkflowItem | undefined;
  SMTA2WorkflowItem: SMTA2WorkflowItem | undefined;
  SMTA2WorkflowItems: Array<SMTA2WorkflowItem>;
  SMTA2WorkflowHistoryItems: Array<SMTA2WorkflowItem>;
  SMTA2WorkflowItemDocument: File | undefined;
  WorklistTimelines: Array<WorklistTimeline>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "SMTA2WorkflowItems",
  store: store,
})
class SMTA2WorkflowItemStore
  extends VuexModule
  implements SMTA2WorkflowItemState
{
  // Private variables
  private smta2WorkflowItemCreate: { value: SMTA2WorkflowItem } = {
    value: this.emptySMTA2WorkflowItem,
  };

  private smta2WorkflowItem: { value: SMTA2WorkflowItem | undefined } = {
    value: undefined,
  };

  private smta2WorkflowItemDocumentInfo: {
    value: SMTA2WorkflowItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private smta2WorkflowItemDownloadFileInfo: {
    value: SMTA2WorkflowItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private smta2WorkflowItems: { value: Array<SMTA2WorkflowItem> } = {
    value: SMTA2WorkflowItems,
  };

  private smta2WorkflowItemDocument: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private smta2WorkflowItemDownloadFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private smta2WorkflowHistoryItems: { value: Array<SMTA2WorkflowItem> } = {
    value: SMTA2WorkflowItems,
  };

  private worklistTimelines: { value: Array<WorklistTimeline> } = {
    value: [],
  };

  private attachmentType: {
    value: AttachmentType | undefined;
  } = {
    value: AttachmentType.Document,
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
  public SET_SMTA2WORKFLOW_CREATE(SMTA2WorkflowItem: SMTA2WorkflowItem): void {
    this.smta2WorkflowItemCreate.value = SMTA2WorkflowItem;
  }

  // Details - Edit
  @Mutation
  public SET_SMTA2WORKFLOW(
    SMTA2WorkflowItem: SMTA2WorkflowItem | undefined
  ): void {
    this.smta2WorkflowItem.value = SMTA2WorkflowItem;
  }

  @Mutation
  public SET_SMTA2WORKFLOWDOWNLOADFILEINFO(
    SMTA2WorkflowItemDownloadFileInfo: SMTA2WorkflowItemFileInfo | undefined
  ): void {
    this.smta2WorkflowItemDownloadFileInfo.value =
      SMTA2WorkflowItemDownloadFileInfo;
  }

  @Mutation
  public SET_SMTA2WORKFLOW_DOCUMENT(file: File | undefined): void {
    this.smta2WorkflowItemDocument.value = file;
  }

  @Mutation
  public CLEAR_SMTA2WORKFLOW(): void {
    this.smta2WorkflowItem.value = undefined;
    this.smta2WorkflowItemDocument.value = undefined;
    this.smta2WorkflowItemDownloadFile.value = undefined;
  }

  @Mutation
  public CLEAR_SMTA2WORKFLOW_CREATE(): void {
    this.smta2WorkflowItem.value = Object.create({
      CurrentStatus: SMTA2WorkflowStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: SMTA2WorkflowStatus.RequestInitiation,
      WorklistItemTitle: "",
      LastSubmissionApproved: "",
      LaboratoryName: "",
      OperationDate: "",
      UserName: "",
      Comment: "",
      SMTA2DocumentId: "",
      SMTA2DocumentName: "",
      LaboratoryId: "",
      OriginalDocumentTemplateSMTA2DocumentId: "",
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
      IsPast: false,
      AssignedOperationDate: null,
    });
    this.smta2WorkflowItemDocument.value = undefined;
    this.smta2WorkflowItemDownloadFile.value = undefined;
  }

  @Mutation
  public SET_SMTA2_CREATE_ISPAST(isPast: boolean): void {
    if (this.smta2WorkflowItemCreate?.value != undefined) {
      this.smta2WorkflowItemCreate!.value.IsPast = isPast;
    }
  }

  @Mutation
  public SET_SMTA2_CREATE_ASSIGNED_OPERATION_DATE(
    assignedOperationDate: Date | null
  ): void {
    if (this.smta2WorkflowItemCreate?.value != undefined) {
      this.smta2WorkflowItemCreate!.value.AssignedOperationDate =
        assignedOperationDate;
    }
  }

  // List
  @Mutation
  public SET_SMTA2WORKFLOWS(
    SMTA2WorkflowItems: Array<SMTA2WorkflowItem>
  ): void {
    this.smta2WorkflowItems.value = SMTA2WorkflowItems;
  }

  @Mutation
  public SET_SMTA2WORKFLOWHISTORYITEMS(
    SMTA2WorkflowHistoryItems: Array<SMTA2WorkflowItem>
  ): void {
    this.smta2WorkflowHistoryItems.value = SMTA2WorkflowHistoryItems;
  }

  @Mutation
  public RESET_ATTACHMENT_TYPE(): void {
    this.attachmentType.value = AttachmentType.Document;
  }

  @Mutation
  public SET_ATTACHMENT_TYPE(attachmentType: AttachmentType | undefined): void {
    this.attachmentType.value = attachmentType;
  }

  @Mutation
  public SET_SMTA2WORKFLOWITEMDOWNLOADFILEINFO(
    worklistToBioHubItemDownloadFileInfo: SMTA2WorkflowItemFileInfo | undefined
  ): void {
    this.smta2WorkflowItemDownloadFileInfo.value =
      worklistToBioHubItemDownloadFileInfo;
  }

  @Mutation
  public SET_SMTA2WORKFLOWITEM_DOCUMENT(file: File | undefined): void {
    this.smta2WorkflowItemDocument.value = file;
  }

  @Mutation
  public SET_SMTA2WORKFLOWITEMEVENTS(
    worklistTimelines: Array<WorklistTimeline>
  ): void {
    this.worklistTimelines.value = worklistTimelines;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateSMTA2WorkflowItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new SMTA2WorkflowItemsService();
    const SMTA2WorkflowItem = this.smta2WorkflowItemCreate.value;
    if (SMTA2WorkflowItem === undefined) {
      this.SET_ERROR({
        message:
          "SMTA2WorkflowItemsStore: not expecting SMTA2WorkflowItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(SMTA2WorkflowItem);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateSMTA2WorkflowItemResponse = response.value;
      SMTA2WorkflowItem.Id = createResponse.Id;
      this.SET_SMTA2WORKFLOW(SMTA2WorkflowItem);
      this.SET_SMTA2WORKFLOW_CREATE(this.emptySMTA2WorkflowItem);
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
  public async UpdateSMTA2WorkflowItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new SMTA2WorkflowItemsService();
    const smta2WorkflowItem: SMTA2WorkflowItem | undefined =
      this.SMTA2WorkflowItem;
    const file = this.smta2WorkflowItemDocument.value;

    if (!smta2WorkflowItem) {
      this.SET_ERROR({
        message:
          "SMTA2WorkflowItemsStore: not expecting SMTA2WorkflowItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(smta2WorkflowItem, file);
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
      this.SET_SMTA2WORKFLOW(smta2WorkflowItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListSMTA2WorkflowItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new SMTA2WorkflowItemsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListSMTA2WorkflowItemResponse = response.value;
      this.SET_SMTA2WORKFLOWS(listResponse.SMTA2WorkflowItems);
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
  public async ListDashboardSMTA2WorkflowItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new SMTA2WorkflowItemsService();
    const response = await service.listForDashboard({});
    if (isLeft(response)) {
      const listResponse: ListDashboardSMTA2WorkflowItemResponse =
        response.value;
      this.SET_SMTA2WORKFLOWS(listResponse.SMTA2WorkflowItems);
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
  public async ListSMTA2WorkflowHistoryItems(
    SMTA2WorkflowItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListSMTA2WorkflowHistoryItemQuery = {
      SMTA2WorkflowItemId: SMTA2WorkflowItemId,
    };
    const service = new SMTA2WorkflowHistoryItemsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListSMTA2WorkflowHistoryItemResponse = response.value;
      this.SET_SMTA2WORKFLOWHISTORYITEMS(
        listResponse.SMTA2WorkflowHistoryItems
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
  public async ReadSMTA2WorkflowItem(id: string): Promise<void> {
    const service = new SMTA2WorkflowItemsService();
    const query: ReadSMTA2WorkflowItemQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadSMTA2WorkflowItemResponse = response.value;
      this.SET_SMTA2WORKFLOW(readResponse.SMTA2WorkflowItemDto);
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
    const id = this.smta2WorkflowItemDownloadFileInfo.value?.Id;
    const name =
      this.smta2WorkflowItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId = this.smta2WorkflowItemDownloadFileInfo.value?.WorklistId;

    const service = new SMTA2WorkflowItemsService();

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
    const id = this.smta2WorkflowItemDownloadFileInfo.value?.Id;
    const name =
      this.smta2WorkflowItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId = this.smta2WorkflowItemDownloadFileInfo.value?.WorklistId;

    const service = new SMTA2WorkflowHistoryItemsService();
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
  public async DeleteSMTA2WorkflowItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new SMTA2WorkflowItemsService();
    const smta2WorkflowItem: SMTA2WorkflowItem | undefined =
      this.SMTA2WorkflowItem;
    if (!smta2WorkflowItem) {
      this.SET_ERROR({
        message:
          "SMTA2WorkflowItemsStore: not expecting SMTA2WorkflowItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(smta2WorkflowItem);
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
      const deleteSMTA2WorkflowItemResponse: DeleteSMTA2WorkflowItemResponse =
        response.value;
      this.SET_SMTA2WORKFLOW(undefined);
      AppModule.SetSuccessNotifications("Successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListSMTA2WorkflowItemEvents(
    SMTA2WorkflowItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListSMTA2WorkflowItemEventQuery = {
      SMTA2WorkflowItemId: SMTA2WorkflowItemId,
    };
    const service = new SMTA2WorkflowItemEventsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListSMTA2WorkflowItemEventResponse = response.value;
      this.SET_SMTA2WORKFLOWITEMEVENTS(listResponse.WorklistTimelines);
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

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get SMTA2WorkflowItem(): SMTA2WorkflowItem | undefined {
    return this.smta2WorkflowItem.value;
  }

  get SMTA2WorkflowItemDocument(): File | undefined {
    return this.smta2WorkflowItemDocument.value;
  }

  get SMTA2WorkflowItems(): SMTA2WorkflowItem[] {
    return this.smta2WorkflowItems.value ?? new Array<SMTA2WorkflowItem>();
  }

  get SMTA2WorkflowHistoryItems(): SMTA2WorkflowItem[] {
    return (
      this.smta2WorkflowHistoryItems.value ?? new Array<SMTA2WorkflowItem>()
    );
  }

  get SMTA2WorkflowItemCreate(): SMTA2WorkflowItem {
    return this.smta2WorkflowItemCreate.value;
  }

  get Status(): SMTA2WorkflowStatus {
    return (
      this.smta2WorkflowItem.value?.CurrentStatus ??
      SMTA2WorkflowStatus.RequestInitiation
    );
  }

  get LaboratoryId(): string {
    return this.smta2WorkflowItem.value?.LaboratoryId ?? "";
  }

  get AttachmentType(): AttachmentType | undefined {
    return this.attachmentType?.value;
  }

  get WorklistTimelines(): WorklistTimeline[] {
    return this.worklistTimelines.value ?? new Array<WorklistTimeline>();
  }

  get emptySMTA2WorkflowItem(): SMTA2WorkflowItem {
    return Object.create({
      CurrentStatus: SMTA2WorkflowStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: SMTA2WorkflowStatus.RequestInitiation,
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
      OriginalDocumentTemplateSMTA2DocumentId: "",
      DocumentTemplateFileType: DocumentFileType.SMTA2,
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
    });
  }
}

export const SMTA2WorkflowItemModule = getModule(SMTA2WorkflowItemStore, store);
