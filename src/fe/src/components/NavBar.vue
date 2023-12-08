<template>
  <v-container class="my-0 py-0" fluid>
    <v-system-bar color="white" height="35px">
      <v-spacer></v-spacer>
      <v-btn text @click="toHome">Home</v-btn>
      <v-btn text @click="toWHO">WHO Website</v-btn>
      <v-btn text @click="toResources">Resources</v-btn>
      <!-- <v-btn text @click="toFaq">FAQ</v-btn> TODO: For the moment it is commented, in the future it will be reintroduced -->
      <v-btn v-if="!IsLogged || isPublicPage" text @click="toRequestAccess"
        >Request Access</v-btn
      >
      <v-btn v-if="IsLogged" text @click="toPrivateArea()">My Dashboard</v-btn>
      <LoginLogoutBtn></LoginLogoutBtn>
    </v-system-bar>
    <v-app-bar color="white" height="100px" elevation="0"> </v-app-bar>
  </v-container>
</template>

<script lang="ts">
import router from "@/router";
import { Component, Prop, Vue } from "vue-property-decorator";
import LoginLogoutBtn from "@/components/LoginLogoutBtn.vue";

import { AuthModule } from "../features/auth/store";

@Component({ components: { LoginLogoutBtn } })
export default class NavBar extends Vue {
  @Prop({ type: Boolean, default: false }) readonly isPublicPage!: boolean;

  toHome(): void {
    this.toRoute("home");
  }
  toResources(): void {
    this.toRoute("resources");
  }
  toFaq(): void {
    this.toRoute("faq");
  }
  toRequestAccess(): void {
    this.toRoute("user-request");
  }
  toWHO(): void {
    window.open("https://www.who.int/initiatives/who-biohub", "_blank");
  }
  toPrivateArea(): void {
    if (AuthModule.UserLoginInfo?.LandingPage) {
      this.toRoute(AuthModule.UserLoginInfo?.LandingPage);
    } else {
      this.toRoute("unauthorized");
    }
  }

  toRoute(routeName: string): void {
    if (this.$route.name != routeName) {
      router.push({ name: routeName });
    }
  }

  get IsLogged(): boolean {
    return AuthModule.IsLogged;
  }
}
</script>

<style scoped>
.v-btn {
  text-transform: none !important;
}
</style>
