<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="materialGridItems"
      :search="search"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
    >
    </v-data-table>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { WorklistFromBioHubItemMaterialGridItem } from "@/models/WorklistFromBioHubItemMaterialGridItem";
import { CourierModule } from "../store";

@Component({ components: {} })
export default class CourierMaterialsTable extends Vue {
  private search = "";

  private headers = [
    {
      text: "BMEPP Name",
      align: "start",
      sortable: true,
      value: "MaterialName",
    },
    {
      text: "Ref Number",
      align: "start",
      sortable: true,
      value: "MaterialNumber",
    },
    {
      text: "Quantity (number of vials)",
      align: "start",
      sortable: true,
      value: "Quantity",
    },
    {
      text: "Amount/Vial",
      align: "start",
      sortable: true,
      value: "Amount",
    },
  ];

  @Prop({ type: Array, default: ["MaterialName"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  get materialGridItems(): Array<WorklistFromBioHubItemMaterialGridItem> {
    const materials =
      CourierModule.CourierBookingForm?.WorklistFromBioHubItemMaterials;

    if (!materials) return new Array<WorklistFromBioHubItemMaterialGridItem>();

    return materials.map((l) => {
      return {
        Id: l.Id,
        WorklistFromBioHubItemId: l.WorklistFromBioHubItemId,
        MaterialId: l.MaterialId,
        Quantity: l.Quantity,
        AvailableQuantity: l.AvailableQuantity,
        QuantityInfo: "vial(s)",
        Amount: l.Amount,
        AmountInfo: "ml/vial",
        MaterialNumber: l.MaterialNumber,
        MaterialProductId: l.MaterialProductId,
        TransportCategoryId: l.TransportCategoryId,
        MaterialName: l.MaterialName,
        CollectionDate: this.getFormatDate(l.CollectionDate),
        Location: l.Location,
        IsolationHostTypeId: l.IsolationHostTypeId,
        Gender: "",
        Age: l.Age,
        Condition: l.Condition,
        Note: l.Note,
        Status: l.Status,
      };
    });
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
