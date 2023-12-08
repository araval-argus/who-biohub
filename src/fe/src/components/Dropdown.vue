<template>
  <v-select
    :value="Model"
    :label="label"
    :readonly="readonly"
    :error-messages="propertyErrors"
    :error-count="propertyErrors.length"
    :error="isError"
    :prepend-icon="prependIcon"
    :rules="rules"
    :items="items"
    :item-text="itemText"
    :item-value="itemValue"
    :menu-props="menuProps"
    @change="change($event)"
  >
    <template v-if="secondaryProperty != ''" #selection="{ item }">
      <span style="white-space: pre-line">
        <br />
        <div>{{ item[itemText] }}</div>
        <small>{{ item[secondaryProperty] }}</small>
      </span>
    </template>
    <template v-if="secondaryProperty != ''" #item="{ item }">
      <span style="white-space: pre-line">
        <br />
        <div>{{ item[itemText] }}</div>
        <small>{{ item[secondaryProperty] }}</small>
      </span>
    </template>
  </v-select>
</template>

<script lang="ts">
import { Vue, Component, Prop, Model } from "vue-property-decorator";

@Component({})
export default class Dropdown extends Vue {
  @Model("change", { type: String }) Model!: string;

  @Prop({ type: String, default: "" }) readonly model!: string;
  @Prop({ type: String, default: "" }) readonly propertyName!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: String, default: "" }) readonly label!: string;
  @Prop({ type: String, default: "" }) readonly prependIcon!: string;
  @Prop({ type: Array, default: [] }) readonly rules!: Array<any>;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  @Prop({ type: Array, default: [] }) readonly items!: Array<any>;
  @Prop({ type: String, default: "" }) readonly itemText!: string;
  @Prop({ type: String, default: "" }) readonly itemValue!: string;
  @Prop({ type: Object, default: {} }) readonly menuProps!: object;

  @Prop({ type: String, default: "" }) readonly secondaryProperty!: string;

  change(value: any): void {
    this.$emit("change", value);
  }

  get propertyErrors(): string | Array<string> {
    if (
      this.propertiesErrors != undefined &&
      this.propertiesErrors.size > 0 &&
      this.propertiesErrors.get(this.propertyName) != undefined
    ) {
      const errors = this.propertiesErrors.get(this.propertyName);
      if (errors != undefined) {
        return errors;
      }
      return "";
    }
    return "";
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
