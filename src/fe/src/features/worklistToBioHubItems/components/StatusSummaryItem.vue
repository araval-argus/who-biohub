<template>
  <div>
    <v-card flat>
      <v-card-text v-if="SubmitAnnex2OfSMTA1">
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
            <strong>Annex 2 of SMTA 1 Rejected Date</strong>
            {{ operationDate }}
          </p>
          <p><strong>Comment</strong> {{ comment }}</p>
        </div>
      </v-card-text>
      <v-card-text v-if="WaitingForAnnex2OfSMTA1SECsApproval">
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
            <strong>Annex 2 of SMTA 1 Submission Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>
      <v-card-text v-if="SubmitBookingFormOfSMTA1">
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
            <strong>Annex 2 of SMTA 1 Approved Date</strong>
            {{ operationDate }}
          </p>
        </div>
        <div v-else>
          <p>
            <strong>Rejected By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Booking Form of SMTA 1 Rejected Date</strong>
            {{ operationDate }}
          </p>
          <p><strong>Comment</strong> {{ comment }}</p>
        </div>
      </v-card-text>

      <v-card-text v-if="WaitForBookingFormSMTA1OPSApproval">
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
            <strong>Booking Form of SMTA1 Submission Date</strong>
            {{ operationDate }}
          </p>
        </div>
      </v-card-text>

      <v-card-text v-if="SubmitSMTA1ShipmentDocuments">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ worklistItemTitle }}</p>
        <div>
          <p>
            <strong>Approved By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p>
            <strong>Booking Form of SMTA 1 Approved Date</strong>
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

      <v-card-text v-if="WaitForCommentBHFSendFeedback">
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

import { WorklistToBioHubItemModule } from "../store";
import { WorklistToBioHubItemFileInfo } from "@/models/WorklistToBioHubItemFileInfo";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import {
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../../utils/helper";
import { WorklistToBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubDownloadPermissionsByStatus";
import { WorklistToBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubReadPermissionsByStatus";
import { WorklistToBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistToBioHubSubmitPermissionsByStatus";
import { RoleType } from "@/models/enums/RoleType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import { PermissionType } from "@/models/enums/PermissionType";
@Component({
  components: {},
})
export default class StatusSummaryItem extends Vue {
  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  @Prop({ type: Array, default: [] })
  readonly worklistToBioHubItem: WorklistToBioHubItem;

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.worklistToBioHubItem.CurrentStatus;
  }

  get documentId(): string {
    if (
      this.SubmitAnnex2OfSMTA1 == true ||
      this.WaitingForAnnex2OfSMTA1SECsApproval == true
    ) {
      if (this.ElectronicallyFill == false) {
        return this.worklistToBioHubItem.Annex2OfSMTA1DocumentId;
      }
    } else if (
      this.SubmitBookingFormOfSMTA1 == true ||
      this.WaitForBookingFormSMTA1OPSApproval == true
    ) {
      if (this.ElectronicallyFill == false) {
        return this.worklistToBioHubItem.BookingFormOfSMTA1DocumentId;
      }
    }

    return "";
  }

  get ElectronicallyFill(): boolean {
    if (
      this.SubmitAnnex2OfSMTA1 == true ||
      this.WaitingForAnnex2OfSMTA1SECsApproval == true
    ) {
      return (
        this.worklistToBioHubItem.Annex2FillingOption ==
        WorklistFillingOption.ElectronicallyFill
      );
    } else if (
      this.SubmitBookingFormOfSMTA1 == true ||
      this.WaitForBookingFormSMTA1OPSApproval == true
    ) {
      return (
        this.worklistToBioHubItem.BookingFormFillingOption ==
        WorklistFillingOption.ElectronicallyFill
      );
    } else {
      return false;
    }
  }
  get worklistId(): string {
    return this.worklistToBioHubItem.Id;
  }

  get documentName(): string {
    if (
      this.SubmitAnnex2OfSMTA1 == true ||
      this.WaitingForAnnex2OfSMTA1SECsApproval == true
    ) {
      return this.worklistToBioHubItem.Annex2OfSMTA1DocumentName;
    } else if (
      this.SubmitBookingFormOfSMTA1 == true ||
      this.WaitForBookingFormSMTA1OPSApproval == true
    ) {
      return this.worklistToBioHubItem.BookingFormOfSMTA1DocumentName;
    } else {
      return "";
    }
  }
  get statusName(): string {
    return this.worklistToBioHubItem.CurrentStatusName;
  }

  get worklistItemTitle(): string {
    return this.worklistToBioHubItem.WorklistItemTitle;
  }

  get userName(): string {
    return this.worklistToBioHubItem.UserName;
  }

  get laboratoryName(): string {
    return this.worklistToBioHubItem.LaboratoryName;
  }

  get bioHubFacilityName(): string {
    return this.worklistToBioHubItem.BioHubFacilityName;
  }

  get operationDate(): string {
    return this.getFormatDate(this.worklistToBioHubItem.OperationDate);
  }

  get userRoleType(): RoleType {
    return this.worklistToBioHubItem.UserRoleType;
  }

  get userRoleName(): string {
    return this.worklistToBioHubItem.UserRoleName;
  }

  get comment(): string {
    return this.worklistToBioHubItem.Comment;
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
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.RequestInitiation
    );
  }

  get SubmitAnnex2OfSMTA1(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitAnnex2OfSMTA1
    );
  }

  get WaitingForAnnex2OfSMTA1SECsApproval(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval
    );
  }
  get SubmitBookingFormOfSMTA1(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitBookingFormOfSMTA1
    );
  }

  get WaitForBookingFormSMTA1OPSApproval(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval
    );
  }

  get SubmitSMTA1ShipmentDocuments(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments
    );
  }

  get WaitForPickUpCompleted(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get WaitForArrivalConditionCheck(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentBHFSendFeedback(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForCommentBHFSendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForFinalApproval
    );
  }

  get ShipmentCompleted(): boolean {
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.ShipmentCompleted
    );
  }

  get LastSubmissionApproved(): boolean {
    return this.worklistToBioHubItem.LastSubmissionApproved;
  }

  get PreviousStatus(): WorklistToBioHubStatus {
    return this.worklistToBioHubItem.PreviousStatus;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.worklistToBioHubItem.CurrentStatus,
      PermissionType.DownloadFile,
      this.worklistToBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.worklistToBioHubItem.CurrentStatus,
      PermissionType.Read,
      this.worklistToBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.worklistToBioHubItem.CurrentStatus,
      PermissionType.Update,
      this.worklistToBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  update() {
    this.$emit("update", this.worklistToBioHubItem);
  }

  downloadDocument(e: any) {
    const fileInfo = Object.create({
      Id: this.documentId,
      Name: this.documentName,
      WorklistId: this.worklistId,
    }) as WorklistToBioHubItemFileInfo;

    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEMDOWNLOADFILEINFO(
      fileInfo
    );
    if (this.worklistToBioHubItem.HistoryDto == true) {
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
