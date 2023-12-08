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
        <v-btn v-if="canCreate" color="primary" @click="create">
          {{ createName }}
        </v-btn>
      </v-card-title>
      <v-card-text>
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="userGridItems"
          :search="search"
          :sort-by.sync="sortBy"
          :sort-desc="sortDesc"
          :custom-filter="customSearch"
          @click:row="selected"
        >
          <template #[`item.Email`]="{ item }">
            <a :href="getMailTo(item.Email)" @click="emailClicked">{{
              item.Email
            }}</a>
          </template>
          <template #[`item.OperationalFocalPoint`]="{ item }">
            <v-icon
              v-if="item.OperationalFocalPoint"
              small
              class="mr-2 green--text"
              >mdi-check-circle</v-icon
            >
            <v-icon v-else small class="mr-2 red--text"
              >mdi-close-circle</v-icon
            >
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
      v-if="canDelete"
      ref="confirmationDialogComponent"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { User } from "@/models/User";
import { Role } from "@/models/Role";
import { UserGridItem } from "@/models/UserGridItem";
import { UserModule } from "../store";
import BackButton from "../../../components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class UsersTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;
  private emailClick = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Member",
      align: "start",
      sortable: true,
      value: "Member",
    },
    {
      text: "Email",
      align: "start",
      sortable: true,
      value: "Email",
    },
    {
      text: "Application Role",
      align: "start",
      sortable: true,
      value: "ApplicationRole",
    },
    {
      text: "Operational Focal Point",
      align: "center",
      sortable: true,
      value: "OperationalFocalPoint",
    },
    {
      text: "Job Title",
      align: "start",
      sortable: true,
      value: "JobTitle",
    },
    {
      text: "Registration Date",
      align: "start",
      sortable: true,
      value: "RegistrationDate",
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

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Array, default: true })
  readonly users: Array<User>;

  @Prop({ type: Array, default: true })
  readonly roles: Array<Role>;

  @Prop({ type: String, default: "Users" })
  readonly title: string;

  @Prop({ type: Array, default: undefined })
  readonly overwriteHeaders:
    | Array<{
        newText: string;
        value: string;
        hide: boolean;
      }>
    | undefined;

  @Prop({ type: Array, default: ["ApplicationRole"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: false })
  readonly hideIsActive: boolean;

  @Prop({ type: Boolean, default: false })
  readonly hideOperationalFocalPoint: boolean;

  @Prop({ type: Boolean, default: false })
  readonly hideJobTitle: boolean;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: String, default: "Create" })
  readonly createName: string;

  get hasActionHeader(): boolean {
    return this.canEdit || this.canDelete;
  }

  get headers(): Array<{
    text: string;
    align: string;
    sortable: boolean;
    value: string;
  }> {
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

    if (this.hideIsActive) {
      headers = headers.filter((h) => {
        return h.value != "IsActive" && h.value != "IsActive";
      });
    }

    if (this.hideJobTitle) {
      headers = headers.filter((h) => {
        return h.value != "JobTitle" && h.value != "JobTitle";
      });
    }

    if (this.hideOperationalFocalPoint) {
      headers = headers.filter((h) => {
        return (
          h.value != "OperationalFocalPoint" &&
          h.value != "OperationalFocalPoint"
        );
      });
    }

    if (this.overwriteHeaders != undefined) {
      headers = headers.reduce(
        (result, header) => {
          let overwriteHeader = this.overwriteHeaders?.find(
            (oh) => oh.value == header.value
          );
          if (!overwriteHeader?.hide) {
            result.push({
              text: overwriteHeader?.newText
                ? overwriteHeader?.newText
                : header.text,
              align: header.align,
              sortable: header.sortable,
              value: header.value,
            });
          }
          return result;
        },
        [] as Array<{
          text: string;
          align: string;
          sortable: boolean;
          value: string;
        }>
      );
    }

    return headers;
  }

  get userGridItems(): Array<UserGridItem> {
    const users = this.users;
    if (!users) return new Array<UserGridItem>();

    return users.map((l) => {
      var role = this.roles
        .filter((r) => {
          return l.RoleId == r.Id;
        })
        .map((m) => {
          return {
            roleName: m.Name,
          };
        });

      if (role.length == 0) {
        role.push({
          roleName: "",
        });
      }
      return {
        Id: l.Id,
        Member: `${l.FirstName} ${l.LastName}`,
        Email: l.Email,
        ApplicationRole: role[0].roleName,
        IsActive: l.IsActive,
        JobTitle: l.JobTitle,
        OperationalFocalPoint: l.OperationalFocalPoint,
        RegistrationDate: l.CreationDate
          ? this.getFormatDate(l.CreationDate)
          : "",
      };
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  emailClicked(): void {
    this.emailClick = true;
  }

  getMailTo(email: string): string {
    return "mailto:" + email;
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: UserGridItem): void {
    this.editClicked = true;
    const usr: User = this.getUser(item.Id);
    this.$emit("edit", usr);
  }

  executeDelete(item: UserGridItem): void {
    const usr: User = this.getUser(item.Id);
    this.$emit("delete", usr);
    this.deleteClicked = false;
  }

  deleteItem(item: UserGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: UserGridItem): void {
    if (
      this.deleteClicked == false &&
      this.editClicked == false &&
      this.emailClick == false
    ) {
      const usr: User = this.getUser(item.Id);
      this.$emit("selected", usr);
    }
    this.deleteClicked = false;
    this.editClicked = false;
    this.emailClick = false;
  }

  getUser(id: string): User {
    const usr: User | undefined = UserModule.Users.find((x) => x.Id == id);
    if (usr === undefined)
      throw {
        message: `Unexpected undefined for User with id ${id}`,
      };
    return usr;
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
