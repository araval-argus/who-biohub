<template>
  <div>
    <v-card v-if="SubmitBiosafetyChecklistFormOfSMTA2" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <v-card-actions>
          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <v-radio-group v-model="model.BiosafetyChecklistFillingOption">
              <v-radio
                :key="0"
                label="Electronically fill and sign the form"
                :value="0"
              ></v-radio>
              <v-radio
                :key="1"
                label="Upload the copy of signed document"
                :value="1"
              ></v-radio>
            </v-radio-group>
          </v-container>
        </v-card-actions>
        <div v-if="BiosafetyChecklistElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the Biosafety Checklist of SMTA 2 Template</p>
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
            <p class="text-h6">Upload</p>
            <p>Upload the file and submit</p>
          </div>

          <v-btn color="ms-3 primary" @click="addDocumentFile"> Upload </v-btn>
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
              {{ documentFileName }}
            </p>

            <FormPopup
              ref="newDocumentFilePopup"
              v-model="newFilePopupItems"
              title="Add Document"
              :properties-errors="error"
              @executeSave="setNewBiosafetyChecklistOfSMTA2Document"
            >
            </FormPopup>
            <v-spacer></v-spacer>
          </div>
          <v-card-actions
            v-if="
              documentUploaded &&
              (model.IsPast != true || model.AssignedOperationDate != null)
            "
          >
            <v-spacer></v-spacer>
            <v-container class="px-0" fluid>
              <CardActionsGenericButton text="Submit" @click="submit">
              </CardActionsGenericButton>
            </v-container>
            <v-spacer></v-spacer>
          </v-card-actions>
        </div>
        <div v-else>
          <h3>Qualified Entity Requirements Checklist</h3>
          <h4 class="mb-10">
            This checklist<sup>1</sup> is for qualified entities (QEs) wishing
            to receive biological materials with epidemic or pandemic potential
            (BMEPP).
          </h4>
          <BiosafetyChecklistForm
            v-model="model"
            :can-edit="hasSubmitPermissionByStatus"
            :can-read="hasReadPermissionByStatus"
            mandatory-message-text=""
            :biosafety-checklist="BiosafetyChecklist"
            @updateBiosafetyChecklist="updateBiosafetyChecklist"
          >
          </BiosafetyChecklistForm>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-container class="px-0" fluid>
              <div
                v-if="
                  model.BiosafetyChecklistThreadComments &&
                  model.BiosafetyChecklistThreadComments.length > 0
                "
              >
                <h2 class="mb-10">Thread</h2>
                <div
                  v-for="i in model.BiosafetyChecklistThreadComments.length"
                  :key="i"
                >
                  <BiosafetyChecklistCommentFlowComponent
                    v-model="model.BiosafetyChecklistThreadComments[i - 1]"
                    :show-arrow="true"
                  >
                  </BiosafetyChecklistCommentFlowComponent>
                </div>
              </div>

              <h3>
                <span style="color: rgb(0, 154, 222)"> Comment</span>
                <span style="color: red">
                  (if any item above was unchecked, please provide additional
                  information to justify)
                </span>
              </h3>
              <text-area
                v-model="model.NewBiosafetyChecklistThreadComment"
                label=""
                :readonly="false"
                :properties-errors="propertiesErrors"
                property-name="NewBiosafetyChecklistThreadComment"
                @input="update"
              ></text-area>
            </v-container>
            <v-spacer></v-spacer>
          </v-card-actions>
          <v-card-actions>
            <v-spacer></v-spacer>

            <v-container class="px-0" fluid>
              <v-row>
                <v-col class="text-right" cols="12" sm="6" md="6">
                  <p>{{ currentUserNameAndTitle }}</p>
                </v-col>
                <v-col cols="12" sm="6" md="6">
                  <text-field
                    v-model="model.BiosafetyChecklistOfSMTA2SignatureText"
                    label="Signature"
                    :readonly="false"
                    :properties-errors="propertiesErrors"
                    property-name="BiosafetyChecklistOfSMTA2SignatureText"
                    @input="update"
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col class="text-right" cols="12" sm="6" md="6">
                  <p>{{ currentPlace }}</p>
                  <p>Date: {{ CurrentDate }}</p>
                </v-col>
                <v-col
                  v-if="
                    model.BiosafetyChecklistOfSMTA2SignatureText == null ||
                    model.BiosafetyChecklistOfSMTA2SignatureText == undefined ||
                    model.BiosafetyChecklistOfSMTA2SignatureText == ''
                  "
                  cols="12"
                  sm="6"
                  md="6"
                >
                  <h4 style="color: red">Please add a Signature</h4>
                </v-col>
              </v-row>
              <v-row v-if="model.IsPast == true">
                <v-col cols="12" sm="6" md="6"> </v-col>
                <v-col cols="12" sm="6" md="6">
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
                </v-col>
              </v-row>
              <v-spacer></v-spacer>
            </v-container>
            <!-- # 54317 -->
            <!-- <FormPopup
            ref="newSignatureFilePopup"
            v-model="newFilePopupItems"
            title="Add Signature"
            :properties-errors="error"
            accept=".jpeg, .jpg"
            @executeSave="setNewBiosafetyChecklistOfSMTA2Signature"
          >
          </FormPopup> -->
            <!-- ------------------- -->
          </v-card-actions>
          <p>
            <sup>1</sup> This checklist is also found in Annex 4 of the WHO
            BioHub System Biosafety and Biosecurity: Criteria and Operational
            Modalities. October 2021 (<a
              style="color: rgb(0, 154, 222)"
              href="https://www.who.int/publications/i/item/9789240044524"
              target="_blank"
              >https://www.who.int/publications/i/item/9789240044524</a
            >).
          </p>
          <p>
            <sup>2</sup> Laboratory biosafety manual, fourth edition. Geneva:
            World Health Organization; 2020 (<a
              style="color: rgb(0, 154, 222)"
              href="https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y"
              target="_blank"
              >https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y</a
            >).
          </p>
          <CardActionsGenericButton
            color="primary"
            style="display: inline-block; float: right"
            text="Save As Draft"
            @click="saveAsDraft"
          >
          </CardActionsGenericButton>
          <CardActionsGenericButton
            v-if="submitBiosafetyChecklistOfSMTA2FormVisible"
            style="display: inline-block; float: right"
            text="Submit"
            @click="submit"
          >
          </CardActionsGenericButton>
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

    <v-card v-if="WaitForBiosafetyChecklistFormSMTA2BSFsApproval" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div v-if="BiosafetyChecklistElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the signed Biosafety Checklist of SMTA 2</p>
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
            <p class="text-h6">Fill the form</p>
            <p>Read carefully the document</p>
            <p>Fill the electronic form accordingly</p>
          </div>
        </div>
        <h3>Qualified Entity Requirements Checklist</h3>
        <h3 class="mb-10">
          This checklist<sup>1</sup> is for qualified entities (QEs) wishing to
          receive biological materials with epidemic or pandemic potential
          (BMEPP).
        </h3>

        <BiosafetyChecklistForm
          v-model="model"
          :can-edit="hasSubmitPermissionByStatus"
          :can-read="hasReadPermissionByStatus"
          :mandatory-message-text="
            WaitForBiosafetyChecklistFormSMTA2BSFsApprovalMessageText
          "
          :biosafety-checklist="BiosafetyChecklist"
          @updateBiosafetyChecklist="updateBiosafetyChecklist"
        >
        </BiosafetyChecklistForm>
        <v-spacer></v-spacer>
        <v-card-actions v-if="BiosafetyChecklistElectronicallyFill == false">
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <text-area
              v-model="model.NewBiosafetyChecklistThreadCommentFromDocument"
              label="Comment From Document"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="NewBiosafetyChecklistThreadCommentFromDocument"
              @input="update"
            ></text-area>
          </v-container>
        </v-card-actions>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <div
              v-if="
                model.BiosafetyChecklistThreadComments &&
                model.BiosafetyChecklistThreadComments.length > 0
              "
            >
              <h2 class="mb-10">Thread</h2>
              <div
                v-for="i in model.BiosafetyChecklistThreadComments.length"
                :key="i"
              >
                <BiosafetyChecklistCommentFlowComponent
                  v-model="model.BiosafetyChecklistThreadComments[i - 1]"
                  :show-arrow="
                    i < model.BiosafetyChecklistThreadComments.length
                  "
                >
                </BiosafetyChecklistCommentFlowComponent>
              </div>
            </div>
          </v-container>
          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-container class="px-0" fluid>
            <v-row>
              <v-col class="text-right" cols="12" sm="6" md="6">
                <p>{{ currentUserNameAndTitle }}</p>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <text-field
                  v-model="model.BiosafetyChecklistOfSMTA2SignatureText"
                  label="Signature"
                  :readonly="true"
                  :properties-errors="propertiesErrors"
                  property-name="BiosafetyChecklistOfSMTA2SignatureText"
                  @input="update"
                >
                </text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="text-right" cols="12" sm="6" md="6">
                <p>{{ currentPlace }}</p>
                <p>Date: {{ CurrentDate }}</p>
              </v-col>
              <v-col
                v-if="
                  BiosafetyChecklistElectronicallyFill == true &&
                  (model.BiosafetyChecklistOfSMTA2SignatureText == null ||
                    model.BiosafetyChecklistOfSMTA2SignatureText == undefined ||
                    model.BiosafetyChecklistOfSMTA2SignatureText == '')
                "
                cols="12"
                sm="6"
                md="6"
              >
                <h4 style="color: red">MISSING SIGNATURE !!!</h4>
              </v-col>
            </v-row>
            <v-row v-if="model.IsPast == true">
              <v-col cols="12" sm="6" md="6"> </v-col>
              <v-col cols="12" sm="6" md="6">
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
              </v-col>
            </v-row>
            <v-spacer></v-spacer>
          </v-container>
        </v-card-actions>
        <p>
          <sup>1</sup> This checklist is also found in Annex 4 of the WHO BioHub
          System Biosafety and Biosecurity: Criteria and Operational Modalities.
          October 2021 (<a
            style="color: rgb(0, 154, 222)"
            href="https://www.who.int/publications/i/item/9789240044524"
            target="_blank"
            >https://www.who.int/publications/i/item/9789240044524</a
          >).
        </p>
        <p>
          <sup>2</sup> Laboratory biosafety manual, fourth edition. Geneva:
          World Health Organization; 2020 (<a
            style="color: rgb(0, 154, 222)"
            href="https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y"
            target="_blank"
            >https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y</a
          >).
        </p>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <v-radio-group v-model="model.LastSubmissionApproved">
              <v-radio :key="0" label="Approve" :value="true"></v-radio>
              <v-radio :key="1" label="Ask for review" :value="false"></v-radio>
            </v-radio-group>
          </v-container>
        </v-card-actions>
        <v-card-actions v-if="approvedSelected == true">
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <h4
              v-if="model.BiosafetyChecklistOfSMTA2ApprovalFlag != true"
              style="color: red"
            >
              Please confirm
            </h4>
            <Checkbox
              v-model="model.BiosafetyChecklistOfSMTA2ApprovalFlag"
              :readonly="!hasSubmitPermissionByStatus"
              label="I confirm that the Biosafety Checklist is fully
