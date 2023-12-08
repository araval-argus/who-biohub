<template>
  <v-card>
    <v-card-text
      v-if="IsWaitForArrivalConditionCheckCompleted == false"
      style="color: red"
      >Please set Condition for every material</v-card-text
    >
    <v-card-text>
      <ShipmentMaterialsTable
        :can-edit="canEdit"
        :only-damaged="onlyDamaged"
        @selected="selected"
      >
      </ShipmentMaterialsTable>
    </v-card-text>

    <ShipmentMaterialsForm
      v-for="i in clinicalDetailItems.length"
      v-show="isVisible(i - 1)"
      :key="i - 1"
      v-model="clinicalDetailItems[i - 1]"
      :index="i - 1"
      :is-current="isVisible(i - 1)"
      :can-edit="canEdit"
      :save-visible="saveVisible"
      :shipping-informations="MaterialShippingInformations"
      @updateClinicalDetailForm="updateClinicalDetailForm"
      @save="save"
    >
    </ShipmentMaterialsForm>

    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import { WaitForArrivalConditionCheckGridItem } from "@/models/WaitForArrivalConditionCheckGridItem";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import ShipmentMaterialsTable from "./ShipmentMaterialsTable.vue";
import ShipmentMaterialsForm from "./ShipmentMaterialsForm.vue";
import { WorklistToBioHubItemModule } from "../../store";
import CardActionsGenericButton from "@/components/CardActionsGenericButton.vue";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    DatePicker,
    ShipmentMaterialsTable,
    ShipmentMaterialsForm,
    CardActionsGenericButton,
  },
})
export default class ShipmentMaterialsSection extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props

  private propertiesErrors = new Map<string, Array<string>>();

  private indexVisible = -1;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: true })
  readonly saveVisible: boolean;

  @Prop({ type: Boolean, default: false })
  readonly onlyDamaged: boolean;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: MaterialClinicalDetail;

  isVisible(index: number) {
    if (index == this.indexVisible) {
      return true;
    }
    return false;
  }

  get MaterialShippingInformations(): Array<MaterialShippingInformation> {
    if (!WorklistToBioHubItemModule.MaterialShippingInformations) {
      return new Array<MaterialShippingInformation>();
    }
    return WorklistToBioHubItemModule.MaterialShippingInformations;
  }

  get IsWaitForArrivalConditionCheckCompleted(): boolean {
    const shippingInformations =
      WorklistToBioHubItemModule.MaterialShippingInformations;

    let clinicalDetails = new Array<MaterialClinicalDetail>();

    shippingInformations.forEach((si) => {
      si.MaterialClinicalDetails.forEach((cd) => {
        if (this.onlyDamaged) {
          if (cd.Condition == ShipmentMaterialCondition.Damaged) {
            clinicalDetails.push(cd);
          }
        } else {
          clinicalDetails.push(cd);
        }
      });
    });

    let materialShippingFormCompleted = true;
    clinicalDetails.forEach((elem) => {
      if (elem.Condition === undefined || elem.Condition === null) {
        materialShippingFormCompleted = false;
      }
    });

    return materialShippingFormCompleted;
  }

  get clinicalDetailItems(): Array<MaterialClinicalDetail> {
    let clinicalDetails = new Array<MaterialClinicalDetail>();

    const shippingInformations =
      WorklistToBioHubItemModule.MaterialShippingInformations;
    if (!shippingInformations) return clinicalDetails;

    shippingInformations.forEach((si) => {
      si.MaterialClinicalDetails.forEach((cd) => {
        if (this.onlyDamaged) {
          if (cd.Condition == ShipmentMaterialCondition.Damaged) {
            clinicalDetails.push(cd);
          }
        } else {
          clinicalDetails.push(cd);
        }
      });
    });

    clinicalDetails = clinicalDetails.sort(this.sortByMaterialNumber);
    return clinicalDetails;
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

  // Events
  save(note: string, condition: ShipmentMaterialCondition, id: string) {
    const index = this.clinicalDetailItems
      .map(function (e) {
        return e.Id;
      })
      .indexOf(id);

    let elementToUpdate = this.clinicalDetailItems[index];
    elementToUpdate.Note = note;
    elementToUpdate.Condition = condition;
    WorklistToBioHubItemModule.UPDATE_MATERIAL_CLINICAL_DETAIL(elementToUpdate);
    this.indexVisible = -1;
  }

  selected(item: WaitForArrivalConditionCheckGridItem): void {
    this.indexVisible = this.clinicalDetailItems
      .map(function (e) {
        return e.Id;
      })
      .indexOf(item.Id);
  }

  validate() {
    this.$refs.form.validate();
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
