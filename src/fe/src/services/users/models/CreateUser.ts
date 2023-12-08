export interface CreateUserCommand {
  FirstName: string;
  LastName: string;
  Email: string;
  JobTitle: string;
  MobilePhone: string;
  BusinessPhone: string;
  OperationalFocalPoint: boolean;
  RoleId: string;
  LaboratoryId: string;
  BioHubFacilityId: string;
  CourierId: string;
}

export interface CreateUserResponse {
  Id: string;
}