well reported"
            ></Checkbox>
            <text-area
              v-model="model.BiosafetyChecklistOfSMTA2ApprovalComment"
              label="Add a comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="BiosafetyChecklistOfSMTA2ApprovalComment"
              @input="update"
            ></text-area>
          </v-container>

          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions v-if="approvedSelected == false">
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <text-area
              v-model="model.NewBiosafetyChecklistThreadComment"
              label="Comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="NewBiosafetyChecklistThreadComment"
              @input="update"
            ></text-area>
          </v-container>
        </v-card-actions>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container
            v-if="submitWaitingForBiosafetyChecklistOfSMTA2SECsApprovalVisible"
            class="px-0"
            fluid
          >
            <CardActionsGenericButton text="Submit" @click="submit">
            </CardActionsGenericButton>
          </v-container>
        </v-card-actions>
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
import { Component, Vue, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";
import TextField from "@/components/TextField.vue";
import { WorklistFromBioHubItemModule } from "../../store";
import { AuthModule } from "../../../auth/store";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import FormPopup from "../../../../components/FormPopup.vue";
import { FormPopupItem } from "@/models/FormPopupItem";
import { createFormPopupItem } from "../../../../utils/helper";
import { InputType } from "@/models/enums/InputType";
import DownloadDocumentComponent from "./../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import Annex2OfSMTA2Form from "./../Annex2ofSMTA2Components/Annex2OfSMTA2Form.vue";
import { AttachmentType } from "@/models/enums/AttachmentType";
import Checkbox from "@/components/Checkbox.vue";
import DatePicker from "@/components/DatePicker.vue";
import BiosafetyChecklistForm from "./../BiosafetyChecklistComponents/BiosafetyChecklistForm.vue";
import BiosafetyChecklistCommentFlowComponent from "./../BiosafetyChecklistComponents/BiosafetyChecklistCommentFlowComponent.vue";
import { PermissionType } from "@/models/enums/PermissionType";
import { WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 } from "@/models/WorklistFromBioHubItemBiosafetyChecklistOfSMTA2";

@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    Annex2OfSMTA2Form,
    Checkbox,
    DatePicker,
    TextField,
    BiosafetyChecklistForm,
    BiosafetyChecklistCommentFlowComponent,
  },
})
export default class BiosafetyChecklistPhase extends Vue {
  private newFilePopupItems: Array<FormPopupItem> = [];

