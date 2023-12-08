<template>
  <div>
    <div class="mb-5">
      <v-card-title>
        <h2>Worklist</h2>
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
          :items="worklistGridItems"
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
import WorklistToBioHubItemsTable from "../worklistToBioHubItems/components/WorklistToBioHubItemsTable.vue";
import WorklistFromBioHubItemsTable from "../worklistFromBioHubItems/components/WorklistFromBioHubItemsTable.vue";

import { AppError } from "@/models/shared/Error";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";

import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { WorklistToBioHubItemModule } from "../worklistToBioHubItems/store";
import { WorklistFromBioHubItemModule } from "../worklistFromBioHubItems/store";
import { ShipmentRequestGridItem } from "@/models/ShipmentRequestGridItem";
import { getAreaFromRoleType } from "../../utils/helper";
import { ShipmentDirection } from "@/models/enums/ShipmentDirection";
import { ShipmentRequestModule } from "../shipmentRequests/store";

@Component({
  components: {},
})
export default class ShipmentRequestsDashboardTable extends Vue {
  @Prop({ type: Array, default: [] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true] })
  readonly sortDesc: Array<string>;

  private search = "";

  private linkPageSuffix = "";

  private title = "Direction: Send BMEPP into BioHub (SMTA 1)";

  private baseHeaders = [
    {
      text: "Shipment Direction",
      align: "start",
      sortable: false,
      value: "ShipmentDirectionString",
    },
    {
      text: "Your Action",
      align: "start",
      sortable: false,
      value: "CurrentStatusName",
    },
    {
      text: "Previous Action",
      align: "start",
      sortable: false,
      value: "WorklistItemTitle",
    },
    {
      text: "By",
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
      text: "Country",
      align: "start",
      sortable: false,
      value: "LaboratoryCountryIso",
    },
    {
      text: "Action Completed Date",
      align: "start",
      sortable: false,
      value: "SendDate",
    },
  ];

  // private baseHeaders = [
  //   {
  //     text: "Worklist",
  //     align: "start",
  //     sortable: false,
  //     value: "WorklistItemTitle",
  //   },
  //   {
  //     text: "Send Date",
  //     align: "start",
  //     sortable: false,
  //     value: "SendDate",
  //   },
  //   {
  //     text: "Send By",
  //     align: "start",
  //     sortable: false,
  //     value: "SendBy",
  //   },
  //   {
  //     text: "Institution",
  //     align: "start",
  //     sortable: false,
  //     value: "Institution",
  //   },
  //   {
  //     text: "Shipment Direction",
  //     align: "start",
  //     sortable: false,
  //     value: "ShipmentDirectionString",
  //   },
  // ];

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

  get worklistGridItems(): Array<ShipmentRequestGridItem> {
    const shipmentRequests = ShipmentRequestModule.ShipmentRequests;

    if (!shipmentRequests) return new Array<ShipmentRequestGridItem>();

    return shipmentRequests.map((elem) => {
      return {
        Id: elem.Id,
        WorklistItemTitle: elem.WorklistItemTitle,
        OperationDate: elem.OperationDate,
        SendDate: this.getFormatDate(elem.OperationDate),
        SendBy: elem.SendBy,
        Institution: elem.Institution,
        ShipmentDirection: elem.ShipmentDirection,
        ShipmentDirectionString:
          elem.ShipmentDirection == ShipmentDirection.ToBioHub
            ? "To BioHub"
            : "From BioHub",
        CurrentStatusName: elem.CurrentStatusName,
        LaboratoryCountryIso: elem.LaboratoryCountryIso,
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
    await ShipmentRequestModule.ListShipmentRequests();
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

  selected(item: ShipmentRequestGridItem): void {
    const area = getAreaFromRoleType();

    if (item) {
      if (item.ShipmentDirection == ShipmentDirection.ToBioHub) {
        this.$router.push({
          name: area + "-worklist-to-bio-hub-details",
          params: { id: item.Id },
        });
      } else {
        this.$router.push({
          name: area + "-worklist-from-bio-hub-details",
          params: { id: item.Id },
        });
      }
    }
  }
}
</script>
