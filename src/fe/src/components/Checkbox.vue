<template>
  <v-checkbox
    v-model="Model"
    :label="label"
    :readonly="readonly"
    :error-messages="propertyErrors"
    :error-count="propertyErrors.length"
    :prepend-icon="prependIcon"
    :error="isError"
    :rules="rules"
    @change="change($event)"
  >
  </v-checkbox>
</template>

<script lang="ts">
import { Vue, Component, Prop, Model } from "vue-property-decorator";

@Component({})
export default class Checkbox extends Vue {
  @Model("change", { type: Boolean }) Model!: boolean;

  @Prop({ type: String, default: "" }) readonly model!: string;
  @Prop({ type: String, default: "" }) readonly propertyName!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: String, default: "" }) readonly label!: string;
  @Prop({ type: String, default: "" }) readonly prependIcon!: string;
  @Prop({ type: Array, default: [] }) readonly rules!: Array<any>;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  change(value: boolean): void {
    this.$emit("change", value);
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
div.v-messages__message {
  color: red;
}
</style>
