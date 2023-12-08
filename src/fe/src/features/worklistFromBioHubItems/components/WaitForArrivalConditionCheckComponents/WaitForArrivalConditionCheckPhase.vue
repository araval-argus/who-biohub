<template>
  <div>
    <v-card v-if="WaitForArrivalConditionCheck" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <ShipmentMaterialsSection :can-edit="hasSubmitPermissionByStatus">
        </ShipmentMaterialsSection>

        <v-row v-if="model.IsPast == true">
          <v-col cols="12" sm="6" md="6"> </v-col>
          <v-col cols="12" sm="6" md="6">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </v-col>
        </v-row>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <v-radio-group v-model="model.LastSubmissionApproved">
              <v-radio :key="0" label="Approve" :value="true"></v-radio>
              <v-radio :key="1" label="Ask for review" :value="false"></v-radio>
            </v-radio-group>
          </v-container>
        </v-card-actions>
        <v-card-actions v-if="approvedSelected == true">
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <h4
              v-if="model.WaitForArrivalConditionCheckApprovalFlag != true"
              style="color: red"
            >
              Please confirm
            </h4>
            <Checkbox
              v-model="model.WaitForArrivalConditionCheckApprovalFlag"
              :readonly="!hasSubmitPermissionByStatus"
              label="I confirm that the BMEPP materials reported in the
