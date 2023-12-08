<template>
  <div v-if="canRead">
    <UserRequestsTable
      v-if="!loading"
      :can-read="canRead"
      :can-create="false"
      :can-edit="false"
      title="Approved User Requests History"
      :user-requests="userRequests"
      :laboratories="laboratories"
      :roles="roles"
      readonly
      :loading="loading"
      @selected="selected"
      @delete="deleteItem"
    >
    </UserRequestsTable>
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
import { AppModule } from "../../store/MainStore";
import { CountryModule } from "./../countries/store";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserRequestsTable } })
export default class UserRequestsApprovedHistoryPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUserRequest);
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

  get laboratories(): Array<Laboratory> {
    return LaboratoryModule.Laboratories;
  }

  get error(): AppError | undefined {
    return UserRequestModule.Error;
  }

  get userRequests(): Array<UserRequest> {
    return UserRequestModule.UserRequests.filter((ur) => {
      return ur.Status == UserRegistrationStatus.Approved;
    });
  }

  get roles(): Array<Role> {
    return RoleModule.Roles;
  }

  selected(item: UserRequest): void {
    UserRequestModule.SET_USERREQUEST(item);
    this.$router.push({
      name: "whoarea-user-request-approved-detail",
      params: { id: item.Id },
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
