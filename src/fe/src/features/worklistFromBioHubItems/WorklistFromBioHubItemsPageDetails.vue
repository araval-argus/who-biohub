<template>
  <div>
    <h2>{{ PhaseTitle }}</h2>
    <h3>Shipment direction: Receive BMEPP from the BioHub</h3>
    <v-container class="px-0" fluid>
      <MacroTimeline :current-status="worklistFromBioHubItem.CurrentStatus">
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
      <DownloadSMTA2ButtonComponent
        v-if="
          CanReadDocument &&
          (worklistFromBioHubItem.IsPast != true ||
            (worklistFromBioHubItem.CurrentDownloadSMTA2DocumentId != '' &&
              worklistFromBioHubItem.CurrentDownloadSMTA2DocumentId != null &&
              worklistFromBioHubItem.CurrentDownloadSMTA2DocumentId !=
                undefined))
        "
        v-model="worklistFromBioHubItem"
      ></DownloadSMTA2ButtonComponent>
    </v-container>
    <h2>Shipment Data</h2>
    <v-container class="px-0" fluid>
      <WorlistFromBioHubItemsTimeline
        :outlined="true"
        :items="currentWorklistFromBioHubItemAsList"
        @downloadHistoryFile="downloadHistoryFile"
        @downloadFile="downloadFile"
      >
      </WorlistFromBioHubItemsTimeline>
    </v-container>
    <v-container class="px-0" fluid>
      <PreShipmentPhase
        v-if="PreShipmentPhase == true && hasReadPermissionByStatus == true"
        v-model="worklistFromBioHubItem"
        @downloadFile="downloadFile"
        @submit="submit"
        @saveAsDraft="saveAsDraft"
      >
      </PreShipmentPhase>
      <ShipmentPhase
        v-if="ShipmentPhase == true && hasReadPermissionByStatus == true"
        v-model="worklistFromBioHubItem"
        @downloadFile="downloadFile"
        @submit="submit"
        @saveAsDraft="saveAsDraft"
      >
      </ShipmentPhase>
      <PostShipmentPhase
        v-if="PostShipmentPhase == true && hasReadPermissionByStatus == true"
        v-model="worklistFromBioHubItem"
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
            <WorlistFromBioHubItemsTimeline
              :items="worklistFromBioHubHistoryItems"
              :flat="true"
              :outlined="false"
              @downloadHistoryFile="downloadHistoryFile"
              @downloadFile="downloadFile"
            >
            </WorlistFromBioHubItemsTimeline>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubItemModule } from "./store";
import { AppModule } from "../../store/MainStore";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../utils/helper";
import PreShipmentPhase from "./components/PreShipmentPhase.vue";
import ShipmentPhase from "./components/ShipmentPhase.vue";
import PostShipmentPhase from "./components/PostShipmentPhase.vue";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import BackButton from "@/components/BackButton.vue";
import { getAreaFromRoleType } from "../../utils/helper";
import WorlistFromBioHubItemsTimeline from "./components/WorlistFromBioHubItemsTimeline.vue";
import MacroTimeline from "./components/MacroTimeline.vue";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import DownloadSMTA2ButtonComponent from "./components/DownloadSMTA2ButtonComponent.vue";
import { component } from "vue/types/umd";
import { PermissionType } from "@/models/enums/PermissionType";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({
  components: {
    BackButton,
    WorlistFromBioHubItemsTimeline,
    MacroTimeline,
    PreShipmentPhase,
    ShipmentPhase,
    PostShipmentPhase,
    WorklistTimelineComponent,
    DownloadSMTA2ButtonComponent,
  },
})
export default class WorklistFromBioHubItemsPageDetails extends Vue {
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
    return WorklistFromBioHubItemModule.WorklistTimelines;
  }

  get currentWorklistFromBioHubItemAsList(): Array<WorklistFromBioHubItem> {
    let result = new Array<WorklistFromBioHubItem>();
    if (this.worklistFromBioHubItem !== undefined) {
      result.push(this.worklistFromBioHubItem);
    }
    return result;
  }

  get worklistFromBioHubHistoryItems(): Array<WorklistFromBioHubItem> {
    let items = WorklistFromBioHubItemModule.WorklistFromBioHubHistoryItems;
    if (this.worklistFromBioHubItem !== undefined) {
      const Id = this.worklistFromBioHubItem.Id;
      if (!items.find((e) => e.Id === Id)) {
        items.push(this.worklistFromBioHubItem);
      }
    }
    items = items.sort(this.sortByDate);
    return items;
  }

  get worklistFromBioHubItem(): WorklistFromBioHubItem | undefined {
    return WorklistFromBioHubItemModule.WorklistFromBioHubItem;
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

  get RequestInitiation(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.RequestInitiation
    );
  }

  get SubmitAnnex2OfSMTA2(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2
    );
  }

  get WaitingForAnnex2OfSMTA2SECsApproval(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval
    );
  }

  get SubmitBiosafetyChecklistFormOfSMTA2(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2
    );
  }

  get WaitForBiosafetyChecklistFormSMTA2BSFsApproval(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval
    );
  }

  get SubmitBookingFormOfSMTA2(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2
    );
  }

  get WaitForBookingFormSMTA2OPSsApproval(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval
    );
  }

  get SubmitBHFSMTA2ShipmentDocuments(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments
    );
  }

  get SubmitQESMTA2ShipmentDocuments(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments
    );
  }

  get WaitForPickUpCompleted(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get WaitForArrivalConditionCheck(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentQESendFeedback(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForCommentQESendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForFinalApproval
    );
  }

  get ShipmentCompleted(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.ShipmentCompleted
    );
  }

  get PreShipmentPhase(): boolean {
    return (
      this.SubmitAnnex2OfSMTA2 ||
      this.WaitingForAnnex2OfSMTA2SECsApproval ||
      this.SubmitBiosafetyChecklistFormOfSMTA2 ||
      this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval ||
      this.SubmitBookingFormOfSMTA2 ||
      this.WaitForBookingFormSMTA2OPSsApproval
    );
  }

  get ShipmentPhase(): boolean {
    return (
      this.SubmitBHFSMTA2ShipmentDocuments ||
      this.SubmitQESMTA2ShipmentDocuments ||
      this.WaitForPickUpCompleted ||
      this.WaitForDeliveryCompleted ||
      this.WaitForArrivalConditionCheck ||
      this.WaitForCommentQESendFeedback ||
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
    await WorklistFromBioHubItemModule.DownloadDocument();
    this.hideLoading();
  }

  async downloadHistoryFile() {
    this.download = true;
    this.showLoading();
    await WorklistFromBioHubItemModule.DownloadHistoryDocument();
    this.hideLoading();
  }

  async submit() {
    this.showLoading();
    await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItem()
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
    await WorklistFromBioHubItemModule.UpdateWorklistFromBioHubItem()
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
    WorklistFromBioHubItemModule.CLEAR_WORKLISTFROMBIOHUBITEM_CREATE();

    await WorklistFromBioHubItemModule.ReadWorklistFromBioHubItem(
      this.$route.params.id
    );

    await WorklistFromBioHubItemModule.ListWorklistFromBioHubHistoryItems(
      this.$route.params.id
    );

    await WorklistFromBioHubItemModule.ListWorklistFromBioHubItemEvents(
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
