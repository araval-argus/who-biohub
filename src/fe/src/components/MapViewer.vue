<template>
  <div>
    <l-map
      ref="map"
      style="height: 450px"
      :zoom="zoom"
      @click="clickClose"
      @update:zoom="zoomEvent"
      :min-zoom="minZoom"
      :max-bounds="maxBounds"
      :center="center"
      :options="{ scrollWheelZoom: true }"
    >
      <l-control-layers
        position="topright"
        :collapsed="false"
      ></l-control-layers>
      <l-layer-group
        v-for="group in groups"
        :key="group"
        layer-type="overlay"
        :name="group"
      >
        <l-marker
          v-for="markerInfo in markersInfosbyGroupName(group)"
          :key="markerInfo"
          :lat-lng="markerInfo.coordinates"
          :icon="markerInfo.icon"
          @click="click(markerInfo)"
        >
          <l-popup>
            <h2>{{ markerInfo.popupHeader }}</h2>
            <div>
              <p>{{ markerInfo.popupAddress }}</p>
            </div>
            <div>
              <h3>{{ markerInfo.popupCountry }}</h3>
            </div>
            <footer>
              <div
                v-for="popupFooter in markerInfo.popupFooter"
                :key="popupFooter.PopupFooterLink"
                style="text-align: right"
              >
                <a :href="popupFooter.PopupFooterLink">{{
                  popupFooter.PopupFooterName
                }}</a>
              </div>
            </footer>
          </l-popup>
        </l-marker>
      </l-layer-group>
      <div class="map-legend">
        <div class="map-legend-item legend-item-pro">
          <img
            class="map-legend-icon"
            src="../assets/maps/marker-yellow.png"
            alt=""
          />
          <div class="map-legend-label">Laboratory</div>
        </div>
        <div class="map-legend-item legend-item-pro">
          <img
            class="map-legend-icon"
            src="../assets/maps/icon-out-yellow.png"
            alt=""
          />
          <div class="map-legend-label">Provider</div>
        </div>
        <div class="map-legend-item legend-item-qe">
          <img
            class="map-legend-icon"
            src="../assets/maps/icon-in-yellow.png"
            alt=""
          />
          <div class="map-legend-label">QE</div>
        </div>
        <div class="map-legend-item legend-item-both">
          <img
            class="map-legend-icon"
            src="../assets/maps/icon-in-out-yellow.png"
            alt=""
          />
          <div class="map-legend-label">Provider &amp; QE</div>
        </div>
      </div>
    </l-map>
  </div>
</template>

<script lang="ts">
import L from "leaflet";
import { curve, Curve } from "leaflet"; // for TypeScript
import "@elfalem/leaflet-curve";
import { LatLng, latLng, icon, Icon, Marker, latLngBounds } from "leaflet";
import {
  LMap,
  LTileLayer,
  LControlLayers,
  LLayerGroup,
  LMarker,
  LPopup,
  LTooltip,
  LIcon,
} from "vue2-leaflet";
import { TiledMapLayer } from "esri-leaflet";
import { Component, Vue, Prop } from "vue-property-decorator";
import "leaflet/dist/leaflet.css";
import { MapElementInfo } from "@/models/shared/MapElementInfo";
import { PopupFooter } from "@/models/shared/PopupFooter";
import { Coordinates } from "@/models/shared/Coordinates";

type D = Icon.Default & {
  _getIconUrl?: string;
};

delete (Icon.Default.prototype as D)._getIconUrl;

class MarkerInfo {
  coordinates: LatLng;
  popupHeader: string;
  popupAddress: string;
  popupCountry: string;
  popupFooter: Array<PopupFooter>;
  group: string;
  icon: object;
  ToBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  FromBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  constructor(
    coordinates: LatLng,
    popupHeader: string,
    popupAddress: string,
    popupCountry: string,
    popupFooter: Array<PopupFooter>,
    Group: string,
    icon: object,
    toBioHubConnectedInstitutesLatLng: Array<Coordinates>,
    fromBioHubConnectedInstitutesLatLng: Array<Coordinates>
  ) {
    this.coordinates = coordinates;
    this.popupHeader = popupHeader;
    this.popupAddress = popupAddress;
    this.popupCountry = popupCountry;
    this.popupFooter = popupFooter;
    this.group = Group;
    this.icon = icon;
    this.ToBioHubConnectedInstitutesLatLng = toBioHubConnectedInstitutesLatLng;
    this.FromBioHubConnectedInstitutesLatLng =
      fromBioHubConnectedInstitutesLatLng;
  }
}

