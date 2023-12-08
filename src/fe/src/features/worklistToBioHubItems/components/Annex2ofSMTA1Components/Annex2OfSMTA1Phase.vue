<template>
  <div>
    <v-card v-if="SubmitAnnex2OfSMTA1" outlined class="ma-2 annex2-smta1">
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <v-card-actions v-if="model.IsPast == true">
          <h4 style="color: red">Please select a signed SMTA 1 document</h4>
          <dropdown
            v-model="model.CurrentDownloadSMTA1DocumentId"
            :items="SignedSmta1Documents"
            item-text="Text"
            item-value="Value"
            :menu-props="{ auto: true }"
            label=""
            :readonly="!hasSubmitPermissionByStatus"
            property-name="CurrentDownloadSMTA1DocumentId"
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
            <p>Please download the Annex 2 of SMTA 1 Template</p>
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
              @executeSave="setNewAnnex2OfSMTA1Document"
            >
            </FormPopup>
            <v-spacer></v-spacer>
          </div>
          <div v-if="SMTA1DocumentSigned == false">
            <h4 style="color: red">
              Warning: the SMTA 1 has not been signed yet. Please complete the
              SMTA 1 request
            </h4>
          </div>
          <v-card-actions
            v-if="
              documentUploaded &&
              SMTA1DocumentSigned == true &&
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
          <Annex2OfSMTA1Form
            v-model="model"
            :can-edit="hasSubmitPermissionByStatus"
            :can-read="hasReadPermissionByStatus"
          >
          </Annex2OfSMTA1Form>
          <v-card-actions>
            <v-spacer></v-spacer>

            <v-container class="px-0" fluid>
              <v-row>
                <v-col class="text-right" cols="12" sm="6" md="6">
                  <p>{{ currentUserNameAndTitle }}</p>
                </v-col>
                <v-col cols="12" sm="6" md="6">
                  <text-field
                    v-model="model.Annex2OfSMTA1SignatureText"
                    label="Signature"
                    :readonly="false"
                    :properties-errors="propertiesErrors"
                    property-name="Annex2OfSMTA1SignatureText"
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
                    model.Annex2OfSMTA1SignatureText == null ||
                    model.Annex2OfSMTA1SignatureText == undefined ||
                    model.Annex2OfSMTA1SignatureText == ''
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
            </v-container>

            <!--# 54317 -->
            <!-- <FormPopup
            ref="newSignatureFilePopup"
            v-model="newFilePopupItems"
            title="Add Signature"
            :properties-errors="error"
            accept=".jpeg, .jpg"
            @executeSave="setNewAnnex2OfSMTA1Signature"
          >
          </FormPopup> -->
            <!-- ------------------- -->
          </v-card-actions>
          <CardActionsGenericButton
            style="display: inline-block; float: right"
            color="primary"
            text="Save As Draft"
            @click="saveAsDraft"
          >
          </CardActionsGenericButton>
          <div v-if="SMTA1DocumentSigned == false">
            <h4 style="color: red">
              Warning: the SMTA 1 has not been signed yet. Please complete the
              SMTA 1 request
            </h4>
          </div>
          <CardActionsGenericButton
            style="display: inline-block; float: right"
            v-if="submitAnnex2OfSMTA1FormVisible"
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
      v-if="WaitingForAnnex2OfSMTA1SECsApproval"
      outlined
      class="ma-2 annex2-smta1"
    >
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div v-if="Annex2ElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the signed Annex 2 of SMTA 1</p>
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
        <Annex2OfSMTA1Form
          v-model="model"
          :can-edit="hasSubmitPermissionByStatus"
          :can-read="hasReadPermissionByStatus"
        >
        </Annex2OfSMTA1Form>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-container class="px-0" fluid>
            <v-row>
              <v-col class="text-right" cols="12" sm="6" md="6">
                <p>{{ currentUserNameAndTitle }}</p>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <text-field
                  v-model="model.Annex2OfSMTA1SignatureText"
                  label="Signature"
                  :readonly="true"
                  :properties-errors="propertiesErrors"
                  property-name="Annex2OfSMTA1SignatureText"
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
                  (model.Annex2OfSMTA1SignatureText == null ||
                    model.Annex2OfSMTA1SignatureText == undefined ||
                    model.Annex2OfSMTA1SignatureText == '')
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
              label="I confirm that Annex 2 of SMTA 1 is filled in satisfactorily"
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
          <div v-if="SMTA1DocumentSigned == false">
            <h4 style="color: red">
              Warning: the SMTA 1 has not been signed yet. Please complete the
              SMTA 1 request
            </h4>
          </div>
          <v-container
            v-if="submitWaitingForAnnex2OfSMTA1SECsApprovalVisible"
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
import Annex2OfSMTA1Form from "./Annex2OfSMTA1Form.vue";
import { AttachmentType } from "@/models/enums/AttachmentType";
import Checkbox from "@/components/Checkbox.vue";
import DatePicker from "@/components/DatePicker.vue";
import { BioHubFacilityModule } from "../../../biohubfacilities/store";
import { DocumentModule } from "../../../documents/store";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { UserModule } from "../../../users/store";
import { TransportCategoryModule } from "../../../transportCategories/store";
import { GeneticSequenceDataModule } from "../../../geneticSequenceDatas/store";
import { SpecimenTypeModule } from "../../../specimenTypes/store";
import { TemperatureUnitOfMeasureModule } from "../../../temperatureUnitOfMeasures/store";
import { SeedData } from "@/models/constants/SeedData";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { SMTAApprovalStatus } from "@/models/enums/SMTAApprovalStatus";
import { DocumentTemplateModule } from "../../../documentTemplates/store";
import { PermissionType } from "@/models/enums/PermissionType";
import { DropdownItem } from "@/models/DropdownItem";
import Dropdown from "@/components/Dropdown.vue";

