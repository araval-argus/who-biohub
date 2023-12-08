<template>
  <v-container fluid class="countries">
    <countries-page-index />
  </v-container>
</template>

<script lang="ts">
import CountriesPageIndex from "@/features/countries/CountriesPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { CountriesPageIndex } })
export default class CountriesView extends Vue {}
</script>
