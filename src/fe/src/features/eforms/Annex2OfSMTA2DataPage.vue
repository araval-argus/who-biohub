<template>
  <div v-if="canRead">
    <h2>Annex 2 of SMTA 2 Data</h2>
    <br />
    <DownloadDocumentComponent
      title="Download SMTA 2"
      :document-id="Annex2OfSMTA2.SMTA2DocumentId"
      :document-name="Annex2OfSMTA2.SMTA2DocumentName"
      @downloadDocument="downloadSMTA2"
    ></DownloadDocumentComponent>

    <DownloadDocumentComponent
      title="Download Annex 2 of SMTA 2 template"
      :document-id="
        Annex2OfSMTA2.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId
      "
      :document-name="
        Annex2OfSMTA2.OriginalDocumentTemplateAnnex2OfSMTA2DocumentName
      "
      @downloadDocument="downloadAnnex2OfSMTA2Template"
    ></DownloadDocumentComponent>
    <Annex2OfSMTA2DataForm :can-read="canRead"></Annex2OfSMTA2DataForm>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import Annex2OfSMTA2DataForm from "./components/Annex2OfSMTA2DataForm.vue";
import DownloadDocumentComponent from "./components/DownloadDocumentComponent.vue";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { EFormModule } from "./store";
import { IsolationHostTypeModule } from "../isolationHostTypes/store";
import { MaterialProductModule } from "../materialProducts/store";
import { TransportCategoryModule } from "../transportCategories/store";
import { GeneticSequenceDataModule } from "../geneticSequenceDatas/store";
import { SpecimenTypeModule } from "../specimenTypes/store";
import { TemperatureUnitOfMeasureModule } from "../temperatureUnitOfMeasures/store";
import { DocumentModule } from "../documents/store";
import { DocumentTemplateModule } from "../documentTemplates/store";
import { Annex2OfSMTA2Data } from "@/models/Annex2OfSMTA2Data";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { CountryModule } from "../countries/store";

@Component({
  components: { Annex2OfSMTA2DataForm, DownloadDocumentComponent },
})
export default class Annex2OfSMTA2DataPage extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadEForm);
  }

  get Annex2OfSMTA2(): Annex2OfSMTA2Data | undefined {
    return EFormModule.Annex2OfSMTA2;
  }

  async downloadSMTA2(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentModule.ReadDocumentForShipmentRequest(info);
  }

  async downloadAnnex2OfSMTA2Template(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentTemplateModule.ReadTemplateForEForm(info);
  }

  async loadPageInfo() {
    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();

    await EFormModule.ReadAnnex2OfSMTA2(this.$route.params.id);
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
