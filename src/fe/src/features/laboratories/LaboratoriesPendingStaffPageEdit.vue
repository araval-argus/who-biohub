<template>
  <v-container v-if="canEdit" fluid>
    <v-row>
      <v-col>
        <UserRequestForm
          ref="userRequestForm"
          v-model="userRequest"
          title="Pending Staff Member Edit"
          :back-button-visible="true"
          :roles="roles"
          :readonly="false"
        >
          <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
          </CardActionsSaveCancel>
        </UserRequestForm>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UserRequestForm from "../userRequests/components/UserRequestForm.vue";
import { Role } from "@/models/Role";
import { RoleModule } from "../roles/store";
import { RoleType } from "../../models/enums/RoleType";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";
import { AuthModule } from "../auth/store";
import { UserRequestModule } from "../userRequests/store";
import { UserRequest } from "@/models/UserRequest";
import { LaboratoryModule } from "../laboratories/store";
import { CountryModule } from "./../countries/store";
import { AppModule } from "./../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserRequestForm, CardActionsSaveCancel } })
export default class LaboratoriesPendingStaffPageDetail extends Vue {
  $refs!: {
    userRequestForm: UserRequestForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUserRequest);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateUserRequest);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditUserRequest);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteUserRequest);
  }

  get userRequest(): UserRequest {
    const userRequest = UserRequestModule.UserRequest;
    if (userRequest) return userRequest;

    throw { message: "no pending staff selected" };
  }

  set userRequest(userRequest: UserRequest) {
    UserRequestModule.SET_USERREQUEST(userRequest);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.Laboratory;
    });
  }

  get LaboratoryId(): string {
    return getUserBioHubFacilityOrLaboratoryOrCourierId("");
  }

  async onSave(): Promise<void> {
    await UserRequestModule.UpdateUserRequest()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    UserRequestModule.SET_ERROR(undefined);
    this.$router.back();
  }

  updateLaboratoryHiddenInfo() {
    this.$refs.userRequestForm.updateLaboratoryId(this.LaboratoryId);
    const lab = LaboratoryModule.Laboratories.filter((lab) => {
      return lab.Id == this.LaboratoryId;
    });
    this.$refs.userRequestForm.updateCountryId(lab[0]?.CountryId ?? "");
  }

  async loadPageInfo() {
    await CountryModule.ListCountries();
    await RoleModule.ListRoles();
    await LaboratoryModule.ListLaboratories();
    await UserRequestModule.ReadUserRequest(this.$route.params.id);
    this.updateLaboratoryHiddenInfo();
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
