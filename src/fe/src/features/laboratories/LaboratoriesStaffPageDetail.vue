<template>
  <v-container v-if="canRead" fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="Staff Member Detail"
          :back-button-visible="true"
          :roles="roles"
          readonly
          :operational-focal-point-visible="false"
        >
        </UserForm>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { UserModule } from "../users/store";
import UserForm from "../users/components/UserForm.vue";
import { User } from "@/models/User";
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";
import { AuthModule } from "../auth/store";
import { AppModule } from "./../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserForm } })
export default class LaboratoriesStaffPageDetail extends Vue {
  $refs!: {
    userForm: UserForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadLaboratoryStaff);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditLaboratoryStaff);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratoryStaff);
  }

  get user(): User {
    const user = UserModule.User;
    if (user) return user;

    throw { message: "no user selected" };
  }

  set user(user: User) {
    UserModule.SET_USER(user);
  }

  get LaboratoryId(): string {
    return getUserBioHubFacilityOrLaboratoryOrCourierId(this.$route.params.id);
  }

  get IsLaboratoryArea(): boolean {
    return this.$route.name == "whoarea-laboratory-details";
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.Laboratory;
    });
  }
  async loadPageInfo() {
    await RoleModule.ListRoles();
    await UserModule.ReadUser(this.$route.params.id);
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
