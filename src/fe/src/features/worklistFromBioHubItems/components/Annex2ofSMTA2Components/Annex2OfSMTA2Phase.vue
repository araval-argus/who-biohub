<template>
  <div>
    <v-card v-if="SubmitAnnex2OfSMTA2" outlined class="ma-2 annex2-smta2">
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <v-card-actions>
          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions v-if="model.IsPast == true">
          <h4 style="color: red">Please select a signed SMTA 2 document</h4>
          <dropdown
            v-model="model.CurrentDownloadSMTA2DocumentId"
            :items="SignedSmta2Documents"
            item-text="Text"
            item-value="Value"
            :menu-props="{ auto: true }"
            label=""
            :readonly="!hasSubmitPermissionByStatus"
            property-name="CurrentDownloadSMTA2DocumentId"
            @change="update"
          ></dropdown>
        </v-card-actions>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <v-radio-group v-model="model.Annex2FillingOption">
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

        <div v-if="Annex2ElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the Annex 2 of SMTA 2 Template</p>
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
              @executeSave="setNewAnnex2OfSMTA2Document"
            >
            </FormPopup>
            <v-spacer></v-spacer>
          </div>
          <div v-if="SMTA2DocumentSigned == false">
            <h4 style="color: red">
              Warning: the SMTA 2 has not been signed yet. Please complete the
              SMTA 2 request
            </h4>
          </div>
          <v-card-actions
            v-if="
              documentUploaded &&
              SMTA2DocumentSigned == true &&
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
          <Annex2OfSMTA2Form
            v-model="model"
            :can-edit="hasSubmitPermissionByStatus"
            :can-read="hasReadPermissionByStatus"
          >
          </Annex2OfSMTA2Form>
          <v-card-actions>
            <v-spacer></v-spacer>

            <v-container class="px-0" fluid>
              <v-row>
                <v-col class="text-right" cols="12" sm="6" md="6">
                  <p>{{ currentUserNameAndTitle }}</p>
                </v-col>
                <v-col cols="12" sm="6" md="6">
                  <text-field
                    v-model="model.Annex2OfSMTA2SignatureText"
                    label="Signature"
                    :readonly="false"
                    :properties-errors="propertiesErrors"
                    property-name="Annex2OfSMTA2SignatureText"
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
                    model.Annex2OfSMTA2SignatureText == null ||
                    model.Annex2OfSMTA2SignatureText == undefined ||
                    model.Annex2OfSMTA2SignatureText == ''
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
            @executeSave="setNewAnnex2OfSMTA2Signature"
          >
          </FormPopup> -->
            <!-- ------------------- -->
          </v-card-actions>
          <CardActionsGenericButton
            color="primary"
            style="display: inline-block; float: right"
            text="Save As Draft"
            @click="saveAsDraft"
          >
          </CardActionsGenericButton>
          <div v-if="SMTA2DocumentSigned == false">
            <h4 style="color: red">
              Warning: the SMTA 2 has not been signed yet. Please complete the
              SMTA 2 request
            </h4>
          </div>
          <CardActionsGenericButton
            v-if="submitAnnex2OfSMTA2FormVisible"
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

    <v-card
      v-if="WaitingForAnnex2OfSMTA2SECsApproval"
      outlined
      class="ma-2 annex2-smta2"
    >
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div v-if="Annex2ElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the signed Annex 2 of SMTA 2</p>
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

        <Annex2OfSMTA2Form
          v-model="model"
          :can-edit="hasSubmitPermissionByStatus"
          :can-read="hasReadPermissionByStatus"
        >
        </Annex2OfSMTA2Form>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-container class="px-0" fluid>
            <v-row>
              <v-col class="text-right" cols="12" sm="6" md="6">
                <p>{{ currentUserNameAndTitle }}</p>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <text-field
                  v-model="model.Annex2OfSMTA2SignatureText"
                  label="Signature"
                  :readonly="true"
                  :properties-errors="propertiesErrors"
                  property-name="Annex2OfSMTA2SignatureText"
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
                  Annex2ElectronicallyFill == true &&
                  (model.Annex2OfSMTA2SignatureText == null ||
                    model.Annex2OfSMTA2SignatureText == undefined ||
                    model.Annex2OfSMTA2SignatureText == '')
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
            <h4 v-if="model.Annex2ApprovalFlag != true" style="color: red">
              Please confirm
            </h4>
            <Checkbox
              v-model="model.Annex2ApprovalFlag"
              :readonly="!hasSubmitPermissionByStatus"
              label="I confirm that Annex 2 of SMTA 2 is filled in satisfactorily"
            ></Checkbox>
            <text-field
              v-model="model.WHODocumentRegistrationNumber"
              label="WHO Document Registration Number"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="WHODocumentRegistrationNumber"
              @input="update"
            >
            </text-field>
            <text-area
              v-model="model.Annex2ApprovalComment"
              label="Add a comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="Annex2ApprovalComment"
              @input="update"
            ></text-area>
          </v-container>

          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions v-if="approvedSelected == false">
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
        <v-card-actions>
          <v-spacer></v-spacer>
          <div v-if="SMTA2DocumentSigned == false">
            <h4 style="color: red">
              Warning: the SMTA 2 has not been signed yet. Please complete the
              SMTA 2 request
            </h4>
          </div>
          <v-container
            v-if="submitWaitingForAnnex2OfSMTA2SECsApprovalVisible"
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
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
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
import { WorklistFromBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import FormPopup from "../../../../components/FormPopup.vue";
import { FormPopupItem } from "@/models/FormPopupItem";
import { createFormPopupItem } from "../../../../utils/helper";
import { InputType } from "@/models/enums/InputType";
import DownloadDocumentComponent from "./../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import Annex2OfSMTA2Form from "./Annex2OfSMTA2Form.vue";
import { AttachmentType } from "@/models/enums/AttachmentType";
import Checkbox from "@/components/Checkbox.vue";
import DatePicker from "@/components/DatePicker.vue";
import { DocumentModule } from "../../../documents/store";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { TransportCategoryModule } from "../../../transportCategories/store";
import { BioHubFacilityModule } from "../../../biohubfacilities/store";
import { CountryModule } from "../../../countries/store";
import { UserModule } from "../../../users/store";
import { MaterialModule } from "../../../materials/store";
import { DocumentTemplateModule } from "../../../documentTemplates/store";
import { SMTAApprovalStatus } from "@/models/enums/SMTAApprovalStatus";
import { PermissionType } from "@/models/enums/PermissionType";
import { DropdownItem } from "@/models/DropdownItem";
import Dropdown from "@/components/Dropdown.vue";

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
    Dropdown,
  },
})
export default class Annex2ofSMTA2Phase extends Vue {
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

