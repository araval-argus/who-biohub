export interface CreateShipmentCommand {
  ReferenceNumber: string;
  MaterialId: string;
  Temperature: number;
  UnitOfMeasureId: string;
  PriorityRequestTypeId: string;
  TransportModeId: string;
  NumberOfVials: number;
  StatusOfRequest: string;
  QELaboratoryId: string;
  BioHubFacilityId: string;
}

export interface CreateShipmentResponse {
  Id: string;
}
