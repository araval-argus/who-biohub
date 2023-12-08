import { Either } from "@/utils/either";
import { SeedData } from "@/models/constants/SeedData";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  CreateFolderCommand,
  CreateFolderResponse,
} from "./models/CreateFolder";
import {
  UploadFileCommand,
  UploadFileCommandResponse,
} from "./models/UploadFile";
import {
  DeleteDocumentTemplateCommand,
  DeleteDocumentTemplateResponse,
} from "./models/DeleteDocumentTemplate";
import {
  ListDocumentTemplatesQuery,
  ListDocumentTemplatesResponse,
} from "./models/ListDocumentTemplates";
import { ReadFileQuery, ReadFileResponse } from "./models/ReadFile";
import { UpdateNameCommand, UpdateNameResponse } from "./models/UpdateName";
import {
  CheckOtherCurrentPresentQuery,
  CheckOtherCurrentPresentResponse,
} from "./models/CheckOtherCurrentPresent";
import {
  CheckCurrentsForDeleteQuery,
  CheckCurrentsForDeleteResponse,
} from "./models/CheckCurrentsForDelete";

import {
  ListSMTADocumentTemplatesQuery,
  ListSMTADocumentTemplatesResponse,
} from "./models/ListSMTADocumentTemplates";

import {
  ReadEFormFileQuery,
  ReadEFormFileResponse,
} from "./models/ReadEFormFile";

export interface iDocumentTemplatesService {
  read(
    query: ReadFileQuery
  ): Promise<Either<ReadFileResponse, CommunicationError>>;

  checkOtherCurrentPresent(
    query: CheckOtherCurrentPresentQuery
  ): Promise<Either<CheckOtherCurrentPresentResponse, CommunicationError>>;

  folderContainsCurrent(
    query: CheckCurrentsForDeleteQuery
  ): Promise<Either<CheckCurrentsForDeleteResponse, CommunicationError>>;

  list(
    query: ListDocumentTemplatesQuery
  ): Promise<Either<ListDocumentTemplatesResponse, CommunicationError>>;

  listSMTA(
    query: ListSMTADocumentTemplatesQuery
  ): Promise<Either<ListSMTADocumentTemplatesResponse, CommunicationError>>;

  createFolder(
    command: CreateFolderCommand
  ): Promise<Either<CreateFolderResponse, CommunicationError>>;

  uploadFile(
    command: UploadFileCommand,
    file: File
  ): Promise<Either<UploadFileCommandResponse, CommunicationError>>;

  delete(
    command: DeleteDocumentTemplateCommand
  ): Promise<Either<DeleteDocumentTemplateResponse, CommunicationError>>;

  update(
    command: UpdateNameCommand
  ): Promise<Either<UpdateNameResponse, CommunicationError>>;

  readEForm(
    query: ReadEFormFileQuery
  ): Promise<Either<ReadEFormFileResponse, CommunicationError>>;
}

export class DocumentTemplatesService implements iDocumentTemplatesService {
  private httpClient: iHttpClient;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "documenttemplates/";
  private buildUrl(suffix: string | undefined): string {
    if (suffix) return `${this.baseUrl}${suffix}`;
    else return this.baseUrl;
  }

  async read(query: ReadFileQuery): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl("readfile/" + query.Id);
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }

  async checkOtherCurrentPresent(
    query: CheckOtherCurrentPresentQuery
  ): Promise<Either<CheckOtherCurrentPresentResponse, CommunicationError>> {
    const url = this.buildUrl("checkothercurrentpresent/" + query.Id);
    const response =
      await this.httpClient.get<CheckOtherCurrentPresentResponse>(url);
    return response;
  }

  async folderContainsCurrent(
    query: CheckCurrentsForDeleteQuery
  ): Promise<Either<CheckCurrentsForDeleteResponse, CommunicationError>> {
    const url = this.buildUrl("foldercontainscurrent/" + query.Id);
    const response = await this.httpClient.get<CheckCurrentsForDeleteResponse>(
      url
    );
    return response;
  }

  async list(
    query: ListDocumentTemplatesQuery
  ): Promise<Either<ListDocumentTemplatesResponse, CommunicationError>> {
    const url = this.buildUrl(
      "readfolder/" + query.Id ?? SeedData.DocumentTemplateRootFolderId
    );
    const response = await this.httpClient.get<ListDocumentTemplatesResponse>(
      url
    );
    return response;
  }

  async listSMTA(
    query: ListSMTADocumentTemplatesQuery
  ): Promise<Either<ListSMTADocumentTemplatesResponse, CommunicationError>> {
    const url = this.buildUrl("smta");
    const response =
      await this.httpClient.get<ListSMTADocumentTemplatesResponse>(url);
    return response;
  }

  async createFolder(
    command: CreateFolderCommand
  ): Promise<Either<CreateFolderResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateFolderCommand,
      CreateFolderResponse
    >(this.baseUrl + "createfolder", command);

    return response;
  }

  async uploadFile(
    command: UploadFileCommand,
    file: File
  ): Promise<Either<UploadFileCommandResponse, CommunicationError>> {
    const formData = new FormData();
    formData.append("data", JSON.stringify(command));
    formData.append("file", file, file.name);
    const response = await this.httpClient.postFile<UploadFileCommandResponse>(
      this.buildUrl("uploadfile"),
      formData
    );

    return response;
  }

  async delete(
    command: DeleteDocumentTemplateCommand
  ): Promise<Either<DeleteDocumentTemplateResponse, CommunicationError>> {
    const url = this.buildUrl("delete/" + command.Id);
    const response =
      await this.httpClient.delete<DeleteDocumentTemplateResponse>(url);
    return response;
  }

  async update(
    command: UpdateNameCommand
  ): Promise<Either<UpdateNameResponse, CommunicationError>> {
    const url = this.buildUrl("updateName/" + command.Id);
    const response = await this.httpClient.patch<
      UpdateNameCommand,
      UpdateNameResponse
    >(url, command);
    return response;
  }

  async readEForm(
    query: ReadFileQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl("readeformfile/" + query.Id);
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }
}
