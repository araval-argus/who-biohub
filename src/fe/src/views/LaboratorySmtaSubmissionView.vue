<template>
  <v-container fluid class="smtaSubmission">
    <v-card-title>
      SMTA Submission
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-subtitle>
      <disclaimer-mockup-images></disclaimer-mockup-images>
    </v-card-subtitle>
    <v-img
      alt="SMTA Submission"
      title="SMTA Submission"
      src="@/assets/icons/lab-smta-submitting.png"
    />
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../store/MainStore";
import DisclaimerMockupImages from "./DisclaimerMockupImages.vue";

@Component({ components: { DisclaimerMockupImages } })
export default class LaboratorySmtaSubmission extends Vue {
  mounted() {
    AppModule.HideLoading();
  }
}
</script>
