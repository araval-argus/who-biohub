<template>
  <div>
    <v-card v-if="SubmitBookingFormOfSMTA2" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <v-card-actions>
          <v-spacer></v-spacer>
        </v-card-actions>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <v-radio-group v-model="model.BookingFormFillingOption">
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
        <div v-if="BookingFormElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the Booking Form of SMTA 2 Template</p>
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
              @executeSave="setNewBookingFormOfSMTA2Document"
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
          <BookingFormTabsComponent
            :can-edit="hasSubmitPermissionByStatus"
            :can-read="hasReadPermissionByStatus"
            :booking-forms="BookingForms"
            :request-user-first-name="RequestUserFirstName"
            :request-user-last-name="RequestUserLastName"
            :request-user-email="RequestUserEmail"
            :request-user-job-title="RequestUserJobTitle"
            :request-user-mobile-phone="RequestUserMobilePhone"
            :request-user-business-phone="RequestUserBusinessPhone"
            :delivery-institute-address="model.LaboratoryAddress"
            :delivery-institute-name="model.LaboratoryName"
            :delivery-institute-country="model.LaboratoryCountry"
            :pickup-institute-address="model.BioHubFacilityAddress"
            :pickup-institute-name="model.BioHubFacilityName"
            :pickup-institute-country="model.BioHubFacilityCountry"
            pickup-institute-title="BioHub Facility"
            delivery-institute-title="The Laboratory"
            :courier-visible="courierVisible"
            :delivery-users="LaboratoryFocalPoints"
            :all-possible-pickup-users="AllPossiblePickupUsers"
            :is-from-bio-hub="true"
            :can-edit-materials-table="CanEditMaterialsTable"
            :materials="Materials"
            @updateBookingForm="updateBookingForm"
            @addBookingFormCourierUser="addBookingFormCourierUser"
            @removeBookingFormCourierUser="removeBookingFormCourierUser"
            @clearBookingFormCourierUser="clearBookingFormCourierUser"
            @addBookingFormPickupUser="addBookingFormPickupUser"
            @removeBookingFormPickupUser="removeBookingFormPickupUser"
            @updateMaterial="updateMaterial"
          >
          </BookingFormTabsComponent>
          <v-card-actions>
            <v-spacer></v-spacer>

            <v-container class="px-0" fluid>
              <v-row>
                <!-- # 54317 -->
                <!-- <v-col cols="12" sm="6" md="6">
                <v-img
                  class="signature-hover"
                  src="@/assets/icons/signature.png"
                  width="50"
                  @click="addSignatureFile"
                >
                </v-img>
                <p>{{ BookingFormSignatureButtonTitle }}</p>

                <h4
                  v-if="BookingFormSignatureWarningVisible"
                  style="color: red"
                >
                  Please add a Signature file
                </h4>
                <v-img
                  v-if="model.BookingFormOfSMTA2SignatureId"
                  :src="ImageSrcBookingFormOfSMTA2"
                />
                <div v-if="base64SignaturePreview != ''">
                  <p>Preview</p>
                  <v-btn color="ms-3 warning" @click="cancelSignatureFile"
                    >Clear Signature</v-btn
                  >
                  {{ signatureFileName }}
                  <v-img :src="base64SignaturePreview" />
                </div>
              </v-col> -->
                <v-col cols="12" sm="6" md="6">
                  <h4
                    v-if="
                      model.BookingFormOfSMTA2SignatureText == null ||
                      model.BookingFormOfSMTA2SignatureText == undefined ||
                      model.BookingFormOfSMTA2SignatureText == ''
                    "
                    style="color: red"
                  >
                    Please add a Signature
                  </h4>
                  <text-field
                    v-model="model.BookingFormOfSMTA2SignatureText"
                    label="Signature"
                    :readonly="false"
                    :properties-errors="propertiesErrors"
                    property-name="BookingFormOfSMTA2SignatureText"
                    @input="update"
                  >
                  </text-field>
                </v-col>
                <!-- ------------------- -->
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
            @executeSave="setNewBookingFormOfSMTA2Signature"
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
          <CardActionsGenericButton
            v-if="submitBookingFormOfSMTA2FormVisible"
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

    <v-card v-if="WaitForBookingFormSMTA2OPSsApproval" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div v-if="BookingFormElectronicallyFill == false">
          <div class="shipment-action-vcard">
            <p class="text-h6">Download</p>
            <p>Please download the signed Booking Form of SMTA 2</p>
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
        <BookingFormTabsComponent
          :can-edit="hasSubmitPermissionByStatus"
          :can-read="hasReadPermissionByStatus"
          :booking-forms="BookingForms"
          :request-user-first-name="RequestUserFirstName"
          :request-user-last-name="RequestUserLastName"
          :request-user-email="RequestUserEmail"
          :request-user-job-title="RequestUserJobTitle"
          :request-user-mobile-phone="RequestUserMobilePhone"
          :request-user-business-phone="RequestUserBusinessPhone"
          :delivery-institute-address="model.LaboratoryAddress"
          :delivery-institute-name="model.LaboratoryName"
          :delivery-institute-country="model.LaboratoryCountry"
          :pickup-institute-address="model.BioHubFacilityAddress"
          :pickup-institute-name="model.BioHubFacilityName"
          :pickup-institute-country="model.BioHubFacilityCountry"
          pickup-institute-title="BioHub Facility"
          delivery-institute-title="The Laboratory"
          :courier-visible="courierVisible"
          :delivery-users="LaboratoryFocalPoints"
          :all-possible-pickup-users="AllPossiblePickupUsers"
          :all-possible-courier-users="AllPossibleCourierUsers"
          :is-from-bio-hub="true"
          :can-edit-materials-table="CanEditMaterialsTable"
          :materials="Materials"
          :couriers="Couriers"
          @updateBookingForm="updateBookingForm"
          @addBookingFormCourierUser="addBookingFormCourierUser"
          @removeBookingFormCourierUser="removeBookingFormCourierUser"
          @clearBookingFormCourierUser="clearBookingFormCourierUser"
          @addBookingFormPickupUser="addBookingFormPickupUser"
          @removeBookingFormPickupUser="removeBookingFormPickupUser"
          @updateMaterial="updateMaterial"
        >
        </BookingFormTabsComponent>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-container class="px-0" fluid>
            <v-row>
              <v-col cols="12" sm="6" md="6">
                <!-- # 54317 -->
                <!-- <v-img
                  v-if="model.BookingFormOfSMTA2SignatureId"
                  :src="ImageSrcBookingFormOfSMTA2"
                />
                <p
                  v-if="
                    !model.BookingFormOfSMTA2SignatureId &&
                    BookingFormElectronicallyFill == true
                  "
                  style="color: red"
                >
                  MISSING SIGNATURE !!!
                </p> -->
                <text-field
                  v-if="BookingFormElectronicallyFill == true"
                  v-model="model.BookingFormOfSMTA2SignatureText"
                  label="Signature"
                  :readonly="true"
                  :properties-errors="propertiesErrors"
                  property-name="BookingFormOfSMTA2SignatureText"
                  @input="update"
                >
                </text-field>
                <p
                  v-if="
                    (model.BookingFormOfSMTA2SignatureText == null ||
                      model.BookingFormOfSMTA2SignatureText == undefined ||
                      model.BookingFormOfSMTA2SignatureText == '') &&
                    BookingFormElectronicallyFill == true
                  "
                  style="color: red"
                >
                  MISSING SIGNATURE !!!
                </p>
                <!-- ------------------- -->
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
            <h4 v-if="model.BookingFormApprovalFlag != true" style="color: red">
              Please confirm
            </h4>
            <Checkbox
              v-model="model.BookingFormApprovalFlag"
              :readonly="!hasSubmitPermissionByStatus"
              label="I confirm that the Booking Form is filled in satisfactorily"
            ></Checkbox>

            <text-area
              v-model="model.BookingFormApprovalComment"
              label="Add a comment"
              :readonly="false"
              :properties-errors="propertiesErrors"
              property-name="BookingFormApprovalComment"
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
          <v-container
            v-if="submitWaitForBookingFormOfSMTA2OPSsApprovalVisible"
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
import { AttachmentType } from "@/models/enums/AttachmentType";
import Checkbox from "@/components/Checkbox.vue";
import BookingFormTabsComponent from "../../../worklistItemsCommonComponents/BookingFormComponents/BookingFormTabsComponent.vue";
import DatePicker from "@/components/DatePicker.vue";
import BiosafetyChecklistForm from "./../BiosafetyChecklistComponents/BiosafetyChecklistForm.vue";
import BiosafetyChecklistCommentFlowComponent from "./../BiosafetyChecklistComponents/BiosafetyChecklistCommentFlowComponent.vue";
import { DocumentModule } from "../../../documents/store";
import { UserModule } from "../../../users/store";
import { CourierModule } from "../../../couriers/store";
import { CountryModule } from "../../../countries/store";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { PermissionType } from "@/models/enums/PermissionType";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import { AuthModule } from "../../../auth/store";
import { Courier } from "@/models/Courier";

