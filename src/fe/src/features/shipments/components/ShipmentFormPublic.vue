<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton v-if="backButtonVisible" @back="onBack"></BackButton>
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation readonly class="ma-2">
        <div>
          <v-row class="mb-5">
            <v-col cols="12" md="8" lg="4">
              <text-field
                v-model="model.ReferenceNumber"
                label="Reference Number"
                readonly
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field v-model="model.From" label="From" readonly>
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field v-model="model.To" label="To" readonly> </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field
                v-model="model.StatusOfRequest"
                label="Status Of Request"
                readonly
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="4">
              <date-picker
                v-model="model.CompletedOn"
                label="Completed On"
                readonly
              >
              </date-picker>
            </v-col>
          </v-row>
        </div>
        <ShipmentMaterialsTablePublic></ShipmentMaterialsTablePublic>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import BackButton from "@/components/BackButton.vue";
import { ShipmentPublic } from "@/models/ShipmentPublic";
import ShipmentMaterialsTablePublic from "./ShipmentMaterialsTablePublic.vue";

import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import { ShipmentModule } from "../store";
import { AppModule } from "../../../store/MainStore";

@Component({
  components: {
    BackButton,
    DatePicker,
    TextField,
    ShipmentMaterialsTablePublic,
  },
})
export default class ShipmentFormPublic extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props
  @Prop({ type: Boolean, default: true })
  readonly backButtonVisible: boolean;

  @Prop({ required: true, type: String, default: "Shipment" })
  readonly title: string;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  $refs!: {
    form: any;
  };

  get model(): ShipmentPublic | undefined {
    return ShipmentModule.ShipmentPublic;
  }
  // Events
  update() {
    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
