<template>
  <BslLevelForm
    v-if="isBSLLevelSet"
    ref="bslLevelForm"
    v-model="BSLLevel"
    title="Biosafety Level Edit"
  >
    <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
    </CardActionsSaveCancel>
  </BslLevelForm>
  <div v-else><span>No Biosafety Level selected</span></div>
</template>

<script lang="ts">
import BslLevelForm from "./components/BslLevelForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { BSLLevelModule } from "./store";
import { BSLLevel } from "@/models/BSLLevel";

@Component({ components: { BslLevelForm, CardActionsSaveCancel } })
export default class BslLevelsPageEdit extends Vue {
  $refs!: {
    bslLevelForm: BslLevelForm;
  };

  get isBSLLevelSet(): boolean {
    return BSLLevelModule.BSLLevel !== undefined;
  }

  get BSLLevel(): BSLLevel {
    const lab = BSLLevelModule.BSLLevel;
    if (lab) return lab;

    throw { message: "no BSLLevel selected" };
  }

  set BSLLevel(lab: BSLLevel) {
    BSLLevelModule.SET_BSLLEVEL(lab);
  }

  async onSave(): Promise<void> {
    this.$refs.bslLevelForm.validate();
    await BSLLevelModule.UpdateBSLLevel()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    BSLLevelModule.SET_ERROR(undefined);
    this.$router.back();
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