@Component({
  components: {
    LMap,
    LTileLayer,
    LLayerGroup,
    LControlLayers,
    LMarker,
    LPopup,
    LTooltip,
    LIcon,
  },
})
export default class MapViewer extends Vue {
  layerGroup: L.LayerGroup;

  showShipmentConnections = false;

  @Prop({ required: true, type: Array, default: [] })
  readonly mapElementInfoList: Array<MapElementInfo>;

  $refs!: {
    map: LMap;
  };

  mounted() {
    this.$refs.map.mapObject.addLayer(
      new TiledMapLayer({
        url: "https://tiles.arcgis.com/tiles/5T5nSi527N4F7luB/arcgis/rest/services/WHO_Polygon_Raster_Basemap_with_labels/MapServer",
      })
    );

    this.$refs.map.mapObject.addLayer(
      new TiledMapLayer({
        url: "https://tiles.arcgis.com/tiles/5T5nSi527N4F7luB/arcgis/rest/services/WHO_Polygon_Raster_Disputed_Areas_and_Borders/MapServer",
      })
    );
  }

  markersInfosbyGroupName(name: string): Array<MarkerInfo> {
    var mapElementInfoByNameList = new Array<MarkerInfo>();

    var selectedInfoList = this.mapElementInfoList.filter((elem) => {
      return elem.Group == name;
    });
    const iconBlue = this.iconBlue;
    const iconRed = this.iconRed;
    const iconPurple = this.iconPurple;
    const iconYellow = this.iconYellow;
    const iconYellowIn = this.iconYellowIn;
    const iconYellowOut = this.iconYellowOut;
    const iconYellowInOut = this.iconYellowInOut;

    selectedInfoList.forEach(function (value) {
      var icon = iconBlue;
      if (value.IconColor == "red") {
        icon = iconRed;
      } else if (value.IconColor == "purple") {
        icon = iconPurple;
      } else if (value.IconColor == "yellow") {
        icon = iconYellow;
      } else if (value.IconColor == "yellow-in") {
        icon = iconYellowIn;
      } else if (value.IconColor == "yellow-out") {
        icon = iconYellowOut;
      } else if (value.IconColor == "yellow-in-out") {
        icon = iconYellowInOut;
      }

      mapElementInfoByNameList.push(
        new MarkerInfo(
          latLng(value.Coordinates.Latitude, value.Coordinates.Longitude),
          value.PopupHeader,
          value.PopupAddress,
          value.PopupCountry,
          value.PopupFooter,
          value.Group,
          icon,
          value.ToBioHubConnectedInstitutesLatLng,
          value.FromBioHubConnectedInstitutesLatLng
        )
      );
    });

    return mapElementInfoByNameList;
  }

  click(info: MarkerInfo) {
    if (this.showShipmentConnections == true) {
      this.layerGroup.removeFrom(this.$refs.map.mapObject);
    }

    this.showShipmentConnections = true;

    this.layerGroup = L.layerGroup([]);

    info.ToBioHubConnectedInstitutesLatLng.forEach((x) => {
      const startingLatitude =
        info.group == "Laboratory" ? info.coordinates.lat : x.Latitude;
      const startingLongitude =
        info.group == "Laboratory" ? info.coordinates.lng : x.Longitude;

      const endingLatitude =
        info.group == "Laboratory" ? x.Latitude : info.coordinates.lat;
      const endingLongitude =
        info.group == "Laboratory" ? x.Longitude : info.coordinates.lng;

      // const color = "rgb(244,168,029)";

      const color = "rgb(121,181,227)";

      this.createCurve(
        startingLatitude,
        startingLongitude,
        endingLatitude,
        endingLongitude,
        color
      );
    });

    info.FromBioHubConnectedInstitutesLatLng.forEach((x) => {
      const startingLatitude =
        info.group == "Laboratory" ? x.Latitude : info.coordinates.lat;
      const startingLongitude =
        info.group == "Laboratory" ? x.Longitude : info.coordinates.lng;

      const endingLatitude =
        info.group == "Laboratory" ? info.coordinates.lat : x.Latitude;
      const endingLongitude =
        info.group == "Laboratory" ? info.coordinates.lng : x.Longitude;

      // const color = "rgb(166,034,140)";

      const color = "rgb(170,207,127)";

      this.createCurve(
        startingLatitude,
        startingLongitude,
        endingLatitude,
        endingLongitude,
        color
      );
    });
  }