@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    Checkbox,
    BookingFormTabsComponent,
    DatePicker,
    TextField,
    BiosafetyChecklistForm,
    BiosafetyChecklistCommentFlowComponent,
  },
})
export default class BookingFormPhase extends Vue {
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

  get BookingForms(): Array<BookingFormOfSMTA> {
    return this.model.BookingForms;
  }

  get Couriers(): Array<Courier> {
    return CourierModule.Couriers;
  }

  get AllPossiblePickupUsers(): Array<WorklistItemUser> {
    return UserModule.WorklistFromBioHubItemAllBioHubFacilityUsers;
  }

  get AllPossibleCourierUsers(): Array<WorklistItemUser> {
    return UserModule.WorklistFromBioHubItemAllCourierUsers;
  }

  get RequestUserFirstName(): string {
    if (this.WaitForBookingFormSMTA2OPSsApproval == true) {
      return this.model.LastOperationUserFirstName;
    }
    return AuthModule.FirstName ?? "";
  }

  get RequestUserLastName(): string {
    if (this.WaitForBookingFormSMTA2OPSsApproval == true) {
      return this.model.LastOperationUserLastName;
    }
    return AuthModule.LastName ?? "";
  }

  get RequestUserEmail(): string {
    if (this.WaitForBookingFormSMTA2OPSsApproval == true) {
      return this.model.LastOperationUserEmail;
    }
    return AuthModule.Email ?? "";
  }

