<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton @back="onBack" />
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="true" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="3" lg="3">
              <text-field
                v-model="model.ReferenceNumber"
                label="WHO BMEPP Reference Number"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="ReferenceNumber"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="7" lg="7">
              <text-field
                v-model="model.Name"
                label="BMEPP Name"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="Name"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="2" lg="2">
              <dropdown
                v-model="model.TypeId"
                :items="materialTypesItems"
                item-text="Text"
                item-value="Value"
                label="BMEPP Type"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="TypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>
          <v-row>
            <v-col v-if="IsProviderBioHubFacility" cols="12" md="12" lg="12">
              <text-field
                v-model="CountryProviderString"
                label="Provider BioHub Facility"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="ProviderBioHubFacilityId"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col v-else cols="12" md="12" lg="12">
              <text-field
                v-model="CountryProviderString"
                label="Provider Laboratory"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="ProviderLaboratoryId"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.OriginalProductTypeId"
                :items="materialProductsItems"
                item-text="Text"
                item-value="Value"
                label="Original Product Type Id"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="OriginalProductTypeId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.ProductTypeId"
                :items="materialProductsItems"
                item-text="Text"
                item-value="Value"
                label="Product Type"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="ProductTypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="TemperatureString"
                label="Temperature in Storage"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="UnitOfMeasureId"
                @input="update"
              ></text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.UsagePermissionId"
                :items="materialUsagePermissionsItems"
                item-text="Text"
                item-value="Value"
                label="Usage Permission"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="UsagePermissionId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.Lineage"
                label="Lineage"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="Lineage"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.Variant"
                label="Variant"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="Variant"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.VariantAssessment"
                label="WHO Variant Assessment"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="VariantAssessment"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.GeneticSequenceDataId"
                :items="geneticSequenceDatasItems"
                item-text="Text"
                item-value="Value"
                :readonly="true"
                label="External Registered Database for Genetic Sequence Data (GSD)"
                :properties-errors="propertiesErrors"
                property-name="GeneticSequenceDataId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <date-picker
                v-model="model.BioHubFacilityDeliveryDate"
                label="Date of BMEPP Receipt at WHO BioHub Facility"
                readonly
                property-name="BioHubFacilityDeliveryDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="7" lg="7">
              <dropdown
                v-model="model.InternationalTaxonomyClassificationId"
                :items="internationalTaxonomyClassificationsItems"
                item-text="Text"
                item-value="Value"
                :readonly="true"
                label="International Taxonomy Classification"
                :properties-errors="propertiesErrors"
                property-name="InternationalTaxonomyClassificationId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="3" lg="3">
              <checkbox
                v-model="model.GMO"
                label="Genetically modified organism (GMO)"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="GMO"
                @change="update"
              >
              </checkbox>
            </v-col>
            <v-col cols="12" md="2" lg="2">
              <dropdown
                v-model="model.IsolationHostTypeId"
                :items="isolationHostTypesItems"
                item-text="Text"
                item-value="Value"
                label="Isolation Host Type"
                :readonly="true"
                :properties-errors="propertiesErrors"
                property-name="IsolationHostTypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>
        </div>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import BackButton from "@/components/BackButton.vue";
import { MaterialPublic } from "@/models/MaterialPublic";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import Dropdown from "@/components/Dropdown.vue";
import Checkbox from "@/components/Checkbox.vue";
import { MaterialModule } from "../store";
import { LaboratoryModule } from "../../laboratories/store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import { CountryModule } from "../../countries/store";
import { DropdownItem } from "@/models/DropdownItem";
import { MaterialTypeModule } from "../../materialTypes/store";
import { MaterialProductModule } from "../../materialProducts/store";
import { TemperatureUnitOfMeasureModule } from "../../temperatureUnitOfMeasures/store";
import { MaterialUsagePermissionModule } from "../../materialUsagePermissions/store";
import { GeneticSequenceDataModule } from "../../geneticSequenceDatas/store";
import { InternationalTaxonomyClassificationModule } from "../../internationalTaxonomyClassifications/store";
import { IsolationHostTypeModule } from "../../isolationHostTypes/store";
import { MaterialProviderLaboratoryDropDown } from "@/models/MaterialProviderLaboratoryDropDown";
import { AppModule } from "../../../store/MainStore";
import DatePicker from "@/components/DatePicker.vue";

