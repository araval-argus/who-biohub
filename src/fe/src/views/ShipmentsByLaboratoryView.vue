<template>
  <v-container fluid class="materials">
    <shipments-page-index />
  </v-container>
</template>

<script lang="ts">
import ShipmentsPageIndex from "@/features/shipments/ShipmentsPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { ShipmentsPageIndex } })
export default class ShipmentsByLaboratoryView extends Vue {}
</script>
