<template>
  <div>
    <v-card class="mb-5">
      <v-card-text>
        <v-data-table
          :headers="headers"
          :items="materialGSDInfoGridItems"
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
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";
import { MaterialGSDInfoGridItem } from "@/models/MaterialGSDInfoGridItem";
import { MaterialModule } from "../store";

import { GSDType } from "@/models/enums/GSDType";

@Component({
  components: { ConfirmationDialogComponent },
})
export default class MaterialGSDInfoTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "GSD of",
      align: "start",
      sortable: true,
      value: "GSDTypeString",
    },
    {
      text: "Cell Line",
      align: "start",
      sortable: true,
      value: "CellLine",
    },
    {
      text: "Passage Number",
      align: "start",
      sortable: true,
      value: "PassageNumber",
    },
    {
      text: "GSD Fasta",
      align: "start",
      sortable: true,
      value: "GSDFasta",
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

  @Prop({ type: Array, default: ["CellLine"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: Array, default: [] })
  readonly items: Array<MaterialGSDInfo>;

  get materialGSDInfoGridItems(): Array<MaterialGSDInfoGridItem> {
    const materialGSDInfo = this.items;

    if (!materialGSDInfo) return new Array<MaterialGSDInfoGridItem>();

    return materialGSDInfo.map((l) => {
      return {
        Id: l.Id,
        MaterialId: l.MaterialId,
        CellLine: l.CellLine,
        GSDType: l.GSDType,
        GSDFasta: l.GSDFasta,
        PassageNumber: l.PassageNumber,
        GSDTypeString:
          l.GSDType == GSDType.OriginalMaterial
            ? "Original Material"
            : "Cultured Material",
      };
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
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

  executeDelete(item: MaterialGSDInfoGridItem): void {
    this.$emit("delete", item.Id);
    this.deleteClicked = false;
  }

  deleteItem(item: MaterialGSDInfoGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  getMaterialGSDInfo(id: string): MaterialGSDInfo {
    const materialShippingInformation: MaterialGSDInfo | undefined =
      MaterialModule.MaterialGSDInfo.find((x) => x.Id == id);
    if (materialShippingInformation === undefined)
      throw {
        message: `Unexpected undefined for MaterialGSDInfo with id ${id}`,
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
