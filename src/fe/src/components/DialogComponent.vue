<template>
  <v-dialog v-model="dialog" max-width="500px">
    <v-card>
      <v-card-title class="text-h5 text-break">{{ dialogText }}</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="close">Close</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({ components: {} })
export default class DialogComponent extends Vue {
  @Prop({ required: true, type: String, default: null })
  readonly dialogText: string;

  dialog: unknown = false;

  public showDialog(): void {
    this.dialog = true;
  }

  close() {
    this.dialog = false;
  }
}
</script>
