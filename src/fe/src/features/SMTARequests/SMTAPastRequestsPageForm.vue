<template>
  <div v-if="canCreate">
    <h2>Create Past SMTA Request</h2>
    <div>
      <v-container class="px-0" fluid>
        <v-radio-group v-model="smtaRequestSelection">
          <v-radio key="1" label="SMTA 1" value="1"></v-radio>
          <v-radio key="2" label="SMTA 2" value="2"></v-radio>
        </v-radio-group>

        <date-picker
          v-model="assignedOperationDate"
          label="Assigned Operation Date"
          :readonly="false"
          property-name="AssignedOperationDate"
          :properties-errors="propertiesErrors"
          @input="update"
        >
        </date-picker>

        <CardActionsSaveCancel
          v-if="assignedOperationDate != null"
          save-name="Continue"
          @save="onSave"
          @cancel="onCancel"
        >
        </CardActionsSaveCancel>
      </v-container>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Model } from "vue-property-decorator";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { getAreaFromRoleType } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { SMTA1WorkflowItemModule } from "../SMTA1WorkflowItems/store";
import { SMTA2WorkflowItemModule } from "../SMTA2WorkflowItems/store";
import { AuthModule } from "../auth/store";
import { DocumentModule } from "../documents/store";
import DatePicker from "../../components/DatePicker.vue";

@Component({
  components: { CardActionsSaveCancel, DatePicker },
})
export default class SMTAPastRequestsPageForm extends Vue {
  private smtaRequestSelection = "1";
  private openSaveOrDelete = false;
  private assignedOperationDate = null;

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanAccessPastRequestIniziation);
  }

  get LaboratoryId(): string {
    return AuthModule.LaboratoryId ?? "";
  }

  get CanStartSMTA1Request(): boolean {
    return DocumentModule.CanStartSMTA1Request;
  }

  get CanStartSMTA2Request(): boolean {
    return DocumentModule.CanStartSMTA2Request;
  }

  get allDocumentsSigned(): boolean {
    return !this.CanStartSMTA1Request && !this.CanStartSMTA2Request;
  }

  updated() {
    AppModule.HideLoading();
  }

  private showLoading() {
    this.openSaveOrDelete = true;
    AppModule.ShowLoading();
  }

  private hideLoading() {
    this.openSaveOrDelete = false;

    AppModule.HideLoading();
  }

  get propertiesErrors(): any {
    if (this.smtaRequestSelection == "1") {
      return SMTA1WorkflowItemModule.ErrorMessage;
    } else {
      return SMTA2WorkflowItemModule.ErrorMessage;
    }
  }

  async onSave(): Promise<void> {
    if (this.smtaRequestSelection == "1") {
      SMTA1WorkflowItemModule.CLEAR_SMTA1WORKFLOW_CREATE();

      SMTA1WorkflowItemModule.SET_SMTA1_CREATE_ISPAST(true);
      SMTA1WorkflowItemModule.SET_SMTA1_CREATE_ASSIGNED_OPERATION_DATE(
        this.assignedOperationDate
      );

      this.showLoading();

      await SMTA1WorkflowItemModule.CreateSMTA1WorkflowItem()
        .then((response) => {
          this.hideLoading();
          const area = getAreaFromRoleType();
          this.$router.push({
            name: area + "-smta-requests",
          });
        })
        .catch((err) => {
          console.log(err);
        });
    } else {
      SMTA2WorkflowItemModule.CLEAR_SMTA2WORKFLOW_CREATE();

      SMTA2WorkflowItemModule.SET_SMTA2_CREATE_ISPAST(true);
      SMTA2WorkflowItemModule.SET_SMTA2_CREATE_ASSIGNED_OPERATION_DATE(
        this.assignedOperationDate
      );

      this.showLoading();
      await SMTA2WorkflowItemModule.CreateSMTA2WorkflowItem()
        .then((response) => {
          this.hideLoading();
          const area = getAreaFromRoleType();
          this.$router.push({
            name: area + "-smta-requests",
          });
        })
        .catch((err) => {
          console.log(err);
        });
    }
  }

  onCancel(): void {
    const area = getAreaFromRoleType();
    this.$router.push({
      name: area + "-smta-requests",
    });
  }
}
</script>
