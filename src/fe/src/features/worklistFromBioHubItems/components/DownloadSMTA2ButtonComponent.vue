<template>
  <div>
    <v-card>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div :class="smta2DownloadClass">
          <div class="smta-header">
            <h4>SMTA-2 Submission</h4>
            <h5 v-if="approvedOnVisible" class="smta-status">
              {{ approvedOn }}
            </h5>
          </div>
          <div class="smta-action">
            <button @click="DownloadSMTA2" class="btn-prime">
              Download Document
            </button>
          </div>
        </div>
        <v-card-actions>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../utils/helper";
import { WorklistFromBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import { DocumentModule } from "../../documents/store";
import { DocumentTemplateModule } from "../../documentTemplates/store";
import { SMTAApprovalStatus } from "@/models/enums/SMTAApprovalStatus";
import { PermissionType } from "@/models/enums/PermissionType";

@Component({
  components: {},
})
export default class DownloadSMTA2ButtonComponent extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

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

  get approvedOn(): string {
    if (
      this.model.SMTA2ApprovalDate != null &&
      this.model.SMTA2ApprovalDate != undefined
    ) {
      return "Approved on " + this.getFormatDate(this.model.SMTA2ApprovalDate);
    } else {
      return "";
    }
  }

  get approvedOnVisible(): boolean {
    return this.approvedOn != "";
  }

  get smta2DownloadClass(): string {
    if (
      this.model.SMTA2ApprovalStatus == SMTAApprovalStatus.DocumentToBeSubmitted
    ) {
      return "smta-document smta-status-requested";
    } else if (
      this.model.SMTA2ApprovalStatus ==
      SMTAApprovalStatus.DocumentApprovalPending
    ) {
      return "smta-document smta-status-pending";
    } else {
      return "smta-document smta-status-approved";
    }
  }

  async DownloadSMTA2() {
    const info = new Map<string, string>();
    info.set("Id", this.model.CurrentDownloadSMTA2DocumentId);
    info.set("Name", this.model.CurrentDownloadSMTA2DocumentName);

    if (
      this.model.SMTA2ApprovalStatus == SMTAApprovalStatus.DocumentToBeSubmitted
    ) {
      await DocumentTemplateModule.ReadTemplateForShipmentRequest(info);
    } else {
      await DocumentModule.ReadDocumentForShipmentRequest(info);
    }
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  update() {
    this.$emit("update", this.model);
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

.shipment-action-vcard {
  margin-left: 12px;
  margin-top: 20px;
}
</style>
