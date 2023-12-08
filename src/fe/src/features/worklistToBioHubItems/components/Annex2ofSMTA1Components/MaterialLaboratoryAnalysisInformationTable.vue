<template>
  <div>
    <v-card class="mb-5">
      <v-card-text>
        <v-data-table
          :headers="headers"
          :items="laboratoryAnalysisInformationGridItems"
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
import { MaterialLaboratoryAnalysisInformationGridItem } from "@/models/MaterialLaboratoryAnalysisInformationGridItem";
import { MaterialLaboratoryAnalysisInformation } from "@/models/MaterialLaboratoryAnalysisInformation";
//import { WorklistToBioHubItemModule } from "../../store";
import { GeneticSequenceDataModule } from "../../../geneticSequenceDatas/store";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { TemperatureUnitOfMeasureModule } from "../../../temperatureUnitOfMeasures/store";
import { SpecimenTypeModule } from "../../../specimenTypes/store";

@Component({ components: { ConfirmationDialogComponent } })
export default class MaterialLaboratoryAnalysisInformationTable extends Vue {
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
      text: "Freezing Date",
      align: "start",
      sortable: true,
      value: "FreezingDate",
    },
    {
      text: "Storage Condition",
      align: "start",
      sortable: true,
      value: "Temperature",
    },
    {
      text: "Virus Concentration (Ct-Value)",
      align: "start",
      sortable: true,
      value: "VirusConcentration",
    },
    {
      text: "Cell-line Used for Culturing",
      align: "start",
      sortable: true,
      value: "CulturingCellLine",
    },
    {
      text: "# of Passage Used for Culturing ",
      align: "start",
      sortable: true,
      value: "CulturingPassagesNumber",
    },
    {
      text: "Specimen Collected from Patient",
      align: "start",
      sortable: true,
      value: "CollectedSpecimenTypes",
    },
    {
      text: "Type of Transport Medium",
      align: "start",
      sortable: true,
      value: "TypeOfTransportMedium",
    },
    {
      text: "Brand of Transport Medium",
      align: "start",
      sortable: true,
      value: "BrandOfTransportMedium",
    },
    {
      text: "GSD Uploaded to Accessible Database",
      align: "start",
      sortable: true,
      value: "GSDUploadedToDatabaseString",
    },

    {
      text: "Database Used for GSD Uploading",
      align: "start",
      sortable: true,
      value: "DatabaseUsedForGSDUploading",
    },

    {
      text: "Accession Number in GSD Database",
      align: "start",
      sortable: true,
      value: "AccessionNumberInGSDDatabase",
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

  @Prop({ type: Array, default: [] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Array, default: [] })
  readonly materialLaboratoryAnalysisInformations: Array<MaterialLaboratoryAnalysisInformation>;

  get laboratoryAnalysisInformationGridItems(): Array<MaterialLaboratoryAnalysisInformationGridItem> {
    if (!this.materialLaboratoryAnalysisInformations)
      return new Array<MaterialLaboratoryAnalysisInformationGridItem>();

    // let laboratoryAnalysisInformations =
    //   new Array<MaterialLaboratoryAnalysisInformation>();

    // shippingInformations.forEach((si) => {
    //   si.MaterialLaboratoryAnalysisInformation.forEach((cd) => {
    //     laboratoryAnalysisInformations.push(cd);
    //   });
    // });

    return this.materialLaboratoryAnalysisInformations.map((l) => {
      const geneticSequenceData =
        GeneticSequenceDataModule.GeneticSequenceDatas.filter((mp) => {
          return l.DatabaseUsedForGSDUploadingId == mp.Id;
        }).map((m) => {
          return {
            databaseUsedForGSDUploadingName: m.Name,
          };
        });

      if (geneticSequenceData.length == 0) {
        geneticSequenceData.push({
          databaseUsedForGSDUploadingName: "",
        });
      }

      const unitOfMeasureInfo =
        TemperatureUnitOfMeasureModule.TemperatureUnitOfMeasures.filter(
          (tuom) => {
            return tuom.Id == l.UnitOfMeasureId;
          }
        ).map((m) => {
          return {
            unitOfMeasureName: m.Unit,
          };
        });

      if (unitOfMeasureInfo.length == 0) {
        unitOfMeasureInfo.push({
          unitOfMeasureName: "",
        });
      }

      let GSDUploadedToDatabaseString = "";

      if (l.GSDUploadedToDatabase === YesNoOption.Yes) {
        GSDUploadedToDatabaseString = "Yes";
      } else if (l.GSDUploadedToDatabase === YesNoOption.No) {
        GSDUploadedToDatabaseString = "No";
      } else {
        GSDUploadedToDatabaseString = "Other";
      }

      return {
        Id: l.Id,
        DatabaseUsedForGSDUploading:
          geneticSequenceData[0].databaseUsedForGSDUploadingName,
        DatabaseUsedForGSDUploadingId: l.DatabaseUsedForGSDUploadingId,
        MaterialNumber: l.MaterialNumber,
        FreezingDate: l.FreezingDate ? this.getFormatDate(l.FreezingDate) : "",
        Temperature: l.Temperature + unitOfMeasureInfo[0].unitOfMeasureName,
        GSDUploadedToDatabaseString: GSDUploadedToDatabaseString,
        GSDUploadedToDatabase: l.GSDUploadedToDatabase,
        VirusConcentration: l.VirusConcentration,
        CulturingCellLine: l.CulturingCellLine,
        CulturingPassagesNumber: l.CulturingPassagesNumber,
        MaterialShippingInformationId: l.MaterialShippingInformationId,
        CollectedSpecimenTypes: this.getCollectedSpecimenTypesString(
          l.CollectedSpecimenTypes
        ),
        TypeOfTransportMedium: l.TypeOfTransportMedium,
        BrandOfTransportMedium: l.BrandOfTransportMedium,
        AccessionNumberInGSDDatabase: l.AccessionNumberInGSDDatabase,
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

  getCollectedSpecimenTypesString(
    CollectedSpecimenTypes: Array<string>
  ): string {
    let collectedSpecimenTypesString = "";

    CollectedSpecimenTypes.forEach((cst) => {
      const specimenTypeInfo = SpecimenTypeModule.SpecimenTypes.filter((st) => {
        return st.Id == cst;
      }).map((m) => {
        return {
          specimenTypeName: m.Name,
        };
      });

      if (specimenTypeInfo.length == 0) {
        specimenTypeInfo.push({
          specimenTypeName: "",
        });
      }

      collectedSpecimenTypesString =
        collectedSpecimenTypesString +
        specimenTypeInfo[0].specimenTypeName +
        ",";
    });

    if (collectedSpecimenTypesString.includes(",")) {
      collectedSpecimenTypesString = collectedSpecimenTypesString.slice(0, -1);
    }

    return collectedSpecimenTypesString;
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
