<template>
  <div v-if="canRead">
    <ShipmentDocumentsTable
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canEdit"
      :loading="loading"
      :shipment-documents="shipmentDocuments"
      :title="title"
      @selected="selected"
      @addShipmentDocument="addShipmentDocument"
      @editShipmentDocument="editShipmentDocument"
      @deleteShipmentDocument="deleteShipmentDocument"
    ></ShipmentDocumentsTable>

    <FormPopup
      ref="newFilePopup"
      v-model="newFilePopupItems"
      title="Add Document"
      :properties-errors="error"
      @executeSave="saveNewFile"
    >
    </FormPopup>
    <FormPopup
      ref="editDocumentPopup"
      v-model="editDocumentPopupItems"
      :title="editDocumentPopopTitle"
      :properties-errors="error"
      @executeSave="updateShipmentDocument"
    >
    </FormPopup>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Model, Prop } from "vue-property-decorator";
import ShipmentDocumentsTable from "./ShipmentDocumentsTable.vue";
import { WorklistFromBioHubItemModule } from "../../store";
import { AppModule } from "../../../../store/MainStore";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { AttachmentType } from "@/models/enums/AttachmentType";
import { createFormPopupItem } from "../../../../utils/helper";
import FormPopup from "../../../../components/FormPopup.vue";
import { ShipmentDocument } from "@/models/ShipmentDocument";
import { ShipmentDocumentGridItem } from "@/models/ShipmentDocumentGridItem";
import { DocumentType } from "@/models/enums/DocumentType";
import { DropdownItem } from "@/models/DropdownItem";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import DialogComponent from "../../../../components/DialogComponent.vue";
import ConfirmationDialogComponent from "../../../../components/ConfirmationDialogComponent.vue";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { ShipmentDocumentOperationType } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubItemFileInfo } from "@/models/WorklistFromBioHubItemFileInfo";

