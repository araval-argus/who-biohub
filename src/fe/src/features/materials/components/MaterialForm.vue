<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton @back="onBack" />
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <h2>Basic Information</h2>
          <v-row>
            <v-col cols="12" md="3" lg="3">
              <text-field
                v-model="model.ReferenceNumber"
                label="WHO Reference Number"
                :prepend-icon="prependIcon"
                :readonly="readonly"
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
                :readonly="readonly"
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
                label="Pathogen Type"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="TypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="3" lg="3">
              <text-field
                v-model="model.Pathogen"
                label="Pathogen"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Pathogen"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="3" lg="3">
              <text-field
                v-model="model.Variant"
                label="Variant"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Variant"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="3" lg="3">
              <text-field
                v-model="model.VariantAssessment"
                label="WHO Variant Assessment"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="VariantAssessment"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="3" lg="3">
              <text-field
                v-model="model.Lineage"
                label="Lineage (Pango)"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Lineage"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row>
            <!-- <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.FacilityGSD"
                label="Facility that Generated GSD"
                :prepend-icon="prependIcon"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="FacilityGSD"
                @input="update"
              >
              </text-field>
            </v-col> -->
          </v-row>
          <v-row>
            <v-col cols="12" md="8" lg="8">
              <dropdown
                v-model="model.InternationalTaxonomyClassificationId"
                :items="internationalTaxonomyClassificationsItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                label="International Taxonomy Classification"
                :properties-errors="propertiesErrors"
                property-name="InternationalTaxonomyClassificationId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <checkbox
                v-model="model.GMO"
                label="Genetically Modified Organism (GMO)"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="GMO"
                @change="update"
              >
              </checkbox>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.SuspectedEpidemiologicalOriginId"
                :menu-props="{ auto: true }"
                :items="countriesItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                label="Suspected Epidemiological Original (Country)"
                :properties-errors="propertiesErrors"
                property-name="SuspectedEpidemiologicalOriginId"
                @change="update"
              ></dropdown>
            </v-col>

            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.UsagePermissionId"
                :items="materialUsagePermissionsItems"
                item-text="Text"
                item-value="Value"
                label="Usage Permission"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="UsagePermissionId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row>
            <v-col
              v-if="readonly || !CanEditMaterialOwnerBioHubFacility"
              cols="12"
              md="6"
              lg="6"
            >
              <text-field
                v-if="OwnerBioHubFacilityString != ''"
                v-model="OwnerBioHubFacilityString"
                label="Owner BioHub Facility"
                :properties-errors="propertiesErrors"
                property-name="OwnerBioHubFacilityId"
              >
              </text-field>
            </v-col>
            <v-col v-else cols="12" md="6" lg="6">
              <dropdown
                v-model="model.OwnerBioHubFacilityId"
                :menu-props="{ auto: true }"
                :items="bioHubFacilitiesItems"
                item-text="Text"
                item-value="Value"
                label="Owner BioHub Facility"
                :properties-errors="propertiesErrors"
                property-name="OwnerBioHubFacilityId"
                @change="update"
              ></dropdown>
            </v-col>

            <v-col v-if="IsProviderBioHubFacility" cols="12" md="6" lg="6">
              <dropdown
                v-model="model.ProviderBioHubFacilityId"
                :items="providersItems"
                item-text="Name"
                item-value="Id"
                label="Provider BioHub Facility"
                :readonly="readonly || !CanEditMaterialShipmentInformation"
                :properties-errors="propertiesErrors"
                property-name="ProviderBioHubFacilityId"
                @change="updateProvider($event)"
              ></dropdown>
              <!-- <text-field
                v-else
                v-model="CountryProviderString"
                label="Provider BioHub Facility"
                readonly
                :properties-errors="propertiesErrors"
                property-name="ProviderBioHubFacilityId"
                @input="update"
              >
              </text-field> -->
            </v-col>
            <v-col v-else cols="12" md="6" lg="6">
              <dropdown
                v-model="model.ProviderLaboratoryId"
                :menu-props="{ auto: true }"
                :items="providersItems"
                item-text="Name"
                item-value="Id"
                label="Provider Laboratory"
                :readonly="readonly || !CanEditMaterialShipmentInformation"
                :properties-errors="propertiesErrors"
                property-name="ProviderLaboratoryId"
                @change="updateProvider($event)"
              ></dropdown>
              <!-- <text-field
                v-else
                v-model="CountryProviderString"
                label="Provider Laboratory"
                :readonly="readOnlySpecificCondition || bioHubFacilityArea"
                :properties-errors="propertiesErrors"
                property-name="ProviderLaboratoryId"
                @input="update"
              >
              </text-field> -->
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <date-picker
                v-model="model.DateOfBMEPPReceipt"
                label="Arrival Date at BioHub Facility"
                readonly
                property-name="DateOfBMEPPReceipt"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>

            <v-col cols="12" md="4" lg="4">
              <dropdown
                v-model="model.ProductTypeId"
                :items="materialProductsItems"
                item-text="Text"
                item-value="Value"
                label="Product Type"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="ProductTypeId"
                @change="update"
              ></dropdown>
            </v-col>
            <template v-if="readonly">
              <v-col cols="12" md="8" lg="8">
                <text-field
                  v-model="TemperatureString"
                  label="Temperature"
                  readonly
                  :properties-errors="propertiesErrors"
                  property-name="UnitOfMeasureId"
                  @input="update"
                ></text-field>
              </v-col>
            </template>
            <template v-else>
              <v-col cols="12" md="2" lg="2">
                <text-field-float
                  v-model="model.Temperature"
                  label="Temperature"
                  :step="0.01"
                  :precision="2"
                  :buttons="false"
                  :readonly="false"
                  decimal
                  :properties-errors="propertiesErrors"
                  property-name="Temperature"
                  @input="update"
                >
                </text-field-float>
              </v-col>
              <v-col cols="12" md="2" lg="2">
                <dropdown
                  v-model="model.UnitOfMeasureId"
                  :items="temperatureUnitOfMeasuresItems"
                  item-text="Text"
                  item-value="Value"
                  label="Unit (Temperature)"
                  :readonly="false"
                  :properties-errors="propertiesErrors"
                  property-name="UnitOfMeasureId"
                  @change="update"
                ></dropdown>
              </v-col>
            </template>
          </v-row>
          <div v-if="model.ShipmentInformationVisible">
            <h2 class="mt-10">
              Information about Shipment from Provider to BioHub Facility
            </h2>

            <v-row>
              <v-col cols="12" md="4" lg="4">
                <dropdown
                  v-model="model.OriginalProductTypeId"
                  :items="materialProductsItems"
                  item-text="Text"
                  item-value="Value"
                  label="Material Type in Shipment to BioHub Facility"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="OriginalProductTypeId"
                  @change="update"
                ></dropdown>
              </v-col>
              <v-col cols="12" md="4" lg="4">
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
              <v-col cols="12" md="4" lg="4">
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
            <v-row>
              <v-col cols="12" md="6" lg="6">
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
              <v-col cols="12" md="6" lg="6">
                <text-field-float
                  v-model="model.ShipmentAmount"
                  label="Shipment Amount per Vial"
                  :step="0.01"
                  :precision="2"
                  :buttons="false"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  decimal
                  :properties-errors="propertiesErrors"
                  property-name="ShipmentAmount"
                  @input="update"
                >
                </text-field-float>
              </v-col>
            </v-row>

            <h2 class="mt-15">
              Information about Laboratory Analysis from Provider
            </h2>

            <v-row>
              <v-col cols="12" md="3" lg="3">
                <date-picker
                  v-model="model.FreezingDate"
                  label="Sample Freezing Date"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="FreezingDate"
                  :properties-errors="propertiesErrors"
                  @input="update"
                >
                </date-picker>
              </v-col>
              <template v-if="readonly || !CanEditMaterialShipmentInformation">
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="ShipmentTemperatureString"
                    label="Temperature in Storage"
                    readonly
                    :properties-errors="propertiesErrors"
                    property-name="ShipmentUnitOfMeasureId"
                    @input="update"
                  ></text-field>
                </v-col>
              </template>
              <template v-else>
                <v-col cols="12" md="3" lg="3">
                  <text-field-float
                    v-model="model.ShipmentTemperature"
                    label="Storage Condition Temperature"
                    :step="0.01"
                    :precision="2"
                    :buttons="false"
                    :readonly="false"
                    decimal
                    :properties-errors="propertiesErrors"
                    property-name="ShipmentTemperature"
                    @input="update"
                  >
                  </text-field-float>
                </v-col>
                <v-col cols="12" md="3" lg="3">
                  <dropdown
                    v-model="model.ShipmentUnitOfMeasureId"
                    :items="temperatureUnitOfMeasuresItems"
                    item-text="Text"
                    item-value="Value"
                    label="Unit (Temperature)"
                    :readonly="false"
                    :properties-errors="propertiesErrors"
                    property-name="ShipmentUnitOfMeasureId"
                    @change="update"
                  ></dropdown>
                </v-col>
              </template>
              <v-col cols="12" md="3" lg="3">
                <text-field
                  v-model="model.VirusConcentration"
                  label="Virus Concentration"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="VirusConcentration"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <text-field
                  v-model="model.SampleId"
                  label="Provider Material's ID"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="SampleId"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>

            <v-row v-if="cultureIsolate">
              <v-col cols="12" md="6" lg="6">
                <text-field
                  v-model="model.CulturingCellLine"
                  label="Culturing Cell Line"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="CulturingCellLine"
                  :properties-errors="allPropertiesErrors"
                  @input="update"
                >
                </text-field>
              </v-col>
              <v-col cols="12" md="6" lg="6">
                <text-field-float
                  v-model="model.CulturingPassagesNumber"
                  label="Culturing Passage Number"
                  :buttons="false"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="CulturingPassagesNumber"
                  :properties-errors="allPropertiesErrors"
                  @input="update"
                >
                </text-field-float>
              </v-col>
            </v-row>

            <v-row v-if="clinicalSpecimen">
              <v-col cols="12" md="6" lg="6">
                <dropdown
                  v-model="CollectedSpecimenType"
                  :items="SpecimenTypes"
                  item-text="Text"
                  item-value="Value"
                  label="Collected Specimen Type"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="CollectedSpecimenTypes"
                  :properties-errors="allPropertiesErrors"
                  @change="updateCollectedSpecimenTypes"
                ></dropdown>
              </v-col>

              <v-col cols="12" md="3" lg="3">
                <text-field
                  v-model="model.TypeOfTransportMedium"
                  label="Type Of Transport Medium"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="TypeOfTransportMedium"
                  :properties-errors="allPropertiesErrors"
                  @input="update"
                >
                </text-field>
              </v-col>

              <v-col cols="12" md="3" lg="3">
                <text-field
                  v-model="model.BrandOfTransportMedium"
                  label="Brand Of Transport Medium"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="BrandOfTransportMedium"
                  :properties-errors="allPropertiesErrors"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>
            <h2>Information from Provider Laboratory - Clinical Details</h2>
            <v-row>
              <v-col cols="12" md="4" lg="4">
                <date-picker
                  v-model="model.CollectionDate"
                  label="Sample Collection Date"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="CollectionDate"
                  :properties-errors="propertiesErrors"
                  @input="update"
                >
                </date-picker>
              </v-col>
              <v-col cols="12" md="4" lg="4">
                <text-field
                  v-model="model.Location"
                  label="Sampling Location"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="Location"
                  @input="update"
                >
                </text-field>
              </v-col>
              <v-col cols="12" md="4" lg="4">
                <dropdown
                  v-model="model.IsolationHostTypeId"
                  :items="isolationHostTypesItems"
                  item-text="Text"
                  item-value="Value"
                  label="Host"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="IsolationHostTypeId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>

            <v-row>
              <v-col cols="12" md="4" lg="4">
                <dropdown
                  v-model="model.Gender"
                  :items="genders"
                  item-text="Text"
                  item-value="Value"
                  label="Gender"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="Gender"
                  :properties-errors="propertiesErrors"
                  @change="update"
                ></dropdown>
              </v-col>
              <v-col cols="12" md="4" lg="4">
                <text-field-float
                  v-model="model.Age"
                  label="Age"
                  :buttons="false"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="Age"
                  :properties-errors="propertiesErrors"
                  @input="update"
                >
                </text-field-float>
              </v-col>
              <v-col cols="12" md="4" lg="4">
                <text-field
                  v-model="model.PatientStatus"
                  label="Patient Status in Sampling"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  :properties-errors="propertiesErrors"
                  property-name="PatientStatus"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" md="4" lg="4">
                <dropdown
                  v-model="model.ShipmentMaterialCondition"
                  :items="shipmentMaterialConditions"
                  item-text="Text"
                  item-value="Value"
                  label="Condition"
                  :readonly="readonly || !CanEditMaterialShipmentInformation"
                  property-name="ShipmentMaterialCondition"
                  :properties-errors="propertiesErrors"
                  @change="update"
                ></dropdown>
              </v-col>
              <v-col cols="12" md="4" lg="4">
                <text-field
                  v-model="model.ShipmentMaterialConditionNote"
                  label="Note"
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
            <v-col cols="12" md="6" lg="6">
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
            <v-col cols="12" md="6" lg="6">
              <date-picker
                v-model="model.CulturingResultDate"
                label="Culturing Result Date"
                :readonly="readonly"
                property-name="CulturingResultDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.IsolationTechniqueTypeId"
                :items="isolationTechniqueTypesItems"
                item-text="Text"
                item-value="Value"
                label="Isolation Technique"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="IsolationTechniqueTypeId"
                @change="update"
              ></dropdown>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.QualityControlResult"
                :items="qualityControlResultItems"
                item-text="Text"
                item-value="Value"
                label="Quality Control Result"
                :readonly="readonly"
                property-name="QualityControlResult"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <date-picker
                v-model="model.QualityControlResultDate"
                label="Quality Control Result Date"
                :readonly="readonly"
                property-name="QualityControlResultDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="model.Infectivity"
                label="Infectivity of Cultured Material"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Infectivity"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="model.ViralTiter"
                label="Viral Titer of Cultured Material"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="ViralTiter"
                @input="update"
              >
              </text-field>
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
                property-name="GSDAnalysisResult"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <date-picker
                v-model="model.GSDAnalysisResultDate"
                label="GSD Analysis Result Date"
                :readonly="readonly"
                property-name="GSDAnalysisResultDate"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="6" lg="6">
              <dropdown
                v-model="model.GSDUploadingStatus"
                :items="gsdUploadingStatusItems"
                item-text="Text"
                item-value="Value"
                label="GSD Uploading Status"
                :readonly="readonly"
                property-name="GSDUploadingStatus"
                :properties-errors="propertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col v-if="GSDUploadingDateVisible" cols="12" md="6" lg="6">
              <date-picker
                v-model="model.GSDUploadingDate"
                label="GSD Uploading Date"
                :readonly="readonly"
                property-name="GSDUploadingStatus"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </date-picker>
            </v-col>
          </v-row>

          <v-row>
            <v-col cols="12" md="4" lg="4">
              <dropdown
                v-model="model.GeneticSequenceDataId"
                :items="geneticSequenceDatasItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                label="Externally Registered Database for GSD"
                :properties-errors="propertiesErrors"
                property-name="GeneticSequenceDataId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <dropdown
                v-model="model.DatabaseUploadedBy"
                :items="databaseUploadedByItems"
                item-text="Text"
                item-value="Value"
                :readonly="readonly"
                label="Uploaded by"
                :properties-errors="propertiesErrors"
                property-name="DatabaseUploadedBy"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.DatabaseAccessionId"
                label="Accession ID in Externally Registered Database for GSD"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="DatabaseAccessionId"
                @input="update"
              >
              </text-field>
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
            :items="MaterialGSDInfo"
          >
          </MaterialGSDInfoTable>

          <v-row>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.StrainDesignation"
                label="Strain Designation at Externally Registered Database for GSD"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="StrainDesignation"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
          <h2 class="mt-5">Available Aliquots in Stock</h2>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.CurrentNumberOfVials"
                label="Available Number of Aliquots"
                :buttons="false"
                readonly
                property-name="CurrentNumberOfVials"
                :properties-errors="propertiesErrors"
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.WarningEmailCurrentNumberOfVialsThreshold"
                label="Threshold Number of Aliquots for BioHub Facility Notification"
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
                style="margin-top: 5px"
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
                label="Additional Information"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Description"
                @input="update"
              >
              </text-field>
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
import { Material } from "@/models/Material";
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
import DatePicker from "@/components/DatePicker.vue";
import { Gender } from "@/models/enums/Gender";
import { Readiness } from "@/models/enums/Readiness";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { hasPermission } from "../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import CardActionsGenericButton from "../../../components/CardActionsGenericButton.vue";
import { DropdownItem } from "@/models/DropdownItem";
import { DatabaseUploadedBy } from "@/models/enums/DatabaseUploadedBy";
import { SpecimenTypeModule } from "../../specimenTypes/store";
import { SeedData } from "@/models/constants/SeedData";
import { GSDUploadingStatus } from "@/models/enums/GSDUploadingStatus";
import { ResultType } from "@/models/enums/ResultType";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";
import MaterialGSDInfoForm from "./MaterialGSDInfoForm.vue";
import MaterialGSDInfoTable from "./MaterialGSDInfoTable.vue";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

