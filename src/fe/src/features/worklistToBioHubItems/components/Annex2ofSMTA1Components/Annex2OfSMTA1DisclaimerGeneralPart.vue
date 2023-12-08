<template>
  <div class="annex2-smta1-general">
    <h2>Voluntary Transfer Form for BMEPP</h2>
    <div>
      <strong
        >This Voluntary Transfer Form will be revised by WHO, following a broad
        consultation process with Member States and other stakeholders.</strong
      >
    </div>
    <div>
      This Voluntary Transfer Form must be filled in and submitted to WHO. It
      should be included with all the information that accompany any requests
      for shipments of BMEPP <strong><i>into</i></strong> a WHO BioHub Facility.
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
              worklistToBioHubItem.BioHubFacilityId == '' ||
              worklistToBioHubItem.BioHubFacilityId == null ||
              worklistToBioHubItem.BioHubFacilityId == undefined
            "
            style="color: red"
          >
            Please select a BioHub Facility
          </h4>
          <dropdown
            v-model="worklistToBioHubItem.BioHubFacilityId"
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
          BMEPP are part of a WHO system known as the ‘WHO BioHub System’ which
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
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { CountryModule } from "../../../countries/store";
import { WorklistToBioHubItemModule } from "../../store";
import {
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import { WorklistToBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistToBioHubSubmitPermissionsByStatus";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import Dropdown from "@/components/Dropdown.vue";
import { PermissionType } from "@/models/enums/PermissionType";

@Component({
  components: { Dropdown },
})
export default class Annex2OfSMTA1DisclaimerGeneralPart extends Vue {
  @Prop({ type: String, default: "" })
  readonly bioHubFacilityName: string;

  @Prop({ type: String, default: "" })
  readonly bioHubFacilityCountry: string;

  @Prop({ type: String, default: "" })
  readonly bioHubFacilityAddress: string;

  $refs!: {
    form: any;
  };

  get worklistToBioHubItem(): WorklistToBioHubItem | undefined {
    return WorklistToBioHubItemModule.WorklistToBioHubItem;
  }

  set worklistToBioHubItem(
    worklistToBioHubItem: WorklistToBioHubItem | undefined
  ) {
    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM(worklistToBioHubItem);
  }

  get SubmitAnnex2OfSMTA1(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.SubmitAnnex2OfSMTA1
    );
  }

  get WaitingForAnnex2OfSMTA1SECsApproval(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    return (
      this.worklistToBioHubItem.CurrentStatus ==
      WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval
    );
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.worklistToBioHubItem.CurrentStatus,
      PermissionType.Update,
      this.worklistToBioHubItem.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get canEdit(): boolean {
    if (this.worklistToBioHubItem === undefined) {
      return false;
    }
    if (this.hasSubmitPermissionByStatus == false) {
      return false;
    }
    if (
      this.WaitingForAnnex2OfSMTA1SECsApproval == true &&
      this.worklistToBioHubItem.Annex2FillingOption ==
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
