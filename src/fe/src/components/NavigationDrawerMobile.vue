<template>
  <div>
    <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>

    <v-navigation-drawer v-if="IsLogged" v-model="drawer">
      <v-list nav dense>
        <v-list-item-group
          v-model="group"
          active-class="blue--text text--accent-4"
        >
          <v-list-item two-line class="px-2" @click="drawer = !drawer">
            <v-list-item-content>
              <v-list-item-title>BioHub</v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-divider v-if="IsLogged"></v-divider>

          <div v-if="IsLogged">
            <div v-for="(m, k) in menu" :key="k">
              <v-list-item :key="m.title" :to="m.url" link>
                <v-list-item-content>
                  <v-list-item-title>{{ m.title }}</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </div>
          </div>

          <div v-if="canAccessRequestInitialization">
            <v-list-item
              class="featured-link"
              :to="shipmentRequestFormUrl"
              link
            >
              <v-list-item-content>
                <v-list-item-title>New Shipment Request</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item class="featured-link" :to="smtaRequestFormUrl" link>
              <v-list-item-content>
                <v-list-item-title>New SMTA Request</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </div>
          <div v-if="canAccessPastRequestInitialization">
            <v-list-item
              class="featured-link"
              :to="shipmentPastRequestFormUrl"
              link
            >
              <v-list-item-content>
                <v-list-item-title>New Shipment Request</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item
              class="featured-link"
              :to="smtaPastRequestFormUrl"
              link
            >
              <v-list-item-content>
                <v-list-item-title>New SMTA Request</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </div>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script lang="ts">
//import Vue from 'vue';
import { Vue, Component } from "vue-property-decorator";
import { AuthModule } from "../features/auth/store";
import { RoleType } from "@/models/enums/RoleType";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { hasPermission } from "../utils/helper";

@Component({
  data: () => ({
    navigation: {
      expandOnHover: true,
      miniVariant: true,
      dark: true,
    },
    drawerRight: null,
    right: false,
    left: false,
  }),
})
export default class NavigationDrawerMobile extends Vue {
  private drawer = false;

  get IsLogged(): boolean {
    return AuthModule.IsLogged;
  }

  get IsWHO(): boolean {
    return AuthModule.IsLogged && AuthModule.RoleType == RoleType.WHO;
  }

  get IsLabUser(): boolean {
    return AuthModule.IsLogged && AuthModule.RoleType == RoleType.Laboratory;
  }

  get IsBioHubUser(): boolean {
    return (
      AuthModule.IsLogged && AuthModule.RoleType == RoleType.BioHubFacility
    );
  }

  get LoggedUserImage(): string {
    return "";
  }

  get LoggedUserName(): string {
    return AuthModule.UserLoginInfo?.LoggedUserName ?? "";
  }

  get RoleName(): string {
    return AuthModule.UserLoginInfo?.RoleName ?? "";
  }

  get menu(): Array<{ title: string; url: string }> {
    if (this.IsLabUser) {
      return this.labMenu;
    } else if (this.IsBioHubUser) {
      return this.bioHubFacilityMenu;
    } else {
      return this.whoMenu;
    }
  }

  get canAccessRequestInitialization(): boolean {
    return hasPermission(PermissionNames.CanAccessRequestIniziation);
  }

  get canAccessPastRequestInitialization(): boolean {
    return hasPermission(PermissionNames.CanAccessPastRequestIniziation);
  }

  get shipmentRequestFormUrl(): string {
    if (this.IsWHO) {
      return "/whoarea/shipmentrequestform";
    } else if (this.IsLabUser) {
      return "/laboratoryarea/shipmentrequestform";
    } else if (this.IsBioHubUser) {
      return "/biohubfacilityarea/shipmentrequestform";
    } else {
      return "";
    }
  }

  get smtaRequestFormUrl(): string {
    if (this.IsWHO) {
      return "/whoarea/smtarequestform";
    } else if (this.IsLabUser) {
      return "/laboratoryarea/smtarequestform";
    } else if (this.IsBioHubUser) {
      return "/biohubfacilityarea/smtarequestform";
    } else {
      return "";
    }
  }

  get smtaPastRequestFormUrl(): string {
    if (this.IsWHO) {
      return "/whoarea/smtapastrequestform";
    } else if (this.IsLabUser) {
      return "/laboratoryarea/smtapastrequestform";
    } else if (this.IsBioHubUser) {
      return "/biohubfacilityarea/smtapastrequestform";
    } else {
      return "";
    }
  }

  get shipmentPastRequestFormUrl(): string {
    if (this.IsWHO) {
      return "/whoarea/shipmentpastrequestform";
    } else if (this.IsLabUser) {
      return "/laboratoryarea/shipmentpastrequestform";
    } else if (this.IsBioHubUser) {
      return "/biohubfacilityarea/shipmentpastrequestform";
    } else {
      return "";
    }
  }

