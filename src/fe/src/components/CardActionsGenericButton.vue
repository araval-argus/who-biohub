<template>
  <div :class="alignment">
    <v-btn :color="color" @click="click">
      {{ text }}
    </v-btn>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({ components: {} })
export default class CardActionsGenericButton extends Vue {
  @Prop({ type: String, default: "" }) readonly text!: string;
  @Prop({ type: String, default: "success" }) readonly color!: string;
  @Prop({ type: String, default: "text-right" }) readonly alignment!: string;

  click(item: unknown): void {
    this.$emit("click", item);
  }
}
</script>
