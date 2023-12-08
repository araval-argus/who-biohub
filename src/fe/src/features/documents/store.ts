import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Document } from "@/models/Document";
import { isLeft, isRight, ParseError } from "@/utils/either";
import { documents } from "./mock";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { DocumentType } from "@/models/enums/DocumentType";
import { DocumentsService } from "@/services/documents/DocumentsService";

import { ListDocumentsResponse } from "@/services/documents/models/ListDocuments";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { CheckDocumentResponse } from "@/services/documents/models/CheckDocument";
import { CanStartSMTARequestResponse } from "@/services/documents/models/CanStartSMTARequest";
import { ListSignedSMTADocumentsResponse } from "@/services/documents/models/ListSignedSMTADocuments";

export interface DocumentState {
  Error: AppError | undefined;
  ErrorMessage: any;
  Document: Document | undefined;
  DocumentBuffer: any;
  Documents: Array<Document>;
  DocumentCreate: Document | undefined;
  DocumentCreateFile: File | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "documents",
  store: store,
})
class DocumentStore extends VuexModule implements DocumentState {
  // Private variables
  private documentCreate: {
    value: Document;
  } = {
    value: this.emptyDocument,
  };

  private documentCreateFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private document: { value: Document | undefined } = {
    value: undefined,
  };

  private documentBuffer: { value: any } = {
    value: undefined,
  };

  private documents: { value: Array<Document> } = {
    value: documents,
  };

  private currentFolderDocuments: { value: Array<Document> } = {
    value: [],
  };

  private smta1DocumentSigned: { value: boolean } = {
    value: false,
  };

  private smta2DocumentSigned: { value: boolean } = {
    value: false,
  };

  private canStartSmta1Request: { value: boolean } = {
    value: false,
  };

