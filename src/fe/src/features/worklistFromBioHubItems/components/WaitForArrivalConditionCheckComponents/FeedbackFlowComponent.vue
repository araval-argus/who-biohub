<template>
  <div v-if="model && model.length > 0">
    <h2 class="mb-10">Communication about Shared Material</h2>
    <div v-for="feedback in model" :key="feedback">
      <v-card>
        <v-card-text class="mb-8">
          <text-area v-model="feedback.Text" label="" readonly></text-area>
          <h3>
            Posted by: {{ feedback.PostedBy }} | date
            {{ getFormatDate(feedback.Date) }}
          </h3>
        </v-card-text>
        <v-card
          class="text-center d-flex flex-column align-center justify-center"
          height="100%"
        >
          <div>
            <v-icon size="70" style="color: rgb(0, 154, 222)"
              >mdi-arrow-down-bold</v-icon
            >
          </div>
        </v-card>
      </v-card>
    </div>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";
import TextField from "@/components/TextField.vue";
import { WorklistFromBioHubItemFeedback } from "@/models/WorklistFromBioHubItemFeedback";

@Component({
  components: {
    TextArea,
    TextField,
  },
})
export default class FeedbackFlowComponent extends Vue {
  //private feedbackItem = 0;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object })
  model!: Array<WorklistFromBioHubItemFeedback>;

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
  //   this.feedbackItem = this.feedbackItem + 1;
  // }

  // addArrow(): boolean {
  //   this.incrementNumber();
  //   if (this.feedbackItem < this.totalFeedbacks) {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // }

  // get totalFeedbacks(): number {
  //   return this.model.length;
  // }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