@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    Annex2OfSMTA1Form,
    Checkbox,
    DatePicker,
    TextField,
    Dropdown,
  },
})
export default class Annex2OfSMTA1Phase extends Vue {
  private newFilePopupItems: Array<FormPopupItem> = [];

  private documentFileName = "";
  private signatureFileName = "";
  private base64SignaturePreview = "";
  private currentDate = this.getFormatDate(new Date());

  $refs!: {
    newDocumentFilePopup: FormPopup;
    newSignatureFilePopup: FormPopup;
  };

  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get ImageSrcAnnex2OfSMTA1(): string {
    const src =
      "data:image/jpeg;base64," + this.model.Annex2OfSMTA1SignatureString;
    return src;
  }

  get Annex2SignatureWarningVisible(): boolean {
    return (
      (this.model.Annex2OfSMTA1SignatureId === undefined ||
        this.model.Annex2OfSMTA1SignatureId === null ||
        this.model.Annex2OfSMTA1SignatureId === "") &&
      (this.base64SignaturePreview === "" ||
        this.base64SignaturePreview === null ||
        this.base64SignaturePreview === undefined)
    );
  }

  get Annex2SignatureButtonTitle(): string {
    if (
      this.model.Annex2OfSMTA1SignatureId !== undefined &&
      this.model.Annex2OfSMTA1SignatureId !== null &&
      this.model.Annex2OfSMTA1SignatureId !== ""
    ) {
      return "Change the image of the signature";
    }
    return "Upload the image of the signature";
  }

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get documentId(): string {
    return this.model.Annex2OfSMTA1DocumentId;
  }

  get worklistId(): string {
    return this.model.Id;
  }

  get documentName(): string {
    return this.model.Annex2OfSMTA1DocumentName;
  }

  get Annex2ElectronicallyFill(): boolean {
    return (
      this.model.Annex2FillingOption == WorklistFillingOption.ElectronicallyFill
    );
  }

  get approvedSelected(): boolean {
    return this.model.LastSubmissionApproved;
  }

  get SubmitAnnex2OfSMTA1(): boolean {
    return (
      this.model.CurrentStatus == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1
    );
  }

  get WaitingForAnnex2OfSMTA1SECsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval
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

  get documentUploaded(): boolean {
    return (
      WorklistToBioHubItemModule.WorklistToBioHubItemDocument !== undefined
    );
  }

  get signatureUploaded(): boolean {
    return (
      WorklistToBioHubItemModule.WorklistToBioHubItemSignature !== undefined
    );
  }

  get submitDocumentVisible(): boolean {
    return (
      (this.approvedSelected == true && this.documentUploaded == true) ||
      (this.approvedSelected == false && this.model.Comment != "")
    );
  }

  get BioHubFacilityIdValid(): boolean {
    if (
      this.model.BioHubFacilityId === undefined ||
      this.model.BioHubFacilityId === "" ||
      this.model.BioHubFacilityId === null
    ) {
      return false;
    }
    return true;
  }

