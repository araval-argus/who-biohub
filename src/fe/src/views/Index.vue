<template>
  <div class="public-content">
    <div class="hero-image">
      <div class="boxed">
        <img src="@/assets/hero.png" alt="" />
      </div>
    </div>

    <section class="biohub-about">
      <h2>About WHO BioHub System Operational Platform</h2>
      <p>
        The WHO BioHub System Operational Platform aims to support a range of
        operations in a secure, timely, efficient and transparent manner. The
        platform enables primarily the following activities (with some features
        visible on this public page while others can be accessed after user
        registration) [<a
          href="https://www.who.int/initiatives/who-biohub"
          target="_blank"
          title="Read More on WHO website"
          >Read More</a
        >]
      </p>
    </section>

    <section class="biohub-kpi">
      <div class="kpi-card">
        <div class="kpi-value">{{ IncomingShipments }}</div>
        <div class="kpi-title">Incoming shipments</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">{{ OutgoingShipments }}</div>
        <div class="kpi-title">Outgoing shipments</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">{{ CountryNumber }}</div>
        <div class="kpi-title">Countries Participating</div>
      </div>
      <div class="kpi-card">
        <div class="kpi-value">{{ MaterialNumber }}</div>
        <div class="kpi-title">
          Successfully Cultured and Sharable Materials
        </div>
      </div>
    </section>

    <section class="biohub-map">
      <h2>WHO BioHub System Operational Map with Partners and Users</h2>
      <p>
        The map visualizes the operations linked to the WHO BioHub System
        partners and users (as they are defined in the current pilot-testing
        phase): (a) WHO BioHub Facilities (in purple on the map) who receive,
        store, grow, sequence and distribute upon request, the BMEPP; (b)
        Providers of BMEPP who provide BMEPP on a voluntary basis; (c) Qualified
        Entities who may receive BMEPP from the WHO BioHub Facility solely for
        non-commercial use. Clicking on one of these stakeholders will enable
        visualisation of all their related shipments.
      </p>

      <laboratories-map-public></laboratories-map-public>
    </section>

    <section class="biohub-catalogue">
      <h2>BMEPP Catalogue</h2>
      <p>
        The catalogue provides an overview of all available Biological Materials
        with Epidemic or Pandemic Potential (BMEPP) within the WHO BioHub
        System. Any parties interested in receiving a specific BMEPP found in
        the catalogue can do so after registering with the WHO BioHub System
        Operational Platform.
      </p>

      <materials-index-list></materials-index-list>
    </section>

    <section class="biohub-access">
      <h2>Request for Accessing Platform</h2>
      <p>
        Parties interested in using the WHO BioHub System Operational Platform
        should click the “Request” button below to first register, and then
        follow subsequent instructions.
      </p>

      <div class="access-request">
        <a href="#/userrequest">
          <img src="../assets/access-request.png" alt="" />

          <button>Access Request</button>
        </a>
      </div>
    </section>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LaboratoriesMapPublic from "@/features/laboratories/components/LaboratoriesMapPublic.vue";
import MaterialsIndexList from "@/features/materials/components/MaterialsIndexList.vue";
import DisclaimerMockupImages from "../views/DisclaimerMockupImages.vue";
import { KpiDataPublic } from "@/models/KpiDataPublic";
import { KpiDataModule } from "../features/kpiDatas/store";

@Component({
  components: {
    LaboratoriesMapPublic,
    MaterialsIndexList,
    DisclaimerMockupImages,
  },
})
export default class Index extends Vue {
  private search = "";

  get KpiDataPublic(): KpiDataPublic | undefined {
    return KpiDataModule.KpiDataPublic;
  }

  get IncomingShipments(): number {
    return this.KpiDataPublic?.IncomingShipments ?? 0;
  }

  get OutgoingShipments(): number {
    return this.KpiDataPublic?.OutgoingShipments ?? 0;
  }

  get CountryNumber(): number {
    return this.KpiDataPublic?.CountryNumber ?? 0;
  }

  get MaterialNumber(): number {
    return this.KpiDataPublic?.MaterialNumber ?? 0;
  }

  //TODO: Temporary solution to center map based on the navigation drawer mounting issue
  async mounted() {
    await KpiDataModule.ReadKpiDataPublic();
    setTimeout(function () {
      window.dispatchEvent(new Event("resize"));
    }, 150);
  }
}
</script>
