<template>
  <v-card>
    <v-card-text
      v-if="IsWaitForArrivalConditionCheckCompleted == false"
      style="color: red"
      >Please set Condition for every material</v-card-text
    >
    <v-card-text>
      <ShipmentMaterialsTable :can-edit="canEdit" @selected="selected">
      </ShipmentMaterialsTable>
    </v-card-text>

    <ShipmentMaterialsForm
      v-for="i in materialItems.length"
      v-show="isVisible(i - 1)"
      :key="i - 1"
      v-model="materialItems[i - 1]"
      :index="i - 1"
      :is-current="isVisible(i - 1)"
      :can-edit="canEdit"
      :save-visible="saveVisible"
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
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import { WorklistFromBioHubItemMaterialGridItem } from "@/models/WorklistFromBioHubItemMaterialGridItem";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { IsolationHostTypeGridItem } from "@/models/IsolationHostTypeGridItem";
import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import ShipmentMaterialsTable from "./ShipmentMaterialsTable.vue";
import ShipmentMaterialsForm from "./ShipmentMaterialsForm.vue";
import { WorklistFromBioHubItemModule } from "../../store";
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

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: WorklistFromBioHubItemMaterial;

  isVisible(index: number) {
    if (index == this.indexVisible) {
      return true;
    }
    return false;
  }

  get IsWaitForArrivalConditionCheckCompleted(): boolean {
    const materials =
      WorklistFromBioHubItemModule.WorklistFromBioHubItemMaterials;

    let materialFormCompleted = true;
    materials.forEach((elem) => {
      if (elem.Condition === undefined || elem.Condition === null) {
        materialFormCompleted = false;
      }
    });

    return materialFormCompleted;
  }

  get materialItems(): Array<WorklistFromBioHubItemMaterial> {
    let materials =
      WorklistFromBioHubItemModule.WorklistFromBioHubItemMaterials;
    if (!materials) return new Array<WorklistFromBioHubItemMaterial>();

    materials = materials.sort(this.sortByMaterialNumber);
    return materials;
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
    const index = this.materialItems
      .map(function (e) {
        return e.Id;
      })
      .indexOf(id);

    let elementToUpdate = this.materialItems[index];
    elementToUpdate.Note = note;
    elementToUpdate.Condition = condition;
    WorklistFromBioHubItemModule.UPDATE_MATERIAL(elementToUpdate);
    this.indexVisible = -1;
  }

  selected(item: WorklistFromBioHubItemMaterialGridItem): void {
    this.indexVisible = this.materialItems
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