  get SMTA1DocumentSigned(): boolean {
    if (this.model.IsPast != true) {
      return DocumentModule.SMTA1DocumentSigned;
    } else {
      return (
        this.model.CurrentDownloadSMTA1DocumentId != "" &&
        this.model.CurrentDownloadSMTA1DocumentId != null &&
        this.model.CurrentDownloadSMTA1DocumentId != undefined
      );
    }
  }

  get submitAnnex2OfSMTA1FormVisible(): boolean {
    //# 54317
    // return (
    //this.SMTA1DocumentSigned &&
    //   (this.signatureUploaded == true ||
    //     (this.model.Annex2OfSMTA1SignatureId !== undefined &&
    //       this.model.Annex2OfSMTA1SignatureId !== "" &&
    //       this.model.Annex2OfSMTA1SignatureId !== null)) &&
    //   this.Annex2OfSMTA1FormCompleted == true &&
    //this.model.BioHubFacilityId !== undefined &&
    // this.model.BioHubFacilityId !== "" &&
    // this.model.BioHubFacilityId !== null
    // );

    return (
      this.SMTA1DocumentSigned &&
      this.model.Annex2OfSMTA1SignatureText !== undefined &&
      this.model.Annex2OfSMTA1SignatureText !== "" &&
      this.model.Annex2OfSMTA1SignatureText !== null &&
      this.Annex2OfSMTA1FormCompleted == true &&
      (this.model.IsPast != true || this.model.AssignedOperationDate != null) &&
      this.model.BioHubFacilityId !== undefined &&
      this.model.BioHubFacilityId !== "" &&
      this.model.BioHubFacilityId !== null
    );
    //////////////////////////
  }

  get submitWaitingForAnnex2OfSMTA1SECsApprovalVisible(): boolean {
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

    if (this.approvedSelected == true && this.SMTA1DocumentSigned == false) {
      return false;
    }
    //# 54317
    // if (
    //   this.approvedSelected == true &&
    //   this.Annex2ElectronicallyFill == true &&
    //   (this.model.Annex2OfSMTA1SignatureId === undefined ||
    //     this.model.Annex2OfSMTA1SignatureId === "" ||
    //     this.model.Annex2OfSMTA1SignatureId === null ||
    //this.model.BioHubFacilityId === undefined ||
    //  this.model.BioHubFacilityId === "" ||
    //  this.model.BioHubFacilityId === null
    // ) {
    //   return false;
    // }
    if (
      this.approvedSelected == true &&
      this.Annex2ElectronicallyFill == true &&
      (this.model.Annex2OfSMTA1SignatureText === undefined ||
        this.model.Annex2OfSMTA1SignatureText === "" ||
        this.model.Annex2OfSMTA1SignatureText === null)
    ) {
      return false;
    }
    //////////////////////////

    if (this.approvedSelected == false && this.model.Comment != "") {
      return true;
    }

    if (
      this.approvedSelected == true &&
      this.Annex2OfSMTA1FormCompleted == true &&
      this.model.Annex2ApprovalFlag == true
    ) {
      return true;
    }
    return false;
  }

