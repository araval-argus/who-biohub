<template>
  <v-container v-if="canCreate" fluid>
    <BioHubFacilityForm
      ref="bioHubFacilityForm"
      v-model="bioHubFacility"
      title="Create BioHub Facility"
    >
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </BioHubFacilityForm>
  </v-container>
</template>

<script lang="ts">
import BioHubFacilityForm from "./components/BioHubFacilityForm.vue";
import CardActionsSaveCancel from "@/components/CardActionsSaveCancel.vue";
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

  get bioHubFacility(): BioHubFacility {
    return BioHubFacilityModule.BioHubFacilityCreate;
  }

  set bioHubFacility(bioHubFacility: BioHubFacility) {
    BioHubFacilityModule.SET_BIOHUBFACILITY_CREATE(bioHubFacility);
  }

  async onSave(): Promise<void> {
    this.$refs.bioHubFacilityForm.validate();
    await BioHubFacilityModule.CreateBioHubFacility()
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
    BioHubFacilityModule.CLEAR_BIOHUBFACILITY_CREATE();
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
