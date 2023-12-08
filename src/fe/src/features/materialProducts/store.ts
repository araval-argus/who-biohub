import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { MaterialProduct } from "@/models/MaterialProduct";
import { MaterialProductsService } from "@/services/materialProducts/MaterialProductsService";
import { isLeft, isRight, Right } from "@/utils/either";
import { MaterialProducts } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListMaterialProductResponse } from "@/services/materialProducts/models/ListMaterialProduct";
import { CreateMaterialProductResponse } from "@/services/materialProducts/models/CreateMaterialProduct";
import { DeleteMaterialProductResponse } from "@/services/materialProducts/models/DeleteMaterialProduct";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface MaterialProductState {
  MaterialProductCreate: MaterialProduct | undefined;
  MaterialProduct: MaterialProduct | undefined;
  MaterialProducts: Array<MaterialProduct>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "MaterialProducts",
  store: store,
})
class MaterialProductStore extends VuexModule implements MaterialProductState {
  // Private variables
  private materialProductCreate: { value: MaterialProduct } = {
    value: this.emptyMaterialProduct,
  };

  private materialProduct: { value: MaterialProduct | undefined } = {
    value: undefined,
  };

  private materialProducts: { value: Array<MaterialProduct> } = {
    value: MaterialProducts,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_MATERIALPRODUCT_CREATE(MaterialProduct: MaterialProduct): void {
    this.materialProductCreate.value = MaterialProduct;
  }

  // Details - Edit
  @Mutation
  public SET_MATERIALPRODUCT(
    MaterialProduct: MaterialProduct | undefined
  ): void {
    this.materialProduct.value = MaterialProduct;
  }

  @Mutation
  public CLEAR_MATERIALPRODUCT(): void {
    this.materialProduct.value = undefined;
  }

  // List
  @Mutation
  public SET_MATERIALPRODUCTS(MaterialProducts: Array<MaterialProduct>): void {
    this.materialProducts.value = MaterialProducts;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateMaterialProduct(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialProductsService();
    const MaterialProduct = this.materialProductCreate.value;
    if (MaterialProduct === undefined) {
      this.SET_ERROR({
        message:
          "MaterialProductsStore: not expecting MaterialProduct to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(MaterialProduct);
    if (isLeft(response)) {
      const createResponse: CreateMaterialProductResponse = response.value;
      MaterialProduct.Id = createResponse.Id;
      this.SET_MATERIALPRODUCT(MaterialProduct);
      this.SET_MATERIALPRODUCT_CREATE(this.emptyMaterialProduct);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListMaterialProducts(): Promise<void> {
    const service = new MaterialProductsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListMaterialProductResponse = response.value;
      this.SET_MATERIALPRODUCTS(listResponse.MaterialProducts);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListMaterialProductsPublic(): Promise<void> {
    const service = new MaterialProductsService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListMaterialProductResponse = response.value;
      this.SET_MATERIALPRODUCTS(listResponse.MaterialProducts);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateMaterialProduct(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialProductsService();
    const MaterialProduct: MaterialProduct | undefined = this.MaterialProduct;
    if (!MaterialProduct) {
      this.SET_ERROR({
        message:
          "MaterialProductsStore: not expecting MaterialProduct to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(MaterialProduct);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteMaterialProduct(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialProductsService();
    const MaterialProduct: MaterialProduct | undefined = this.MaterialProduct;
    if (!MaterialProduct) {
      this.SET_ERROR({
        message:
          "MaterialProductsStore: not expecting MaterialProduct to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(MaterialProduct);
    if (isLeft(response)) {
      const deleteMaterialProductResponse: DeleteMaterialProductResponse =
        response.value;

      this.SET_MATERIALPRODUCT(undefined);
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

  get MaterialProduct(): MaterialProduct | undefined {
    return this.materialProduct.value;
  }

  get MaterialProducts(): MaterialProduct[] {
    return this.materialProducts.value ?? new Array<MaterialProduct>();
  }

  get MaterialProductCreate(): MaterialProduct {
    return this.materialProductCreate.value;
  }

  get emptyMaterialProduct(): MaterialProduct {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const MaterialProductModule = getModule(MaterialProductStore, store);