  get ImageSrcAnnex2OfSMTA2(): string {
    const src =
      "data:image/jpeg;base64," + this.model.Annex2OfSMTA2SignatureString;
    return src;
  }

  get Annex2SignatureWarningVisible(): boolean {
    return (
      (this.model.Annex2OfSMTA2SignatureId === undefined ||
        this.model.Annex2OfSMTA2SignatureId === null ||
        this.model.Annex2OfSMTA2SignatureId === "") &&
      (this.base64SignaturePreview === "" ||
        this.base64SignaturePreview === null ||
        this.base64SignaturePreview === undefined)
    );
  }

  get Annex2SignatureButtonTitle(): string {
    if (
      this.model.Annex2OfSMTA2SignatureId !== undefined &&
      this.model.Annex2OfSMTA2SignatureId !== null &&
      this.model.Annex2OfSMTA2SignatureId !== ""
    ) {
      return "Change the image of the signature";
    }
    return "Upload the image of the signature";
  }

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get documentId(): string {
    return this.model.Annex2OfSMTA2DocumentId;
  }

  get worklistId(): string {
    return this.model.Id;
  }

  get documentName(): string {
    return this.model.Annex2OfSMTA2DocumentName;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get Annex2ElectronicallyFill(): boolean {
    return (
      this.model.Annex2FillingOption == WorklistFillingOption.ElectronicallyFill
    );
  }

  get SubmitAnnex2OfSMTA2(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2
    );
  }

  get WaitingForAnnex2OfSMTA2SECsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval
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

  get SMTA2DocumentSigned(): boolean {
    if (this.model.IsPast != true) {
      return DocumentModule.SMTA2DocumentSigned;
    } else {
      return (
        this.model.CurrentDownloadSMTA2DocumentId != "" &&
        this.model.CurrentDownloadSMTA2DocumentId != null &&
        this.model.CurrentDownloadSMTA2DocumentId != undefined
      );
    }
  }

  get SignedSmta2Documents(): Array<DropdownItem> {
    const smtaDocuments = DocumentModule.Documents.filter((dt) => {
      return dt.FileType == DocumentFileType.SMTA2;
    });

    if (!smtaDocuments) return new Array<DropdownItem>();

    return smtaDocuments.map((d) => {
      return {
        Value: d.Id,
        Text:
          d.Name + "." + d.Extension + " - " + this.getFormatDate(d.UploadTime),
      } as DropdownItem;
    });
  }

