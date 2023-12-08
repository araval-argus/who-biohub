<template>
  <div v-if="canRead">
    <DocumentTemplatesTable
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canDelete"
      :document-template="templates"
      :breadcrumbs-items="breadcrumbsItems"
      :loading="loading"
      @selected="selected"
      @createFolder="createFolder"
      @addTemplate="addFile"
      @edit="editDocumentTemplate"
      @delete="deleteItem"
    >
    </DocumentTemplatesTable>
    <FormPopup
      ref="newFolderPopup"
      v-model="newFolderPopupItems"
      title="Create Folder"
      :properties-errors="error"
      @executeSave="saveNewFolder"
    >
    </FormPopup>
    <FormPopup
      ref="newFilePopup"
      v-model="newFilePopupItems"
      title="Add Document"
      :properties-errors="error"
      @executeSave="saveNewFile"
    >
    </FormPopup>
    <FormPopup
      ref="editTemplatePopup"
      v-model="editTemplatePopupItems"
      :title="editTemplatePopopTitle"
      :properties-errors="error"
      @executeSave="updateDocumentTemplate"
    >
    </FormPopup>
    <DialogComponent ref="dialog" :dialog-text="dialogText"> </DialogComponent>
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      :message="confirmationDialogMessage"
      @onConfirm="confirmUpdateDocumentTemplate"
    ></ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import DocumentTemplatesTable from "./components/DocumentTemplatesTable.vue";
import { DocumentTemplateModule } from "./store";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { AppModule } from "../../store/MainStore";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { createFormPopupItem } from "../../utils/helper";
import FormPopup from "../../components/FormPopup.vue";
import { DocumentTemplate } from "@/models/DocumentTemplate";
import { DocumentType } from "@/models/enums/DocumentType";
import { DropdownItem } from "@/models/DropdownItem";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import DialogComponent from "../../components/DialogComponent.vue";
import ConfirmationDialogComponent from "../../components/ConfirmationDialogComponent.vue";
import { SeedData } from "@/models/constants/SeedData";

