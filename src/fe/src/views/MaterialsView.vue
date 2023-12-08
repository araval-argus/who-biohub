<template>
  <v-container fluid class="materials">
    <materials-page-index />
  </v-container>
</template>

<script lang="ts">
import MaterialsPageIndex from "@/features/materials/MaterialsPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { MaterialsPageIndex } })
export default class MaterialsView extends Vue {}
</script>
