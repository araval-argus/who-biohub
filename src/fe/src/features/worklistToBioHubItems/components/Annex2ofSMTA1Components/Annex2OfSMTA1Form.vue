<template>
  <v-form v-if="model" ref="form" lazy-validation :readonly="readonly">
    <Annex2OfSMTA1DisclaimerGeneralPart
      :bio-hub-facility-name="model.BioHubFacilityName"
      :bio-hub-facility-address="model.BioHubFacilityAddress"
      :bio-hub-facility-country="model.BioHubFacilityCountry"
    ></Annex2OfSMTA1DisclaimerGeneralPart>
    <div class="annex2-smta1-form">
      <h2>II. Voluntary shipment of BMEPP into a WHO BioHub Facility</h2>
      <h3>A) Information about the Provider Member State</h3>
      <p>Name of the Member State: {{ model.LaboratoryCountry }}</p>
      <LaboratoryFocalPointForm
        :can-edit="canEdit"
        :laboratory-focal-points="LaboratoryFocalPoints"
        :all-laboratory-users="AllLaboratoryUsers"
        @addLaboratoryFocalPoint="addLaboratoryFocalPoint"
        @removeLaboratoryFocalPoint="removeLaboratoryFocalPoint"
        @updateLaboratoryFocalPoint="updateLaboratoryFocalPoint"
      >
      </LaboratoryFocalPointForm>
    </div>
    <v-spacer></v-spacer>
    <div>
      <!-- <Checkbox
        readonly
        :model="true"
        label="The Provider has already signed an SMTA1 with a WHO and a WHO BioHub
        Facility"
      ></Checkbox> -->
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-container class="px-0" fluid>
          <h3>
            B) Additional Information about the Provider Member State relations
            with the WHO BioHub System
          </h3>
          <p>
            The Provider understands that the BMEPP are covered by the terms and
            conditions set out in the SMTA 1
          </p>
          <v-radio-group v-model="model.Annex2TermsAndConditions">
            <v-radio :key="0" label="Yes" :value="true"></v-radio>
            <v-radio :key="1" label="No" :value="false"></v-radio>
          </v-radio-group>
        </v-container>
      </v-card-actions>
      <text-area
        v-model="model.Annex2Comment"
        label="Comments"
        :readonly="!canEdit"
        property-name="Annex2Comment"
        :properties-errors="allPropertiesErrors"
        @input="update"
      >
      </text-area>
    </div>
    <v-spacer></v-spacer>
    <h3>
      C) Information about the BMEPP shared in this shipment (This document is
      only for SARS-CoV-2 as this will be the sole type of BMEPP shared during
      the Pilot Testing Phase)
    </h3>
    <p>
      By clicking "New" you can add to the shipment request the type of
      materials that you would like to share like Clinical specimen or Cultured
      isolate.
    </p>
    <v-card-text v-if="ShippingInformationWarningVisible">
      <h4 style="color: red">Please add at least a Type Of Material</h4>
    </v-card-text>
    <CardActionsGenericButton
      v-if="!newShippingInformationClicked"
      color="primary"
      text="New"
      @click="newShippingInformationClick"
    >
    </CardActionsGenericButton>
    <br />
    <div v-if="newShippingInformationClicked">
      <h4>C.1) SARS-CoV-2 BMEPP shipping information</h4>
      <ShippingInformationForm
        v-model="temporaryShippingInformation"
        :can-edit="canEdit"
        @updateMaterialClinicalDetail="updateMaterialClinicalDetail"
        @updateMaterialLaboratoryAnalysisInformation="
          updateMaterialLaboratoryAnalysisInformation
        "
      >
      </ShippingInformationForm>

      <v-container class="px-0" fluid>
        <CardActionsGenericButton
          style="display: inline-block; float: right"
          color="primary"
          text="Cancel"
          @click="cancel"
        >
        </CardActionsGenericButton>

        <CardActionsGenericButton
          style="display: inline-block; float: right"
          v-if="addVisible"
          text="Add"
          @click="add"
        >
        </CardActionsGenericButton>

        <v-spacer></v-spacer>
      </v-container>
    </div>
    <h4>C.1) BMEPP shipping information</h4>
    <ShippingInformationsTable
      :can-edit="canEdit"
      :can-read="canRead"
      :material-shipping-informations="MaterialShippingInformations"
      @removeMaterialShippingInformation="removeMaterialShippingInformation"
    >
    </ShippingInformationsTable>
    <h4>C.2) SARS-CoV-2 BMEPP laboratory analysis information</h4>
    <MaterialLaboratoryAnalysisInformationTable
      :material-laboratory-analysis-informations="
        MaterialLaboratoryAnalysisInformations
      "
    >
    </MaterialLaboratoryAnalysisInformationTable>
    <h4>C.3) BMEPP clinical details</h4>
    <ClinicalDetailsTable :material-clinical-details="MaterialClinicalDetails">
    </ClinicalDetailsTable>

    <!-- <dropdown
      v-if="bioHubFacilitySelectionVisible"
      v-model="model.BioHubFacilityId"
      :items="BioHubFacilityList"
      item-text="Text"
      item-value="Value"
      label="Select BioHub Facility"
      :readonly="!canEdit"
      property-name="BioHubFacilityId"
      @change="update"
    ></dropdown> -->

    <Annex2OfSMTA1DisclaimerLastPart
      :provider="model.LaboratoryName"
    ></Annex2OfSMTA1DisclaimerLastPart>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";

