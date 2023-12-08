<template>
  <v-form
    v-if="model"
    ref="form"
    lazy-validation
    :readonly="readonly"
    class="ma-2"
  >
    <Annex2OfSMTA2DisclaimerGeneralPart
      :bio-hub-facility-name="model.BioHubFacilityName"
      :bio-hub-facility-address="model.BioHubFacilityAddress"
      :bio-hub-facility-country="model.BioHubFacilityCountry"
    ></Annex2OfSMTA2DisclaimerGeneralPart>
    <div class="annex2-smta2-form">
      <h2>II. Qualified Entity requesting BMEPP from a WHO BioHub Facility</h2>
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
    <h2>III. Information about the BMEPP requested in this shipment</h2>

    <MaterialsForm
      :can-edit="canEdit"
      :materials="Materials"
      :all-materials="AllMaterials"
      @addMaterial="addMaterial"
      @updateMaterial="updateMaterial"
      @deleteMaterial="deleteMaterial"
    >
    </MaterialsForm>
    <v-spacer></v-spacer>

    <v-spacer></v-spacer>
    <h2>
      IV. Conditions for shipment of BMEPP by a WHO BioHub Facility to a
      Qualified Entity
    </h2>
    <ConditionsForm
      :can-edit="canEdit"
      :conditions="Conditions"
      @updateAnnex2OfSMTA2Condition="updateAnnex2OfSMTA2Condition"
    >
    </ConditionsForm>

    <Annex2OfSMTA2DisclaimerLastPart
      :provider="model.LaboratoryName"
    ></Annex2OfSMTA2DisclaimerLastPart>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import MaterialsForm from "./MaterialsForm.vue";
import ConditionsForm from "./ConditionsForm.vue";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import LaboratoryFocalPointForm from "../../../worklistItemsCommonComponents/Annex2ofSMTAComponents/LaboratoryFocalPointForm.vue";
import Checkbox from "@/components/Checkbox.vue";
import Annex2OfSMTA2DisclaimerGeneralPart from "./Annex2OfSMTA2DisclaimerGeneralPart.vue";
import Annex2OfSMTA2DisclaimerLastPart from "./Annex2OfSMTA2DisclaimerLastPart.vue";
import { UserModule } from "../../../users/store";
import { WorklistFromBioHubItemModule } from "../../store";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { WorklistFromBioHubItemAnnex2OfSMTA2Condition } from "@/models/WorklistFromBioHubItemAnnex2OfSMTA2Condition";
import { MaterialModule } from "../../../materials/store";

@Component({
  components: {
    CardActionsGenericButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    MaterialsForm,
    LaboratoryFocalPointForm,
    Checkbox,
    TextArea,
    ConditionsForm,
    Annex2OfSMTA2DisclaimerGeneralPart,
    Annex2OfSMTA2DisclaimerLastPart,
  },
})
export default class Annex2OfSMTA2Form extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

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
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;

  get AllLaboratoryUsers(): Array<WorklistItemUser> {
    return UserModule.WorklistFromBioHubItemAllLaboratoryUsers;
  }

  get LaboratoryFocalPoints(): Array<WorklistItemUser> {
    return WorklistFromBioHubItemModule.LaboratoryFocalPoints;
  }

  get Conditions(): Array<WorklistFromBioHubItemAnnex2OfSMTA2Condition> {
    return WorklistFromBioHubItemModule.WorklistFromBioHubItemAnnex2OfSMTA2Conditions;
  }

  get Materials(): Array<WorklistFromBioHubItemMaterial> {
    return WorklistFromBioHubItemModule.WorklistFromBioHubItemMaterials;
  }

  get AllMaterials(): Array<WorklistFromBioHubItemMaterial> {
    return MaterialModule.WorklistFromBioHubItemAllMaterials;
  }

  get MaterialWarningVisible(): boolean {
    return this.Materials.length == 0;
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }

  updateAnnex2TermsAndConditions(option: boolean) {
    this.model.Annex2TermsAndConditions = option;
    this.$emit("update", this.model);
  }

  addMaterial(material: WorklistFromBioHubItemMaterial) {
    WorklistFromBioHubItemModule.ADD_MATERIAL(material);
  }

  updateMaterial(material: WorklistFromBioHubItemMaterial) {
    WorklistFromBioHubItemModule.UPDATE_MATERIAL(material);
  }

  removeMaterial(id: string) {
    WorklistFromBioHubItemModule.REMOVE_MATERIAL(id);
  }

  updateAnnex2OfSMTA2Condition(
    condition: WorklistFromBioHubItemAnnex2OfSMTA2Condition
  ) {
    WorklistFromBioHubItemModule.UPDATE_ANNEX2OFSMTA2CONDITION(condition);
  }

  addLaboratoryFocalPoint(userId: string) {
    const totalUsers = UserModule.WorklistFromBioHubItemAllLaboratoryUsers;
    const userToAdd = totalUsers.find((x) => x.UserId == userId);
    if (userToAdd !== undefined) {
      WorklistFromBioHubItemModule.ADD_LABORATORY_FOCAL_POINT(userToAdd);
    }
  }

  removeLaboratoryFocalPoint(id: string) {
    WorklistFromBioHubItemModule.REMOVE_LABORATORY_FOCAL_POINT(id);
  }

  updateLaboratoryFocalPoint(user: WorklistItemUser) {
    WorklistFromBioHubItemModule.UPDATE_OTHER_FIELD_LABORATORY_FOCAL_POINT(
      user
    );
  }

  mounted() {
    if (this.Annex2TermsAndConditionsSelection == "1") {
      this.updateAnnex2TermsAndConditions(true);
    } else {
      this.updateAnnex2TermsAndConditions(false);
    }
  }

  @Watch("Annex2TermsAndConditionsSelection")
  annex2FillingOption() {
    if (this.Annex2TermsAndConditionsSelection == "1") {
      this.updateAnnex2TermsAndConditions(true);
    } else {
      this.updateAnnex2TermsAndConditions(false);
    }
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
