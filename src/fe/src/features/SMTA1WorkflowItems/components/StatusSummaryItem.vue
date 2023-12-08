<template>
  <div>
    <v-card flat>
      <v-card-text v-if="SubmitSMTA1">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ workflowItemTitle }}</p>
        <div v-if="LastSubmissionApproved">
          <p><strong>Created By</strong> {{ userName }} ({{ userRoleName }})</p>
          <p><strong>Request Initiation Date</strong> {{ operationDate }}</p>
        </div>
        <div v-else>
          <p>
            <strong>Rejected By</strong> {{ userName }} ({{ userRoleName }})
          </p>
          <p><strong>From</strong> {{ userInstitute }}</p>
          <p><strong>SMTA 1 Rejected Date</strong> {{ operationDate }}</p>
          <p><strong>Comment</strong> {{ comment }}</p>
        </div>
      </v-card-text>
      <v-card-text v-if="WaitingForSMTA1SECsApproval">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p>
          <strong>Status</strong> {{ workflowItemTitle }}&nbsp;
          <a
            v-if="hasDownloadPermissionByStatus && documentId"
            @click="downloadDocument"
          >
            Download
          </a>
        </p>
        <p><strong>Approved By</strong> {{ userName }} ({{ userRoleName }})</p>
        <p><strong>From</strong> {{ userInstitute }}</p>
        <p><strong>SMTA 1 Approved Date</strong> {{ operationDate }}</p>
      </v-card-text>
      <v-card-text v-if="SMTA1WorkflowCompleted">
        <p><strong>Action</strong> {{ statusName }}</p>
        <p><strong>Status</strong> {{ workflowItemTitle }}</p>
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

import { SMTA1WorkflowItemModule } from "../store";
import { SMTA1WorkflowItemFileInfo } from "@/models/SMTA1WorkflowItemFileInfo";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import {
  GetSMTA1WorkflowStatusPermission,
  hasPermission,
} from "../../../utils/helper";
import { SMTA1WorkflowDownloadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowDownloadPermissionsByStatus";
import { SMTA1WorkflowReadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowReadPermissionsByStatus";
import { SMTA1WorkflowSubmitPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowSubmitPermissionsByStatus";
import { RoleType } from "@/models/enums/RoleType";
import { PermissionType } from "@/models/enums/PermissionType";

@Component({
  components: {},
})
export default class StatusSummaryItem extends Vue {
  @Model("update", { type: Object }) model!: SMTA1WorkflowItem;
  // Props

  @Prop({ type: Array, default: [] })
  readonly workflowItem: SMTA1WorkflowItem;

  get CurrentStatus(): SMTA1WorkflowStatus {
    return this.workflowItem.CurrentStatus;
  }

  get documentId(): string {
    if (this.SubmitSMTA1 == true || this.WaitingForSMTA1SECsApproval == true) {
      return this.workflowItem.SMTA1DocumentId;
    } else {
      return "";
    }
  }

  get workflowId(): string {
    return this.workflowItem.Id;
  }

  get documentName(): string {
    if (this.SubmitSMTA1 == true || this.WaitingForSMTA1SECsApproval == true) {
      return this.workflowItem.SMTA1DocumentName;
    } else {
      return "";
    }
  }
  get statusName(): string {
    return this.workflowItem.CurrentStatusName;
  }

  get workflowItemTitle(): string {
    return this.workflowItem.WorkflowItemTitle;
  }

  get userName(): string {
    return this.workflowItem.UserName;
  }

  get laboratoryName(): string {
    return this.workflowItem.LaboratoryName;
  }

  get operationDate(): string {
    return this.getFormatDate(this.workflowItem.OperationDate);
  }

  get userRoleType(): RoleType {
    return this.workflowItem.UserRoleType;
  }

  get userRoleName(): string {
    return this.workflowItem.UserRoleName;
  }

  get comment(): string {
    return this.workflowItem.Comment;
  }

  get userInstitute(): string {
    if (this.userRoleType == RoleType.WHO) {
      return "WHO";
    } else {
      return this.laboratoryName;
    }
  }

  get RequestInitiation(): boolean {
    return (
      this.workflowItem.CurrentStatus == SMTA1WorkflowStatus.RequestInitiation
    );
  }

  get SubmitSMTA1(): boolean {
    return this.workflowItem.CurrentStatus == SMTA1WorkflowStatus.SubmitSMTA1;
  }

  get WaitingForSMTA1SECsApproval(): boolean {
    return (
      this.workflowItem.CurrentStatus ==
      SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval
    );
  }

  get SMTA1WorkflowCompleted(): boolean {
    return (
      this.workflowItem.CurrentStatus ==
      SMTA1WorkflowStatus.SMTA1WorkflowComplete
    );
  }

  get LastSubmissionApproved(): boolean {
    return this.workflowItem.LastSubmissionApproved;
  }

  get PreviousStatus(): SMTA1WorkflowStatus {
    return this.workflowItem.PreviousStatus;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.workflowItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.workflowItem.CurrentStatus,
      PermissionType.DownloadFile,
      this.workflowItem.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.workflowItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.workflowItem.CurrentStatus,
      PermissionType.Read,
      this.workflowItem.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.workflowItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.workflowItem.CurrentStatus,
      PermissionType.Update,
      this.workflowItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  update() {
    this.$emit("update", this.workflowItem);
  }

  downloadDocument(e: any) {
    const fileInfo = Object.create({
      Id: this.documentId,
      Name: this.documentName,
      WorklistId: this.workflowId,
    }) as SMTA1WorkflowItemFileInfo;

    SMTA1WorkflowItemModule.SET_SMTA1WORKFLOWITEMDOWNLOADFILEINFO(fileInfo);
    if (this.workflowItem.HistoryDto == true) {
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
