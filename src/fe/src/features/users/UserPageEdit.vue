<template>
  <v-container fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="User Profile Edit"
          :back-button-visible="true"
          :roles="roles"
          :readonly="false"
          :operational-focal-point-visible="BioHubFacilityId != ''"
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
import { User } from "@/models/User";
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { AuthModule } from "../auth/store";
import { AppModule } from "../../store/MainStore";

@Component({ components: { UserForm, CardActionsSaveCancel } })
export default class UserPageEdit extends Vue {
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
      return (
        (this.IsLaboratoryArea && r.RoleType == RoleType.Laboratory) ||
        (this.IsBioHubFacilityArea && r.RoleType == RoleType.BioHubFacility)
      );
    });
  }

  get LaboratoryId(): string {
    return AuthModule.LaboratoryId ?? "";
  }

  get BioHubFacilityId(): string {
    return AuthModule.BioHubFacilityId ?? "";
  }

  get IsLaboratoryArea(): boolean {
    return this.$route.name == "laboratoryarea-user-edit";
  }

  get IsBioHubFacilityArea(): boolean {
    return this.$route.name == "biohubfacilityarea-user-edit";
  }

  get UserId(): string {
    if (this.IsLaboratoryArea || this.IsBioHubFacilityArea) {
      return AuthModule.UserId;
    }
    return this.$route.params.id;
  }

  async onSave(): Promise<void> {
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
    await UserModule.ReadUser(this.UserId);
  }

  async mounted() {
    try {
      if (AuthModule.UserToken == undefined) {
        await AuthModule.setUserTokenAsync();
      }
      if (AuthModule.IsLogged == false) {
        await AuthModule.setUserInfoAsync().then(async () => {
          await this.loadPageInfo();
        });
      } else {
        await this.loadPageInfo();
      }
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
