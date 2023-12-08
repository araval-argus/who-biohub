<template>
  <v-app>
    <header class="site-header">
      <v-container :class="headerClassName">
        <div class="site-brand">
          <img src="@/assets/icons/who-logo.svg" class="who-logo logo" />

          <img
            v-if="isLogged"
            src="@/assets/icons/biohub-logo.svg"
            class="biohub-logo logo"
          />
        </div>

        <nav class="site-navs">
          <div v-if="isLogged" class="user-nav">
            <img class="user-icon" src="@/assets/icons/user-profile.svg" />
            <div class="user-profile">
              <div class="user-name">
                {{ LoggedUserName }}
              </div>
              <div class="user-data">
                <span class="user-role">{{ RoleName }}</span>
              </div>
            </div>
          </div>

          <nav class="service-nav offset-menu">
            <nav-bar v-if="!isMobile" :is-public-page="isPublicPage" />
            <nav-bar-mobile v-else :is-public-page="isPublicPage" />
          </nav>
        </nav>
      </v-container>
    </header>

    <v-container :class="mainClassName">
      <nav class="main-nav offset-menu">
        <navigation-drawer
          v-if="isLogged && userLoginChecked && !isPublicPage && !isMobile"
        ></navigation-drawer>
        <navigation-drawer-mobile
          v-if="isLogged && userLoginChecked && !isPublicPage && isMobile"
        ></navigation-drawer-mobile>
      </nav>
      <v-main v-if="isPublicPage || userLoginChecked" class="main-content">
        <router-view>
          <home-view />
        </router-view>
      </v-main>
    </v-container>
    <alert-component
      v-for="alert in ErrorNotifications"
      :key="alert"
      type="error"
      :alert-text="alert"
    ></alert-component>
    <alert-component
      v-for="alert in SuccessNotifications"
      :key="alert"
      type="success"
      :alert-text="alert"
    ></alert-component>
    <loading-component :show-loading="ShowLoading"></loading-component>
    <footer class="site-footer">
      <container class="boxed">
        <nav class="info-nav">
          <a href="https://www.who.int/about/policies/privacy" target="_blank"
            ><u>Privacy Legal Notice</u></a
          >
        </nav>

        <div class="imprint">© 2023 World Health Organization</div>
      </container>
    </footer>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import AlertComponent from "./components/AlertComponent.vue";
import LoadingComponent from "./components/LoadingComponent.vue";
import NavBar from "./components/NavBar.vue";
import NavBarMobile from "./components/NavBarMobile.vue";
import NavigationDrawer from "./components/NavigationDrawer.vue";
import NavigationDrawerMobile from "./components/NavigationDrawerMobile.vue";
import { AuthModule } from "./features/auth/store";
import { AreaType } from "./models/enums/AreaType";
import { AppModule } from "./store/MainStore";
import {
  IsMobile,
  getAreaFromRouteName,
  getAreaFromRoleType,
  canAccessThisArea,
} from "./utils/helper";
import HomeView from "./views/HomeView.vue";
import "@mdi/font/css/materialdesignicons.css";
import "@mdi/font/css/materialdesignicons.min.css";

export const vers = process.env.VUE_APP_VERS;

@Component({
  components: {
    HomeView,
    NavBar,
    NavBarMobile,
    NavigationDrawer,
    NavigationDrawerMobile,
    AlertComponent,
    LoadingComponent,
  },
})
export default class App extends Vue {
  private userLoginChecked = false;

  get area() {
    const areaType = getAreaFromRouteName(this.RouteName);
    let areaString = "";
    if (areaType == AreaType.WHO) {
      areaString = "whoarea";
    } else if (areaType == AreaType.BioHubFacility) {
      areaString = "biohubfacilityarea";
    } else if (areaType == AreaType.Laboratory) {
      areaString = "laboratoryarea";
    } else if (areaType == AreaType.Public) {
      areaString = "publicarea";
    }

    return areaString;
  }

