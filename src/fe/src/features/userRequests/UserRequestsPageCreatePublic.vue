<template>
  <v-container fluid>
    <UserRequestFormPublic
      ref="userRequestFormPublic"
      v-model="userRequest"
      title="Request access to BioHub platform"
      :back-button-visible="false"
      :roles="roles"
    >
      <v-row>
        <v-spacer></v-spacer>
        <v-col cols="12" class="text-center">
          <v-btn color="success" class="mr-4" @click="onSave"> Register </v-btn>
        </v-col>
      </v-row>
    </UserRequestFormPublic>
    <vue-recaptcha
      ref="invisibleRecaptcha"
      size="invisible"
      :sitekey="SiteKey"
      @verify="save"
      @expired="onExpired"
    >
    </vue-recaptcha>
  </v-container>
</template>

<script
  src="https://www.google.com/recaptcha/api.js?onload=vueRecaptchaApiLoaded&render=explicit"
  async
  defer
></script>

<script lang="ts">
import { VueRecaptcha } from "vue-recaptcha";
import { Component, Vue } from "vue-property-decorator";
import { UserRequestModule } from "./store";
import UserRequestFormPublic from "./components/UserRequestFormPublic.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { UserRequest } from "@/models/UserRequest";
import { RolePublic } from "@/models/RolePublic";
import { RoleModule } from "../roles/store";

import { siteKey } from "./configuration";

@Component({
  components: { UserRequestFormPublic, CardActionsSaveCancel, VueRecaptcha },
})
export default class UserRequestsPageCreatePublic extends Vue {
  $refs!: {
    userRequestFormPublic: UserRequestFormPublic;
    invisibleRecaptcha: VueRecaptcha;
  };

  get SiteKey(): string {
    return siteKey ?? "";
  }

  get userRequest(): UserRequest {
    return UserRequestModule.UserRequestPublicCreate;
  }

  set userRequest(userRequest: UserRequest) {
    UserRequestModule.SET_USERREQUEST_PUBLIC_CREATE(userRequest);
  }

  get roles(): Array<RolePublic> {
    return RoleModule.RolesPublic;
  }

  async save(recaptchaResponse: string): Promise<void> {
    this.$refs.userRequestFormPublic.validate();

    let userRequest = this.userRequest;

    userRequest.RecaptchaResponse = recaptchaResponse;

    UserRequestModule.SET_USERREQUEST_PUBLIC_CREATE(userRequest);

    await UserRequestModule.PublicCreateUserRequest()
      .then((response) => {
        this.resetRecaptcha();
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
        this.resetRecaptcha();
      });
  }

  onSave(): void {
    this.$refs.invisibleRecaptcha.execute();
  }

  onCancel(): void {
    UserRequestModule.SET_ERROR(undefined);
    this.resetRecaptcha();
    this.$router.back();
  }

  onExpired(): void {
    this.resetRecaptcha();
  }

  resetRecaptcha(): void {
    this.$refs.invisibleRecaptcha.reset();
  }

  async mounted() {
    UserRequestModule.CLEAR_USERREQUEST_PUBLIC_CREATE();
    await RoleModule.ListRolesPublic();
    this.resetRecaptcha();
  }
}
</script>
