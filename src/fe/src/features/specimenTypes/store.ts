import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { SpecimenType } from "@/models/SpecimenType";
import { SpecimenTypesService } from "@/services/specimenTypes/SpecimenTypesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { SpecimenTypes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListSpecimenTypeResponse } from "@/services/specimenTypes/models/ListSpecimenType";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface SpecimenTypeState {
  SpecimenTypeCreate: SpecimenType | undefined;
  SpecimenType: SpecimenType | undefined;
  SpecimenTypes: Array<SpecimenType>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "SpecimenTypes",
  store: store,
})
class SpecimenTypeStore extends VuexModule implements SpecimenTypeState {
  // Private variables
  private specimenTypeCreate: { value: SpecimenType } = {
    value: this.emptySpecimenType,
  };

  private specimenType: { value: SpecimenType | undefined } = {
    value: undefined,
  };

  private specimenTypes: { value: Array<SpecimenType> } = {
    value: SpecimenTypes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_ISOLATIONHOSTTYPE_CREATE(SpecimenType: SpecimenType): void {
    this.specimenTypeCreate.value = SpecimenType;
  }

  // Details - Edit
  @Mutation
  public SET_ISOLATIONHOSTTYPE(SpecimenType: SpecimenType | undefined): void {
    this.specimenType.value = SpecimenType;
  }

  @Mutation
  public CLEAR_ISOLATIONHOSTTYPE(): void {
    this.specimenType.value = undefined;
  }

  // List
  @Mutation
  public SET_SPECIMENTYPES(SpecimenTypes: Array<SpecimenType>): void {
    this.specimenTypes.value = SpecimenTypes;
  }

  @Action({ rawError: true })
  public async ListSpecimenTypes(): Promise<void> {
    const service = new SpecimenTypesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListSpecimenTypeResponse = response.value;
      this.SET_SPECIMENTYPES(listResponse.SpecimenTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  // @Action({ rawError: true })
  // public async ListSpecimenTypesPublic(): Promise<void> {
  //   const service = new SpecimenTypesService();
  //   const response = await service.listPublic({});
  //   if (isLeft(response)) {
  //     const listResponse: ListSpecimenTypeResponse = response.value;
  //     this.SET_ISOLATIONHOSTTYPES(listResponse.SpecimenTypes);
  //     return;
  //   }

  //   this.SET_ERROR(response.value as AppError);
  // }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get SpecimenType(): SpecimenType | undefined {
    return this.specimenType.value;
  }

  get SpecimenTypes(): SpecimenType[] {
    return this.specimenTypes.value ?? new Array<SpecimenType>();
  }

  get SpecimenTypeCreate(): SpecimenType {
    return this.specimenTypeCreate.value;
  }

  get emptySpecimenType(): SpecimenType {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const SpecimenTypeModule = getModule(SpecimenTypeStore, store);
