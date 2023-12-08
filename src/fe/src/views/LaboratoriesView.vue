<template>
  <v-container fluid class="laboratories">
    <laboratories-page-index />
  </v-container>
</template>

<script lang="ts">
import LaboratoriesPageIndex from "@/features/laboratories/LaboratoriesPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { LaboratoriesPageIndex } })
export default class LaboratoriesView extends Vue {}
</script>