@Component({
  components: {
    BackButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    Checkbox,
    DatePicker,
    CardActionsGenericButton,
    MaterialGSDInfoForm,
    MaterialGSDInfoTable,
  },
})
export default class MaterialForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private addNumberOfVialsVisible = true;

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
  readonly isPublicPage: boolean;

  @Prop({ type: Boolean, default: false })
  readonly isLaboratoryBmeppPage: boolean;

  @Prop({ type: Boolean, default: false })
  readonly laboratoryArea: boolean;

  @Prop({ type: Boolean, default: false })
  readonly bioHubFacilityArea: boolean;

  @Prop({ required: true, type: String, default: "" })
  readonly providerId: string;

  @Prop({ required: true, type: String, default: "Material" })
  readonly title: string;

  // Model
  @Model("update", { type: Object }) model!: Material;

  validate() {
    this.$refs.form.validate();
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  updateCollectedSpecimenTypes() {
    this.model.MaterialCollectedSpecimenTypes = [this.CollectedSpecimenType];
    this.update();
  }

  onAddNumberOfVialsClick() {
    this.addNumberOfVialsVisible = false;
    this.model.NumberOfVialsToAdd = 0;
    this.$emit("update", this.model);
  }

  onCancelAddNumberOfVialsClick() {
    this.model.NumberOfVialsToAdd = null;
    this.model.LastAliquotsAdditionDate = null;
    this.$emit("update", this.model);
    this.addNumberOfVialsVisible = true;
  }

  get CanEditMaterialOwnerBioHubFacility(): boolean {
    return hasPermission(PermissionNames.CanEditMaterialOwnerBioHubFacility);
  }

  get CanCreateMaterial(): boolean {
    return hasPermission(PermissionNames.CanCreateMaterial);
  }

  get CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold(): boolean {
    return hasPermission(
      PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold
    );
  }

  get CanSetMaterialPublic(): boolean {
    return hasPermission(PermissionNames.CanSetMaterialPublic);
  }

  get CanSetMaterialReadyToShare(): boolean {
    return (
      hasPermission(PermissionNames.CanSetMaterialReadyToShare) &&
      this.model.PublicShare != YesNoOption.Yes
    );
  }

  get CanAddMaterialNewVials(): boolean {
    return hasPermission(PermissionNames.CanAddMaterialNewVials);
  }

  get CanEditMaterialShipmentInformation(): boolean {
    return hasPermission(PermissionNames.CanEditMaterialShipmentInformation);
  }

  get prependIcon(): string {
    return "";
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

  get ShipmentTemperatureString(): string {
    if (
      this.model.ShipmentTemperature == null ||
      this.model.ShipmentTemperature == undefined
    ) {
      return "";
    }
    var unitOfMeasureInfo =
      TemperatureUnitOfMeasureModule.TemperatureUnitOfMeasures.filter(
        (tuom) => {
          return tuom.Id == this.model.ShipmentUnitOfMeasureId;
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

    return (
      this.model.ShipmentTemperature + unitOfMeasureInfo[0].unitOfMeasureName
    );
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

  get CountryProviderString(): string {
    if (this.readOnlySpecificCondition == true) {
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

    return "";
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

  get IsProviderBioHubFacility(): boolean {
    return (
      this.model.ProviderBioHubFacilityId != undefined &&
      this.model.ProviderBioHubFacilityId != null &&
      this.model.ProviderBioHubFacilityId != ""
    );
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
    } else if (
      itemElement[0] != undefined &&
      itemElement[0].Type == "BioHubFacility"
    ) {
      this.model.ProviderBioHubFacilityId = itemElement[0].Id;
      this.model.ProviderLaboratoryId = null;
    }

    this.$emit("update", this.model);
  }

  get genders(): Array<DropdownItem> {
    const gendersList = new Array<DropdownItem>();
    gendersList.push({ Text: "Male", Value: Gender.Male });
    gendersList.push({ Text: "Female", Value: Gender.Female });
    gendersList.push({ Text: "Undisclosed", Value: Gender.Undisclosed });
    return gendersList;
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

  get readOnlySpecificCondition(): boolean {
    if (this.readonly || this.isLaboratoryBmeppPage) {
      return true;
    }

    return false;
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

  get providersItems(): Array<any> {
    const Laboratories = LaboratoryModule.Laboratories;
    const BioHubFacilities = BioHubFacilityModule.BioHubFacilities;
    var providersItems = new Array<any>();

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

    if (this.isLaboratoryBmeppPage == true) {
      providersItems = providersItems.filter((i) => {
        return i.Id == this.providerId;
      });
      if (providersItems.length > 0 && providersItems[0].Type == "Laboratory") {
        this.model.ProviderLaboratoryId = providersItems[0].Id;
        this.model.ProviderBioHubFacilityId = null;
      }
    }

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
    MaterialModule.ADD_TEMPORARY_MATERIAL_GSD_INFO();
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

  get MaterialGSDInfo() {
    return MaterialModule.MaterialGSDInfo;
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
    MaterialModule.REMOVE_MATERIAL_GSD_INFO(id);
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