import { WorklistToBioHubItemModule } from "../../store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { MaterialProductGridItem } from "@/models/MaterialProductGridItem";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import ShippingInformationForm from "./ShippingInformationForm.vue";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import ShippingInformationsTable from "./ShippingInformationsTable.vue";
import ClinicalDetailsTable from "./ClinicalDetailsTable.vue";
import MaterialLaboratoryAnalysisInformationTable from "./MaterialLaboratoryAnalysisInformationTable.vue";
import LaboratoryFocalPointForm from "../../../worklistItemsCommonComponents/Annex2ofSMTAComponents/LaboratoryFocalPointForm.vue";
import Checkbox from "@/components/Checkbox.vue";
import Annex2OfSMTA1DisclaimerGeneralPart from "./Annex2OfSMTA1DisclaimerGeneralPart.vue";
import Annex2OfSMTA1DisclaimerLastPart from "./Annex2OfSMTA1DisclaimerLastPart.vue";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import { BioHubFacilityModule } from "../../../biohubfacilities/store";
import { DropdownItem } from "@/models/DropdownItem";
import { SeedData } from "@/models/constants/SeedData";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { UserModule } from "../../../users/store";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { MaterialLaboratoryAnalysisInformation } from "@/models/MaterialLaboratoryAnalysisInformation";