  private documentFileName = "";
  private signatureFileName = "";
  private base64SignaturePreview = "";
  private currentDate = this.getFormatDate(new Date());

  $refs!: {
    newDocumentFilePopup: FormPopup;
    newSignatureFilePopup: FormPopup;
  };

  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get BiosafetyChecklist(): Array<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> {
    return this.model.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s;
  }

  get ImageSrcBiosafetyChecklistOfSMTA2(): string {
    const src =
      "data:image/jpeg;base64," +
      this.model.BiosafetyChecklistOfSMTA2SignatureString;
    return src;
  }

  get BiosafetyChecklistSignatureWarningVisible(): boolean {
    return (
      (this.model.BiosafetyChecklistOfSMTA2SignatureId === undefined ||
        this.model.BiosafetyChecklistOfSMTA2SignatureId === null ||
        this.model.BiosafetyChecklistOfSMTA2SignatureId === "") &&
      (this.base64SignaturePreview === "" ||
        this.base64SignaturePreview === null ||
        this.base64SignaturePreview === undefined)
    );
  }

  get BiosafetyChecklistSignatureButtonTitle(): string {
    if (
      this.model.BiosafetyChecklistOfSMTA2SignatureId !== undefined &&
      this.model.BiosafetyChecklistOfSMTA2SignatureId !== null &&
      this.model.BiosafetyChecklistOfSMTA2SignatureId !== ""
    ) {
      return "Change the image of the signature";
    }
    return "Upload the image of the signature";
  }

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get documentId(): string {
    return this.model.BiosafetyChecklistOfSMTA2DocumentId;
  }

