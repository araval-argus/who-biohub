<template>
  <v-radio-group
    :value="Model"
    :readonly="readonly"
    :prepend-icon="prependIcon"
    :error-messages="propertyErrors"
    :error-count="propertyErrors.length"
    :error="isError"
    @change="updateValue($event)"
  >
    <v-radio :key="0" label="Waiting for Verification" :value="0"></v-radio>
    <v-radio :key="1" label="Verify" :value="1"></v-radio>
    <v-radio :key="2" label="Unverify" :value="2"></v-radio>
  </v-radio-group>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import { MaterialValidationSelection } from "@/models/enums/MaterialValidationSelection";

@Component({
  components: {},
})
export default class MaterialValidationSelectionComponent extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private IsProviderBioHubFacility = false;

  $refs!: {
    form: any;
  };

  // Props

  @Prop({ type: String, default: "" }) readonly propertyName!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: String, default: "" }) readonly label!: string;
  @Prop({ type: String, default: "" }) readonly prependIcon!: string;
  @Prop({ type: Array, default: [] }) readonly rules!: Array<any>;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  // Model
  @Model("input", { type: Object }) Model!: MaterialValidationSelection;

  validate() {
    this.$refs.form.validate();
  }

  // Events
  updateValue(value: string | null): void {
    this.$emit("input", value);
  }

  get propertyErrors(): Array<string> {
    if (
      this.propertiesErrors != undefined &&
      this.propertiesErrors.size > 0 &&
      this.propertiesErrors.get(this.propertyName) != undefined
    ) {
      const errors = this.propertiesErrors.get(this.propertyName);
      if (errors != undefined) {
        console.log(errors);
        return errors;
      }
      return [];
    }
    return [];
  }

  get isError(): boolean {
    const errors = this.propertiesErrors?.get(this.propertyName);
    if (errors != undefined) {
      return true;
    }
    return false;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
