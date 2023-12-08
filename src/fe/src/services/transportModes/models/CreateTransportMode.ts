export interface CreateTransportModeCommand {
  Name: string;
  Description: string;
  HexColor: string;
  IsActive: boolean;
}

export interface CreateTransportModeResponse {
  Id: string;
}
