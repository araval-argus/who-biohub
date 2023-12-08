<template>
  <div>
    <v-card class="mb-5">
      <v-card-text>
        <v-data-table
          :headers="headers"
          :items="clinicalDetailGridItems"
          :search="search"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
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
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../../components/ConfirmationDialogComponent.vue";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { MaterialClinicalDetailGridItem } from "@/models/MaterialClinicalDetailGridItem";
import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
//import { WorklistToBioHubItemModule } from "../../store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { Gender } from "@/models/enums/Gender";
import { TransportCategoryModule } from "../../../transportCategories/store";

@Component({ components: { ConfirmationDialogComponent } })
export default class ClinicalDetailsTable extends Vue {
  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Provider's Material ID",
      align: "start",
      sortable: true,
      value: "MaterialNumber",
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
    {
      text: "Patient Status",
      align: "start",
      sortable: true,
      value: "PatientStatus",
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

  @Prop({ type: Array, default: [] })
  readonly materialClinicalDetails: Array<MaterialClinicalDetail>;

  get clinicalDetailGridItems(): Array<MaterialClinicalDetailGridItem> {
    if (!this.materialClinicalDetails)
      return new Array<MaterialClinicalDetailGridItem>();

    //let clinicalDetails = new Array<MaterialClinicalDetail>();

    // shippingInformations.forEach((si) => {
    //   si.MaterialClinicalDetails.forEach((cd) => {
    //     clinicalDetails.push(cd);
    //   });
    // });

    return this.materialClinicalDetails.map((l) => {
      const materialProduct = MaterialProductModule.MaterialProducts.filter(
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

      return {
        Id: l.Id,
        MaterialProduct: materialProduct[0].materialProductName,
        MaterialProductId: l.MaterialProductId,
        TransportCategory: transportCategory[0].transportCategoryName,
        TransportCategoryId: l.TransportCategoryId,
        MaterialNumber: l.MaterialNumber,
        CollectionDate: l.CollectionDate
          ? this.getFormatDate(l.CollectionDate)
          : "",
        Location: l.Location,
        IsolationHostType: host[0].hostName,
        IsolationHostTypeId: l.IsolationHostTypeId,
        Gender: gender,
        Age: l.Age,
        PatientStatus: l.PatientStatus,
        MaterialShippingInformationId: l.MaterialShippingInformationId,
        Condition: l.Condition,
        ConditionString: "",
        Note: l.Note,
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
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
