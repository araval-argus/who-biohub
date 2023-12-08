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
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="SMTA1WorkflowItemGridItems"
          :search="search"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          :custom-filter="customSearch"
          @click:row="selected"
        >
          <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
            <v-icon v-if="canDelete" small @click="deleteItem(item)">
              mdi-delete-circle-outline
            </v-icon>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA1WorkflowItemGridItem } from "@/models/SMTA1WorkflowItemGridItem";
import { SMTA1WorkflowItemModule } from "../store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";

@Component({
  components: { ConfirmationDialogComponent, BackButton },
})
export default class SMTA1WorkflowItemsTable extends Vue {
  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Array, default: [""] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  private search = "";

  private linkPageSuffix = "";

  private title = "SMTA 1";
  private baseHeaders = [
    {
      text: "Next Action",
      align: "start",
      sortable: false,
      value: "CurrentStatusName",
    },
    {
      text: "By",
      align: "start",
      sortable: false,
      value: "NextActionBy",
    },
    {
      text: "Previous Action",
      align: "start",
      sortable: false,
      value: "WorkflowItemTitle",
    },
    {
      text: "By",
      align: "start",
      sortable: false,
      value: "PreviousActionBy",
    },
    {
      text: "Institution",
      align: "start",
      sortable: false,
      value: "Institution",
    },
    {
      text: "Country",
      align: "start",
      sortable: false,
      value: "LaboratoryCountry",
    },
    {
      text: "Action Completed Date",
      align: "start",
      sortable: false,
      value: "OperationDate",
    },
  ];

  private actionHeader = [
    {
      text: "Actions",
      align: "start",
      sortable: false,
      value: "actions",
    },
  ];

  private editableHeaders = this.actionHeader.concat(this.baseHeaders);

  get hasActionHeader(): boolean {
    return this.canDelete;
  }

  get headers(): Array<{
    text: string;
    align: string;
    sortable: boolean;
    value: string;
  }> {
    let headers = [] as Array<{
      text: string;
      align: string;
      sortable: boolean;
      value: string;
    }>;

    if (this.hasActionHeader == true) {
      headers = this.editableHeaders;
    } else {
      headers = this.baseHeaders;
    }
    return headers;
  }

  get SMTA1WorkflowItemGridItems(): Array<SMTA1WorkflowItemGridItem> {
    const SMTA1WorkflowItems = SMTA1WorkflowItemModule.SMTA1WorkflowItems;

    if (!SMTA1WorkflowItems) return new Array<SMTA1WorkflowItemGridItem>();

    return SMTA1WorkflowItems.map((l) => {
      return {
        Id: l.Id,
        WorkflowItemTitle: l.WorkflowItemTitle,
        CurrentStatus: l.CurrentStatus,
        CurrentStatusName: l.CurrentStatusName,
        OperationDate: l.OperationDate
          ? this.getFormatDate(l.OperationDate)
          : "",
        PreviousActionBy: l.PreviousActionBy,
        NextActionBy: l.NextActionBy,
        Institution: l.LaboratoryName,
        LaboratoryCountry: l.LaboratoryCountry,
        UserName: l.UserName,
      };
    });
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();
    const hour = parsedDate.getHours().toString().padStart(2, "0");
    const minutes = parsedDate.getMinutes().toString().padStart(2, "0");
    const seconds = parsedDate.getSeconds().toString().padStart(2, "0");

    return (
      day +
      "/" +
      month +
      "/" +
      year +
      " " +
      hour +
      ":" +
      minutes +
      ":" +
      seconds
    );
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  selected(item: SMTA1WorkflowItemGridItem): void {
    const lab: SMTA1WorkflowItem = this.getSMTA1WorkflowItem(item.Id);
    this.$emit("selected", lab);
  }

  getSMTA1WorkflowItem(id: string): SMTA1WorkflowItem {
    const lab: SMTA1WorkflowItem | undefined =
      SMTA1WorkflowItemModule.SMTA1WorkflowItems.find((x) => x.Id == id);
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for SMTA1WorkflowItem with id ${id}`,
      };
    return lab;
  }

  deleteItem(item: SMTA1WorkflowItemGridItem): void {
    this.$emit("deleteToBioHubItem", item);
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
