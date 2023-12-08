<template>
  <div v-if="canRead">
    <h2>Booking Form of SMTA 2 Data</h2>
    <br />
    <DownloadDocumentComponent
      title="Download SMTA 2"
      :document-id="BookingFormOfSMTA2.SMTA2DocumentId"
      :document-name="BookingFormOfSMTA2.SMTA2DocumentName"
      @downloadDocument="downloadSMTA2"
    ></DownloadDocumentComponent>

    <DownloadDocumentComponent
      title="Download Booking Form of SMTA 2 template"
      :document-id="
        BookingFormOfSMTA2.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId
      "
      :document-name="
        BookingFormOfSMTA2.OriginalDocumentTemplateBookingFormOfSMTA2DocumentName
      "
      @downloadDocument="downloadBookingFormOfSMTA2Template"
    ></DownloadDocumentComponent>
    <BookingFormOfSMTA2DataForm
      :can-read="canRead"
      :can-read-courier="CanReadCourier"
    ></BookingFormOfSMTA2DataForm>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import BookingFormOfSMTA2DataForm from "./components/BookingFormOfSMTA2DataForm.vue";
import DownloadDocumentComponent from "./components/DownloadDocumentComponent.vue";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { EFormModule } from "./store";
import { MaterialProductModule } from "../materialProducts/store";
import { TransportCategoryModule } from "../transportCategories/store";
import { DocumentModule } from "../documents/store";
import { DocumentTemplateModule } from "../documentTemplates/store";
import { BookingFormOfSMTA2Data } from "@/models/BookingFormOfSMTA2Data";
import { CountryModule } from "../countries/store";
import { CourierModule } from "../couriers/store";

@Component({
  components: { BookingFormOfSMTA2DataForm, DownloadDocumentComponent },
})
export default class BookingFormOfSMTA2DataPage extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadEForm);
  }

  get BookingFormOfSMTA2(): BookingFormOfSMTA2Data | undefined {
    return EFormModule.BookingFormOfSMTA2;
  }

  get CanReadCourier(): boolean {
    return hasPermission(PermissionNames.CanReadCourier);
  }

  async downloadSMTA2(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentModule.ReadDocumentForShipmentRequest(info);
  }

  async downloadBookingFormOfSMTA2Template(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentTemplateModule.ReadTemplateForEForm(info);
  }

  async loadPageInfo() {
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();
    await CountryModule.ListCountries();
    await EFormModule.ReadBookingFormOfSMTA2(this.$route.params.id);
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
