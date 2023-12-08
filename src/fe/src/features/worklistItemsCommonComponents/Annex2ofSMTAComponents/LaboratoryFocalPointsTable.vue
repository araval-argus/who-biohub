<template>
  <div class="mt-5">
    <v-data-table
      :headers="headers"
      :items="laboratoryFocalPointGridItems"
      :search="search"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
    >
      <template #[`item.Email`]="{ item }">
        <a :href="getMailTo(item.Email)">{{ item.Email }}</a>
      </template>
      <template #[`item.Other`]="{ item }">
        <v-text-field
          v-if="canEdit"
          v-model="item.Other"
          :hide-details="true"
          dense
          single-line
          :autofocus="true"
          @input="updateOther(item)"
        ></v-text-field>
        <span v-else>{{ item.Other }}</span>
      </template>
      <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
        <v-icon v-if="canEdit" small @click="deleteItem(item)">
          mdi-delete-circle-outline
        </v-icon>
      </template>
    </v-data-table>

    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "@/components/ConfirmationDialogComponent.vue";
import { WorklistItemUserGridItem } from "@/models/WorklistItemUserGridItem";
import { WorklistItemUser } from "@/models/WorklistItemUser";

@Component({ components: { ConfirmationDialogComponent } })
export default class LaboratoryFocalPointsTable extends Vue {
  private deleteClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Name of State Member",
      align: "start",
      sortable: true,
      value: "Country",
    },
    {
      text: "Operational Focal Point",
      align: "start",
      sortable: true,
      value: "UserName",
    },
    {
      text: "Email",
      align: "start",
      sortable: true,
      value: "Email",
    },
    {
      text: "Job Title",
      align: "start",
      sortable: true,
      value: "JobTitle",
    },
    {
      text: "Mobile Phone",
      align: "start",
      sortable: true,
      value: "MobilePhone",
    },
    {
      text: "Laboratory",
      align: "start",
      sortable: true,
      value: "Laboratory",
    },
    {
      text: "Other",
      align: "start",
      sortable: true,
      value: "Other",
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

  @Prop({ type: Array, default: ["UserName"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  // @Prop({ type: Boolean, default: true })
  // readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Array, default: [] })
  readonly laboratoryFocalPoints: Array<WorklistItemUser>;

  get laboratoryFocalPointGridItems(): Array<WorklistItemUserGridItem> {
    const laboratoryFocalPoints = this.laboratoryFocalPoints;
    if (!laboratoryFocalPoints) return new Array<WorklistItemUserGridItem>();

    return laboratoryFocalPoints.map((l) => {
      return {
        Id: l.Id,
        UserId: l.UserId,
        UserName: l.UserName,
        Country: l.Country,
        Laboratory: l.Laboratory,
        BioHubFacility: l.BioHubFacility,
        MobilePhone: l.MobilePhone,
        BusinessPhone: l.BusinessPhone,
        Email: l.Email,
        JobTitle: l.JobTitle,
        Other: l.Other,
        WorklistItemId: l.WorklistItemId,
        LaboratoryId: l.LaboratoryId,
        BioHubFacilityId: l.BioHubFacilityId,
        CourierId: l.CourierId,
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

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  executeDelete(item: WorklistItemUserGridItem): void {
    //WorklistToBioHubItemModule.REMOVE_LABORATORY_FOCAL_POINT(item.Id);
    this.$emit("removeLaboratoryFocalPoint", item.Id);
    this.deleteClicked = false;
  }

  deleteItem(item: WorklistItemUserGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  updateOther(item: WorklistItemUserGridItem): void {
    const laboratoryFocalPoint: WorklistItemUser = this.getWorklistItemUser(
      item.Id
    );
    laboratoryFocalPoint.Other = item.Other;
    // WorklistToBioHubItemModule.UPDATE_OTHER_FIELD_LABORATORY_FOCAL_POINT(
    //   laboratoryFocalPoint
    // );
    this.$emit("updateLaboratoryFocalPoint", laboratoryFocalPoint);
  }

  getWorklistItemUser(id: string): WorklistItemUser {
    const laboratoryFocalPoint: WorklistItemUser | undefined =
      this.laboratoryFocalPoints.find((x) => x.Id == id);
    if (laboratoryFocalPoint === undefined)
      throw {
        message: `Unexpected undefined for WorklistToBioHubItemsFocalPoint with id ${id}`,
      };
    return laboratoryFocalPoint;
  }

  getMailTo(email: string): string {
    return "mailto:" + email;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
