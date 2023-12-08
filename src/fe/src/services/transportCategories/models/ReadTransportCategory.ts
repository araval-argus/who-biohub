import { TransportCategory } from "@/models/TransportCategory";

export interface ReadTransportCategoryQuery {
  Id: string;
}

export interface ReadTransportCategoryResponse {
  TransportCategory: TransportCategory;
}
