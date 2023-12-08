<template>
  <div v-if="canRead">
    <h2>Annex 2 of SMTA 1 Data</h2>
    <br />
    <DownloadDocumentComponent
      title="Download SMTA 1"
      :document-id="Annex2OfSMTA1.SMTA1DocumentId"
      :document-name="Annex2OfSMTA1.SMTA1DocumentName"
      @downloadDocument="downloadSMTA1"
    ></DownloadDocumentComponent>

    <DownloadDocumentComponent
      title="Download Annex 2 of SMTA 1 template"
      :document-id="
        Annex2OfSMTA1.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId
      "
      :document-name="
        Annex2OfSMTA1.OriginalDocumentTemplateAnnex2OfSMTA1DocumentName
      "
      @downloadDocument="downloadAnnex2OfSMTA1Template"
    ></DownloadDocumentComponent>
    <Annex2OfSMTA1DataForm :can-read="canRead"></Annex2OfSMTA1DataForm>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import Annex2OfSMTA1DataForm from "./components/Annex2OfSMTA1DataForm.vue";
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
import { Annex2OfSMTA1Data } from "@/models/Annex2OfSMTA1Data";

@Component({
  components: { Annex2OfSMTA1DataForm, DownloadDocumentComponent },
})
export default class Annex2OfSMTA1DataPage extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadEForm);
  }

  get Annex2OfSMTA1(): Annex2OfSMTA1Data | undefined {
    return EFormModule.Annex2OfSMTA1;
  }

  async downloadSMTA1(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentModule.ReadDocumentForShipmentRequest(info);
  }

  async downloadAnnex2OfSMTA1Template(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentTemplateModule.ReadTemplateForEForm(info);
  }

  async loadPageInfo() {
    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();

    await GeneticSequenceDataModule.ListGeneticSequenceDatas();
    await SpecimenTypeModule.ListSpecimenTypes();
    await TemperatureUnitOfMeasureModule.ListTemperatureUnitOfMeasures();

    await EFormModule.ReadAnnex2OfSMTA1(this.$route.params.id);
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
