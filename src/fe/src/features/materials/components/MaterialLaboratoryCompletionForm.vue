<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton @back="onBack" />
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.ReferenceNumber"
                label="WHO Reference Number"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="ReferenceNumber"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.ReferenceNumberValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="ReferenceNumberValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.ReferenceNumberValidation != 0"
                v-model="model.ReferenceNumberComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="ReferenceNumberComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Name"
                label="BMEPP Name"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="Name"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.NameValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="NameValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.NameValidation != 0"
                v-model="model.NameComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="NameComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.TypeId"
                :items="materialTypesItems"
                item-text="Text"
                item-value="Value"
                label="Pathogen Type"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="TypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.TypeValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="TypeValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.TypeValidation != 0"
                v-model="model.TypeComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="TypeComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Pathogen"
                label="Pathogen"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="Pathogen"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.PathogenValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="PathogenValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.PathogenValidation != 0"
                v-model="model.PathogenComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="PathogenComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Variant"
                label="Variant"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="Variant"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.VariantValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="VariantValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.VariantValidation != 0"
                v-model="model.VariantComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="VariantComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.VariantAssessment"
                label="Variant Assessment"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="VariantAssessment"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.VariantAssessmentValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="VariantAssessmentValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.VariantAssessmentValidation != 0"
                v-model="model.VariantAssessmentComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="VariantAssessmentComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.InternationalTaxonomyClassificationId"
                :items="internationalTaxonomyClassificationsItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                label="International Taxonomy Classification"
                :properties-errors="propertiesErrors"
                property-name="InternationalTaxonomyClassificationId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.InternationalTaxonomyClassificationValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="InternationalTaxonomyClassificationValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="
                  model.InternationalTaxonomyClassificationValidation != 0
                "
                v-model="model.InternationalTaxonomyClassificationComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="InternationalTaxonomyClassificationComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <checkbox
                v-model="model.GMO"
                label="Genetically Modified Organism (GMO)"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="GMO"
                @change="update"
              >
              </checkbox>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.GMOValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="GMOValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.GMOValidation != 0"
                v-model="model.GMOComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="GMOComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Lineage"
                label="Lineage (Pango)"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="Lineage"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.LineageValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="LineageValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.LineageValidation != 0"
                v-model="model.LineageComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="LineageComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.SuspectedEpidemiologicalOriginId"
                :menu-props="{ auto: true }"
                :items="countriesItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                label="Suspected Epidemiological Original (Country)"
                :properties-errors="propertiesErrors"
                property-name="SuspectedEpidemiologicalOriginId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.SuspectedEpidemiologicalOriginValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="SuspectedEpidemiologicalOriginValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.SuspectedEpidemiologicalOriginValidation != 0"
                v-model="model.SuspectedEpidemiologicalOriginComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="SuspectedEpidemiologicalOriginComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.UsagePermissionId"
                :items="materialUsagePermissionsItems"
                item-text="Text"
                item-value="Value"
                label="Usage Permission"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="UsagePermissionId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.UsagePermissionValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="UsagePermissionValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.UsagePermissionValidation != 0"
                v-model="model.UsagePermissionComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="UsagePermissionComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <div v-if="readonly || !CanEditMaterialOwnerBioHubFacility">
            <v-row v-if="OwnerBioHubFacilityString != ''">
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="OwnerBioHubFacilityString"
                  label="Owner BioHub Facility"
                  readonly
                  :properties-errors="propertiesErrors"
                  property-name="OwnerBioHubFacilityId"
                >
                </text-field>
              </v-col>
            </v-row>
          </div>
          <div v-else>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.OwnerBioHubFacilityId"
                  :menu-props="{ auto: true }"
                  :items="bioHubFacilitiesItems"
                  item-text="Text"
                  item-value="Value"
                  label="BioHub Facility Owner"
                  :readonly="readOnly"
                  :prepend-icon="prependIconField"
                  :properties-errors="propertiesErrors"
                  property-name="OwnerBioHubFacilityId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>
          </div>
          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.OwnerBioHubFacilityValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="OwnerBioHubFacilityValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.OwnerBioHubFacilityValidation != 0"
                v-model="model.OwnerBioHubFacilityComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="OwnerBioHubFacilityComment"
                @input="update"
              >
              </text-area>
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
            <v-col cols="12" md="12" lg="12">
              <date-picker
                v-model="model.DateOfBMEPPReceipt"
                label="Arrival Date at BioHub Facility"
                :readonly="readonly || !CanEditMaterialShipmentInformation"
                property-name="DateOfBMEPPReceipt"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.DateOfBMEPPReceiptValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="DateOfBMEPPReceiptValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.DateOfBMEPPReceiptValidation != 0"
                v-model="model.DateOfBMEPPReceiptComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="DateOfBMEPPReceiptComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.ProductTypeId"
                :items="materialProductsItems"
                item-text="Text"
                item-value="Value"
                label="Product Type"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="ProductTypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.ProductTypeValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="ProductTypeValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.ProductTypeValidation != 0"
                v-model="model.ProductTypeComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="ProductTypeComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field-float
                v-model="model.Temperature"
                label="Temperature in Storage"
                :step="0.01"
                :precision="2"
                :buttons="false"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                decimal
                :properties-errors="propertiesErrors"
                property-name="Temperature"
                @input="update"
              >
              </text-field-float>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.TemperatureValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="TemperatureValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.TemperatureValidation != 0"
                v-model="model.TemperatureComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="TemperatureComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.UnitOfMeasureId"
                :items="temperatureUnitOfMeasuresItems"
                item-text="Text"
                item-value="Value"
                label="Unit (Temperature)"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="UnitOfMeasureId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.UnitOfMeasureValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="UnitOfMeasureValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.UnitOfMeasureValidation != 0"
                v-model="model.UnitOfMeasureComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="UnitOfMeasureComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <div>
            <h2>Information about Shipment from Provider to BioHub Facility</h2>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.OriginalProductTypeId"
                  :items="materialProductsItems"
                  item-text="Text"
                  item-value="Value"
                  label="Material Type in Shipment to BioHub Facility"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="OriginalProductTypeId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.OriginalProductTypeValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="OriginalProductTypeValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.OriginalProductTypeValidation != 0"
                  v-model="model.OriginalProductTypeComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="OriginalProductTypeComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.TransportCategoryId"
                  :items="transportCategoriesItems"
                  item-text="Text"
                  item-value="Value"
                  label="Material Category for Transport to BioHub Facility"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="TransportCategoryId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.TransportCategoryValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="TransportCategoryValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.TransportCategoryValidation != 0"
                  v-model="model.TransportCategoryComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="TransportCategoryComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field-float
                  v-model="model.ShipmentNumberOfVials"
                  label="Number of Vials Shipped to BioHub Facility"
                  :buttons="false"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="ShipmentNumberOfVials"
                  :properties-errors="propertiesErrors"
                >
                </text-field-float>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.ShipmentNumberOfVialsValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentNumberOfVialsValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.ShipmentNumberOfVialsValidation != 0"
                  v-model="model.ShipmentNumberOfVialsComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentNumberOfVialsComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field-float
                  v-model="model.ShipmentAmount"
                  label="Shipment Amount per Vial"
                  :step="0.01"
                  :precision="2"
                  :buttons="false"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  decimal
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentAmount"
                  @input="update"
                >
                </text-field-float>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.ShipmentAmountValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentAmountValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.ShipmentAmountValidation != 0"
                  v-model="model.ShipmentAmountComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentAmountComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <h2>Information about Laboratory Analysis from Provider</h2>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <date-picker
                  v-model="model.FreezingDate"
                  label="Sample Freezing Date"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="FreezingDate"
                  :properties-errors="propertiesErrors"
                  @input="update"
                >
                </date-picker>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.FreezingDateValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="FreezingDateValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.FreezingDateValidation != 0"
                  v-model="model.FreezingDateComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="FreezingDateComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field-float
                  v-model="model.ShipmentTemperature"
                  label="Storage Condition Temperature"
                  :step="0.01"
                  :precision="2"
                  :buttons="false"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  decimal
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentTemperature"
                  @input="update"
                >
                </text-field-float>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.ShipmentTemperatureValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentTemperatureValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.ShipmentTemperatureValidation != 0"
                  v-model="model.ShipmentTemperatureComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentTemperatureComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.ShipmentUnitOfMeasureId"
                  :items="temperatureUnitOfMeasuresItems"
                  item-text="Text"
                  item-value="Value"
                  label="Unit (Temperature)"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentUnitOfMeasureId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.ShipmentUnitOfMeasureValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentUnitOfMeasureValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.ShipmentUnitOfMeasureValidation != 0"
                  v-model="model.ShipmentUnitOfMeasureComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentUnitOfMeasureComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="model.VirusConcentration"
                  label="Virus Concentration"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="VirusConcentration"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.VirusConcentrationValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="VirusConcentrationValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.VirusConcentrationValidation != 0"
                  v-model="model.VirusConcentrationComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="VirusConcentrationComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="model.SampleId"
                  label="Provider Material's ID"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="SampleId"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.SampleIdValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="SampleIdValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.SampleIdValidation != 0"
                  v-model="model.SampleIdComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="SampleIdComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <div v-if="cultureIsolate">
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <text-field
                    v-model="model.CulturingCellLine"
                    label="Culturing Cell Line"
                    :prepend-icon="prependIconShipmentMaterialField"
                    :readonly="readonly || !CanEditMaterialShipmentInformation"
                    property-name="CulturingCellLine"
                    :properties-errors="allPropertiesErrors"
                    @input="update"
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row class="mb-15">
                <v-col cols="12" md="6" lg="6">
                  <MaterialValidationSelectionComponent
                    v-model="model.CulturingCellLineValidation"
                    :readonly="!canVerifyMaterial"
                    :prepend-icon="prependIconValidation"
                    label="Validation"
                    :properties-errors="propertiesErrors"
                    property-name="CulturingCellLineValidation"
                    @input="update"
                  >
                  </MaterialValidationSelectionComponent>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-area
                    v-show="model.CulturingCellLineValidation != 0"
                    v-model="model.CulturingCellLineComment"
                    :prepend-icon="prependIconValidation"
                    label="Comment"
                    :readonly="!canVerifyMaterial"
                    :properties-errors="propertiesErrors"
                    property-name="CulturingCellLineComment"
                    @input="update"
                  >
                  </text-area>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <text-field-float
                    v-model="model.CulturingPassagesNumber"
                    label="Culturing Passage Number"
                    :buttons="false"
                    :prepend-icon="prependIconShipmentMaterialField"
                    :readonly="readonly || !CanEditMaterialShipmentInformation"
                    property-name="CulturingPassagesNumber"
                    :properties-errors="allPropertiesErrors"
                    @input="update"
                  >
                  </text-field-float>
                </v-col>
              </v-row>
              <v-row class="mb-15">
                <v-col cols="12" md="6" lg="6">
                  <MaterialValidationSelectionComponent
                    v-model="model.CulturingPassagesNumberValidation"
                    :readonly="!canVerifyMaterial"
                    :prepend-icon="prependIconValidation"
                    label="Validation"
                    :properties-errors="propertiesErrors"
                    property-name="CulturingPassagesNumberValidation"
                    @input="update"
                  >
                  </MaterialValidationSelectionComponent>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-area
                    v-show="model.CulturingPassagesNumberValidation != 0"
                    v-model="model.CulturingPassagesNumberComment"
                    :prepend-icon="prependIconValidation"
                    label="Comment"
                    :readonly="!canVerifyMaterial"
                    :properties-errors="propertiesErrors"
                    property-name="CulturingPassagesNumberComment"
                    @input="update"
                  >
                  </text-area>
                </v-col>
              </v-row>
            </div>

            <div v-if="clinicalSpecimen">
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <dropdown
                    v-model="CollectedSpecimenType"
                    :items="SpecimenTypes"
                    item-text="Text"
                    item-value="Value"
                    label="Collected Specimen Type"
                    :prepend-icon="prependIconShipmentMaterialField"
                    :readonly="readonly || !CanEditMaterialShipmentInformation"
                    property-name="CollectedSpecimenTypes"
                    :properties-errors="allPropertiesErrors"
                    @change="updateCollectedSpecimenTypes"
                  ></dropdown>
                </v-col>
              </v-row>
              <v-row class="mb-15">
                <v-col cols="12" md="6" lg="6">
                  <MaterialValidationSelectionComponent
                    v-model="model.MaterialCollectedSpecimenTypesValidation"
                    :readonly="!canVerifyMaterial"
                    :prepend-icon="prependIconValidation"
                    label="Validation"
                    :properties-errors="propertiesErrors"
                    property-name="MaterialCollectedSpecimenTypesValidation"
                    @input="update"
                  >
                  </MaterialValidationSelectionComponent>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-area
                    v-show="model.MaterialCollectedSpecimenTypesValidation != 0"
                    v-model="model.MaterialCollectedSpecimenTypesComment"
                    :prepend-icon="prependIconValidation"
                    label="Comment"
                    :readonly="!canVerifyMaterial"
                    :properties-errors="propertiesErrors"
                    property-name="MaterialCollectedSpecimenTypesComment"
                    @input="update"
                  >
                  </text-area>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <text-field
                    v-model="model.TypeOfTransportMedium"
                    label="Type Of Transport Medium"
                    :prepend-icon="prependIconShipmentMaterialField"
                    :readonly="readonly || !CanEditMaterialShipmentInformation"
                    property-name="TypeOfTransportMedium"
                    :properties-errors="allPropertiesErrors"
                    @input="update"
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row class="mb-15">
                <v-col cols="12" md="6" lg="6">
                  <MaterialValidationSelectionComponent
                    v-model="model.TypeOfTransportMediumValidation"
                    :readonly="!canVerifyMaterial"
                    :prepend-icon="prependIconValidation"
                    label="Validation"
                    :properties-errors="propertiesErrors"
                    property-name="TypeOfTransportMediumValidation"
                    @input="update"
                  >
                  </MaterialValidationSelectionComponent>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-area
                    v-show="model.TypeOfTransportMediumValidation != 0"
                    v-model="model.TypeOfTransportMediumComment"
                    :prepend-icon="prependIconValidation"
                    label="Comment"
                    :readonly="!canVerifyMaterial"
                    :properties-errors="propertiesErrors"
                    property-name="TypeOfTransportMediumComment"
                    @input="update"
                  >
                  </text-area>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <text-field
                    v-model="model.BrandOfTransportMedium"
                    label="Brand Of Transport Medium"
                    :prepend-icon="prependIconShipmentMaterialField"
                    :readonly="readonly || !CanEditMaterialShipmentInformation"
                    property-name="BrandOfTransportMedium"
                    :properties-errors="allPropertiesErrors"
                    @input="update"
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row class="mb-15">
                <v-col cols="12" md="6" lg="6">
                  <MaterialValidationSelectionComponent
                    v-model="model.BrandOfTransportMediumValidation"
                    :readonly="!canVerifyMaterial"
                    :prepend-icon="prependIconValidation"
                    label="Validation"
                    :properties-errors="propertiesErrors"
                    property-name="BrandOfTransportMediumValidation"
                    @input="update"
                  >
                  </MaterialValidationSelectionComponent>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-area
                    v-show="model.BrandOfTransportMediumValidation != 0"
                    v-model="model.BrandOfTransportMediumComment"
                    :prepend-icon="prependIconValidation"
                    label="Comment"
                    :readonly="!canVerifyMaterial"
                    :properties-errors="propertiesErrors"
                    property-name="BrandOfTransportMediumComment"
                    @input="update"
                  >
                  </text-area>
                </v-col>
              </v-row>
            </div>
            <h2>Information from Provider Laboratory - Clinical Details</h2>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <date-picker
                  v-model="model.CollectionDate"
                  label="Sample Collection Date"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="CollectionDate"
                  :properties-errors="propertiesErrors"
                  @input="update"
                >
                </date-picker>
              </v-col>
            </v-row>
            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.CollectionDateValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="CollectionDateValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.CollectionDateValidation != 0"
                  v-model="model.CollectionDateComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="CollectionDateComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="model.Location"
                  label="Sampling Location"
                  :readonly="readonly"
                  :prepend-icon="prependIconField"
                  :properties-errors="propertiesErrors"
                  property-name="Location"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.LocationValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="LocationValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.LocationValidation != 0"
                  v-model="model.LocationComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="LocationComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.IsolationHostTypeId"
                  :items="isolationHostTypesItems"
                  item-text="Text"
                  item-value="Value"
                  label="Host"
                  :readonly="readonly"
                  :prepend-icon="prependIconField"
                  :properties-errors="propertiesErrors"
                  property-name="IsolationHostTypeId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.IsolationHostTypeValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="IsolationHostTypeValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.IsolationHostTypeValidation != 0"
                  v-model="model.IsolationHostTypeComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="IsolationHostTypeComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.Gender"
                  :items="genders"
                  item-text="Text"
                  item-value="Value"
                  label="Gender"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="Gender"
                  :properties-errors="propertiesErrors"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.GenderValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="GenderValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.GenderValidation != 0"
                  v-model="model.GenderComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="GenderComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field-float
                  v-model="model.Age"
                  label="Age"
                  :buttons="false"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="Age"
                  :properties-errors="propertiesErrors"
                  @input="update"
                >
                </text-field-float>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.AgeValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="AgeValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.AgeValidation != 0"
                  v-model="model.AgeComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="AgeComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="model.PatientStatus"
                  label="Patient Status in Sampling"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="PatientStatus"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.PatientStatusValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="PatientStatusValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.PatientStatusValidation != 0"
                  v-model="model.PatientStatusComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="PatientStatusComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.ShipmentMaterialCondition"
                  :items="shipmentMaterialConditions"
                  item-text="Text"
                  item-value="Value"
                  label="Condition"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="ShipmentMaterialCondition"
                  :properties-errors="propertiesErrors"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row class="mb-15">
              <v-col cols="12" md="6" lg="6">
                <MaterialValidationSelectionComponent
                  v-model="model.ShipmentMaterialConditionValidation"
                  :readonly="!canVerifyMaterial"
                  :prepend-icon="prependIconValidation"
                  label="Validation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentMaterialConditionValidation"
                  @input="update"
                >
                </MaterialValidationSelectionComponent>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-area
                  v-show="model.ShipmentMaterialConditionValidation != 0"
                  v-model="model.ShipmentMaterialConditionComment"
                  :prepend-icon="prependIconValidation"
                  label="Comment"
                  :readonly="!canVerifyMaterial"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentMaterialConditionComment"
                  @input="update"
                >
                </text-area>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="model.ShipmentMaterialConditionNote"
                  label="Note"
                  :prepend-icon="prependIconShipmentMaterialField"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentMaterialConditionNote"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>
          </div>

          <h2>Laboratory Analysis Results</h2>
          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.CulturingResult"
                :items="culturingResultItems"
                item-text="Text"
                item-value="Value"
                label="Culturing Result"
                :readonly="readonly"
                property-name="CulturingResult"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.CulturingResultValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="CulturingResultValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.CulturingResultValidation != 0"
                v-model="model.CulturingResultComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="CulturingResultComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <date-picker
                v-model="model.CulturingResultDate"
                label="Culturing Result Date"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="CulturingResultDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.CulturingResultDateValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="CulturingResultDateValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.CulturingResultDateValidation != 0"
                v-model="model.CulturingResultDateComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="CulturingResultDateComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.IsolationTechniqueTypeId"
                :items="isolationTechniqueTypesItems"
                item-text="Text"
                item-value="Value"
                label="Isolation Technique"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="IsolationTechniqueTypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.IsolationTechniqueTypeValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="IsolationTechniqueTypeValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.IsolationTechniqueTypeValidation != 0"
                v-model="model.IsolationTechniqueTypeComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="IsolationTechniqueTypeIdComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.QualityControlResult"
                :items="qualityControlResultItems"
                item-text="Text"
                item-value="Value"
                label="Quality Control Result"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="QualityControlResult"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.QualityControlResultValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="QualityControlResultValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.QualityControlResultValidation != 0"
                v-model="model.QualityControlResultComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="QualityControlResultComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <date-picker
                v-model="model.QualityControlResultDate"
                label="Quality Control Result Date"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="QualityControlResultDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.QualityControlResultDateValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="QualityControlResultDateValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.QualityControlResultDateValidation != 0"
                v-model="model.QualityControlResultDateComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="QualityControlResultDateComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Infectivity"
                label="Infectivity of Cultured Material"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="Infectivity"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.InfectivityValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="InfectivityValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.InfectivityValidation != 0"
                v-model="model.InfectivityComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="InfectivityComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.ViralTiter"
                label="Viral Titer of Cultured Material"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="ViralTiter"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.ViralTiterValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="ViralTiterValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.ViralTiterValidation != 0"
                v-model="model.ViralTiterComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="ViralTiterComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.GSDAnalysisResult"
                :items="gsdAnalysisResultItems"
                item-text="Text"
                item-value="Value"
                label="GSD Analysis Result"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="GSDAnalysisResult"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.GSDAnalysisResultValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="GSDAnalysisResultValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.GSDAnalysisResultValidation != 0"
                v-model="model.GSDAnalysisResultComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="GSDAnalysisResultComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <date-picker
                v-model="model.GSDAnalysisResultDate"
                label="GSD Analysis Result Date"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="GSDAnalysisResultDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.GSDAnalysisResultDateValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="GSDAnalysisResultDateValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.GSDAnalysisResultDateValidation != 0"
                v-model="model.GSDAnalysisResultDateComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="GSDAnalysisResultDateComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.GSDUploadingStatus"
                :items="gsdUploadingStatusItems"
                item-text="Text"
                item-value="Value"
                label="GSD Uploading Status"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="GSDUploadingStatus"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.GSDUploadingStatusValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="GSDUploadingStatusValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.GSDUploadingStatusValidation != 0"
                v-model="model.GSDUploadingStatusComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="GSDUploadingStatusComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row v-if="GSDUploadingDateVisible">
            <v-col cols="12" md="12" lg="12">
              <date-picker
                v-model="model.GSDUploadingDate"
                label="GSD Uploading Date"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="GSDUploadingStatus"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row class="mb-15" v-if="GSDUploadingDateVisible">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.GSDUploadingDateValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="GSDUploadingDateValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.GSDUploadingDateValidation != 0"
                v-model="model.GSDUploadingDateComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="GSDUploadingDateComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.GeneticSequenceDataId"
                :items="geneticSequenceDatasItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                label="Externally Registered Database for GSD"
                :properties-errors="propertiesErrors"
                property-name="GeneticSequenceDataId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.GeneticSequenceDataValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="GeneticSequenceDataValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.GeneticSequenceDataValidation != 0"
                v-model="model.GeneticSequenceDataComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="GeneticSequenceDataComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.DatabaseUploadedBy"
                :items="databaseUploadedByItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                label="Uploaded by"
                :properties-errors="propertiesErrors"
                property-name="DatabaseUploadedBy"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.DatabaseUploadedByValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="DatabaseUploadedByValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.DatabaseUploadedByValidation != 0"
                v-model="model.DatabaseUploadedByComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="DatabaseUploadedByComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.DatabaseAccessionId"
                label="Accession ID in Externally Registered Database for GSD"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="DatabaseAccessionId"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.DatabaseAccessionIdValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="DatabaseAccessionIdValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.DatabaseAccessionIdValidation != 0"
                v-model="model.DatabaseAccessionIdComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="DatabaseAccessionIdComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <div>
            <CardActionsGenericButton
              v-if="!newGeneticSequenceDataClicked && !readonly"
              color="primary"
              text="New"
              @click="newGeneticSequenceDataClick"
            >
            </CardActionsGenericButton>
            <br />
            <div v-if="newGeneticSequenceDataClicked && !readonly">
              <MaterialGSDInfoForm
                v-model="temporaryMaterialGSDInfo"
                :can-edit="!readonly"
              >
              </MaterialGSDInfoForm>
              <v-container class="px-0" fluid>
                <CardActionsGenericButton
                  style="display: inline-block; float: right"
                  color="error"
                  text="Cancel"
                  @click="cancel"
                >
                </CardActionsGenericButton>
                <CardActionsGenericButton
                  style="display: inline-block; float: right"
                  v-if="addVisible"
                  text="Add"
                  @click="addGeneticSequenceData"
                >
                </CardActionsGenericButton>

                <v-spacer></v-spacer>
              </v-container>
            </div>
          </div>

          <MaterialGSDInfoTable
            style="margin-top: 50px"
            :can-edit="!readonly"
            :can-read="canRead"
            @delete="deleteGSDInfo"
            :items="MaterialLaboratoryCompletionGSDInfo"
          >
          </MaterialGSDInfoTable>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.MaterialGSDInfoValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="MaterialGSDInfoValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.MaterialGSDInfoValidation != 0"
                v-model="model.MaterialGSDInfoComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="MaterialGSDInfoComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.StrainDesignation"
                label="Strain Designation at External Registered Database for GSD"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="StrainDesignation"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.StrainDesignationValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="StrainDesignationValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.StrainDesignationValidation != 0"
                v-model="model.StrainDesignationComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="StrainDesignationComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <h2>Available Aliquots in Stock</h2>

          <v-row>
            <v-col cols="12" md="4" lg="4">
              <text-field-float
                v-model="model.CurrentNumberOfVials"
                label="Current Number Of Vials"
                :buttons="false"
                readonly
                property-name="CurrentNumberOfVials"
                :properties-errors="propertiesErrors"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field-float
                v-model="model.WarningEmailCurrentNumberOfVialsThreshold"
                label="Warning Email Threshold"
                :buttons="false"
                :readonly="
                  readonly ||
                  !CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold
                "
                property-name="WarningEmailCurrentNumberOfVialsThreshold"
                :properties-errors="propertiesErrors"
              >
              </text-field-float>
            </v-col>
          </v-row>

          <v-row v-if="CanAddMaterialNewVials && !readonly">
            <v-col cols="12" md="4" lg="4" v-if="addNumberOfVialsVisible">
              <CardActionsGenericButton
                alignment="text-left"
                text="Add Number of Aliquots"
                @click="onAddNumberOfVialsClick"
              >
              </CardActionsGenericButton>
            </v-col>
            <v-col cols="12" md="4" lg="4" v-if="!addNumberOfVialsVisible">
              <text-field-float
                v-model="model.NumberOfVialsToAdd"
                label="Number of Aliquots Added"
                :buttons="false"
                property-name="NumberOfVialsToAdd"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="4" lg="4" v-if="!addNumberOfVialsVisible">
              <CardActionsGenericButton
                alignment="text-left"
                text="Cancel"
                color="primary"
                @click="onCancelAddNumberOfVialsClick"
              >
              </CardActionsGenericButton>
            </v-col>

            <v-col cols="12" md="4" lg="4" v-if="!addNumberOfVialsVisible">
              <date-picker
                v-model="model.LastAliquotsAdditionDate"
                label="Date"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="LastAliquotsAdditionDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Description"
                label="Description"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="Description"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row class="mb-15">
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.DescriptionValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="DescriptionValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.DescriptionValidation != 0"
                v-model="model.DescriptionComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="DescriptionComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row>

          <h2>Permission of Material and Data Sharing</h2>

          <v-row>
            <v-col
              v-if="!readonly && CanSetMaterialReadyToShare"
              cols="12"
              md="4"
              lg="4"
            >
              <dropdown
                v-model="model.BHFShareReadiness"
                :items="BHFShareReadinessValues"
                item-text="Text"
                item-value="Value"
                label="BioHub Facility's Readiness for Sharing Material"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="BHFShareReadiness"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col v-else cols="12" md="5" lg="5">
              <text-field
                v-if="BHFShareReadiness"
                model="This Material is ready to be shared"
                label="BioHub Facility's Readiness for Sharing Material"
                readonly
              >
              </text-field>
              <text-field
                v-else
                model="This Material cannot be marked as public"
                label="BioHub Facility's Readiness for Sharing Material"
                readonly
              >
              </text-field>
            </v-col>

            <v-col
              v-if="!readonly && CanSetMaterialPublic && BHFShareReadiness"
              cols="12"
              md="4"
              lg="4"
            >
              <dropdown
                v-model="model.PublicShare"
                :items="PublicShareValues"
                item-text="Text"
                item-value="Value"
                label="Data Above Shared on the Public Page"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                property-name="PublicShare"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col v-else cols="12" md="4" lg="4">
              <text-field
                v-model="PublicShare"
                label="Data Above Shared on the Public Page"
                disabled
                readonly
                solo
              >
              </text-field>
            </v-col>
          </v-row>

          <!-- <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.SampleId"
                label="Provider's Sample ID"
                :readonly="readonly"
                :prepend-icon="prependIconField"
                :properties-errors="propertiesErrors"
                property-name="SampleId"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6" lg="6">
              <MaterialValidationSelectionComponent
                v-model="model.SampleIdValidation"
                :readonly="!canVerifyMaterial"
                :prepend-icon="prependIconValidation"
                label="Validation"
                :properties-errors="propertiesErrors"
                property-name="SampleIdValidation"
                @input="update"
              >
              </MaterialValidationSelectionComponent>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-area
                v-show="model.SampleIdValidation != 0"
                v-model="model.SampleIdComment"
                :prepend-icon="prependIconValidation"
                label="Comment"
                :readonly="!canVerifyMaterial"
                :properties-errors="propertiesErrors"
                property-name="SampleIdComment"
                @input="update"
              >
              </text-area>
            </v-col>
          </v-row> -->

          <!-- ************************************************* -->
        </div>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import BackButton from "@/components/BackButton.vue";
