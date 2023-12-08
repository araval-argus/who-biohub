<template>
  <div>
    <v-card class="mb-5">
      <v-card-title>
        <h2>{{ title }}</h2>
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Table Search"
          single-line
          hide-details
          class="mr-8"
        ></v-text-field>
      </v-card-title>
      <v-card-text>
        <v-row>
          <v-col class="pb-0 pt-6">
            <v-breadcrumbs :items="breadcrumbsItems" class="pa-0">
              <template #item="{ item }">
                <v-breadcrumbs-item>
                  <v-btn
                    text
                    :disabled="item.disabled"
                    @click="selectedBreadcrumbsItem(item.to, item.text)"
                  >
                    {{ item.text }}
                  </v-btn>
                </v-breadcrumbs-item>
              </template>
            </v-breadcrumbs>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12">
            <div v-if="loading">
              <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
              <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
            </div>
            <v-data-table
              v-else
              :headers="headers"
              :items="resourcesGridItems"
              :search="search"
              :custom-filter="customSearch"
              :sort-by.sync="sortBy"
              :sort-desc.sync="sortDesc"
              @click:row="selected"
            >
              <template #[`item.Name`]="{ item }">
                <div class="d-flex align-center">
                  <v-img
                    v-if="item.Extension == undefined"
                    max-height="25"
                    max-width="25"
                    src="@/assets/icons/folder.svg"
                  ></v-img>
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
                  <label class="ms-4">{{ item.FileCompleteName }}</label>
                </div>
              </template>
            </v-data-table>
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
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
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import BackButton from "../../../components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";
import { ResourcePublic } from "@/models/ResourcePublic";
import { ResourceModule } from "../store";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { ResourceType } from "@/models/enums/ResourceType";
import { ResourceFileType } from "@/models/enums/ResourceFileType";
import { ResourcePublicGridItem } from "@/models/ResourcePublicGridItem";
import DialogComponent from "../../../components/DialogComponent.vue";

@Component({
  components: { ConfirmationDialogComponent, BackButton, DialogComponent },
})
export default class ResourcesTable extends Vue {
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
      text: "Doc Type",
      align: "start",
      sortable: true,
      value: "FileType",
    },
  ];

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Array, default: [] })
  readonly resources: Array<ResourcePublic>;

  @Prop({ type: String, default: "Resources" })
  readonly title: string;

  @Prop({ type: Array, default: [] })
  readonly breadcrumbsItems: Array<BreadcrumbsItem>;

  @Prop({ type: Array, default: [""] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  readonly customBaseHeaders: Array<object>;

  get resourcesGridItems(): Array<ResourcePublicGridItem> {
    if (!this.resources) return new Array<ResourcePublicGridItem>();

    return this.resources.map((d) => {
      let fileType = "";
      switch (d.FileType) {
        case ResourceFileType.SMTA1:
          fileType = "Standard Materials Transfer Agreement (SMTA) 1";
          break;
        case ResourceFileType.SMTA2:
          fileType = "Standard Materials Transfer Agreement (SMTA) 2";
          break;
        case ResourceFileType.Shipment:
          fileType = "Shipment-related Document";
          break;
        case ResourceFileType.BiosafetyAndBiosecurity:
          fileType = "Biosafety and Biosecurity Document";
          break;
      }

      return {
        Id: d.Id,
        FileCompleteName: d.FileCompleteName,
        Name: d.Name,
        Extension: d.Extension,
        Type: d.Type,
        FileType: fileType,
        ParentId: d.ParentId,
      } as ResourcePublicGridItem;
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
    dialog: DialogComponent;
  };

  get hasActionHeader(): boolean {
    return this.canEdit || this.canDelete;
  }

  get headers(): any {
    return this.baseHeaders;
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  selected(item: ResourcePublicGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      let d: ResourcePublic = ResourceModule.emptyResourcePublic;

      d = this.getResource(item.Id);

      this.$emit("selected", d);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  selectedBreadcrumbsItem(folderId: string, folderName: string) {
    this.$emit("selected", {
      Id: folderId,
      Name: folderName,
      Type: ResourceType.Folder,
    } as ResourcePublic);
  }

  getResource(id: string): ResourcePublic {
    const lab: ResourcePublic | undefined = ResourceModule.ResourcesPublic.find(
      (x: any) => x.Id == id
    );
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for ResourcePublic with id ${id}`,
      };
    return lab;
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