  createCurve(
    startingLatitude: number,
    startingLongitude: number,
    endingLatitude: number,
    endingLongitude: number,
    color: string
  ) {
    let offsetX = 0;
    let offsetY = 0;
    let r = 0;
    let theta = 0;
    const thetaOffset = 3.14 / 10;
    let r2 = 0;
    let theta2 = 0;
    let midpointX = 0;
    let midpointY = 0;

    offsetX = endingLatitude - startingLatitude;
    offsetY = endingLongitude - startingLongitude;

    (r = Math.sqrt(Math.pow(offsetX, 2) + Math.pow(offsetY, 2))),
      (theta = Math.atan2(offsetY, offsetX));

    (r2 = r / 2 / Math.cos(thetaOffset)), (theta2 = theta + thetaOffset);

    (midpointX = r2 * Math.cos(theta2) + startingLatitude),
      (midpointY = r2 * Math.sin(theta2) + startingLongitude);

    var pathOptions = {
      color: color,
      weight: 4,
      //dashArray: "20",
      animate: {
        duration: 7000,
        iterations: Infinity,
        easing: "ease-in-out",
        //direction: "alternate",
      },
    };

    L.curve(
      [
        "M",
        [startingLatitude, startingLongitude],
        "Q",
        [midpointX, midpointY],
        [endingLatitude, endingLongitude],
      ],
      pathOptions
    ).addTo(this.layerGroup);
    this.layerGroup.addTo(this.$refs.map.mapObject);
  }

  clickClose() {
    this.showShipmentConnections = false;
    this.layerGroup.removeFrom(this.$refs.map.mapObject);
    this.layerGroup = L.layerGroup([]);
  }

  zoomEvent() {
    if (this.showShipmentConnections) {
      this.layerGroup.removeFrom(this.$refs.map.mapObject);
      this.layerGroup.addTo(this.$refs.map.mapObject);
    }
  }

  get groups(): Array<string> {
    var groupsList = new Array<string>();

    const distinctGroupsByName = this.mapElementInfoList.filter(
      (elem, i, arr) => arr.findIndex((t) => t.Group === elem.Group) === i
    );

    distinctGroupsByName.forEach((elem, i) => {
      groupsList.push(elem.Group);
    });
    return groupsList;
  }

  private zoom = 3;
  private minZoom = 2.3;

  private maxBounds = latLngBounds(latLng(-90, -180), latLng(90, 180));

  private iconBlue = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/marker-icon-blue.png"),
    iconRetinaUrl: require("../assets/maps/marker-icon-2x-blue.png"),
    shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private iconRed = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/marker-icon-red.png"),
    iconRetinaUrl: require("../assets/maps/marker-icon-2x-red.png"),
    shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private iconYellow = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/marker-yellow.png"),
    //iconRetinaUrl: require("../assets/maps/marker-yellow.png"),
    // shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [27, 50],
    iconAnchor: [13.5, 50],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private iconYellowIn = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/icon-in-yellow.png"),
    //iconRetinaUrl: require("../assets/maps/marker-yellow.png"),
    // shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [27, 50],
    iconAnchor: [13.5, 50],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private iconYellowOut = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/icon-out-yellow.png"),
    //iconRetinaUrl: require("../assets/maps/marker-yellow.png"),
    // shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [27, 50],
    iconAnchor: [13.5, 50],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private iconYellowInOut = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/icon-in-out-yellow.png"),
    //iconRetinaUrl: require("../assets/maps/marker-yellow.png"),
    // shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [27, 50],
    iconAnchor: [13.5, 50],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private iconPurple = (Marker.prototype.options.icon = icon({
    iconUrl: require("../assets/maps/marker-purple.png"),
    //iconRetinaUrl: require("../assets/maps/marker-purple.png"),
    // shadowUrl: require("../assets/maps/marker-shadow.png"),
    iconSize: [27, 50],
    iconAnchor: [13.5, 50],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  }));

  private center = latLng(46.6904905167597, 7.6438726903634);
}
</script>
<style lang="scss">
.vue2leaflet-map {
  z-index: 1;
}
</style>
