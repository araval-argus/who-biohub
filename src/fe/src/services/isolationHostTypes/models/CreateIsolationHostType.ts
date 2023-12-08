export interface CreateIsolationHostTypeCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateIsolationHostTypeResponse {
  Id: string;
}
