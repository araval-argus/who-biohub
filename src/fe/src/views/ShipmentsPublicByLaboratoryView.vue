<template>
  <v-container fluid class="materials">
    <shipments-public-page-index />
  </v-container>
</template>

<script lang="ts">
import ShipmentsPublicPageIndex from "@/features/shipments/ShipmentsPublicPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { ShipmentsPublicPageIndex } })
export default class ShipmentsPublicByLaboratoryView extends Vue {}
</script>
