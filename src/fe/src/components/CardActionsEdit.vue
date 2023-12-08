<template>
  <v-row>
    <v-spacer></v-spacer>
    <v-col cols="3" class="text-right">
      <v-btn color="success" class="mr-4" @click="save"> Edit </v-btn>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({ components: {} })
export default class CardActionsEdit extends Vue {
  save(item: unknown): void {
    this.$emit("edit", item);
  }
}
</script>
