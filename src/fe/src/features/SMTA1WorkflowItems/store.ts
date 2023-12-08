import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA1WorkflowItemsService } from "@/services/SMTA1WorkflowItems/SMTA1WorkflowItemsService";
import { SMTA1WorkflowHistoryItemsService } from "@/services/SMTA1WorkflowHistoryItems/SMTA1WorkflowHistoryItemsService";
import { RoleType } from "@/models/enums/RoleType";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { SMTA1WorkflowItems } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListSMTA1WorkflowItemResponse } from "@/services/SMTA1WorkflowItems/models/ListSMTA1WorkflowItem";
import { ListSMTA1WorkflowHistoryItemResponse } from "@/services/SMTA1WorkflowHistoryItems/models/ListSMTA1WorkflowHistoryItem";
import { ListDashboardSMTA1WorkflowItemResponse } from "@/services/SMTA1WorkflowItems/models/ListDashboardSMTA1WorkflowItem";

import { CreateSMTA1WorkflowItemResponse } from "@/services/SMTA1WorkflowItems/models/CreateSMTA1WorkflowItem";
import { DeleteSMTA1WorkflowItemResponse } from "@/services/SMTA1WorkflowItems/models/DeleteSMTA1WorkflowItem";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadSMTA1WorkflowItemQuery } from "@/services/SMTA1WorkflowItems/models/ReadSMTA1WorkflowItem";
import { ListSMTA1WorkflowHistoryItemQuery } from "@/services/SMTA1WorkflowHistoryItems/models/ListSMTA1WorkflowHistoryItem";

import { ReadSMTA1WorkflowItemResponse } from "@/services/SMTA1WorkflowItems/models/ReadSMTA1WorkflowItem";
import { AppModule } from "../../store/MainStore";
import { SMTA1WorkflowItemFileInfo } from "@/models/SMTA1WorkflowItemFileInfo";
import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import { DocumentFileType } from "@/models/enums/DocumentFileType";

import { AttachmentType } from "@/models/enums/AttachmentType";

import { SMTA1WorkflowItemEventsService } from "@/services/SMTA1WorkflowItemEvents/SMTA1WorkflowItemEventsService";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import {
  ListSMTA1WorkflowItemEventQuery,
  ListSMTA1WorkflowItemEventResponse,
} from "@/services/SMTA1WorkflowItemEvents/models/ListSMTA1WorkflowItemEvent";

declare global {
  interface Crypto {
    randomUUID: () => string;
  }
}

