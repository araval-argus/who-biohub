<template>
  <div>
    <v-card-title>
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-subtitle v-if="canCreate">
      Please upload all the shipment related documents. You can use the
      templates above or you can use your own template. Once you upload a
      document please specify the type of the document that you're uploading. In
      case the type of the document that you're uploading is not present in the
      system feel free to send an email to biohub@who.int for support.
    </v-card-subtitle>
    <v-btn
      v-if="canCreate"
      class="mb-5"
      color="ms-3 primary"
      @click="addShipmentDocument"
    >
      Upload Document
    </v-btn>
    <div v-if="loading">
      <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
      <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
    </div>
    <v-data-table
      v-else
      class="mt-5"
      :headers="headers"
      :items="documentsGridItem"
      :search="search"
      :custom-filter="customSearch"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
      @click:row="selected"
    >
      <template #[`item.Name`]="{ item }">
        <div class="d-flex align-center">
          <v-img
            v-if="item.Extension == 'docx' || item.Extension == 'doc'"
            max-height="25"
            max-width="25"
            src="@/assets/icons/word.svg"
          ></v-img>
          <v-img
            v-if="item.Extension == 'xlsx' || item.Extension == 'xls'"
            max-height="25"
            max-width="25"
            src="@/assets/icons/excel.svg"
          ></v-img>
          <v-img
            v-if="item.Extension == 'pptx' || item.Extension == 'ppt'"
            max-height="25"
            max-width="25"
            src="@/assets/icons/powerpoint.svg"
          ></v-img>
          <v-img
            v-if="item.Extension == 'pdf'"
            max-height="25"
            max-width="25"
            src="@/assets/icons/pdf.svg"
          ></v-img>
          <v-img
            v-if="item.Extension == 'jpg' || item.Extension == 'jpeg'"
            max-height="25"
            max-width="25"
            src="@/assets/icons/image.svg"
          ></v-img>
          <v-img
            v-if="item.Extension == 'mp4'"
            max-height="25"
            max-width="25"
            src="@/assets/icons/video.svg"
          ></v-img>
          <label class="ms-4">{{ item.Name }}</label>
        </div>
      </template>
      <template #[`item.Current`]="{ item }">
        <v-icon v-if="item.Current" small class="mr-2 green--text"
          >mdi-check-circle</v-icon
        >
        <span v-else-if="item.Current == undefined"></span>
        <v-icon v-else small class="mr-2 red--text">mdi-close-circle</v-icon>
      </template>
      <template v-if="hasActionHeader" #[`item.Actions`]="{ item }">
        <v-icon v-if="canEdit" small class="mr-2" @click="editItem(item)">
          mdi-pencil-circle-outline
        </v-icon>
        <v-icon v-if="canEdit" small @click="deleteItem(item)">
          mdi-delete-circle-outline
        </v-icon>
      </template>
    </v-data-table>

    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      :message="deleteDialogMessage"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
    <DialogComponent ref="dialog" :dialog-text="dialogText"> </DialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../../components/ConfirmationDialogComponent.vue";
import BackButton from "../../../../components/BackButton.vue";
import { customTableSearch } from "../../../../utils/helper";
import { DocumentTemplate } from "@/models/DocumentTemplate";
import { WorklistFromBioHubItemModule } from "../../store";
import { DocumentType } from "@/models/enums/DocumentType";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { ShipmentDocumentGridItem } from "@/models/ShipmentDocumentGridItem";
import { ShipmentDocument } from "@/models/ShipmentDocument";
import DialogComponent from "../../../../components/DialogComponent.vue";

@Component({
  components: { ConfirmationDialogComponent, BackButton, DialogComponent },
})
export default class DocumentTemplatesTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;
  deleteDialogMessage = "";
  dialogText = "";

  search = "";

  private baseHeaders = [
    {
      text: "Name",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "Extension",
      align: "start",
      sortable: true,
      value: "Extension",
    },
    {
      text: "File Type",
      align: "start",
      sortable: true,
      value: "FileType",
    },

    {
      text: "Upload date",
      align: "start",
      sortable: true,
      value: "UploadTime",
    },
    {
      text: "Uploaded by",
      align: "start",
      sortable: true,
      value: "UploadedBy",
    },
  ];

  private actionHeader = [
    {
      text: "Actions",
      align: "center",
      sortable: false,
      value: "Actions",
    },
  ];

  private editableHeaders = this.actionHeader.concat(this.baseHeaders);

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Array, default: [] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: [] })
  readonly shipmentDocuments: Array<ShipmentDocument>;

  @Prop({ type: String, default: "Shipment related documents" })
  readonly title: string;

  readonly customBaseHeaders: Array<object>;

  get documentsGridItem(): Array<ShipmentDocumentGridItem> {
    if (!this.shipmentDocuments) return new Array<ShipmentDocumentGridItem>();

    return this.shipmentDocuments.map((d) => {
      let fileType = "";
      switch (d.FileType) {
        case DocumentFileType.PackagingList:
          fileType = "Packaging List";
          break;
        case DocumentFileType.NonCommercialInvoiceCatA:
          fileType = "Non-Commercial Invoice (Category A - UN2814)";
          break;
        case DocumentFileType.NonCommercialInvoiceCatB:
          fileType = "Non-Commercial Invoice (Category B - UN3373)";
          break;
        case DocumentFileType.DangerousGoodsDeclaration:
          fileType = "Dangerous Goods Declaration";
          break;
        case DocumentFileType.ExportPermit:
          fileType = "Export Permit";
          break;
        case DocumentFileType.ImportPermit:
          fileType = "Import Permit";
          break;
        case DocumentFileType.Other:
          fileType = "Other";
          break;
      }

      return {
        Id: d.Id,
        Name: d.Name,
        Extension: d.Extension,
        FileType: fileType,
        UploadTime: this.getFormatDate(d.UploadTime),
        UploadedBy: d.UploadedBy,
      } as ShipmentDocumentGridItem;
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
    dialog: DialogComponent;
  };

  get hasActionHeader(): boolean {
    return this.canEdit;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: ShipmentDocumentGridItem): void {
    this.editClicked = true;
    this.$emit("editShipmentDocument", item);
  }

  executeDelete(item: ShipmentDocumentGridItem): void {
    this.$emit("deleteShipmentDocument", item);
    this.deleteClicked = false;
  }

  deleteItem(item: ShipmentDocumentGridItem): void {
    this.deleteClicked = true;
    this.deleteDialogMessage = "Are you sure you want to delete this file?";
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: ShipmentDocumentGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      this.$emit("selected", item);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  addShipmentDocument(): void {
    this.$emit("addShipmentDocument");
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
