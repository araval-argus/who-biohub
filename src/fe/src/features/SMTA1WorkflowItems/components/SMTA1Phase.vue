<template>
  <div>
    <v-card v-if="SubmitSMTA1" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div v-if="model.CanSkipSMTA1Phase == true">
          <div class="shipment-action-message">
            <p>An SMTA 1 signed document is already present</p>
          </div>
          <v-container class="px-0" fluid>
            <CardActionsGenericButton text="Move to next step" @click="submit">
            </CardActionsGenericButton>
          </v-container>
        </div>
        <div v-else>
          <div v-if="model.IsPast">
            <v-row>
              <v-col cols="12" md="6" lg="6">
                <dropdown
                  v-model="model.OriginalDocumentTemplateSMTA1DocumentId"
                  :items="SmtaDocumentTemplates"
                  item-text="Text"
                  item-value="Value"
                  :menu-props="{ auto: true }"
                  label="Link To Document Template"
                  :readonly="readonly"
                  :properties-errors="propertiesErrors"
                  property-name="OriginalDocumentTemplateSMTA1DocumentId"
                  @change="update"
                ></dropdown>
              </v-col>
            </v-row>
          </div>
          <div
            class="shipment-action-vcard"
            v-if="
              model.IsPast != true ||
              (model.OriginalDocumentTemplateSMTA1DocumentId != '' &&
                model.OriginalDocumentTemplateSMTA1DocumentId != null &&
                model.OriginalDocumentTemplateSMTA1DocumentId != undefined)
            "
          >
            <div>
              <p class="text-h6">Download</p>
              <p>Please download the SMTA 1 Template</p>
            </div>
          </div>

          <DownloadDocumentComponent
            v-if="
              model.IsPast != true ||
              (model.OriginalDocumentTemplateSMTA1DocumentId != '' &&
                model.OriginalDocumentTemplateSMTA1DocumentId != null &&
                model.OriginalDocumentTemplateSMTA1DocumentId != undefined)
            "
            title="Download"
            :document-id="model.OriginalDocumentTemplateSMTA1DocumentId"
            :document-name="documentName"
            :worklist-id="worklistId"
            @downloadFile="downloadFile"
          >
          </DownloadDocumentComponent>

          <div class="shipment-action-vcard">
            <p class="text-h6">Proceed to uploading</p>
            <p>Print, fill in and sign the document</p>
            <p>Scan the paper document at the proper format and resolution</p>
          </div>

          <div class="shipment-action-vcard">
            <p class="text-h6">Upload</p>
            <p>Upload the signed version of the copied document</p>
          </div>

          <v-btn color="ms-3 primary" @click="addFile"> Upload </v-btn>
          <div style="margin-top: 40px">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </div>

          <v-spacer></v-spacer>
          <div>
            <p>
              {{ fileName }}
            </p>

            <FormPopup
              ref="newFilePopup"
              v-model="newFilePopupItems"
              title="Add Document"
              :properties-errors="error"
              @executeSave="setNewFile"
            >
            </FormPopup>
            <v-spacer></v-spacer>
          </div>

          <v-card-actions v-if="fileUploaded">
            <v-container class="px-0" fluid>
              <CardActionsGenericButton
                v-if="
                  model.IsPast != true || model.AssignedOperationDate != null
                "
                text="Submit"
                @click="submit"
              >
              </CardActionsGenericButton>
            </v-container>

            <v-spacer></v-spacer>
          </v-card-actions>
        </div>
      </v-card-text>
      <v-card-text v-else>
        <v-card-actions>
          <v-spacer></v-spacer>
          <p>No action should be taken from your side at this stage</p>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-actions>
        <slot></slot>
      </v-card-actions>
    </v-card>

    <v-card v-if="WaitingForSMTA1SECsApproval" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div v-if="model.CanSkipSMTA1Phase == true">
          <div class="shipment-action-message">
            <p>An SMTA 1 signed document is already present</p>
          </div>
          <v-container class="px-0" fluid>
            <CardActionsGenericButton text="Move to next step" @click="submit">
            </CardActionsGenericButton>
          </v-container>
        </div>
        <div v-else>
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the signed SMTA 1</p>
          </div>
          <DownloadDocumentComponent
            title="Download"
            :document-id="documentId"
            :document-name="documentName"
            :worklist-id="worklistId"
            @downloadFile="downloadFile"
          >
          </DownloadDocumentComponent>
          <div class="shipment-action-vcard">
            <p class="text-h6">Proceed to Uploading</p>
            <p>Print, fill in, and sign the document</p>
            <p>Scan the paper document at the proper format and resolution</p>
          </div>

          <div class="shipment-action-vcard">
            <v-radio-group v-model="approvedSelection">
              <v-radio key="1" label="Proceed to Approval" value="1"></v-radio>
              <v-radio key="2" label="Suspend the process" value="2"></v-radio>
            </v-radio-group>
          </div>
          <div v-if="approvedSelected" class="shipment-action-vcard">
            <p class="text-h6">Upload</p>
            <p>Upload the file and submit</p>
          </div>

          <v-btn v-if="approvedSelected" color="ms-3 primary" @click="addFile">
            Upload
          </v-btn>
          <div style="margin-top: 40px">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </div>

          <v-spacer></v-spacer>
          <div>
            <p>
              {{ fileName }}
            </p>
          </div>
          <FormPopup
            ref="newFilePopup"
            v-model="newFilePopupItems"
            title="Add Document"
            :properties-errors="error"
            @executeSave="setNewFile"
          >
          </FormPopup>
          <v-spacer></v-spacer>

          <v-card-actions v-if="!approvedSelected">
            <v-spacer></v-spacer>
            <v-container class="px-0" fluid>
              <text-area
                v-model="model.Comment"
                label="Comment"
                :readonly="false"
                :properties-errors="propertiesErrors"
                property-name="Comment"
                @input="update"
              ></text-area>
            </v-container>
            <v-spacer></v-spacer>
          </v-card-actions>
          <v-card-actions v-if="submitVisible">
            <v-spacer></v-spacer>
            <v-container class="px-0" fluid>
              <CardActionsGenericButton text="Submit" @click="submit">
              </CardActionsGenericButton>
              <v-spacer></v-spacer>
            </v-container>
          </v-card-actions>
        </div>
      </v-card-text>
      <v-card-text v-else>
        <v-card-actions>
          <v-spacer></v-spacer>
          <p>No action should be taken from your side at this stage</p>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-actions>
        <slot></slot>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";