import { MaterialLaboratoryCompletion } from "@/models/MaterialLaboratoryCompletion";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import Dropdown from "@/components/Dropdown.vue";
import Checkbox from "@/components/Checkbox.vue";
import { MaterialModule } from "../store";
import { LaboratoryModule } from "../../laboratories/store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import { CountryModule } from "../../countries/store";
import { MaterialTypeModule } from "../../materialTypes/store";
import { MaterialProductModule } from "../../materialProducts/store";
import { TransportCategoryModule } from "../../transportCategories/store";
import { TemperatureUnitOfMeasureModule } from "../../temperatureUnitOfMeasures/store";
import { MaterialUsagePermissionModule } from "../../materialUsagePermissions/store";
import { GeneticSequenceDataModule } from "../../geneticSequenceDatas/store";
import { InternationalTaxonomyClassificationModule } from "../../internationalTaxonomyClassifications/store";
import { IsolationHostTypeModule } from "../../isolationHostTypes/store";
import { CultivabilityTypeModule } from "../../cultivabilityTypes/store";
import { IsolationTechniqueTypeModule } from "../../isolationTechniqueTypes/store";
import { MaterialProviderLaboratoryDropDown } from "@/models/MaterialProviderLaboratoryDropDown";
import DatePicker from "@/components/DatePicker.vue";
import { Gender } from "@/models/enums/Gender";
import MaterialValidationSelectionComponent from "./MaterialValidationSelectionComponent.vue";
import TextArea from "@/components/TextArea.vue";
import { hasPermission } from "../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { DropdownItem } from "@/models/DropdownItem";
import { Readiness } from "@/models/enums/Readiness";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { DatabaseUploadedBy } from "@/models/enums/DatabaseUploadedBy";
import { SpecimenTypeModule } from "../../specimenTypes/store";
import { SeedData } from "@/models/constants/SeedData";
import { GSDUploadingStatus } from "@/models/enums/GSDUploadingStatus";
import { ResultType } from "@/models/enums/ResultType";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";
import MaterialGSDInfoForm from "./MaterialGSDInfoForm.vue";
import MaterialGSDInfoTable from "./MaterialGSDInfoTable.vue";
import CardActionsGenericButton from "../../../components/CardActionsGenericButton.vue";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

