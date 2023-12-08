<template>
  <div>
    <v-card class="mb-5">
      <v-card-text>
        <v-data-table
          :headers="headers"
          :items="shippingInformationGridItems"
          :search="search"
          :custom-filter="customSearch"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
        >
        </v-data-table>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { MaterialShippingInformationGridItem } from "@/models/MaterialShippingInformationGridItem";
import { MaterialProductModule } from "../../materialProducts/store";
import { TransportCategoryModule } from "../../transportCategories/store";
import { CourierModule } from "../store";

@Component({
  components: {},
})
export default class CourierShippingInformationsTable extends Vue {
  private search = "";

  private headers = [
    {
      text: "Your Facility's Material Reference Number",
      align: "start",
      sortable: true,
      value: "MaterialNumber",
    },
    {
      text: "Type of Material",
      align: "start",
      sortable: true,
      value: "MaterialProduct",
    },
    {
      text: "Transport Category",
      align: "start",
      sortable: true,
      value: "TransportCategory",
    },
    {
      text: "Condition",
      align: "start",
      sortable: true,
      value: "Condition",
    },
    {
      text: "Additional Information",
      align: "start",
      sortable: true,
      value: "AdditionalInformation",
    },
    {
      text: "Quantity (# of Vials)",
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

  @Prop({ type: Array, default: ["MaterialProduct"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  get shippingInformationGridItems(): Array<MaterialShippingInformationGridItem> {
    const shippingInformations =
      CourierModule.CourierBookingForm?.MaterialShippingInformations;

    if (!shippingInformations)
      return new Array<MaterialShippingInformationGridItem>();

    return shippingInformations.map((l) => {
      const materialProduct = MaterialProductModule.MaterialProducts.filter(
        (mp) => {
          return l.MaterialProductId == mp.Id;
        }
      ).map((m) => {
        return {
          materialProductName: m.Name,
        };
      });

      if (materialProduct.length == 0) {
        materialProduct.push({
          materialProductName: "",
        });
      }

      const transportCategory =
        TransportCategoryModule.TransportCategories.filter((mp) => {
          return l.TransportCategoryId == mp.Id;
        }).map((m) => {
          return {
            transportCategoryName: m.Name,
          };
        });

      if (transportCategory.length == 0) {
        transportCategory.push({
          transportCategoryName: "",
        });
      }

      return {
        Id: l.Id,
        MaterialNumber: l.MaterialNumber,
        MaterialProduct: materialProduct[0].materialProductName,
        MaterialProductId: l.MaterialProductId,
        TransportCategory: transportCategory[0].transportCategoryName,
        TransportCategoryId: l.TransportCategoryId,
        Quantity: l.Quantity,
        Amount: l.Amount,
        Condition: l.Condition,
        AdditionalInformation: l.AdditionalInformation,
      };
    });
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
