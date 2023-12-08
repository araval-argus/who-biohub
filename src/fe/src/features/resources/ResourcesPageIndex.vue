<template>
  <div v-if="canRead">
    <ResourcesTable
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canDelete"
      :resources="resources"
      :breadcrumbs-items="breadcrumbsItems"
      :loading="loading"
      @selected="selected"
      @createFolder="createFolder"
      @addResource="addResource"
      @edit="editResource"
      @delete="deleteItem"
    >
    </ResourcesTable>
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
      @executeSave="updateResource"
    >
    </FormPopup>
    <DialogComponent ref="dialog" :dialog-text="dialogText"> </DialogComponent>
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      :message="confirmationDialogMessage"
      @onConfirm="confirmUpdateResource"
    ></ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ResourcesTable from "./components/ResourcesTable.vue";
import { ResourceModule } from "./store";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { AppModule } from "../../store/MainStore";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { createFormPopupItem } from "../../utils/helper";
import FormPopup from "../../components/FormPopup.vue";
import { Resource } from "@/models/Resource";
import { ResourceType } from "@/models/enums/ResourceType";
import { DropdownItem } from "@/models/DropdownItem";
import { ResourceFileType } from "@/models/enums/ResourceFileType";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import DialogComponent from "../../components/DialogComponent.vue";
import ConfirmationDialogComponent from "../../components/ConfirmationDialogComponent.vue";
import { SeedData } from "@/models/constants/SeedData";

@Component({
  components: {
    ResourcesTable,
    CardActionsGenericButton,
    FormPopup,
    DialogComponent,
    ConfirmationDialogComponent,
  },
})
export default class ResourcesPageIndex extends Vue {
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

  get resourceFileTypeItems(): Array<DropdownItem> {
    return [
      {
        Value: ResourceFileType.SMTA1,
        Text: "Standard Materials Transfer Agreement (SMTA) 1",
      },
      {
        Value: ResourceFileType.SMTA2,
        Text: "Standard Materials Transfer Agreement (SMTA) 2",
      },
      {
        Value: ResourceFileType.Shipment,
        Text: "Shipment-related Document",
      },
      {
        Value: ResourceFileType.BiosafetyAndBiosecurity,
        Text: "Biosafety and Biosecurity Document",
      },
    ] as DropdownItem[];
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadResource);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateResource);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditResource);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteResource);
  }

  async loadPageInfo() {
    await ResourceModule.ListResources(SeedData.ResourceRootFolderId);
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
    return ResourceModule.ErrorMessage == ""
      ? undefined
      : ResourceModule.ErrorMessage;
  }

  get resources(): Array<Resource> {
    return ResourceModule.Resources;
  }

  async selected(item: Resource): Promise<void> {
    if (item.Type == ResourceType.Folder) {
      this.showLoading();
      if (item.Id) {
        if (this.breadcrumbsItems.length == 0) {
          this.breadcrumbsItems.push({
            text: "...",
            to: SeedData.ResourceRootFolderId,
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
        await ResourceModule.ListResources(item.Id);
      } else {
        this.breadcrumbsItems = [];
        await ResourceModule.ListResources(undefined);
      }
    } else {
      this.download = true;
      this.showLoading();
      ResourceModule.SET_RESOURCE(item);
      await ResourceModule.DownloadResource();
    }
    this.hideLoading();
  }

  async deleteItem(item: Resource): Promise<void> {
    this.showLoading();
    ResourceModule.SET_RESOURCE(item);

    await ResourceModule.DeleteResource();
    await ResourceModule.ListResources(item.ParentId);

    this.hideLoading();
  }

  async createFolder(): Promise<void> {
    this.newFolderPopupItems = new Array<FormPopupItem>(
      createFormPopupItem(InputType.String, "Folder Name", "Name", true)
    );
    this.$refs.newFolderPopup.showPopup();
  }

  async addResource(): Promise<void> {
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
        this.resourceFileTypeItems
      )
    );
    this.$refs.newFilePopup.showPopup();
  }

  async editResource(resource: Resource): Promise<void> {
    this.editTemplatePopupItems = new Array<FormPopupItem>(
      createFormPopupItem(
        InputType.String,
        "Id",
        "Id",
        true,
        true,
        true,
        resource.Id
      ),
      createFormPopupItem(
        InputType.String,
        "Type",
        "Type",
        resource.Type == ResourceType.File,
        true,
        true,
        resource.Type
      ),
      createFormPopupItem(
        InputType.String,
        resource.Type == ResourceType.File ? "File name" : "Folder name",
        "Name",
        true,
        false,
        false,
        resource.Name
      )
    );

    if (resource.Type == ResourceType.File) {
      this.editTemplatePopopTitle = "Edit file";
      // this.editTemplatePopupItems.push(
      //   createFormPopupItem(
      //     InputType.Checkbox,
      //     "Current",
      //     "Current",
      //     false,
      //     resource.Current,
      //     false,
      //     resource.Current ?? false
      //   )
      // );
    } else {
      this.editTemplatePopopTitle = "Edit folder";
    }

    this.$refs.editTemplatePopup.showPopup();
  }

  async saveNewFolder(folder: Array<FormPopupItem>) {
    this.showLoading();
    let parentId = this.getParentId();
    ResourceModule.CLEAR_RESOURCE_CREATE();
    ResourceModule.SET_RESOURCE_CREATE({
      Name: folder.find((f) => f.PropertyName == "Name")?.Value,
      ParentId: parentId,
    } as Resource);
    await ResourceModule.CreateFolder();
    await ResourceModule.ListResources(parentId);
    this.newFolderPopupItems = [];
    this.hideLoading();
  }

  async saveNewFile(file: Array<FormPopupItem>) {
    this.showLoading();
    let parentId = this.getParentId();
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;
    ResourceModule.CLEAR_RESOURCE_CREATE();
    ResourceModule.SET_RESOURCE_CREATE({
      ParentId: parentId,
      FileType: file.find((f) => f.PropertyName == "Type")?.Value,
    } as Resource);
    ResourceModule.SET_RESOURCE_CREATE_FILE(fileSelected);

    await ResourceModule.UploadFile();
    await ResourceModule.ListResources(parentId);
    this.newFilePopupItems = [];
    this.hideLoading();
  }

  async updateResource(file: Array<FormPopupItem>) {
    this.showLoading();
    let type = file.find((f) => f.PropertyName == "Type")
      ?.Value as ResourceType;
    let documentTemplate = {
      Id: file.find((f) => f.PropertyName == "Id")?.Value,
      Name: file.find((f) => f.PropertyName == "Name")?.Value,
    } as Resource;
    ResourceModule.SET_RESOURCE(documentTemplate);

    await ResourceModule.UpdateResource();
    await ResourceModule.ListResources(this.getParentId());
    this.editTemplatePopupItems = [];
    this.hideLoading();
  }

  async confirmUpdateResource(item: Resource) {
    this.showLoading();
    ResourceModule.SET_RESOURCE(item);
    await ResourceModule.UpdateResource();
    await ResourceModule.ListResources(this.getParentId());
    this.editTemplatePopupItems = [];
    this.hideLoading();
  }

  private getParentId() {
    return this.breadcrumbsItems.length > 0
      ? (this.breadcrumbsItems[this.breadcrumbsItems.length - 1].to as string)
      : SeedData.ResourceRootFolderId;
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
