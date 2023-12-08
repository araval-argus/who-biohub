<template>
  <div class="auth"></div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AuthModule } from "../features/auth/store";
import { RoleType } from "@/models/enums/RoleType";

@Component({ components: {} })
export default class AuthView extends Vue {
  async created() {
    try {
      if (AuthModule.UserToken == undefined) {
        await AuthModule.setUserTokenAsync();
      }
      await AuthModule.setLoginInfoAsync();
      this.$router.push({
        name: AuthModule.LandingPage,
      });
    } catch (error) {
      this.$router.push({
        name: "unauthorized",
      });
    }
  }
}
</script>