@Component({
  components: {
    BackButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    Checkbox,
    DatePicker,
  },
})
export default class MaterialFormPublic extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  $refs!: {
    form: any;
  };

  // Props
  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "" })
  readonly providerId: string;

  @Prop({ required: true, type: String, default: "Material" })
  readonly title: string;

  // Model
  @Model("update", { type: Object }) model!: MaterialPublic;

  validate() {
    this.$refs.form.validate();
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  get IsProviderBioHubFacility(): boolean {
    return (
      this.model.ProviderBioHubFacilityId != undefined &&
      this.model.ProviderBioHubFacilityId != null &&
      this.model.ProviderBioHubFacilityId != ""
    );
  }

  get TemperatureString(): string {
    if (this.model.Temperature == null || this.model.Temperature == undefined) {
      return "";
    }
    var unitOfMeasureInfo =
      TemperatureUnitOfMeasureModule.TemperatureUnitOfMeasures.filter(
        (tuom) => {
          return tuom.Id == this.model.UnitOfMeasureId;
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

    return this.model.Temperature + unitOfMeasureInfo[0].unitOfMeasureName;
  }

  get CountryProviderString(): string {
    if (this.IsProviderBioHubFacility == true) {
      var bioHubFacilityInfo =
        BioHubFacilityModule.BioHubFacilitiesPublic.filter((b) => {
          return b.Id == this.model.ProviderBioHubFacilityId;
        }).map((b) => {
          return {
            countryId: b.CountryId,
            name: b.Name,
          };
        });

      if (bioHubFacilityInfo.length == 0) {
        return "";
      }

      var countryInfoBioHubFacility = CountryModule.Countries.filter((c) => {
        return c.Id == bioHubFacilityInfo[0].countryId;
      }).map((c) => {
        return {
          countryName: c.Name,
        };
      });

      return (
        countryInfoBioHubFacility[0].countryName +
        " - " +
        bioHubFacilityInfo[0].name
      );
    } else {
      var laboratoryInfo = LaboratoryModule.LaboratoriesPublic.filter((l) => {
        return l.Id == this.model.ProviderLaboratoryId;
      }).map((l) => {
        return {
          countryId: l.CountryId,
          name: l.Name,
        };
      });

      if (laboratoryInfo.length == 0) {
        return "";
      }

      var countryInfoLaboratory = CountryModule.Countries.filter((c) => {
        return c.Id == laboratoryInfo[0].countryId;
      }).map((c) => {
        return {
          countryName: c.Name,
        };
      });

      return (
        countryInfoLaboratory[0].countryName + " - " + laboratoryInfo[0].name
      );
    }
  }

  onBack() {
    MaterialModule.SET_ERROR(undefined);
  }

  async mounted() {
    try {
      await CountryModule.ListCountriesPublic();
      await MaterialTypeModule.ListMaterialTypesPublic();
      await MaterialProductModule.ListMaterialProductsPublic();
      await TemperatureUnitOfMeasureModule.ListTemperatureUnitOfMeasuresPublic();
      await MaterialUsagePermissionModule.ListMaterialUsagePermissionsPublic();
      await GeneticSequenceDataModule.ListGeneticSequenceDatasPublic();
      await InternationalTaxonomyClassificationModule.ListInternationalTaxonomyClassificationsPublic();
      await IsolationHostTypeModule.ListIsolationHostTypesPublic();
      await LaboratoryModule.ListLaboratoriesPublic();
      await BioHubFacilityModule.ListBioHubFacilitiesPublic();
    } finally {
      AppModule.HideLoading();
    }
  }

  get countriesItems(): Array<DropdownItem> {
    const Countries = CountryModule.Countries;
    if (!Countries) return new Array<DropdownItem>();

    return Countries.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get materialTypesItems(): Array<DropdownItem> {
    const MaterialTypes = MaterialTypeModule.MaterialTypes;
    if (!MaterialTypes) return new Array<DropdownItem>();

    return MaterialTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get materialProductsItems(): Array<DropdownItem> {
    const MaterialProducts = MaterialProductModule.MaterialProducts;
    if (!MaterialProducts) return new Array<DropdownItem>();

    return MaterialProducts.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get materialUsagePermissionsItems(): Array<DropdownItem> {
    const MaterialUsagePermissions =
      MaterialUsagePermissionModule.MaterialUsagePermissions;
    if (!MaterialUsagePermissions) return new Array<DropdownItem>();

    return MaterialUsagePermissions.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get geneticSequenceDatasItems(): Array<DropdownItem> {
    const GeneticSequenceDatas = GeneticSequenceDataModule.GeneticSequenceDatas;
    if (!GeneticSequenceDatas) return new Array<DropdownItem>();

    return GeneticSequenceDatas.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get internationalTaxonomyClassificationsItems(): Array<DropdownItem> {
    const InternationalTaxonomyClassifications =
      InternationalTaxonomyClassificationModule.InternationalTaxonomyClassifications;
    if (!InternationalTaxonomyClassifications) return new Array<DropdownItem>();

    return InternationalTaxonomyClassifications.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get isolationHostTypesItems(): Array<DropdownItem> {
    const IsolationHostTypes = IsolationHostTypeModule.IsolationHostTypes;
    if (!IsolationHostTypes) return new Array<DropdownItem>();

    return IsolationHostTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get providersItems(): Array<MaterialProviderLaboratoryDropDown> {
    const Laboratories = LaboratoryModule.LaboratoriesPublic;
    const BioHubFacilities = BioHubFacilityModule.BioHubFacilitiesPublic;

    var providersItems = new Array<MaterialProviderLaboratoryDropDown>();

    Laboratories.forEach((laboratory) => {
      var laboratoryCountryNameInfo = CountryModule.Countries.filter(
        (country) => {
          return country.Id == laboratory.CountryId;
        }
      ).map((l) => {
        return {
          name: l.Name,
        };
      });

      if (laboratoryCountryNameInfo.length == 0) {
        laboratoryCountryNameInfo.push({
          name: "",
        });
      }

      providersItems.push({
        Id: laboratory.Id,
        Name: laboratoryCountryNameInfo[0].name + " - " + laboratory.Name,
        Type: "Laboratory",
      });
    });

    BioHubFacilities.forEach((biohubfacility) => {
      var bioHubFacilityCountryNameInfo = CountryModule.Countries.filter(
        (country) => {
          return country.Id == biohubfacility.CountryId;
        }
      ).map((l) => {
        return {
          name: l.Name,
        };
      });

      if (bioHubFacilityCountryNameInfo.length == 0) {
        bioHubFacilityCountryNameInfo.push({
          name: "",
        });
      }

      providersItems.push({
        Id: biohubfacility.Id,
        Name:
          bioHubFacilityCountryNameInfo[0].name + " - " + biohubfacility.Name,
        Type: "BioHubFacility",
      });
    });

    return providersItems;
  }

  get propertiesErrors(): any {
    return MaterialModule.ErrorMessage;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