  private canStartSmta2Request: { value: boolean } = {
    value: false,
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
  public SET_DOCUMENT_CREATE(template: Document): void {
    this.documentCreate.value = template;
  }

  @Mutation
  public SET_DOCUMENT_CREATE_FILE(file: File | undefined): void {
    this.documentCreateFile.value = file;
  }

  // Details - Edit
  @Mutation
  public SET_DOCUMENT(template: Document | undefined): void {
    this.document.value = template;
  }

  @Mutation
  public SET_DOCUMENT_BUFFER(arrayBuffer: any): void {
    this.documentBuffer.value = arrayBuffer;
  }

  @Mutation
  public CLEAR_DOCUMENT(): void {
    this.document.value = undefined;
  }

  // List
  @Mutation
  public SET_DOCUMENTS(templates: Array<Document>): void {
    this.documents.value = templates;
  }

  @Mutation
  public SET_CURRENT_FOLDER_DOCUMENTS(folderId: string | null): void {
    this.currentFolderDocuments.value = this.documents.value.filter((r) => {
      return r.ParentId == folderId;
    });
  }

  @Mutation
  public SET_CAN_START_SMTA1_REQUEST(canStart: boolean): void {
    this.canStartSmta1Request.value = canStart;
  }

  @Mutation
  public SET_CAN_START_SMTA2_REQUEST(canStart: boolean): void {
    this.canStartSmta2Request.value = canStart;
  }

  @Mutation
  public SET_SMTA1_DOCUMENT_SIGNED(isSigned: boolean): void {
    this.smta1DocumentSigned.value = isSigned;
  }

  @Mutation
  public SET_SMTA2_DOCUMENT_SIGNED(isSigned: boolean): void {
    this.smta2DocumentSigned.value = isSigned;
  }

  @Action({ rawError: true })
  public async ListDocuments(): Promise<void> {
    const service = new DocumentsService();
    const response = await service.list();
    if (isLeft(response)) {
      const listResponse: ListDocumentsResponse = response.value;
      this.SET_DOCUMENTS(listResponse.Documents);
      this.SET_CURRENT_FOLDER_DOCUMENTS(null);

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
  public async ListSignedSMTADocuments(): Promise<void> {
    const service = new DocumentsService();
    const response = await service.listSignedSMTA();
    if (isLeft(response)) {
      const listResponse: ListSignedSMTADocumentsResponse = response.value;
      this.SET_DOCUMENTS(listResponse.Documents);

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
  public async ReadDocument(): Promise<void> {
    const id = this.document.value?.Id;
    const name =
      this.document.value?.Name +
      "." +
      this.document.value?.Extension?.toLowerCase();
    const service = new DocumentsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.read({ Id: id, Name: name });
      if (isRight(response)) {
        if (response.value.code == 404) {
          AppModule.SetErrorNotifications("File not found");
        } else if (response.value.code == 401) {
          AppModule.SetErrorNotifications("Unauthorized");
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
  public async ReadDocumentForShipmentRequest(
    info: Map<string, string>
  ): Promise<void> {
    const id = info.get("Id");
    const name = info.get("Name");

    const service = new DocumentsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.read({ Id: id, Name: name });
      if (isRight(response)) {
        if (response.value.code == 404) {
          AppModule.SetErrorNotifications("File not found");
        } else if (response.value.code == 401) {
          AppModule.SetErrorNotifications("Unauthorized");
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
  public async CheckSMTA1Document(laboratoryId: string): Promise<void> {
    const service = new DocumentsService();
    const type = DocumentFileType.SMTA1;
    const response = await service.check({
      Type: type,
      LaboratoryId: laboratoryId,
    });
    if (isRight(response)) {
      if (response.value.code == 404) {
        AppModule.SetErrorNotifications("Document not found");
      } else if (response.value.code == 401) {
        AppModule.SetErrorNotifications("Unauthorized");
      } else {
        AppModule.SetErrorNotifications("Internal Server Error");
      }
      throw response;
    }
    const readResponse: CheckDocumentResponse = response.value;
    this.SET_SMTA1_DOCUMENT_SIGNED(readResponse.IsSigned);
    this.SET_ERROR(undefined);
    return;
  }

  @Action({ rawError: true })
  public async CheckSMTA2Document(laboratoryId: string): Promise<void> {
    const service = new DocumentsService();
    const type = DocumentFileType.SMTA2;
    const response = await service.check({
      Type: type,
      LaboratoryId: laboratoryId,
    });
    if (isRight(response)) {
      if (response.value.code == 404) {
        AppModule.SetErrorNotifications("Document not found");
      } else if (response.value.code == 401) {
        AppModule.SetErrorNotifications("Unauthorized");
      } else {
        AppModule.SetErrorNotifications("Internal Server Error");
      }
      throw response;
    }
    const readResponse: CheckDocumentResponse = response.value;
    this.SET_SMTA2_DOCUMENT_SIGNED(readResponse.IsSigned);
    this.SET_ERROR(undefined);
    return;
  }

  @Action({ rawError: true })
  public async CanStartSMTA1RequestCheck(): Promise<void> {
    const service = new DocumentsService();
    const type = DocumentFileType.SMTA1;
    const response = await service.canStartSMTARequest({ Type: type });
    if (isRight(response)) {
      if (response.value.code == 404) {
        AppModule.SetErrorNotifications("Document not found");
      } else if (response.value.code == 401) {
        AppModule.SetErrorNotifications("Unauthorized");
      } else {
        AppModule.SetErrorNotifications("Internal Server Error");
      }
      throw response;
    }
    const readResponse: CanStartSMTARequestResponse = response.value;
    this.SET_CAN_START_SMTA1_REQUEST(readResponse.CanStartSMTARequest);
    this.SET_ERROR(undefined);
    return;
  }

  @Action({ rawError: true })
  public async CanStartSMTA2RequestCheck(): Promise<void> {
    const service = new DocumentsService();
    const type = DocumentFileType.SMTA2;
    const response = await service.canStartSMTARequest({ Type: type });
    if (isRight(response)) {
      if (response.value.code == 404) {
        AppModule.SetErrorNotifications("Document not found");
      } else if (response.value.code == 401) {
        AppModule.SetErrorNotifications("Unauthorized");
      } else {
        AppModule.SetErrorNotifications("Internal Server Error");
      }
      throw response;
    }
    const readResponse: CanStartSMTARequestResponse = response.value;
    this.SET_CAN_START_SMTA2_REQUEST(readResponse.CanStartSMTARequest);
    this.SET_ERROR(undefined);
    return;
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get Document(): Document | undefined {
    return this.document.value;
  }

  get DocumentBuffer(): any {
    return this.documentBuffer.value;
  }

  get Documents(): Document[] {
    return this.documents.value ?? new Array<Document>();
  }

  get CurrentFolderDocuments(): Document[] {
    return this.currentFolderDocuments.value ?? new Array<Document>();
  }

  get DocumentCreate(): Document {
    return this.documentCreate.value;
  }

  get DocumentCreateFile(): File | undefined {
    return this.documentCreateFile.value;
  }

  get SMTA1DocumentSigned(): boolean {
    return this.smta1DocumentSigned.value;
  }

  get SMTA2DocumentSigned(): boolean {
    return this.smta2DocumentSigned.value;
  }

  get CanStartSMTA1Request(): boolean {
    return this.canStartSmta1Request.value;
  }

  get CanStartSMTA2Request(): boolean {
    return this.canStartSmta2Request.value;
  }

  get emptyDocument(): Document {
    return Object.create({
      Id: "",
      Name: "",
      Extension: undefined,
      Type: DocumentType.File,
      UploadTime: new Date(),
      UploadedBy: "",
      ParentId: "",
    } as Document);
  }
}

export const DocumentModule = getModule(DocumentStore, store);
