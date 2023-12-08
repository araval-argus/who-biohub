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
        <v-btn v-if="canCreate" color="primary" @click="createFolder">
          Create Folder
        </v-btn>
        <v-btn v-if="canCreate" color="ms-3 primary" @click="addTemplate">
          Add Document
        </v-btn>
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
                  <label class="ms-4">{{ item.Name }}</label>
                </div>
              </template>
              <template #[`item.Current`]="{ item }">
                <v-icon v-if="item.Current" small class="mr-2 green--text"
                  >mdi-check-circle</v-icon
                >
                <span v-else-if="item.Current == undefined"></span>
                <v-icon v-else small class="mr-2 red--text"
                  >mdi-close-circle</v-icon
                >
              </template>
              <template v-if="hasActionHeader" #[`item.Actions`]="{ item }">
                <v-icon
                  v-if="canEdit"
                  small
                  class="mr-2"
                  @click="editItem(item)"
                >
                  mdi-pencil-circle-outline
                </v-icon>
                <v-icon v-if="canDelete" small @click="deleteItem(item)">
                  mdi-delete-circle-outline
                </v-icon>
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
import { DocumentTemplate } from "@/models/DocumentTemplate";
import { DocumentTemplateModule } from "../store";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { DocumentType } from "@/models/enums/DocumentType";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { DocumentTemplateGridItem } from "@/models/DocumentTemplateGridItem";
import DialogComponent from "../../../components/DialogComponent.vue";

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
      text: "Doc Type",
      align: "start",
      sortable: true,
      value: "FileType",
    },
    {
      text: "Current",
      align: "start",
      sortable: true,
      value: "Current",
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
  readonly documentTemplate: Array<DocumentTemplate>;

  @Prop({ type: Array, default: [] })
  readonly breadcrumbsItems: Array<BreadcrumbsItem>;

  @Prop({ type: String, default: "Templates/WHO Guidance" })
  readonly title: string;

  @Prop({ type: Array, default: [""] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  readonly customBaseHeaders: Array<object>;

  get documentsGridItem(): Array<DocumentTemplateGridItem> {
    if (!this.documentTemplate) return new Array<DocumentTemplateGridItem>();

    return this.documentTemplate.map((d) => {
      let fileType = "";
      switch (d.FileType) {
        case DocumentFileType.SMTA1:
          fileType = "SMTA1";
          break;
        case DocumentFileType.SMTA2:
          fileType = "SMTA2";
          break;
        case DocumentFileType.Annex2OfSMTA1:
          fileType = "Annex 2 of SMTA1";
          break;
        case DocumentFileType.Annex2OfSMTA2:
          fileType = "Annex 2 of SMTA2";
          break;
        case DocumentFileType.BookingFormOfSMTA1:
          fileType = "Booking Form of SMTA 1";
          break;
        case DocumentFileType.PackagingList:
          fileType = "Packaging List";
          break;
        case DocumentFileType.NonCommercialInvoiceCatA:
          fileType = "Non-Commercial Invoice (Category A - UN2814)";
          break;
        case DocumentFileType.NonCommercialInvoiceCatB:
          fileType = "Non-Commercial Invoice (Category B - UN3373)";
          break;
        case DocumentFileType.BiosafetyChecklist:
          fileType = "Biosafety Checklist";
          break;
        case DocumentFileType.BookingFormOfSMTA2:
          fileType = "Booking Form of SMTA 2";
          break;
        case DocumentFileType.WHOGuidance:
          fileType = "WHO Guidance";
          break;
      }

      return {
        Id: d.Id,
        Name: d.Name,
        Extension: d.Extension,
        Type: d.Type,
        FileType: fileType,
        Current: d.Current,
        UploadTime: this.getFormatDate(d.UploadTime),
        UploadedBy: d.UploadedBy,
        ParentId: d.ParentId,
      } as DocumentTemplateGridItem;
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
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: DocumentTemplateGridItem): void {
    this.editClicked = true;
    const t: DocumentTemplate = this.getTemplate(item.Id);
    this.$emit("edit", t);
  }

  executeDelete(item: DocumentTemplateGridItem): void {
    const d: DocumentTemplate = this.getTemplate(item.Id);
    this.$emit("delete", d);
    this.deleteClicked = false;
  }

  deleteItem(item: DocumentTemplateGridItem): void {
    this.deleteClicked = true;
    if (!item.Current) {
      if (item.Type == DocumentType.Folder) {
        this.deleteDialogMessage =
          "Are you sure you want to delete this folder and all its contents? ";
      } else {
        this.deleteDialogMessage = "Are you sure you want to delete this file?";
      }
      this.$refs.confirmationDialogComponent.showDialog(item);
    } else {
      this.dialogText = "It is not possible to delete a file set as current";
      this.$refs.dialog.showDialog();
    }
  }

  selected(item: DocumentTemplateGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      let d: DocumentTemplate = DocumentTemplateModule.emptyDocumentTemplate;

      d = this.getTemplate(item.Id);

      this.$emit("selected", d);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  selectedBreadcrumbsItem(folderId: string, folderName: string) {
    this.$emit("selected", {
      Id: folderId,
      Name: folderName,
      Type: DocumentType.Folder,
    } as DocumentTemplate);
  }

  createFolder(): void {
    this.$emit("createFolder");
  }

  addTemplate(): void {
    this.$emit("addTemplate");
  }

  getTemplate(id: string): DocumentTemplate {
    const lab: DocumentTemplate | undefined =
      DocumentTemplateModule.DocumentTemplates.find((x: any) => x.Id == id);
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for Template with id ${id}`,
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
