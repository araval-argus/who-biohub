<template>
  <v-container v-if="canRead" fluid>
    <UserRequestFormApprove
      ref="userRequestFormApprove"
      v-model="userRequest"
      title="Approved User Request Detail"
      :back-button-visible="false"
      :roles="roles"
      :laboratories="laboratories"
      readonly
    >
    </UserRequestFormApprove>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { UserRequestModule } from "./store";
import { UserModule } from "../users/store";
import UserRequestFormApprove from "./components/UserRequestFormApprove.vue";
import { UserRequest } from "@/models/UserRequest";
import { Role } from "@/models/Role";
import { User } from "@/models/User";
import { RoleModule } from "../roles/store";
import { Laboratory } from "@/models/Laboratory";
import { LaboratoryModule } from "../laboratories/store";
import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import { UserRequestStatusModule } from "@/features/userRequestsStatuses/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UserRequestFormApprove } })
export default class UserRequestsPageApprovedDetail extends Vue {
  private fromPublic = false;

  $refs!: {
    userRequestFormApprove: UserRequestFormApprove;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUserRequest);
  }

  get userRequest(): UserRequest {
    const ur = UserRequestModule.UserRequest;
    if (ur) return ur;

    throw { message: "no user request selected" };
  }

  set userRequest(userRequest: UserRequest) {
    UserRequestModule.SET_USERREQUEST(userRequest);
  }

  get roles(): Array<Role> {
    return RoleModule.Roles;
  }

  get laboratories(): Array<Laboratory> {
    return LaboratoryModule.Laboratories;
  }

  async onSave(): Promise<void> {
    this.$refs.userRequestFormApprove.validate();
    await UserRequestModule.UpdateUserRequest()

      .then(async (response) => {
        if (
          UserRequestModule.UserRequest?.Status ==
          UserRegistrationStatus.Approved
        ) {
          if (this.fromPublic) {
            await LaboratoryModule.CreateLaboratoryFromUserRequest(
              UserRequestModule.UserRequest
            ).then(async (laboratoryId) => {
              UserRequestModule.SET_LABORATORY_ID(laboratoryId);
              await UserModule.CreateUserFromUserRequest(
                UserRequestModule.UserRequest
              );
            });
          } else {
            await UserModule.CreateUserFromUserRequest(
              UserRequestModule.UserRequest
            );
          }
        }

        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  updateInstituteNameInfo() {
    if (
      UserRequestModule.UserRequest?.LaboratoryId != null &&
      UserRequestModule.UserRequest?.LaboratoryId != undefined &&
      UserRequestModule.UserRequest?.LaboratoryId != ""
    ) {
      this.fromPublic = false;
      const laboratory = this.laboratories.filter((lab) => {
        return lab.Id == UserRequestModule.UserRequest?.LaboratoryId;
      });

      if (laboratory.length > 0) {
        const instituteName = laboratory[0].Name;
        this.$refs.userRequestFormApprove.updateInstituteName(instituteName);
      }
    } else {
      this.fromPublic = true;
    }
  }

  async loadPageInfo() {
    await RoleModule.ListRoles();
    await LaboratoryModule.ListLaboratories();
    await UserRequestStatusModule.ListUserRequestStatuses();
    await UserRequestModule.ReadUserRequest(this.$route.params.id).then(() => {
      this.updateInstituteNameInfo();
    });
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