table above are all in good condition and mark the
shipment process as completed."
            ></Checkbox>
            <text-area
              v-model="model.WaitForArrivalConditionCheckApprovalComment"
              label="Add a comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="WaitForArrivalConditionCheckApprovalComment"
              @input="update"
            ></text-area>
          </v-container>

          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions v-if="approvedSelected == false">
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <text-area
              v-model="model.NewFeedback"
              label="Comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="Comment"
              @input="update"
            ></text-area>
          </v-container>
          <v-spacer></v-spacer>
        </v-card-actions>
        <CardActionsGenericButton
          color="primary"
          style="display: inline-block; float: right"
          text="Save As Draft"
          @click="saveAsDraft"
        >
        </CardActionsGenericButton>
        <CardActionsGenericButton
          v-if="submitWaitForArrivalConditionCheckVisible"
          style="display: inline-block; float: right"
          text="Submit"
          @click="submit"
        >
        </CardActionsGenericButton>
      </v-card-text>
      <div v-if="model.IsPast == true">
        <br />
        <br />
      </div>
      <v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <template v-if="hasSubmitPermissionByStatus">
            <p
              v-if="
                CanSubmitBHFSMTA2ShipmentDocuments ||
                CanSubmitQESMTA2ShipmentDocuments
              "
            >
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >
            </p>
          </template>
          <template v-else>
            <p
              v-if="
                CanSubmitBHFSMTA2ShipmentDocuments ||
                CanSubmitQESMTA2ShipmentDocuments
              "
            >
              Although no actions should be taken from your side at this stage,
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >, which are fianlized in communication between you and the
              courier company. You can find templates for some of the documents
              here.
            </p>
            <p v-else>No action should be taken from your side at this stage</p>
          </template>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-text>
        <ShipmentDocumentsComponent
          v-model="model"
          @downloadFile="downloadFile"
        >
        </ShipmentDocumentsComponent>
      </v-card-text>
    </v-card>

    <v-card v-if="WaitForCommentQESendFeedback" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <ShipmentMaterialsSection :can-edit="false" :save-visible="false">
        </ShipmentMaterialsSection>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <FeedbackFlowComponent v-model="model.Feedbacks">
            </FeedbackFlowComponent>
            <text-area
              v-model="model.NewFeedback"
              label="Comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="NewFeedback"
              @input="update"
            ></text-area>
          </v-container>
          <v-spacer></v-spacer>
        </v-card-actions>
        <v-row v-if="model.IsPast == true">
          <v-col cols="12" sm="6" md="6"> </v-col>
          <v-col cols="12" sm="6" md="6">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </v-col>
        </v-row>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container
            v-if="
              model.NewFeedback !== '' &&
              model.NewFeedback !== undefined &&
              model.NewFeedback !== null &&
              (model.IsPast != true || model.AssignedOperationDate != null)
            "
            class="px-0"
            fluid
          >
            <CardActionsGenericButton text="Submit" @click="submit">
            </CardActionsGenericButton>
          </v-container>
        </v-card-actions>
      </v-card-text>
      <div v-if="model.IsPast == true">
        <br />
        <br />
      </div>
      <v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <template v-if="hasSubmitPermissionByStatus">
            <p
              v-if="
                CanSubmitBHFSMTA2ShipmentDocuments ||
                CanSubmitQESMTA2ShipmentDocuments
              "
            >
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >
            </p>
          </template>
          <template v-else>
            <p
              v-if="
                CanSubmitBHFSMTA2ShipmentDocuments ||
                CanSubmitQESMTA2ShipmentDocuments
              "
            >
              Although no actions should be taken from your side at this stage,
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >, which are fianlized in communication between you and the
              courier company. You can find templates for some of the documents
              here.
            </p>
            <p v-else>No action should be taken from your side at this stage</p>
          </template>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-text>
        <ShipmentDocumentsComponent
          v-model="model"
          @downloadFile="downloadFile"
        >
        </ShipmentDocumentsComponent>
      </v-card-text>
    </v-card>

    <v-card v-if="WaitForFinalApproval" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <ShipmentMaterialsSection :can-edit="hasSubmitPermissionByStatus">
        </ShipmentMaterialsSection>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <FeedbackFlowComponent v-model="model.Feedbacks">
            </FeedbackFlowComponent>
            <v-spacer></v-spacer>

            <text-area
              v-model="model.NewFeedback"
              label="Comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="NewFeedback"
              @input="update"
            ></text-area>
          </v-container>
          <v-spacer></v-spacer>
        </v-card-actions>

        <v-row v-if="model.IsPast == true">
          <v-col cols="12" sm="6" md="6"> </v-col>
          <v-col cols="12" sm="6" md="6">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </v-col>
        </v-row>

        <CardActionsGenericButton
          v-if="CompleteWaitForFinalApprovalVisible"
          text="Complete"
          style="display: inline-block; float: right"
          @click="submit"
        >
        </CardActionsGenericButton>

        <CardActionsGenericButton
          v-if="
            model.NewFeedback !== '' &&
            model.NewFeedback !== undefined &&
            model.NewFeedback !== null &&
            (model.IsPast != true || model.AssignedOperationDate != null)
          "
          text="Send Comment"
          style="display: inline-block; float: right"
          color="warning"
          @click="submitForNewFeedback"
        >
        </CardActionsGenericButton>
      </v-card-text>
      <div v-if="model.IsPast == true">
        <br />
        <br />
      </div>
      <v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <template v-if="hasSubmitPermissionByStatus">
            <p
              v-if="
                CanSubmitBHFSMTA2ShipmentDocuments ||
                CanSubmitQESMTA2ShipmentDocuments
              "
            >
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >
            </p>
          </template>
          <template v-else>
            <p
              v-if="
                CanSubmitBHFSMTA2ShipmentDocuments ||
                CanSubmitQESMTA2ShipmentDocuments
              "
            >
              Although no actions should be taken from your side at this stage,
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >, which are fianlized in communication between you and the
              courier company. You can find templates for some of the documents
              here.
            </p>
            <p v-else>No action should be taken from your side at this stage</p>
          </template>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-text>
        <ShipmentDocumentsComponent
          v-model="model"
          @downloadFile="downloadFile"
        >
        </ShipmentDocumentsComponent>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { WorklistFromBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import FormPopup from "../../../../components/FormPopup.vue";
import DownloadDocumentComponent from "./../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import ShipmentDocumentsForm from "./../ShipmentDocumentsComponents/ShipmentDocumentsForm.vue";
import Checkbox from "@/components/Checkbox.vue";
import ShipmentMaterialsSection from "./../WaitForArrivalConditionCheckComponents/ShipmentMaterialsSection.vue";
import FeedbackFlowComponent from "./../WaitForArrivalConditionCheckComponents/FeedbackFlowComponent.vue";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { TransportCategoryModule } from "../../../transportCategories/store";
import { PermissionNames } from "@/models/constants/PermissionNames";
import ShipmentDocumentsComponent from "./../ShipmentDocumentsComponents/ShipmentDocumentsComponent.vue";
import { PermissionType } from "@/models/enums/PermissionType";
import DatePicker from "@/components/DatePicker.vue";

@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    ShipmentDocumentsForm,
    Checkbox,
    ShipmentMaterialsSection,
    FeedbackFlowComponent,
    ShipmentDocumentsComponent,
    DatePicker,
  },
})
export default class WaitForArrivalConditionCheckPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get CanSubmitBHFSMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments);
    }
  }

  get CanSubmitQESMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanSubmitQESMTA2ShipmentDocuments);
    }
  }

  get WaitForArrivalConditionCheck(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentQESendFeedback(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForCommentQESendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.WaitForFinalApproval
    );
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.DownloadFile,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Read,
      this.model.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get approvedSelected(): boolean {
    return this.model.LastSubmissionApproved;
  }

  get submitWaitForArrivalConditionCheckVisible(): boolean {
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (this.approvedSelected == false && this.model.NewFeedback != "") {
      return true;
    }

    if (
      this.approvedSelected == true &&
      this.IsWaitForArrivalConditionCheckCompleted == true &&
      this.model.WaitForArrivalConditionCheckApprovalFlag == true
    ) {
      return true;
    }
    return false;
  }

  get CompleteWaitForFinalApprovalVisible(): boolean {
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (this.IsWaitForArrivalConditionCheckCompleted == true) {
      return true;
    }
    return false;
  }

  get IsWaitForArrivalConditionCheckCompleted(): boolean {
    if (
      this.model.WorklistFromBioHubItemMaterials === undefined ||
      this.model.WorklistFromBioHubItemMaterials.length == 0
    ) {
      return false;
    }

    let materialFormCompleted = true;
    this.model.WorklistFromBioHubItemMaterials.forEach((elem) => {
      if (elem.Condition === undefined || elem.Condition === null) {
        materialFormCompleted = false;
      }
    });

    return materialFormCompleted;
  }

  get completeWaitForFinalApprovalVisible(): boolean {
    if (
      this.model.NewFeedback != "" &&
      this.model.NewFeedback != undefined &&
      this.model.NewFeedback != null
    ) {
      return false;
    }

    if (this.IsWaitForFinalApprovalCompleted == true) {
      return true;
    }
    return false;
  }

  get IsWaitForFinalApprovalCompleted(): boolean {
    if (
      this.model.WorklistFromBioHubItemMaterials === undefined ||
      this.model.WorklistFromBioHubItemMaterials.length == 0
    ) {
      return false;
    }

    let materialFormCompleted = true;
    this.model.WorklistFromBioHubItemMaterials.forEach((elem) => {
      if (elem.Condition === undefined || elem.Condition === null) {
        materialFormCompleted = false;
      }
    });

    return materialFormCompleted;
  }

  update() {
    this.$emit("update", this.model);
  }

  updateLastSubmissionApproved(approved: boolean) {
    this.model.LastSubmissionApproved = approved;
    this.$emit("update", this.model);
  }

  setSaveAsDraft(saveAsDraft: boolean) {
    this.model.IsSaveDraft = saveAsDraft;
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  submit() {
    if (this.WaitForArrivalConditionCheck && this.approvedSelected == false) {
      this.submitForNewFeedback();
    } else {
      this.updateLastSubmissionApproved(true);
      this.setSaveAsDraft(false);
      this.$emit("submit");
    }
  }

  submitForNewFeedback() {
    this.updateLastSubmissionApproved(false);
    this.setSaveAsDraft(false);
    this.$emit("submit");
  }

  saveAsDraft() {
    this.setSaveAsDraft(true);
    this.$emit("saveAsDraft");
  }

  async mounted() {
    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.signature-hover {
  cursor: pointer;
}
</style>
