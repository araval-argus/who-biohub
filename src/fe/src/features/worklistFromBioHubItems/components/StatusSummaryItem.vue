<template>
  <div>
    <v-card flat>
      <v-card-text v-if="SubmitAnnex2OfSMTA2">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ worklistItemTitle }}
          <a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>
        <div v-if="LastSubmissionApproved">
          <p><strong>Created By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>Request Initiation Date</strong> {{ operationDate }}</p>
        </div>
        <div v-else>
          <p>
            <strong>Rejected By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Annex 2 of SMTA 2 Rejected Date</strong>
            {{ operationDate }}
          </p>
          <p><strong>Comment</strong> {{ comment }}</p>
        </div>
      </v-card-text>
      <v-card-text v-if="WaitingForAnnex2OfSMTA2SECsApproval">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ worklistItemTitle }}
          <a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>
        <div>
          <p>
            <strong>Uploaded By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Annex 2 of SMTA 2 Submission Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>

      <!-- -->
      <v-card-text v-if="SubmitBiosafetyChecklistFormOfSMTA2">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ worklistItemTitle }}
          <a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>

        <div v-if="LastSubmissionApproved">
          <p>
            <strong>Approved By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Operation Date</strong>
            {{ operationDate }}
          </p>
        </div>
        <div v-else>
          <p>
            <strong>Rejected By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Rejected Date</strong>
            {{ operationDate }}
          </p>
          <p><strong>Comment</strong> {{ comment }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForBiosafetyChecklistFormSMTA2BSFsApproval">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ worklistItemTitle
          }}<a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>
        <div>
          <p>
            <strong>Approved By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Operation Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>
      <!-- -->

      <v-card-text v-if="SubmitBookingFormOfSMTA2">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ worklistItemTitle }}
          <a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>
        <div v-if="LastSubmissionApproved">
          <p>
            <strong>Approved By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Annex 2 of SMTA 2 Approved Date</strong>
            {{ operationDate }}
          </p>
        </div>
        <div v-else>
          <p>
            <strong>Rejected By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Booking Form of SMTA 2 Rejected Date</strong>
            {{ operationDate }}
          </p>
          <p><strong>Comment</strong> {{ comment }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForBookingFormSMTA2OPSsApproval">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ worklistItemTitle }}&nbsp;
          <a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>
        <div>
          <p>
            <strong>Uploaded By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Booking Form of SMTA2 Submission Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>

      <v-card-text v-if="SubmitBHFSMTA2ShipmentDocuments">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p>
            <strong>Approved By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Operation Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>

      <v-card-text v-if="SubmitQESMTA2ShipmentDocuments">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p>
            <strong>Approved By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Operation Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForPickUpCompleted">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p>
            <strong>Submitted By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Shipment documents submitted Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForDeliveryCompleted">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p><strong>Action By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>Operation Date</strong> {{ operationDate }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForArrivalConditionCheck">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p><strong>Action By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>Operation Date</strong> {{ operationDate }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForCommentQESendFeedback">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p><strong>By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>Operation Date</strong> {{ operationDate }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForFinalApproval">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p><strong>Action By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>Operation Date</strong> {{ operationDate }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="ShipmentCompleted">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p><strong>Action By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>Operation Date</strong> {{ operationDate }}</p>
        </div>
      </v-card-text>
      <br />
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model } from "vue-property-decorator";

import { WorklistFromBioHubItemModule } from "../store";
import { WorklistFromBioHubItemFileInfo } from "@/models/WorklistFromBioHubItemFileInfo";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../utils/helper";
import { WorklistFromBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import { RoleType } from "@/models/enums/RoleType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import { PermissionType } from "@/models/enums/PermissionType";

@Component({
  components: {},
})
export default class StatusSummaryItem extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  @Prop({ type: Array, default: [] })
  readonly worklistFromBioHubItem: WorklistFromBioHubItem;

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.worklistFromBioHubItem.CurrentStatus;
  }

  get documentId(): string {
    if (
      this.SubmitAnnex2OfSMTA2 == true ||
      this.WaitingForAnnex2OfSMTA2SECsApproval == true
    ) {
      if (this.ElectronicallyFill == false) {
        return this.worklistFromBioHubItem.Annex2OfSMTA2DocumentId;
      }
    } else if (
      this.SubmitBiosafetyChecklistFormOfSMTA2 == true ||
      this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval == true
    ) {
      if (this.ElectronicallyFill == false) {
        return this.worklistFromBioHubItem.BiosafetyChecklistOfSMTA2DocumentId;
      }
    } else if (
      this.SubmitBookingFormOfSMTA2 == true ||
      this.WaitForBookingFormSMTA2OPSsApproval == true
    ) {
      if (this.ElectronicallyFill == false) {
        return this.worklistFromBioHubItem.BookingFormOfSMTA2DocumentId;
      }
    }

    return "";
  }

  get ElectronicallyFill(): boolean {
    if (
      this.SubmitAnnex2OfSMTA2 == true ||
      this.WaitingForAnnex2OfSMTA2SECsApproval == true
    ) {
      return (
        this.worklistFromBioHubItem.Annex2FillingOption ==
        WorklistFillingOption.ElectronicallyFill
      );
    } else if (
      this.SubmitBiosafetyChecklistFormOfSMTA2 == true ||
      this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval == true
    ) {
      return (
        this.worklistFromBioHubItem.BiosafetyChecklistFillingOption ==
        WorklistFillingOption.ElectronicallyFill
      );
    } else if (
      this.SubmitBookingFormOfSMTA2 == true ||
      this.WaitForBookingFormSMTA2OPSsApproval == true
    ) {
      return (
        this.worklistFromBioHubItem.BookingFormFillingOption ==
        WorklistFillingOption.ElectronicallyFill
      );
    } else {
      return false;
    }
  }
  get worklistId(): string {
    return this.worklistFromBioHubItem.Id;
  }

  get documentName(): string {
    if (
      this.SubmitAnnex2OfSMTA2 == true ||
      this.WaitingForAnnex2OfSMTA2SECsApproval == true
    ) {
      return this.worklistFromBioHubItem.Annex2OfSMTA2DocumentName;
    } else if (
      this.SubmitBiosafetyChecklistFormOfSMTA2 == true ||
      this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval == true
    ) {
      return this.worklistFromBioHubItem.BiosafetyChecklistOfSMTA2DocumentName;
    } else if (
      this.SubmitBookingFormOfSMTA2 == true ||
      this.WaitForBookingFormSMTA2OPSsApproval == true
    ) {
      return this.worklistFromBioHubItem.BookingFormOfSMTA2DocumentName;
    } else {
      return "";
    }
  }
  get statusName(): string {
    return this.worklistFromBioHubItem.CurrentStatusName;
  }

  get worklistItemTitle(): string {
    return this.worklistFromBioHubItem.WorklistItemTitle;
  }

  get userName(): string {
    return this.worklistFromBioHubItem.UserName;
  }

  get laboratoryName(): string {
    return this.worklistFromBioHubItem.LaboratoryName;
  }

  get bioHubFacilityName(): string {
    return this.worklistFromBioHubItem.BioHubFacilityName;
  }

  get operationDate(): string {
    return this.getFormatDate(this.worklistFromBioHubItem.OperationDate);
  }

  get userRoleType(): RoleType {
    return this.worklistFromBioHubItem.UserRoleType;
  }

  get userRoleName(): string {
    return this.worklistFromBioHubItem.UserRoleName;
  }

  get comment(): string {
    return this.worklistFromBioHubItem.Comment;
  }

  get userInstitute(): string {
    if (this.userRoleType == RoleType.BioHubFacility) {
      return this.bioHubFacilityName;
    } else if (this.userRoleType == RoleType.WHO) {
      return "WHO";
    } else {
      return this.laboratoryName;
    }
  }

  get RequestInitiation(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.RequestInitiation
    );
  }

  get SubmitAnnex2OfSMTA2(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2
    );
  }

  get WaitingForAnnex2OfSMTA2SECsApproval(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval
    );
  }

  get SubmitBiosafetyChecklistFormOfSMTA2(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2
    );
  }

  get WaitForBiosafetyChecklistFormSMTA2BSFsApproval(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval
    );
  }

  get SubmitBookingFormOfSMTA2(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2
    );
  }

  get WaitForBookingFormSMTA2OPSsApproval(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval
    );
  }

  get SubmitBHFSMTA2ShipmentDocuments(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments
    );
  }

  get SubmitQESMTA2ShipmentDocuments(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments
    );
  }

  get WaitForPickUpCompleted(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get WaitForArrivalConditionCheck(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentQESendFeedback(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForCommentQESendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForFinalApproval
    );
  }

  get ShipmentCompleted(): boolean {
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.ShipmentCompleted
    );
  }

  get LastSubmissionApproved(): boolean {
    return this.worklistFromBioHubItem.LastSubmissionApproved;
  }

  get PreviousStatus(): WorklistFromBioHubStatus {
    return this.worklistFromBioHubItem.PreviousStatus;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.worklistFromBioHubItem.CurrentStatus,
      PermissionType.DownloadFile,
      this.worklistFromBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.worklistFromBioHubItem.CurrentStatus,
      PermissionType.Read,
      this.worklistFromBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.worklistFromBioHubItem.CurrentStatus,
      PermissionType.Update,
      this.worklistFromBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  update() {
    this.$emit("update", this.worklistFromBioHubItem);
  }

  downloadDocument(e: any) {
    const fileInfo = Object.create({
      Id: this.documentId,
      Name: this.documentName,
      WorklistId: this.worklistId,
    }) as WorklistFromBioHubItemFileInfo;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEMDOWNLOADFILEINFO(
      fileInfo
    );
    if (this.worklistFromBioHubItem.HistoryDto == true) {
      this.$emit("downloadHistoryFile");
    } else {
      this.$emit("downloadFile");
    }
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();
    const hour = parsedDate.getHours().toString().padStart(2, "0");
    const minutes = parsedDate.getMinutes().toString().padStart(2, "0");
    const seconds = parsedDate.getSeconds().toString().padStart(2, "0");

    return (
      day +
      "/" +
      month +
      "/" +
      year +
      " " +
      hour +
      ":" +
      minutes +
      ":" +
      seconds
    );
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
