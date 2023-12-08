<template>
  <v-card outlined class="mb-5">
    <section class="timeline boxed">
      <h2 v-if="title != ''">{{ title }}</h2>

      <div class="timeline-container">
        <div :class="dailyPanelClass">
          <div class="timeline-day">
            <div class="timeline-day-title">
              <h4>{{ TimelineEventTitle }}</h4>
              <time>{{ TimelineEventDate }}</time>
            </div>
            <dl class="daily-details">
              <template v-for="DetailsItem in DetailsItems">
                <dt v-bind:key="DetailsItem">
                  {{ DetailsItem.Time }}
                </dt>
                <dd v-bind:key="DetailsItem">
                  {{ DetailsItem.Event }}
                </dd>
              </template>
            </dl>
          </div>
          <div
            class="timeline-data-controller"
            id="panelClose"
            @click="closeDailyPanel"
          >
            «
          </div>
        </div>

        <div class="timeline-canvas">
          <div class="timeline-header">
            <h4>{{ timelineTitle }}</h4>
            <label for="calendarSwitch" class="timeline-switch"
              >Calendar Dates
              <input
                type="checkbox"
                id="calendarSwitch"
                name="calendarSwitch"
                @click="switchDate"
                checked
            /></label>
          </div>

          <div class="timeline-chart">
            <template
              v-for="worklistTimelineEventsDay in worklistTimelineEventsDays"
            >
              <div
                v-bind:key="worklistTimelineEventsDay"
                :class="DayClassName(worklistTimelineEventsDay)"
              >
                <div class="val" v-if="dateOn == false">
                  {{ worklistTimelineEventsDay.DateZeroBased }}
                </div>
                <div class="val" v-else>
                  {{ worklistTimelineEventsDay.DateString }}
                </div>

                <template
                  v-for="worklistTimelineEventsType in worklistTimelineEventsDay.WorklistTimelineEventsTypes"
                >
                  <div
                    v-bind:key="worklistTimelineEventsType"
                    :class="EventTypeClass(worklistTimelineEventsType)"
                  >
                    <div
                      class="item-icon"
                      @click="EventDetails(worklistTimelineEventsType)"
                    ></div>
                    {{ Label(worklistTimelineEventsType) }}
                  </div>
                </template>
              </div>
            </template>
          </div>
        </div>
      </div>
    </section>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

import { WorklistTimeline } from "@/models/WorklistTimeline";
import { WorklistTimelineEventsDay } from "@/models/WorklistTimelineEventsDay";
import { WorklistEventDetailItem } from "@/models/WorklistEventDetailItem";
import { WorklistEventType } from "@/models/enums/WorklistEventType";
import { WorklistTimelineEventsType } from "@/models/WorklistTimelineEventsType";

@Component({
  components: {},
})
export default class WorklistTimelineComponent extends Vue {
  private dateOn = true;

  private TimelineEventTitle = "";
  private TimelineEventDate = "";
  private DetailsItems: WorklistEventDetailItem[];

  private dailyPanelClass = "timeline-data";

  @Prop({ type: Array, default: [] })
  readonly worklistTimelineEventsDays: Array<WorklistTimelineEventsDay>;

  @Prop({ type: String, default: "" })
  readonly title: string;

  @Prop({ type: String, default: "" })
  readonly timelineTitle: string;

  DayClassName(worklistTimelineEventsDay: WorklistTimelineEventsDay): string {
    if (worklistTimelineEventsDay.DateZeroBased < 0) {
      return "item offset";
    } else {
      return "item m" + worklistTimelineEventsDay.DateMM;
    }
  }

  switchDate() {
    this.dateOn = !this.dateOn;
  }

  EventTypeClass(
    worklistTimelineEventsType: WorklistTimelineEventsType
  ): string {
    if (worklistTimelineEventsType.EventType == WorklistEventType.Milestone) {
      if (worklistTimelineEventsType.Position == "down") {
        return "label label-down";
      } else {
        return "label";
      }
    } else if (
      worklistTimelineEventsType.EventType == WorklistEventType.Minor
    ) {
      if (worklistTimelineEventsType.Position == "down") {
        return "minor minor-down";
      } else {
        return "minor";
      }
    } else {
      return "";
    }
  }

  Label(worklistTimelineEventsType: WorklistTimelineEventsType): string {
    if (worklistTimelineEventsType.EventType == WorklistEventType.Milestone) {
      return worklistTimelineEventsType.Label;
    } else {
      return "";
    }
  }

  EventDetails(worklistTimelineEventsType: WorklistTimelineEventsType): void {
    this.DetailsItems = worklistTimelineEventsType.EventDetailItems;
    this.TimelineEventTitle = worklistTimelineEventsType.Title;
    this.TimelineEventDate = worklistTimelineEventsType.Date;

    this.dailyPanelClass = "timeline-data is-open";

    if (worklistTimelineEventsType.EventType == WorklistEventType.Minor) {
      this.dailyPanelClass = this.dailyPanelClass + " minor";
    }
  }

  closeDailyPanel(): void {
    this.DetailsItems = [];

    this.TimelineEventTitle = "";
    this.TimelineEventDate = "";
    this.dailyPanelClass = "timeline-data";
  }
}
</script>