@Component({
  components: {
    CardActionsGenericButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    ShippingInformationForm,
    ShippingInformationsTable,
    ClinicalDetailsTable,
    MaterialLaboratoryAnalysisInformationTable,
    LaboratoryFocalPointForm,
    Checkbox,
    TextArea,
    Annex2OfSMTA1DisclaimerGeneralPart,
    Annex2OfSMTA1DisclaimerLastPart,
  },
})
export default class Annex2OfSMTA1Form extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private newShippingInformationClicked = false;
  private addVisible = false;
  private Annex2TermsAndConditionsSelection = "1";
  // Props

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  $refs!: {
    form: any;
  };

  // Model
  @Model("update", { type: Object }) model!: WorklistToBioHubItem;

  get AllLaboratoryUsers(): Array<WorklistItemUser> {
    return UserModule.WorklistToBioHubItemAllLaboratoryUsers;
  }

  get LaboratoryFocalPoints(): Array<WorklistItemUser> {
    return WorklistToBioHubItemModule.LaboratoryFocalPoints;
  }

  get MaterialShippingInformations(): Array<MaterialShippingInformation> {
    return WorklistToBioHubItemModule.MaterialShippingInformations;
  }

  get MaterialClinicalDetails(): Array<MaterialClinicalDetail> {
    const clinicalDetails = new Array<MaterialClinicalDetail>();

    WorklistToBioHubItemModule.MaterialShippingInformations.forEach((si) => {
      si.MaterialClinicalDetails.forEach((cd) => {
        clinicalDetails.push(cd);
      });
    });

    return clinicalDetails;
  }

  get MaterialLaboratoryAnalysisInformations(): Array<MaterialLaboratoryAnalysisInformation> {
    const materialLaboratoryAnalysisInformations =
      new Array<MaterialLaboratoryAnalysisInformation>();

    WorklistToBioHubItemModule.MaterialShippingInformations.forEach((si) => {
      si.MaterialLaboratoryAnalysisInformation.forEach((la) => {
        materialLaboratoryAnalysisInformations.push(la);
      });
    });

    return materialLaboratoryAnalysisInformations;
  }

  get bioHubFacilitySelectionVisible(): boolean {
    return (
      WorklistToBioHubItemModule.Status ==
      WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval
    );
  }

  get BioHubFacilityList(): Array<DropdownItem> {
    const bioHubFacilities = BioHubFacilityModule.BioHubFacilities;
    if (!bioHubFacilities) return new Array<DropdownItem>();

    return bioHubFacilities.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get temporaryShippingInformation(): MaterialShippingInformation | undefined {
    return WorklistToBioHubItemModule.TemporaryMaterialShippingInformation;
  }

  set temporaryShippingInformation(
    item: MaterialShippingInformation | undefined
  ) {
    WorklistToBioHubItemModule.SET_TEMPORARY_MATERIAL_SHIPPING_INFORMATION(
      item
    );
    this.addVisible = this.setAddVisible();
  }

  get ShippingInformations(): Array<MaterialShippingInformation> | undefined {
    return this.model.MaterialShippingInformations;
  }

  get ShippingInformationWarningVisible(): boolean {
    if (this.model.MaterialShippingInformations !== undefined) {
      return this.model.MaterialShippingInformations.length == 0;
    } else {
      return true;
    }
  }

  addLaboratoryFocalPoint(userId: string) {
    const totalUsers = UserModule.WorklistToBioHubItemAllLaboratoryUsers;
    const userToAdd = totalUsers.find((x) => x.UserId == userId);
    if (userToAdd !== undefined) {
      WorklistToBioHubItemModule.ADD_LABORATORY_FOCAL_POINT(userToAdd);
    }
  }

  removeLaboratoryFocalPoint(id: string) {
    WorklistToBioHubItemModule.REMOVE_LABORATORY_FOCAL_POINT(id);
  }

  updateLaboratoryFocalPoint(user: WorklistItemUser) {
    WorklistToBioHubItemModule.UPDATE_OTHER_FIELD_LABORATORY_FOCAL_POINT(user);
  }

  updateMaterialClinicalDetail() {
    this.addVisible = this.setAddVisible();
  }

  updateMaterialLaboratoryAnalysisInformation() {
    this.addVisible = this.setAddVisible();
  }

  removeMaterialShippingInformation(id: string) {
    WorklistToBioHubItemModule.REMOVE_MATERIAL_SHIPPING_INFORMATION(id);
  }

  setAddVisible(): boolean {
    if (this.newShippingInformationClicked === false) {
      return false;
    }
    if (this.temporaryShippingInformation === undefined) {
      return false;
    }

    if (
      this.temporaryShippingInformation.MaterialProductId === undefined ||
      this.temporaryShippingInformation.MaterialProductId === ""
    ) {
      return false;
    }

    if (
      this.temporaryShippingInformation.Quantity === undefined ||
      this.temporaryShippingInformation.Quantity <= 0
    ) {
      return false;
    }

    if (
      this.temporaryShippingInformation.Amount === undefined ||
      this.temporaryShippingInformation.Amount <= 0
    ) {
      return false;
    }

    if (
      this.temporaryShippingInformation.Condition === undefined ||
      this.temporaryShippingInformation.Condition === ""
    ) {
      return false;
    }

    let visible = true;
    if (
      this.temporaryShippingInformation.MaterialClinicalDetails.length === 0
    ) {
      return false;
    }

    if (
      this.temporaryShippingInformation.MaterialLaboratoryAnalysisInformation
        .length === 0
    ) {
      return false;
    }

    this.temporaryShippingInformation.MaterialClinicalDetails.forEach(
      (element) => {
        if (
          element.MaterialNumber === undefined ||
          element.MaterialNumber === ""
        ) {
          visible = false;
        }
        if (element.CollectionDate === undefined) {
          visible = false;
        }
        if (element.Location === undefined || element.Location === "") {
          visible = false;
        }
        if (
          element.IsolationHostTypeId === undefined ||
          element.IsolationHostTypeId === ""
        ) {
          visible = false;
        }
        if (element.Gender === undefined) {
          visible = false;
        }
        if (element.Age === undefined || element.Age < 0) {
          visible = false;
        }
        if (
          element.PatientStatus === undefined ||
          element.PatientStatus === ""
        ) {
          visible = false;
        }
      }
    );

    this.temporaryShippingInformation.MaterialLaboratoryAnalysisInformation.forEach(
      (element) => {
        if (
          element.MaterialNumber === undefined ||
          element.MaterialNumber === ""
        ) {
          visible = false;
        }
        if (
          element.FreezingDate === undefined ||
          element.FreezingDate === null
        ) {
          visible = false;
        }

        if (element.Temperature === undefined || element.Temperature === null) {
          visible = false;
        }

        if (
          element.UnitOfMeasureId === undefined ||
          element.UnitOfMeasureId === ""
        ) {
          visible = false;
        }

        if (
          element.VirusConcentration === undefined ||
          element.VirusConcentration === ""
        ) {
          visible = false;
        }

        if (
          this.temporaryShippingInformation != undefined &&
          this.temporaryShippingInformation.MaterialProductId ==
            SeedData.CulturedIsolateProductId
        ) {
          if (
            element.CulturingCellLine === undefined ||
            element.CulturingCellLine === ""
          ) {
            visible = false;
          }

          if (
            element.CulturingPassagesNumber === undefined ||
            element.CulturingPassagesNumber <= 0
          ) {
            visible = false;
          }
        } else if (
          this.temporaryShippingInformation != undefined &&
          this.temporaryShippingInformation.MaterialProductId ==
            SeedData.ClinicalSpecimenProductId
        ) {
          if (element.CollectedSpecimenTypes.length == 0) {
            visible = false;
          }

          if (
            element.TypeOfTransportMedium === undefined ||
            element.TypeOfTransportMedium == ""
          ) {
            visible = false;
          }

          if (
            element.BrandOfTransportMedium === undefined ||
            element.BrandOfTransportMedium == ""
          ) {
            visible = false;
          }
        }

        if (
          element.GSDUploadedToDatabase === undefined ||
          element.GSDUploadedToDatabase === null
        ) {
          visible = false;
        }

        if (element.GSDUploadedToDatabase == YesNoOption.Yes) {
          if (
            element.DatabaseUsedForGSDUploadingId === undefined ||
            element.DatabaseUsedForGSDUploadingId == ""
          ) {
            visible = false;
          }

          if (
            element.AccessionNumberInGSDDatabase === undefined ||
            element.AccessionNumberInGSDDatabase == ""
          ) {
            visible = false;
          }
        }
      }
    );

    return visible;
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }

  newShippingInformationClick(): void {
    WorklistToBioHubItemModule.SET_NEW_TEMPORARY_MATERIAL_SHIPPING_INFORMATION();
    this.newShippingInformationClicked = true;
    this.addVisible = this.setAddVisible();
  }

  updateShippingInformation(): void {
    this.newShippingInformationClicked = false;
    this.addVisible = this.setAddVisible();
  }

  add(): void {
    WorklistToBioHubItemModule.ADD_TEMPORARY_MATERIAL_SHIPPING_INFORMATION();
    this.newShippingInformationClicked = false;
    this.addVisible = this.setAddVisible();
  }

  cancel(): void {
    WorklistToBioHubItemModule.CLEAR_TEMPORARY_MATERIAL_SHIPPING_INFORMATION();
    this.newShippingInformationClicked = false;
    this.addVisible = this.setAddVisible();
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
