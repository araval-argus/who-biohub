import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { BioHubFacility } from "@/models/BioHubFacility";
import { BioHubFacilitiesService } from "@/services/bioHubFacilities/BioHubFacilitiesService";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { bioHubFacilities } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListBioHubFacilityResponse } from "@/services/bioHubFacilities/models/ListBioHubFacility";
import { CreateBioHubFacilityResponse } from "@/services/bioHubFacilities/models/CreateBioHubFacility";
import { DeleteBioHubFacilityResponse } from "@/services/bioHubFacilities/models/DeleteBioHubFacility";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadBioHubFacilityQuery } from "@/services/bioHubFacilities/models/ReadBioHubFacility";
import { ReadBioHubFacilityResponse } from "@/services/bioHubFacilities/models/ReadBioHubFacility";
import { AppModule } from "../../store/MainStore";
import { ListBioHubFacilityPublicResponse } from "@/services/bioHubFacilities/models/ListBioHubFacilityPublic";
import { ReadBioHubFacilityPublicResponse } from "@/services/bioHubFacilities/models/ReadBioHubFacilityPublic";
import { ReadBioHubFacilityPublicQuery } from "@/services/bioHubFacilities/models/ReadBioHubFacilityPublic";
import { bioHubFacilitiesPublic } from "./mock";
import { bioHubFacilitiesMapPublic } from "./mock";
import { BioHubFacilityPublic } from "@/models/BioHubFacilityPublic";
import { BioHubFacilityMapPublic } from "@/models/BioHubFacilityMapPublic";
import { ListBioHubFacilityMapPublicResponse } from "@/services/bioHubFacilities/models/ListBioHubFacilityMapPublic";
import { bioHubFacilitiesMap } from "./mock";
import { BioHubFacilityMap } from "@/models/BioHubFacilityMap";
import { ListBioHubFacilityMapResponse } from "@/services/bioHubFacilities/models/ListBioHubFacilityMap";

export interface BioHubFacilityState {
  BioHubFacilityCreate: BioHubFacility | undefined;
  BioHubFacility: BioHubFacility | undefined;
  BioHubFacilities: Array<BioHubFacility>;
  BioHubFacilitiesMap: Array<BioHubFacilityMap>;
  BioHubFacilityPublic: BioHubFacilityPublic | undefined;
  BioHubFacilitiesPublic: Array<BioHubFacilityPublic>;
  BioHubFacilitiesMapPublic: Array<BioHubFacilityMapPublic>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "bioHubFacilities",
  store: store,
})
class BioHubFacilityStore extends VuexModule implements BioHubFacilityState {
  // Private variables
  private bioHubFacilityCreate: { value: BioHubFacility } = {
    value: this.emptyBioHubFacility,
  };

  private bioHubFacility: { value: BioHubFacility | undefined } = {
    value: undefined,
  };

  private bioHubFacilities: { value: Array<BioHubFacility> } = {
    value: bioHubFacilities,
  };

  private bioHubFacilitiesMap: { value: Array<BioHubFacilityMap> } = {
    value: bioHubFacilitiesMap,
  };

  private bioHubFacilityPublic: { value: BioHubFacilityPublic | undefined } = {
    value: undefined,
  };

  private bioHubFacilitiesPublic: { value: Array<BioHubFacilityPublic> } = {
    value: bioHubFacilitiesPublic,
  };

