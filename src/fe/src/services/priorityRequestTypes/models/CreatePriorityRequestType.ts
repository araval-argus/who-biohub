export interface CreatePriorityRequestTypeCommand {
  Name: string;
  Description: string;
  HexColor: string;
  IsActive: boolean;
}

export interface CreatePriorityRequestTypeResponse {
  Id: string;
}
