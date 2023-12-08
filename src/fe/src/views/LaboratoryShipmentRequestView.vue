<template>
  <v-container fluid class="laboratoryShipment">
    <v-card-subtitle>
      <disclaimer-mockup-images></disclaimer-mockup-images>
      <v-spacer></v-spacer>
    </v-card-subtitle>
    <v-card-title> Shipment Request </v-card-title>
    <v-img
      alt="Shipment Request"
      title="Shipment Request"
      src="@/assets/icons/direction-of-shipment.png"
      width="450px"
    />
    <v-spacer></v-spacer>
    <hr />
    <v-card-title>
      Shipment Request (Sending BMEPP to BioHub)
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-subtitle> Step 1: SMTA 1 uploading </v-card-subtitle>
    <v-img
      alt="SMTA 1 Upload"
      title="SMTA 1 Upload"
      src="@/assets/icons/shipment-step-1.png"
    />
    <v-spacer></v-spacer>
    <hr />
    <v-card-title>
      Shipment Request (Sending BMEPP to BioHub)
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-subtitle>
      Step 2: Upload Shipment specific documents (1)
    </v-card-subtitle>
    <v-img
      alt="Shipment documents (1)"
      title="Shipment documents (1)"
      src="@/assets/icons/shipment-step-2.png"
    />
    <v-spacer></v-spacer>
    <hr />
    <v-card-title>
      Shipment Request (Sending BMEPP to BioHub)
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-subtitle>
      Step 3: Upload Shipment specific documents (2)
    </v-card-subtitle>
    <v-img
      alt="Shipment documents (2)"
      title="Shipment documents (2)"
      src="@/assets/icons/shipment-step-3.png"
    />
    <v-spacer></v-spacer>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../store/MainStore";
import DisclaimerMockupImages from "./DisclaimerMockupImages.vue";

@Component({ components: { DisclaimerMockupImages } })
export default class LaboratoryShipmentRequestView extends Vue {
  mounted() {
    AppModule.HideLoading();
  }
}
</script>
