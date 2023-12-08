<template>
  <v-container fluid class="laboratories">
    <map-viewer :map-element-info-list="mapElementInfoList"></map-viewer>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { MapElementInfo } from "@/models/shared/MapElementInfo";
import { PopupFooter } from "@/models/shared/PopupFooter";

import MapViewer from "@/components/MapViewer.vue";
import { LaboratoryModule } from "../store";
import { CountryModule } from "../../countries/store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import { AppModule } from "../../../store/MainStore";
import { InstituteType } from "@/models/enums/InstituteType";

@Component({ components: { MapViewer } })
export default class LaboratoriesMapPublic extends Vue {
  get mapElementInfoList(): Array<MapElementInfo> {
    const laboratories = LaboratoryModule.LaboratoriesMapPublic;

    const bioHubFacilities = BioHubFacilityModule.BioHubFacilitiesMapPublic;

    const mapElementInfoList = new Array<MapElementInfo>();

    const routeName = "public";

    laboratories.forEach(function (value) {
      var country = CountryModule.Countries.filter((country) => {
        return country.Id == value.CountryId;
      }).map((l) => {
        return {
          countryName: l.Name,
        };
      });

      const materialLink =
        "/#/" + routeName + "/bmeppcatalogue/" + value.Id + "/provider";

      var popupFooter = new Array<PopupFooter>();
      popupFooter.push(
        new PopupFooter(
          "/#/" + routeName + "/shipments/" + value.Id + "/laboratory",
          "Shipments"
        )
      );

      popupFooter.push(new PopupFooter(materialLink, "BMEPP"));

      let color = "yellow";

      if (value.InstituteType == InstituteType.Provider) {
        color = "yellow-out";
      } else if (value.InstituteType == InstituteType.QE) {
        color = "yellow-in";
      } else if (value.InstituteType == InstituteType.ProviderQE) {
        color = "yellow-in-out";
      }

      mapElementInfoList.push(
        new MapElementInfo(
          value.Latitude,
          value.Longitude,
          value.Name + " - " + value.Abbreviation,
          value.Address,
          country[0].countryName,
          popupFooter,
          "Laboratory",
          color,
          value.ToBioHubConnectedInstitutesLatLng,
          value.FromBioHubConnectedInstitutesLatLng
        )
      );
    });

    bioHubFacilities.forEach(function (value) {
      var country = CountryModule.Countries.filter((country) => {
        return country.Id == value.CountryId;
      }).map((l) => {
        return {
          countryName: l.Name,
        };
      });

      const materialLink =
        "/#/" + routeName + "/bmeppcatalogue/" + value.Id + "/provider";

      var popupFooter = new Array<PopupFooter>();

      popupFooter.push(
        new PopupFooter(
          "/#/" + routeName + "/shipments/" + value.Id + "/laboratory",
          "Shipments"
        )
      );
      popupFooter.push(new PopupFooter(materialLink, "BMEPP"));
      mapElementInfoList.push(
        new MapElementInfo(
          value.Latitude,
          value.Longitude,
          value.Name + " - " + value.Abbreviation,
          value.Address,
          country[0].countryName,
          popupFooter,
          "BioHub Facility",
          "purple",
          value.ToBioHubConnectedInstitutesLatLng,
          value.FromBioHubConnectedInstitutesLatLng
        )
      );
    });

    return mapElementInfoList;
  }

  async mounted() {
    try {
      await LaboratoryModule.ListLaboratoriesMapPublic();
      await BioHubFacilityModule.ListBioHubFacilitiesMapPublic();
      await CountryModule.ListCountriesPublic();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