  get submitAnnex2OfSMTA2FormVisible(): boolean {
    //# 54317
    // return (
    // this.SMTA2DocumentSigned &&
    //   (this.signatureUploaded == true ||
    //     (this.model.Annex2OfSMTA2SignatureId !== undefined &&
    //       this.model.Annex2OfSMTA2SignatureId !== "" &&
    //       this.model.Annex2OfSMTA2SignatureId !== null)) &&
    //   this.Annex2OfSMTA2FormCompleted == true
    // );
    return (
      this.SMTA2DocumentSigned &&
      this.model.Annex2OfSMTA2SignatureText !== undefined &&
      this.model.Annex2OfSMTA2SignatureText !== "" &&
      this.model.Annex2OfSMTA2SignatureText !== null &&
      this.Annex2OfSMTA2FormCompleted == true &&
      (this.model.IsPast != true || this.model.AssignedOperationDate != null) &&
      this.model.BioHubFacilityId !== undefined &&
      this.model.BioHubFacilityId !== "" &&
      this.model.BioHubFacilityId !== null
    );
    //////////////////////////
  }

  get submitWaitingForAnnex2OfSMTA2SECsApprovalVisible(): boolean {
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (
      this.model.BioHubFacilityId === undefined ||
      this.model.BioHubFacilityId === "" ||
      this.model.BioHubFacilityId === null
    ) {
      return false;
    }

    if (this.approvedSelected == true && this.SMTA2DocumentSigned == false) {
      return false;
    }

    //# 54317
    // if (
    //   this.approvedSelected == true &&
    //   this.Annex2ElectronicallyFill == true &&
    //   (this.model.Annex2OfSMTA2SignatureId === undefined ||
    //     this.model.Annex2OfSMTA2SignatureId === "" ||
    //     this.model.Annex2OfSMTA2SignatureId === null)
    // ) {
    //   return false;
    // }

    if (
      this.approvedSelected == true &&
      this.Annex2ElectronicallyFill == true &&
      (this.model.Annex2OfSMTA2SignatureText === undefined ||
        this.model.Annex2OfSMTA2SignatureText === "" ||
        this.model.Annex2OfSMTA2SignatureText === null)
    ) {
      return false;
    }
    //////////////////////////

    if (this.approvedSelected == false && this.model.Comment != "") {
      return true;
    }

    if (
      this.approvedSelected == true &&
      this.Annex2OfSMTA2FormCompleted == true &&
      this.model.Annex2ApprovalFlag == true
    ) {
      return true;
    }
    return false;
  }

  get Annex2OfSMTA2FormCompleted(): boolean {
    if (this.SubmitAnnex2OfSMTA2 || this.WaitingForAnnex2OfSMTA2SECsApproval) {
      if (
        this.model.WorklistFromBioHubItemMaterials === undefined ||
        this.model.WorklistFromBioHubItemMaterials.length == 0
      ) {
        return false;
      }
      if (
        this.model.LaboratoryFocalPoints === undefined ||
        this.model.LaboratoryFocalPoints.length == 0
      ) {
        return false;
      }

      let materialFormCompleted = true;
      this.model.WorklistFromBioHubItemMaterials.forEach((elem) => {
        if (
          elem.Quantity === undefined ||
          elem.Quantity === null ||
          isNaN(elem.Quantity) ||
          elem.Quantity <= 0
        ) {
          materialFormCompleted = false;
        }

        // if (elem.Quantity > elem.AvailableQuantity) {
        //   materialFormCompleted = false;
        // }

        if (
          elem.Amount === undefined ||
          elem.Amount === null ||
          isNaN(elem.Amount) ||
          elem.Amount <= 0
        ) {
          materialFormCompleted = false;
        }
      });

      if (materialFormCompleted !== true) {
        return materialFormCompleted;
      }

      let Annex2OfSMTA2ConditionsFormCompleted = true;
      this.model.WorklistFromBioHubItemAnnex2OfSMTA2Conditions.forEach(
        (elem) => {
          if (elem.Mandatory === true && elem.Flag !== true) {
            Annex2OfSMTA2ConditionsFormCompleted = false;
          }
        }
      );

      return Annex2OfSMTA2ConditionsFormCompleted;
    } else {
      return true;
    }
  }

