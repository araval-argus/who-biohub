<template>
  <BioHubFacilityForm
    v-if="isBioHubFacilitySet && canEdit"
    ref="bioHubFacilityForm"
    v-model="bioHubFacility"
    title="BioHub Facility Edit"
  >
    <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
    </CardActionsSaveCancel>
  </BioHubFacilityForm>
  <div v-else><span>No BioHubFacility selected</span></div>
</template>

<script lang="ts">
import BioHubFacilityForm from "./components/BioHubFacilityForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { BioHubFacilityModule } from "./store";
import { BioHubFacility } from "@/models/BioHubFacility";
import { BSLLevelModule } from "./../bsllevels/store";
import { CountryModule } from "./../countries/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { BioHubFacilityForm, CardActionsSaveCancel } })
export default class BioHubFacilitiesPageEdit extends Vue {
  $refs!: {
    bioHubFacilityForm: BioHubFacilityForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadBioHubFacility);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateBioHubFacility);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditBioHubFacility);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteBioHubFacility);
  }

  get isBioHubFacilitySet(): boolean {
    return BioHubFacilityModule.BioHubFacility !== undefined;
  }

  get bioHubFacility(): BioHubFacility {
    const lab = BioHubFacilityModule.BioHubFacility;
    if (lab) return lab;

    throw { message: "no bioHubFacility selected" };
  }

  set bioHubFacility(lab: BioHubFacility) {
    BioHubFacilityModule.SET_BIOHUBFACILITY(lab);
  }

  async onSave(): Promise<void> {
    this.$refs.bioHubFacilityForm.validate();
    await BioHubFacilityModule.UpdateBioHubFacility()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    BioHubFacilityModule.SET_ERROR(undefined);
    this.$router.back();
  }

  async loadPageInfo() {
    await BSLLevelModule.ListBSLLevels();
    await CountryModule.ListCountries();
    await BioHubFacilityModule.ReadBioHubFacility(this.$route.params.id);
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
