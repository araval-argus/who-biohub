<template>
  <div v-if="canRead">
    <h2>Biosafety Checklist Data</h2>
    <br />
    <DownloadDocumentComponent
      title="Download SMTA 2"
      :document-id="BiosafetyChecklistOfSMTA2.SMTA2DocumentId"
      :document-name="BiosafetyChecklistOfSMTA2.SMTA2DocumentName"
      @downloadDocument="downloadSMTA2"
    ></DownloadDocumentComponent>

    <DownloadDocumentComponent
      title="Download Biosafety Checklist of SMTA 2 template"
      :document-id="
        BiosafetyChecklistOfSMTA2.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId
      "
      :document-name="
        BiosafetyChecklistOfSMTA2.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentName
      "
      @downloadDocument="downloadBiosafetyChecklistOfSMTA2Template"
    ></DownloadDocumentComponent>
    <BiosafetyChecklistOfSMTA2DataForm
      :can-read="canRead"
    ></BiosafetyChecklistOfSMTA2DataForm>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import BiosafetyChecklistOfSMTA2DataForm from "./components/BiosafetyChecklistOfSMTA2DataForm.vue";
import DownloadDocumentComponent from "./components/DownloadDocumentComponent.vue";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { EFormModule } from "./store";

import { DocumentModule } from "../documents/store";
import { DocumentTemplateModule } from "../documentTemplates/store";
import { BiosafetyChecklistOfSMTA2Data } from "@/models/BiosafetyChecklistOfSMTA2Data";

@Component({
  components: { BiosafetyChecklistOfSMTA2DataForm, DownloadDocumentComponent },
})
export default class BiosafetyChecklistOfSMTA2DataPage extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadEForm);
  }

  get BiosafetyChecklistOfSMTA2(): BiosafetyChecklistOfSMTA2Data | undefined {
    return EFormModule.BiosafetyChecklistOfSMTA2;
  }

  async downloadSMTA2(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentModule.ReadDocumentForShipmentRequest(info);
  }

  async downloadBiosafetyChecklistOfSMTA2Template(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentTemplateModule.ReadTemplateForEForm(info);
  }

  async loadPageInfo() {
    await EFormModule.ReadBiosafetyChecklistOfSMTA2(this.$route.params.id);
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