  get currentUserNameAndTitle(): string {
    if (this.WaitingForAnnex2OfSMTA2SECsApproval) {
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
    if (this.WaitingForAnnex2OfSMTA2SECsApproval) {
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

  async setNewAnnex2OfSMTA2Document(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_DOCUMENT(
      fileSelected
    );
    this.setDocumentFileType(DocumentFileType.Annex2OfSMTA2);

    this.documentFileName = fileSelected.name;
  }

  async setNewAnnex2OfSMTA2Signature(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_SIGNATURE(
      fileSelected
    );
    this.setDocumentFileType(DocumentFileType.Annex2OfSMTA2);

    this.signatureFileName = fileSelected.name;
    this.createBase64ImageForSignaturePreview();
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

  updateLastSubmissionApproved(approved: boolean) {
    this.model.LastSubmissionApproved = approved;
    this.$emit("update", this.model);
  }

  submit() {
    if (this.CurrentStatus == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2) {
      this.updateLastSubmissionApproved(true);
    }
    this.setSaveAsDraft(false);
    this.$emit("submit");
  }

  saveAsDraft() {
    this.setSaveAsDraft(true);
    this.$emit("saveAsDraft");
  }

  async loadAllBioHubFacilityMaterials() {
    const laboratoryId = WorklistFromBioHubItemModule.LaboratoryId;

    const bioHubFacilityId = WorklistFromBioHubItemModule.BioHubFacilityId;
    if (
      bioHubFacilityId != "" &&
      bioHubFacilityId != null &&
      bioHubFacilityId != undefined
    ) {
      const worklistFromBioHubItemId = this.$route.params.id;
      const info = new Map<string, string>();
      info.set("WorklistFromBioHubItemId", worklistFromBioHubItemId);
      info.set("BioHubFacilityId", bioHubFacilityId);
      await MaterialModule.ListMaterialsForWorklistFromBioHubItem(info);
    }
  }

  async mounted() {
    if (
      this.model.Annex2FillingOption == WorklistFillingOption.ElectronicallyFill
    ) {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(
        AttachmentType.Signature
      );
    } else {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
    }

    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();
    await BioHubFacilityModule.ListBioHubFacilities();
    await CountryModule.ListCountries();

    const laboratoryId = WorklistFromBioHubItemModule.LaboratoryId;

    if (this.model.IsPast == true) {
      if (this.SubmitAnnex2OfSMTA2) {
        await DocumentModule.ListSignedSMTADocuments();
      }
    } else {
      await DocumentModule.CheckSMTA2Document(laboratoryId).catch((err) => {
        console.log(err);
      });
    }

    const worklistFromBioHubItemId = this.$route.params.id;

    const info = new Map<string, string>();
    info.set("LaboratoryId", laboratoryId);
    info.set("WorklistFromBioHubItemId", worklistFromBioHubItemId);

    await UserModule.ListUsersByLaboratoryIdForWorklistFromBioHubItem(info);
  }

  @Watch("model.Annex2FillingOption")
  annex2FillingOption() {
    if (
      this.model.Annex2FillingOption == WorklistFillingOption.ElectronicallyFill
    ) {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(
        AttachmentType.Signature
      );
    } else {
      WorklistFromBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
    }
  }

  @Watch("model.BioHubFacilityId")
  async BioHubFacilityIdChange(oldValue: any, newValue: string) {
    if (
      oldValue != "" &&
      oldValue != null &&
      oldValue != undefined &&
      newValue != "" &&
      newValue != null &&
      newValue != undefined
    ) {
      WorklistFromBioHubItemModule.REMOVE_ALL_MATERIALS();
    }

    await this.loadAllBioHubFacilityMaterials();
  }

  @Watch("model.CurrentDownloadSMTA2DocumentId")
  currentDownloadSMTA2DocumentId() {
    if (this.SubmitAnnex2OfSMTA2 && this.model.IsPast == true) {
      this.model.SMTA2ApprovalStatus =
        SMTAApprovalStatus.DocumentApprovalComplete;
      this.model.SMTA2ApprovalDate = null;
      this.model.CurrentDownloadSMTA2DocumentName = "";

      const documentInfo = DocumentModule.Documents.filter((d) => {
        return d.Id == this.model.CurrentDownloadSMTA2DocumentId;
      });

      if (documentInfo.length != 0) {
        this.model.SMTA2ApprovalDate = documentInfo[0].UploadTime;
        this.model.CurrentDownloadSMTA2DocumentName =
          documentInfo[0].Name + "." + documentInfo[0].Extension;
      }

      this.update();
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
