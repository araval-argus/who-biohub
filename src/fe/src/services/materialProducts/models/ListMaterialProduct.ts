import { MaterialProduct } from "@/models/MaterialProduct";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialProductQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialProductResponse {
  MaterialProducts: MaterialProduct[];
}
