export interface UpdateUserCommand {
  Id: string;
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

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateUserResponse {}
