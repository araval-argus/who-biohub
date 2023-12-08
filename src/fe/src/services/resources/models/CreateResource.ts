import { ResourceFileType } from "@/models/enums/ResourceFileType";

export interface CreateResourceCommand {
  Id: string;
  FileCompleteName: string;
  ParentId: string;
  UploadedById: string;
  ResourceFileType: ResourceFileType;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface CreateResourceResponse {
  Id: string;
}
