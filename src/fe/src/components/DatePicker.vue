<template>
  <v-menu
    v-model="menu"
    :close-on-content-click="false"
    :nudge-right="40"
    transition="scale-transition"
    offset-y
    min-width="auto"
  >
    <template #activator="{ on, attrs }">
      <v-text-field
        v-model="formattedDate"
        :label="label"
        readonly
        :prepend-icon="prependIcon"
        :error-messages="propertyErrors"
        :error-count="propertyErrors.length"
        :error="isError"
        v-bind="attrs"
        v-on="on"
        @input="updateValue($event)"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="pickerDate"
      :readonly="readonly"
      no-title
      @input="menu = false"
    ></v-date-picker>
  </v-menu>
</template>

<script lang="ts">
import { Vue, Component, Prop, Model } from "vue-property-decorator";

@Component({})
export default class DatePicker extends Vue {
  @Model("input", { type: Date }) Model!: Date;

  @Prop({ type: String, default: "" }) readonly propertyName!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: String, default: "" }) readonly hint!: string;
  @Prop({ type: Boolean, default: false }) readonly persistentHint!: boolean;
  @Prop({ type: String, default: "" }) readonly label!: string;

  @Prop({ type: String, default: "mdi-calendar" })
  readonly prependIcon!: string;
  @Prop({ type: Array, default: [] }) readonly rules!: Array<any>;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  menu: boolean;

  get formattedDate(): string {
    if (!this.Model) return "";

    return this.getFormatDate(this.Model);
  }

  get pickerDate(): string {
    if (!this.Model) return "";
    else this.Model = new Date(this.Model);

    return this.Model.toISOString();
  }

  set pickerDate(value) {
    let valueDate = value ? new Date(value) : new Date();
    this.$emit("input", valueDate);
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

  updateValue(value: string | null): void {
    this.$emit("input", value);
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }
}
</script>
<style lang="scss">
div.v-messages__message {
  color: red;
}
</style>
