<template>
  <div>
    <BioHubFacilityForm
      v-if="bioHubFacility && canRead"
      v-model="bioHubFacility"
      title="BioHub Facility Details"
      readonly
    >
    </BioHubFacilityForm>
    <div v-else><span>No BioHub Facility selected</span></div>
    <div class="mt-5"></div>
    <UsersTable
      v-if="canAccessUsers"
      :hide-is-active="true"
      :users="users"
      :roles="roles"
      :loading="loading"
      title="BioHub Facility Staff"
      :can-create="canCreateUser"
      :can-edit="canEditUser"
      :can-delete="canDeleteUser"
      @selected="selected"
      @create="createItem"
      @edit="editItem"
      @delete="deleteRequestItem"
    >
    </UsersTable>
  </div>
</template>

<script lang="ts">
import BioHubFacilityForm from "./components/BioHubFacilityForm.vue";
import { Component, Vue } from "vue-property-decorator";
import { BioHubFacility } from "@/models/BioHubFacility";
import { BioHubFacilityModule } from "./store";
import { BSLLevelModule } from "./../bsllevels/store";
import { CountryModule } from "./../countries/store";
import { AppModule } from "../../store/MainStore";
import { AuthModule } from "../auth/store";
import { RoleType } from "@/models/enums/RoleType";
import { Role } from "@/models/Role";
import { User } from "@/models/User";
import { RoleModule } from "../roles/store";
import { users } from "../users/mock";
import { UserModule } from "../users/store";
import UsersTable from "../users/components/UsersTable.vue";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { BioHubFacilityForm, UsersTable } })
export default class BioHubFacilitiesPageDetails extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadBioHubFacility);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateBioHubFacility);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditBioHubFacility);
  }

  get canAccessUsers(): boolean {
    return hasPermission(PermissionNames.CanAccessWHOUsers);
  }

  get canReadUser(): boolean {
    return hasPermission(PermissionNames.CanReadUser);
  }

  get canCreateUser(): boolean {
    return hasPermission(PermissionNames.CanCreateUser);
  }

  get canEditUser(): boolean {
    return hasPermission(PermissionNames.CanEditUser);
  }

  get canDeleteUser(): boolean {
    return hasPermission(PermissionNames.CanDeleteUser);
  }

  get bioHubFacility(): BioHubFacility | undefined {
    return BioHubFacilityModule.BioHubFacility;
  }

  get users(): Array<User> {
    return UserModule.Users.filter((u) => {
      return u.BioHubFacilityId == this.$route.params.id;
    });
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.BioHubFacility;
    });
  }

  createItem(): void {
    UserModule.CLEAR_USER_CREATE();
    this.$router.push({
      name: "whoarea-biohubfacility-staff-create",
      params: { id: this.$route.params.id },
    });
  }

  editItem(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-biohubfacility-staff-edit",
      params: { id: item.Id },
    });
  }

  selected(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-biohubfacility-staff-details",
      params: { id: item.Id },
    });
  }

  async deleteRequestItem(item: User): Promise<void> {
    UserModule.SET_USER(item);
    await UserModule.DeleteUser();
    await UserModule.ListUsers();
    return;
  }

  async loadPageInfo() {
    await BSLLevelModule.ListBSLLevels();
    await CountryModule.ListCountries();
    await BioHubFacilityModule.ReadBioHubFacility(this.$route.params.id);
    if (this.canAccessUsers) {
      await RoleModule.ListRoles();
      await UserModule.ListUsers();
    }
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
