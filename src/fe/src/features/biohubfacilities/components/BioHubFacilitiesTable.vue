<template>
  <div>
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
        <v-btn v-if="canCreate" color="primary" @click="create"> Create </v-btn>
      </v-card-title>
      <v-card-text>
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="bioHubFacilityGridItems"
          :search="search"
          :custom-filter="customSearch"
          @click:row="selected"
        >
          <template #[`item.IsActive`]="{ item }">
            <v-icon v-if="item.IsActive" small class="mr-2 green--text"
              >mdi-check-circle</v-icon
            >
            <v-icon v-else small class="mr-2 red--text"
              >mdi-close-circle</v-icon
            >
          </template>
          <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
            <v-icon v-if="canEdit" small class="mr-2" @click="editItem(item)">
              mdi-pencil-circle-outline
            </v-icon>
            <v-icon v-if="canDelete" small @click="deleteItem(item)">
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
import { BioHubFacility } from "@/models/BioHubFacility";
import { BioHubFacilityGridItem } from "@/models/BioHubFacilityGridItem";
import { BioHubFacilityModule } from "../store";
import { BSLLevelModule } from "../../bsllevels/store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";
import { CountryModule } from "../../countries/store";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class BioHubFacilitiesTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;
  private search = "";

  private title = "BioHub Facilities";
  private baseHeaders = [
    {
      text: "Country",
      align: "start",
      sortable: true,
      value: "Country",
    },
    {
      text: "Name",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "Abbreviation",
      align: "start",
      sortable: true,
      value: "Abbreviation",
    },
    {
      text: "Biosafety level",
      align: "start",
      sortable: true,
      value: "BSLLevel",
    },
    {
      text: "Is Active",
      align: "center",
      sortable: true,
      value: "IsActive",
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

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  get bioHubFacilityGridItems(): Array<BioHubFacilityGridItem> {
    const bioHubFacilities = BioHubFacilityModule.BioHubFacilities;
    if (!bioHubFacilities) return new Array<BioHubFacilityGridItem>();

    return bioHubFacilities.map((l) => {
      const bslLevel = BSLLevelModule.BSLLevels.filter((bsl) => {
        return l.BSLLevelId == bsl.Id;
      }).map((m) => {
        return {
          bslLevelName: m.Name,
        };
      });

      if (bslLevel.length == 0) {
        bslLevel.push({
          bslLevelName: "",
        });
      }

      const country = CountryModule.Countries.filter((c) => {
        return l.CountryId == c.Id;
      }).map((m) => {
        return {
          countryName: m.Name,
        };
      });

      if (country.length == 0) {
        country.push({
          countryName: "",
        });
      }

      return {
        Id: l.Id,
        Name: l.Name,
        Abbreviation: l.Abbreviation,
        IsActive: l.IsActive,
        Description: l.Description,
        Address: l.Address,
        Latitude: l.Latitude,
        Longitude: l.Longitude,
        BSLLevel: bslLevel[0].bslLevelName,
        Country: country[0].countryName,
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: BioHubFacilityGridItem): void {
    this.editClicked = true;
    const lab: BioHubFacility = this.getBioHubFacility(item.Id);
    this.$emit("edit", lab);
  }

  executeDelete(item: BioHubFacilityGridItem): void {
    const lab: BioHubFacility = this.getBioHubFacility(item.Id);
    this.$emit("delete", lab);
    this.deleteClicked = false;
  }

  deleteItem(item: BioHubFacilityGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: BioHubFacilityGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      const lab: BioHubFacility = this.getBioHubFacility(item.Id);
      this.$emit("selected", lab);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  getBioHubFacility(id: string): BioHubFacility {
    const lab: BioHubFacility | undefined =
      BioHubFacilityModule.BioHubFacilities.find((x) => x.Id == id);
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for BioHubFacility with id ${id}`,
      };
    return lab;
  }

  get hasActionHeader(): boolean {
    return this.canEdit || this.canDelete;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
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
