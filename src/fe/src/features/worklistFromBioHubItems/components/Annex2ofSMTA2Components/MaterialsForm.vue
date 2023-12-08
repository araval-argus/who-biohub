<template>
  <v-card class="mb-5">
    <v-card-text>
      <dropdown
        v-model="materialId"
        :items="eligibleMaterialList"
        item-text="MaterialName"
        item-value="MaterialId"
        label="BMEPP Name"
        :readonly="!canEdit"
        property-name="MaterialId"
        @change="update"
      ></dropdown>

      <CardActionsGenericButton
        v-if="materialId !== '' && canEdit"
        text="Add"
        @click="addMaterial"
      >
      </CardActionsGenericButton>
    </v-card-text>
    <v-card-text v-if="MaterialWarningVisible">
      <h4 style="color: red">Please add at least a Material</h4>
    </v-card-text>
    <v-card-text>
      <MaterialsTable
        :can-edit="canEdit"
        :can-delete="canEdit"
        :hide-amount="false"
        :materials="materials"
        @updateMaterial="updateMaterial"
        @deleteMaterial="deleteMaterial"
      ></MaterialsTable>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import MaterialsTable from "./MaterialsTable.vue";

@Component({
  components: {
    CardActionsGenericButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    TextArea,
    MaterialsTable,
  },
})
export default class MaterialsForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private materialId = "";
  // Props

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Array, default: [] })
  readonly materials: Array<WorklistFromBioHubItemMaterial>;

  @Prop({ type: Array, default: [] })
  readonly allMaterials: Array<WorklistFromBioHubItemMaterial>;

  $refs!: {
    form: any;
  };

  // Events
  addMaterial() {
    const totalMaterials = this.allMaterials;
    const materialToAdd = totalMaterials.find(
      (x) => x.MaterialId == this.materialId
    );
    if (materialToAdd !== undefined) {
      this.$emit("addMaterial", materialToAdd);
      //WorklistFromBioHubItemModule.ADD_MATERIAL(materialToAdd);
    }
    this.materialId = "";
  }

  updateMaterial(material: WorklistFromBioHubItemMaterial) {
    this.$emit("updateMaterial", material);
  }

  deleteMaterial(id: string) {
    this.$emit("deleteMaterial", id);
    //WorklistFromBioHubItemModule.REMOVE_MATERIAL(item.Id);
  }

  validate() {
    this.$refs.form.validate();
  }

  get MaterialWarningVisible(): boolean {
    return this.materials.length == 0;
  }

  get eligibleMaterialList(): Array<WorklistFromBioHubItemMaterial> {
    const totalMaterials = this.allMaterials;

    if (!totalMaterials) return new Array<WorklistFromBioHubItemMaterial>();

    const selectedMaterials = this.materials;

    let filteredMaterials = new Array<WorklistFromBioHubItemMaterial>();
    totalMaterials.forEach((item) => {
      let found = false;
      selectedMaterials.forEach((elem) => {
        if (elem.MaterialId == item.MaterialId) {
          found = true;
        }
      });
      if (found == false) {
        filteredMaterials.push(item);
      }
    });

    return filteredMaterials.map((l) => {
      return {
        Id: l.Id,
        WorklistFromBioHubItemId: l.WorklistFromBioHubItemId,
        MaterialId: l.MaterialId,
        Quantity: l.Quantity,
        AvailableQuantity: l.AvailableQuantity,
        Amount: l.Amount,
        MaterialNumber: l.MaterialNumber,
        MaterialProductId: l.MaterialProductId,
        TransportCategoryId: l.TransportCategoryId,
        MaterialName: l.MaterialName,
        CollectionDate: l.CollectionDate,
        Location: l.Location,
        IsolationHostTypeId: l.IsolationHostTypeId,
        Gender: l.Gender,
        Age: l.Age,
        Condition: l.Condition,
        Note: l.Note,
        Status: l.Status,
      };
    });
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
