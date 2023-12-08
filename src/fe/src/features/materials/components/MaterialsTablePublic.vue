<template>
  <div>
    <v-card class="mb-5">
      <v-card-title>
        <BackButton />
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
        </v-data-table>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { MaterialPublicGridItem } from "@/models/MaterialPublicGridItem";
import { MaterialModule } from "../store";
import { MaterialPublic } from "@/models/MaterialPublic";
import { InternationalTaxonomyClassificationModule } from "../../internationalTaxonomyClassifications/store";
import { LaboratoryModule } from "../../laboratories/store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import BackButton from "@/components/BackButton.vue";
import { CountryModule } from "../../countries/store";
import { customTableSearch } from "../../../utils/helper";

@Component({
  components: { BackButton },
})
export default class MaterialsTablePublic extends Vue {
  @Prop({ type: String, default: "" })
  readonly providerId: string;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  private search = "";

  private linkPageSuffix = "";

  private title = "Materials";
  private baseHeaders = [
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
  ];

  get headers(): any {
    return this.baseHeaders;
  }

  get materialGridItems(): Array<MaterialPublicGridItem> {
    var materials = MaterialModule.MaterialsPublic;

    if (this.providerId != "") {
      materials = materials.filter((material) => {
        return (
          material.ProviderLaboratoryId == this.providerId ||
          material.ProviderBioHubFacilityId == this.providerId
        );
      });
    }

    if (!materials) return new Array<MaterialPublicGridItem>();

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

      var laboratoryInfo = LaboratoryModule.LaboratoriesPublic.filter((l) => {
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

      var bioHubFacilityInfo =
        BioHubFacilityModule.BioHubFacilitiesPublic.filter((l) => {
          return l.Id == m.ProviderBioHubFacilityId;
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

      if (bioHubFacilityInfo.length == 0) {
        bioHubFacilityInfo.push({ bioHubFacility: "" });
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
      };
    });
  }

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  selected(item: MaterialPublicGridItem): void {
    const lab: MaterialPublic = this.getMaterial(item.Id);
    this.$emit("selected", lab);
  }

  getMaterial(id: string): MaterialPublic {
    const lab: MaterialPublic | undefined = MaterialModule.MaterialsPublic.find(
      (x) => x.Id == id
    );
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for Material with id ${id}`,
      };
    return lab;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
