<template>
  <BslLevelForm
    v-if="BSLLevel"
    v-model="BSLLevel"
    title="Biosafety Level Details"
    readonly
  >
  </BslLevelForm>
  <div v-else><span>No Biosafety Level selected</span></div>
</template>

<script lang="ts">
import BslLevelForm from "./components/BslLevelForm.vue";
import { Component, Vue } from "vue-property-decorator";
import { BSLLevel } from "@/models/BSLLevel";
import { BSLLevelModule } from "./store";

@Component({ components: { BslLevelForm } })
export default class BslLevelsPageDetails extends Vue {
  get BSLLevel(): BSLLevel | undefined {
    return BSLLevelModule.BSLLevel;
  }
  async mounted() {
    try {
      await BSLLevelModule.ReadBSLLevel(this.$route.params.id);
    } finally {
      console.log("BSLLevel Page loaded");
    }
  }
}
</script>
