<template>
  <v-container v-if="canRead">
    <v-row>
      <v-col>
        <UserForm
          v-if="user"
          ref="userForm"
          v-model="user"
          title="User Profile"
          :back-button-visible="false"
          :roles="roles"
          :operational-focal-point-visible="false"
          readonly
        >
          <CardActionsEdit @edit="onEdit"> </CardActionsEdit>
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
import CardActionsEdit from "../../components/CardActionsEdit.vue";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import { Role } from "@/models/Role";
import { AuthModule } from "../auth/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserForm, CardActionsEdit } })
export default class LaboratoriesUserPage extends Vue {
  $refs!: {
    userForm: UserForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUser);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateUser);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditUser);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteUser);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.Laboratory;
    });
  }

  get user(): User | undefined {
    return UserModule.User;
  }

  async onEdit(): Promise<void> {
    this.$router.push({
      name: "laboratoryarea-user-edit",
    });
  }

  onCancel(): void {
    UserModule.SET_ERROR(undefined);
    this.$router.back();
  }

  async loadPageInfo() {
    await UserModule.ReadUser(AuthModule.UserId);
    await RoleModule.ListRoles();
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
