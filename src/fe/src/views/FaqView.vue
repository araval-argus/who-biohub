<template>
  <!-- <v-container fluid class="faq"> -->
  <faq-page class="faq" />
  <!-- </v-container> -->
</template>

<script lang="ts">
import FaqPage from "@/features/faq/FaqPage.vue";
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../store/MainStore";

@Component({ components: { FaqPage } })
export default class FaqView extends Vue {
  mounted() {
    AppModule.HideLoading();
  }
}
</script>
<style>
.faq {
  overflow-y: hidden; /* Hide scrollbars */
  overflow-x: auto;
}
</style>
