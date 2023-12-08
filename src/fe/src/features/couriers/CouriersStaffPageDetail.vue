<template>
  <v-container fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="Staff Member Detail"
          :back-button-visible="true"
          :roles="roles"
          readonly
          :job-title-visible="false"
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
import { AppModule } from "@/store/MainStore";

@Component({ components: { UserForm } })
export default class CouriersStaffPageDetail extends Vue {
  $refs!: {
    userForm: UserForm;
  };

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
      return r.RoleType == RoleType.Courier;
    });
  }
  async loadPageInfo() {
    await RoleModule.ListRoles();
    await UserModule.ReadCourierUser(this.$route.params.id);
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
