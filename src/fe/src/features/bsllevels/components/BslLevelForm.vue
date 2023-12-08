<template>
  <v-card>
    <v-card-title>
      <BackButton @back="onBack" />
      {{ title }}
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="8" lg="4">
              <text-field
                v-model="model.Name"
                label="Name"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Name"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.Code"
                label="Code"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Code"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field
                v-model="model.Description"
                label="Description"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Description"
                @input="update"
              >
              </text-field>
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
import { BSLLevel } from "@/models/BSLLevel";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import { BSLLevelModule } from "../store";
import { AppModule } from "../../../store/MainStore";

@Component({ components: { BackButton, TextFieldFloat, TextField } })
export default class BSLLevelsForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  $refs!: {
    form: any;
  };

  // Props
  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "Biosafety level" })
  readonly title: string;

  // Model
  @Model("update", { type: Object }) model!: BSLLevel;

  validate() {
    this.$refs.form.validate();
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  onBack() {
    BSLLevelModule.SET_ERROR(undefined);
  }

  mounted() {
    AppModule.HideLoading();
  }

  get propertiesErrors(): any {
    return BSLLevelModule.ErrorMessage;
  }
}
</script>
