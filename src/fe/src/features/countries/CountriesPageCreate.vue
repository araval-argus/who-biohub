<template>
  <v-container fluid>
    <CountryForm v-model="country" title="Create Country">
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </CountryForm>
  </v-container>
</template>

<script lang="ts">
import CountryForm from "./components/CountryForm.vue";
import CardActionsSaveCancel from "@/components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { CountryModule } from "./store";
import { Country } from "@/models/Country";

@Component({ components: { CountryForm, CardActionsSaveCancel } })
export default class CountriesPageEdit extends Vue {
  get country(): Country {
    return CountryModule.CountryCreate;
  }

  set country(country: Country) {
    CountryModule.SET_COUNTRY_CREATE(country);
  }

  async onSave(): Promise<void> {
    await CountryModule.CreateCountry();
    this.$router.back();
  }

  onCancel(): void {
    this.$router.back();
  }
}
</script>
