<template>
  <div v-if="canRead">
    <DocumentsTable
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canDelete"
      :documents="documents"
      :breadcrumbs-items="breadcrumbsItems"
      :loading="loading"
      @selected="selected"
      @createFolder="createFolder"
      @addTemplate="addFile"
      @edit="editDocument"
      @delete="deleteItem"
    >
    </DocumentsTable>

    <DialogComponent ref="dialog" :dialog-text="dialogText"> </DialogComponent>
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      :message="confirmationDialogMessage"
      @onConfirm="confirmUpdateDocument"
    ></ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import DocumentsTable from "./components/DocumentsTable.vue";
import { DocumentModule } from "./store";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { AppModule } from "../../store/MainStore";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { createFormPopupItem } from "../../utils/helper";
import FormPopup from "../../components/FormPopup.vue";
import { Document } from "@/models/Document";
import { DocumentType } from "@/models/enums/DocumentType";
import { DropdownItem } from "@/models/DropdownItem";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import DialogComponent from "../../components/DialogComponent.vue";
import ConfirmationDialogComponent from "../../components/ConfirmationDialogComponent.vue";

@Component({
  components: {
    DocumentsTable,
    CardActionsGenericButton,
    FormPopup,
    DialogComponent,
    ConfirmationDialogComponent,
  },
})
export default class DocumentsPageIndex extends Vue {
  $refs!: {
    newFolderPopup: FormPopup;
    newFilePopup: FormPopup;
    editTemplatePopup: FormPopup;
    dialog: DialogComponent;
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  breadcrumbsItems: Array<BreadcrumbsItem> = [];

  newFolderPopupItems: Array<FormPopupItem> = [];
  newFilePopupItems: Array<FormPopupItem> = [];
  editTemplatePopupItems: Array<FormPopupItem> = [];
  editTemplatePopopTitle = "";

  dialogText = "";

  confirmationDialogMessage = "";

  private openSaveOrDelete = false;
  private download = false;

  get loading(): boolean {
    return !this.download && AppModule.IsLoadingActive;
  }

  get templateFileTypeItems(): Array<DropdownItem> {
    return [
      {
        Value: DocumentFileType.SMTA1,
        Text: "SMTA1",
      },
      {
        Value: DocumentFileType.SMTA2,
        Text: "SMTA2",
      },
      {
        Value: DocumentFileType.Annex2OfSMTA1,
        Text: "Annex 2 of SMTA 1",
      },
      {
        Value: DocumentFileType.Annex2OfSMTA2,
        Text: "Annex 2 of SMTA 2",
      },
      {
        Value: DocumentFileType.BookingFormOfSMTA1,
        Text: "Booking Form of SMTA 1",
      },
      {
        Value: DocumentFileType.PackagingList,
        Text: "Packaging List",
      },
      {
        Value: DocumentFileType.NonCommercialInvoiceCatA,
        Text: "Non-Commercial Invoice (Category A - UN2814)",
      },
      {
        Value: DocumentFileType.NonCommercialInvoiceCatB,
        Text: "Non-Commercial Invoice (Category B - UN3373)",
      },
      {
        Value: DocumentFileType.BiosafetyChecklist,
        Text: "Biosafety Checklist",
      },
    ] as DropdownItem[];
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadDocument);
  }

  async loadPageInfo() {
    await DocumentModule.ListDocuments();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    if (!this.openSaveOrDelete) AppModule.HideLoading();
  }

  get error(): any {
    return DocumentModule.ErrorMessage;
  }

  get documents(): Array<Document> {
    return DocumentModule.CurrentFolderDocuments;
  }

  async selected(item: Document): Promise<void> {
    if (item.Type == DocumentType.Folder) {
      this.showLoading();
      if (item.Id) {
        if (this.breadcrumbsItems.length == 0) {
          this.breadcrumbsItems.push({
            text: "...",
            to: null,
          } as unknown as BreadcrumbsItem);
        }

        const index = this.breadcrumbsItems.findIndex((bi) => bi.to == item.Id);
        if (index == -1) {
          this.breadcrumbsItems.push({
            text: item.Name,
            to: item.Id,
          } as BreadcrumbsItem);
        } else {
          this.breadcrumbsItems = this.breadcrumbsItems.slice(0, index + 1);
        }
        this.breadcrumbsItems.forEach((bi) => (bi.disabled = false));
        this.breadcrumbsItems[this.breadcrumbsItems.length - 1].disabled = true;
        DocumentModule.SET_CURRENT_FOLDER_DOCUMENTS(item.Id);
      } else {
        this.breadcrumbsItems = [];
        await DocumentModule.ListDocuments();
      }
    } else {
      this.download = true;
      this.showLoading();
      DocumentModule.SET_DOCUMENT(item);
      await DocumentModule.ReadDocument();
    }
    this.hideLoading();
  }

  private getParentId() {
    return this.breadcrumbsItems.length > 0
      ? (this.breadcrumbsItems[this.breadcrumbsItems.length - 1].to as string)
      : "807ea670-33c7-48e5-995b-43b0583c0ee9";
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
