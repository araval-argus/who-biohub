import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { DocumentTemplate } from "@/models/DocumentTemplate";
import { isLeft, isRight, ParseError } from "@/utils/either";
import { documentTemplates } from "./mock";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { DocumentType } from "@/models/enums/DocumentType";
import { DocumentTemplatesService } from "@/services/documentTemplates/DocumentTemplatesService";
import { UploadFileCommandResponse } from "@/services/documentTemplates/models/UploadFile";
import { CreateFolderResponse } from "@/services/documentTemplates/models/CreateFolder";
import { ListDocumentTemplatesResponse } from "@/services/documentTemplates/models/ListDocumentTemplates";
import { DeleteDocumentTemplateResponse } from "@/services/documentTemplates/models/DeleteDocumentTemplate";
import {
  CheckOtherCurrentPresentQuery,
  CheckOtherCurrentPresentResponse,
} from "@/services/documentTemplates/models/CheckOtherCurrentPresent";
import {
  CheckCurrentsForDeleteQuery,
  CheckCurrentsForDeleteResponse,
} from "@/services/documentTemplates/models/CheckCurrentsForDelete";
import { ListSMTADocumentTemplatesResponse } from "@/services/documentTemplates/models/ListSMTADocumentTemplates";

export interface DocumentTemplateState {
  Error: AppError | undefined;
  ErrorMessage: any;
  DocumentTemplate: DocumentTemplate | undefined;
  DocumentTemplateBuffer: any;
  DocumentTemplates: Array<DocumentTemplate>;
  DocumentTemplateCreate: DocumentTemplate | undefined;
  DocumentTemplateCreateFile: File | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "templates",
  store: store,
})
class DocumentTemplateStore
  extends VuexModule
  implements DocumentTemplateState
{
  // Private variables
  private documentTemplateCreate: {
    value: DocumentTemplate;
  } = {
    value: this.emptyDocumentTemplate,
  };

  private documentTemplateCreateFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private documentTemplate: { value: DocumentTemplate | undefined } = {
    value: undefined,
  };

  private documentTemplateBuffer: { value: any } = {
    value: undefined,
  };

  private documentTemplates: { value: Array<DocumentTemplate> } = {
    value: documentTemplates,
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
  public SET_DOCUMENTTEMPLATE_CREATE(template: DocumentTemplate): void {
    this.documentTemplateCreate.value = template;
  }

  @Mutation
  public SET_DOCUMENTTEMPLATE_CREATE_FILE(file: File | undefined): void {
    this.documentTemplateCreateFile.value = file;
  }

  // Details - Edit
  @Mutation
  public SET_DOCUMENTTEMPLATE(template: DocumentTemplate | undefined): void {
    this.documentTemplate.value = template;
  }

  @Mutation
  public SET_DOCUMENTTEMPLATE_BUFFER(arrayBuffer: any): void {
    this.documentTemplateBuffer.value = arrayBuffer;
  }

  @Mutation
  public CLEAR_DOCUMENTTEMPLATE(): void {
    this.documentTemplate.value = undefined;
  }

  @Mutation
  public CLEAR_DOCUMENTTEMPLATE_CREATE(): void {
    this.documentTemplateCreate.value = this.emptyDocumentTemplate;
    this.documentTemplateCreateFile.value = undefined;
  }

  // List
  @Mutation
  public SET_DOCUMENTTEMPLATES(templates: Array<DocumentTemplate>): void {
    this.documentTemplates.value = templates;
  }

  // Actions
  @Action({ rawError: true })
  public async UploadFile(): Promise<void> {
    AppModule.ShowLoading();
    const service = new DocumentTemplatesService();
    let template = this.documentTemplateCreate.value;
    const file = this.documentTemplateCreateFile.value;
    if (template?.ParentId === undefined || file === undefined) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting template to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    } else {
      AppModule.ClearErrorNotifications();
      AppModule.ClearSuccessNotifications();
      const response = await service.uploadFile(
        {
          ParentId: template.ParentId,
          DocumentTemplateFileType: template.FileType,
        },
        file
      );
      if (isLeft(response)) {
        this.SET_ERROR(undefined);
        const createResponse: UploadFileCommandResponse = response.value;
        template = createResponse.DocumentTemplate;
        this.SET_DOCUMENTTEMPLATE(template);
        this.SET_DOCUMENTTEMPLATE_CREATE(this.emptyDocumentTemplate);
        AppModule.SetSuccessNotifications("File successfully uploaded");
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
  }

  @Action({ rawError: true })
  public async CreateFolder(): Promise<void> {
    AppModule.ShowLoading();
    const service = new DocumentTemplatesService();
    let folder = this.documentTemplateCreate.value;
    if (folder?.ParentId === undefined) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting folder to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    } else {
      AppModule.ClearErrorNotifications();
      AppModule.ClearSuccessNotifications();
      const response = await service.createFolder(folder);
      if (isLeft(response)) {
        this.SET_ERROR(undefined);
        const createResponse: CreateFolderResponse = response.value;
        folder = createResponse.DocumentTemplate;
        this.SET_DOCUMENTTEMPLATE(folder);
        this.SET_DOCUMENTTEMPLATE_CREATE(this.emptyDocumentTemplate);
        AppModule.SetSuccessNotifications("Folder successfully created");
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
  }

  @Action({ rawError: true })
  public async ListDocumentTemplates(
    parentId: string | undefined
  ): Promise<void> {
    const service = new DocumentTemplatesService();
    const response = await service.list({ Id: parentId });
    if (isLeft(response)) {
      const listResponse: ListDocumentTemplatesResponse = response.value;
      this.SET_DOCUMENTTEMPLATES(listResponse.DocumentTemplates);
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
  public async ListSMTADocumentTemplates(): Promise<void> {
    const service = new DocumentTemplatesService();
    const response = await service.listSMTA({});
    if (isLeft(response)) {
      const listResponse: ListSMTADocumentTemplatesResponse = response.value;
      this.SET_DOCUMENTTEMPLATES(listResponse.DocumentTemplates);
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
  public async ReadTemplate(): Promise<void> {
    const id = this.documentTemplate.value?.Id;
    const name =
      this.documentTemplate.value?.Name +
      "." +
      this.documentTemplate.value?.Extension?.toLowerCase();
    const service = new DocumentTemplatesService();
    if (id !== undefined && name !== undefined) {
      const response = await service.read({ Id: id, Name: name });
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
  public async ReadTemplateForShipmentRequest(
    info: Map<string, string>
  ): Promise<void> {
    const id = info.get("Id");
    const name = info.get("Name");

    const service = new DocumentTemplatesService();
    if (id !== undefined && name !== undefined) {
      const response = await service.read({ Id: id, Name: name });
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
  public async ReadTemplateForEForm(info: Map<string, string>): Promise<void> {
    const id = info.get("Id");
    const name = info.get("Name");

    const service = new DocumentTemplatesService();
    if (id !== undefined && name !== undefined) {
      const response = await service.readEForm({ Id: id, Name: name });
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
  public async CheckOtherCurrentPresent(): Promise<boolean> {
    const service = new DocumentTemplatesService();
    const query: CheckOtherCurrentPresentQuery = {
      Id: this.DocumentTemplate?.Id ?? "",
    };
    const response = await service.checkOtherCurrentPresent(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: CheckOtherCurrentPresentResponse = response.value;
      return readResponse.IsOtherCurrentPresent;
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
  public async FolderContainsCurrent(): Promise<boolean> {
    const service = new DocumentTemplatesService();
    const query: CheckCurrentsForDeleteQuery = {
      Id: this.DocumentTemplate?.Id ?? "",
    };
    const response = await service.folderContainsCurrent(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: CheckCurrentsForDeleteResponse = response.value;
      return readResponse.FolderContainsCurrent;
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
  public async UpdateDocumentTemplate(): Promise<void> {
    AppModule.ShowLoading();
    const service = new DocumentTemplatesService();
    const template = this.DocumentTemplate;
    if (!template) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting template to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(template);
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
      this.SET_DOCUMENTTEMPLATE(template);
      AppModule.SetSuccessNotifications(
        template.Type == DocumentType.File
          ? "File" + " successfully updated"
          : "Folder" + " successfully updated"
      );
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteDocumentTemplate(): Promise<void> {
    AppModule.ShowLoading();
    const service = new DocumentTemplatesService();
    const template = this.DocumentTemplate;
    if (!template) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting template to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(template);
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
      const deleteTemplateResponse: DeleteDocumentTemplateResponse =
        response.value;
      this.SET_DOCUMENTTEMPLATE(undefined);
      AppModule.SetSuccessNotifications(
        template.Type == DocumentType.File
          ? "File" + " successfully deleted"
          : "Folder" + " successfully deleted"
      );
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

  get DocumentTemplate(): DocumentTemplate | undefined {
    return this.documentTemplate.value;
  }

  get DocumentTemplateBuffer(): any {
    return this.documentTemplateBuffer.value;
  }

  get DocumentTemplates(): DocumentTemplate[] {
    return this.documentTemplates.value ?? new Array<DocumentTemplate>();
  }

  get DocumentTemplateCreate(): DocumentTemplate {
    return this.documentTemplateCreate.value;
  }

  get DocumentTemplateCreateFile(): File | undefined {
    return this.documentTemplateCreateFile.value;
  }

  get emptyDocumentTemplate(): DocumentTemplate {
    return Object.create({
      Id: "",
      Name: "",
      Extension: undefined,
      Type: DocumentType.File,
      UploadTime: new Date(),
      UploadedBy: "",
      ParentId: "",
    } as DocumentTemplate);
  }
}

export const DocumentTemplateModule = getModule(DocumentTemplateStore, store);