import { SMTA1WorkflowItemModule } from "../store";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import {
  GetSMTA1WorkflowStatusPermission,
  hasPermission,
} from "../../../utils/helper";
import { SMTA1WorkflowDownloadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowDownloadPermissionsByStatus";
import { SMTA1WorkflowReadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowReadPermissionsByStatus";
import { SMTA1WorkflowSubmitPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowSubmitPermissionsByStatus";
import FormPopup from "../../../components/FormPopup.vue";
import { FormPopupItem } from "@/models/FormPopupItem";
import { createFormPopupItem } from "../../../utils/helper";
import { InputType } from "@/models/enums/InputType";
import DownloadDocumentComponent from "./DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../components/CardActionsGenericButton.vue";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { AttachmentType } from "@/models/enums/AttachmentType";
import { PermissionType } from "@/models/enums/PermissionType";
import DatePicker from "@/components/DatePicker.vue";

import Dropdown from "@/components/Dropdown.vue";

import { DropdownItem } from "@/models/DropdownItem";
import { DocumentTemplateModule } from "../../documentTemplates/store";

@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    DatePicker,
    Dropdown,
  },
})
export default class SMTA1Phase extends Vue {
  private newFilePopupItems: Array<FormPopupItem> = [];

  private approvedSelection = "1";
  private fileName = "";

  $refs!: {
    newFilePopup: FormPopup;
  };

  @Model("update", { type: Object }) model!: SMTA1WorkflowItem;
  // Props

  get CurrentStatus(): SMTA1WorkflowStatus {
    return this.model.CurrentStatus;
  }

  get documentId(): string {
    if (this.SubmitSMTA1 && this.model.IsPast) {
      const documentIdInfo = this.SmtaDocumentTemplates.filter((sdt) => {
        return sdt.Value == this.model.OriginalDocumentTemplateSMTA1DocumentId;
      });

      if (documentIdInfo.length == 0) {
        return "";
      } else {
        return documentIdInfo[0].Value;
      }
    } else {
      return this.model.SMTA1DocumentId;
    }
  }

