<template>
  <div>
    <v-skeleton-loader v-if="loading"> </v-skeleton-loader>
    <div v-else>
      <span v-if="error"> Error retrieving Materials: {{ error }} </span>
      <MaterialsTablePublic
        v-else
        :provider-id="providerId"
        :loading="loading"
        @selected="selected"
      >
      </MaterialsTablePublic>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import MaterialsTablePublic from "./components/MaterialsTablePublic.vue";
import { MaterialModule } from "./store";
import { InternationalTaxonomyClassificationModule } from "../internationalTaxonomyClassifications/store";
import { LaboratoryModule } from "../laboratories/store";
import { CountryModule } from "../countries/store";
import { AppError } from "@/models/shared/Error";
import { MaterialPublic } from "@/models/MaterialPublic";
import { getMaterialProviderId } from "@/utils/helper";
import { AppModule } from "../../store/MainStore";

@Component({ components: { MaterialsTablePublic } })
export default class MaterialsPageIndexPublic extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  async loadPageInfo() {
    await InternationalTaxonomyClassificationModule.ListInternationalTaxonomyClassificationsPublic();
    await CountryModule.ListCountriesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await MaterialModule.ListMaterialsPublic();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get detailRouting(): Map<string, string> {
    var dictionary = new Map<string, string>();
    dictionary.set("public-materials", "public-material-details");
    dictionary.set(
      "public-materials-provider",
      "public-material-provider-details"
    );

    return dictionary;
  }

  get routingNamePrefix(): string {
    return "public-";
  }

  get providerId(): string {
    return getMaterialProviderId(this.$route.params.providerId);
  }
  get filterByProviderId(): boolean {
    return (
      this.$route.params.providerId != undefined &&
      this.$route.params.providerId != null &&
      this.$route.params.providerId != ""
    );
  }

  get isFromPublicBmeppLink(): boolean {
    if (this.$route.name == "public-materials-provider") {
      return true;
    }
    return false;
  }

  get error(): AppError | undefined {
    return MaterialModule.Error;
  }

  selected(item: MaterialPublic): void {
    var customParams = {};
    if (this.providerId == "") {
      customParams = {
        id: item.Id,
      };
    } else {
      customParams = {
        id: item.Id,
        providerId: this.providerId,
      };
    }

    if (this.$route.name != null && this.$route.name != undefined) {
      var route = this.detailRouting.get(this.$route.name);
      if (route != null && route != undefined) {
        this.$router.push({
          name: route,
          params: customParams,
        });
      } else {
        this.$router.push({
          name: "public-material-details",
          params: customParams,
        });
      }
    } else {
      this.$router.push({
        name: "public-material-details",
        params: customParams,
      });
    }
  }
}
</script>
