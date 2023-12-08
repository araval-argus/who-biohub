<template>
  <v-container v-if="canEdit" fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="WHO User Edit"
          :back-button-visible="true"
          :roles="roles"
          :readonly="false"
          :operational-focal-point-visible="false"
        >
          <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
          </CardActionsSaveCancel>
        </UserForm>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { UserModule } from "./store";
import UserForm from "./components/UserForm.vue";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { AuthModule } from "../auth/store";
import { Role } from "@/models/Role";
import { User } from "@/models/User";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserForm, CardActionsSaveCancel } })
export default class WhoUserPageEdit extends Vue {
  $refs!: {
    userForm: UserForm;
  };

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditUser);
  }

  get user(): User {
    const user = UserModule.User;
    if (user) return user;

    throw { message: "no user selected" };
  }

  set user(user: User) {
    UserModule.SET_USER(user);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.WHO;
    });
  }

  async onSave(): Promise<void> {
    this.$refs.userForm.validate();
    await UserModule.UpdateUser()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    UserModule.SET_ERROR(undefined);
    this.$router.back();
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
