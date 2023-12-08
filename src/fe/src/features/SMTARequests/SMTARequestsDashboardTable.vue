<template>
  <div>
    <div class="mb-5">
      <v-card-title>
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
          :items="workflowGridItems"
          :search="search"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          :custom-filter="customSearch"
          @click:row="selected"
        >
        </v-data-table>
      </v-card-text>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import SMTA1WorkflowItemsTable from "../SMTA1WorkflowItems/components/SMTA1WorkflowItemsTable.vue";
import SMTA2WorkflowItemsTable from "../SMTA2WorkflowItems/components/SMTA2WorkflowItemsTable.vue";

import { AppError } from "@/models/shared/Error";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";

import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { SMTA1WorkflowItemModule } from "../SMTA1WorkflowItems/store";
import { SMTA2WorkflowItemModule } from "../SMTA2WorkflowItems/store";
import { SMTARequestGridItem } from "@/models/SMTARequestGridItem";
import { getAreaFromRoleType } from "../../utils/helper";
import { SMTARequestModule } from "../SMTARequests/store";

@Component({
  components: {},
})
export default class SMTARequestsDashboardTable extends Vue {
  @Prop({ type: Array, default: [] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true] })
  readonly sortDesc: Array<string>;

  private search = "";

  private linkPageSuffix = "";

  private title = "SMTA 1 Workflow";
  private baseHeaders = [
    {
      text: "Workflow",
      align: "start",
      sortable: false,
      value: "WorkflowItemTitle",
    },
    {
      text: "Send Date",
      align: "start",
      sortable: false,
      value: "SendDate",
    },
    {
      text: "Send By",
      align: "start",
      sortable: false,
      value: "SendBy",
    },
    {
      text: "Institution",
      align: "start",
      sortable: false,
      value: "Institution",
    },
    {
      text: "SMTA Type",
      align: "start",
      sortable: false,
      value: "SMTATypeString",
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
    return false;
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

  get workflowGridItems(): Array<SMTARequestGridItem> {
    const smtaRequests = SMTARequestModule.SMTARequests;

    if (!smtaRequests) return new Array<SMTARequestGridItem>();

    return smtaRequests.map((elem) => {
      return {
        Id: elem.Id,
        WorkflowItemTitle: elem.WorkflowItemTitle,
        OperationDate: elem.OperationDate,
        SendDate: this.getFormatDate(elem.OperationDate),
        SendBy: elem.SendBy,
        Institution: elem.Institution,
        SMTAType: elem.SMTAType,
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

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanAccessWorklist);
  }

  async loadPageInfo() {
    await SMTARequestModule.ListSMTARequests();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  selected(item: SMTARequestGridItem): void {
    const area = getAreaFromRoleType();

    if (item) {
      if (item.SMTAType == "SMTA 1") {
        this.$router.push({
          name: area + "-smta1-workflow-details",
          params: { id: item.Id },
        });
      } else {
        this.$router.push({
          name: area + "-smta2-workflow-details",
          params: { id: item.Id },
        });
      }
    }
  }
}
</script>
