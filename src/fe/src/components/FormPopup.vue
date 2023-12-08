<template>
  <v-dialog
    ref="formPopupComponent"
    v-model="dialog"
    max-width="500px"
    :persistent="true"
  >
    <v-card>
      <v-card-title class="text-h5">{{ title }}</v-card-title>
      <v-card-text class="mt-5">
        <v-row>
          <v-col
            v-for="item in model"
            :key="item.PropertyName"
            :hidden="item.Hide"
            cols="12"
            class="py-0"
          >
            <text-field
              v-if="item.Type == 0 || item.Type == 1"
              v-model="item.Value"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @input="update"
            >
            </text-field>
            <text-area
              v-if="item.Type == 2"
              v-model="item.Value"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @input="update"
            >
            </text-area>
            <text-editor
              v-if="item.Type == 3"
              v-model="item.Value"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @input="update"
            >
            </text-editor>
            <file-input
              v-if="item.Type == 4"
              v-model="item.Value"
              :show-size="true"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @input="update"
            >
            </file-input>
            <file-input
              v-if="item.Type == 5"
              v-model="item.Value"
              :show-size="true"
              :multiple="true"
              :counter="true"
              :accept="accept"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @input="update"
            >
            </file-input>
            <dropdown
              v-if="item.Type == 6"
              v-model="item.Value"
              item-value="Value"
              item-text="Text"
              :items="item.Items"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @change="update"
            ></dropdown>
            <checkbox
              v-if="item.Type == 7"
              v-model="item.Value"
              :label="item.Label"
              :readonly="item.Readonly"
              :properties-errors="allPropertiesErrors"
              :property-name="item.PropertyName"
              @change="update"
            >
            </checkbox>
          </v-col>
        </v-row>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="cancel">Cancel</v-btn>
        <v-btn color="blue darken-1" :disabled="!isOkEnable" text @click="save"
          >OK</v-btn
        >
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { FormPopupItem } from "@/models/FormPopupItem";
import { RefreshTokenClient } from "@azure/msal-common";
import { Component, Vue, Prop, Watch, Model } from "vue-property-decorator";
import Checkbox from "./Checkbox.vue";
import Dropdown from "./Dropdown.vue";
import FileInput from "./FileInput.vue";
import TextArea from "./TextArea.vue";
import TextEditor from "./TextEditor.vue";
import TextField from "./TextField.vue";

@Component({
  components: {
    TextArea,
    TextEditor,
    TextField,
    FileInput,
    Dropdown,
    Checkbox,
  },
})
export default class FormPopup extends Vue {
  @Model("update", { type: Array }) model!: Array<FormPopupItem>;

  @Prop({ type: String, default: "" }) readonly title: string;
  @Prop({ type: Array, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;
  @Prop({
    type: String,
    default: ".doc, .docx, .xls, .xlsx, .ppt, .pptx, .pdf, .jpeg, .jpg, .mp4",
  })
  readonly accept!: string;

  dialog: unknown = false;

  get isOkEnable(): boolean {
    return this.fieldsErrors.size == 0;
  }

  get allPropertiesErrors(): Map<string, Array<string>> {
    let result = this.propertiesErrors ?? new Map<string, Array<string>>();

    this.fieldsErrors.forEach((value, key, map) => {
      result.set(key, value);
    });

    return result;
  }

  get fieldsErrors(): Map<string, Array<string>> {
    let result = new Map<string, Array<string>>();

    for (let i = 0; i < this.model.length; i++) {
      let item = this.model[i];
      if (
        item.Required &&
        (item.Value == undefined ||
          (typeof item.Value == "string" && item.Value == ""))
      ) {
        result.set(item.PropertyName, [item.Label + " is required"]);
      } else if (
        item.Value != undefined &&
        (item.Value as File).name &&
        this.accept
          .split(", ")
          .findIndex((a) => (item.Value as File).name.includes(a)) == -1
      ) {
        result.set(item.PropertyName, [
          "The extensions allowed are: " + this.accept,
        ]);
      }
    }

    return result;
  }

  public showPopup(): void {
    this.dialog = true;
  }

  update() {
    this.$emit("update", this.model);
  }

  cancel() {
    this.dialog = false;
  }

  save() {
    if (this.fieldsErrors.size == 0) {
      this.$emit("executeSave", this.model);
      this.cancel();
    }
  }

  @Watch("dialog")
  dialogChanged(val: boolean) {
    this.dialog = val;
  }
}
</script>
