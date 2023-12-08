<template>
  <ResourcesPublicTable
    :resources="resources"
    :breadcrumbs-items="breadcrumbsItems"
    :loading="loading"
    @selected="selected"
  >
  </ResourcesPublicTable>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ResourcesPublicTable from "./components/ResourcesPublicTable.vue";
import { ResourceModule } from "./store";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { AppModule } from "../../store/MainStore";
import { FormPopupItem } from "@/models/FormPopupItem";
import { ResourcePublic } from "@/models/ResourcePublic";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { SeedData } from "@/models/constants/SeedData";
import { ResourceType } from "@/models/enums/ResourceType";
import { ResourceFileType } from "@/models/enums/ResourceFileType";

@Component({
  components: {
    ResourcesPublicTable,
    CardActionsGenericButton,
  },
})
export default class ResourcesPublicPageIndex extends Vue {
  newFilePopupItems: Array<FormPopupItem> = [];
  editTemplatePopupItems: Array<FormPopupItem> = [];
  editTemplatePopopTitle = "";

  dialogText = "";

  confirmationDialogMessage = "";

  breadcrumbsItems: Array<BreadcrumbsItem> = [];

  private openSaveOrDelete = false;
  private download = false;

  get loading(): boolean {
    return !this.download && AppModule.IsLoadingActive;
  }

  async loadPageInfo() {
    await ResourceModule.ListResourcesPublic(SeedData.ResourceRootFolderId);
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
    return ResourceModule.ErrorMessage;
  }

  get resources(): Array<ResourcePublic> {
    return ResourceModule.ResourcesPublic;
  }

  async selected(item: ResourcePublic): Promise<void> {
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
        await ResourceModule.ListResourcesPublic(item.Id);
      } else {
        this.breadcrumbsItems = [];
        await ResourceModule.ListResourcesPublic(undefined);
      }
    } else {
      this.download = true;
      this.showLoading();
      ResourceModule.SET_RESOURCE_PUBLIC(item);
      await ResourceModule.DownloadResourcePublic();
    }
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