  get headerClassName() {
    let className =
      "pl-5 scrollable-content boxed " + this.area + " " + this.RouteName;

    className = className.replace("whoarea-", "");
    className = className.replace("laboratoryarea-", "");
    className = className.replace("biohubfacilityarea-", "");

    return className;
  }

  get mainClassName() {
    let className = "main-container boxed " + this.area + " " + this.RouteName;
    if (
      this.RouteName == "whoarea" ||
      this.RouteName == "laboratoryarea" ||
      this.RouteName == "biohubfacilityarea"
    ) {
      className = "dashboard " + className;
    }

    className = className.replace("whoarea-", "");
    className = className.replace("laboratoryarea-", "");
    className = className.replace("biohubfacilityarea-", "");

    return className;
  }

  get RouteName() {
    return this.$route.name ?? "";
  }

  get isMobile(): boolean {
    return IsMobile(this.$vuetify.breakpoint.name);
  }

  get isPublicPage(): boolean {
    return getAreaFromRouteName(this.RouteName) == AreaType.Public;
  }

  get isLogged() {
    return AuthModule.IsLogged;
  }

  get ErrorNotifications() {
    return AppModule.ErrorNotifications;
  }

  get SuccessNotifications() {
    return AppModule.SuccessNotifications;
  }

  get ShowLoading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get LoggedUserName(): string {
    return AuthModule.UserLoginInfo?.LoggedUserName ?? "";
  }

  get RoleName(): string {
    return AuthModule.UserLoginInfo?.RoleName ?? "";
  }

  async handleHardReload(url: string) {
    await fetch(url, {
      headers: {
        Pragma: "no-cache",
        Expires: "-1",
        "Cache-Control": "no-cache",
      },
    });
    url = url + "?ts=" + new Date().getTime().toString();
    window.location.href = url;

    window.location.reload();
  }

  async checkRefresh() {
    const v = vers || "default";
    if (v !== localStorage.getItem("vers")) {
      localStorage.setItem("vers", v);
      await this.handleHardReload(window.location.href);
    }
  }

  async created() {
    await this.checkRefresh();
    if (AuthModule.UserToken == undefined) {
      await AuthModule.setUserTokenAsync();
    }
    if (AuthModule.UserLoginInfo == undefined) {
      await AuthModule.setUserInfoAsync()
        .then((response) => {
          const isAllowed = canAccessThisArea(this.RouteName);
          if (isAllowed == false) {
            this.$router.push({
              name: "forbidden",
            });
          }
        })
        .catch((err) => {
          if (err.code == 401) {
            this.toRoute("unauthorized");
          } else if (err.code == 403) {
            this.toRoute("forbidden");
          } else {
            this.toRoute("home");
          }
        });
    }
    this.userLoginChecked = true;
  }

  toRoute(routeName: string): void {
    if (this.$route.name != routeName) {
      this.$router.push({ name: routeName });
    }
  }

  @Watch("RouteName")
  async routeNameChanged() {
    await this.checkRefresh();
    AppModule.ShowLoading();
    this.userLoginChecked = false;
    try {
      if (AuthModule.UserToken == undefined) {
        await AuthModule.setUserTokenAsync();
      }
      await AuthModule.setUserInfoAsync()
        .then(() => {
          const isAllowed = canAccessThisArea(this.RouteName);
          if (isAllowed == false) {
            this.toRoute("forbidden");
          }
        })
        .catch((err) => {
          if (getAreaFromRouteName(this.RouteName) != AreaType.Public) {
            if (err.code == 401) {
              this.toRoute("unauthorized");
            } else if (err.code == 403) {
              this.toRoute("forbidden");
            } else {
              this.toRoute("home");
            }
          }
        });
    } catch {
      this.toRoute("home");
    }
    AppModule.HideLoading();
    this.userLoginChecked = true;
  }
}
</script>

<style scoped>
.logo {
  margin-top: -10px;
}
.v-alert {
  position: fixed;
  right: 5%;
  top: 0%;
}
</style>