  get worklistId(): string {
    return this.model.Id;
  }

  get documentName(): string {
    return this.model.BiosafetyChecklistOfSMTA2DocumentName;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get BiosafetyChecklistElectronicallyFill(): boolean {
    return (
      this.model.BiosafetyChecklistFillingOption ==
      WorklistFillingOption.ElectronicallyFill
    );
  }

  get WaitForBiosafetyChecklistFormSMTA2BSFsApprovalMessageText(): string {
    //if (this.BiosafetyChecklistElectronicallyFill == true) {
    return "The requester provided a response that may need to justify";
    // } else {
    //   return "Please accept";
    // }
  }

  get SubmitBiosafetyChecklistFormOfSMTA2(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2
    );
  }

  get WaitForBiosafetyChecklistFormSMTA2BSFsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval
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

  get documentUploaded(): boolean {
    return (
      WorklistFromBioHubItemModule.WorklistFromBioHubItemDocument !== undefined
    );
  }

  get signatureUploaded(): boolean {
    return (
      WorklistFromBioHubItemModule.WorklistFromBioHubItemSignature !== undefined
    );
  }

  get approvedSelected(): boolean {
    return this.model.LastSubmissionApproved;
  }

  get submitBiosafetyChecklistOfSMTA2FormVisible(): boolean {
    //# 54317
    // return (
    //   (this.signatureUploaded == true ||
    //     (this.model.BiosafetyChecklistOfSMTA2SignatureId !== undefined &&
    //       this.model.BiosafetyChecklistOfSMTA2SignatureId !== "" &&
    //       this.model.BiosafetyChecklistOfSMTA2SignatureId !== null)) &&
    //   this.BiosafetyChecklistOfSMTA2FormCompleted == true
    // );
    return (
      this.model.BiosafetyChecklistOfSMTA2SignatureText !== undefined &&
      this.model.BiosafetyChecklistOfSMTA2SignatureText !== "" &&
      this.model.BiosafetyChecklistOfSMTA2SignatureText !== null &&
      (this.model.IsPast != true || this.model.AssignedOperationDate != null)
      //this.BiosafetyChecklistOfSMTA2FormCompleted == true
    );
    ///////////////////////////////
  }

