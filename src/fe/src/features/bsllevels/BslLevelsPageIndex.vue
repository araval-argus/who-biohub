<template>
  <div>
    <span v-if="error"> Error retrieving Biosafety Levels: {{ error }} </span>
    <BslLevelTable
      v-else
      :loading="loading"
      @selected="selected"
      @create="create"
      @edit="editItem"
      @delete="deleteItem"
    >
    </BslLevelTable>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BslLevelTable from "./components/BslLevelsTable.vue";
import { BSLLevelModule } from "./store";
import { AppError } from "@/models/shared/Error";
import { BSLLevel } from "@/models/BSLLevel";
import { AppModule } from "../../store/MainStore";

@Component({ components: { BslLevelTable } })
export default class BslLevelsPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  async mounted() {
    try {
      await BSLLevelModule.ListBSLLevels();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get error(): AppError | undefined {
    return BSLLevelModule.Error;
  }

  editItem(item: BSLLevel): void {
    BSLLevelModule.SET_BSLLEVEL(item);
    this.$router.push({
      name: "whoarea-bsllevel-edit",
      params: { id: item.Id },
    });
  }

  selected(item: BSLLevel): void {
    BSLLevelModule.SET_BSLLEVEL(item);

    this.$router.push({
      name: "whoarea-bsllevel-details",
      params: { id: item.Id },
    });
  }

  create(): void {
    this.$router.push({
      name: "whoarea-bsllevel-create",
    });
  }

  async deleteItem(item: BSLLevel): Promise<void> {
    BSLLevelModule.SET_BSLLEVEL(item);
    await BSLLevelModule.DeleteBSLLevel();
    await BSLLevelModule.ListBSLLevels();
    return;
  }
}
</script>
