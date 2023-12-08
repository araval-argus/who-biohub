<template>
  <div>
    <v-card>
      <v-card-text class="mb-8">
        <text-area v-model="model.Text" label="" readonly></text-area>
        <h3>
          Posted by: {{ model.PostedBy }} | date
          {{ getFormatDate(model.Date) }}
        </h3>
      </v-card-text>
      <v-card
        class="text-center d-flex flex-column align-center justify-center"
        height="100%"
      >
        <div v-if="showArrow">
          <v-icon size="70" style="color: rgb(0, 154, 222)"
            >mdi-arrow-down-bold</v-icon
          >
        </div>
      </v-card>
    </v-card>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";
import TextField from "@/components/TextField.vue";
import { WorklistFromBioHubItemBiosafetyChecklistThreadComment } from "@/models/WorklistFromBioHubItemBiosafetyChecklistThreadComment";

@Component({
  components: {
    TextArea,
    TextField,
  },
})
export default class BiosafetyChecklistCommentFlowComponent extends Vue {
  //private commentItem = 0;

  $refs!: {
    form: any;
  };

  @Prop({ required: true, type: Boolean, default: false })
  readonly showArrow: boolean;

  // Model
  @Model("update", { type: Object })
  model!: WorklistFromBioHubItemBiosafetyChecklistThreadComment;

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();
    const hour = parsedDate.getHours().toString().padStart(2, "0");
    const minutes = parsedDate.getMinutes().toString().padStart(2, "0");

    return day + "/" + month + "/" + year + " " + hour + ":" + minutes;
  }

  // incrementNumber() {
  //   this.commentItem = this.commentItem + 1;
  // }

  // addArrow(): boolean {
  //   this.incrementNumber();
  //   if (this.commentItem < this.totalComments) {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // }

  // get totalComments(): number {
  //   return this.model.length;
  // }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
