<template>
  <div>
    <h2>{{ PhaseTitle }}</h2>
    <v-container class="px-0" fluid>
      <MacroTimeline :current-status="SMTA1WorkflowItem.CurrentStatus">
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
    <h2>SMTA Request Data</h2>
    <v-container class="px-0" fluid>
      <SMTA1WorkflowItemsTimeline
        :outlined="true"
        :items="currentSMTA1WorkflowItemAsList"
        @downloadHistoryFile="downloadHistoryFile"
        @downloadFile="downloadFile"
      >
      </SMTA1WorkflowItemsTimeline>
    </v-container>
    <v-container class="px-0" fluid>
      <SMTA1Phase
        v-if="SMTA1Phase && hasReadPermissionByStatus"
        v-model="SMTA1WorkflowItem"
        @downloadFile="downloadFile"
        @submit="submit"
      >
      </SMTA1Phase>
    </v-container>
    <v-container class="px-0 mt-5" fluid>
      <v-expansion-panels focusable>
        <v-expansion-panel>
          <v-expansion-panel-header> Timeline </v-expansion-panel-header>
          <v-expansion-panel-content>
            <SMTA1WorkflowItemsTimeline
              :items="SMTA1WorkflowHistoryItems"
              :flat="true"
              :outlined="false"
              @downloadHistoryFile="downloadHistoryFile"
              @downloadFile="downloadFile"
            >
            </SMTA1WorkflowItemsTimeline>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA1WorkflowItemModule } from "./store";
import { AppModule } from "../../store/MainStore";
import {
  GetSMTA1WorkflowStatusPermission,
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import SMTA1Phase from "./components/SMTA1Phase.vue";
import { SMTA1WorkflowDownloadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowDownloadPermissionsByStatus";
import { SMTA1WorkflowReadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowReadPermissionsByStatus";
import { SMTA1WorkflowSubmitPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowSubmitPermissionsByStatus";
import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import BackButton from "@/components/BackButton.vue";
import { getAreaFromRoleType } from "../../utils/helper";
import SMTA1WorkflowItemsTimeline from "./components/SMTA1WorkflowItemsTimeline.vue";
import MacroTimeline from "./components/MacroTimeline.vue";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import { PermissionType } from "@/models/enums/PermissionType";
import { DocumentTemplateModule } from "../documentTemplates/store";

@Component({
  components: {
    SMTA1Phase,
    BackButton,
    SMTA1WorkflowItemsTimeline,
    MacroTimeline,
    WorklistTimelineComponent,
  },
})
export default class SMTA1WorkflowItemsPageDetails extends Vue {
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

  get WorklistTimelines(): Array<WorklistTimeline> {
    return SMTA1WorkflowItemModule.WorklistTimelines;
  }

  get currentSMTA1WorkflowItemAsList(): Array<SMTA1WorkflowItem> {
    let result = new Array<SMTA1WorkflowItem>();
    if (this.SMTA1WorkflowItem !== undefined) {
      result.push(this.SMTA1WorkflowItem);
    }
    return result;
  }

  get SMTA1WorkflowHistoryItems(): Array<SMTA1WorkflowItem> {
    let items = SMTA1WorkflowItemModule.SMTA1WorkflowHistoryItems;
    if (this.SMTA1WorkflowItem !== undefined) {
      const Id = this.SMTA1WorkflowItem.Id;
      if (!items.find((e) => e.Id === Id)) {
        items.push(this.SMTA1WorkflowItem);
      }
    }
    items = items.sort(this.sortByDate);
    return items;
  }

  get SMTA1WorkflowItem(): SMTA1WorkflowItem | undefined {
    return SMTA1WorkflowItemModule.SMTA1WorkflowItem;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.SMTA1WorkflowItem.CurrentStatus,
      PermissionType.DownloadFile,
      this.SMTA1WorkflowItem.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.SMTA1WorkflowItem.CurrentStatus,
      PermissionType.Read,
      this.SMTA1WorkflowItem.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.SMTA1WorkflowItem.CurrentStatus,
      PermissionType.Update,
      this.SMTA1WorkflowItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get RequestInitiation(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA1WorkflowItem.CurrentStatus ==
      SMTA1WorkflowStatus.RequestInitiation
    );
  }

  get SubmitSMTA1(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA1WorkflowItem.CurrentStatus == SMTA1WorkflowStatus.SubmitSMTA1
    );
  }

  get WaitingForSMTA1SECsApproval(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA1WorkflowItem.CurrentStatus ==
      SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval
    );
  }

  get SMTA1WorkflowComplete(): boolean {
    if (this.SMTA1WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA1WorkflowItem.CurrentStatus >=
      SMTA1WorkflowStatus.SMTA1WorkflowComplete
    );
  }

  get SMTA1Phase(): boolean {
    return (
      this.SubmitSMTA1 ||
      this.WaitingForSMTA1SECsApproval ||
      this.SMTA1WorkflowComplete
    );
  }

  get PhaseTitle(): string {
    if (this.SubmitSMTA1 == true) {
      return "Submit SMTA 1";
    } else if (this.WaitingForSMTA1SECsApproval == true) {
      return "Waiting For SMTA 1 Secretariat's Approval";
    } else if (this.SMTA1WorkflowComplete == true) {
      return "SMTA 1 Complete";
    }
    return "";
  }

  async downloadFile() {
    this.download = true;
    this.showLoading();
    await SMTA1WorkflowItemModule.DownloadDocument();
    this.hideLoading();
  }

  async downloadHistoryFile() {
    this.download = true;
    this.showLoading();
    await SMTA1WorkflowItemModule.DownloadHistoryDocument();
    this.hideLoading();
  }

  async submit() {
    this.showLoading();
    await SMTA1WorkflowItemModule.UpdateSMTA1WorkflowItem()
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

  async saveAsDraft() {
    this.showLoading();
    await SMTA1WorkflowItemModule.UpdateSMTA1WorkflowItem()
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

  async loadPageInfo() {
    SMTA1WorkflowItemModule.CLEAR_SMTA1WORKFLOW_CREATE();

    await SMTA1WorkflowItemModule.ReadSMTA1WorkflowItem(
      this.$route.params.id
    ).then(() => {
      if (this.SMTA1WorkflowItem?.IsPast == true) {
        DocumentTemplateModule.ListSMTADocumentTemplates();
      }
    });

    await SMTA1WorkflowItemModule.ListSMTA1WorkflowHistoryItems(
      this.$route.params.id
    );

    await SMTA1WorkflowItemModule.ListSMTA1WorkflowItemEvents(
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
