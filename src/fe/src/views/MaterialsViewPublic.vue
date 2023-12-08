<template>
  <v-container fluid class="materials">
    <materials-page-index-public />
  </v-container>
</template>

<script lang="ts">
import MaterialsPageIndexPublic from "@/features/materials/MaterialsPageIndexPublic.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { MaterialsPageIndexPublic } })
export default class MaterialsViewPublic extends Vue {}
</script>
