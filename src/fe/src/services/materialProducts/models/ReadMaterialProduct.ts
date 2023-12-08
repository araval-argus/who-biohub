import { MaterialProduct } from "@/models/MaterialProduct";

export interface ReadMaterialProductQuery {
  Id: string;
}

export interface ReadMaterialProductResponse {
  MaterialProduct: MaterialProduct;
}
