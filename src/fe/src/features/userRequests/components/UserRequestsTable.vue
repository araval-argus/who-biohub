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
          :items="userRequestGridItems"
          :search="search"
          :custom-filter="customSearch"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          @click:row="selected"
        >
          <template #[`item.Email`]="{ item }">
            <a :href="getMailTo(item.Email)" @click="emailClicked">{{
              item.Email
            }}</a>
          </template>
          <template #[`item.Status`]="{ item }">
            <span
              v-if="item.Status == 0"
              class="font-weight-bold light-blue--text"
              >Pending</span
            >
            <span v-if="item.Status == 1" class="font-weight-bold green--text"
              >Approved</span
            >
            <span v-if="item.Status == 2" class="font-weight-bold red--text"
              >Rejected</span
            >
          </template>
          <template v-if="hasActionHeader" #[`item.Actions`]="{ item }">
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
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { UserRequestModule } from "../store";
import BackButton from "../../../components/BackButton.vue";
import { CountryModule } from "../../countries/store";
import { RoleModule } from "../../roles/store";
import { UserRequestGridItem } from "@/models/UserRequestGridItem";
import { UserRequest } from "@/models/UserRequest";
import { Role } from "@/models/Role";
import { Laboratory } from "@/models/Laboratory";
import { customTableSearch } from "../../../utils/helper";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class UserRequestsTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Requester",
      align: "start",
      sortable: true,
      value: "Requests",
    },
    {
      text: "Email",
      align: "start",
      sortable: true,
      value: "Email",
    },
    {
      text: "Role",
      align: "start",
      sortable: true,
      value: "Role",
    },
    {
      text: "Facility/Institute Name",
      align: "start",
      sortable: true,
      value: "InstituteName",
    },
    {
      text: "Country",
      align: "start",
      sortable: true,
      value: "Country",
    },
    {
      text: "Request Date",
      align: "start",
      sortable: true,
      value: "RequestDate",
    },
    {
      text: "Registration Date",
      align: "start",
      sortable: true,
      value: "RegistrationDate",
    },
    {
      text: "Status",
      align: "start",
      sortable: true,
      value: "Status",
    },
  ];

  private actionHeader = [
    {
      text: "Actions",
      align: "center",
      sortable: false,
      value: "Actions",
    },
  ];

  private editableHeaders = this.actionHeader.concat(this.baseHeaders);
  private emailClick = false;

  @Prop({ type: Array, default: [] })
  readonly userRequests: Array<UserRequest>;

  @Prop({ type: Array, default: [] })
  readonly laboratories: Array<Laboratory>;

  @Prop({ type: Array, default: [] })
  readonly roles: Array<Role>;

  @Prop({ type: String, default: "User Access Requests" })
  readonly title: string;

  @Prop({ type: Array, default: ["RegistrationDate", "Status"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: false })
  readonly hideInstituteInformation: boolean;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  readonly customBaseHeaders: Array<object>;

  get userRequestGridItems(): Array<UserRequestGridItem> {
    if (!this.userRequests) return new Array<UserRequestGridItem>();

    return this.userRequests.map((ur) => {
      // Country
      const country = CountryModule.Countries.filter((country) => {
        return ur.CountryId == country.Id;
      }).map((m) => {
        return {
          countryName: m.Name,
        };
      });

      // Role
      const role = this.roles
        .filter((role) => {
          return ur.RoleId == role.Id;
        })
        .map((m) => {
          return {
            roleName: m.Name,
          };
        });

      const laboratory = this.laboratories.filter((lab) => {
        return ur.LaboratoryId == lab.Id;
      });

      const instituteName = laboratory.length == 0 ? "" : laboratory[0].Name;

      return {
        Id: ur.Id,
        Requests: `${ur.FirstName} ${ur.LastName}`,
        Email: ur.Email,
        Role: role.length == 0 ? "" : role[0].roleName,
        InstituteName: instituteName != "" ? instituteName : ur.InstituteName,
        Country: country.length == 0 ? "" : country[0].countryName,
        RequestDate: ur.RequestDate ? this.getFormatDate(ur.RequestDate) : "",
        RegistrationDate: ur.RegistrationDate
          ? this.getFormatDate(ur.RegistrationDate)
          : "",
        Status: ur.Status,
      } as UserRequestGridItem;
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  get hasActionHeader(): boolean {
    return this.canEdit || this.canDelete;
  }

  get headers(): any {
    let headers = [];

    if (this.hasActionHeader == true) {
      headers = this.editableHeaders;
    } else {
      headers = this.baseHeaders;
    }

    if (this.hideInstituteInformation) {
      headers = headers.filter((h) => {
        return h.value != "InstituteName" && h.value != "Country";
      });
    }

    return headers;
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  getMailTo(email: string): string {
    return "mailto:" + email;
  }

  emailClicked(): void {
    this.emailClick = true;
  }

  editItem(item: UserRequestGridItem): void {
    this.editClicked = true;
    const ur: UserRequest = this.getUserRequest(item.Id);
    this.$emit("edit", ur);
  }

  executeDelete(item: UserRequestGridItem): void {
    const ur: UserRequest = this.getUserRequest(item.Id);
    this.$emit("delete", ur);
    this.deleteClicked = false;
  }

  deleteItem(item: UserRequestGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: UserRequestGridItem): void {
    if (
      this.deleteClicked == false &&
      this.editClicked == false &&
      this.emailClick == false
    ) {
      const ur: UserRequest = this.getUserRequest(item.Id);
      this.$emit("selected", ur);
    }
    this.deleteClicked = false;
    this.editClicked = false;
    this.emailClick = false;
  }

  create(): void {
    this.$emit("create");
  }

  getUserRequest(id: string): UserRequest {
    const ur: UserRequest | undefined = UserRequestModule.UserRequests.find(
      (x) => x.Id == id
    );
    if (ur === undefined)
      throw {
        message: `Unexpected undefined for UserRequest with id ${id}`,
      };
    return ur;
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