export interface SMTA1WorkflowItemState {
  SMTA1WorkflowItemCreate: SMTA1WorkflowItem | undefined;
  SMTA1WorkflowItem: SMTA1WorkflowItem | undefined;
  SMTA1WorkflowItems: Array<SMTA1WorkflowItem>;
  SMTA1WorkflowHistoryItems: Array<SMTA1WorkflowItem>;
  SMTA1WorkflowItemDocument: File | undefined;
  WorklistTimelines: Array<WorklistTimeline>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "SMTA1WorkflowItems",
  store: store,
})
class SMTA1WorkflowItemStore
  extends VuexModule
  implements SMTA1WorkflowItemState
{
  // Private variables
  private smta1WorkflowItemCreate: { value: SMTA1WorkflowItem } = {
    value: this.emptySMTA1WorkflowItem,
  };

  private smta1WorkflowItem: { value: SMTA1WorkflowItem | undefined } = {
    value: undefined,
  };

  private smta1WorkflowItemDocumentInfo: {
    value: SMTA1WorkflowItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private smta1WorkflowItemDownloadFileInfo: {
    value: SMTA1WorkflowItemFileInfo | undefined;
  } = {
    value: undefined,
  };

  private smta1WorkflowItems: { value: Array<SMTA1WorkflowItem> } = {
    value: SMTA1WorkflowItems,
  };

  private smta1WorkflowItemDocument: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private smta1WorkflowItemDownloadFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private smta1WorkflowHistoryItems: { value: Array<SMTA1WorkflowItem> } = {
    value: SMTA1WorkflowItems,
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
  public SET_SMTA1WORKFLOW_CREATE(SMTA1WorkflowItem: SMTA1WorkflowItem): void {
    this.smta1WorkflowItemCreate.value = SMTA1WorkflowItem;
  }

  // Details - Edit
  @Mutation
  public SET_SMTA1WORKFLOW(
    SMTA1WorkflowItem: SMTA1WorkflowItem | undefined
  ): void {
    this.smta1WorkflowItem.value = SMTA1WorkflowItem;
  }

  @Mutation
  public SET_SMTA1WORKFLOWDOWNLOADFILEINFO(
    SMTA1WorkflowItemDownloadFileInfo: SMTA1WorkflowItemFileInfo | undefined
  ): void {
    this.smta1WorkflowItemDownloadFileInfo.value =
      SMTA1WorkflowItemDownloadFileInfo;
  }

  @Mutation
  public SET_SMTA1WORKFLOW_DOCUMENT(file: File | undefined): void {
    this.smta1WorkflowItemDocument.value = file;
  }

  @Mutation
  public CLEAR_SMTA1WORKFLOW(): void {
    this.smta1WorkflowItem.value = undefined;
    this.smta1WorkflowItemDocument.value = undefined;
    this.smta1WorkflowItemDownloadFile.value = undefined;
  }

  @Mutation
  public CLEAR_SMTA1WORKFLOW_CREATE(): void {
    this.smta1WorkflowItem.value = Object.create({
      CurrentStatus: SMTA1WorkflowStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: SMTA1WorkflowStatus.RequestInitiation,
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
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
      IsPast: false,
      AssignedOperationDate: null,
    });
    this.smta1WorkflowItemDocument.value = undefined;
    this.smta1WorkflowItemDownloadFile.value = undefined;
  }

  @Mutation
  public SET_SMTA1_CREATE_ISPAST(isPast: boolean): void {
    if (this.smta1WorkflowItemCreate?.value != undefined) {
      this.smta1WorkflowItemCreate!.value.IsPast = isPast;
    }
  }

  @Mutation
  public SET_SMTA1_CREATE_ASSIGNED_OPERATION_DATE(
    assignedOperationDate: Date | null
  ): void {
    if (this.smta1WorkflowItemCreate?.value != undefined) {
      this.smta1WorkflowItemCreate!.value.AssignedOperationDate =
        assignedOperationDate;
    }
  }

  // List
  @Mutation
  public SET_SMTA1WORKFLOWS(
    SMTA1WorkflowItems: Array<SMTA1WorkflowItem>
  ): void {
    this.smta1WorkflowItems.value = SMTA1WorkflowItems;
  }

  @Mutation
  public SET_SMTA1WORKFLOWHISTORYITEMS(
    SMTA1WorkflowHistoryItems: Array<SMTA1WorkflowItem>
  ): void {
    this.smta1WorkflowHistoryItems.value = SMTA1WorkflowHistoryItems;
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
  public SET_SMTA1WORKFLOWITEMDOWNLOADFILEINFO(
    SMTA1WorkflowItemDownloadFileInfo: SMTA1WorkflowItemFileInfo | undefined
  ): void {
    this.smta1WorkflowItemDownloadFileInfo.value =
      SMTA1WorkflowItemDownloadFileInfo;
  }

  @Mutation
  public SET_SMTA1WORKFLOWITEM_DOCUMENT(file: File | undefined): void {
    this.smta1WorkflowItemDocument.value = file;
  }

  @Mutation
  public SET_SMTA1WORKFLOWITEMEVENTS(
    worklistTimelines: Array<WorklistTimeline>
  ): void {
    this.worklistTimelines.value = worklistTimelines;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateSMTA1WorkflowItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new SMTA1WorkflowItemsService();
    const SMTA1WorkflowItem = this.smta1WorkflowItemCreate.value;
    if (SMTA1WorkflowItem === undefined) {
      this.SET_ERROR({
        message:
          "SMTA1WorkflowItemsStore: not expecting SMTA1WorkflowItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(SMTA1WorkflowItem);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateSMTA1WorkflowItemResponse = response.value;
      SMTA1WorkflowItem.Id = createResponse.Id;
      this.SET_SMTA1WORKFLOW(SMTA1WorkflowItem);
      this.SET_SMTA1WORKFLOW_CREATE(this.emptySMTA1WorkflowItem);
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
  public async UpdateSMTA1WorkflowItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new SMTA1WorkflowItemsService();
    const smta1WorkflowItem: SMTA1WorkflowItem | undefined =
      this.SMTA1WorkflowItem;
    const file = this.smta1WorkflowItemDocument.value;

    if (!smta1WorkflowItem) {
      this.SET_ERROR({
        message:
          "SMTA1WorkflowItemsStore: not expecting SMTA1WorkflowItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(smta1WorkflowItem, file);
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
      this.SET_SMTA1WORKFLOW(smta1WorkflowItem);
      AppModule.SetSuccessNotifications("Successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListSMTA1WorkflowItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new SMTA1WorkflowItemsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListSMTA1WorkflowItemResponse = response.value;
      this.SET_SMTA1WORKFLOWS(listResponse.SMTA1WorkflowItems);
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
  public async ListDashboardSMTA1WorkflowItems(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new SMTA1WorkflowItemsService();
    const response = await service.listForDashboard({});
    if (isLeft(response)) {
      const listResponse: ListDashboardSMTA1WorkflowItemResponse =
        response.value;
      this.SET_SMTA1WORKFLOWS(listResponse.SMTA1WorkflowItems);
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
  public async ListSMTA1WorkflowHistoryItems(
    SMTA1WorkflowItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListSMTA1WorkflowHistoryItemQuery = {
      SMTA1WorkflowItemId: SMTA1WorkflowItemId,
    };
    const service = new SMTA1WorkflowHistoryItemsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListSMTA1WorkflowHistoryItemResponse = response.value;
      this.SET_SMTA1WORKFLOWHISTORYITEMS(
        listResponse.SMTA1WorkflowHistoryItems
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
  public async ReadSMTA1WorkflowItem(id: string): Promise<void> {
    const service = new SMTA1WorkflowItemsService();
    const query: ReadSMTA1WorkflowItemQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadSMTA1WorkflowItemResponse = response.value;
      this.SET_SMTA1WORKFLOW(readResponse.SMTA1WorkflowItemDto);
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
    const id = this.smta1WorkflowItemDownloadFileInfo.value?.Id;
    const name =
      this.smta1WorkflowItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId = this.smta1WorkflowItemDownloadFileInfo.value?.WorklistId;

    const service = new SMTA1WorkflowItemsService();

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
    const id = this.smta1WorkflowItemDownloadFileInfo.value?.Id;
    const name =
      this.smta1WorkflowItemDownloadFileInfo.value?.Name.toLowerCase();
    const worklistId = this.smta1WorkflowItemDownloadFileInfo.value?.WorklistId;

    const service = new SMTA1WorkflowHistoryItemsService();
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
  public async DeleteSMTA1WorkflowItem(): Promise<void> {
    AppModule.ShowLoading();
    const service = new SMTA1WorkflowItemsService();
    const smta1WorkflowItem: SMTA1WorkflowItem | undefined =
      this.SMTA1WorkflowItem;
    if (!smta1WorkflowItem) {
      this.SET_ERROR({
        message:
          "SMTA1WorkflowItemsStore: not expecting SMTA1WorkflowItem to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(smta1WorkflowItem);
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
      const deleteSMTA1WorkflowItemResponse: DeleteSMTA1WorkflowItemResponse =
        response.value;
      this.SET_SMTA1WORKFLOW(undefined);
      AppModule.SetSuccessNotifications("Successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListSMTA1WorkflowItemEvents(
    SMTA1WorkflowItemId: string
  ): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListSMTA1WorkflowItemEventQuery = {
      SMTA1WorkflowItemId: SMTA1WorkflowItemId,
    };
    const service = new SMTA1WorkflowItemEventsService();
    const response = await service.list(query);
    if (isLeft(response)) {
      const listResponse: ListSMTA1WorkflowItemEventResponse = response.value;
      this.SET_SMTA1WORKFLOWITEMEVENTS(listResponse.WorklistTimelines);
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

  get SMTA1WorkflowItem(): SMTA1WorkflowItem | undefined {
    return this.smta1WorkflowItem.value;
  }

  get SMTA1WorkflowItemDocument(): File | undefined {
    return this.smta1WorkflowItemDocument.value;
  }

  get SMTA1WorkflowItems(): SMTA1WorkflowItem[] {
    return this.smta1WorkflowItems.value ?? new Array<SMTA1WorkflowItem>();
  }

  get SMTA1WorkflowHistoryItems(): SMTA1WorkflowItem[] {
    return (
      this.smta1WorkflowHistoryItems.value ?? new Array<SMTA1WorkflowItem>()
    );
  }

  get SMTA1WorkflowItemCreate(): SMTA1WorkflowItem {
    return this.smta1WorkflowItemCreate.value;
  }

  get Status(): SMTA1WorkflowStatus {
    return (
      this.smta1WorkflowItem.value?.CurrentStatus ??
      SMTA1WorkflowStatus.RequestInitiation
    );
  }

  get LaboratoryId(): string {
    return this.smta1WorkflowItem.value?.LaboratoryId ?? "";
  }

  get AttachmentType(): AttachmentType | undefined {
    return this.attachmentType?.value;
  }

  get WorklistTimelines(): WorklistTimeline[] {
    return this.worklistTimelines.value ?? new Array<WorklistTimeline>();
  }

  get emptySMTA1WorkflowItem(): SMTA1WorkflowItem {
    return Object.create({
      CurrentStatus: SMTA1WorkflowStatus.RequestInitiation,
      CurrentStatusName: "",
      PreviousStatus: SMTA1WorkflowStatus.RequestInitiation,
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
      DocumentTemplateFileType: DocumentFileType.SMTA1,
      HistoryDto: false,
      UserRoleName: "",
      UserRoleTypeName: "",
      UserRoleType: null,
    });
  }
}

export const SMTA1WorkflowItemModule = getModule(SMTA1WorkflowItemStore, store);