  get submitWaitingForBiosafetyChecklistOfSMTA2SECsApprovalVisible(): boolean {
    //# 54317
    // if (
    //   this.approvedSelected == true &&
    //   this.BiosafetyChecklistElectronicallyFill == true &&
    //   (this.model.BiosafetyChecklistOfSMTA2SignatureId === undefined ||
    //     this.model.BiosafetyChecklistOfSMTA2SignatureId === "" ||
    //     this.model.BiosafetyChecklistOfSMTA2SignatureId === null)
    // ) {
    //   return false;
    // }

    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (
      this.approvedSelected == true &&
      this.BiosafetyChecklistElectronicallyFill == true &&
      (this.model.BiosafetyChecklistOfSMTA2SignatureText === undefined ||
        this.model.BiosafetyChecklistOfSMTA2SignatureText === "" ||
        this.model.BiosafetyChecklistOfSMTA2SignatureText === null)
    ) {
      return false;
    }
    ///////////////////////////////
    if (
      this.approvedSelected == false &&
      ((this.BiosafetyChecklistElectronicallyFill == false &&
        this.model.NewBiosafetyChecklistThreadComment != "") ||
        this.BiosafetyChecklistElectronicallyFill == true)
    ) {
      return true;
    }

    if (
      this.approvedSelected == true &&
      //this.BiosafetyChecklistOfSMTA2FormCompleted == true &&
      this.model.BiosafetyChecklistOfSMTA2ApprovalFlag == true
    ) {
      return true;
    }
    return false;
  }

  get BiosafetyChecklistOfSMTA2FormCompleted(): boolean {
    if (
      this.SubmitBiosafetyChecklistFormOfSMTA2 ||
      this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval
    ) {
      let BiosafetyChecklistFormCompleted = true;
      this.model.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.forEach(
        (elem) => {
          if (
            elem.IsVisible === true &&
            elem.Mandatory === true &&
            elem.Flag !== true
          ) {
            BiosafetyChecklistFormCompleted = false;
          }
        }
      );

      return BiosafetyChecklistFormCompleted;
    } else {
      return true;
    }
  }

  get currentUserNameAndTitle(): string {
    if (this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval) {
      return (
        "Name, Title:" +
        this.model.LastOperationUserFirstName +
        " " +
        this.model.LastOperationUserLastName +
        ", " +
        (this.model.LastOperationUserJobTitle != null
          ? this.model.LastOperationUserJobTitle
          : "")
      );
    } else {
      return (
        "Name, Title: " + AuthModule.LoggedUserName ??
        "" + ", " + AuthModule.LoggedUserJobTitle ??
        ""
      );
    }
  }

