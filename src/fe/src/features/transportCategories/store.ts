import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { TransportCategory } from "@/models/TransportCategory";
import { TransportCategoriesService } from "@/services/transportCategories/TransportCategoriesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { TransportCategories } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListTransportCategoryResponse } from "@/services/transportCategories/models/ListTransportCategory";
import { CreateTransportCategoryResponse } from "@/services/transportCategories/models/CreateTransportCategory";
import { DeleteTransportCategoryResponse } from "@/services/transportCategories/models/DeleteTransportCategory";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface TransportCategoryState {
  TransportCategoryCreate: TransportCategory | undefined;
  TransportCategory: TransportCategory | undefined;
  TransportCategories: Array<TransportCategory>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "TransportCategories",
  store: store,
})
class TransportCategoryStore
  extends VuexModule
  implements TransportCategoryState
{
  // Private variables
  private transportCategoryCreate: { value: TransportCategory } = {
    value: this.emptyTransportCategory,
  };

  private transportCategory: { value: TransportCategory | undefined } = {
    value: undefined,
  };

  private transportCategories: { value: Array<TransportCategory> } = {
    value: TransportCategories,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_TRANSPORTCATEGORY_CREATE(
    TransportCategory: TransportCategory
  ): void {
    this.transportCategoryCreate.value = TransportCategory;
  }

  // Details - Edit
  @Mutation
  public SET_TRANSPORTCATEGORY(
    TransportCategory: TransportCategory | undefined
  ): void {
    this.transportCategory.value = TransportCategory;
  }

  @Mutation
  public CLEAR_TRANSPORTCATEGORY(): void {
    this.transportCategory.value = undefined;
  }

  // List
  @Mutation
  public SET_TRANSPORTCATEGORIES(
    TransportCategories: Array<TransportCategory>
  ): void {
    this.transportCategories.value = TransportCategories;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateTransportCategory(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TransportCategoriesService();
    const TransportCategory = this.transportCategoryCreate.value;
    if (TransportCategory === undefined) {
      this.SET_ERROR({
        message:
          "TransportCategoriesStore: not expecting TransportCategory to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(TransportCategory);
    if (isLeft(response)) {
      const createResponse: CreateTransportCategoryResponse = response.value;
      TransportCategory.Id = createResponse.Id;
      this.SET_TRANSPORTCATEGORY(TransportCategory);
      this.SET_TRANSPORTCATEGORY_CREATE(this.emptyTransportCategory);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListTransportCategories(): Promise<void> {
    const service = new TransportCategoriesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListTransportCategoryResponse = response.value;
      this.SET_TRANSPORTCATEGORIES(listResponse.TransportCategories);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListTransportCategoriesPublic(): Promise<void> {
    const service = new TransportCategoriesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListTransportCategoryResponse = response.value;
      this.SET_TRANSPORTCATEGORIES(listResponse.TransportCategories);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateTransportCategory(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TransportCategoriesService();
    const TransportCategory: TransportCategory | undefined =
      this.TransportCategory;
    if (!TransportCategory) {
      this.SET_ERROR({
        message:
          "TransportCategoriesStore: not expecting TransportCategory to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(TransportCategory);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteTransportCategory(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TransportCategoriesService();
    const TransportCategory: TransportCategory | undefined =
      this.TransportCategory;
    if (!TransportCategory) {
      this.SET_ERROR({
        message:
          "TransportCategoriesStore: not expecting TransportCategory to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(TransportCategory);
    if (isLeft(response)) {
      const deleteTransportCategoryResponse: DeleteTransportCategoryResponse =
        response.value;
      this.SET_TRANSPORTCATEGORY(undefined);
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

  get TransportCategory(): TransportCategory | undefined {
    return this.transportCategory.value;
  }

  get TransportCategories(): TransportCategory[] {
    return this.transportCategories.value ?? new Array<TransportCategory>();
  }

  get TransportCategoryCreate(): TransportCategory {
    return this.transportCategoryCreate.value;
  }

  get emptyTransportCategory(): TransportCategory {
    return Object.create({
      Id: "",
      Name: "",
      HexColor: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const TransportCategoryModule = getModule(TransportCategoryStore, store);
