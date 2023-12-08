import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Resource } from "@/models/Resource";
import { isLeft, isRight, ParseError } from "@/utils/either";
import { resources, resourcesPublic } from "./mock";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { DocumentType } from "@/models/enums/DocumentType";
import { ResourcesService } from "@/services/resources/ResourcesService";
import { UploadResourceFileTokenQueryResponse } from "@/services/resources/models/UploadResourceFileToken";
import { ListResourcesResponse } from "@/services/resources/models/ListResources";
import { DeleteResourceResponse } from "@/services/resources/models/DeleteResource";
import {
  CreateResourceCommand,
  CreateResourceResponse,
} from "@/services/resources/models/CreateResource";
import { ListResourcesPublicResponse } from "@/services/resources/models/ListResourcesPublic";
import { ResourcePublic } from "@/models/ResourcePublic";
import { CreateFolderResponse } from "@/services/resources/models/CreateFolder";

export interface ResourceState {
  Error: AppError | undefined;
  ErrorMessage: any;
  Resource: Resource | undefined;
  ResourceBuffer: any;
  Resources: Array<Resource>;
  ResourceCreate: Resource | undefined;
  ResourceCreateFile: File | undefined;
  ResourcesPublic: Array<ResourcePublic>;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "resources",
  store: store,
})
class ResourceStore extends VuexModule implements ResourceState {
  // Private variables
  private resourceCreate: {
    value: Resource;
  } = {
    value: this.emptyResource,
  };

  private resourceCreateFile: {
    value: File | undefined;
  } = {
    value: undefined,
  };

  private resource: { value: Resource | undefined } = {
    value: undefined,
  };

  private resourceBuffer: { value: any } = {
    value: undefined,
  };

  private resources: { value: Array<Resource> } = {
    value: resources,
  };

  private resourcesPublic: { value: Array<ResourcePublic> } = {
    value: resourcesPublic,
  };

