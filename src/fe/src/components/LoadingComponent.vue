<template>
  <v-overlay :z-index="100" :value="showLoading">
    <v-progress-circular indeterminate size="64"></v-progress-circular>
  </v-overlay>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({ components: {} })
export default class LoadingComponent extends Vue {
  @Prop({ required: true, type: Boolean, default: false })
  readonly showLoading: boolean;
}
</script>
