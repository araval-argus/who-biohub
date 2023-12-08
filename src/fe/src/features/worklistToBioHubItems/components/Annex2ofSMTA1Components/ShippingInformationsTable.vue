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
          <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
            <v-icon v-if="canEdit" small @click="deleteItem(item)">
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
    <ShippingInformationPopup ref="shippingInformationPopup">
    </ShippingInformationPopup>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../../components/ConfirmationDialogComponent.vue";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { MaterialShippingInformationGridItem } from "@/models/MaterialShippingInformationGridItem";
import { MaterialProductModule } from "../../../materialProducts/store";
import ShippingInformationPopup from "./ShippingInformationPopup.vue";
import { TransportCategoryModule } from "../../../transportCategories/store";

@Component({
  components: { ConfirmationDialogComponent, ShippingInformationPopup },
})
export default class ShippingInformationsTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Provider's Material ID",
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

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: String, default: "" })
  readonly transportCategoryId: string;

  @Prop({ type: Array, default: [] })
  readonly materialShippingInformations: Array<MaterialShippingInformation>;

  get shippingInformationGridItems(): Array<MaterialShippingInformationGridItem> {
    const shippingInformations =
      this.transportCategoryId != ""
        ? this.materialShippingInformations.filter((m) => {
            return m.TransportCategoryId == this.transportCategoryId;
          })
        : this.materialShippingInformations;

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

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
    shippingInformationPopup: ShippingInformationPopup;
  };

  get hasActionHeader(): boolean {
    return this.canEdit;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
  }

  executeDelete(item: MaterialShippingInformationGridItem): void {
    //WorklistToBioHubItemModule.REMOVE_MATERIAL_SHIPPING_INFORMATION(item.Id);
    this.$emit("removeMaterialShippingInformation", item.Id);
    this.deleteClicked = false;
  }

  deleteItem(item: MaterialShippingInformationGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  getMaterialShippingInformation(id: string): MaterialShippingInformation {
    const materialShippingInformation: MaterialShippingInformation | undefined =
      this.materialShippingInformations.find((x) => x.Id == id);
    if (materialShippingInformation === undefined)
      throw {
        message: `Unexpected undefined for MaterialShippingInformation with id ${id}`,
      };
    return materialShippingInformation;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
