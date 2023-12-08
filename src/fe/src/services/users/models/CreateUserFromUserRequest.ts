export interface CreateUserFromUserRequestCommand {
  FirstName: string;
  LastName: string;
  Email: string;
  RoleId: string;
  LaboratoryId: string;
}

export interface CreateUserFromUserRequestResponse {
  Id: string;
}
