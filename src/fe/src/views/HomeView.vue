<template>
  <div class="home">
    <index />
  </div>
</template>

<script lang="ts">
import Index from "./Index.vue";
import { Component, Vue } from "vue-property-decorator";

@Component({ components: { Index } })
export default class HomeView extends Vue {}
</script>
