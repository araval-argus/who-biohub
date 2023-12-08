<template>
  <v-btn text @click="onClick">{{ Text }}</v-btn>
</template>

<script lang="ts">
import { AuthModule } from "@/features/auth/store";
import { Component, Vue } from "vue-property-decorator";

@Component({})
export default class LoginLogoutBtn extends Vue {
  get IsLogged(): boolean {
    return AuthModule.IsLogged;
  }

  get Text(): string {
    if (this.IsLogged) return "Logout";
    return "Login";
  }

  async onClick(): Promise<void> {
    if (this.IsLogged) await this.logout();
    else await this.login();
  }

  private async login(): Promise<void> {
    await AuthModule.loginAsync();
    this.$router.push({ name: "auth" });
  }

  private async logout(): Promise<void> {
    await AuthModule.logoutAsync();
    this.$router.push({ name: "home" });
  }
}
</script>
