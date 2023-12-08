<template>
  <CountryForm v-if="isCountrySet" v-model="country" title="Country Edit">
    <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
    </CardActionsSaveCancel>
  </CountryForm>
  <div v-else><span>No Country selected</span></div>
</template>

<script lang="ts">
import CountryForm from "./components/CountryForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { CountryModule } from "./store";
import { Country } from "@/models/Country";

@Component({ components: { CountryForm, CardActionsSaveCancel } })
export default class CountriesPageEdit extends Vue {
  get isCountrySet(): boolean {
    return CountryModule.Country !== undefined;
  }

  get country(): Country {
    const lab = CountryModule.Country;
    if (lab) return lab;

    throw { message: "no country selected" };
  }

  set country(lab: Country) {
    CountryModule.SET_COUNTRY(lab);
  }

  async onSave(): Promise<void> {
    await CountryModule.UpdateCountry();
    this.$router.back();
  }

  onCancel(): void {
    this.$router.back();
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
