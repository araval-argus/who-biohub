import { ResourceFileType } from "@/models/enums/ResourceFileType";

export interface UploadResourceFileTokenQuery {
  FileCompleteName: string;
  FileType: ResourceFileType | undefined;
}

export interface UploadResourceFileTokenQueryResponse {
  Id: string;
  FileCompleteName: string;
  FileToken: string;
}
