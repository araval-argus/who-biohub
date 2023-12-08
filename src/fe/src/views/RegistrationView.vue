<template>
  <v-container fluid class="registration">
    <v-card flat color="transparent">
      <v-card-title>
        Registration
        <v-spacer></v-spacer>
      </v-card-title>
      <img
        alt="Registration"
        title="Registration"
        src="@/assets/icons/registration.png"
      />
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({ components: {} })
export default class RegistrationView extends Vue {}
</script>
