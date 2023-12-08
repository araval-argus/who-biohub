<template>
  <v-container fluid class="laboratoryProfile">
    <v-card-title>
      Staff
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-subtitle>
      <disclaimer-mockup-images></disclaimer-mockup-images>
    </v-card-subtitle>
    <v-card flat color="transparent">
      <v-img
        alt="Staff"
        title="Staff"
        src="@/assets/icons/lab-staffmembers-page.png"
      />
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../store/MainStore";
import DisclaimerMockupImages from "../views/DisclaimerMockupImages.vue";

@Component({ components: { DisclaimerMockupImages } })
export default class LaboratoryStaff extends Vue {
  mounted() {
    AppModule.HideLoading();
  }
}
</script>