  get RequestUserJobTitle(): string {
    if (this.WaitForBookingFormSMTA2OPSsApproval == true) {
      return this.model.LastOperationUserJobTitle;
    }
    return AuthModule.JobTitle ?? "";
  }

  get RequestUserBusinessPhone(): string {
    if (this.WaitForBookingFormSMTA2OPSsApproval == true) {
      return this.model.LastOperationUserBusinessPhone;
    }
    return AuthModule.BusinessPhone ?? "";
  }

  get RequestUserMobilePhone(): string {
    if (this.WaitForBookingFormSMTA2OPSsApproval == true) {
      return this.model.LastOperationUserMobilePhone;
    }
    return AuthModule.MobilePhone ?? "";
  }

  get courierVisible(): boolean {
    return (
      WorklistFromBioHubItemModule.Status ==
      WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval
    );
  }

  get LaboratoryFocalPoints(): Array<WorklistItemUser> {
    return WorklistFromBioHubItemModule.LaboratoryFocalPoints;
  }

  get ImageSrcBookingFormOfSMTA2(): string {
    const src =
      "data:image/jpeg;base64," + this.model.BookingFormOfSMTA2SignatureString;
    return src;
  }

  get CanEditMaterialsTable(): boolean {
    return (
      this.hasSubmitPermissionByStatus &&
      (WorklistFromBioHubItemModule.Status ==
        WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2 ||
        WorklistFromBioHubItemModule.BookingFormFillingOption ==
          WorklistFillingOption.DocumentUpload)
    );
  }

  get Materials(): Array<WorklistFromBioHubItemMaterial> {
    return WorklistFromBioHubItemModule.WorklistFromBioHubItemMaterials;
  }

  get BookingFormSignatureWarningVisible(): boolean {
    return (
      (this.model.BookingFormOfSMTA2SignatureId === undefined ||
        this.model.BookingFormOfSMTA2SignatureId === null ||
        this.model.BookingFormOfSMTA2SignatureId === "") &&
      (this.base64SignaturePreview === "" ||
        this.base64SignaturePreview === null ||
        this.base64SignaturePreview === undefined)
    );
  }

