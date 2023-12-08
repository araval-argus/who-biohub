<template>
  <div>
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <h2>
          III. Acknowledgement of genetic sequence database for upload of BMEPP
          Genetic Sequence Data (GSD)
        </h2>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <p>
          The WHO BioHub Facility will upload BMEPP GSD that it generates, in a
          timely manner, to one or more publicly accessible genetic sequence
          databases, including for example GISAID or INSDC. WHO will inform the
          Provider of the upload of GSD and will post the information on where
          the BMEPP has been uploaded, as part of the publicly available
          catalogue hosted on the WHO's website.
        </p>
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col cols="12" sm="12" md="12">
        <p>This Voluntary Transfer Form has been completed on behalf of the</p>
        <h4>Provider:</h4>
        <h4>{{ provider }}</h4>
      </v-col>
    </v-row>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

@Component({
  components: {},
})
export default class Annex2OfSMTA1DisclaimerLastPart extends Vue {
  @Prop({ type: String, default: "" })
  readonly provider: string;
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
