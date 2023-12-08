<template>
  <v-container fluid class="user">
    <user-profile-page />
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UserPage from "@/features/users/UserPage.vue";
import DisclaimerMockupImages from "../views/DisclaimerMockupImages.vue";

@Component({ components: { DisclaimerMockupImages } })
export default class LaboratoryProfile extends Vue {}
</script>
