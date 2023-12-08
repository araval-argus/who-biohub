<template>
  <div>
    <LaboratoryForm
      v-if="laboratory && canRead"
      v-model="laboratory"
      :laboratory-area="false"
      :bio-hub-facility-area="bioHubFacilityArea"
      title="Laboratory Details"
      readonly
    >
    </LaboratoryForm>
    <div class="mt-5"></div>
    <UsersTable
      v-if="canAccessUsers"
      :only-details="false"
      :hide-is-active="true"
      :users="users"
      :roles="roles"
      :loading="loading"
      title="Staff Members"
      create-name="Add Member"
      :can-create="canCreateUser"
      :can-edit="canEditUser"
      :can-delete="canDeleteUser"
      @selected="selected"
      @create="create"
      @edit="editItem"
      @delete="deleteItem"
    >
    </UsersTable>
  </div>
</template>

<script lang="ts">
import LaboratoryForm from "./components/LaboratoryForm.vue";
import { Component, Vue } from "vue-property-decorator";
import { Laboratory } from "@/models/Laboratory";
import { LaboratoryModule } from "./store";
import { User } from "@/models/User";
import UsersTable from "../users/components/UsersTable.vue";
import { UserModule } from "../users/store";
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import { AppModule } from "../../store/MainStore";
import { BSLLevelModule } from "./../bsllevels/store";
import { CountryModule } from "./../countries/store";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { LaboratoryForm, UsersTable } })
export default class LaboratoriesPageDetails extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadLaboratory);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateLaboratory);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditLaboratory);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratory);
  }

  get laboratory(): Laboratory | undefined {
    return LaboratoryModule.Laboratory;
  }

  get bioHubFacilityArea(): boolean {
    return this.$route.name == "biohubfacilityarea-laboratory-details";
  }

  get canAccessUsers(): boolean {
    return hasPermission(PermissionNames.CanAccessWHOUsers);
  }

  get canReadUser(): boolean {
    return hasPermission(PermissionNames.CanReadUser);
  }

  get canCreateUser(): boolean {
    return hasPermission(PermissionNames.CanCreateUserRequest);
  }

  get canEditUser(): boolean {
    return hasPermission(PermissionNames.CanEditLaboratoryStaff);
  }

  get canDeleteUser(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratoryStaff);
  }

  get users(): Array<User> {
    return UserModule.Users.filter((u) => {
      return u.LaboratoryId == this.$route.params.id;
    });
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.Laboratory;
    });
  }

  editItem(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-laboratory-staff-edit",
      params: { id: item.Id },
    });
  }

  selected(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-laboratory-staff-details",
      params: { id: item.Id },
    });
  }

  create(): void {
    this.$router.push({
      name: "whoarea-laboratory-staff-create",
      params: { id: this.$route.params.id },
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
    await LaboratoryModule.ReadLaboratory(this.$route.params.id);
    if (this.canAccessUsers == true) {
      await RoleModule.ListRoles();
      await UserModule.ListUsers();
    }
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
      console.log("Laboratory Page loaded");
    }
  }
}
</script>