@Component({
  components: {
    BackButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    Checkbox,
    DatePicker,
    MaterialValidationSelectionComponent,
    TextArea,
    MaterialGSDInfoForm,
    MaterialGSDInfoTable,
    CardActionsGenericButton,
  },
})
export default class MaterialLaboratoryCompletionForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private IsProviderBioHubFacility = false;

  private CollectedSpecimenType = "";

  private newGeneticSequenceDataClicked = false;

  private addVisible = false;

  $refs!: {
    form: any;
  };

  // Props

  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canVerifyMaterial: boolean;

  @Prop({ required: true, type: String, default: "Material" })
  readonly title: string;

  // Model
  @Model("update", { type: Object }) model!: MaterialLaboratoryCompletion;

  validate() {
    this.$refs.form.validate();
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  get CanSetMaterialReadyToShare(): boolean {
    return hasPermission(PermissionNames.CanSetMaterialReadyToShare);
  }

  get CanAddMaterialNewVials(): boolean {
    return hasPermission(PermissionNames.CanAddMaterialNewVials);
  }

  get CanEditMaterialOwnerBioHubFacility(): boolean {
    return hasPermission(PermissionNames.CanEditMaterialOwnerBioHubFacility);
  }

  get CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold(): boolean {
    return hasPermission(
      PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold
    );
  }

  get genders(): Array<DropdownItem> {
    const gendersList = new Array<DropdownItem>();
    gendersList.push({ Text: "Male", Value: Gender.Male });
    gendersList.push({ Text: "Female", Value: Gender.Female });
    gendersList.push({ Text: "Undisclosed", Value: Gender.Undisclosed });
    return gendersList;
  }

  get BHFShareReadinessValues(): Array<DropdownItem> {
    const list = new Array<DropdownItem>();
    list.push({ Text: "Ready", Value: Readiness.Ready });
    list.push({ Text: "Not Ready", Value: Readiness.NotReady });
    return list;
  }

  get BHFShareReadiness(): boolean {
    return this.model.BHFShareReadiness == Readiness.Ready;
  }

  get PublicShareValues(): Array<DropdownItem> {
    const list = new Array<DropdownItem>();
    list.push({ Text: "Yes", Value: YesNoOption.Yes });
    list.push({ Text: "No", Value: YesNoOption.No });
    return list;
  }

  get PublicShare(): string {
    if (this.model.PublicShare == YesNoOption.Yes) {
      return "Yes";
    } else {
      return "No";
    }
  }

  get prependIconField(): string {
    if (
      this.readonly == false &&
      hasPermission(PermissionNames.CanEditMaterial)
    ) {
      return "mdi-pencil-circle-outline";
    } else {
      return "";
    }
  }

  get prependIconShipmentMaterialField(): string {
    if (
      this.readonly == false &&
      hasPermission(PermissionNames.CanEditMaterialShipmentInformation)
    ) {
      return "mdi-pencil-circle-outline";
    } else {
      return "";
    }
  }

  get prependIconValidation(): string {
    if (
      this.readonly == false &&
      hasPermission(PermissionNames.CanVerifyMaterial)
    ) {
      return "mdi-pencil-circle-outline";
    } else {
      return "";
    }
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
      var bioHubFacilityInfo = BioHubFacilityModule.BioHubFacilities.filter(
        (b) => {
          return b.Id == this.model.ProviderBioHubFacilityId;
        }
      ).map((b) => {
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
      var laboratoryInfo = LaboratoryModule.Laboratories.filter((l) => {
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

  get OwnerBioHubFacilityString(): string {
    var bioHubFacilityInfo = BioHubFacilityModule.BioHubFacilities.filter(
      (b) => {
        return b.Id == this.model.OwnerBioHubFacilityId;
      }
    ).map((b) => {
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
  }

  get IsPublicFacingString(): string {
    if (this.model !== undefined) {
      if (this.model.PublicShare == YesNoOption.Yes) {
        return "This Material is public";
      } else {
        return "This Material is WHO internal";
      }
    } else {
      return "";
    }
  }

  onBack() {
    MaterialModule.SET_ERROR(undefined);
  }

  updateProvider(e: string) {
    let itemElement = this.providersItems.filter((p) => {
      return p.Id == e;
    });

    if (itemElement[0] != undefined && itemElement[0].Type == "Laboratory") {
      this.model.ProviderLaboratoryId = itemElement[0].Id;
      this.model.ProviderBioHubFacilityId = null;
      this.IsProviderBioHubFacility = false;
    } else if (
      itemElement[0] != undefined &&
      itemElement[0].Type == "BioHubFacility"
    ) {
      this.model.ProviderBioHubFacilityId = itemElement[0].Id;
      this.model.ProviderLaboratoryId = null;
      this.IsProviderBioHubFacility = true;
    }

    this.$emit("update", this.model);
  }

  updateCollectedSpecimenTypes() {
    this.model.MaterialCollectedSpecimenTypes = [this.CollectedSpecimenType];
    this.update();
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

  get temperatureUnitOfMeasuresItems(): Array<DropdownItem> {
    const TemperatureUnitOfMeasures =
      TemperatureUnitOfMeasureModule.TemperatureUnitOfMeasures;
    if (!TemperatureUnitOfMeasures) return new Array<DropdownItem>();

    return TemperatureUnitOfMeasures.map((l) => {
      return {
        Value: l.Id,
        Text: l.Unit,
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

  get isolationTechniqueTypesItems(): Array<DropdownItem> {
    const IsolationTechniqueTypes =
      IsolationTechniqueTypeModule.IsolationTechniqueTypes;
    if (!IsolationTechniqueTypes) return new Array<DropdownItem>();

    return IsolationTechniqueTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get cultivabilityTypesItems(): Array<DropdownItem> {
    const CultivabilityTypes = CultivabilityTypeModule.CultivabilityTypes;
    if (!CultivabilityTypes) return new Array<DropdownItem>();

    return CultivabilityTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get transportCategoriesItems(): Array<DropdownItem> {
    const TransportCategories = TransportCategoryModule.TransportCategories;
    if (!TransportCategories) return new Array<DropdownItem>();

    return TransportCategories.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get providersItems(): Array<MaterialProviderLaboratoryDropDown> {
    const Laboratories = LaboratoryModule.Laboratories;
    const BioHubFacilities = BioHubFacilityModule.BioHubFacilities;
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

  get bioHubFacilitiesItems(): Array<DropdownItem> {
    var items = new Array<DropdownItem>();

    BioHubFacilityModule.BioHubFacilities.forEach((b) => {
      const bioHubFacilityCountryNameInfo = CountryModule.Countries.filter(
        (country) => {
          return country.Id == b.CountryId;
        }
      ).map((c) => {
        return {
          name: c.Name,
        };
      });

      if (bioHubFacilityCountryNameInfo.length == 0) {
        bioHubFacilityCountryNameInfo.push({
          name: "",
        });
      }

      items.push({
        Value: b.Id,
        Text: bioHubFacilityCountryNameInfo[0].name + " - " + b.Name,
      });
    });
    return items;
  }

  get cultureIsolate(): boolean {
    return (
      this.model.OriginalProductTypeId == SeedData.CulturedIsolateProductId
    );
  }

  get clinicalSpecimen(): boolean {
    return (
      this.model.OriginalProductTypeId == SeedData.ClinicalSpecimenProductId
    );
  }

  get propertiesErrors(): any {
    return MaterialModule.ErrorMessage;
  }

  get temporaryMaterialGSDInfo(): MaterialGSDInfo | undefined {
    return MaterialModule.TemporaryMaterialGSDInfo;
  }

  set temporaryMaterialGSDInfo(item: MaterialGSDInfo | undefined) {
    MaterialModule.SET_TEMPORARY_MATERIAL_GSD_INFO(item);
    this.addVisible = this.setAddVisible();
  }

  get GSDUploadingDateVisible(): boolean {
    return this.model.GSDUploadingStatus == GSDUploadingStatus.Uploaded;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadMaterial);
  }

  newGeneticSequenceDataClick(): void {
    MaterialModule.SET_NEW_TEMPORARY_MATERIAL_GSD_INFO();
    this.newGeneticSequenceDataClicked = true;
    this.addVisible = this.setAddVisible();
  }

  addGeneticSequenceData(): void {
    MaterialModule.ADD_TEMPORARY_MATERIAL_LABORATORY_COMPLETION_GSD_INFO();
    this.newGeneticSequenceDataClicked = false;
    this.addVisible = this.setAddVisible();
  }

  updateGeneticSequenceData(): void {
    this.newGeneticSequenceDataClicked = false;
    this.addVisible = this.setAddVisible();
  }

  cancel(): void {
    MaterialModule.CLEAR_TEMPORARY_MATERIAL_GSD_INFO();
    this.newGeneticSequenceDataClicked = false;
    this.addVisible = this.setAddVisible();
  }

  setAddVisible(): boolean {
    if (this.newGeneticSequenceDataClicked === false) {
      return false;
    }
    if (this.temporaryMaterialGSDInfo === undefined) {
      return false;
    }

    if (
      this.temporaryMaterialGSDInfo.CellLine == undefined ||
      this.temporaryMaterialGSDInfo.CellLine == "" ||
      this.temporaryMaterialGSDInfo.CellLine == null
    ) {
      return false;
    }

    if (
      this.temporaryMaterialGSDInfo.GSDFasta === undefined ||
      this.temporaryMaterialGSDInfo.GSDFasta === "" ||
      this.temporaryMaterialGSDInfo.GSDFasta === null
    ) {
      return false;
    }

    if (
      this.temporaryMaterialGSDInfo.GSDType === undefined ||
      this.temporaryMaterialGSDInfo.GSDFasta === null
    ) {
      return false;
    }

    if (
      this.temporaryMaterialGSDInfo.PassageNumber === undefined ||
      this.temporaryMaterialGSDInfo.PassageNumber < 0 ||
      isNaN(this.temporaryMaterialGSDInfo.PassageNumber)
    ) {
      return false;
    }

    return true;
  }

  get CanEditMaterialShipmentInformation(): boolean {
    return hasPermission(PermissionNames.CanEditMaterialShipmentInformation);
  }

  get SpecimenTypes(): Array<DropdownItem> {
    const SpecimenTypes = SpecimenTypeModule.SpecimenTypes;
    if (!SpecimenTypes) return new Array<DropdownItem>();

    return SpecimenTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get databaseUploadedByItems(): Array<DropdownItem> {
    const databaseUploadedByItemsList = new Array<DropdownItem>();
    databaseUploadedByItemsList.push({
      Text: "Provider",
      Value: DatabaseUploadedBy.Provider,
    });
    databaseUploadedByItemsList.push({
      Text: "BioHubFacility",
      Value: DatabaseUploadedBy.BioHubFacility,
    });
    databaseUploadedByItemsList.push({
      Text: "Other",
      Value: DatabaseUploadedBy.Other,
    });
    return databaseUploadedByItemsList;
  }

  get culturingResultItems(): Array<DropdownItem> {
    const culturingResultList = new Array<DropdownItem>();
    culturingResultList.push({
      Text: "Succeeded",
      Value: ResultType.Succeeded,
    });
    culturingResultList.push({ Text: "Failed", Value: ResultType.Failed });
    culturingResultList.push({ Text: "Other", Value: ResultType.Other });
    return culturingResultList;
  }

  get qualityControlResultItems(): Array<DropdownItem> {
    const qualityControlResultList = new Array<DropdownItem>();
    qualityControlResultList.push({
      Text: "Succeeded (Ready to Share)",
      Value: ResultType.Succeeded,
    });
    qualityControlResultList.push({ Text: "Failed", Value: ResultType.Failed });
    qualityControlResultList.push({ Text: "Other", Value: ResultType.Other });
    return qualityControlResultList;
  }

  get gsdAnalysisResultItems(): Array<DropdownItem> {
    const gsdAnalysisResultList = new Array<DropdownItem>();
    gsdAnalysisResultList.push({
      Text: "Succeeded",
      Value: ResultType.Succeeded,
    });
    gsdAnalysisResultList.push({ Text: "Failed", Value: ResultType.Failed });
    gsdAnalysisResultList.push({ Text: "Other", Value: ResultType.Other });
    return gsdAnalysisResultList;
  }

  get gsdUploadingStatusItems(): Array<DropdownItem> {
    const gsdUploadingStatusList = new Array<DropdownItem>();
    gsdUploadingStatusList.push({
      Text: "Uploaded",
      Value: GSDUploadingStatus.Uploaded,
    });
    gsdUploadingStatusList.push({
      Text: "Not uploaded (uploaded by Provider)",
      Value: GSDUploadingStatus.NotUploadedProvider,
    });
    gsdUploadingStatusList.push({
      Text: "Not uploaded (other reasons)",
      Value: GSDUploadingStatus.NotUploadedOtherReasons,
    });
    gsdUploadingStatusList.push({
      Text: "Other",
      Value: GSDUploadingStatus.Other,
    });
    return gsdUploadingStatusList;
  }

  get MaterialLaboratoryCompletionGSDInfo() {
    return MaterialModule.MaterialLaboratoryCompletionGSDInfo;
  }

  get shipmentMaterialConditions(): Array<DropdownItem> {
    const conditionsList = new Array<DropdownItem>();
    conditionsList.push({
      Text: "Intact",
      Value: ShipmentMaterialCondition.Intact,
    });
    conditionsList.push({
      Text: "Damaged",
      Value: ShipmentMaterialCondition.Damaged,
    });
    return conditionsList;
  }

  deleteGSDInfo(id: string) {
    MaterialModule.REMOVE_MATERIAL_LABORATORY_COMPLETION_GSD_INFO(id);
  }

  mounted() {
    if (this.model.MaterialCollectedSpecimenTypes.length > 0) {
      this.CollectedSpecimenType = this.model.MaterialCollectedSpecimenTypes[0];
    }
  }

  @Watch("model.MaterialCollectedSpecimenTypes.length")
  CollectedSpecimenTypesChange() {
    if (this.model.MaterialCollectedSpecimenTypes.length > 0) {
      this.CollectedSpecimenType = this.model.MaterialCollectedSpecimenTypes[0];
    }
  }

  @Watch("model.QualityControlResult")
  QualityControlResultChange() {
    if (
      this.model.QualityControlResult == ResultType.Succeeded &&
      this.model.BHFShareReadiness != null &&
      this.CanSetMaterialReadyToShare
    ) {
      this.model.BHFShareReadiness = Readiness.Ready;
      this.update();
    }
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
