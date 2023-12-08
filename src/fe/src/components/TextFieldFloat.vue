<template>
  <v-text-field
    :value="Model"
    :label="label"
    :type="!decimal ? 'number' : ''"
    :dense="dense"
    :prefix="prefix"
    :suffix="suffix"
    :readonly="readonly"
    :prepend-icon="prependIcon"
    :rules="rules"
    :error-messages="propertyErrors"
    :error-count="propertyErrors.length"
    :error="isError"
    hide-details="auto"
    @input="updateValue($event)"
  >
    <template v-if="decimal && buttons" #append-outer>
      <v-btn icon small @click="decrement">
        <v-icon dark> mdi-minus </v-icon>
      </v-btn>
      <v-btn icon small @click="increment">
        <v-icon dark> mdi-plus </v-icon>
      </v-btn>
    </template>
  </v-text-field>
</template>

<script lang="ts">
import { Vue, Component, Prop, Model } from "vue-property-decorator";

@Component({})
export default class TextFieldFloat extends Vue {
  @Prop({ type: String, default: "" }) readonly label!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: Boolean, default: false }) readonly dense!: boolean;
  @Prop({ type: String, default: "" }) readonly prefix!: string;
  @Prop({ type: String, default: "" }) readonly suffix!: string;
  @Prop({ type: String, default: "" }) readonly prependIcon!: string;
  @Prop({ type: Boolean, default: false }) readonly decimal!: boolean;
  @Prop({ type: Number, default: 1 }) readonly step!: number;
  @Prop({ type: Number, default: 2 }) readonly precision!: number;
  @Prop({ type: Boolean, default: true }) readonly buttons!: boolean;
  @Prop({ type: Array, default: [] }) readonly rules!: Array<any>;

  @Prop({
    default: function () {
      new Map<string, Array<string>>();
    },
  })
  @Prop({ type: String, default: "" })
  readonly propertyName!: string;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  @Model("input", { type: Number }) Model!: number;
  updateValue(value: number | null): void {
    value = value ?? 0;
    if (this.decimal) value = this.removeInaspectedChars(value);
    if (value != null) {
      value = this.decimal
        ? this.round(parseFloat(value.toString().trim()))
        : parseInt(value.toString().trim());

      if (value.toString() == "NaN") {
        value = null;
      }
    }

    this.$emit("input", value);
  }

  private removeInaspectedChars(value: number): number | null {
    if (value != null) {
      if (isNaN(value)) {
        let newValues = new Array<string>();
        for (const v of value.toString()) {
          if (!isNaN(parseInt(v))) newValues.push(v);
          if (v === "." || v === ",") newValues.push(".");
        }
        return newValues.length > 0 ? parseFloat(newValues.join("")) : null;
      }
    }

    return value;
  }

  private increment(): void {
    const newValue = parseFloat(this.Model.toString());
    this.updateValue(newValue + this.step);
  }

  private decrement(): void {
    const newValue = parseFloat(this.Model.toString());
    this.updateValue(newValue - this.step);
  }

  private round(num: number): number {
    var exp = Math.pow(10, this.precision);
    return Math.floor(num * exp + 0.5) / exp;
  }

  get propertyErrors(): string | Array<string> {
    if (
      this.propertiesErrors !== undefined &&
      this.propertiesErrors.size > 0 &&
      this.propertiesErrors.get(this.propertyName) !== undefined
    ) {
      const errors = this.propertiesErrors.get(this.propertyName);
      if (errors !== undefined) {
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
