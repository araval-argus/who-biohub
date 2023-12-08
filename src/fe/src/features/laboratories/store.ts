import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Laboratory } from "@/models/Laboratory";
import { UserRequest } from "@/models/UserRequest";
import { LaboratoryPublic } from "@/models/LaboratoryPublic";
import { LaboratoriesService } from "@/services/laboratories/LaboratoriesService";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { laboratories } from "./mock";
import { laboratoriesPublic } from "./mock";
import { laboratoriesMapPublic } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListLaboratoryResponse } from "@/services/laboratories/models/ListLaboratory";
import { ReadLaboratoryResponse } from "@/services/laboratories/models/ReadLaboratory";
import { CreateLaboratoryResponse } from "@/services/laboratories/models/CreateLaboratory";
import { DeleteLaboratoryResponse } from "@/services/laboratories/models/DeleteLaboratory";
import { ReadLaboratoryQuery } from "@/services/laboratories/models/ReadLaboratory";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "../../store/MainStore";
import { ListLaboratoryPublicResponse } from "@/services/laboratories/models/ListLaboratoryPublic";
import { ReadLaboratoryPublicResponse } from "@/services/laboratories/models/ReadLaboratoryPublic";
import { ReadLaboratoryPublicQuery } from "@/services/laboratories/models/ReadLaboratoryPublic";
import { CreateLaboratoryFromUserRequestResponse } from "@/services/laboratories/models/CreateLaboratoryFromUserRequest";
import { ListLaboratoryMapPublicResponse } from "@/services/laboratories/models/ListLaboratoryMapPublic";
import { LaboratoryMapPublic } from "@/models/LaboratoryMapPublic";
import { laboratoriesMap } from "./mock";
import { LaboratoryMap } from "@/models/LaboratoryMap";
import { ListLaboratoryMapResponse } from "@/services/laboratories/models/ListLaboratoryMap";

export interface LaboratoryState {
  LaboratoryCreate: Laboratory | undefined;
  Laboratory: Laboratory | undefined;
  Laboratories: Array<Laboratory>;
  LaboratoriesMap: Array<LaboratoryMap>;
  LaboratoryPublic: LaboratoryPublic | undefined;
  LaboratoriesPublic: Array<LaboratoryPublic>;
  LaboratoriesMapPublic: Array<LaboratoryMapPublic>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "laboratories",
  store: store,
})
class LaboratoryStore extends VuexModule implements LaboratoryState {
  // Private variables
  private laboratoryCreate: { value: Laboratory } = {
    value: this.emptyLaboratory,
  };

  private laboratory: { value: Laboratory | undefined } = {
    value: undefined,
  };

  private laboratoryPublic: { value: LaboratoryPublic | undefined } = {
    value: undefined,
  };

  private laboratories: { value: Array<Laboratory> } = { value: laboratories };

  private laboratoriesMap: { value: Array<LaboratoryMap> } = {
    value: laboratoriesMap,
  };

  private laboratoriesPublic: { value: Array<LaboratoryPublic> } = {
    value: laboratoriesPublic,
  };