@Component({
  components: {
    ShipmentDocumentsTable,
    FormPopup,
    DialogComponent,
    ConfirmationDialogComponent,
  },
})
export default class ShipmentDocumentsForm extends Vue {
  $refs!: {
    newFolderPopup: FormPopup;
    newFilePopup: FormPopup;
    editDocumentPopup: FormPopup;
    dialog: DialogComponent;
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  newFolderPopupItems: Array<FormPopupItem> = [];
  newFilePopupItems: Array<FormPopupItem> = [];
  editDocumentPopupItems: Array<FormPopupItem> = [];
  editDocumentPopopTitle = "";

  dialogText = "";

  confirmationDialogMessage = "";

  private openSaveOrDelete = false;
  private download = false;

  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;

  @Prop({ type: Boolean, default: [] })
  readonly shipmentDocuments: Array<ShipmentDocument>;

  @Prop({ type: String, default: "Shipment related documents" })
  readonly title: string;

  get loading(): boolean {
    return !this.download && AppModule.IsLoadingActive;
  }

  get documentFileTypeItems(): Array<DropdownItem> {
    const array = new Array<DropdownItem>();

    if (this.isTypePresent(DocumentFileType.PackagingList) == false) {
      array.push({
        Value: DocumentFileType.PackagingList,
        Text: "Packaging List",
      });
    }
    if (
      this.isTypePresent(DocumentFileType.NonCommercialInvoiceCatA) == false
    ) {
      array.push({
        Value: DocumentFileType.NonCommercialInvoiceCatA,
        Text: "Non-Commercial Invoice (Category A - UN2814)",
      });
    }
    if (
      this.isTypePresent(DocumentFileType.NonCommercialInvoiceCatB) == false
    ) {
      array.push({
        Value: DocumentFileType.NonCommercialInvoiceCatB,
        Text: "Non-Commercial Invoice (Category B - UN3373)",
      });
    }
    if (
      this.isTypePresent(DocumentFileType.DangerousGoodsDeclaration) == false
    ) {
      array.push({
        Value: DocumentFileType.DangerousGoodsDeclaration,
        Text: "Dangerous Goods Declaration",
      });
    }
    if (this.isTypePresent(DocumentFileType.ExportPermit) == false) {
      array.push({
        Value: DocumentFileType.ExportPermit,
        Text: "Export Permit",
      });
    }
    if (this.isTypePresent(DocumentFileType.ImportPermit) == false) {
      array.push({
        Value: DocumentFileType.ImportPermit,
        Text: "Import Permit",
      });
    }
    array.push({
      Value: DocumentFileType.Other,
      Text: "Other",
    });

    return array;
  }

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDownload: boolean;

  @Prop({ type: Boolean, default: false })
  readonly bhfDocuments: boolean;

  async reloadData() {
    await WorklistFromBioHubItemModule.ReloadShipmentDocuments(this.model.Id);
  }

  isTypePresent(type: DocumentFileType): boolean {
    const elem: ShipmentDocument | undefined = this.shipmentDocuments.find(
      (x) => x.FileType == type
    );
    if (elem === undefined) return false;
    return true;
  }

  updated() {
    if (!this.openSaveOrDelete) AppModule.HideLoading();
  }

  get error(): any {
    return WorklistFromBioHubItemModule.ErrorMessage;
  }

  async selected(item: ShipmentDocumentGridItem): Promise<void> {
    if (this.canDownload == true) {
      const fileInfo = Object.create({
        Id: item.Id,
        Name: item.Name + "." + item.Extension,
        WorklistId: this.model.Id,
      }) as WorklistFromBioHubItemFileInfo;

      WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEMDOWNLOADFILEINFO(
        fileInfo
      );
      this.$emit("downloadFile");
    }
  }

  async deleteShipmentDocument(item: ShipmentDocument): Promise<void> {
    this.showLoading();
    this.model.ShipmentDocumentId = item.Id;
    this.model.ShipmentDocumentOperationType =
      ShipmentDocumentOperationType.Delete;

    this.$emit("update", this.model);

    if (this.bhfDocuments) {
      await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItemBHFShipmentDocuments();
    } else {
      await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItemQEShipmentDocuments();
    }

    await this.reloadData();

    this.hideLoading();
  }

  async addShipmentDocument(): Promise<void> {
    this.newFilePopupItems = new Array<FormPopupItem>(
      createFormPopupItem(InputType.File, "File", "File", true),
      createFormPopupItem(
        InputType.Dropdown,
        "Type",
        "Type",
        true,
        false,
        false,
        undefined,
        this.documentFileTypeItems
      )
    );
    this.$refs.newFilePopup.showPopup();
  }

  async editShipmentDocument(document: ShipmentDocument): Promise<void> {
    this.editDocumentPopupItems = new Array<FormPopupItem>(
      createFormPopupItem(
        InputType.String,
        "Id",
        "Id",
        true,
        true,
        true,
        document.Id
      ),
      createFormPopupItem(
        InputType.String,
        "Type",
        "Type",
        true,
        true,
        true,
        document.FileType
      ),
      createFormPopupItem(
        InputType.String,
        "File Name",
        "Name",
        true,
        false,
        false,
        document.Name
      )
    );

    this.editDocumentPopopTitle = "Edit file";

    this.$refs.editDocumentPopup.showPopup();
  }

  async saveNewFile(file: Array<FormPopupItem>) {
    this.showLoading();
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    const fileType = file.find((f) => f.PropertyName == "Type")
      ?.Value as DocumentFileType;

    this.model.DocumentTemplateFileType = fileType;
    this.model.ShipmentDocumentOperationType =
      ShipmentDocumentOperationType.Add;

    this.$emit("update", this.model);

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_DOCUMENT(
      fileSelected
    );
    WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);

    if (this.bhfDocuments) {
      await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItemBHFShipmentDocuments();
    } else {
      await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItemQEShipmentDocuments();
    }

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_DOCUMENT(undefined);
    await this.reloadData();
    this.newFilePopupItems = [];
    this.hideLoading();
  }

  async updateShipmentDocument(file: Array<FormPopupItem>) {
    this.showLoading();

    this.model.ShipmentDocumentId = file.find((f) => f.PropertyName == "Id")
      ?.Value as string;
    this.model.ShipmentDocumentNewName = file.find(
      (f) => f.PropertyName == "Name"
    )?.Value as string;

    this.model.ShipmentDocumentOperationType =
      ShipmentDocumentOperationType.Update;

    this.$emit("update", this.model);

    if (this.bhfDocuments) {
      await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItemBHFShipmentDocuments();
    } else {
      await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItemQEShipmentDocuments();
    }

    await this.reloadData();
    this.editDocumentPopupItems = [];
    this.hideLoading();
  }

  private showLoading() {
    this.openSaveOrDelete = true;
    AppModule.ShowLoading();
  }

  private hideLoading() {
    this.openSaveOrDelete = false;
    this.download = false;
    AppModule.HideLoading();
  }
}
</script>
