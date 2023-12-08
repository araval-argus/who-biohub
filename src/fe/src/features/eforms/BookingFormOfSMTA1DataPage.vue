<template>
  <div v-if="canRead">
    <h2>Booking Form of SMTA 1 Data</h2>
    <br />
    <DownloadDocumentComponent
      title="Download SMTA 1"
      :document-id="BookingFormOfSMTA1.SMTA1DocumentId"
      :document-name="BookingFormOfSMTA1.SMTA1DocumentName"
      @downloadDocument="downloadSMTA1"
    ></DownloadDocumentComponent>

    <DownloadDocumentComponent
      title="Download Booking Form of SMTA 1 template"
      :document-id="
        BookingFormOfSMTA1.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId
      "
      :document-name="
        BookingFormOfSMTA1.OriginalDocumentTemplateBookingFormOfSMTA1DocumentName
      "
      @downloadDocument="downloadBookingFormOfSMTA1Template"
    ></DownloadDocumentComponent>
    <BookingFormOfSMTA1DataForm
      :can-read="canRead"
      :can-read-courier="CanReadCourier"
    ></BookingFormOfSMTA1DataForm>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import BookingFormOfSMTA1DataForm from "./components/BookingFormOfSMTA1DataForm.vue";
import DownloadDocumentComponent from "./components/DownloadDocumentComponent.vue";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { EFormModule } from "./store";
import { MaterialProductModule } from "../materialProducts/store";
import { TransportCategoryModule } from "../transportCategories/store";
import { DocumentModule } from "../documents/store";
import { DocumentTemplateModule } from "../documentTemplates/store";
import { BookingFormOfSMTA1Data } from "@/models/BookingFormOfSMTA1Data";
import { CountryModule } from "../countries/store";
import { CourierModule } from "../couriers/store";

@Component({
  components: { BookingFormOfSMTA1DataForm, DownloadDocumentComponent },
})
export default class BookingFormOfSMTA1DataPage extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadEForm);
  }

  get BookingFormOfSMTA1(): BookingFormOfSMTA1Data | undefined {
    return EFormModule.BookingFormOfSMTA1;
  }

  get CanReadCourier(): boolean {
    return hasPermission(PermissionNames.CanReadCourier);
  }

  async downloadSMTA1(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentModule.ReadDocumentForShipmentRequest(info);
  }

  async downloadBookingFormOfSMTA1Template(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);
    await DocumentTemplateModule.ReadTemplateForEForm(info);
  }

  async loadPageInfo() {
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();
    await CountryModule.ListCountries();
    await EFormModule.ReadBookingFormOfSMTA1(this.$route.params.id);
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