  private resourcePublic: { value: ResourcePublic | undefined } = {
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
  public SET_RESOURCE_CREATE(resource: Resource): void {
    this.resourceCreate.value = resource;
  }

  @Mutation
  public SET_RESOURCE_CREATE_FILE(file: File | undefined): void {
    this.resourceCreateFile.value = file;
  }

  // Details - Edit
  @Mutation
  public SET_RESOURCE(resource: Resource | undefined): void {
    this.resource.value = resource;
  }

  @Mutation
  public SET_RESOURCE_BUFFER(arrayBuffer: any): void {
    this.resourceBuffer.value = arrayBuffer;
  }

  @Mutation
  public CLEAR_RESOURCE(): void {
    this.resource.value = undefined;
  }

  @Mutation
  public CLEAR_RESOURCE_CREATE(): void {
    this.resourceCreate.value = this.emptyResource;
    this.resourceCreateFile.value = undefined;
  }

  // List
  @Mutation
  public SET_RESOURCES(resources: Array<Resource>): void {
    this.resources.value = resources;
  }

  @Mutation
  public SET_RESOURCES_PUBLIC(resources: Array<ResourcePublic>): void {
    this.resourcesPublic.value = resources;
  }

  @Mutation
  public SET_RESOURCE_PUBLIC(resource: ResourcePublic | undefined): void {
    this.resourcePublic.value = resource;
  }

  @Action({ rawError: true })
  public async CreateFolder(): Promise<void> {
    AppModule.ShowLoading();
    const service = new ResourcesService();
    const folder = this.resourceCreate.value;
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
        //folder = createResponse.Resource;
        this.SET_RESOURCE(folder);
        this.SET_RESOURCE_CREATE(this.emptyResource);
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

  // Actions
  @Action({ rawError: true })
  public async UploadFile(): Promise<void> {
    AppModule.ShowLoading();
    const service = new ResourcesService();
    const file = this.resourceCreateFile.value;
    if (file === undefined) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting resource to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    } else {
      AppModule.ClearErrorNotifications();
      AppModule.ClearSuccessNotifications();
      const responseUpload = await service.uploadResource(
        {
          FileCompleteName: file.name,
          FileType: this.resourceCreate.value.FileType,
        },
        file
      );
      if (isLeft(responseUpload)) {
        this.SET_ERROR(undefined);
        const createResponseUpload: UploadResourceFileTokenQueryResponse =
          responseUpload.value;

        const command = Object.assign({
          Id: createResponseUpload.Id,
          FileCompleteName: createResponseUpload.FileCompleteName,
          FileType: this.resourceCreate.value.FileType,
          ParentId: this.resourceCreate.value.ParentId,
        }) as CreateResourceCommand;

        const response = await service.create(command);
        if (isLeft(response)) {
          this.SET_RESOURCE_CREATE(this.emptyResource);
          AppModule.SetSuccessNotifications("File successfully uploaded");
          AppModule.HideLoading();
          return;
        }
      }

      // this.SET_ERROR(response.value as AppError);
      // if (
      //   response.value.message !== undefined &&
      //   response.value.message["ErrorType"] != 3
      // ) {
      //   AppModule.SetErrorNotifications(this.ErrorMessage);
      // }
      AppModule.HideLoading();
      //throw response.value;
    }
  }

  @Action({ rawError: true })
  public async ListResources(parentId: string | undefined): Promise<void> {
    const service = new ResourcesService();
    const response = await service.list({ Id: parentId });
    if (isLeft(response)) {
      const listResponse: ListResourcesResponse = response.value;
      this.SET_RESOURCES(listResponse.Resources);
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
  public async DownloadResource(): Promise<void> {
    const id = this.resource.value?.Id;
    const service = new ResourcesService();
    if (id !== undefined) {
      const response = await service.downloadResource({ Id: id });
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
  public async UpdateResource(): Promise<void> {
    AppModule.ShowLoading();
    const service = new ResourcesService();
    const resource = this.Resource;
    if (!resource) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting resource to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(resource);
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
      this.SET_RESOURCE(resource);
      AppModule.SetSuccessNotifications("File successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteResource(): Promise<void> {
    AppModule.ShowLoading();
    const service = new ResourcesService();
    const resource = this.Resource;
    if (!resource) {
      this.SET_ERROR({
        message:
          "TemplatesStore: not expecting resource to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(resource);
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
      const deleteTemplateResponse: DeleteResourceResponse = response.value;
      this.SET_RESOURCE(undefined);
      AppModule.SetSuccessNotifications("File successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListResourcesPublic(
    parentId: string | undefined
  ): Promise<void> {
    const service = new ResourcesService();
    const response = await service.listPublic({ Id: parentId });
    if (isLeft(response)) {
      const listResponse: ListResourcesPublicResponse = response.value;
      this.SET_RESOURCES_PUBLIC(listResponse.Resources);
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
  public async DownloadResourcePublic(): Promise<void> {
    const id = this.resourcePublic.value?.Id;
    const service = new ResourcesService();
    if (id !== undefined) {
      const response = await service.downloadResourcePublic({ Id: id });
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

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get Resource(): Resource | undefined {
    return this.resource.value;
  }

  get ResourceBuffer(): any {
    return this.resourceBuffer.value;
  }

  get Resources(): Resource[] {
    return this.resources.value ?? new Array<Resource>();
  }

  get ResourceCreate(): Resource {
    return this.resourceCreate.value;
  }

  get ResourceCreateFile(): File | undefined {
    return this.resourceCreateFile.value;
  }

  get ResourcesPublic(): ResourcePublic[] {
    return this.resourcesPublic.value ?? new Array<ResourcePublic>();
  }

  get emptyResource(): Resource {
    return Object.create({
      Id: "",
      Name: "",
      Extension: undefined,
      UploadTime: new Date(),
      UploadedBy: "",
    } as Resource);
  }

  get emptyResourcePublic(): ResourcePublic {
    return Object.create({
      Id: "",
      Name: "",
      Extension: undefined,
    } as Resource);
  }
}

export const ResourceModule = getModule(ResourceStore, store);
