<template>
  <div>
    <v-card-title>
      <h2>Materials</h2>
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
    <v-data-table
      class="mb-8"
      :headers="headers"
      :items="materialGridItems"
      :search="search"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
      @click:row="selected"
    >
    </v-data-table>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

import { ShipmentModule } from "../store";
import { MaterialProductModule } from "../../materialProducts/store";
import { IsolationHostTypeModule } from "../../isolationHostTypes/store";
import { Gender } from "@/models/enums/Gender";

import { Shipment } from "@/models/Shipment";
import { ShipmentMaterial } from "@/models/ShipmentMaterial";
import { ShipmentPublicMaterialGridItem } from "@/models/ShipmentPublicMaterialGridItem";
import { MaterialStatus } from "@/models/enums/MaterialStatus";

import { hasPermission } from "../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: {} })
export default class ShipmentMaterialsTablePublic extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "BMEPP Number",
      align: "start",
      sortable: true,
      value: "MaterialNumber",
    },
    {
      text: "BMEPP Name",
      align: "start",
      sortable: true,
      value: "MaterialName",
    },
    {
      text: "Type of Material",
      align: "start",
      sortable: true,
      value: "MaterialProduct",
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

  @Prop({ type: Array, default: ["MaterialProduct"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  get materialGridItems(): Array<ShipmentPublicMaterialGridItem> {
    const materials = ShipmentModule.ShipmentPublicMaterials;
    if (!materials) return new Array<ShipmentPublicMaterialGridItem>();

    return materials.map((l) => {
      var materialProduct = MaterialProductModule.MaterialProducts.filter(
        (mp) => {
          return l.MaterialProductId == mp.Id;
        }
      ).map((m) => {
        return {
          materialProductName:
            m.Name == m.Description
              ? m.Name
              : m.Name + " (" + m.Description + ")",
        };
      });

      if (materialProduct.length == 0) {
        materialProduct.push({
          materialProductName: "",
        });
      }

      return {
        Id: l.Id,
        MaterialProduct: materialProduct[0].materialProductName,
        MaterialProductId: l.MaterialProductId,
        MaterialNumber: l.MaterialNumber,
        MaterialName: l.MaterialName,
        MaterialId: l.MaterialId,
      };
    });
  }

  get hasActionHeader(): boolean {
    return false;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
  }

  get routingNamePrefix(): string {
    return "public-";
  }

  selected(item: ShipmentPublicMaterialGridItem): void {
    var customParams = { id: item.MaterialId };

    this.$router.push({
      name: "public-material-details",
      params: customParams,
    });
  }

  sortByMaterialNumber(a: any, b: any) {
    if (a.MaterialNumber < b.MaterialNumber) {
      return 1;
    }
    if (a.MaterialNumber > b.MaterialNumber) {
      return -1;
    }
    return 0;
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
