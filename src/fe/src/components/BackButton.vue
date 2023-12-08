<template>
  <v-btn class="mr-2" small fab color="primary" @click="back">
    <v-icon> mdi-arrow-left </v-icon>
  </v-btn>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({ components: {} })
export default class BackButton extends Vue {
  back(): void {
    this.$emit("back");
    this.$router.go(-1);
  }
}
</script>
