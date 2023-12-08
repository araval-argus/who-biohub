<template>
  <v-navigation-drawer v-if="IsLogged" v-model="drawer">
    <div v-if="IsLogged">
      <div v-for="(m, k) in menu" :key="k">
        <v-list-item :key="m.title" :to="m.url" link>
          <v-list-item-content>
            <v-list-item-title>{{ m.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </div>
    </div>

    <div v-if="IsLogged && IsLabUser">
      <div v-if="canAccessRequestInitialization">
        <v-list-item class="featured-link" :to="shipmentRequestFormUrl" link>
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
            <v-list-item-title>Create Past Shipment Request</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item class="featured-link" :to="smtaPastRequestFormUrl" link>
          <v-list-item-content>
            <v-list-item-title>Create Past SMTA Request</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </div>
    </div>
  </v-navigation-drawer>
</template>

<script lang="ts">
//import Vue from 'vue';
import { Vue, Component } from "vue-property-decorator";
import { AuthModule } from "../features/auth/store";
import { RoleType } from "@/models/enums/RoleType";
import router from "@/router";
import { hasPermission } from "../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

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
export default class NavigationDrawer extends Vue {
  private drawer = true;

  smtaSubmission(): void {
    router.push({ name: "laboratoryarea-smta-submission" });
  }
  shipmentRequest(): void {
    router.push({ name: "laboratoryarea-shipment-request-form" });
  }

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

  get canAccessRequestInitialization(): boolean {
    return hasPermission(PermissionNames.CanAccessRequestIniziation);
  }

  get canAccessPastRequestInitialization(): boolean {
    return hasPermission(PermissionNames.CanAccessPastRequestIniziation);
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
    ];

    if (hasPermission(PermissionNames.CanAccessSMTAWorkflow)) {
      menu.push({
        title: "SMTA Requests",
        url: `/whoarea/smtarequests`,
      });
    }

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
        title: "Resources (Public page)",
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
    ];

    if (hasPermission(PermissionNames.CanAccessSMTAWorkflow)) {
      menu.push({
        title: "SMTA Requests",
        url: `/laboratoryarea/smtarequests`,
      });
    }

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
        title: "BioHub Facility Staff",
        url: `/biohubfacilityarea/staff`,
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

<style>
.logo {
  max-width: 80%;
  height: auto;
}

.logoVertical {
  max-width: 110%;
  height: auto;
}

.whoTitleClass {
  margin-top: 20px;
}
</style>
