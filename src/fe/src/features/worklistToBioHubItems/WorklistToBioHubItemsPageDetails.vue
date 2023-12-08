<template>
  <div>
    <h2>{{ PhaseTitle }}</h2>
    <h3>Shipment direction: Send BMEPP into the BioHub</h3>
    <v-container class="px-0" fluid>
      <MacroTimeline :current-status="worklistToBioHubItem.CurrentStatus">
      </MacroTimeline>
    </v-container>
    <v-container class="px-0 timeline-container" fluid>
      <template v-for="WorklistTimeline in WorklistTimelines">
        <WorklistTimelineComponent
          v-bind:key="WorklistTimeline"
          :timeline-title="WorklistTimeline.TimelineTitle"
          :worklist-timeline-events-days="WorklistTimeline.Events"
        >
        </WorklistTimelineComponent>
      </template>
    </v-container>
    <v-container class="px-0" fluid>
      <DownloadSMTA1ButtonComponent
        v-if="
          CanReadDocument &&
          (worklistToBioHubItem.IsPast != true ||
            (worklistToBioHubItem.CurrentDownloadSMTA1DocumentId != '' &&
              worklistToBioHubItem.CurrentDownloadSMTA1DocumentId != null &&
              worklistToBioHubItem.CurrentDownloadSMTA1DocumentId != undefined))
        "
        v-model="worklistToBioHubItem"
      ></DownloadSMTA1ButtonComponent>
    </v-container>
    <h2>Shipment Data</h2>
    <v-container class="px-0" fluid>
      <WorklistToBioHubItemsTimeline
        :outlined="true"
        :items="currentWorklistToBioHubItemAsList"
        @downloadHistoryFile="downloadHistoryFile"
        @downloadFile="downloadFile"
      >
      </WorklistToBioHubItemsTimeline>
    </v-container>
    <v-container class="px-0" fluid>
      <PreShipmentPhase
        v-if="PreShipmentPhase == true && hasReadPermissionByStatus == true"
        v-model="worklistToBioHubItem"
        @downloadFile="downloadFile"
        @submit="submit"
        @saveAsDraft="saveAsDraft"
      >
      </PreShipmentPhase>
      <ShipmentPhase
        v-if="ShipmentPhase == true && hasReadPermissionByStatus == true"
        v-model="worklistToBioHubItem"
        @downloadFile="downloadFile"
        @submit="submit"
        @saveAsDraft="saveAsDraft"
      >
      </ShipmentPhase>
      <PostShipmentPhase
        v-if="PostShipmentPhase == true && hasReadPermissionByStatus == true"
        v-model="worklistToBioHubItem"
        @downloadFile="downloadFile"
        @submit="submit"
        @saveAsDraft="saveAsDraft"
      >
      </PostShipmentPhase>
    </v-container>
    <v-container class="px-0 mt-5" fluid>
      <v-expansion-panels focusable>
        <v-expansion-panel>
          <v-expansion-panel-header> Timeline </v-expansion-panel-header>
          <v-expansion-panel-content>
            <WorklistToBioHubItemsTimeline
              :items="worklistToBioHubHistoryItems"
              :flat="true"
              :outlined="false"
              @downloadHistoryFile="downloadHistoryFile"
              @downloadFile="downloadFile"
            >
            </WorklistToBioHubItemsTimeline>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubItemModule } from "./store";
