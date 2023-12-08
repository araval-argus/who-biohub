import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { InternationalTaxonomyClassification } from "@/models/InternationalTaxonomyClassification";
import { InternationalTaxonomyClassificationsService } from "@/services/internationalTaxonomyClassifications/InternationalTaxonomyClassificationsService";
import { isLeft, isRight, Right } from "@/utils/either";
import { InternationalTaxonomyClassifications } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListInternationalTaxonomyClassificationResponse } from "@/services/internationalTaxonomyClassifications/models/ListInternationalTaxonomyClassification";
import { CreateInternationalTaxonomyClassificationResponse } from "@/services/internationalTaxonomyClassifications/models/CreateInternationalTaxonomyClassification";
import { DeleteInternationalTaxonomyClassificationResponse } from "@/services/internationalTaxonomyClassifications/models/DeleteInternationalTaxonomyClassification";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface InternationalTaxonomyClassificationState {
  InternationalTaxonomyClassificationCreate:
    | InternationalTaxonomyClassification
    | undefined;
  InternationalTaxonomyClassification:
    | InternationalTaxonomyClassification
    | undefined;
  InternationalTaxonomyClassifications: Array<InternationalTaxonomyClassification>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "InternationalTaxonomyClassifications",
  store: store,
})
class InternationalTaxonomyClassificationStore
  extends VuexModule
  implements InternationalTaxonomyClassificationState
{
  // Private variables
  private internationalTaxonomyClassificationCreate: {
    value: InternationalTaxonomyClassification;
  } = {
    value: this.emptyInternationalTaxonomyClassification,
  };

  private internationalTaxonomyClassification: {
    value: InternationalTaxonomyClassification | undefined;
  } = {
    value: undefined,
  };

  private internationalTaxonomyClassifications: {
    value: Array<InternationalTaxonomyClassification>;
  } = {
    value: InternationalTaxonomyClassifications,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_INTERNATIONALTAXONOMYCLASSIFICATION_CREATE(
    InternationalTaxonomyClassification: InternationalTaxonomyClassification
  ): void {
    this.internationalTaxonomyClassificationCreate.value =
      InternationalTaxonomyClassification;
  }

  // Details - Edit
  @Mutation
  public SET_INTERNATIONALTAXONOMYCLASSIFICATION(
    InternationalTaxonomyClassification:
      | InternationalTaxonomyClassification
      | undefined
  ): void {
    this.internationalTaxonomyClassification.value =
      InternationalTaxonomyClassification;
  }

  @Mutation
  public CLEAR_INTERNATIONALTAXONOMYCLASSIFICATION(): void {
    this.internationalTaxonomyClassification.value = undefined;
  }

  // List
  @Mutation
  public SET_INTERNATIONALTAXONOMYCLASSIFICATIONS(
    InternationalTaxonomyClassifications: Array<InternationalTaxonomyClassification>
  ): void {
    this.internationalTaxonomyClassifications.value =
      InternationalTaxonomyClassifications;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateInternationalTaxonomyClassification(): Promise<void> {
    AppModule.ShowLoading();
    const service = new InternationalTaxonomyClassificationsService();
    const InternationalTaxonomyClassification =
      this.internationalTaxonomyClassificationCreate.value;
    if (InternationalTaxonomyClassification === undefined) {
      this.SET_ERROR({
        message:
          "InternationalTaxonomyClassificationsStore: not expecting InternationalTaxonomyClassification to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(InternationalTaxonomyClassification);
    if (isLeft(response)) {
      const createResponse: CreateInternationalTaxonomyClassificationResponse =
        response.value;
      InternationalTaxonomyClassification.Id = createResponse.Id;
      this.SET_INTERNATIONALTAXONOMYCLASSIFICATION(
        InternationalTaxonomyClassification
      );
      this.SET_INTERNATIONALTAXONOMYCLASSIFICATION_CREATE(
        this.emptyInternationalTaxonomyClassification
      );
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListInternationalTaxonomyClassifications(): Promise<void> {
    const service = new InternationalTaxonomyClassificationsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListInternationalTaxonomyClassificationResponse =
        response.value;
      this.SET_INTERNATIONALTAXONOMYCLASSIFICATIONS(
        listResponse.InternationalTaxonomyClassifications
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListInternationalTaxonomyClassificationsPublic(): Promise<void> {
    const service = new InternationalTaxonomyClassificationsService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListInternationalTaxonomyClassificationResponse =
        response.value;
      this.SET_INTERNATIONALTAXONOMYCLASSIFICATIONS(
        listResponse.InternationalTaxonomyClassifications
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateInternationalTaxonomyClassification(): Promise<void> {
    AppModule.ShowLoading();
    const service = new InternationalTaxonomyClassificationsService();
    const InternationalTaxonomyClassification:
      | InternationalTaxonomyClassification
      | undefined = this.InternationalTaxonomyClassification;
    if (!InternationalTaxonomyClassification) {
      this.SET_ERROR({
        message:
          "InternationalTaxonomyClassificationsStore: not expecting InternationalTaxonomyClassification to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(InternationalTaxonomyClassification);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteInternationalTaxonomyClassification(): Promise<void> {
    AppModule.ShowLoading();
    const service = new InternationalTaxonomyClassificationsService();
    const InternationalTaxonomyClassification:
      | InternationalTaxonomyClassification
      | undefined = this.InternationalTaxonomyClassification;
    if (!InternationalTaxonomyClassification) {
      this.SET_ERROR({
        message:
          "InternationalTaxonomyClassificationsStore: not expecting InternationalTaxonomyClassification to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(InternationalTaxonomyClassification);
    if (isLeft(response)) {
      const deleteInternationalTaxonomyClassificationResponse: DeleteInternationalTaxonomyClassificationResponse =
        response.value;
      this.SET_INTERNATIONALTAXONOMYCLASSIFICATION(undefined);
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

  get InternationalTaxonomyClassification():
    | InternationalTaxonomyClassification
    | undefined {
    return this.internationalTaxonomyClassification.value;
  }

  get InternationalTaxonomyClassifications(): InternationalTaxonomyClassification[] {
    return (
      this.internationalTaxonomyClassifications.value ??
      new Array<InternationalTaxonomyClassification>()
    );
  }

  get InternationalTaxonomyClassificationCreate(): InternationalTaxonomyClassification {
    return this.internationalTaxonomyClassificationCreate.value;
  }

  get emptyInternationalTaxonomyClassification(): InternationalTaxonomyClassification {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const InternationalTaxonomyClassificationModule = getModule(
  InternationalTaxonomyClassificationStore,
  store
);
