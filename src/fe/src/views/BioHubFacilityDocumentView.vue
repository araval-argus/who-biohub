<template>
  <v-container fluid class="documents">
    <document-templates-page-index />
  </v-container>
</template>

<script lang="ts">
import DocumentTemplatesPageIndex from "@/features/documentTemplates/DocumentTemplatesPageIndex.vue";
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../store/MainStore";

@Component({
  components: { DocumentTemplatesPageIndex: DocumentTemplatesPageIndex },
})
export default class BioHubFacilityDocumentView extends Vue {
  mounted() {
    AppModule.HideLoading();
  }
}
</script>
