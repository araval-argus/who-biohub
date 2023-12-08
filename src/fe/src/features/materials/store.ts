import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Material } from "@/models/Material";
import { MaterialsService } from "@/services/materials/MaterialsService";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { materials } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListMaterialResponse } from "@/services/materials/models/ListMaterial";
import { CreateMaterialResponse } from "@/services/materials/models/CreateMaterial";
import { DeleteMaterialResponse } from "@/services/materials/models/DeleteMaterial";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadMaterialQuery } from "@/services/materials/models/ReadMaterial";
import { ReadMaterialResponse } from "@/services/materials/models/ReadMaterial";
import { AppModule } from "../../store/MainStore";
import { ListMaterialPublicResponse } from "@/services/materials/models/ListMaterialPublic";
import { ReadMaterialPublicResponse } from "@/services/materials/models/ReadMaterialPublic";
import { ReadMaterialPublicQuery } from "@/services/materials/models/ReadMaterialPublic";
import { materialsPublic } from "./mock";
import { MaterialPublic } from "@/models/MaterialPublic";

import { worklistFromBioHubItemAllMaterials } from "./mock";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";

import { worklistToBioHubItemAllMaterials } from "./mock";
import { WorklistToBioHubItemMaterial } from "@/models/WorklistToBioHubItemMaterial";

import {
  ListMaterialsForWorklistFromBioHubItemQuery,
  ListMaterialsForWorklistFromBioHubItemResponse,
} from "@/services/materials/models/ListMaterialsForWorklistFromBioHubItem";

import {
  ListMaterialsForWorklistToBioHubItemQuery,
  ListMaterialsForWorklistToBioHubItemResponse,
} from "@/services/materials/models/ListMaterialsForWorklistToBioHubItem";

import { MaterialLaboratoryCompletion } from "@/models/MaterialLaboratoryCompletion";
import {
  ReadMaterialForBioHubFacilityCompletionQuery,
  ReadMaterialForBioHubFacilityCompletionResponse,
} from "@/services/materials/models/ReadMaterialForBioHubFacilityCompletion";
import {
  ReadMaterialForLaboratoryCompletionQuery,
  ReadMaterialForLaboratoryCompletionResponse,
} from "@/services/materials/models/ReadMaterialForLaboratoryCompletion";
import { Readiness } from "@/models/enums/Readiness";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import {
  ListMaterialEventQuery,
  ListMaterialEventResponse,
} from "@/services/materials/models/ListMaterialEvent";

export interface MaterialState {
  MaterialCreate: Material | undefined;
  Material: Material | undefined;
  Materials: Array<Material>;
  Error: AppError | undefined;
  MaterialPublic: MaterialPublic | undefined;
  MaterialsPublic: Array<MaterialPublic>;
  MaterialTimeline: WorklistTimeline | undefined;

  MaterialLaboratoryCompletion: MaterialLaboratoryCompletion | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "materials",
  store: store,
})
class MaterialStore extends VuexModule implements MaterialState {
  // Private variables
  private materialCreate: { value: Material } = {
    value: this.emptyMaterial,
  };

  private material: { value: Material | undefined } = {
    value: undefined,
  };

  private materials: { value: Array<Material> } = {
    value: materials,
  };

  private materialPublic: { value: MaterialPublic | undefined } = {
    value: undefined,
  };

  private materialsPublic: { value: Array<MaterialPublic> } = {
    value: materialsPublic,
  };

  private worklistFromBioHubItemAllMaterials: {
    value: Array<WorklistFromBioHubItemMaterial>;
  } = { value: worklistFromBioHubItemAllMaterials };

  private worklistToBioHubItemAllMaterials: {
    value: Array<WorklistToBioHubItemMaterial>;
  } = { value: worklistToBioHubItemAllMaterials };

  private materialLaboratoryCompletion: {
    value: MaterialLaboratoryCompletion | undefined;
  } = {
    value: undefined,
  };

  private temporaryMaterialGSDInfo: {
    value: MaterialGSDInfo | undefined;
  } = {
    value: undefined,
  };

