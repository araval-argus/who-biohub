export interface CreateIsolationTechniqueTypeCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateIsolationTechniqueTypeResponse {
  Id: string;
}
