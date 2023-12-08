<template>
  <div>
    <v-card class="mb-5">
      <v-card-title>
        <BackButton v-if="isPublicBmeppCataloguePage" />
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
      </v-card-title>
      <v-card-text>
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="materialGridItems"
          :search="search"
          :custom-filter="customSearch"
          @click:row="selected"
        >
          <template #[`item.warning`]="{ item }">
            <v-icon
              v-if="item.Status != 0 && item.ShipmentMaterialCondition == 0"
              small
              class="mr-2"
              style="color: #c7bc3c"
            >
              mdi-alert
            </v-icon>
            <v-icon
              v-if="item.Status != 0 && item.ShipmentMaterialCondition == 1"
              small
              class="mr-2 red--text"
              >mdi-close-circle</v-icon
            >
            <p v-if="item.Status == 0"></p>
          </template>

          <template #[`item.SharedWithQE`]="{ item }">
            <v-icon v-if="item.SharedWithQE" small class="mr-2 green--text"
              >mdi-check-circle</v-icon
            >
          </template>
          <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
            <v-icon
              v-if="
                isPrivate &&
                (Editable(item.OwnerBioHubFacilityId) ||
                  (item.Status == 2 &&
                    (canVerifyMaterial || canApproveLaboratoryCompletion)) ||
                  (item.Status == 1 &&
                    canApproveBioHubFacilityCompletion &&
                    Editable(item.OwnerBioHubFacilityId))) &&
                (item.ShipmentMaterialCondition != 1 ||
                  CanEditMaterialShipmentInformation)
              "
              small
              class="mr-2"
              @click="editItem(item)"
            >
              mdi-pencil-circle-outline
            </v-icon>
            <v-icon
              v-if="isPrivate && canDelete"
              small
              @click="deleteItem(item)"
            >
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
import { Material } from "@/models/Material";
import { MaterialGridItem } from "@/models/MaterialGridItem";
import { MaterialModule } from "../store";
import { InternationalTaxonomyClassificationModule } from "../../internationalTaxonomyClassifications/store";
import { LaboratoryModule } from "../../laboratories/store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import BackButton from "@/components/BackButton.vue";
import { CountryModule } from "../../countries/store";
import { customTableSearch } from "../../../utils/helper";
import { hasPermission } from "../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { AuthModule } from "../../auth/store";
import { RoleType } from "../../../models/enums/RoleType";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

@Component({
  components: { ConfirmationDialogComponent, BackButton },
})
export default class MaterialsTable extends Vue {
  @Prop({ type: Boolean, default: true })
  readonly isPublicBmeppCataloguePage: boolean;

  @Prop({ type: Boolean, default: false })
  readonly isLaboratoryBmeppPage: boolean;

  @Prop({ type: Boolean, default: true })
  readonly isLaboratoryBmeppCataloguePage: boolean;

  @Prop({ type: Boolean, default: false })
  readonly filterByProviderId: boolean;

  @Prop({ type: String, default: "" })
  readonly providerId: string;

  @Prop({ type: Boolean, default: false })
  readonly bioHubFacilityArea: boolean;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  private deleteClicked = false;
  private editClicked = false;

  private search = "";

  private linkPageSuffix = "";

  private title = "Materials";

