<template>
  <v-card>
    <v-card-title>
      <BackButton />
      {{ title }}
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="8" lg="4">
              <v-text-field
                v-model="model.Name"
                label="Name"
                :readonly="readonly"
                @input="update"
              >
              </v-text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <v-text-field
                v-model="model.FullName"
                label="FullName"
                :readonly="readonly"
                @input="update"
              >
              </v-text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <v-text-field
                v-model="model.Uncode"
                label="Uncode"
                :readonly="readonly"
                @input="update"
              >
              </v-text-field>
            </v-col>
            <v-col cols="12" md="12" lg="6">
              <v-text-field
                v-model="model.Iso2"
                label="Iso2"
                :readonly="readonly"
                @input="update"
              >
              </v-text-field>
            </v-col>
            <v-col cols="12" md="12" lg="6">
              <v-text-field
                v-model="model.Iso3"
                label="Iso3"
                :readonly="readonly"
                @input="update"
              >
              </v-text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.Latitude"
                label="Latitude"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.Longitude"
                label="Longitude"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.GmtHour"
                label="GmtHour"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.GmtMin"
                label="GmtMin"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="3">
              <v-checkbox
                v-model="model.IsActive"
                label="Is Active"
                :readonly="readonly"
                @change="update"
              >
              </v-checkbox>
            </v-col>
          </v-row>
        </div>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import BackButton from "@/components/BackButton.vue";
import { Country } from "@/models/Country";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import { BSLLevelModule } from "../../bsllevels/store";
import { BSLLevelGridItem } from "@/models/BSLLevelGridItem";
import { AppModule } from "../../../store/MainStore";

@Component({ components: { BackButton, TextFieldFloat } })
export default class CountriesForm extends Vue {
  // Props
  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "Country" })
  readonly title: string;

  // Model
  @Model("update", { type: Object }) model!: Country;

  // Events
  update() {
    if (this.model.Uncode.length == 1) {
      this.model.Uncode = "00" + this.model.Uncode;
    }
    if (this.model.Uncode.length == 2) {
      this.model.Uncode = "0" + this.model.Uncode;
    }
    this.$emit("update", this.model);
  }

  mounted() {
    AppModule.HideLoading();
  }
}
</script>
