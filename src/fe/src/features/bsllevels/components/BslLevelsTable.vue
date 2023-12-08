<template>
  <div>
    <v-card class="mb-5">
      <v-card-title>
        {{ title }}
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Table Search"
          single-line
          hide-details
          class="mr-8"
        ></v-text-field>
        <v-btn color="primary" @click="create"> Create </v-btn>
      </v-card-title>
      <v-card-text>
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="BSLLevelGridItems"
          :search="search"
          :custom-filter="customSearch"
          @click:row="selected"
        >
          <template #[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              mdi-pencil-circle-outline
            </v-icon>
            <v-icon small @click="deleteItem(item)">
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
import { Component, Prop, Vue } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { BSLLevel } from "@/models/BSLLevel";
import { BSLLevelGridItem } from "@/models/BSLLevelGridItem";
import { BSLLevelModule } from "../store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class BslLevelTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private title = "Biosafety Levels";
  private headers = [
    {
      text: "Actions",
      align: "start",
      sortable: false,
      value: "actions",
    },
    {
      text: "Name",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "Code",
      align: "start",
      sortable: true,
      value: "Code",
    },
  ];

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  get BSLLevelGridItems(): Array<BSLLevelGridItem> {
    const BSLLevels = BSLLevelModule.BSLLevels;
    if (!BSLLevels) return new Array<BSLLevelGridItem>();

    return BSLLevels.map((l) => {
      return {
        Id: l.Id,
        Name: l.Name,
        Code: l.Code,
        Description: l.Description,
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: BSLLevelGridItem): void {
    this.editClicked = true;
    const lab: BSLLevel = this.getBSLLevel(item.Id);
    this.$emit("edit", lab);
  }

  executeDelete(item: BSLLevelGridItem): void {
    const lab: BSLLevel = this.getBSLLevel(item.Id);
    this.$emit("delete", lab);
    this.deleteClicked = false;
  }

  deleteItem(item: BSLLevelGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: BSLLevelGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      const lab: BSLLevel = this.getBSLLevel(item.Id);
      this.$emit("selected", lab);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  getBSLLevel(id: string): BSLLevel {
    const lab: BSLLevel | undefined = BSLLevelModule.BSLLevels.find(
      (x) => x.Id == id
    );
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for BSLLevel with id ${id}`,
      };
    return lab;
  }

  create(): void {
    this.$emit("create");
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
