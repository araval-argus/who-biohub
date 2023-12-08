<template>
  <v-container fluid>
    <v-row>
      <v-col>
        <UserForm
          ref="userForm"
          v-model="user"
          title="Staff Member Create"
          :create="true"
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
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";
import { CountryModule } from "../countries/store";
import { AppModule } from "../../store/MainStore";
import UserForm from "../users/components/UserForm.vue";
import { UserModule } from "../users/store";
import { User } from "@/models/User";

@Component({ components: { UserForm, CardActionsSaveCancel } })
export default class BioHubFacilitiesStaffPageCreate extends Vue {
  $refs!: {
    userForm: UserForm;
  };

  get user(): User {
    return UserModule.UserCreate;
  }

  set user(user: User) {
    UserModule.SET_USER_CREATE(user);
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

  updateBioHubFacilityHiddenInfo() {
    this.$refs.userForm.updateBioHubFacilityId(this.BioHubFacilityId);
  }

  async loadPageInfo() {
    await CountryModule.ListCountries();
    await RoleModule.ListRoles();
    this.updateBioHubFacilityHiddenInfo();
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
