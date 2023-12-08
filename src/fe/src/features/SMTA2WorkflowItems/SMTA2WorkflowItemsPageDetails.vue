<template>
  <div>
    <h2>{{ PhaseTitle }}</h2>
    <v-container class="px-0" fluid>
      <MacroTimeline :current-status="SMTA2WorkflowItem.CurrentStatus">
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
      <SMTA2WorkflowItemsTimeline
        :outlined="true"
        :items="currentSMTA2WorkflowItemAsList"
        @downloadHistoryFile="downloadHistoryFile"
        @downloadFile="downloadFile"
      >
      </SMTA2WorkflowItemsTimeline>
    </v-container>
    <v-container class="px-0" fluid>
      <SMTA2Phase
        v-if="SMTA2Phase && hasReadPermissionByStatus"
        v-model="SMTA2WorkflowItem"
        @downloadFile="downloadFile"
        @submit="submit"
      >
      </SMTA2Phase>
    </v-container>
    <v-container class="px-0 mt-5" fluid>
      <v-expansion-panels focusable>
        <v-expansion-panel>
          <v-expansion-panel-header> Timeline </v-expansion-panel-header>
          <v-expansion-panel-content>
            <SMTA2WorkflowItemsTimeline
              :items="SMTA2WorkflowHistoryItems"
              :flat="true"
              :outlined="false"
              @downloadHistoryFile="downloadHistoryFile"
              @downloadFile="downloadFile"
            >
            </SMTA2WorkflowItemsTimeline>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";
import { SMTA2WorkflowItemModule } from "./store";
import { AppModule } from "../../store/MainStore";
import {
  GetSMTA2WorkflowStatusPermission,
  hasPermission,
} from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import SMTA2Phase from "./components/SMTA2Phase.vue";
import { SMTA2WorkflowDownloadPermissionsByStatusList } from "@/models/constants/SMTA2WorkflowDownloadPermissionsByStatus";
import { SMTA2WorkflowReadPermissionsByStatusList } from "@/models/constants/SMTA2WorkflowReadPermissionsByStatus";
import { SMTA2WorkflowSubmitPermissionsByStatusList } from "@/models/constants/SMTA2WorkflowSubmitPermissionsByStatus";
import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";
import BackButton from "@/components/BackButton.vue";
import { getAreaFromRoleType } from "../../utils/helper";
import SMTA2WorkflowItemsTimeline from "./components/SMTA2WorkflowItemsTimeline.vue";
import MacroTimeline from "./components/MacroTimeline.vue";
import { CountryModule } from "../countries/store";
import { UserModule } from "../users/store";
import { AuthModule } from "../auth/store";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import { PermissionType } from "@/models/enums/PermissionType";
import { DocumentTemplateModule } from "../documentTemplates/store";

@Component({
  components: {
    SMTA2Phase,
    BackButton,
    SMTA2WorkflowItemsTimeline,
    MacroTimeline,
    WorklistTimelineComponent,
  },
})
export default class SMTA2WorkflowItemsPageDetails extends Vue {
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
    return SMTA2WorkflowItemModule.WorklistTimelines;
  }

  get currentSMTA2WorkflowItemAsList(): Array<SMTA2WorkflowItem> {
    let result = new Array<SMTA2WorkflowItem>();
    if (this.SMTA2WorkflowItem !== undefined) {
      result.push(this.SMTA2WorkflowItem);
    }
    return result;
  }

  get SMTA2WorkflowHistoryItems(): Array<SMTA2WorkflowItem> {
    let items = SMTA2WorkflowItemModule.SMTA2WorkflowHistoryItems;
    if (this.SMTA2WorkflowItem !== undefined) {
      const Id = this.SMTA2WorkflowItem.Id;
      if (!items.find((e) => e.Id === Id)) {
        items.push(this.SMTA2WorkflowItem);
      }
    }
    items = items.sort(this.sortByDate);
    return items;
  }

  get SMTA2WorkflowItem(): SMTA2WorkflowItem | undefined {
    return SMTA2WorkflowItemModule.SMTA2WorkflowItem;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetSMTA2WorkflowStatusPermission(
      this.SMTA2WorkflowItem.CurrentStatus,
      PermissionType.DownloadFile,
      this.SMTA2WorkflowItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetSMTA2WorkflowStatusPermission(
      this.SMTA2WorkflowItem.CurrentStatus,
      PermissionType.Read,
      this.SMTA2WorkflowItem.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetSMTA2WorkflowStatusPermission(
      this.SMTA2WorkflowItem.CurrentStatus,
      PermissionType.Update,
      this.SMTA2WorkflowItem.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get RequestInitiation(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA2WorkflowItem.CurrentStatus ==
      SMTA2WorkflowStatus.RequestInitiation
    );
  }

  get SubmitSMTA2(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA2WorkflowItem.CurrentStatus == SMTA2WorkflowStatus.SubmitSMTA2
    );
  }

  get WaitingForSMTA2SECsApproval(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA2WorkflowItem.CurrentStatus ==
      SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval
    );
  }

  get SMTA2WorkflowComplete(): boolean {
    if (this.SMTA2WorkflowItem === undefined) {
      return false;
    }
    return (
      this.SMTA2WorkflowItem.CurrentStatus >=
      SMTA2WorkflowStatus.SMTA2WorkflowComplete
    );
  }

  get SMTA2Phase(): boolean {
    return (
      this.SubmitSMTA2 ||
      this.WaitingForSMTA2SECsApproval ||
      this.SMTA2WorkflowComplete
    );
  }

  get PhaseTitle(): string {
    if (this.SubmitSMTA2 == true) {
      return "Submit SMTA 2";
    } else if (this.WaitingForSMTA2SECsApproval == true) {
      return "Waiting For SMTA 2 Secretariat's Approval";
    } else if (this.SMTA2WorkflowComplete == true) {
      return "SMTA 2 Complete";
    }
    return "";
  }

  async downloadFile() {
    this.download = true;
    this.showLoading();
    await SMTA2WorkflowItemModule.DownloadDocument();
    this.hideLoading();
  }

  async downloadHistoryFile() {
    this.download = true;
    this.showLoading();
    await SMTA2WorkflowItemModule.DownloadHistoryDocument();
    this.hideLoading();
  }

  async submit() {
    this.showLoading();
    await SMTA2WorkflowItemModule.UpdateSMTA2WorkflowItem()
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
    await SMTA2WorkflowItemModule.UpdateSMTA2WorkflowItem()
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
    SMTA2WorkflowItemModule.CLEAR_SMTA2WORKFLOW_CREATE();

    await SMTA2WorkflowItemModule.ReadSMTA2WorkflowItem(
      this.$route.params.id
    ).then(() => {
      if (this.SMTA2WorkflowItem?.IsPast == true) {
        DocumentTemplateModule.ListSMTADocumentTemplates();
      }
    });

    await SMTA2WorkflowItemModule.ListSMTA2WorkflowHistoryItems(
      this.$route.params.id
    );

    await SMTA2WorkflowItemModule.ListSMTA2WorkflowItemEvents(
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