  get Annex2OfSMTA1FormCompleted(): boolean {
    if (
      this.model.MaterialShippingInformations === undefined ||
      this.model.MaterialShippingInformations.length == 0
    ) {
      return false;
    }
    if (
      this.model.LaboratoryFocalPoints === undefined ||
      this.model.LaboratoryFocalPoints.length == 0
    ) {
      return false;
    }

    let materialShippingFormCompleted = true;
    this.model.MaterialShippingInformations.forEach((elem) => {
      if (
        elem.MaterialProductId === undefined ||
        elem.MaterialProductId === ""
      ) {
        materialShippingFormCompleted = false;
      }

      if (
        elem.TransportCategoryId === undefined ||
        elem.TransportCategoryId === ""
      ) {
        materialShippingFormCompleted = false;
      }

      if (
        elem.Quantity === undefined ||
        elem.Quantity === null ||
        elem.Quantity <= 0
      ) {
        materialShippingFormCompleted = false;
      }

      if (
        elem.Amount === undefined ||
        elem.Amount === null ||
        elem.Amount <= 0
      ) {
        materialShippingFormCompleted = false;
      }

      if (elem.Condition === undefined || elem.Condition === "") {
        materialShippingFormCompleted = false;
      }

      if (
        elem.MaterialClinicalDetails === undefined ||
        elem.MaterialClinicalDetails.length == 0
      ) {
        materialShippingFormCompleted = false;
      }

      elem.MaterialClinicalDetails.forEach((materialClinicalDetail) => {
        if (
          materialClinicalDetail.MaterialNumber === undefined ||
          materialClinicalDetail.MaterialNumber === ""
        ) {
          materialShippingFormCompleted = false;
        }
        if (
          materialClinicalDetail.CollectionDate === undefined ||
          materialClinicalDetail.CollectionDate === null
        ) {
          materialShippingFormCompleted = false;
        }

        if (
          materialClinicalDetail.Location === undefined ||
          materialClinicalDetail.Location === ""
        ) {
          materialShippingFormCompleted = false;
        }

        if (
          materialClinicalDetail.IsolationHostTypeId === undefined ||
          materialClinicalDetail.IsolationHostTypeId === ""
        ) {
          materialShippingFormCompleted = false;
        }

        if (
          materialClinicalDetail.Gender === undefined ||
          materialClinicalDetail.Gender === null
        ) {
          materialShippingFormCompleted = false;
        }

        if (
          materialClinicalDetail.Age === undefined ||
          materialClinicalDetail.Age === null ||
          materialClinicalDetail.Age < 0 ||
          isNaN(materialClinicalDetail.Age) === true
        ) {
          materialShippingFormCompleted = false;
        }

        if (
          materialClinicalDetail.PatientStatus === undefined ||
          materialClinicalDetail.PatientStatus === ""
        ) {
          materialShippingFormCompleted = false;
        }
      });

      elem.MaterialLaboratoryAnalysisInformation.forEach(
        (materialLaboratoryAnalysisInformation) => {
          if (
            materialLaboratoryAnalysisInformation.MaterialNumber ===
              undefined ||
            materialLaboratoryAnalysisInformation.MaterialNumber === ""
          ) {
            materialShippingFormCompleted = false;
          }
          if (
            materialLaboratoryAnalysisInformation.FreezingDate === undefined ||
            materialLaboratoryAnalysisInformation.FreezingDate === null
          ) {
            materialShippingFormCompleted = false;
          }

          if (
            materialLaboratoryAnalysisInformation.Temperature === undefined ||
            materialLaboratoryAnalysisInformation.Temperature === null
          ) {
            materialShippingFormCompleted = false;
          }

          if (
            materialLaboratoryAnalysisInformation.UnitOfMeasureId ===
              undefined ||
            materialLaboratoryAnalysisInformation.UnitOfMeasureId === ""
          ) {
            materialShippingFormCompleted = false;
          }

          if (
            materialLaboratoryAnalysisInformation.VirusConcentration ===
              undefined ||
            materialLaboratoryAnalysisInformation.VirusConcentration === ""
          ) {
            materialShippingFormCompleted = false;
          }

          if (elem.MaterialProductId == SeedData.CulturedIsolateProductId) {
            if (
              materialLaboratoryAnalysisInformation.CulturingCellLine ===
                undefined ||
              materialLaboratoryAnalysisInformation.CulturingCellLine === ""
            ) {
              materialShippingFormCompleted = false;
            }

            if (
              materialLaboratoryAnalysisInformation.CulturingPassagesNumber ===
                undefined ||
              materialLaboratoryAnalysisInformation.CulturingPassagesNumber <= 0
            ) {
              materialShippingFormCompleted = false;
            }
          } else if (
            elem.MaterialProductId == SeedData.ClinicalSpecimenProductId
          ) {
            if (
              materialLaboratoryAnalysisInformation.CollectedSpecimenTypes
                .length == 0
            ) {
              materialShippingFormCompleted = false;
            }

            if (
              materialLaboratoryAnalysisInformation.TypeOfTransportMedium ===
                undefined ||
              materialLaboratoryAnalysisInformation.TypeOfTransportMedium == ""
            ) {
              materialShippingFormCompleted = false;
            }

            if (
              materialLaboratoryAnalysisInformation.BrandOfTransportMedium ===
                undefined ||
              materialLaboratoryAnalysisInformation.BrandOfTransportMedium == ""
            ) {
              materialShippingFormCompleted = false;
            }
          }

          if (
            materialLaboratoryAnalysisInformation.GSDUploadedToDatabase ===
              undefined ||
            materialLaboratoryAnalysisInformation.GSDUploadedToDatabase === null
          ) {
            materialShippingFormCompleted = false;
          }

          if (
            materialLaboratoryAnalysisInformation.GSDUploadedToDatabase ==
            YesNoOption.Yes
          ) {
            if (
              materialLaboratoryAnalysisInformation.DatabaseUsedForGSDUploadingId ===
                undefined ||
              materialLaboratoryAnalysisInformation.DatabaseUsedForGSDUploadingId ==
                ""
            ) {
              materialShippingFormCompleted = false;
            }

            if (
              materialLaboratoryAnalysisInformation.AccessionNumberInGSDDatabase ===
                undefined ||
              materialLaboratoryAnalysisInformation.AccessionNumberInGSDDatabase ==
                ""
            ) {
              materialShippingFormCompleted = false;
            }
          }
        }
      );
    });

    return materialShippingFormCompleted;
  }

