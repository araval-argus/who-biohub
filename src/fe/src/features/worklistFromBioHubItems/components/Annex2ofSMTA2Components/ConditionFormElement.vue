<template>
  <v-row>
    <v-col cols="12" md="5" lg="5">
      <v-row>
        <v-col cols="12" md="12" lg="12">
          <div v-html="verifyHtml(model.Condition)"></div>
        </v-col>
      </v-row>
    </v-col>
    <v-col cols="12" md="2" lg="2">
      <v-switch
        v-model="model.Flag"
        label=""
        :readonly="!canEdit || model.Selectable !== true"
        @change="update"
      ></v-switch>

      <h4 v-if="model.Mandatory && model.Flag !== true" style="color: red">
        Please Accept
      </h4>
    </v-col>
    <v-col cols="12" md="5" lg="5">
      <text-area
        v-model="model.Comment"
        label="Comment"
        :readonly="!canEdit"
        property-name="Comment"
        @input="update"
      >
      </text-area
    ></v-col>
  </v-row>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import { MaterialModule } from "../../../materials/store";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import TextArea from "@/components/TextArea.vue";
import { WorklistFromBioHubItemAnnex2OfSMTA2Condition } from "@/models/WorklistFromBioHubItemAnnex2OfSMTA2Condition";

@Component({
  components: {
    CardActionsGenericButton,
    TextArea,
  },
})
export default class ConditionFormElement extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

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

  $refs!: {
    form: any;
  };

  @Model("update", { type: Object })
  model!: WorklistFromBioHubItemAnnex2OfSMTA2Condition;

  update() {
    this.$emit("updateAnnex2OfSMTA2Condition", this.model);
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

  // Events
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
