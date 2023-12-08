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
import { ShipmentGridItem } from "@/models/ShipmentGridItem";
import { ShipmentModule } from "../store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import { LaboratoryModule } from "../../laboratories/store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";
import { AuthModule } from "../../../features/auth/store";
import { Shipment } from "@/models/Shipment";
import { ShipmentDirection } from "@/models/enums/ShipmentDirection";

@Component({ components: { BackButton } })
export default class ShipmentsTable extends Vue {
  @Prop({ type: String, default: null })
  readonly laboratoryId: string;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Array, default: [] })
  readonly shipments: Array<Shipment>;

  @Prop({ type: String, default: "Completed Shipments" })
  readonly title: string;

  private search = "";

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
      var laboratory = LaboratoryModule.Laboratories.filter((l) => {
        return l.Id == labId;
      }).map((m) => {
        return {
          laboratoryName: m.Name,
        };
      });

      if (laboratory.length != 0) {
        return laboratory[0].laboratoryName;
      }

      var bioHubFacility = BioHubFacilityModule.BioHubFacilities.filter((b) => {
        return b.Id == labId;
      }).map((m) => {
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

  get shipmentGridItems(): Array<ShipmentGridItem> {
    let shipments = this.shipments;

    const userLaboratoryId = AuthModule.LaboratoryId?.toLowerCase() ?? "";

    if (this.laboratoryId != "") {
      const labId = this.laboratoryId;

      shipments = shipments.filter((s) => {
        return s.LaboratoryId == labId || s.BioHubFacilityId == labId;
      });
    } else if (userLaboratoryId != "") {
      shipments = shipments.filter((s) => {
        return (
          s.LaboratoryId == userLaboratoryId ||
          s.BioHubFacilityId == userLaboratoryId
        );
      });
    }

    if (!shipments) return new Array<ShipmentGridItem>();

    return shipments.map((s) => {
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

  get routingNamePrefix(): string {
    var routeName = this.$route.name;
    if (routeName != null && routeName != undefined) {
      if (routeName.startsWith("who")) {
        return "whoarea-";
      } else if (routeName.startsWith("laboratory")) {
        return "laboratoryarea-";
      } else {
        return "biohubfacilityarea-";
      }
    } else {
      return "";
    }
  }

  selected(item: ShipmentGridItem): void {
    var customParams = { id: item.Id };

    this.$router.push({
      name: this.routingNamePrefix + "shipment-details",
      params: customParams,
    });
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }
}
</script>
