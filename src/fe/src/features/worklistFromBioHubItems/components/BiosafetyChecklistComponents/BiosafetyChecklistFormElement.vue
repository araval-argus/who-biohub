<template>
  <div v-if="model.IsParentCondition !== true">
    <v-row v-show="model.IsVisible" :class="BiosafetyChecklistConditionClass">
      <v-col class="biosafety-checklist-error" cols="12"
        ><h4 v-if="model.Mandatory && model.Flag !== true">
          {{ mandatoryMessageText }}
        </h4></v-col
      >
      <v-col cols="12">
        <Checkbox
          v-model="model.Flag"
          :readonly="!canEdit || model.Selectable !== true"
          label=""
          @change="update"
        ></Checkbox>
        <div
          class="biosafety-checklist-condition"
          v-html="verifyHtml(model.Condition)"
        ></div>
      </v-col>
    </v-row>
    <v-divider></v-divider>
  </div>
  <div v-else>
    <v-row class="biosafety-checklist-parent">
      <v-col class="biosafety-checklist-error" cols="12">
        <h4 v-if="model.Mandatory && model.Flag !== true">
          {{ mandatoryMessageText }}
        </h4>
      </v-col>
      <v-col class="biosafety-checklist-parent" cols="12">
        <div
          v-html="verifyHtml(model.Condition)"
          class="biosafety-checklist-condition"
        ></div>
        <Checkbox
          v-model="yesFlag"
          :readonly="!canEdit || model.Selectable !== true"
          label="Yes"
          @change="updateYes"
        ></Checkbox>
        <Checkbox
          v-model="noFlag"
          :readonly="!canEdit || model.Selectable !== true"
          label="No"
          @change="updateNo"
        ></Checkbox>
      </v-col>
    </v-row>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import { WorklistFromBioHubItemModule } from "../../store";
import { MaterialModule } from "../../../materials/store";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import TextField from "@/components/TextField.vue";
import Checkbox from "@/components/Checkbox.vue";

import { WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 } from "@/models/WorklistFromBioHubItemBiosafetyChecklistOfSMTA2";

@Component({
  components: {
    CardActionsGenericButton,
    TextField,
    Checkbox,
  },
})
export default class BiosafetyChecklistFormElement extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private yesFlag = false;
  private noFlag = false;

  private unallowedElements = [
    "<img",
    "<audio",
    "<video",
    "<picture",
    "<svg",
    "<object",
    "<map",
    "<iframe",
    "<embed",
    "window.open",
    "http://",
    "src",
    "ftp",
    "mailto",
    "<cite",
    "<link",
  ];

  // Props

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: String, default: "Please accept" })
  readonly mandatoryMessageText: string;

  $refs!: {
    form: any;
  };

  @Model("update", { type: Object })
  model!: WorklistFromBioHubItemBiosafetyChecklistOfSMTA2;

  get ParentFlag(): boolean | null {
    const parentElement:
      | WorklistFromBioHubItemBiosafetyChecklistOfSMTA2
      | undefined =
      WorklistFromBioHubItemModule.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.find(
        (x: WorklistFromBioHubItemBiosafetyChecklistOfSMTA2) =>
          x.Id == this.model.ParentConditionId
      );

    if (parentElement == undefined) {
      return false;
    }
    return parentElement.Flag;
  }

  get BiosafetyChecklistConditionClass(): string {
    if (this.model.ParentConditionId !== null) {
      return "biosafety-checklist-child";
    } else {
      return "biosafety-checklist";
    }
  }

  verifyHtml(html: string): string {
    let found = false;
    const stringToCheck = html.toLowerCase();
    this.unallowedElements.forEach((elem) => {
      if (stringToCheck.indexOf(elem) != -1) {
        found = true;
      }
    });

    if (found == false) {
      for (let i = 0; i < html.length - 26; i++) {
        if (
          stringToCheck.substring(i, i + 4) == "href" &&
          stringToCheck.substring(i, i + 26) != "href='https://www.who.int/"
        ) {
          found = true;
        }
      }
    }
    if (found == true) {
      return "";
    } else {
      return html;
    }
  }

  update() {
    this.$emit("updateBiosafetyChecklist", this.model);
  }

  updateYes() {
    this.noFlag = false;
    this.model.Flag = this.yesFlag == true ? true : null;
    this.$emit("updateBiosafetyChecklist", this.model);
  }

  updateNo() {
    this.yesFlag = false;
    this.model.Flag = this.noFlag == true ? false : null;
    this.$emit("updateBiosafetyChecklist", this.model);
  }

  mounted() {
    if (this.model.IsParentCondition == true) {
      this.yesFlag = this.model.Flag == true;
      this.noFlag = this.model.Flag == false;
    }
  }

  // Events
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
