<template>
  <v-btn
    class="mb-2"
    :style="style"
    color="ms-3 primary"
    @click="downloadDocument"
    ><span class="wrap-text" :style="textStyle">{{ title }}</span></v-btn
  >
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

import { SMTA2WorkflowItemModule } from "../store";
import { SMTA2WorkflowItemFileInfo } from "@/models/SMTA2WorkflowItemFileInfo";

@Component({
  components: {},
})
export default class DownloadDocumentComponent extends Vue {
  // Props
  @Prop({ required: true, type: String, default: "" })
  readonly documentId: string;

  @Prop({ required: true, type: String, default: "" })
  readonly documentName: string;

  @Prop({ required: true, type: String, default: "" })
  readonly worklistId: string;

  @Prop({ required: true, type: String, default: "Download" })
  readonly title: string;

  @Prop({ required: true, type: String, default: "" })
  readonly style: string;

  @Prop({ required: true, type: String, default: "" })
  readonly textStyle: string;

  downloadDocument() {
    const fileInfo = Object.create({
      Id: this.documentId,
      Name: this.documentName,
      WorklistId: this.worklistId,
    }) as SMTA2WorkflowItemFileInfo;

    SMTA2WorkflowItemModule.SET_SMTA2WORKFLOWITEMDOWNLOADFILEINFO(fileInfo);
    this.$emit("downloadFile");
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.box {
  inline-size: 300px;
  word-break: break-all;
}
</style>
