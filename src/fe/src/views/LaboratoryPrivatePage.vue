<template>
  <v-container>
    <v-row>
      <v-col>
        <laboratories-map :public-page="false"></laboratories-map>
        <v-card class="mx-auto" max-width="100%" outlined color="transparent">
          <v-container>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <ShipmentRequestsDashboardTable></ShipmentRequestsDashboardTable>
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LaboratoriesMap from "@/features/laboratories/components/LaboratoriesMap.vue";
import MaterialsIndexList from "@/features/materials/components/MaterialsIndexList.vue";
import { AuthModule } from "../features/auth/store";
import ShipmentRequestsDashboardTable from "@/features/shipmentRequests/ShipmentRequestsDashboardTable.vue";

@Component({
  components: {
    LaboratoriesMap,
    MaterialsIndexList,
    ShipmentRequestsDashboardTable,
  },
})
export default class Index extends Vue {
  private search = "";

  //TODO: Temporary solution to center map based on the navigation drawer mounting issue
  mounted() {
    setTimeout(function () {
      window.dispatchEvent(new Event("resize"));
    }, 150);
  }
}
</script>
