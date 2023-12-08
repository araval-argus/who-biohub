<template>
  <div>
    <v-data-table
      class="mb-8"
      :headers="headers"
      :items="materialGridItems"
      :search="search"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
      @click:row="selected"
    >
      <template #[`item.warning`]="{ item }">
        <v-icon
          v-if="item.Condition == null"
          small
          class="mr-2"
          style="color: #c7bc3c"
        >
          mdi-alert
        </v-icon>
        <p v-else></p>
      </template>
    </v-data-table>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../../components/ConfirmationDialogComponent.vue";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { WorklistFromBioHubItemMaterialGridItem } from "@/models/WorklistFromBioHubItemMaterialGridItem";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import { WorklistFromBioHubItemModule } from "../../store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { TransportCategoryModule } from "../../../transportCategories/store";

@Component({ components: { ConfirmationDialogComponent } })
export default class ShipmentMaterialsTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "",
      align: "start",
      sortable: false,
      value: "warning",
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
      text: "BMEPP Number",
      align: "start",
      sortable: true,
      value: "MaterialNumber",
    },
    {
      text: "Collection Date",
      align: "start",
      sortable: true,
      value: "CollectionDate",
    },
    {
      text: "Location",
      align: "start",
      sortable: true,
      value: "Location",
    },
    {
      text: "Host",
      align: "start",
      sortable: true,
      value: "IsolationHostType",
    },
    {
      text: "Gender",
      align: "start",
      sortable: true,
      value: "Gender",
    },
    {
      text: "Age",
      align: "start",
      sortable: true,
      value: "Age",
    },
    {
      text: "Number of vials",
      align: "start",
      sortable: true,
      value: "Quantity",
    },
    {
      text: "Volume per vial (ml/vial)",
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

  get materialGridItems(): Array<WorklistFromBioHubItemMaterialGridItem> {
    const materials =
      WorklistFromBioHubItemModule.WorklistFromBioHubItemMaterials;
    if (!materials) return new Array<WorklistFromBioHubItemMaterialGridItem>();

    return materials.map((l) => {
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

      var host = IsolationHostTypeModule.IsolationHostTypes.filter((iht) => {
        return l.IsolationHostTypeId == iht.Id;
      }).map((m) => {
        return {
          hostName: m.Name,
        };
      });

      if (host.length == 0) {
        host.push({
          hostName: "",
        });
      }

      var gender = "";

      if (l.Gender === Gender.Male) {
        gender = "Male";
      } else if (l.Gender === Gender.Female) {
        gender = "Female";
      } else {
        gender = "Undisclosed";
      }

      var condition = "";

      if (l.Condition === ShipmentMaterialCondition.Intact) {
        condition = "Intact";
      } else if (l.Condition === ShipmentMaterialCondition.Damaged) {
        condition = "Damaged";
      }

      return {
        Id: l.Id,
        MaterialProduct: materialProduct[0].materialProductName,
        MaterialProductId: l.MaterialProductId,
        TransportCategoryId: l.TransportCategoryId,
        TransportCategory: transportCategory[0].transportCategoryName,
        MaterialNumber: l.MaterialNumber,
        CollectionDate: l.CollectionDate
          ? this.getFormatDate(l.CollectionDate)
          : "",
        Location: l.Location,
        IsolationHostType: host[0].hostName,
        IsolationHostTypeId: l.IsolationHostTypeId,
        Gender: gender,
        Age: l.Age,
        Condition: l.Condition,
        ConditionString: condition,
        Note: l.Note,
        WorklistFromBioHubItemId: l.WorklistFromBioHubItemId,
        MaterialId: l.MaterialId,
        Quantity: l.Quantity,
        AvailableQuantity: 0,
        QuantityInfo: "vial(s)",
        Amount: l.Amount,
        AmountInfo: "ml/vial",
        MaterialName: l.MaterialName,
        Status: l.Status,
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  get hasActionHeader(): boolean {
    return false;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
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

  selected(item: WorklistFromBioHubItemMaterialGridItem): void {
    this.$emit("selected", item);
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
