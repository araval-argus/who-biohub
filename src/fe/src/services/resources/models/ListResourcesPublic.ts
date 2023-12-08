import { ResourcePublic } from "@/models/ResourcePublic";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListResourcesPublicQuery {
  Id: string | undefined;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListResourcesPublicResponse {
  Resources: ResourcePublic[];
}
