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
      </v-card-title>
      <v-card-text>
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="countryGridItems"
          :search="search"
          :custom-filter="customSearch"
          @click:row="selected"
        >
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
import { Country } from "@/models/Country";
import { CountryGridItem } from "@/models/CountryGridItem";
import { CountryModule } from "../store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class CountriesTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private title = "Countries";
  private headers = [
    {
      text: "Name",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "Uncode",
      align: "start",
      sortable: true,
      value: "Uncode",
    },
    {
      text: "Iso2",
      align: "start",
      sortable: true,
      value: "Iso2",
    },
    {
      text: "Iso3",
      align: "start",
      sortable: true,
      value: "Iso3",
    },
  ];

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  get countryGridItems(): Array<CountryGridItem> {
    const countries = CountryModule.Countries;
    if (!countries) return new Array<CountryGridItem>();

    return countries.map((l) => {
      return {
        Id: l.Id,
        Name: l.Name,
        Uncode: l.Uncode,
        IsActive: l.IsActive,
        FullName: l.FullName,
        Latitude: l.Latitude,
        Longitude: l.Longitude,
        Iso2: l.Iso2,
        Iso3: l.Iso3,
        GmtHour: l.GmtHour,
        GmtMin: l.GmtMin,
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: CountryGridItem): void {
    this.editClicked = true;
    const lab: Country = this.getCountry(item.Id);
    this.$emit("edit", lab);
  }

  executeDelete(item: CountryGridItem): void {
    const lab: Country = this.getCountry(item.Id);
    this.$emit("delete", lab);
    this.deleteClicked = false;
  }

  deleteItem(item: CountryGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: CountryGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      const lab: Country = this.getCountry(item.Id);
      this.$emit("selected", lab);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  getCountry(id: string): Country {
    const lab: Country | undefined = CountryModule.Countries.find(
      (x) => x.Id == id
    );
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for Country with id ${id}`,
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
