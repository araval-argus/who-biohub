<template>
  <div>
    <span v-if="error"> Error retrieving staff members: {{ error }} </span>
    <div v-else>
      <UsersTable
        :users="users"
        :roles="roles"
        :hide-is-active="true"
        :hide-operational-focal-point="true"
        title="Staff Members"
        create-name="Add Member"
        :can-create="canCreateUserRequest"
        :can-edit="canEdit"
        :can-delete="canDelete"
        :loading="loading"
        @selected="selected"
        @create="create"
        @edit="editItem"
        @delete="deleteItem"
      >
      </UsersTable>
      <v-spacer></v-spacer>
      <UserRequestsTable
        v-if="canReadUserRequest"
        :user-requests="userRequests"
        :can-create="false"
        :can-edit="canEditUserRequest"
        :can-delete="canDeleteUserRequest"
        :hide-is-active="true"
        :roles="roles"
        :laboratories="laboratories"
        :hide-institute-information="true"
        :loading="loading"
        title="Staff Members Pending Requests"
        @selected="selectedRequest"
        @edit="editRequestItem"
        @delete="deleteRequestItem"
      >
      </UserRequestsTable>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UsersTable from "../users/components/UsersTable.vue";
import UserRequestsTable from "../userRequests/components/UserRequestsTable.vue";
import { UserModule } from "../users/store";
import { UserRequestModule } from "../userRequests/store";
import { LaboratoryModule } from "../laboratories/store";
import { AppError } from "@/models/shared/Error";
import { User } from "@/models/User";
import { Laboratory } from "@/models/Laboratory";
import { UserRequest } from "@/models/UserRequest";
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { AuthModule } from "../auth/store";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";
import { RoleType } from "../../models/enums/RoleType";
import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UsersTable, UserRequestsTable } })
export default class LaboratoriesStaffPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadLaboratoryStaff);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateUser);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditLaboratoryStaff);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratoryStaff);
  }

  get canReadUserRequest(): boolean {
    return hasPermission(PermissionNames.CanReadUserRequest);
  }

  get canCreateUserRequest(): boolean {
    return hasPermission(PermissionNames.CanCreateUserRequest);
  }

  get canEditUserRequest(): boolean {
    return hasPermission(PermissionNames.CanEditUserRequest);
  }

  get canDeleteUserRequest(): boolean {
    return hasPermission(PermissionNames.CanDeleteUserRequest);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.Laboratory;
    });
  }

  get users(): Array<User> {
    return UserModule.Users.filter((u) => {
      return u.LaboratoryId === this.LaboratoryId;
    });
  }

  get laboratories(): Array<Laboratory> {
    return LaboratoryModule.Laboratories;
  }

  get userRequests(): Array<UserRequest> {
    return UserRequestModule.UserRequests.filter((u) => {
      return (
        u.LaboratoryId === this.LaboratoryId &&
        (u.Status == UserRegistrationStatus.Pending ||
          u.Status == UserRegistrationStatus.Rejected)
      );
    });
  }

  async loadPageInfo() {
    await RoleModule.ListRoles();
    await UserModule.ListUsers();
    if (this.canReadUserRequest == true) {
      await UserRequestModule.ListUserRequests();
      await LaboratoryModule.ListLaboratories();
    }
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get error(): AppError | undefined {
    return UserModule.Error;
  }

  get LaboratoryId(): string {
    return getUserBioHubFacilityOrLaboratoryOrCourierId(this.$route.params.id);
  }

  get CurrentUserId(): string {
    return AuthModule.UserId;
  }

  editItem(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "laboratoryarea-staff-edit",
      params: { id: item.Id },
    });
  }

  selected(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "laboratoryarea-staff-details",
      params: { id: item.Id },
    });
  }

  create(): void {
    this.$router.push({
      name: "laboratoryarea-staff-create",
    });
  }

  async deleteItem(item: User): Promise<void> {
    UserModule.SET_USER(item);
    await UserModule.DeleteUser();
    await UserModule.ListUsers();
    return;
  }

  editRequestItem(item: UserRequest): void {
    UserRequestModule.SET_USERREQUEST(item);
    this.$router.push({
      name: "laboratoryarea-pending-staff-edit",
      params: { id: item.Id },
    });
  }

  selectedRequest(item: UserRequest): void {
    UserRequestModule.SET_USERREQUEST(item);
    this.$router.push({
      name: "laboratoryarea-pending-staff-details",
      params: { id: item.Id },
    });
  }

  async deleteRequestItem(item: UserRequest): Promise<void> {
    UserRequestModule.SET_USERREQUEST(item);
    await UserRequestModule.DeleteUserRequest();
    await UserRequestModule.ListUserRequests();
    return;
  }
}
</script>
