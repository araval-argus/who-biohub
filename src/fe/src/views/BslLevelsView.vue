<template>
  <div class="BSLLevels">
    <bsl-levels-page-index />
  </div>
</template>

<script lang="ts">
import BslLevelsPageIndex from "@/features/bsllevels/BslLevelsPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { BslLevelsPageIndex } })
export default class BslLevelsView extends Vue {}
</script>
