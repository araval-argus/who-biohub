<template>
  <v-container v-if="canCreate" fluid>
    <LaboratoryForm
      ref="laboratoryForm"
      v-model="laboratory"
      title="Create Laboratory"
    >
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </LaboratoryForm>
  </v-container>
</template>

<script lang="ts">
import LaboratoryForm from "./components/LaboratoryForm.vue";
import CardActionsSaveCancel from "@/components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { LaboratoryModule } from "./store";
import { Laboratory } from "@/models/Laboratory";
import { BSLLevelModule } from "./../bsllevels/store";
import { CountryModule } from "./../countries/store";
import { AppModule } from "./../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { LaboratoryForm, CardActionsSaveCancel } })
export default class LaboratoriesPageCreate extends Vue {
  $refs!: {
    laboratoryForm: LaboratoryForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadLaboratory);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateLaboratory);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditLaboratory);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratory);
  }

  get laboratory(): Laboratory {
    return LaboratoryModule.LaboratoryCreate;
  }

  set laboratory(laboratory: Laboratory) {
    LaboratoryModule.SET_LABORATORY_CREATE(laboratory);
  }

  async onSave(): Promise<void> {
    this.$refs.laboratoryForm.validate();
    await LaboratoryModule.CreateLaboratory()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    LaboratoryModule.SET_ERROR(undefined);
    this.$router.back();
  }

  async loadPageInfo() {
    await BSLLevelModule.ListBSLLevels();
    await CountryModule.ListCountries();
    LaboratoryModule.CLEAR_LABORATORY_CREATE();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
