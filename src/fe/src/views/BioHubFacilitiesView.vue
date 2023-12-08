<template>
  <div class="bslLevels">
    <bio-hub-facilities-page-index />
  </div>
</template>

<script lang="ts">
import BioHubFacilitiesPageIndex from "@/features/biohubfacilities/BioHubFacilitiesPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { BioHubFacilitiesPageIndex } })
export default class BioHubFacilitiesView extends Vue {}
</script>
