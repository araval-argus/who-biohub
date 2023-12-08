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
          v-if="item.Status != 0"
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
import { MaterialClinicalDetailGridItem } from "@/models/MaterialClinicalDetailGridItem";
import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import { WorklistToBioHubItemModule } from "../../store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { Gender } from "@/models/enums/Gender";

import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { WorklistToBioHubItemMaterialGridItem } from "@/models/WorklistToBioHubItemMaterialGridItem";
import { WorklistToBioHubItemMaterial } from "@/models/WorklistToBioHubItemMaterial";
import { MaterialStatus } from "@/models/enums/MaterialStatus";

import { hasPermission } from "../../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { TransportCategoryModule } from "../../../transportCategories/store";

@Component({ components: { ConfirmationDialogComponent } })
export default class ShipmentCompletedMaterialsTable extends Vue {
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
      text: "Status",
      align: "start",
      sortable: true,
      value: "StatusDescription",
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

  get materialGridItems(): Array<WorklistToBioHubItemMaterialGridItem> {
    const materials = WorklistToBioHubItemModule.WorklistToBioHubItemMaterials;
    if (!materials) return new Array<WorklistToBioHubItemMaterialGridItem>();

    return materials.map((l) => {
      var materialProduct = MaterialProductModule.MaterialProducts.filter(
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

      var statusDescription = "Completed";

      if (l.Status === MaterialStatus.WaitingForBioHubFacilityCompletion) {
        statusDescription = "Waiting For BioHub Facility Completion";
      } else if (l.Status === MaterialStatus.WaitingForLaboratoryCompletion) {
        statusDescription = "Waiting For Laboratory Completion";
      }

      if (l.Gender === Gender.Male) {
        gender = "Male";
      } else if (l.Gender === Gender.Female) {
        gender = "Female";
      } else {
        gender = "Undisclosed";
      }

      return {
        Id: l.Id,
        MaterialProduct: materialProduct[0].materialProductName,
        MaterialProductId: l.MaterialProductId,
        TransportCategory: transportCategory[0].transportCategoryName,
        TransportCategoryId: l.TransportCategoryId,
        MaterialNumber: l.MaterialNumber,
        MaterialId: l.MaterialId,
        CollectionDate: l.CollectionDate
          ? this.getFormatDate(l.CollectionDate)
          : "",
        Location: l.Location,
        IsolationHostType: host[0].hostName,
        IsolationHostTypeId: l.IsolationHostTypeId,
        Gender: gender,
        Age: l.Age,
        Status: l.Status,
        WorklistToBioHubItemId: l.WorklistToBioHubItemId,
        StatusDescription: statusDescription,

        //PatientStatus: l.PatientStatus,
        //MaterialShippingInformationId: l.MaterialShippingInformationId,
        // Condition: l.Condition,
        // ConditionString: condition,
        // Note: l.Note,
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

  get routingNamePrefix(): string {
    var routeName = this.$route.name;
    if (routeName != null && routeName != undefined) {
      if (routeName.startsWith("who")) {
        return "whoarea-";
      } else if (routeName.startsWith("laboratory")) {
        return "laboratoryarea-";
      } else {
        return "biohubfacilityarea-";
      }
    } else {
      return "";
    }
  }

  selected(item: WorklistToBioHubItemMaterialGridItem): void {
    let destinationStatusRoute = "";

    if (hasPermission(PermissionNames.CanEditMaterial)) {
      destinationStatusRoute = "material-edit";
    } else {
      destinationStatusRoute = "material-details-bmepp";
    }

    if (item.Status == MaterialStatus.WaitingForBioHubFacilityCompletion) {
      destinationStatusRoute = "material-edit-biohubcompletion";

      if (hasPermission(PermissionNames.CanApproveBioHubFacilityCompletion)) {
        destinationStatusRoute = "material-edit-biohubcompletion";
      } else {
        destinationStatusRoute = "material-details-biohubcompletion";
      }
    } else if (item.Status == MaterialStatus.WaitingForLaboratoryCompletion) {
      if (
        hasPermission(PermissionNames.CanApproveLaboratoryCompletion) ||
        hasPermission(PermissionNames.CanVerifyMaterial)
      ) {
        destinationStatusRoute = "material-edit-laboratorycompletion";
      } else {
        destinationStatusRoute = "material-details-laboratorycompletion";
      }
    }

    var customParams = { id: item.MaterialId };

    this.$router.push({
      name: this.routingNamePrefix + destinationStatusRoute,
      params: customParams,
    });
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
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