  get worklistId(): string {
    return this.model.Id;
  }

  get documentName(): string {
    if (this.SubmitSMTA1 && this.model.IsPast) {
      const smtaDocumentTemplate =
        DocumentTemplateModule.DocumentTemplates.filter((dt) => {
          return dt.Id == this.model.OriginalDocumentTemplateSMTA1DocumentId;
        });

      if (smtaDocumentTemplate.length == 0) {
        return "";
      } else {
        return (
          smtaDocumentTemplate[0].Name + "." + smtaDocumentTemplate[0].Extension
        );
      }
    } else {
      return this.model.SMTA1DocumentName;
    }
  }

  get worklistItemTitle(): string {
    return this.model.WorkflowItemTitle;
  }

  get RequestInitiation(): boolean {
    return this.model.CurrentStatus == SMTA1WorkflowStatus.RequestInitiation;
  }

  get SubmitSMTA1(): boolean {
    return this.model.CurrentStatus == SMTA1WorkflowStatus.SubmitSMTA1;
  }

  get WaitingForSMTA1SECsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval
    );
  }

  get SMTA1WorkflowCompleted(): boolean {
    return (
      this.model.CurrentStatus == SMTA1WorkflowStatus.SMTA1WorkflowComplete
    );
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
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
    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
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

    const requiredPermissionByStatus = GetSMTA1WorkflowStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );

    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get fileUploaded(): boolean {
    return SMTA1WorkflowItemModule.SMTA1WorkflowItemDocument !== undefined;
  }

  get approvedSelected(): boolean {
    return this.approvedSelection == "1";
  }

  get submitVisible(): boolean {
    return (
      ((this.approvedSelected == true && this.fileUploaded == true) ||
        (this.approvedSelected == false && this.model.Comment != "")) &&
      (this.model.IsPast != true || this.model.AssignedOperationDate != null)
    );
  }

  get SmtaDocumentTemplates(): Array<DropdownItem> {
    const smtaDocumentTemplates =
      DocumentTemplateModule.DocumentTemplates.filter((dt) => {
        return dt.FileType == DocumentFileType.SMTA1;
      });

    if (!smtaDocumentTemplates) return new Array<DropdownItem>();

    return smtaDocumentTemplates.map((d) => {
      const text =
        d.Current == true
          ? d.Name + "." + d.Extension + " - Current"
          : d.Name + "." + d.Extension;
      return {
        Value: d.Id,
        Text: text,
      } as DropdownItem;
    });
  }

  update() {
    this.$emit("update", this.model);
  }

  async addFile(): Promise<void> {
    this.newFilePopupItems = new Array<FormPopupItem>(
      createFormPopupItem(InputType.File, "File", "File", true)
    );
    this.$refs.newFilePopup.showPopup();
  }

  async setNewFile(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    SMTA1WorkflowItemModule.SET_SMTA1WORKFLOW_DOCUMENT(fileSelected);
    this.setDocumentFileType(DocumentFileType.SMTA1);
    this.fileName = fileSelected.name;
  }

  updateLastSubmissionApproved(approved: boolean) {
    this.model.LastSubmissionApproved = approved;
    this.$emit("update", this.model);
  }

  setOriginalDocumentTemplateId(originalDocumentTemplateId: string) {
    this.model.OriginalDocumentTemplateSMTA1DocumentId =
      originalDocumentTemplateId;
    this.$emit("update", this.model);
  }

  setDocumentFileType(documentFileType: DocumentFileType) {
    this.model.DocumentTemplateFileType = documentFileType;
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  submit() {
    if (this.SubmitSMTA1 == true) {
      this.updateLastSubmissionApproved(true);
    } else {
      if (this.approvedSelection == "1") {
        this.updateLastSubmissionApproved(true);
      } else {
        this.updateLastSubmissionApproved(false);
      }
    }
    this.$emit("submit");
  }

  mounted() {
    SMTA1WorkflowItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.shipment-action-vcard {
  margin-left: 12px;
  margin-top: 20px;
}
</style>
