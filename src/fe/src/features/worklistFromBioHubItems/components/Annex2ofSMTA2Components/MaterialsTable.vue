<template>
  <div>
    <!-- <v-card class="mb-5">
      <v-card-text> -->
    <v-data-table
      :headers="headers"
      :items="materialGridItems"
      :search="search"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
    >
      <template #[`item.Quantity`]="{ item }">
        <v-text-field
          v-if="canEdit"
          :key="key"
          v-model="item.Quantity"
          type="number"
          :error-messages="getQuantityErrorMessage(item)"
          :error-count="getQuantityErrorMessage(item).length"
          @input="update(item)"
        ></v-text-field>
        <span v-else>{{ item.Quantity }}</span>
      </template>
      <template v-if="!hideAmount" #[`item.Amount`]="{ item }">
        <v-text-field
          v-if="canEdit"
          v-model="item.Amount"
          type="number"
          :error-messages="getAmountErrorMessage(item)"
          :error-count="getAmountErrorMessage(item).length"
          @input="update(item)"
        ></v-text-field>
        <span v-else>{{ item.Amount }}</span>
      </template>
      <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
        <v-icon v-if="canDelete" small @click="deleteItem(item)">
          mdi-delete-circle-outline
        </v-icon>
      </template>
    </v-data-table>
    <!-- </v-card-text>
    </v-card> -->
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../../components/ConfirmationDialogComponent.vue";
import { WorklistFromBioHubItemMaterialGridItem } from "@/models/WorklistFromBioHubItemMaterialGridItem";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
//import { WorklistFromBioHubItemModule } from "../../store";

@Component({ components: { ConfirmationDialogComponent } })
export default class MaterialsTable extends Vue {
  private deleteClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "BMEPP Name",
      align: "start",
      sortable: true,
      value: "MaterialName",
    },
    {
      text: "WHO BMEPP Ref No.",
      align: "start",
      sortable: true,
      value: "MaterialNumber",
    },
    {
      text: "Quantity (Number of Vials)",
      align: "start",
      sortable: true,
      value: "Quantity",
    },
    {
      text: "",
      align: "start",
      sortable: true,
      value: "QuantityInfo",
    },
    {
      text: "Amount per Vial (ml)",
      align: "start",
      sortable: true,
      value: "Amount",
    },
    {
      text: "",
      align: "start",
      sortable: true,
      value: "AmountInfo",
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

  private key = 1;

  @Prop({ type: Array, default: ["MaterialName"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  // @Prop({ type: Boolean, default: true })
  // readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Boolean, default: false })
  readonly hideAmount: boolean;

  @Prop({ type: String, default: "" })
  readonly transportCategoryId: string;

  @Prop({ type: Boolean, default: false })
  readonly checkQuantity: boolean;

  @Prop({ type: Array, default: [] })
  readonly materials: Array<WorklistFromBioHubItemMaterial>;

  get materialGridItems(): Array<WorklistFromBioHubItemMaterialGridItem> {
    const materials =
      this.transportCategoryId != ""
        ? this.materials.filter((m) => {
            return m.TransportCategoryId == this.transportCategoryId;
          })
        : this.materials;

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

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  get hasActionHeader(): boolean {
    return this.canDelete;
  }

  get headers(): any {
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

    if (this.hideAmount) {
      headers = headers.filter((h) => {
        return h.value != "Amount" && h.value != "Amount";
      });
    }

    return headers;
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  executeDelete(item: WorklistFromBioHubItemMaterialGridItem): void {
    //WorklistFromBioHubItemModule.REMOVE_MATERIAL(item.Id);
    this.$emit("deleteMaterial", item.Id);
    this.deleteClicked = false;
  }

  deleteItem(item: WorklistFromBioHubItemMaterialGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  update(item: WorklistFromBioHubItemMaterialGridItem): void {
    let material: WorklistFromBioHubItemMaterial =
      this.getWorklistFromBioHubItemMaterial(item.Id);
    material.Quantity = this.formatValue(item.Quantity, false);
    material.Amount = this.formatValue(item.Amount, true);
    //WorklistFromBioHubItemModule.UPDATE_MATERIAL(material);
    this.$emit("updateMaterial", material);
  }

  formatValue(value: number | null, decimal: boolean): number {
    let result = 0;
    value = value ?? 0;
    if (decimal) value = this.removeInaspectedChars(value);
    if (value != null) {
      value = decimal
        ? this.round(parseFloat(value.toString().trim()))
        : parseInt(value.toString().trim());

      if (value.toString() == "NaN") {
        result = 0;
      } else {
        result = value;
      }
    }
    return result;
  }

  private round(num: number): number {
    var exp = Math.pow(10, 1);
    return Math.floor(num * exp + 0.5) / exp;
  }

  private removeInaspectedChars(value: number): number | null {
    if (value != null) {
      if (isNaN(value)) {
        let newValues = new Array<string>();
        for (const v of value.toString()) {
          if (!isNaN(parseInt(v))) newValues.push(v);
          if (v === "." || v === ",") newValues.push(".");
        }
        return newValues.length > 0 ? parseFloat(newValues.join("")) : null;
      }
    }

    return value;
  }

  getQuantityErrorMessage(
    item: WorklistFromBioHubItemMaterialGridItem
  ): Array<string> {
    if (
      item.Quantity === undefined ||
      item.Quantity === null ||
      isNaN(item.Quantity) ||
      item.Quantity <= 0
    ) {
      return ["'Field' is Required"];
    } else if (this.checkQuantity && item.Quantity > item.AvailableQuantity) {
      return ["Value higher than available quantity"];
    } else {
      return [];
    }
  }

  getAmountErrorMessage(
    item: WorklistFromBioHubItemMaterialGridItem
  ): Array<string> {
    if (
      item.Amount === undefined ||
      item.Amount === null ||
      isNaN(item.Amount) ||
      item.Amount <= 0
    ) {
      return ["'Field' is Required"];
    } else {
      return [];
    }
  }

  getWorklistFromBioHubItemMaterial(
    id: string
  ): WorklistFromBioHubItemMaterial {
    const material: WorklistFromBioHubItemMaterial | undefined =
      this.materials.find((x) => x.Id == id);
    if (material === undefined)
      throw {
        message: `Unexpected undefined for WorklistFromBioHubItemsFocalPoint with id ${id}`,
      };
    return material;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
