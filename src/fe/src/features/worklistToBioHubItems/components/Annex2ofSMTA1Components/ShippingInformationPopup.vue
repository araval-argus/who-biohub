<template>
  <v-dialog v-model="popup" max-width="500px">
    <v-card>
      <v-card-title></v-card-title>
      <v-card-text class="text-break">
        <ul>
          <li>
            <strong>Category A</strong> infectious substances are considered
            <strong
              >capable of causing permanent disability, life-threatening or
              fatal disease</strong
            >
            in otherwise health humans or animals.
            <strong>Category B</strong> infectious substances are
            <strong
              >considered capable of causing infection in human or animals, but
              NOT meeting the criteria for Category A</strong
            >. And, Infectious substances in <strong>Other</strong> are ones
            <strong>not meeting the criteria for both Category A and B</strong>.
          </li>
          <li>
            For more information, find
            <strong
              >"WHO Guidance on Regulations for Transport of Infectious
              Substances"</strong
            >
            by <strong>clicking the "Templates/WHO Guidance" tab</strong> on the
            left side of the page.
          </li>
        </ul>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer
        ><v-btn color="blue darken-1" text @click="close"
          >Close</v-btn
        ></v-card-actions
      >
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({ components: {} })
export default class ShippingInformationPopup extends Vue {
  private popup = false;

  public showPopup(): void {
    this.popup = true;
  }

  close() {
    this.popup = false;
  }
}
</script>
