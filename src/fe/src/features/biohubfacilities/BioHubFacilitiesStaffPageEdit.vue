<template>
  <v-container fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="Staff Member Edit"
          :back-button-visible="true"
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
import { UserModule } from "../users/store";
import UserForm from "../users/components/UserForm.vue";
import { User } from "@/models/User";
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { AppModule } from "../../store/MainStore";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";

@Component({ components: { UserForm, CardActionsSaveCancel } })
export default class BioHubFacilitiesStaffPageEdit extends Vue {
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
      return r.RoleType == RoleType.BioHubFacility;
    });
  }

  get BioHubFacilityId(): string {
    return getUserBioHubFacilityOrLaboratoryOrCourierId(this.$route.params.id);
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