  private bioHubFacilitiesMapPublic: { value: Array<BioHubFacilityMapPublic> } =
    {
      value: bioHubFacilitiesMapPublic,
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
  public SET_BIOHUBFACILITY_CREATE(bioHubFacility: BioHubFacility): void {
    this.bioHubFacilityCreate.value = bioHubFacility;
  }

  // Details - Edit
  @Mutation
  public SET_BIOHUBFACILITY(bioHubFacility: BioHubFacility | undefined): void {
    this.bioHubFacility.value = bioHubFacility;
  }

  @Mutation
  public CLEAR_BIOHUBFACILITY(): void {
    this.bioHubFacility.value = undefined;
  }

  @Mutation
  public CLEAR_BIOHUBFACILITY_CREATE(): void {
    this.bioHubFacilityCreate.value = Object.create({
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
  public SET_BIOHUBFACILITIES(bioHubFacilities: Array<BioHubFacility>): void {
    this.bioHubFacilities.value = bioHubFacilities;
  }

  @Mutation
  public SET_BIOHUBFACILITIES_MAP(
    bioHubFacilities: Array<BioHubFacilityMap>
  ): void {
    this.bioHubFacilitiesMap.value = bioHubFacilities;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateBioHubFacility(): Promise<void> {
    AppModule.ShowLoading();
    const service = new BioHubFacilitiesService();
    const bioHubFacility = this.bioHubFacilityCreate.value;
    if (bioHubFacility === undefined) {
      this.SET_ERROR({
        message:
          "BioHubFacilitiesStore: not expecting bioHubFacility to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(bioHubFacility);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateBioHubFacilityResponse = response.value;
      bioHubFacility.Id = createResponse.Id;
      this.SET_BIOHUBFACILITY(bioHubFacility);
      this.SET_BIOHUBFACILITY_CREATE(this.emptyBioHubFacility);
      AppModule.SetSuccessNotifications("BioHub Facility successfully created");

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
  public async ListBioHubFacilities(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new BioHubFacilitiesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListBioHubFacilityResponse = response.value;
      this.SET_BIOHUBFACILITIES(listResponse.BioHubFacilities);
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
  public async ListBioHubFacilitiesMap(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new BioHubFacilitiesService();
    const response = await service.listMap({});
    if (isLeft(response)) {
      const listResponse: ListBioHubFacilityMapResponse = response.value;
      this.SET_BIOHUBFACILITIES_MAP(listResponse.BioHubFacilities);
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
  public async ReadBioHubFacility(id: string): Promise<void> {
    const service = new BioHubFacilitiesService();
    const query: ReadBioHubFacilityQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadBioHubFacilityResponse = response.value;
      this.SET_BIOHUBFACILITY(readResponse.BioHubFacility);
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
  public async ListBioHubFacilitiesPublic(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new BioHubFacilitiesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListBioHubFacilityPublicResponse = response.value;
      this.SET_BIOHUBFACILITIES_PUBLIC(listResponse.BioHubFacilities);
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
  public async ListBioHubFacilitiesMapPublic(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new BioHubFacilitiesService();
    const response = await service.listMapPublic({});
    if (isLeft(response)) {
      const listResponse: ListBioHubFacilityMapPublicResponse = response.value;
      this.SET_BIOHUBFACILITIES_MAP_PUBLIC(listResponse.BioHubFacilities);
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
  public async ReadBioHubFacilityPublic(id: string): Promise<void> {
    const service = new BioHubFacilitiesService();
    const query: ReadBioHubFacilityPublicQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadBioHubFacilityPublicResponse = response.value;
      this.SET_BIOHUBFACILITY_PUBLIC(readResponse.BioHubFacility);
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
  public async UpdateBioHubFacility(): Promise<void> {
    AppModule.ShowLoading();
    const service = new BioHubFacilitiesService();
    const bioHubFacility: BioHubFacility | undefined = this.BioHubFacility;
    if (!bioHubFacility) {
      this.SET_ERROR({
        message:
          "BioHubFacilitiesStore: not expecting bioHubFacility to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(bioHubFacility);
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
      this.SET_BIOHUBFACILITY(bioHubFacility);
      AppModule.SetSuccessNotifications("BioHub Facility successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteBioHubFacility(): Promise<void> {
    AppModule.ShowLoading();
    const service = new BioHubFacilitiesService();
    const bioHubFacility: BioHubFacility | undefined = this.BioHubFacility;
    if (!bioHubFacility) {
      this.SET_ERROR({
        message:
          "BioHubFacilitiesStore: not expecting bioHubFacility to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(bioHubFacility);
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
      const deleteBioHubFacilityResponse: DeleteBioHubFacilityResponse =
        response.value;
      this.SET_BIOHUBFACILITY(undefined);
      AppModule.SetSuccessNotifications("BioHub Facility successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async PublicListBioHubFacilities(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new BioHubFacilitiesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListBioHubFacilityPublicResponse = response.value;
      this.SET_BIOHUBFACILITIES_PUBLIC(listResponse.BioHubFacilities);
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
  public async PublicReadBioHubFacility(id: string): Promise<void> {
    const service = new BioHubFacilitiesService();
    const query: ReadBioHubFacilityPublicQuery = { Id: id };
    const response = await service.readPublic(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadBioHubFacilityPublicResponse = response.value;
      this.SET_BIOHUBFACILITY_PUBLIC(readResponse.BioHubFacility);
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

  @Mutation
  public SET_BIOHUBFACILITIES_PUBLIC(
    bioHubFacilities: Array<BioHubFacilityPublic>
  ): void {
    this.bioHubFacilitiesPublic.value = bioHubFacilities;
  }

  @Mutation
  public SET_BIOHUBFACILITIES_MAP_PUBLIC(
    bioHubFacilities: Array<BioHubFacilityMapPublic>
  ): void {
    this.bioHubFacilitiesMapPublic.value = bioHubFacilities;
  }

  @Mutation
  public SET_BIOHUBFACILITY_PUBLIC(
    bioHubFacility: BioHubFacilityPublic | undefined
  ): void {
    this.bioHubFacilityPublic.value = bioHubFacility;
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get BioHubFacility(): BioHubFacility | undefined {
    return this.bioHubFacility.value;
  }

  get BioHubFacilitiesPublic(): BioHubFacilityPublic[] {
    return (
      this.bioHubFacilitiesPublic.value ?? new Array<BioHubFacilityPublic>()
    );
  }

  get BioHubFacilitiesMapPublic(): BioHubFacilityMapPublic[] {
    return (
      this.bioHubFacilitiesMapPublic.value ??
      new Array<BioHubFacilityMapPublic>()
    );
  }

  get BioHubFacilityPublic(): BioHubFacilityPublic | undefined {
    return this.bioHubFacilityPublic.value;
  }

  get BioHubFacilities(): BioHubFacility[] {
    return this.bioHubFacilities.value ?? new Array<BioHubFacility>();
  }

  get BioHubFacilitiesMap(): BioHubFacilityMap[] {
    return this.bioHubFacilitiesMap.value ?? new Array<BioHubFacilityMap>();
  }

  get BioHubFacilityCreate(): BioHubFacility {
    return this.bioHubFacilityCreate.value;
  }

  get emptyBioHubFacility(): BioHubFacility {
    return Object.create({
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
}

export const BioHubFacilityModule = getModule(BioHubFacilityStore, store);
