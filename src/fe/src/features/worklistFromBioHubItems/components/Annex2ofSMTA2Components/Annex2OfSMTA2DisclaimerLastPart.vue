<template>
  <div>
    <v-row class="text-center">
      <v-col cols="12" sm="12" md="12">
        <p>This Request Form has been completed on behalf of the</p>
        <h4>Qualified Entity:</h4>
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
export default class Annex2OfSMTA2DisclaimerLastPart extends Vue {
  @Prop({ type: String, default: "" })
  readonly provider: string;
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
