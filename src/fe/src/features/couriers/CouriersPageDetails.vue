<template>
  <div>
    <CourierForm
      v-if="courier && canRead"
      v-model="courier"
      title="Courier Details"
      readonly
    >
    </CourierForm>
    <div v-else><span>No Courier selected</span></div>
    <div class="mt-5"></div>
    <UsersTable
      v-if="canReadCourierStaff"
      :hide-is-active="true"
      :users="users"
      :roles="roles"
      :loading="loading"
      title="Staff Members"
      :can-create="canCreateCourierStaff"
      :can-edit="canEditCourierStaff"
      :can-delete="canDeleteCourierStaff"
      :hide-operational-focal-point="true"
      :hide-job-title="true"
      @selected="selected"
      @create="createItem"
      @edit="editItem"
      @delete="deleteRequestItem"
    >
    </UsersTable>
    <div class="mt-5"></div>
    <CourierBookingFormsTable
      :loading="loading"
      @selected="selectedBookingForm"
    >
    </CourierBookingFormsTable>
  </div>
</template>

<script lang="ts">
import CourierForm from "./components/CourierForm.vue";
import { Component, Vue } from "vue-property-decorator";
import { Courier } from "@/models/Courier";
import { CourierModule } from "./store";
import { CountryModule } from "../countries/store";
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
import CourierBookingFormsTable from "./components/CourierBookingFormsTable.vue";
import { CourierBookingFormGridItem } from "@/models/CourierBookingFormGridItem";

@Component({
  components: { CourierForm, UsersTable, CourierBookingFormsTable },
})
export default class CouriersPageDetails extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadCourier);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateCourier);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditCourier);
  }

  get canReadCourierStaff(): boolean {
    return hasPermission(PermissionNames.CanReadCourierStaff);
  }

  get canCreateCourierStaff(): boolean {
    return hasPermission(PermissionNames.CanCreateCourierStaff);
  }

  get canEditCourierStaff(): boolean {
    return hasPermission(PermissionNames.CanEditCourierStaff);
  }

  get canDeleteCourierStaff(): boolean {
    return hasPermission(PermissionNames.CanDeleteCourierStaff);
  }

  get courier(): Courier | undefined {
    return CourierModule.Courier;
  }

  get users(): Array<User> {
    return UserModule.Users.filter((u) => {
      return u.CourierId == this.$route.params.id;
    });
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.Courier;
    });
  }

  createItem(): void {
    UserModule.CLEAR_USER_CREATE();
    this.$router.push({
      name: "whoarea-courier-staff-create",
      params: { id: this.$route.params.id },
    });
  }

  editItem(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-courier-staff-edit",
      params: { id: item.Id },
    });
  }

  selected(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-courier-staff-details",
      params: { id: item.Id },
    });
  }

  selectedBookingForm(item: CourierBookingFormGridItem): void {
    this.$router.push({
      name: "whoarea-courier-booking-form-info",
      params: { id: item.Id },
    });
  }

  async deleteRequestItem(item: User): Promise<void> {
    UserModule.SET_USER(item);
    await UserModule.DeleteUser();
    await UserModule.ListCourierUsers();
    return;
  }

  async loadPageInfo() {
    await CountryModule.ListCountries();
    await CourierModule.ReadCourier(this.$route.params.id);
    await CourierModule.ListCourierBookingForms(this.$route.params.id);
    if (this.canReadCourierStaff) {
      await RoleModule.ListRoles();
      await UserModule.ListCourierUsers();
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
