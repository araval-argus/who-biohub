import { TransportCategory } from "@/models/TransportCategory";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListTransportCategoryQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListTransportCategoryResponse {
  TransportCategories: TransportCategory[];
}
