<template>
  <v-dialog v-model="confirmationDialog" max-width="500px">
    <v-card>
      <v-card-title class="text-h5 text-break">{{ message }}</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="close">Cancel</v-btn>
        <v-btn color="blue darken-1" text @click="confirm">OK</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";

@Component({ components: {} })
export default class ConfirmationDialogComponent extends Vue {
  @Prop({
    required: true,
    type: String,
    default: "Are you sure you want to delete this item?",
  })
  readonly message: string;

  confirmationDialog: unknown = false;
  private editedItem: unknown;

  public showDialog(item: unknown): void {
    this.editedItem = Object.assign({}, item);
    this.confirmationDialog = true;
  }

  confirm() {
    this.$emit("onConfirm", this.editedItem);
    this.close();
  }

  close() {
    this.confirmationDialog = false;
    this.$nextTick(() => {
      this.editedItem = Object.assign({}, null);
    });
  }

  @Watch("confirmationDialog")
  confirmationDialogChanged(val: boolean) {
    this.confirmationDialog = val;
  }
}
</script>
