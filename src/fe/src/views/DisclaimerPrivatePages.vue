// this is a temporary component, to be removed in the future
<template>
  <div v-if="IsLogged">
    <p>
      <b>Disclaimer:</b> This is the private area for Minimal Viable Product
      (MVP). Because the current MVP is not equipped with an authentication
      system for protecting data confidentiality, information that cannot be
      shared publicly is obfuscated.
    </p>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AuthModule } from "../features/auth/store";

@Component({})
export default class DisclaimerPrivatePages extends Vue {
  get IsLogged(): boolean {
    return AuthModule.IsLogged;
  }
}
</script>