  private materialTimeline: { value: WorklistTimeline | undefined } = {
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
  public SET_MATERIAL_CREATE(material: Material): void {
    this.materialCreate.value = material;
  }

  // Details - Edit
  @Mutation
  public SET_MATERIAL(material: Material | undefined): void {
    this.material.value = material;
  }

  @Mutation
  public SET_MATERIAL_LABORATORY_COMPLETION(
    material: MaterialLaboratoryCompletion | undefined
  ): void {
    this.materialLaboratoryCompletion.value = material;
  }

  @Mutation
  public CLEAR_MATERIAL(): void {
    this.material.value = undefined;
  }

  @Mutation
  public CLEAR_MATERIAL_PUBLIC(): void {
    this.materialPublic.value = undefined;
  }

  @Mutation
  public CLEAR_MATERIAL_LABORATORY_COMPLETION(): void {
    this.materialLaboratoryCompletion.value = undefined;
  }

  @Mutation
  public CLEAR_MATERIAL_CREATE(): void {
    this.material.value = Object.create({
      Id: "",
      ReferenceNumber: "",
      Name: "",
      Description: "",
      Temperature: 0,
      SampleId: "",
      Lineage: "",
      Variant: "",
      VariantAssessment: "",
      StrainDesignation: "",
      Genotype: "",
      Serotype: "",
      DatabaseAccessionId: "",
      OriginalGeneticSequence: "",
      GSDCulturedMaterialCellLine1: "",
      GSDCulturedMaterialCellLine2: "",
      FacilityGSD: "",
      GMO: false,
      // ProductionCellLine: "",
      Infectivity: "",
      ViralTiter: "",
      IsNew: false,
      TypeId: "",
      SuspectedEpidemiologicalOriginId: "",
      ProductTypeId: "",
      TransportCategoryId: "",
      UnitOfMeasureId: "",
      UsagePermissionId: "",
      GeneticSequenceDataId: "",
      InternationalTaxonomyClassificationId: "",
      IsolationHostTypeId: "",
      CultivabilityTypeId: "",
      IsolationTechniqueTypeId: "",
      ProviderLaboratoryId: null,
      ProviderMaterialId: null,
      ReadyToShare: true,
      ShipmentNumberOfVials: 0,
      CurrentNumberOfVials: 0,
      WarningEmailCurrentNumberOfVialsThreshold: 10,
      NumberOfVialsToAdd: 0,
      BHFShareReadiness: Readiness.Ready,
      PublicShare: YesNoOption.No,
    });
  }

  // List
  @Mutation
  public SET_MATERIALS(materials: Array<Material>): void {
    this.materials.value = materials;
  }

  @Mutation
  public SET_MATERIAL_EVENTS(materialTimeline: WorklistTimeline): void {
    this.materialTimeline.value = materialTimeline;
  }

  @Action({ rawError: true })
  public async ListMaterialEvents(Id: string): Promise<void> {
    this.SET_ERROR(undefined);
    const query: ListMaterialEventQuery = {
      Id: Id,
    };
    const service = new MaterialsService();
    const response = await service.listEvents(query);
    if (isLeft(response)) {
      const listResponse: ListMaterialEventResponse = response.value;
      this.SET_MATERIAL_EVENTS(listResponse.MaterialTimeline);
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

  // Actions
  @Action({ rawError: true })
  public async CreateMaterial(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialsService();
    const material = this.materialCreate.value;
    if (material === undefined) {
      this.SET_ERROR({
        message:
          "MaterialsStore: not expecting material to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(material);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateMaterialResponse = response.value;
      material.Id = createResponse.Id;
      this.SET_MATERIAL(material);
      this.SET_MATERIAL_CREATE(this.emptyMaterial);
      AppModule.SetSuccessNotifications("Material successfully created");
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
  public async ListMaterials(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new MaterialsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListMaterialResponse = response.value;
      this.SET_MATERIALS(listResponse.Materials);
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
  public async ReadMaterial(id: string): Promise<void> {
    const service = new MaterialsService();
    const query: ReadMaterialQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadMaterialResponse = response.value;
      this.SET_MATERIAL(readResponse.Material);
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
  public async ReadMaterialForLaboratoryCompletion(id: string): Promise<void> {
    const service = new MaterialsService();
    const query: ReadMaterialForLaboratoryCompletionQuery = { Id: id };
    const response = await service.readForLaboratoryCompletion(query);
    if (isLeft(response)) {
      const readResponse: ReadMaterialForLaboratoryCompletionResponse =
        response.value;
      this.SET_MATERIAL_LABORATORY_COMPLETION(readResponse.Material);
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
  public async ReadMaterialForBioHubFacilityCompletion(
    id: string
  ): Promise<void> {
    const service = new MaterialsService();
    const query: ReadMaterialForBioHubFacilityCompletionQuery = { Id: id };
    const response = await service.readForBioHubFacilityCompletion(query);
    if (isLeft(response)) {
      const readResponse: ReadMaterialForBioHubFacilityCompletionResponse =
        response.value;
      this.SET_MATERIAL(readResponse.Material);
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
  public async ListMaterialsPublic(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new MaterialsService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListMaterialPublicResponse = response.value;
      this.SET_MATERIALS_PUBLIC(listResponse.Materials);
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
  public async ReadMaterialPublic(id: string): Promise<void> {
    const service = new MaterialsService();
    const query: ReadMaterialPublicQuery = { Id: id };
    const response = await service.readPublic(query);
    if (isLeft(response)) {
      const readResponse: ReadMaterialPublicResponse = response.value;
      this.SET_MATERIAL_PUBLIC(readResponse.Material);
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
  public SET_MATERIALS_PUBLIC(materials: Array<MaterialPublic>): void {
    this.materialsPublic.value = materials;
  }

  @Mutation
  public SET_MATERIAL_PUBLIC(material: MaterialPublic | undefined): void {
    this.materialPublic.value = material;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMALLMATERIALS(
    worklistFromBioHubItemAllMaterials: Array<WorklistFromBioHubItemMaterial>
  ): void {
    this.worklistFromBioHubItemAllMaterials.value =
      worklistFromBioHubItemAllMaterials;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEMALLMATERIALS(
    worklistToBioHubItemAllMaterials: Array<WorklistToBioHubItemMaterial>
  ): void {
    this.worklistToBioHubItemAllMaterials.value =
      worklistToBioHubItemAllMaterials;
  }

  // @Mutation
  // public SET_MATERIAL_LABORATORY_COMPLETION_APPROVE(approve: boolean): void {
  //   this.materialLaboratoryCompletion.value?.Approve = approve;
  // }

  @Mutation
  public SET_NEW_TEMPORARY_MATERIAL_GSD_INFO(): void {
    this.temporaryMaterialGSDInfo.value = Object.create({
      Id: crypto.randomUUID(),
      MaterialId: "",
      CellLine: "",
      GSDFasta: "",
      GSDType: null,
      PassageNumber: 0,
    }) as MaterialGSDInfo;
  }

  @Mutation
  public ADD_TEMPORARY_MATERIAL_GSD_INFO(): void {
    const temporaryMaterialGSDInfo = this.temporaryMaterialGSDInfo.value;

    if (temporaryMaterialGSDInfo !== undefined) {
      const MaterialGSDInfo = Object.assign({
        Id: temporaryMaterialGSDInfo.Id,
        MaterialId: temporaryMaterialGSDInfo.MaterialId,
        CellLine: temporaryMaterialGSDInfo.CellLine,
        GSDFasta: temporaryMaterialGSDInfo.GSDFasta,
        GSDType: temporaryMaterialGSDInfo.GSDType,
        PassageNumber: temporaryMaterialGSDInfo.PassageNumber,
      }) as MaterialGSDInfo;

      this.material.value?.MaterialGSDInfo.push(MaterialGSDInfo);

      this.temporaryMaterialGSDInfo.value = undefined;
    }
  }

  @Mutation
  public ADD_TEMPORARY_MATERIAL_LABORATORY_COMPLETION_GSD_INFO(): void {
    const temporaryMaterialGSDInfo = this.temporaryMaterialGSDInfo.value;

    if (temporaryMaterialGSDInfo !== undefined) {
      const MaterialGSDInfo = Object.assign({
        Id: temporaryMaterialGSDInfo.Id,
        MaterialId: temporaryMaterialGSDInfo.MaterialId,
        CellLine: temporaryMaterialGSDInfo.CellLine,
        GSDFasta: temporaryMaterialGSDInfo.GSDFasta,
        GSDType: temporaryMaterialGSDInfo.GSDType,
        PassageNumber: temporaryMaterialGSDInfo.PassageNumber,
      }) as MaterialGSDInfo;

      this.materialLaboratoryCompletion.value?.MaterialGSDInfo.push(
        MaterialGSDInfo
      );

      this.temporaryMaterialGSDInfo.value = undefined;
    }
  }

  @Mutation
  public CLEAR_TEMPORARY_MATERIAL_GSD_INFO(): void {
    this.temporaryMaterialGSDInfo.value = Object.create({
      Id: "",
      MaterialId: "",
      CellLine: "",
      GSDFasta: "",
      GSDType: null,
      PassageNumber: 0,
    }) as MaterialGSDInfo;
  }

  @Mutation
  public SET_TEMPORARY_MATERIAL_GSD_INFO(
    item: MaterialGSDInfo | undefined
  ): void {
    this.temporaryMaterialGSDInfo.value = item;
  }

  @Mutation
  public REMOVE_MATERIAL_GSD_INFO(id: string): void {
    const array = this.material.value?.MaterialGSDInfo;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(id);
      if (index !== -1) {
        array.splice(index, 1);
      }
    }
  }

  @Mutation
  public REMOVE_MATERIAL_LABORATORY_COMPLETION_GSD_INFO(id: string): void {
    const array = this.materialLaboratoryCompletion.value?.MaterialGSDInfo;
    if (array !== undefined) {
      const index = array
        .map(function (e) {
          return e.Id;
        })
        .indexOf(id);
      if (index !== -1) {
        array.splice(index, 1);
      }
    }
  }

  @Action({ rawError: true })
  public async UpdateMaterial(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialsService();
    const material: Material | undefined = this.Material;
    if (!material) {
      this.SET_ERROR({
        message:
          "MaterialsStore: not expecting material to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(material);
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
      AppModule.SetSuccessNotifications("Material successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async UpdateMaterialForLaboratoryCompletion(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialsService();
    const material: MaterialLaboratoryCompletion | undefined =
      this.MaterialLaboratoryCompletion;
    if (!material) {
      this.SET_ERROR({
        message:
          "MaterialsStore: not expecting material to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updateForLaboratoryCompletion(material);
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
      AppModule.SetSuccessNotifications("Material successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async UpdateMaterialForBioHubFacilityCompletion(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialsService();
    const material: Material | undefined = this.Material;
    if (!material) {
      this.SET_ERROR({
        message:
          "MaterialsStore: not expecting material to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updateForBioHubFacilityCompletion(material);
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
      AppModule.SetSuccessNotifications("Material successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteMaterial(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialsService();
    const material: Material | undefined = this.Material;
    if (!material) {
      this.SET_ERROR({
        message:
          "MaterialsStore: not expecting material to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(material);
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
      const deleteMaterialResponse: DeleteMaterialResponse = response.value;

      this.SET_MATERIAL(undefined);
      AppModule.SetSuccessNotifications("Material successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListMaterialsForWorklistFromBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new MaterialsService();
    const worklistFromBioHubItemId = info.get("WorklistFromBioHubItemId");
    const bioHubFacilityId = info.get("BioHubFacilityId");

    const query: ListMaterialsForWorklistFromBioHubItemQuery = {
      WorklistFromBioHubItemId:
        worklistFromBioHubItemId !== undefined ? worklistFromBioHubItemId : "",
      BioHubFacilityId: bioHubFacilityId !== undefined ? bioHubFacilityId : "",
    };
    const response = await service.listMaterialsForWorklistFromBioHubItem(
      query
    );

    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListMaterialsForWorklistFromBioHubItemResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBITEMALLMATERIALS(
        readResponse.WorklistFromBioHubItemMaterials
      );
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
  public async ListMaterialsForWorklistToBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new MaterialsService();
    const worklistToBioHubItemId = info.get("WorklistToBioHubItemId");

    const query: ListMaterialsForWorklistToBioHubItemQuery = {
      WorklistToBioHubItemId:
        worklistToBioHubItemId !== undefined ? worklistToBioHubItemId : "",
    };
    const response = await service.listMaterialsForWorklistToBioHubItem(query);

    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListMaterialsForWorklistToBioHubItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEMALLMATERIALS(
        readResponse.WorklistToBioHubItemMaterials
      );
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

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get Material(): Material | undefined {
    return this.material.value;
  }

  get MaterialLaboratoryCompletion(): MaterialLaboratoryCompletion | undefined {
    return this.materialLaboratoryCompletion.value;
  }

  get Materials(): Material[] {
    return this.materials.value ?? new Array<Material>();
  }

  get MaterialCreate(): Material {
    return this.materialCreate.value;
  }

  get MaterialsPublic(): MaterialPublic[] {
    return this.materialsPublic.value ?? new Array<MaterialPublic>();
  }

  get MaterialPublic(): MaterialPublic | undefined {
    return this.materialPublic.value;
  }

  get WorklistFromBioHubItemAllMaterials(): Array<WorklistFromBioHubItemMaterial> {
    return this.worklistFromBioHubItemAllMaterials.value;
  }

  get WorklistToBioHubItemAllMaterials(): Array<WorklistToBioHubItemMaterial> {
    return this.worklistToBioHubItemAllMaterials.value;
  }

  get TemporaryMaterialGSDInfo(): MaterialGSDInfo | undefined {
    return this.temporaryMaterialGSDInfo?.value;
  }

  get MaterialGSDInfo(): Array<MaterialGSDInfo> {
    return this.material.value?.MaterialGSDInfo ?? new Array<MaterialGSDInfo>();
  }

  get MaterialLaboratoryCompletionGSDInfo(): Array<MaterialGSDInfo> {
    return (
      this.materialLaboratoryCompletion.value?.MaterialGSDInfo ??
      new Array<MaterialGSDInfo>()
    );
  }

  get MaterialTimeline(): WorklistTimeline | undefined {
    return this.materialTimeline.value;
  }

  get emptyMaterial(): Material {
    return Object.create({
      Id: "",
      ReferenceNumber: "",
      Name: "",
      Description: "",
      Temperature: 0,
      SampleId: "",
      Lineage: "",
      Variant: "",
      VariantAssessment: "",
      StrainDesignation: "",
      Genotype: "",
      Serotype: "",
      DatabaseAccessionId: "",
      OriginalGeneticSequence: "",
      GSDCulturedMaterialCellLine1: "",
      GSDCulturedMaterialCellLine2: "",
      FacilityGSD: "",
      GMO: false,
      //ProductionCellLine: "",
      Infectivity: "",
      ViralTiter: "",
      IsNew: false,
      TypeId: "",
      SuspectedEpidemiologicalOriginId: "",
      ProductTypeId: "",
      TransportCategoryId: "",
      UnitOfMeasureId: "",
      UsagePermissionId: "",
      GeneticSequenceDataId: "",
      InternationalTaxonomyClassificationId: "",
      IsolationHostTypeId: "",
      CultivabilityTypeId: "",
      IsolationTechniqueTypeId: "",
      ProviderLaboratoryId: "d328a70c-ce70-4184-a96f-eb43fab91cbe",
      ProviderMaterialId: null,
      ShipmentNumberOfVials: 0,
      CurrentNumberOfVials: 0,
      WarningEmailCurrentNumberOfVialsThreshold: 10,
      NumberOfVialsToAdd: 0,
      BHFShareReadiness: Readiness.NotReady,
      PublicShare: YesNoOption.No,
    });
  }
}

export const MaterialModule = getModule(MaterialStore, store);
