<template>
  <v-container fluid class="laboratories">
    <map-viewer :map-element-info-list="mapElementInfoList"></map-viewer>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { MapElementInfo } from "@/models/shared/MapElementInfo";
import { PopupFooter } from "@/models/shared/PopupFooter";
import { InstituteType } from "@/models/enums/InstituteType";

import MapViewer from "@/components/MapViewer.vue";
import { LaboratoryModule } from "../store";
import { CountryModule } from "../../countries/store";
import { BioHubFacilityModule } from "../../biohubfacilities/store";
import { AppModule } from "../../../store/MainStore";
import { AuthModule } from "../../../features/auth/store";

@Component({ components: { MapViewer } })
export default class LaboratoriesMap extends Vue {
  @Prop({ type: Boolean, default: true })
  readonly publicPage: boolean;

  get mapElementInfoList(): Array<MapElementInfo> {
    const userLaboratoryId = AuthModule.LaboratoryId?.toLowerCase() ?? "";

    const publicPage = this.publicPage;

    var laboratories =
      publicPage == true
        ? LaboratoryModule.LaboratoriesMapPublic
        : LaboratoryModule.LaboratoriesMap;

    var bioHubFacilities =
      publicPage == true
        ? BioHubFacilityModule.BioHubFacilitiesMapPublic
        : BioHubFacilityModule.BioHubFacilitiesMap;

    var mapElementInfoList = new Array<MapElementInfo>();

    var routeName = "public";

    if (
      this.$route.name == "whoarea" ||
      this.$route.name == "laboratoryarea" ||
      this.$route.name == "biohubfacilityarea"
    ) {
      routeName = this.$route.name;
    }

    laboratories.forEach(function (value) {
      var country = CountryModule.Countries.filter((country) => {
        return country.Id == value.CountryId;
      }).map((l) => {
        return {
          countryName: l.Name,
        };
      });

      var materialLink = "/#/" + routeName + "/bmepp/" + value.Id + "/provider";

      if (publicPage == true) {
        materialLink =
          "/#/" + routeName + "/bmeppcatalogue/" + value.Id + "/provider";
      } else if (userLaboratoryId != "") {
        if (userLaboratoryId != value.Id) {
          materialLink =
            "/#/" + routeName + "/bmeppcatalogue/" + value.Id + "/provider";
        } else {
          materialLink = "/#/" + routeName + "/bmepp";
        }
      }

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

      var materialLink = "/#/" + routeName + "/bmepp/" + value.Id + "/provider";

      if (publicPage == true) {
        materialLink =
          "/#/" + routeName + "/bmeppcatalogue/" + value.Id + "/provider";
      } else if (userLaboratoryId != "") {
        if (userLaboratoryId != value.Id) {
          materialLink =
            "/#/" + routeName + "/bmeppcatalogue/" + value.Id + "/provider";
        } else {
          materialLink = "/#/" + routeName + "/bmepp";
        }
      }

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
      if (this.publicPage == true) {
        await LaboratoryModule.ListLaboratoriesMapPublic();
        await BioHubFacilityModule.ListBioHubFacilitiesMapPublic();
        await CountryModule.ListCountriesPublic();
      } else {
        await LaboratoryModule.ListLaboratoriesMap();
        await BioHubFacilityModule.ListBioHubFacilitiesMap();
        await CountryModule.ListCountries();
      }
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