  get currentUserNameAndTitle(): string {
    if (this.WaitingForAnnex2OfSMTA1SECsApproval) {
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
    if (this.WaitingForAnnex2OfSMTA1SECsApproval) {
      return this.getFormatDate(this.model.OperationDate);
    } else {
      return this.currentDate;
    }
  }

  get SignedSmta1Documents(): Array<DropdownItem> {
    const smtaDocuments = DocumentModule.Documents.filter((dt) => {
      return dt.FileType == DocumentFileType.SMTA1;
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
    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_SIGNATURE(undefined);
    this.signatureFileName = "";
    this.base64SignaturePreview = "";
  }

  async setNewAnnex2OfSMTA1Document(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_DOCUMENT(fileSelected);
    this.setDocumentFileType(DocumentFileType.Annex2OfSMTA1);

    this.documentFileName = fileSelected.name;
  }

  async setNewAnnex2OfSMTA1Signature(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_SIGNATURE(fileSelected);
    this.setDocumentFileType(DocumentFileType.Annex2OfSMTA1);

    this.signatureFileName = fileSelected.name;
    this.createBase64ImageForSignaturePreview();
  }

  setDocumentFileType(documentFileType: DocumentFileType) {
    this.model.DocumentTemplateFileType = documentFileType;
    this.$emit("update", this.model);
  }

  setOriginalDocumentTemplateId(originalDocumentTemplateId: string) {
    this.model.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId =
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
    const file = WorklistToBioHubItemModule.SignatureFile;

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
    if (this.CurrentStatus == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1) {
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
    WorklistToBioHubItemModule.CLEAR_TEMPORARY_MATERIAL_SHIPPING_INFORMATION();
    if (
      this.model.Annex2FillingOption == WorklistFillingOption.ElectronicallyFill
    ) {
      WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Signature);
    } else {
      WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
    }

    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();
    await BioHubFacilityModule.ListBioHubFacilities();

    await GeneticSequenceDataModule.ListGeneticSequenceDatas();
    await SpecimenTypeModule.ListSpecimenTypes();
    await TemperatureUnitOfMeasureModule.ListTemperatureUnitOfMeasures();

    const laboratoryId = WorklistToBioHubItemModule.LaboratoryId;

    if (this.model.IsPast == true) {
      if (this.SubmitAnnex2OfSMTA1) {
        await DocumentModule.ListSignedSMTADocuments();
      }
    } else {
      await DocumentModule.CheckSMTA1Document(laboratoryId).catch((err) => {
        console.log(err);
      });
    }

    const worklistToBioHubItemId = this.$route.params.id;

    const info = new Map<string, string>();
    info.set("LaboratoryId", laboratoryId);
    info.set("WorklistToBioHubItemId", worklistToBioHubItemId);

    await UserModule.ListUsersByLaboratoryIdForWorklistToBioHubItem(info);
  }

  @Watch("model.Annex2FillingOption")
  annex2FillingOption() {
    if (
      this.model.Annex2FillingOption == WorklistFillingOption.ElectronicallyFill
    ) {
      WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Signature);
    } else {
      WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
    }
  }

  @Watch("model.CurrentDownloadSMTA1DocumentId")
  currentDownloadSMTA1DocumentId() {
    if (this.SubmitAnnex2OfSMTA1 && this.model.IsPast == true) {
      this.model.SMTA1ApprovalStatus =
        SMTAApprovalStatus.DocumentApprovalComplete;
      this.model.SMTA1ApprovalDate = null;
      this.model.CurrentDownloadSMTA1DocumentName = "";

      const documentInfo = DocumentModule.Documents.filter((d) => {
        return d.Id == this.model.CurrentDownloadSMTA1DocumentId;
      });

      if (documentInfo.length != 0) {
        this.model.SMTA1ApprovalDate = documentInfo[0].UploadTime;
        this.model.CurrentDownloadSMTA1DocumentName =
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
