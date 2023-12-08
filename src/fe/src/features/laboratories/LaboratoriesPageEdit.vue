<template>
  <LaboratoryForm
    v-if="canEdit"
    ref="laboratoryForm"
    v-model="laboratory"
    :laboratory-area="LaboratoryArea"
    :show-bsl-level="ShowBslLevel"
    title="Laboratory Edit"
  >
    <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
    </CardActionsSaveCancel>
  </LaboratoryForm>
</template>

<script lang="ts">
import LaboratoryForm from "./components/LaboratoryForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { LaboratoryModule } from "./store";
import { Laboratory } from "@/models/Laboratory";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";
import { AuthModule } from "../auth/store";
import { AppModule } from "../../store/MainStore";
import { BSLLevelModule } from "./../bsllevels/store";
import { CountryModule } from "./../countries/store";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { LaboratoryForm, CardActionsSaveCancel } })
export default class LaboratoriesPageEdit extends Vue {
  $refs!: {
    laboratoryForm: LaboratoryForm;
  };

  get LaboratoryId(): string {
    return getUserBioHubFacilityOrLaboratoryOrCourierId(this.$route.params.id);
  }

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

  get isLaboratorySet(): boolean {
    return LaboratoryModule.Laboratory !== undefined;
  }

  get LaboratoryArea(): boolean {
    return this.$route.name === "laboratoryarea-private-page-edit";
  }

  get ShowBslLevel(): boolean {
    return this.$route.name != "laboratoryarea-private-page-edit";
  }

  get laboratory(): Laboratory {
    const lab = LaboratoryModule.Laboratory;
    if (lab) return lab;

    throw { message: "no laboratory selected" };
  }

  set laboratory(lab: Laboratory) {
    LaboratoryModule.SET_LABORATORY(lab);
  }

  async onSave(): Promise<void> {
    this.$refs.laboratoryForm.validate();
    await LaboratoryModule.UpdateLaboratory()
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
    await LaboratoryModule.ReadLaboratory(this.LaboratoryId);
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
