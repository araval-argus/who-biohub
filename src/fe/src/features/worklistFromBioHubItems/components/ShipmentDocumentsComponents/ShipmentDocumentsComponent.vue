<template>
  <div>
    <v-card-text>
      <v-card-actions
        v-if="
          CanSubmitBHFSMTA2ShipmentDocuments ||
          CanSubmitQESMTA2ShipmentDocuments
        "
      >
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
        v-if="CanReadBHFSMTA2ShipmentDocuments"
        v-model="model"
        :can-create="CanSubmitBHFSMTA2ShipmentDocuments"
        :can-edit="CanSubmitBHFSMTA2ShipmentDocuments"
        :can-read="CanReadBHFSMTA2ShipmentDocuments"
        :can-download="CanDownloadBHFSMTA2ShipmentDocuments"
        :shipment-documents="model.BHFShipmentDocuments"
        @downloadFile="downloadFile"
        :bhf-documents="true"
      >
      </ShipmentDocumentsForm>
      <ShipmentDocumentsForm
        v-if="CanReadQESMTA2ShipmentDocuments"
        v-model="model"
        :can-create="CanSubmitQESMTA2ShipmentDocuments"
        :can-edit="CanSubmitQESMTA2ShipmentDocuments"
        :can-read="CanReadQESMTA2ShipmentDocuments"
        :can-download="CanDownloadQESMTA2ShipmentDocuments"
        :shipment-documents="model.QEShipmentDocuments"
        @downloadFile="downloadFile"
      >
      </ShipmentDocumentsForm>
    </v-card-text>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { WorklistFromBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import FormPopup from "../../../../components/FormPopup.vue";
import DownloadDocumentComponent from "../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import ShipmentDocumentsForm from "./../ShipmentDocumentsComponents/ShipmentDocumentsForm.vue";
import Checkbox from "@/components/Checkbox.vue";
import ShipmentMaterialsSection from "./../WaitForArrivalConditionCheckComponents/ShipmentMaterialsSection.vue";
import FeedbackFlowComponent from "./../WaitForArrivalConditionCheckComponents/FeedbackFlowComponent.vue";
import { AttachmentType } from "@/models/enums/AttachmentType";
import { WorklistFromBioHubItemModule } from "../../store";
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
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get CanSubmitBHFSMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments);
    }
  }

  get CanReadBHFSMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanReadBHFSMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanReadBHFSMTA2ShipmentDocuments);
    }
  }

  get CanDownloadBHFSMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanDownloadBHFSMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(
        PermissionNames.CanDownloadBHFSMTA2ShipmentDocuments
      );
    }
  }

  get CanSubmitQESMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanSubmitQESMTA2ShipmentDocuments);
    }
  }

  get CanReadQESMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(PermissionNames.CanReadQESMTA2ShipmentDocumentsPast);
    } else {
      return hasPermission(PermissionNames.CanReadQESMTA2ShipmentDocuments);
    }
  }

  get CanDownloadQESMTA2ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(
        PermissionNames.CanDownloadQESMTA2ShipmentDocumentsPast
      );
    } else {
      return hasPermission(PermissionNames.CanDownloadQESMTA2ShipmentDocuments);
    }
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

  get SubmitBHFSMTA2ShipmentDocuments(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments
    );
  }

  get SubmitQESMTA2ShipmentDocuments(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments
    );
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
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
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
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
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  update() {
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  async downloadTemplate(id: string, name: string) {
    const info = new Map<string, string>();
    info.set("Id", id);
    info.set("Name", name);

    await DocumentTemplateModule.ReadTemplateForShipmentRequest(info);
  }

  mounted() {
    WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
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
