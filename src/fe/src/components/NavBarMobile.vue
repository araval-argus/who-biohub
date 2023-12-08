<template>
  <v-container class="my-0 py-0" fluid>
    <v-system-bar color="white" height="35px">
      <v-spacer></v-spacer>
      <v-app-bar-nav-icon
        right
        @click.stop="drawer = !drawer"
      ></v-app-bar-nav-icon>
    </v-system-bar>
    <v-app-bar color="white" elevation="0"> </v-app-bar>
    <v-navigation-drawer v-model="drawer" app right @click="drawer = !drawer">
      <v-list nav dense>
        <v-list-item-group
          v-model="group"
          active-class="blue--text text--accent-4"
        >
          <v-list-item two-line class="px-2" @click="drawer = !drawer">
            <v-list-item-content>
              <v-list-item-title>Menu</v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-content>
              <v-btn text @click="toHome">Home</v-btn>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-content>
              <v-btn text @click="toWHO">WHO Website</v-btn>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-content>
              <v-btn text @click="toResources">Resources</v-btn>
            </v-list-item-content>
          </v-list-item>

          <!-- TODO: For the moment it is commented, in the future it will be reintroduced 
          <v-list-item>
            <v-list-item-content>
              <v-btn text @click="toFaq">FAQ</v-btn>
            </v-list-item-content>
          </v-list-item> -->

          <v-list-item v-if="!IsLogged || isPublicPage">
            <v-list-item-content>
              <v-btn text @click="toRequestAccess">Request Access</v-btn>
            </v-list-item-content>
          </v-list-item>

          <v-list-item v-if="IsLogged">
            <v-list-item-content>
              <v-btn text @click="toPrivateArea()">My Dashboard</v-btn>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-content>
              <LoginLogoutBtn></LoginLogoutBtn>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </v-container>
</template>

<script lang="ts">
import router from "@/router";
import { Component, Prop, Vue } from "vue-property-decorator";
import LoginLogoutBtn from "@/components/LoginLogoutBtn.vue";
import { AuthModule } from "../features/auth/store";

@Component({ components: { LoginLogoutBtn } })
export default class NavBarMobile extends Vue {
  private drawer = false;
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
