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
          :items="courierGridItems"
          :search="search"
          :custom-filter="customSearch"
          @click:row="selected"
        >
          <template #[`item.Email`]="{ item }">
            <a :href="getMailTo(item.Email)" @click="emailClicked">{{
              item.Email
            }}</a>
          </template>
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
import { Courier } from "@/models/Courier";
import { CourierGridItem } from "@/models/CourierGridItem";
import { CourierModule } from "../store";
import { CountryModule } from "../../countries/store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class CouriersTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;
  private search = "";

  private emailClick = false;

  private title = "Couriers";
  private baseHeaders = [
    {
      text: "Name",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "WHO Account Number",
      align: "start",
      sortable: true,
      value: "WHOAccountNumber",
    },
    {
      text: "Country",
      align: "start",
      sortable: true,
      value: "Country",
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

  get courierGridItems(): Array<CourierGridItem> {
    const couriers = CourierModule.Couriers;
    if (!couriers) return new Array<CourierGridItem>();

    return couriers.map((l) => {
      var country = CountryModule.Countries.filter((c) => {
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
        WHOAccountNumber: l.WHOAccountNumber,
        Email: l.Email,
        IsActive: l.IsActive,
        Description: l.Description,
        Address: l.Address,
        BusinessPhone: l.BusinessPhone,
        Latitude: l.Latitude,
        Longitude: l.Longitude,
        Country: country[0].countryName,
        CountryId: l.CountryId,
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: CourierGridItem): void {
    this.editClicked = true;
    const lab: Courier = this.getCourier(item.Id);
    this.$emit("edit", lab);
  }

  executeDelete(item: CourierGridItem): void {
    const lab: Courier = this.getCourier(item.Id);
    this.$emit("delete", lab);
    this.deleteClicked = false;
  }

  deleteItem(item: CourierGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: CourierGridItem): void {
    if (
      this.deleteClicked == false &&
      this.editClicked == false &&
      this.emailClick == false
    ) {
      const lab: Courier = this.getCourier(item.Id);
      this.$emit("selected", lab);
    }
    this.deleteClicked = false;
    this.editClicked = false;
    this.emailClick = false;
  }

  getCourier(id: string): Courier {
    const lab: Courier | undefined = CourierModule.Couriers.find(
      (x) => x.Id == id
    );
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for Courier with id ${id}`,
      };
    return lab;
  }

  getMailTo(email: string): string {
    return "mailto:" + email;
  }

  emailClicked(): void {
    this.emailClick = true;
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
