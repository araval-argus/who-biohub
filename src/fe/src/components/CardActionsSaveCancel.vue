<template>
  <v-row>
    <v-spacer></v-spacer>
    <v-col cols="6" class="text-right">
      <v-btn color="success" class="mr-4" @click="save"> {{ saveName }} </v-btn>
      <v-btn color="error" class="mr-4" @click="cancel">
        {{ cancelName }}
      </v-btn>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({ components: {} })
export default class CardActionsSaveCancel extends Vue {
  @Prop({ type: String, default: "Save" }) readonly saveName!: string;
  @Prop({ type: String, default: "Cancel" }) readonly cancelName!: string;

  save(item: unknown): void {
    this.$emit("save", item);
  }

  cancel(): void {
    this.$emit("cancel");
  }
}
</script>