  get BookingFormSignatureButtonTitle(): string {
    if (
      this.model.BookingFormOfSMTA2SignatureId !== undefined &&
      this.model.BookingFormOfSMTA2SignatureId !== null &&
      this.model.BookingFormOfSMTA2SignatureId !== ""
    ) {
      return "Change the image of the signature";
    }
    return "Upload the image of the signature";
  }

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get documentId(): string {
    return this.model.BookingFormOfSMTA2DocumentId;
  }

  get worklistId(): string {
    return this.model.Id;
  }

  get documentName(): string {
    return this.model.BookingFormOfSMTA2DocumentName;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get BookingFormElectronicallyFill(): boolean {
    return (
      this.model.BookingFormFillingOption ==
      WorklistFillingOption.ElectronicallyFill
    );
  }

  get SubmitBookingFormOfSMTA2(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2
    );
  }

  get WaitForBookingFormSMTA2OPSsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval
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
    return DocumentModule.SMTA2DocumentSigned;
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

  get submitBookingFormOfSMTA2FormVisible(): boolean {
    //# 54317
    // return (
    //   (this.signatureUploaded == true ||
    //     (this.model.BookingFormOfSMTA2SignatureId !== undefined &&
    //       this.model.BookingFormOfSMTA2SignatureId !== "" &&
    //       this.model.BookingFormOfSMTA2SignatureId !== null)) &&
    //   this.BookingFormOfSMTA2FormCompleted == true
    // );
    return (
      this.model.BookingFormOfSMTA2SignatureText !== undefined &&
      this.model.BookingFormOfSMTA2SignatureText !== "" &&
      this.model.BookingFormOfSMTA2SignatureText !== null &&
      this.BookingFormOfSMTA2FormCompleted == true &&
      (this.model.IsPast != true || this.model.AssignedOperationDate != null)
    );
    ////////////////////////
  }

  get submitWaitForBookingFormOfSMTA2OPSsApprovalVisible(): boolean {
    //# 54317
    // if (
    //   this.approvedSelected == true &&
    //   this.BookingFormElectronicallyFill == true &&
    //   (this.model.BookingFormOfSMTA2SignatureId === undefined ||
    //     this.model.BookingFormOfSMTA2SignatureId === "" ||
    //     this.model.BookingFormOfSMTA2SignatureId === null)
    // ) {
    //   return false;
    // }
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (
      this.approvedSelected == true &&
      this.BookingFormElectronicallyFill == true &&
      (this.model.BookingFormOfSMTA2SignatureText === undefined ||
        this.model.BookingFormOfSMTA2SignatureText === "" ||
        this.model.BookingFormOfSMTA2SignatureText === null)
    ) {
      return false;
    }
    ////////////////////////

    if (this.approvedSelected == false && this.model.Comment != "") {
      return true;
    }

    if (
      this.approvedSelected == true &&
      this.BookingFormOfSMTA2FormCompleted == true &&
      this.model.BookingFormApprovalFlag == true
    ) {
      return true;
    }
    return false;
  }

