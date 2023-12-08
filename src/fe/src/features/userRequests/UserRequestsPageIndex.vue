<template>
  <div v-if="canRead">
    <UserRequestsTable
      v-if="!loading"
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-read="canRead"
      :user-requests="userRequests"
      :laboratories="laboratories"
      :roles="roles"
      :loading="loading"
      @selected="selected"
      @delete="deleteItem"
    >
    </UserRequestsTable>
    <CardActionsGenericButton :text="text" @click="click">
    </CardActionsGenericButton>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UserRequestsTable from "./components/UserRequestsTable.vue";
import { UserRequestModule } from "./store";
import { AppError } from "@/models/shared/Error";
import { UserRequest } from "@/models/UserRequest";
import { RoleModule } from "../roles/store";
import { Role } from "@/models/Role";
import { Laboratory } from "@/models/Laboratory";
import { LaboratoryModule } from "../laboratories/store";
import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { AppModule } from "../../store/MainStore";
import { CountryModule } from "./../countries/store";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserRequestsTable, CardActionsGenericButton } })
export default class UserRequestsPageIndex extends Vue {
  private text = "Approved User Requests History";

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUserRequest);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateUserRequest);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditUserRequest);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteUserRequest);
  }

  async loadPageInfo() {
    await CountryModule.ListCountries();
    await RoleModule.ListRoles();
    await LaboratoryModule.ListLaboratories();
    await UserRequestModule.ListUserRequests();
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

  get laboratories(): Array<Laboratory> {
    return LaboratoryModule.Laboratories;
  }

  get error(): AppError | undefined {
    return UserRequestModule.Error;
  }

  get userRequests(): Array<UserRequest> {
    return UserRequestModule.UserRequests.filter((ur) => {
      return ur.Status != UserRegistrationStatus.Approved;
    });
  }

  get roles(): Array<Role> {
    return RoleModule.Roles;
  }

  selected(item: UserRequest): void {
    UserRequestModule.SET_USERREQUEST(item);
    this.$router.push({
      name: "whoarea-user-request-approve",
      params: { id: item.Id },
    });
  }

  click(): void {
    this.$router.push({
      name: "whoarea-approved-user-requests-history",
    });
  }

  async deleteItem(item: UserRequest): Promise<void> {
    UserRequestModule.SET_USERREQUEST(item);
    await UserRequestModule.DeleteUserRequest();
    await UserRequestModule.ListUserRequests();
    return;
  }
}
</script>
