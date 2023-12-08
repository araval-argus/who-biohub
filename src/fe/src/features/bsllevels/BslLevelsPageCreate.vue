<template>
  <v-container fluid>
    <BslLevelForm
      ref="bslLevelForm"
      v-model="BSLLevel"
      title="Create Biosafety Level"
    >
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </BslLevelForm>
  </v-container>
</template>

<script lang="ts">
import BslLevelForm from "./components/BslLevelForm.vue";
import CardActionsSaveCancel from "@/components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { BSLLevelModule } from "./store";
import { BSLLevel } from "@/models/BSLLevel";

@Component({ components: { BslLevelForm, CardActionsSaveCancel } })
export default class BslLevelsPageEdit extends Vue {
  $refs!: {
    bslLevelForm: BslLevelForm;
  };

  get BSLLevel(): BSLLevel {
    return BSLLevelModule.BSLLevelCreate;
  }

  set BSLLevel(BSLLevel: BSLLevel) {
    BSLLevelModule.SET_BSLLEVEL_CREATE(BSLLevel);
  }

  async onSave(): Promise<void> {
    this.$refs.bslLevelForm.validate();
    await BSLLevelModule.CreateBSLLevel()
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

  mounted() {
    BSLLevelModule.CLEAR_BSLLEVEL_CREATE();
  }
}
</script>