  private laboratoriesMapPublic: { value: Array<LaboratoryMapPublic> } = {
    value: laboratoriesMapPublic,
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
  public SET_LABORATORY_CREATE(laboratory: Laboratory): void {
    this.laboratoryCreate.value = laboratory;
  }

  // Details - Edit
  @Mutation
  public SET_LABORATORY(laboratory: Laboratory | undefined): void {
    this.laboratory.value = laboratory;
  }

  @Mutation
  public SET_LABORATORY_PUBLIC(laboratory: LaboratoryPublic | undefined): void {
    this.laboratoryPublic.value = laboratory;
  }

  @Mutation
  public CLEAR_LABORATORY(): void {
    this.laboratory.value = undefined;
  }

  @Mutation
  public CLEAR_LABORATORY_CREATE(): void {
    this.laboratoryCreate.value = Object.create({
      Id: "",
      Name: "",
      Abbreviation: "",
      IsActive: false,
      Description: "",
      Address: "",
      Latitude: "",
      Longitude: "",
      BSLLevelId: "",
      CountryId: "",
      IsPublicFacing: false,
    });
  }

  // List
  @Mutation
  public SET_LABORATORIES(laboratories: Array<Laboratory>): void {
    this.laboratories.value = laboratories;
  }

  @Mutation
  public SET_LABORATORIES_MAP(laboratories: Array<LaboratoryMap>): void {
    this.laboratoriesMap.value = laboratories;
  }

  @Mutation
  public SET_LABORATORIES_PUBLIC(laboratories: Array<LaboratoryPublic>): void {
    this.laboratoriesPublic.value = laboratories;
  }

  @Mutation
  public SET_LABORATORIES_MAP_PUBLIC(
    laboratories: Array<LaboratoryMapPublic>
  ): void {
    this.laboratoriesMapPublic.value = laboratories;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateLaboratory(): Promise<void> {
    AppModule.ShowLoading();
    const service = new LaboratoriesService();
    const laboratory = this.laboratoryCreate.value;
    if (laboratory === undefined) {
      this.SET_ERROR({
        message:
          "LaboratoriesStore: not expecting laboratory to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(laboratory);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateLaboratoryResponse = response.value;
      laboratory.Id = createResponse.Id;
      this.SET_LABORATORY(laboratory);
      this.SET_LABORATORY_CREATE(this.emptyLaboratory);
      AppModule.SetSuccessNotifications("Laboratory successfully created");
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

  // Actions
  @Action({ rawError: true })
  public async CreateLaboratoryFromUserRequest(
    userRequest: UserRequest | undefined
  ): Promise<string> {
    const service = new LaboratoriesService();
    const laboratory = userRequest;
    if (laboratory === undefined) {
      this.SET_ERROR({
        message:
          "LaboratoriesStore: not expecting laboratory to be undefined; this should be a bug",
      });
      return "";
    }
    const response = await service.createFromUserRequest(laboratory);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateLaboratoryFromUserRequestResponse =
        response.value;
      const laboratoryId = createResponse.Laboratory.Id;
      return laboratoryId;
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
  public async ListLaboratories(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new LaboratoriesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListLaboratoryResponse = response.value;
      this.SET_LABORATORIES(listResponse.Laboratories);
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
  public async ListLaboratoriesMap(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new LaboratoriesService();
    const response = await service.listMap({});
    if (isLeft(response)) {
      const listResponse: ListLaboratoryMapResponse = response.value;
      this.SET_LABORATORIES_MAP(listResponse.Laboratories);
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
  public async ReadLaboratory(id: string): Promise<void> {
    const service = new LaboratoriesService();
    const query: ReadLaboratoryQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadLaboratoryResponse = response.value;
      this.SET_LABORATORY(readResponse.Laboratory);
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
  public async ListLaboratoriesPublic(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new LaboratoriesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListLaboratoryPublicResponse = response.value;
      this.SET_LABORATORIES_PUBLIC(listResponse.Laboratories);
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
  public async ListLaboratoriesMapPublic(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new LaboratoriesService();
    const response = await service.listMapPublic({});
    if (isLeft(response)) {
      const listResponse: ListLaboratoryMapPublicResponse = response.value;
      this.SET_LABORATORIES_MAP_PUBLIC(listResponse.Laboratories);
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
  public async PublicReadLaboratory(id: string): Promise<void> {
    const service = new LaboratoriesService();
    const query: ReadLaboratoryPublicQuery = { Id: id };
    const response = await service.readPublic(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadLaboratoryPublicResponse = response.value;
      this.SET_LABORATORY_PUBLIC(readResponse.Laboratory);
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
  public async UpdateLaboratory(): Promise<void> {
    AppModule.ShowLoading();
    const service = new LaboratoriesService();
    const laboratory: Laboratory | undefined = this.Laboratory;
    if (!laboratory) {
      this.SET_ERROR({
        message:
          "LaboratoriesStore: not expecting laboratory to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(laboratory);
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
      this.SET_LABORATORY(laboratory);
      AppModule.SetSuccessNotifications("Laboratory successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteLaboratory(): Promise<void> {
    AppModule.ShowLoading();
    const service = new LaboratoriesService();
    const laboratory: Laboratory | undefined = this.Laboratory;
    if (!laboratory) {
      this.SET_ERROR({
        message:
          "LaboratoriesStore: not expecting laboratory to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(laboratory);
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
      const deleteLaboratoryResponse: DeleteLaboratoryResponse = response.value;
      this.SET_LABORATORY(undefined);
      AppModule.SetSuccessNotifications("Laboratory successfully deleted");
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

  get Laboratory(): Laboratory | undefined {
    return this.laboratory.value;
  }

  get Laboratories(): Laboratory[] {
    return this.laboratories.value ?? new Array<Laboratory>();
  }

  get LaboratoriesMap(): LaboratoryMap[] {
    return this.laboratoriesMap.value ?? new Array<LaboratoryMap>();
  }

  get LaboratoryPublic(): LaboratoryPublic | undefined {
    return this.laboratoryPublic.value;
  }

  get LaboratoriesPublic(): LaboratoryPublic[] {
    return this.laboratoriesPublic.value ?? new Array<LaboratoryPublic>();
  }

  get LaboratoriesMapPublic(): LaboratoryMapPublic[] {
    return this.laboratoriesMapPublic.value ?? new Array<LaboratoryMapPublic>();
  }

  get LaboratoryCreate(): Laboratory {
    return this.laboratoryCreate.value;
  }

  get emptyLaboratory(): Laboratory {
    return Object.create({
      Id: "",
      Name: "",
      Abbreviation: "",
      IsActive: false,
      Description: "",
      Address: "",
      Latitude: 0.0,
      Longitude: 0.0,
      BSLLevelId: "",
      CountryId: "",
      IsPublicFacing: false,
    });
  }
}

export const LaboratoryModule = getModule(LaboratoryStore, store);
