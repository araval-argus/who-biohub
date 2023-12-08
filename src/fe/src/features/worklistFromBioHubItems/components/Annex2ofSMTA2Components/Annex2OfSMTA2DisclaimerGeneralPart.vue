<template>
  <div class="annex2-smta2">
    <h2>Annex 2 of SMTA 2: Request Form for BMEPP</h2>
    <div>
      <strong
        >This Request Form will be revised by WHO, following a broad
        consultation process with Member States and other stakeholders.</strong
      >
    </div>
    <div>
      This Request Form must be filled in and submitted to WHO. It should be
      included with the documents that accompany any requests for shipments of
      BMEPP <strong><i>from</i></strong> a WHO BioHub Facility to a Qualified
      Entity for non-commercial purposes. Shipments out of a WHO BioHub Facility
      will be made following a request received from a Qualified Entity that
      meets all required biosafety and biosecurity requirements for the BMEPP
      requested.
    </div>
    <br />
    <h2>I. General</h2>
    <div>
      <ol>
        <li>
          This shipment is made in furtherance of the objectives of the WHO
          BioHub System from the WHO BioHub Facility located at:
          <br />
          <h4
            v-if="
              worklistFromBioHubItem.BioHubFacilityId == '' ||
              worklistFromBioHubItem.BioHubFacilityId == null ||
              worklistFromBioHubItem.BioHubFacilityId == undefined
            "
            style="color: red"
          >
            Please select a BioHub Facility
          </h4>
          <dropdown
            v-model="worklistFromBioHubItem.BioHubFacilityId"
            :items="bioHubFacilities"
            item-text="Text"
            item-value="Value"
            label=""
            :readonly="!canEdit"
            property-name="BioHubFacilityId"
            @change="update"
          ></dropdown>
        </li>
        <li>
          BMEPP are part of a WHO system known as the "WHO BioHub System" which
          aims to protect and strengthen global public health security by
          providing one or more impartial, reliable, safe and secure Facilities
          for biological materials with epidemic or pandemic potential (BMEPP)
          contributed by WHO Member States. The WHO BioHub System should
          facilitate an effective, efficient, fair and equitable response to
          outbreaks through the rapid development of diagnostics, therapeutics
          and vaccines, as appropriate.
        </li>
        <li>
          The BMEPP in this shipment are provided on the condition that all
          terms and conditions applicable to BMEPP through the WHO BioHub System
          will apply.
        </li>
        <li>
          This Annex contains three sections which should be completed as
          necessary by the concerned entity(ies).
        </li>
      </ol>
    </div>
    <br />
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { BioHubFacilityModule } from "../../../biohubfacilities/store";
import { DropdownItem } from "@/models/DropdownItem";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { CountryModule } from "../../../countries/store";
import { WorklistFromBioHubItemModule } from "../../store";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import Dropdown from "@/components/Dropdown.vue";
import { DocumentModule } from "../../../documents/store";
import { PermissionType } from "@/models/enums/PermissionType";

@Component({
  components: { Dropdown },
})
export default class Annex2OfSMTA2DisclaimerGeneralPart extends Vue {
  @Prop({ type: String, default: "" })
  readonly bioHubFacilityName: string;

  @Prop({ type: String, default: "" })
  readonly bioHubFacilityCountry: string;

  @Prop({ type: String, default: "" })
  readonly bioHubFacilityAddress: string;

  $refs!: {
    form: any;
  };

  get worklistFromBioHubItem(): WorklistFromBioHubItem | undefined {
    return WorklistFromBioHubItemModule.WorklistFromBioHubItem;
  }

  set worklistFromBioHubItem(
    worklistFromBioHubItem: WorklistFromBioHubItem | undefined
  ) {
    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM(
      worklistFromBioHubItem
    );
  }

  get SMTA2DocumentSigned(): boolean {
    return DocumentModule.SMTA2DocumentSigned;
  }

  get SubmitAnnex2OfSMTA2(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2
    );
  }

  get WaitingForAnnex2OfSMTA2SECsApproval(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistFromBioHubItem.CurrentStatus ==
      WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval
    );
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.worklistFromBioHubItem.CurrentStatus,
      PermissionType.Update,
      this.worklistFromBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get canEdit(): boolean {
    if (this.worklistFromBioHubItem === undefined) {
      return false;
    }
    if (this.hasSubmitPermissionByStatus == false) {
      return false;
    }
    if (
      this.WaitingForAnnex2OfSMTA2SECsApproval == true &&
      this.worklistFromBioHubItem.Annex2FillingOption ==
        WorklistFillingOption.ElectronicallyFill
    ) {
      return false;
    }
    return true;
  }

  get bioHubFacilities(): Array<DropdownItem> {
    return BioHubFacilityModule.BioHubFacilities.map((b) => {
      var countryInfo = CountryModule.Countries.filter((c) => {
        return c.Id == b.CountryId;
      }).map((c) => {
        return {
          Country: c.Name,
        };
      });

      if (countryInfo.length == 0) {
        countryInfo.push({
          Country: "",
        });
      }

      return {
        Value: b.Id,
        Text: b.Name + " " + b.Address + " " + countryInfo[0].Country,
      };
    });
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
