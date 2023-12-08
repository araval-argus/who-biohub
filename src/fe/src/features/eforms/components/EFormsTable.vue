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
              :items="eFormsGridItem"
              :search="search"
              :custom-filter="customSearch"
              :sort-by.sync="sortBy"
              :sort-desc.sync="sortDesc"
              @click:row="selected"
            >
              <template #[`item.Name`]="{ item }">
                <div class="d-flex align-center">
                  <v-img
                    v-if="item.Type != 1"
                    max-height="25"
                    max-width="25"
                    src="@/assets/icons/folder.svg"
                  ></v-img>

                  <v-icon v-if="item.Type == 1" small class="mr-2">
                    mdi-open-in-new
                  </v-icon>

                  <label class="ms-4">{{ item.Name }}</label>
                </div>
              </template>
            </v-data-table>
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import BackButton from "../../../components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";
import { EForm } from "@/models/EForm";
import { EFormModule } from "../store";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { EFormType } from "@/models/enums/EFormType";
import { EFormItemType } from "@/models/enums/EFormItemType";
import { EFormGridItem } from "@/models/EFormGridItem";
import DialogComponent from "../../../components/DialogComponent.vue";

@Component({
  components: { ConfirmationDialogComponent, BackButton, DialogComponent },
})
export default class EFormsTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;
  deleteDialogMessage = "";
  dialogText = "";

  search = "";

  private baseHeaders = [
    {
      text: "E-Form",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "Approved date",
      align: "start",
      sortable: true,
      value: "ApprovedTime",
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
  readonly eForms: Array<EForm>;

  @Prop({ type: Array, default: [] })
  readonly breadcrumbsItems: Array<BreadcrumbsItem>;

  @Prop({ type: String, default: "E-Forms" })
  readonly title: string;

  @Prop({ type: Array, default: [""] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  readonly customBaseHeaders: Array<object>;

  get eFormsGridItem(): Array<EFormGridItem> {
    if (!this.eForms) return new Array<EFormGridItem>();

    return this.eForms.map((d) => {
      let fileType = "";
      switch (d.EFormType) {
        case EFormType.Annex2OfSMTA1:
          fileType = "Annex 2 of SMTA1";
          break;
        case EFormType.Annex2OfSMTA2:
          fileType = "Annex 2 of SMTA2";
          break;
        case EFormType.BookingFormOfSMTA1:
          fileType = "Booking Form of SMTA 1";
          break;

        case EFormType.BookingFormOfSMTA2:
          fileType = "Booking Form of SMTA 2";
          break;

        case EFormType.BiosafetyChecklistOfSMTA2:
          fileType = "Biosafety Checklist";
          break;
      }

      return {
        Id: d.Id,
        Name: d.Name,
        Type: d.Type,
        EFormType: fileType,
        ApprovedTime: this.getFormatDate(d.ApprovedTime),
        ParentId: d.ParentId,
        Url: d.Url,
      } as EFormGridItem;
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

  selected(item: EFormGridItem | EForm): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      let d: EForm = EFormModule.emptyEForm;
      if (typeof item.ApprovedTime === "string") {
        d = this.getEForm(item.Id);
      } else if (item != undefined) {
        d = item as EForm;
      }

      this.$emit("selected", d);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  selectedBreadcrumbsItem(folderId: string, folderName: string) {
    this.$emit("selected", {
      Id: folderId,
      Name: folderName,
      Type: EFormItemType.Folder,
    } as EForm);
  }

  createFolder(): void {
    this.$emit("createFolder");
  }

  addEForm(): void {
    this.$emit("addEForm");
  }

  getEForm(id: string): EForm {
    const lab: EForm | undefined = this.eForms.find((x: any) => x.Id == id);
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for EForm with id ${id}`,
      };
    return lab;
  }

  getFormatDate(date: Date | string): string {
    if (date) {
      let parsedDate = new Date(date);
      const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
      const day = parsedDate.getDate().toString().padStart(2, "0");
      const year = parsedDate.getFullYear();

      return day + "/" + month + "/" + year;
    } else {
      return "";
    }
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
