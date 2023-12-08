<template>
  <v-file-input
    :value="Model"
    :prepend-icon="prependIcon"
    :accept="accept"
    :show-size="showSize"
    :counter="counter"
    :multiple="multiple"
    :label="label"
    :readonly="readonly"
    :rules="rules"
    :error-messages="propertyErrors"
    :error-count="propertyErrors.length"
    :error="isError"
    @change="updateValue($event)"
  ></v-file-input>
</template>

<script lang="ts">
import { Vue, Component, Prop, Model } from "vue-property-decorator";

@Component({})
export default class FileInput extends Vue {
  @Model("input") Model!: any;

  @Prop() readonly model!: any;
  @Prop({ type: String, default: "" }) readonly propertyName!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: String, default: "" }) readonly label!: string;
  @Prop({ type: String, default: "" }) readonly prependIcon!: string;
  @Prop({ type: Array, default: [] }) readonly rules!: Array<any>;
  @Prop({
    type: String,
    default: ".doc, .docx, .xls, .xlsx, .ppt, .pptx, .pdf, .jpeg, .jpg, .mp4",
  })
  readonly accept!: string;
  @Prop({ type: Boolean, default: false }) readonly multiple!: boolean;
  @Prop({ type: Boolean, default: false }) readonly counter!: boolean;
  @Prop({ type: Boolean, default: false }) readonly showSize!: boolean;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  updateValue(value: File | File[]): void {
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
div.v-messages__message {
  color: red;
}
</style>
