import { Resource } from "@/models/Resource";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListResourcesQuery {
  Id: string | undefined;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListResourcesResponse {
  Resources: Resource[];
}
