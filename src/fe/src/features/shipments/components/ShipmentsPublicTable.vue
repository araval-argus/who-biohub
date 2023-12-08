<template>
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
    <v-card-subtitle>
      <h4>{{ laboratoryName }}</h4>
    </v-card-subtitle>
    <v-card-text>
      <div v-if="loading">
        <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
        <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
      </div>
      <v-data-table
        v-else
        :items-per-page="10"
        :headers="headers"
        :items="shipmentGridItems"
        :search="search"
        :custom-filter="customSearch"
        @click:row="selected"
      >
      </v-data-table>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { ShipmentPublicGridItem } from "@/models/ShipmentPublicGridItem";
import { ShipmentModule } from "../store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import { LaboratoryModule } from "../../laboratories/store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";
import { ShipmentPublic } from "@/models/ShipmentPublic";
import { ShipmentDirection } from "@/models/enums/ShipmentDirection";

@Component({ components: { BackButton } })
export default class ShipmentsPublicTable extends Vue {
  @Prop({ type: String, default: null })
  readonly laboratoryId: string;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Array, default: [] })
  readonly shipments: Array<ShipmentPublic>;

  private search = "";

  @Prop({ type: String, default: "Shipments" })
  readonly title: string;

  private headers = [
    {
      text: "Shipment Reference Number",
      align: "start",
      sortable: true,
      value: "ReferenceNumber",
    },

    {
      text: "From",
      align: "start",
      sortable: true,
      value: "From",
    },
    {
      text: "To",
      align: "start",
      sortable: true,
      value: "To",
    },

    {
      text: "Completed On",
      align: "start",
      sortable: true,
      value: "CompletedOn",
    },
  ];

  get laboratoryName(): string {
    if (this.laboratoryId != "") {
      const labId = this.laboratoryId;
      var laboratory = LaboratoryModule.LaboratoriesPublic.filter((l) => {
        return l.Id == labId;
      }).map((m) => {
        return {
          laboratoryName: m.Name,
        };
      });

      if (laboratory.length != 0) {
        return laboratory[0].laboratoryName;
      }

      var bioHubFacility = BioHubFacilityModule.BioHubFacilitiesPublic.filter(
        (b) => {
          return b.Id == labId;
        }
      ).map((m) => {
        return {
          laboratoryName: m.Name,
        };
      });

      if (bioHubFacility.length != 0) {
        return bioHubFacility[0].laboratoryName;
      }
    }
    return "";
  }

  get shipmentGridItems(): Array<ShipmentPublicGridItem> {
    let shipments = this.shipments;

    if (!shipments) return new Array<ShipmentPublicGridItem>();

    return shipments.map((s) => {
      if (this.laboratoryId != "") {
        const labId = this.laboratoryId;
        shipments = shipments.filter((s) => {
          return s.LaboratoryId == labId || s.BioHubFacilityId == labId;
        });
      }

      return {
        Id: s.Id,
        ReferenceNumber: s.ReferenceNumber,
        To: s.To,
        From: s.From,
        StatusOfRequest: s.StatusOfRequest,
        CompletedOn: this.getFormatDate(s.CompletedOn),
        ShipmentDirection: s.ShipmentDirection,
        ShipmentDirectionString:
          s.ShipmentDirection == ShipmentDirection.FromBioHub
            ? "From BioHub (SMTA 2)"
            : "Into BioHub (SMTA 1)",
      };
    });
  }

  selected(item: ShipmentPublicGridItem): void {
    var customParams = { id: item.Id };

    this.$router.push({
      name: "public-shipment-details",
      params: customParams,
    });
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
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
