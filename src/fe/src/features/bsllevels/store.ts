import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { BSLLevel } from "@/models/BSLLevel";
import { BSLLevelsService } from "@/services/bslLevels/BSLLevelsService";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { BSLLevels } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListBSLLevelResponse } from "@/services/bslLevels/models/ListBSLLevel";
import { CreateBSLLevelResponse } from "@/services/bslLevels/models/CreateBSLLevel";
import { DeleteBSLLevelResponse } from "@/services/bslLevels/models/DeleteBSLLevel";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadBSLLevelQuery } from "@/services/bslLevels/models/ReadBSLLevel";
import { ReadBSLLevelResponse } from "@/services/bslLevels/models/ReadBSLLevel";
import { AppModule } from "@/store/MainStore";

export interface BSLLevelState {
  BSLLevelCreate: BSLLevel | undefined;
  BSLLevel: BSLLevel | undefined;
  BSLLevels: Array<BSLLevel>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "BSLLevels",
  store: store,
})
class BSLLevelStore extends VuexModule implements BSLLevelState {
  // Private variables
  private bslLevelCreate: { value: BSLLevel } = {
    value: this.emptyBSLLevel,
  };

  private bslLevel: { value: BSLLevel | undefined } = {
    value: undefined,
  };

  private bslLevels: { value: Array<BSLLevel> } = {
    value: BSLLevels,
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
  public SET_BSLLEVEL_CREATE(BSLLevel: BSLLevel): void {
    this.bslLevelCreate.value = BSLLevel;
  }

  // Details - Edit
  @Mutation
  public SET_BSLLEVEL(BSLLevel: BSLLevel | undefined): void {
    this.bslLevel.value = BSLLevel;
  }

  @Mutation
  public CLEAR_BSLLEVEL(): void {
    this.bslLevel.value = undefined;
  }

  @Mutation
  public CLEAR_BSLLEVEL_CREATE(): void {
    this.bslLevel.value = Object.create({
      Id: "",
      Name: "",
      Code: "",
      Description: "",
    });
  }

  // List
  @Mutation
  public SET_BSLLEVELS(BSLLevels: Array<BSLLevel>): void {
    this.bslLevels.value = BSLLevels;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateBSLLevel(): Promise<void> {
    AppModule.ShowLoading();
    const service = new BSLLevelsService();
    const BSLLevel = this.bslLevelCreate.value;
    if (BSLLevel === undefined) {
      this.SET_ERROR({
        message:
          "BSLLevelsStore: not expecting BSLLevel to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(BSLLevel);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateBSLLevelResponse = response.value;
      BSLLevel.Id = createResponse.Id;
      this.SET_BSLLEVEL(BSLLevel);
      this.SET_BSLLEVEL_CREATE(this.emptyBSLLevel);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListBSLLevels(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new BSLLevelsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListBSLLevelResponse = response.value;
      this.SET_BSLLEVELS(listResponse.BSLLevels);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ReadBSLLevel(id: string): Promise<void> {
    const service = new BSLLevelsService();
    const query: ReadBSLLevelQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadBSLLevelResponse = response.value;
      this.SET_BSLLEVEL(readResponse.BSLLevel);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateBSLLevel(): Promise<void> {
    AppModule.ShowLoading();
    const service = new BSLLevelsService();
    const bslLevel: BSLLevel | undefined = this.BSLLevel;
    if (!bslLevel) {
      this.SET_ERROR({
        message:
          "BSLLevelsStore: not expecting BSLLevel to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(bslLevel);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_BSLLEVEL(bslLevel);
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteBSLLevel(): Promise<void> {
    AppModule.ShowLoading();
    const service = new BSLLevelsService();
    const BSLLevel: BSLLevel | undefined = this.BSLLevel;
    if (!BSLLevel) {
      this.SET_ERROR({
        message:
          "BSLLevelsStore: not expecting BSLLevel to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(BSLLevel);
    if (isLeft(response)) {
      const deleteBSLLevelResponse: DeleteBSLLevelResponse = response.value;
      this.SET_BSLLEVEL(undefined);
      AppModule.HideLoading();
      return;
    }

    const error = (response as Right<CommunicationError>).value;
    this.SET_ERROR(error as AppError);
    AppModule.HideLoading();
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get BSLLevel(): BSLLevel | undefined {
    return this.bslLevel.value;
  }

  get BSLLevels(): BSLLevel[] {
    return this.bslLevels.value ?? new Array<BSLLevel>();
  }

  get BSLLevelCreate(): BSLLevel {
    return this.bslLevelCreate.value;
  }

  get emptyBSLLevel(): BSLLevel {
    return Object.create({
      Id: "",
      Name: "",
      Code: "",
      Description: "",
    });
  }
}

export const BSLLevelModule = getModule(BSLLevelStore, store);
