<template>
  <div v-if="canCreate">
    <h2>New SMTA Request</h2>
    <div v-if="!allDocumentsSigned">
      <v-container class="px-0" fluid>
        <v-radio-group v-model="smtaRequestSelection">
          <v-radio
            v-if="CanStartSMTA1Request"
            key="1"
            label="SMTA 1"
            value="1"
          ></v-radio>
          <v-radio
            v-if="CanStartSMTA2Request"
            key="2"
            label="SMTA 2"
            value="2"
          ></v-radio>
        </v-radio-group>
        <CardActionsSaveCancel
          save-name="Continue"
          @save="onSave"
          @cancel="onCancel"
        >
        </CardActionsSaveCancel>
      </v-container>
    </div>
    <div class="smta-message message-alert" v-else>
      <p>No New SMTA requests to be started</p>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { getAreaFromRoleType } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { SMTA1WorkflowItemModule } from "../SMTA1WorkflowItems/store";
import { SMTA2WorkflowItemModule } from "../SMTA2WorkflowItems/store";
import { AuthModule } from "../auth/store";
import { DocumentModule } from "../documents/store";

@Component({
  components: { CardActionsSaveCancel },
})
export default class SMTARequestsPageForm extends Vue {
  private smtaRequestSelection = "1";
  private openSaveOrDelete = false;

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanAccessRequestIniziation);
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

  async onSave(): Promise<void> {
    if (
      !this.CanStartSMTA1Request == true &&
      this.smtaRequestSelection == "1"
    ) {
      return;
    }
    if (
      !this.CanStartSMTA2Request == true &&
      this.smtaRequestSelection == "2"
    ) {
      return;
    }
    if (this.smtaRequestSelection == "1") {
      SMTA1WorkflowItemModule.CLEAR_SMTA1WORKFLOW_CREATE();
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

  async mounted() {
    await DocumentModule.CanStartSMTA1RequestCheck();
    await DocumentModule.CanStartSMTA2RequestCheck();
  }
}
</script>
