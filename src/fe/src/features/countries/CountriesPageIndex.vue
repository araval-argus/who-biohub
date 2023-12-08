<template>
  <div>
    <span v-if="error"> Error retrieving countries: {{ error }} </span>
    <CountriesTable
      v-else
      :loading="loading"
      @selected="selected"
      @create="create"
      @edit="editItem"
      @delete="deleteItem"
    >
    </CountriesTable>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CountriesTable from "./components/CountriesTable.vue";
import { CountryModule } from "./store";
import { AppError } from "@/models/shared/Error";
import { Country } from "@/models/Country";
import { AppModule } from "../../store/MainStore";

@Component({ components: { CountriesTable } })
export default class CountriesPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  async mounted() {
    try {
      await CountryModule.ListCountries();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get error(): AppError | undefined {
    return CountryModule.Error;
  }

  editItem(item: Country): void {
    CountryModule.SET_COUNTRY(item);
    this.$router.push({
      name: "whoarea-country-edit",
      params: { id: item.Id },
    });
  }

  selected(item: Country): void {
    CountryModule.SET_COUNTRY(item);

    this.$router.push({
      name: "whoarea-country-details",
      params: { id: item.Id },
    });
  }

  create(): void {
    this.$router.push({
      name: "whoarea-country-create",
    });
  }

  async deleteItem(item: Country): Promise<void> {
    CountryModule.SET_COUNTRY(item);
    await CountryModule.DeleteCountry();
    await CountryModule.ListCountries();
    return;
  }
}
</script>