  private baseHeaders = [
    {
      text: "",
      align: "start",
      sortable: false,
      value: "warning",
    },
    {
      text: "BMEPP Reference Number",
      align: "start",
      sortable: true,
      value: "ReferenceNumber",
    },
    {
      text: "BMEPP Name",
      align: "start",
      sortable: true,
      value: "Name",
    },
    {
      text: "International Taxonomy classification",
      align: "start",
      sortable: true,
      value: "InternationalTaxonomyClassification",
    },
    {
      text: "Variant",
      align: "start",
      sortable: true,
      value: "Variant",
    },
    {
      text: "Pango Lineage",
      align: "start",
      sortable: true,
      value: "Lineage",
    },
    {
      text: "Provider Laboratory",
      align: "start",
      sortable: true,
      value: "Provider",
    },
    {
      text: "Owner BioHub Facility",
      align: "start",
      sortable: true,
      value: "OwnerBioHubFacility",
    },
    {
      text: "Shared With QE",
      align: "start",
      sortable: true,
      value: "SharedWithQE",
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

  get canApproveBioHubFacilityCompletion(): boolean {
    return hasPermission(PermissionNames.CanApproveBioHubFacilityCompletion);
  }

  get canApproveLaboratoryCompletion(): boolean {
    return hasPermission(PermissionNames.CanApproveLaboratoryCompletion);
  }

  get canVerifyMaterial(): boolean {
    return hasPermission(PermissionNames.CanVerifyMaterial);
  }

  get CanEditMaterialShipmentInformation(): boolean {
    return hasPermission(PermissionNames.CanEditMaterialShipmentInformation);
  }

  get isPrivate(): boolean {
    if (
      this.isPublicBmeppCataloguePage == true ||
      this.isLaboratoryBmeppCataloguePage == true
    ) {
      return false;
    }

    return true;
  }

  get hasActionHeader(): boolean {
    return (this.canEdit || this.canDelete) && this.isPrivate == true;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
  }

  get materialGridItems(): Array<MaterialGridItem> {
    var materials = MaterialModule.Materials;

    if (this.isPublicBmeppCataloguePage) {
      materials = materials.filter((material) => {
        return material.PublicShare == YesNoOption.Yes;
      });
    }

    if (this.isLaboratoryBmeppCataloguePage) {
      materials = materials.filter((material) => {
        return material.PublicShare == YesNoOption.Yes;
      });
    }

    if (
      this.providerId != "" &&
      (this.filterByProviderId == true || this.isLaboratoryBmeppPage)
    ) {
      materials = materials.filter((material) => {
        return (
          material.ProviderLaboratoryId == this.providerId ||
          material.ProviderBioHubFacilityId == this.providerId
        );
      });
    }

    if (!materials) return new Array<MaterialGridItem>();

    return materials.map((m) => {
      var internationalTaxonomyClassificationInfo =
        InternationalTaxonomyClassificationModule.InternationalTaxonomyClassifications.filter(
          (itc) => {
            return itc.Id == m.InternationalTaxonomyClassificationId;
          }
        ).map((m) => {
          return {
            internationalTaxonomyClassification: m.Name,
          };
        });

      if (internationalTaxonomyClassificationInfo.length == 0) {
        internationalTaxonomyClassificationInfo.push({
          internationalTaxonomyClassification: "",
        });
      }

      var laboratoryInfo = LaboratoryModule.Laboratories.filter((l) => {
        return l.Id == m.ProviderLaboratoryId;
      }).map((m) => {
        var laboratoryCountryNameInfo = CountryModule.Countries.filter(
          (country) => {
            return country.Id == m.CountryId;
          }
        ).map((l) => {
          return {
            name: l.Name,
          };
        });

        return {
          laboratory: laboratoryCountryNameInfo[0].name + " - " + m.Name,
        };
      });

      if (laboratoryInfo.length == 0) {
        laboratoryInfo.push({ laboratory: "" });
      }

      var bioHubFacilityInfo = BioHubFacilityModule.BioHubFacilities.filter(
        (l) => {
          return l.Id == m.ProviderBioHubFacilityId;
        }
      ).map((b) => {
        var bioHubFacilityCountryNameInfo = CountryModule.Countries.filter(
          (country) => {
            return country.Id == b.CountryId;
          }
        ).map((l) => {
          return {
            name: l.Name,
          };
        });

        return {
          bioHubFacility:
            bioHubFacilityCountryNameInfo[0].name + " - " + b.Name,
        };
      });

      if (bioHubFacilityInfo.length == 0) {
        bioHubFacilityInfo.push({ bioHubFacility: "" });
      }

      var ownerBioHubFacilityInfo =
        BioHubFacilityModule.BioHubFacilities.filter((l) => {
          return l.Id == m.OwnerBioHubFacilityId;
        }).map((b) => {
          var bioHubFacilityCountryNameInfo = CountryModule.Countries.filter(
            (country) => {
              return country.Id == b.CountryId;
            }
          ).map((l) => {
            return {
              name: l.Name,
            };
          });

          return {
            bioHubFacility:
              bioHubFacilityCountryNameInfo[0].name + " - " + b.Name,
          };
        });

      if (ownerBioHubFacilityInfo.length == 0) {
        ownerBioHubFacilityInfo.push({ bioHubFacility: "" });
      }

      return {
        Id: m.Id,
        ReferenceNumber: m.ReferenceNumber,
        Name: m.Name,
        Lineage: m.Lineage,
        Variant: m.Variant,
        InternationalTaxonomyClassificationId:
          m.InternationalTaxonomyClassificationId,
        InternationalTaxonomyClassification:
          internationalTaxonomyClassificationInfo[0]
            .internationalTaxonomyClassification,
        ProviderLaboratoryId: m.ProviderLaboratoryId,
        ProviderLaboratory: laboratoryInfo[0].laboratory,
        ProviderBioHubFacilityId: m.ProviderBioHubFacilityId,
        ProviderBioHubFacility: bioHubFacilityInfo[0].bioHubFacility,
        Provider:
          laboratoryInfo[0].laboratory == ""
            ? bioHubFacilityInfo[0].bioHubFacility
            : laboratoryInfo[0].laboratory,
        OwnerBioHubFacilityId: m.OwnerBioHubFacilityId,
        OwnerBioHubFacility: ownerBioHubFacilityInfo[0].bioHubFacility,
        Status: m.Status,
        SharedWithQE: m.SharedWithQE,
        ShipmentMaterialCondition: m.ShipmentMaterialCondition,
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  editItem(item: MaterialGridItem): void {
    this.editClicked = true;
    const lab: Material = this.getMaterial(item.Id);
    this.$emit("edit", lab);
  }

  executeDelete(item: MaterialGridItem): void {
    const lab: Material = this.getMaterial(item.Id);
    this.$emit("delete", lab);
    this.deleteClicked = false;
  }

  deleteItem(item: MaterialGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  selected(item: MaterialGridItem): void {
    if (this.deleteClicked == false && this.editClicked == false) {
      const lab: Material = this.getMaterial(item.Id);
      this.$emit("selected", lab);
    }
    this.deleteClicked = false;
    this.editClicked = false;
  }

  getMaterial(id: string): Material {
    const lab: Material | undefined = MaterialModule.Materials.find(
      (x) => x.Id == id
    );
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for Material with id ${id}`,
      };
    return lab;
  }

  create(): void {
    this.$emit("create");
  }

  Editable(ownerBioHubFacilityId: string): boolean {
    if (this.canEdit == true) {
      if (
        AuthModule.RoleType == RoleType.BioHubFacility &&
        AuthModule.BioHubFacilityId != ownerBioHubFacilityId
      ) {
        return false;
      }
    }
    return this.canEdit;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