  get whoMenu(): Array<{ title: string; url: string }> {
    let menu = [
      {
        title: "Dashboard",
        url: "/whoarea/dashboard",
      },
      {
        title: "BMEPP",
        url: "/whoarea/bmepp",
      },
      {
        title: "BioHub Facilities",
        url: "/whoarea/bioHubFacilities",
      },
      {
        title: "BioHub Users",
        url: "/whoarea/laboratories",
      },

      // For the moment it is hidden, maybe in the future it will be resumed
      // {
      //   title: "Biosafety Levels",
      //   url: "/whoarea/bslLevels",
      // },
      // {
      //   title: "Countries",
      //   url: "/whoarea/countries",
      // },
      {
        title: "Templates/WHO Guidance",
        url: "/whoarea/templates",
      },
      {
        title: "Completed Shipments",
        url: `/whoarea/shipments`,
      },
      {
        title: "Shipment Requests",
        url: `/whoarea/shipmentrequests`,
      },
      {
        title: "SMTA Requests",
        url: `/whoarea/smtarequests`,
      },
    ];

    if (hasPermission(PermissionNames.CanAccessWHOPendingRequest)) {
      menu.push({
        title: "User Access Requests",
        url: `/whoarea/userrequests`,
      });
    }

    if (hasPermission(PermissionNames.CanAccessWHOUsers)) {
      menu.push({
        title: "WHO Users",
        url: `/whoarea/users`,
      });
    }

    if (hasPermission(PermissionNames.CanReadCourier)) {
      menu.push({
        title: "Couriers",
        url: `/whoarea/couriers`,
      });
    }

    if (hasPermission(PermissionNames.CanReadDocument)) {
      menu.push({
        title: "Documents",
        url: `/whoarea/documents`,
      });
    }

    if (hasPermission(PermissionNames.CanReadEForm)) {
      menu.push({
        title: "E-Forms",
        url: `/whoarea/eforms`,
      });
    }

    if (hasPermission(PermissionNames.CanReadResource)) {
      menu.push({
        title: "Resources",
        url: `/whoarea/resources`,
      });
    }

    return menu;
  }

  get labMenu(): Array<{ title: string; url: string }> {
    let menu = [
      {
        title: "Dashboard",
        url: "/laboratoryarea/dashboard",
      },
      {
        title: "User Profile",
        url: `/laboratoryarea/profile`,
      },
      {
        title: "Facility/Institute Profile",
        url: `/laboratoryarea/info`,
      },
      {
        title: "Staff Members",
        url: `/laboratoryarea/staff`,
      },
      {
        title: "BMEPP Exchanged with BioHub",
        url: `/laboratoryarea/bmepp`,
      },
      {
        title: "BMEPP Catalogue",
        url: `/laboratoryarea/bmeppcatalogue`,
      },
      {
        title: "Templates/WHO Guidance",
        url: `/laboratoryarea/templates`,
      },
      {
        title: "Completed Shipments",
        url: `/laboratoryarea/shipments`,
      },
      {
        title: "Shipment Requests",
        url: `/laboratoryarea/shipmentrequests`,
      },
      {
        title: "SMTA Requests",
        url: `/laboratoryarea/smtarequests`,
      },
    ];

    // if (hasPermission(PermissionNames.CanAccessRequestIniziation)) {
    //   menu.push({
    //     title: "Shipment Request",
    //     url: `/laboratoryarea/shipmentrequestform`,
    //   });
    // }

    if (hasPermission(PermissionNames.CanReadDocument)) {
      menu.push({
        title: "Documents",
        url: `/laboratoryarea/documents`,
      });
    }

    if (hasPermission(PermissionNames.CanReadEForm)) {
      menu.push({
        title: "E-Forms",
        url: `/laboratoryarea/eforms`,
      });
    }

    return menu;
  }

  get bioHubFacilityMenu(): Array<{ title: string; url: string }> {
    let menu = [
      {
        title: "Dashboard",
        url: "/biohubfacilityarea/dashboard",
      },
      {
        title: "User Profile",
        url: `/biohubfacilityarea/profile`,
      },
      {
        title: "BioHub Users",
        url: "/biohubfacilityarea/laboratories",
      },
      {
        title: "BMEPP",
        url: `/biohubfacilityarea/bmepp`,
      },
      {
        title: "Templates/WHO Guidance",
        url: `/biohubfacilityarea/templates`,
      },
      {
        title: "Completed Shipments",
        url: `/biohubfacilityarea/shipments`,
      },
      {
        title: "Shipment Requests",
        url: `/biohubfacilityarea/shipmentrequests`,
      },
    ];

    if (hasPermission(PermissionNames.CanReadDocument)) {
      menu.push({
        title: "Documents",
        url: `/biohubfacilityarea/documents`,
      });
    }

    if (hasPermission(PermissionNames.CanReadEForm)) {
      menu.push({
        title: "E-Forms",
        url: `/biohubfacilityarea/eforms`,
      });
    }

    return menu;
  }
}
</script>
