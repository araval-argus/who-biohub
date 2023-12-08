<template>
  <v-card>
    <v-card-text>
      <v-card-actions v-if="CanSubmitSMTA1ShipmentDocuments">
        <v-container class="px-0" fluid>
          <v-row>
            <v-col cols="12" md="12" lg="12">
              <p class="text-h6">Available templates</p>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <DownloadDocumentComponent
                style="height: 100%; width: 100%"
                text-style="width: 250px"
                title="Download Packing List"
                :document-id="packagingListDocumentTemplateId"
                :document-name="packagingListDocumentTemplateName"
                :worklist-id="model.Id"
                @downloadFile="
                  downloadTemplate(
                    packagingListDocumentTemplateId,
                    packagingListDocumentTemplateName
                  )
                "
              >
              </DownloadDocumentComponent>
            </v-col>
            <v-col>
              <DownloadDocumentComponent
                style="height: 100%; width: 100%"
                text-style="width: 250px"
                title="Download Non-Commercial Invoice (Category A - UN2814)"
                :document-id="nonCommercialInvoiceCatADocumentTemplateId"
                :document-name="nonCommercialInvoiceCatADocumentTemplateName"
                :worklist-id="model.Id"
                @downloadFile="
                  downloadTemplate(
                    nonCommercialInvoiceCatADocumentTemplateId,
                    nonCommercialInvoiceCatADocumentTemplateName
                  )
                "
              >
              </DownloadDocumentComponent>
            </v-col>
            <v-col>
              <DownloadDocumentComponent
                style="height: 100%; width: 100%"
                text-style="width: 250px"
                title="Download Non-Commercial Invoice (Category B - UN3373)"
                :document-id="nonCommercialInvoiceCatBDocumentTemplateId"
                :document-name="nonCommercialInvoiceCatBDocumentTemplateName"
                :worklist-id="model.Id"
                @downloadFile="
                  downloadTemplate(
                    nonCommercialInvoiceCatBDocumentTemplateId,
                    nonCommercialInvoiceCatBDocumentTemplateName
                  )
                "
              >
              </DownloadDocumentComponent>
            </v-col>
          </v-row>
        </v-container>
      </v-card-actions>

      <ShipmentDocumentsForm
        v-if="CanReadSMTA1ShipmentDocuments"
        v-model="model"
        :can-create="CanSubmitSMTA1ShipmentDocuments"
        :can-edit="CanSubmitSMTA1ShipmentDocuments"
        :can-read="CanReadSMTA1ShipmentDocuments"
        :can-download="CanDownloadSMTA1ShipmentDocuments"
        @downloadFile="downloadFile"
      >
      </ShipmentDocumentsForm>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";

import { WorklistToBioHubItemModule } from "../../store";
import { AuthModule } from "../../../auth/store";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import {
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { WorklistToBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubDownloadPermissionsByStatus";
import { WorklistToBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubReadPermissionsByStatus";
import { WorklistToBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistToBioHubSubmitPermissionsByStatus";
import FormPopup from "../../../../components/FormPopup.vue";
import { FormPopupItem } from "@/models/FormPopupItem";
import { createFormPopupItem } from "../../../../utils/helper";
import { InputType } from "@/models/enums/InputType";
import DownloadDocumentComponent from "../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import ShipmentDocumentsForm from "./ShipmentDocumentsForm.vue";
import { AttachmentType } from "@/models/enums/AttachmentType";
import Checkbox from "@/components/Checkbox.vue";
import ShipmentMaterialsSection from "../WaitForArrivalConditionCheckComponents/ShipmentMaterialsSection.vue";
import FeedbackFlowComponent from "../WaitForArrivalConditionCheckComponents/FeedbackFlowComponent.vue";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { PermissionType } from "@/models/enums/PermissionType";
import { DocumentTemplateModule } from "../../../documentTemplates/store";
@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    ShipmentDocumentsForm,
    Checkbox,
    ShipmentMaterialsSection,
    FeedbackFlowComponent,
  },
})
export default class ShipmentDocumentsComponent extends Vue {
  private newFilePopupItems: Array<FormPopupItem> = [];

  $refs!: {
    newDocumentFilePopup: FormPopup;
    newSignatureFilePopup: FormPopup;
  };

  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  @Prop({ type: String, default: "" })
  readonly title: string;

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get nonCommercialInvoiceCatADocumentTemplateId(): string {
    return this.model.NonCommercialInvoiceCatADocumentTemplateId;
  }
  get nonCommercialInvoiceCatBDocumentTemplateId(): string {
    return this.model.NonCommercialInvoiceCatBDocumentTemplateId;
  }
  get packagingListDocumentTemplateId(): string {
    return this.model.PackagingListDocumentTemplateId;
  }

  get nonCommercialInvoiceCatADocumentTemplateName(): string {
    return this.model.NonCommercialInvoiceCatADocumentTemplateName;
  }
  get nonCommercialInvoiceCatBDocumentTemplateName(): string {
    return this.model.NonCommercialInvoiceCatBDocumentTemplateName;
  }
  get packagingListDocumentTemplateName(): string {
    return this.model.PackagingListDocumentTemplateName;
  }

  get SubmitSMTA1ShipmentDocuments(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments
    );
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.DownloadFile,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Read,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get CanSubmitSMTA1ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast);
    } else {
      return hasPermission(PermissionNames.CanSubmitSMTA1ShipmentDocuments);
    }
  }

  get CanReadSMTA1ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(PermissionNames.CanReadSMTA1ShipmentDocumentsPast);
    } else {
      return hasPermission(PermissionNames.CanReadSMTA1ShipmentDocuments);
    }
  }

  get CanDownloadSMTA1ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanDownloadSMTA1ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanDownloadSMTA1ShipmentDocuments);
    }
  }

  update() {
    this.$emit("update", this.model);
  }

  async downloadTemplate(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);

    await DocumentTemplateModule.ReadTemplateForShipmentRequest(info);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  mounted() {
    WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.signature-hover {
  cursor: pointer;
}
</style>
