<template>
  <v-container v-if="canCreate" fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="WHO User Create"
          :create="true"
          :back-button-visible="true"
          :operational-focal-point-visible="false"
          :roles="roles"
          :readonly="false"
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
export default class WhoUserPageCreate extends Vue {
  $refs!: {
    userForm: UserForm;
  };

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateUser);
  }

  get user(): User {
    return UserModule.UserCreate;
  }

  set user(user: User) {
    UserModule.SET_USER_CREATE(user);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.WHO;
    });
  }

  async onSave(): Promise<void> {
    this.$refs.userForm.validate();
    await UserModule.CreateUser()
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
    UserModule.CLEAR_USER_CREATE();
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
