<template>
  <CountryForm
    v-if="country"
    v-model="country"
    title="Country Details"
    readonly
  >
  </CountryForm>
  <div v-else><span>No Country selected</span></div>
</template>

<script lang="ts">
import CountryForm from "./components/CountryForm.vue";
import { Component, Vue } from "vue-property-decorator";
import { Country } from "@/models/Country";
import { CountryModule } from "./store";

@Component({ components: { CountryForm } })
export default class CountriesPageDetails extends Vue {
  get country(): Country | undefined {
    return CountryModule.Country;
  }
  async mounted() {
    try {
      await CountryModule.ReadCountry(this.$route.params.id);
    } finally {
      console.log("Country Page loaded");
    }
  }
}
</script>