@Component({
  components: {
    DocumentTemplatesTable,
    CardActionsGenericButton,
    FormPopup,
    DialogComponent,
    ConfirmationDialogComponent,
  },
})
export default class DocumentTemplatesPageIndex extends Vue {
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
        Value: DocumentFileType.BookingFormOfSMTA2,
        Text: "Booking Form of SMTA 2",
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
      {
        Value: DocumentFileType.WHOGuidance,
        Text: "WHO Guidance",
      },
    ] as DropdownItem[];
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadDocumentTemplate);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateDocumentTemplate);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditDocumentTemplate);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteDocumentTemplate);
  }

  async loadPageInfo() {
    await DocumentTemplateModule.ListDocumentTemplates(
      SeedData.DocumentTemplateRootFolderId
    );
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
    return DocumentTemplateModule.ErrorMessage;
  }

  get templates(): Array<DocumentTemplate> {
    return DocumentTemplateModule.DocumentTemplates;
  }

  async selected(item: DocumentTemplate): Promise<void> {
    if (item.Type == DocumentType.Folder) {
      this.showLoading();
      if (item.Id) {
        if (this.breadcrumbsItems.length == 0) {
          this.breadcrumbsItems.push({
            text: "...",
            to: SeedData.DocumentTemplateRootFolderId,
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
        await DocumentTemplateModule.ListDocumentTemplates(item.Id);
      } else {
        this.breadcrumbsItems = [];
        await DocumentTemplateModule.ListDocumentTemplates(undefined);
      }
    } else {
      this.download = true;
      this.showLoading();
      DocumentTemplateModule.SET_DOCUMENTTEMPLATE(item);
      await DocumentTemplateModule.ReadTemplate();
    }
    this.hideLoading();
  }

  async deleteItem(item: DocumentTemplate): Promise<void> {
    this.showLoading();
    DocumentTemplateModule.SET_DOCUMENTTEMPLATE(item);

    if (item.Type == DocumentType.Folder) {
      if (await DocumentTemplateModule.FolderContainsCurrent()) {
        this.dialogText =
          "The folder cannot be deleted, because it contains at least a file set as current";
        this.$refs.dialog.showDialog();
        this.hideLoading();
        return;
      }
    } else if (item.Current) {
      this.dialogText = "It is not possible to delete a file set as current";
      this.$refs.dialog.showDialog();
      this.hideLoading();
      return;
    }

    await DocumentTemplateModule.DeleteDocumentTemplate();
    await DocumentTemplateModule.ListDocumentTemplates(item.ParentId);

    this.hideLoading();
  }

  async createFolder(): Promise<void> {
    this.newFolderPopupItems = new Array<FormPopupItem>(
      createFormPopupItem(InputType.String, "Folder Name", "Name", true)
    );
    this.$refs.newFolderPopup.showPopup();
  }

  async addFile(): Promise<void> {
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
        this.templateFileTypeItems
      )
    );
    this.$refs.newFilePopup.showPopup();
  }

  async editDocumentTemplate(template: DocumentTemplate): Promise<void> {
    this.editTemplatePopupItems = new Array<FormPopupItem>(
      createFormPopupItem(
        InputType.String,
        "Id",
        "Id",
        true,
        true,
        true,
        template.Id
      ),
      createFormPopupItem(
        InputType.String,
        "Type",
        "Type",
        template.Type == DocumentType.File,
        true,
        true,
        template.Type
      ),
      createFormPopupItem(
        InputType.String,
        template.Type == DocumentType.File ? "File name" : "Folder name",
        "Name",
        true,
        false,
        false,
        template.Name
      )
    );

    if (template.Type == DocumentType.File) {
      this.editTemplatePopopTitle = "Edit file";
      this.editTemplatePopupItems.push(
        createFormPopupItem(
          InputType.Checkbox,
          "Current",
          "Current",
          false,
          template.Current,
          false,
          template.Current ?? false
        )
      );
    } else {
      this.editTemplatePopopTitle = "Edit folder";
    }

    this.$refs.editTemplatePopup.showPopup();
  }

  async saveNewFolder(folder: Array<FormPopupItem>) {
    this.showLoading();
    let parentId = this.getParentId();
    DocumentTemplateModule.CLEAR_DOCUMENTTEMPLATE_CREATE();
    DocumentTemplateModule.SET_DOCUMENTTEMPLATE_CREATE({
      Name: folder.find((f) => f.PropertyName == "Name")?.Value,
      ParentId: parentId,
    } as DocumentTemplate);
    await DocumentTemplateModule.CreateFolder();
    await DocumentTemplateModule.ListDocumentTemplates(parentId);
    this.newFolderPopupItems = [];
    this.hideLoading();
  }

  async saveNewFile(file: Array<FormPopupItem>) {
    this.showLoading();
    let parentId = this.getParentId();
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;
    DocumentTemplateModule.CLEAR_DOCUMENTTEMPLATE_CREATE();
    DocumentTemplateModule.SET_DOCUMENTTEMPLATE_CREATE({
      ParentId: parentId,
      FileType: file.find((f) => f.PropertyName == "Type")?.Value,
    } as DocumentTemplate);
    DocumentTemplateModule.SET_DOCUMENTTEMPLATE_CREATE_FILE(fileSelected);

    await DocumentTemplateModule.UploadFile();
    await DocumentTemplateModule.ListDocumentTemplates(parentId);
    this.newFilePopupItems = [];
    this.hideLoading();
  }

  async updateDocumentTemplate(file: Array<FormPopupItem>) {
    this.showLoading();
    let type = file.find((f) => f.PropertyName == "Type")
      ?.Value as DocumentType;
    let documentTemplate = {
      Id: file.find((f) => f.PropertyName == "Id")?.Value,
      Name: file.find((f) => f.PropertyName == "Name")?.Value,
      Current:
        type == DocumentType.File
          ? file.find((f) => f.PropertyName == "Current")?.Value ?? false
          : null,
    } as DocumentTemplate;
    DocumentTemplateModule.SET_DOCUMENTTEMPLATE(documentTemplate);

    if (
      type == DocumentType.File &&
      !file.find((f) => f.PropertyName == "Current")?.Readonly &&
      file.find((f) => f.PropertyName == "Current")?.Value == true
    ) {
      if (await DocumentTemplateModule.CheckOtherCurrentPresent()) {
        this.hideLoading();
        this.confirmationDialogMessage =
          "The element previously set as current will be disabled. Are you sure to continue?";
        this.$refs.confirmationDialogComponent.showDialog(documentTemplate);
        return;
      }
    }

    await DocumentTemplateModule.UpdateDocumentTemplate();
    await DocumentTemplateModule.ListDocumentTemplates(this.getParentId());
    this.editTemplatePopupItems = [];
    this.hideLoading();
  }

  async confirmUpdateDocumentTemplate(item: DocumentTemplate) {
    this.showLoading();
    DocumentTemplateModule.SET_DOCUMENTTEMPLATE(item);
    await DocumentTemplateModule.UpdateDocumentTemplate();
    await DocumentTemplateModule.ListDocumentTemplates(this.getParentId());
    this.editTemplatePopupItems = [];
    this.hideLoading();
  }

  private getParentId() {
    return this.breadcrumbsItems.length > 0
      ? (this.breadcrumbsItems[this.breadcrumbsItems.length - 1].to as string)
      : SeedData.DocumentTemplateRootFolderId;
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
