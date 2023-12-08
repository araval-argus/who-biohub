<template>
  <div>
    <v-row>
      <v-col>
        <laboratories-map :public-page="false"></laboratories-map>

        <v-card class="mx-auto" max-width="100%" outlined color="transparent">
          <v-container>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <!-- <v-card flat color="transparent">
                  <v-img
                    alt="worklist"
                    title="worklist"
                    src="@/assets/icons/who-dashboard-page-over-kpis.png"
                  />
                </v-card> -->
                <ShipmentRequestsDashboardTable></ShipmentRequestsDashboardTable>
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </v-col>
    </v-row>
    <section class="biohub-kpi incoming-kpi">
      <h2>Incoming Shipments</h2>
      <h3>
        The following board represents average values for the incoming shipments
      </h3>
      <!-- <v-row>
        <v-col cols="12" md="12" lg="12"> -->
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageDaysBetweenRequestAndPickup }}
        </div>
        <div class="kpi-title">Days Between Request And Pickup</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageDaysBetweenRequestAndSMTASigning }}
        </div>
        <div class="kpi-title">Days Between Request And SMTA Signing</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageDaysBetweenRequestAndBookingFormSigning }}
        </div>
        <div class="kpi-title">
          Days Between Request And Booking Form Signing
        </div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageDaysBetweenBookingFormCourierReceiptAndPickup }}
        </div>
        <div class="kpi-title">
          Days Between Booking Form Courier Receipt And Pickup
        </div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageTotalTransportDaysOfSamples }}
        </div>
        <div class="kpi-title">Total Transport Days Of Samples</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageTotalDaysFromRequestToDelivery }}
        </div>
        <div class="kpi-title">Total Days From Request To Delivery</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ IncomingAverageGSDUploadingTime }}
        </div>
        <div class="kpi-title">
          Total Days From Delivery To GSD Confirmation
        </div>
      </div>
      <!-- </v-col>
      </v-row>
      <v-row class="biohub-kpi">
        <v-col cols="12" md="12" lg="12"> -->
    </section>
    <section class="biohub-kpi outgoing-kpi">
      <h2>Outgoing Shipments</h2>
      <h3>
        The following board represents average values for the outgoing shipments
      </h3>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ OutgoingAverageDaysBetweenRequestAndPickup }}
        </div>
        <div class="kpi-title">Days Between Request And Pickup</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ OutgoingAverageDaysBetweenRequestAndSMTASigning }}
        </div>
        <div class="kpi-title">Days Between Request And SMTA Signing</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ OutgoingAverageDaysBetweenRequestAndBookingFormSigning }}
        </div>
        <div class="kpi-title">
          Days Between Request And Booking Form Signing
        </div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ OutgoingAverageDaysBetweenBookingFormCourierReceiptAndPickup }}
        </div>
        <div class="kpi-title">
          Days Between Booking Form Courier Receipt And Pickup
        </div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ OutgoingAverageTotalTransportDaysOfSamples }}
        </div>
        <div class="kpi-title">Total Transport Days Of Samples</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">
          {{ OutgoingAverageTotalDaysFromRequestToDelivery }}
        </div>
        <div class="kpi-title">Total Days From Request To Delivery</div>
      </div>
      <!-- </v-col>
      </v-row> -->
    </section>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LaboratoriesMap from "@/features/laboratories/components/LaboratoriesMap.vue";
import MaterialsIndexList from "@/features/materials/components/MaterialsIndexList.vue";
import { AuthModule } from "../features/auth/store";
import ShipmentRequestsDashboardTable from "@/features/shipmentRequests/ShipmentRequestsDashboardTable.vue";
import { Kpi } from "@/models/Kpi";
import { KpiDataModule } from "../features/kpiDatas/store";

@Component({
  components: {
    LaboratoriesMap,
    MaterialsIndexList,
    ShipmentRequestsDashboardTable,
  },
})
export default class Index extends Vue {
  private search = "";

  get Kpi(): Kpi | undefined {
    return KpiDataModule.Kpi;
  }

  get IncomingAverageDaysBetweenRequestAndPickup(): number {
    return (
      this.Kpi?.IncomingShipmentInformation
        ?.AverageDaysBetweenRequestAndPickup ?? 0
    );
  }

  get IncomingAverageDaysBetweenRequestAndSMTASigning(): number {
    return (
      this.Kpi?.IncomingShipmentInformation
        ?.AverageDaysBetweenRequestAndSMTASigning ?? 0
    );
  }

  get IncomingAverageDaysBetweenRequestAndBookingFormSigning(): number {
    return (
      this.Kpi?.IncomingShipmentInformation
        ?.AverageDaysBetweenRequestAndBookingFormSigning ?? 0
    );
  }

  get IncomingAverageDaysBetweenBookingFormCourierReceiptAndPickup(): number {
    return (
      this.Kpi?.IncomingShipmentInformation
        ?.AverageDaysBetweenBookingFormCourierReceiptAndPickup ?? 0
    );
  }

  get IncomingAverageTotalTransportDaysOfSamples(): number {
    return (
      this.Kpi?.IncomingShipmentInformation
        ?.AverageTotalTransportDaysOfSamples ?? 0
    );
  }

  get IncomingAverageTotalDaysFromRequestToDelivery(): number {
    return (
      this.Kpi?.IncomingShipmentInformation
        ?.AverageTotalDaysFromRequestToDelivery ?? 0
    );
  }

  get IncomingAverageGSDUploadingTime(): number {
    return this.Kpi?.IncomingShipmentInformation?.AverageGSDUploadingTime ?? 0;
  }

  get OutgoingAverageDaysBetweenRequestAndPickup(): number {
    return (
      this.Kpi?.OutgoingShipmentInformation
        ?.AverageDaysBetweenRequestAndPickup ?? 0
    );
  }

  get OutgoingAverageDaysBetweenRequestAndSMTASigning(): number {
    return (
      this.Kpi?.OutgoingShipmentInformation
        ?.AverageDaysBetweenRequestAndSMTASigning ?? 0
    );
  }

  get OutgoingAverageDaysBetweenRequestAndBookingFormSigning(): number {
    return (
      this.Kpi?.OutgoingShipmentInformation
        ?.AverageDaysBetweenRequestAndBookingFormSigning ?? 0
    );
  }

  get OutgoingAverageDaysBetweenBookingFormCourierReceiptAndPickup(): number {
    return (
      this.Kpi?.OutgoingShipmentInformation
        ?.AverageDaysBetweenBookingFormCourierReceiptAndPickup ?? 0
    );
  }

  get OutgoingAverageTotalTransportDaysOfSamples(): number {
    return (
      this.Kpi?.OutgoingShipmentInformation
        ?.AverageTotalTransportDaysOfSamples ?? 0
    );
  }

  get OutgoingAverageTotalDaysFromRequestToDelivery(): number {
    return (
      this.Kpi?.OutgoingShipmentInformation
        ?.AverageTotalDaysFromRequestToDelivery ?? 0
    );
  }
  //TODO: Temporary solution to center map based on the navigation drawer mounting issue
  async mounted() {
    await KpiDataModule.ReadKpi();
    setTimeout(function () {
      window.dispatchEvent(new Event("resize"));
    }, 150);
  }
}
</script>
