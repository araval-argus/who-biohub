<template>
  <div>
    <v-card v-if="SubmitBookingFormOfSMTA1" outlined>
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
            <p>Please download the Booking Form of SMTA 1 Template</p>
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
              @executeSave="setNewBookingFormOfSMTA1Document"
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
            :pickup-institute-address="model.LaboratoryAddress"
            :pickup-institute-name="model.LaboratoryName"
            :pickup-institute-country="model.LaboratoryCountry"
            :delivery-institute-address="model.BioHubFacilityAddress"
            :delivery-institute-name="model.BioHubFacilityName"
            :delivery-institute-country="model.BioHubFacilityCountry"
            pickup-institute-title="The Laboratory"
            delivery-institute-title="BioHub Facility"
            :courier-visible="courierVisible"
            :delivery-users="BioHubFacilityFocalPoints"
            :all-possible-pickup-users="AllPossiblePickupUsers"
            :material-shipping-informations="MaterialShippingInformations"
            @updateBookingForm="updateBookingForm"
            @addBookingFormCourierUser="addBookingFormCourierUser"
            @removeBookingFormCourierUser="removeBookingFormCourierUser"
            @clearBookingFormCourierUser="clearBookingFormCourierUser"
            @addBookingFormPickupUser="addBookingFormPickupUser"
            @removeBookingFormPickupUser="removeBookingFormPickupUser"
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
                  v-if="model.BookingFormOfSMTA1SignatureId"
                  :src="ImageSrcBookingFormOfSMTA1"
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
                      model.BookingFormOfSMTA1SignatureText == null ||
                      model.BookingFormOfSMTA1SignatureText == undefined ||
                      model.BookingFormOfSMTA1SignatureText == ''
                    "
                    style="color: red"
                  >
                    Please add a Signature
                  </h4>
                  <text-field
                    v-model="model.BookingFormOfSMTA1SignatureText"
                    label="Signature"
                    :readonly="false"
                    :properties-errors="propertiesErrors"
                    property-name="BookingFormOfSMTA1SignatureText"
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
            @executeSave="setNewBookingFormOfSMTA1Signature"
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
            v-if="submitBookingFormOfSMTA1FormVisible"
            text="Submit"
            style="display: inline-block; float: right"
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

    <v-card v-if="WaitForBookingFormSMTA1OPSApproval" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <div
          v-if="BookingFormElectronicallyFill == false"
          class="shipment-action-vcard"
        >
          <p class="text-h6">Download</p>
          <p>Please download the signed Booking Form of SMTA 1</p>
        </div>
        <DownloadDocumentComponent
          v-if="BookingFormElectronicallyFill == false"
          title="Download"
          :document-id="documentId"
          :document-name="documentName"
          :worklist-id="worklistId"
          @downloadFile="downloadFile"
        >
        </DownloadDocumentComponent>
        <div
          v-if="BookingFormElectronicallyFill == false"
          class="shipment-action-vcard"
        >
          <p class="text-h6">Fill the form</p>
          <p>Read carefully the document</p>
          <p>Fill the electronic form accordingly</p>
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
          :pickup-institute-address="model.LaboratoryAddress"
          :pickup-institute-name="model.LaboratoryName"
          :pickup-institute-country="model.LaboratoryCountry"
          :delivery-institute-address="model.BioHubFacilityAddress"
          :delivery-institute-name="model.BioHubFacilityName"
          :delivery-institute-country="model.BioHubFacilityCountry"
          pickup-institute-title="The Laboratory"
          delivery-institute-title="BioHub Facility"
          :courier-visible="courierVisible"
          :delivery-users="BioHubFacilityFocalPoints"
          :all-possible-pickup-users="AllPossiblePickupUsers"
          :all-possible-courier-users="AllPossibleCourierUsers"
          :material-shipping-informations="MaterialShippingInformations"
          :couriers="Couriers"
          @updateBookingForm="updateBookingForm"
          @addBookingFormCourierUser="addBookingFormCourierUser"
          @removeBookingFormCourierUser="removeBookingFormCourierUser"
          @clearBookingFormCourierUser="clearBookingFormCourierUser"
          @addBookingFormPickupUser="addBookingFormPickupUser"
          @removeBookingFormPickupUser="removeBookingFormPickupUser"
        >
        </BookingFormTabsComponent>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-container class="px-0" fluid>
            <v-row>
              <v-col cols="12" sm="6" md="6">
                <!-- # 54317 -->
                <!-- <v-img
                  v-if="model.BookingFormOfSMTA1SignatureId"
                  :src="ImageSrcBookingFormOfSMTA1"
                />
                <p
                  v-if="
                    !model.BookingFormOfSMTA1SignatureId &&
                    ElectronicallyFill == true
                  "
                  style="color: red"
                >
                  MISSING SIGNATURE !!!
                </p> -->
                <text-field
                  v-if="BookingFormElectronicallyFill == true"
                  v-model="model.BookingFormOfSMTA1SignatureText"
                  label="Signature"
                  :readonly="true"
                  :properties-errors="propertiesErrors"
                  property-name="BookingFormOfSMTA1SignatureText"
                  @input="update"
                >
                </text-field>
                <p
                  v-if="
                    (model.BookingFormOfSMTA1SignatureText == null ||
                      model.BookingFormOfSMTA1SignatureText == undefined ||
                      model.BookingFormOfSMTA1SignatureText == '') &&
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
            v-if="submitWaitForBookingFormOfSMTA1OPSsApprovalVisible"
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
import FormPopup from "../../../../components/FormPopup.vue";
import { FormPopupItem } from "@/models/FormPopupItem";
import { createFormPopupItem } from "../../../../utils/helper";
import { InputType } from "@/models/enums/InputType";
import DownloadDocumentComponent from "./../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import { AttachmentType } from "@/models/enums/AttachmentType";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import Checkbox from "@/components/Checkbox.vue";
import BookingFormTabsComponent from "../../../worklistItemsCommonComponents/BookingFormComponents/BookingFormTabsComponent.vue";
import DatePicker from "@/components/DatePicker.vue";
import { UserModule } from "../../../users/store";
import { CourierModule } from "../../../couriers/store";
import { CountryModule } from "../../../countries/store";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { MaterialProductModule } from "../../../materialProducts/store";
import { TransportCategoryModule } from "../../../transportCategories/store";
import { PermissionType } from "@/models/enums/PermissionType";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
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

  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get BookingForms(): Array<BookingFormOfSMTA> {
    return this.model.BookingForms;
  }

  get Couriers(): Array<Courier> {
    return CourierModule.Couriers;
  }

  get AllPossiblePickupUsers(): Array<WorklistItemUser> {
    return UserModule.WorklistToBioHubItemAllLaboratoryUsers;
  }

  get AllPossibleCourierUsers(): Array<WorklistItemUser> {
    return UserModule.WorklistToBioHubItemAllCourierUsers;
  }

  get MaterialShippingInformations(): Array<MaterialShippingInformation> {
    return WorklistToBioHubItemModule.MaterialShippingInformations;
  }

  get RequestUserFirstName(): string {
    if (this.WaitForBookingFormSMTA1OPSApproval == true) {
      return this.model.LastOperationUserFirstName;
    }
    return AuthModule.FirstName ?? "";
  }

  get RequestUserLastName(): string {
    if (this.WaitForBookingFormSMTA1OPSApproval == true) {
      return this.model.LastOperationUserLastName;
    }
    return AuthModule.LastName ?? "";
  }

  get RequestUserEmail(): string {
    if (this.WaitForBookingFormSMTA1OPSApproval == true) {
      return this.model.LastOperationUserEmail;
    }
    return AuthModule.Email ?? "";
  }

  get RequestUserJobTitle(): string {
    if (this.WaitForBookingFormSMTA1OPSApproval == true) {
      return this.model.LastOperationUserJobTitle;
    }
    return AuthModule.JobTitle ?? "";
  }

  get RequestUserBusinessPhone(): string {
    if (this.WaitForBookingFormSMTA1OPSApproval == true) {
      return this.model.LastOperationUserBusinessPhone;
    }
    return AuthModule.BusinessPhone ?? "";
  }

  get RequestUserMobilePhone(): string {
    if (this.WaitForBookingFormSMTA1OPSApproval == true) {
      return this.model.LastOperationUserMobilePhone;
    }
    return AuthModule.MobilePhone ?? "";
  }

  get courierVisible(): boolean {
    return (
      WorklistToBioHubItemModule.Status ==
      WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval
    );
  }

  get BioHubFacilityFocalPoints(): Array<WorklistItemUser> {
    return WorklistToBioHubItemModule.WorklistToBioHubItemBioHubFacilityFocalPoints;
  }

  get ImageSrcBookingFormOfSMTA1(): string {
    const src =
      "data:image/jpeg;base64," + this.model.BookingFormOfSMTA1SignatureString;
    return src;
  }

  get BookingFormSignatureWarningVisible(): boolean {
    return (
      (this.model.BookingFormOfSMTA1SignatureId === undefined ||
        this.model.BookingFormOfSMTA1SignatureId === null ||
        this.model.BookingFormOfSMTA1SignatureId === "") &&
      (this.base64SignaturePreview === "" ||
        this.base64SignaturePreview === null ||
        this.base64SignaturePreview === undefined)
    );
  }

  get BookingFormSignatureButtonTitle(): string {
    if (
      this.model.BookingFormOfSMTA1SignatureId !== undefined &&
      this.model.BookingFormOfSMTA1SignatureId !== null &&
      this.model.BookingFormOfSMTA1SignatureId !== ""
    ) {
      return "Change the image of the signature";
    }
    return "Upload the image of the signature";
  }

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get documentId(): string {
    return this.model.BookingFormOfSMTA1DocumentId;
  }

  get worklistId(): string {
    return this.model.Id;
  }

  get documentName(): string {
    return this.model.BookingFormOfSMTA1DocumentName;
  }

  get BookingFormElectronicallyFill(): boolean {
    return (
      this.model.BookingFormFillingOption ==
      WorklistFillingOption.ElectronicallyFill
    );
  }

  get SubmitBookingFormOfSMTA1(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.SubmitBookingFormOfSMTA1
    );
  }

  get WaitForBookingFormSMTA1OPSApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval
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

  get approvedSelected(): boolean {
    return this.model.LastSubmissionApproved;
  }

  get submitBookingFormOfSMTA1FormVisible(): boolean {
    //# 54317
    // return (
    //   (this.signatureUploaded == true ||
    //     (this.model.BookingFormOfSMTA1SignatureId !== undefined &&
    //       this.model.BookingFormOfSMTA1SignatureId !== "" &&
    //       this.model.BookingFormOfSMTA1SignatureId !== null)) &&
    //   this.BookingFormOfSMTA1FormCompleted == true
    // );
    return (
      this.model.BookingFormOfSMTA1SignatureText !== undefined &&
      this.model.BookingFormOfSMTA1SignatureText !== "" &&
      this.model.BookingFormOfSMTA1SignatureText !== null &&
      this.BookingFormOfSMTA1FormCompleted == true &&
      (this.model.IsPast != true || this.model.AssignedOperationDate != null)
    );
    ///////////////////////////////
  }

  get submitWaitForBookingFormOfSMTA1OPSsApprovalVisible(): boolean {
    //# 54317
    // if (
    //   this.approvedSelected == true &&
    //   this.BookingFormElectronicallyFill == true &&
    //   (this.model.BookingFormOfSMTA1SignatureId === undefined ||
    //     this.model.BookingFormOfSMTA1SignatureId === "" ||
    //     this.model.BookingFormOfSMTA1SignatureId === null)
    // ) {
    //   return false;
    // }
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (
      this.approvedSelected == true &&
      this.BookingFormElectronicallyFill == true &&
      (this.model.BookingFormOfSMTA1SignatureText === undefined ||
        this.model.BookingFormOfSMTA1SignatureText === "" ||
        this.model.BookingFormOfSMTA1SignatureText === null)
    ) {
      return false;
    }
    ////////////////////////

    if (this.approvedSelected == false && this.model.Comment != "") {
      return true;
    }

    if (
      this.approvedSelected == true &&
      this.BookingFormOfSMTA1FormCompleted == true &&
      this.model.BookingFormApprovalFlag == true
    ) {
      return true;
    }
    return false;
  }

  get BookingFormOfSMTA1FormCompleted(): boolean {
    if (
      this.model.BookingForms === undefined ||
      this.model.BookingForms.length == 0
    ) {
      return false;
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
        this.WaitForBookingFormSMTA1OPSApproval &&
        (elem.BookingFormCourierUsers === undefined ||
          elem.BookingFormCourierUsers.length == 0 ||
          elem.EstimateDateOfPickup == null)
      ) {
        bookingFormsCompleted = false;
      }
    });

    return bookingFormsCompleted;
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

  updateBookingForm(item: BookingFormOfSMTA) {
    WorklistToBioHubItemModule.UPDATE_BOOKING_FORM(item);
  }

  addBookingFormCourierUser(user: WorklistItemUser, bookingFormId: string) {
    const info = { BookingFormId: bookingFormId, Item: user };

    WorklistToBioHubItemModule.ADD_BOOKINGFORM_COURIER_USER(info);
  }

  removeBookingFormCourierUser(id: string, bookingFormId: string) {
    const info = new Map<string, string>();
    info.set("BookingFormId", bookingFormId);
    info.set("Id", id);

    WorklistToBioHubItemModule.REMOVE_BOOKINGFORM_COURIER_USER(info);
  }

  addBookingFormPickupUser(user: WorklistItemUser, bookingFormId: string) {
    const info = { BookingFormId: bookingFormId, Item: user };

    WorklistToBioHubItemModule.ADD_BOOKINGFORM_PICKUP_USER(info);
  }

  removeBookingFormPickupUser(id: string, bookingFormId: string) {
    const info = new Map<string, string>();
    info.set("BookingFormId", bookingFormId);
    info.set("Id", id);
    WorklistToBioHubItemModule.REMOVE_BOOKINGFORM_PICKUP_USER(info);
  }

  clearBookingFormCourierUser(bookingFormId: string) {
    WorklistToBioHubItemModule.CLEAR_BOOKINGFORM_COURIER_USER(bookingFormId);
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

  async setNewBookingFormOfSMTA1Document(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_DOCUMENT(fileSelected);
    this.setDocumentFileType(DocumentFileType.BookingFormOfSMTA1);

    this.documentFileName = fileSelected.name;
  }

  async setNewBookingFormOfSMTA1Signature(file: Array<FormPopupItem>) {
    const fileSelected = file.find((f) => f.PropertyName == "File")
      ?.Value as File;

    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_SIGNATURE(fileSelected);
    this.setDocumentFileType(DocumentFileType.BookingFormOfSMTA1);

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
    if (this.CurrentStatus == WorklistToBioHubStatus.SubmitBookingFormOfSMTA1) {
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
    const laboratoryId = WorklistToBioHubItemModule.LaboratoryId;
    const bioHubFacilityId = WorklistToBioHubItemModule.BioHubFacilityId;
    const worklistToBioHubItemId = this.$route.params.id;

    const info = new Map<string, string>();
    info.set("LaboratoryId", laboratoryId);
    info.set("WorklistToBioHubItemId", worklistToBioHubItemId);
    info.set("BioHubFacilityId", bioHubFacilityId);

    await UserModule.ListUsersByLaboratoryIdForWorklistToBioHubItem(info);
    await UserModule.ListUsersByBioHubFacilityIdForWorklistToBioHubItem(info);
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();

    if (
      this.WaitForBookingFormSMTA1OPSApproval &&
      hasPermission(PermissionNames.CanReadCourier)
    ) {
      await UserModule.ListCourierUsersForWorklistToBioHubItem(info);
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
      WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Signature);
    } else {
      WorklistToBioHubItemModule.SET_ATTACHMENT_TYPE(AttachmentType.Document);
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