  get BookingFormOfSMTA2FormCompleted(): boolean {
    if (
      this.SubmitBookingFormOfSMTA2 ||
      this.WaitForBookingFormSMTA2OPSsApproval
    ) {
      if (
        this.model.BookingForms === undefined ||
        this.model.BookingForms.length == 0
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

        if (elem.Quantity > elem.AvailableQuantity) {
          materialFormCompleted = false;
        }

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

      let bookingFormsCompleted = true;
      this.model.BookingForms.forEach((elem) => {
        if (elem.Date === undefined || elem.Date === null) {
          bookingFormsCompleted = false;
        }

        if (
          elem.RequestDateOfPickup === undefined ||
          elem.RequestDateOfPickup === null
        ) {
          bookingFormsCompleted = false;
        }

        if (
          elem.TemperatureTransportCondition === undefined ||
          elem.TemperatureTransportCondition == null
        ) {
          bookingFormsCompleted = false;
        }

        if (
          elem.BookingFormPickupUsers === undefined ||
          elem.BookingFormPickupUsers.length == 0
        ) {
          bookingFormsCompleted = false;
        }

        if (
          this.WaitForBookingFormSMTA2OPSsApproval &&
          (elem.BookingFormCourierUsers === undefined ||
            elem.BookingFormCourierUsers.length == 0 ||
            elem.EstimateDateOfPickup == null)
        ) {
          bookingFormsCompleted = false;
        }
      });

      return bookingFormsCompleted;
    } else {
      return true;
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

  async setNewBookingFormOfSMTA2Document(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_DOCUMENT(
      fileSelected
    );
    this.setDocumentFileType(DocumentFileType.BookingFormOfSMTA2);

    this.documentFileName = fileSelected.name;
  }

  async setNewBookingFormOfSMTA2Signature(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_SIGNATURE(
      fileSelected
    );
    this.setDocumentFileType(DocumentFileType.BookingFormOfSMTA2);

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

  updateBookingForm(item: BookingFormOfSMTA) {
    WorklistFromBioHubItemModule.UPDATE_BOOKING_FORM(item);
  }

  addBookingFormCourierUser(user: WorklistItemUser, bookingFormId: string) {
    const info = { BookingFormId: bookingFormId, Item: user };

    WorklistFromBioHubItemModule.ADD_BOOKINGFORM_COURIER_USER(info);
  }

  removeBookingFormCourierUser(id: string, bookingFormId: string) {
    const info = new Map<string, string>();
    info.set("BookingFormId", bookingFormId);
    info.set("Id", id);

    WorklistFromBioHubItemModule.REMOVE_BOOKINGFORM_COURIER_USER(info);
  }

  addBookingFormPickupUser(user: WorklistItemUser, bookingFormId: string) {
    const info = { BookingFormId: bookingFormId, Item: user };

    WorklistFromBioHubItemModule.ADD_BOOKINGFORM_PICKUP_USER(info);
  }

  removeBookingFormPickupUser(id: string, bookingFormId: string) {
    const info = new Map<string, string>();
    info.set("BookingFormId", bookingFormId);
    info.set("Id", id);
    WorklistFromBioHubItemModule.REMOVE_BOOKINGFORM_PICKUP_USER(info);
  }

  clearBookingFormCourierUser(bookingFormId: string) {
    WorklistFromBioHubItemModule.CLEAR_BOOKINGFORM_COURIER_USER(bookingFormId);
  }

  updateMaterial(
    material: WorklistFromBioHubItemMaterial,
    bookingFormId: string
  ) {
    WorklistFromBioHubItemModule.UPDATE_MATERIAL(material);

    let totalNumberOfVials = 0;
    let totalAmount = 0;

    const materials =
      WorklistFromBioHubItemModule.WorklistFromBioHubItemMaterials.filter(
        (m) => {
          return m.TransportCategoryId == material.TransportCategoryId;
        }
      );

    materials.forEach((elem) => {
      totalNumberOfVials = totalNumberOfVials + elem.Quantity;
      totalAmount = totalAmount + elem.Quantity * elem.Amount;
    });

    const info = {
      BookingFormId: bookingFormId,
      TotalNumberOfVials: totalNumberOfVials,
      TotalAmount: totalAmount,
    };

    WorklistFromBioHubItemModule.UPDATE_BOOKING_FORM_TOTALS(info);
  }

  submit() {
    if (
      this.CurrentStatus == WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2
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
    const laboratoryId = WorklistFromBioHubItemModule.LaboratoryId;
    const bioHubFacilityId = WorklistFromBioHubItemModule.BioHubFacilityId;
    const worklistFromBioHubItemId = this.$route.params.id;

    const info = new Map<string, string>();
    info.set("LaboratoryId", laboratoryId);
    info.set("WorklistFromBioHubItemId", worklistFromBioHubItemId);
    info.set("BioHubFacilityId", bioHubFacilityId);

    await UserModule.ListUsersByLaboratoryIdForWorklistFromBioHubItem(info);
    await UserModule.ListUsersByBioHubFacilityIdForWorklistFromBioHubItem(info);

    if (
      this.WaitForBookingFormSMTA2OPSsApproval &&
      hasPermission(PermissionNames.CanReadCourier)
    ) {
      await UserModule.ListCourierUsersForWorklistFromBioHubItem(info);
      await CourierModule.ListCouriers();
      await CountryModule.ListCountries();
    }
  }

  @Watch("model.BookingFormFillingOption")
  bookingFormFillingOption() {
    if (
      this.model.BookingFormFillingOption ==
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