import { AppModule } from "../../store/MainStore";
import {
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../utils/helper";
import PreShipmentPhase from "./components/PreShipmentPhase.vue";
import ShipmentPhase from "./components/ShipmentPhase.vue";
import PostShipmentPhase from "./components/PostShipmentPhase.vue";
import { WorklistToBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubDownloadPermissionsByStatus";
import { WorklistToBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubReadPermissionsByStatus";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import BackButton from "@/components/BackButton.vue";
import { getAreaFromRoleType } from "../../utils/helper";
import WorklistToBioHubItemsTimeline from "./components/WorklistToBioHubItemsTimeline.vue";
import MacroTimeline from "./components/MacroTimeline.vue";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import DownloadSMTA1ButtonComponent from "./components/DownloadSMTA1ButtonComponent.vue";
import { PermissionType } from "@/models/enums/PermissionType";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({
  components: {
    BackButton,
    WorklistToBioHubItemsTimeline,
    MacroTimeline,
    PreShipmentPhase,
    ShipmentPhase,
    PostShipmentPhase,
    WorklistTimelineComponent,
    DownloadSMTA1ButtonComponent,
  },
})
export default class WorklistToBioHubItemsPageDetails extends Vue {
  private download = false;
  private openSaveOrDelete = false;

  private showLoading() {
    this.openSaveOrDelete = true;
    AppModule.ShowLoading();
  }

  private hideLoading() {
    this.openSaveOrDelete = false;
    this.download = false;
    AppModule.HideLoading();
  }

  get CanReadDocument(): boolean {
    return hasPermission(PermissionNames.CanReadDocument);
  }

  get WorklistTimelines(): Array<WorklistTimeline> {
    return WorklistToBioHubItemModule.WorklistTimelines;
  }

  get currentWorklistToBioHubItemAsList(): Array<WorklistToBioHubItem> {
    let result = new Array<WorklistToBioHubItem>();
    if (this.worklistToBioHubItem !== undefined) {
      result.push(this.worklistToBioHubItem);
    }
    return result;
  }

  get worklistToBioHubHistoryItems(): Array<WorklistToBioHubItem> {
    let items = WorklistToBioHubItemModule.WorklistToBioHubHistoryItems;
    if (this.worklistToBioHubItem !== undefined) {
      const Id = this.worklistToBioHubItem.Id;
      if (!items.find((e) => e.Id === Id)) {
        items.push(this.worklistToBioHubItem);
      }
    }
    items = items.sort(this.sortByDate);
    return items;
  }

  get worklistToBioHubItem(): WorklistToBioHubItem | undefined {
    return WorklistToBioHubItemModule.WorklistToBioHubItem;
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

  get RequestInitiation(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.RequestInitiation
    );
  }

  get SubmitAnnex2OfSMTA1(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitAnnex2OfSMTA1
    );
  }

  get WaitingForAnnex2OfSMTA1SECsApproval(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval
    );
  }

  get SubmitBookingFormOfSMTA1(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitBookingFormOfSMTA1
    );
  }

  get WaitForBookingFormSMTA1OPSApproval(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval
    );
  }

  get SubmitSMTA1ShipmentDocuments(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments
    );
  }

  get WaitForPickUpCompleted(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get WaitForArrivalConditionCheck(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentBHFSendFeedback(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForCommentBHFSendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitForFinalApproval
    );
  }

  get ShipmentCompleted(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.ShipmentCompleted
    );
  }

  get PreShipmentPhase(): boolean {
    return (
      this.SubmitAnnex2OfSMTA1 ||
      this.WaitingForAnnex2OfSMTA1SECsApproval ||
      this.SubmitBookingFormOfSMTA1 ||
      this.WaitForBookingFormSMTA1OPSApproval
    );
  }

  get ShipmentPhase(): boolean {
    return (
      this.SubmitSMTA1ShipmentDocuments ||
      this.WaitForPickUpCompleted ||
      this.WaitForDeliveryCompleted ||
      this.WaitForArrivalConditionCheck ||
      this.WaitForCommentBHFSendFeedback ||
      this.WaitForFinalApproval
    );
  }

  get PostShipmentPhase(): boolean {
    return this.ShipmentCompleted;
  }

  get PhaseTitle(): string {
    if (this.PreShipmentPhase == true) {
      return "Pre-Shipment";
    } else if (this.ShipmentPhase == true) {
      return "Shipment";
    } else if (this.PostShipmentPhase == true) {
      return "Post-Shipment";
    }
    return "";
  }

  async downloadFile() {
    this.download = true;
    this.showLoading();
    await WorklistToBioHubItemModule.DownloadDocument();
    this.hideLoading();
  }

  async downloadHistoryFile() {
    this.download = true;
    this.showLoading();
    await WorklistToBioHubItemModule.DownloadHistoryDocument();
    this.hideLoading();
  }

  async submit() {
    this.showLoading();
    await WorklistToBioHubItemModule.UpdateWorklistToBioHubItem()
      .then((response) => {
        this.hideLoading();
        const area = getAreaFromRoleType();
        this.$router.push({
          name: area + "-shipment-requests",
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  async saveAsDraft() {
    this.showLoading();
    await WorklistToBioHubItemModule.UpdateWorklistToBioHubItem()
      .then((response) => {
        this.hideLoading();
        const area = getAreaFromRoleType();
        this.$router.push({
          name: area + "-shipment-requests",
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  async loadPageInfo() {
    WorklistToBioHubItemModule.CLEAR_WORKLISTTOBIOHUBITEM_CREATE();

    await WorklistToBioHubItemModule.ReadWorklistToBioHubItem(
      this.$route.params.id
    );

    await WorklistToBioHubItemModule.ListWorklistToBioHubHistoryItems(
      this.$route.params.id
    );

    await WorklistToBioHubItemModule.ListWorklistToBioHubItemEvents(
      this.$route.params.id
    );
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  sortByDate(a: any, b: any) {
    if (a.OperationDate < b.OperationDate) {
      return 1;
    }
    if (a.OperationDate > b.OperationDate) {
      return -1;
    }
    return 0;
  }
}
</script>