  get currentPlace(): string {
    return (
      "Place: " +
      this.model.LaboratoryName +
      " (" +
      this.model.LaboratoryCountry +
      ")"
    );
  }

  get CurrentDate(): string {
    if (this.WaitForBiosafetyChecklistFormSMTA2BSFsApproval) {
      return this.getFormatDate(this.model.OperationDate);
    } else {
      return this.currentDate;
    }
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  update() {
    this.$emit("update", this.model);
  }

  async addDocumentFile(): Promise<void> {
    this.newFilePopupItems = new Array<FormPopupItem>(
      createFormPopupItem(InputType.File, "File", "File", true)
    );
    this.$refs.newDocumentFilePopup.showPopup();
  }

  async addSignatureFile(): Promise<void> {
    this.newFilePopupItems = new Array<FormPopupItem>(
      createFormPopupItem(InputType.File, "File", "File", true)
    );
    this.$refs.newSignatureFilePopup.showPopup();
  }

  cancelSignatureFile(): void {
    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_SIGNATURE(
      undefined
    );
    this.signatureFileName = "";
    this.base64SignaturePreview = "";
  }

  async setNewBiosafetyChecklistOfSMTA2Document(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_DOCUMENT(
      fileSelected
    );
    this.setDocumentFileType(DocumentFileType.BiosafetyChecklist);

    this.documentFileName = fileSelected.name;
  }

  async setNewBiosafetyChecklistOfSMTA2Signature(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_SIGNATURE(
      fileSelected
    );
    this.setDocumentFileType(DocumentFileType.BiosafetyChecklist);

    this.signatureFileName = fileSelected.name;
    this.createBase64ImageForSignaturePreview();
  }

  updateLastSubmissionApproved(approved: boolean) {
    this.model.LastSubmissionApproved = approved;
    this.$emit("update", this.model);
  }

  setDocumentFileType(documentFileType: DocumentFileType) {
    this.model.DocumentTemplateFileType = documentFileType;
    this.$emit("update", this.model);
  }

  setOriginalDocumentTemplateId(originalDocumentTemplateId: string) {
    this.model.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId =
      originalDocumentTemplateId;
    this.$emit("update", this.model);
  }

  setSaveAsDraft(saveAsDraft: boolean) {
    this.model.IsSaveDraft = saveAsDraft;
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  createBase64ImageForSignaturePreview(): void {
    const file = WorklistFromBioHubItemModule.SignatureFile;

    if (file === undefined || file === null) {
      return;
    }
    this.base64SignaturePreview = URL.createObjectURL(file);
  }

  updateBiosafetyChecklist(
    model: WorklistFromBioHubItemBiosafetyChecklistOfSMTA2
  ) {
    WorklistFromBioHubItemModule.UPDATE_BIOSAFETYCHECKLIST(model);
  }

  submit() {
    if (
      this.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2
    ) {
      this.updateLastSubmissionApproved(true);
    }
    this.setSaveAsDraft(false);
    this.$emit("submit");
  }

  saveAsDraft() {
    this.setSaveAsDraft(true);
    this.$emit("saveAsDraft");
  }

  async mounted() {
    if (
      this.model.BiosafetyChecklistFillingOption ==
      WorklistFillingOption.ElectronicallyFill
    ) {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(
        AttachmentType.Signature
      );
    } else {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
    }
  }

  @Watch("model.BiosafetyChecklistFillingOption")
  biosafetyChecklistFillingOption() {
    if (
      this.model.BiosafetyChecklistFillingOption ==
      WorklistFillingOption.ElectronicallyFill
    ) {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(
        AttachmentType.Signature
      );
    } else {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
    }
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

.shipment-action-vcard {
  margin-left: 12px;
  margin-top: 20px;
}
</style>
