<template>
  <v-container fluid class="resources">
    <resources-page-index />
  </v-container>
</template>

<script lang="ts">
import ResourcesPageIndex from "@/features/resources/ResourcesPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { ResourcesPageIndex } })
export default class ResourcesView extends Vue {}
</script>
