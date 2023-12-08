<template>
  <div>
    <span v-if="error"> Error retrieving staff members: {{ error }} </span>
    <div v-else>
      <UsersTable
        v-if="canRead"
        :only-details="true"
        :hide-is-active="true"
        :users="users"
        :roles="roles"
        :loading="loading"
        title="BioHub Facility Staff"
        :can-create="false"
        :can-edit="false"
        :can-delete="false"
        @selected="selected"
      >
      </UsersTable>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UsersTable from "../users/components/UsersTable.vue";
import UserRequestsTable from "../userRequests/components/UserRequestsTable.vue";
import { UserModule } from "../users/store";
import { UserRequestModule } from "../userRequests/store";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { AppError } from "@/models/shared/Error";
import { User } from "@/models/User";
import { BioHubFacility } from "@/models/BioHubFacility";
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
export default class BioHubFacilitiesStaffPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUser);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.BioHubFacility;
    });
  }

  get users(): Array<User> {
    return UserModule.Users.filter((u) => {
      return u.BioHubFacilityId === this.BioHubFacilityId;
    });
  }

  get bioHubFacilities(): Array<BioHubFacility> {
    return BioHubFacilityModule.BioHubFacilities;
  }

  async loadPageInfo() {
    await RoleModule.ListRoles();
    await UserModule.ListUsers();
    //await BioHubFacilityModule.ListBioHubFacilities();
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

  get BioHubFacilityId(): string {
    return getUserBioHubFacilityOrLaboratoryOrCourierId(this.$route.params.id);
  }

  get CurrentUserId(): string {
    return AuthModule.UserId;
  }

  selected(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "biohubfacilityarea-biohubfacility-staff-details",
      params: { id: item.Id },
    });
  }
}
</script>
