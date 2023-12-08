<template>
  <v-container v-if="canRead">
    <v-row>
      <v-col>
        <LaboratoryForm
          v-if="laboratory"
          v-model="laboratory"
          laboratory-area="true"
          :back-button-visible="false"
          title="Facility/Institute Profile"
          readonly
          :show-bsl-level="false"
        >
          <CardActionsEdit @edit="onEdit"> </CardActionsEdit>
        </LaboratoryForm>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LaboratoryForm from "./components/LaboratoryForm.vue";
import { Laboratory } from "@/models/Laboratory";
import { LaboratoryModule } from "./store";
import CardActionsEdit from "../../components/CardActionsEdit.vue";
import { getUserBioHubFacilityOrLaboratoryOrCourierId } from "@/utils/helper";
import { AuthModule } from "../auth/store";
import { RoleType } from "../../models/enums/RoleType";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({
  components: {
    LaboratoryForm,
    CardActionsEdit,
  },
})
export default class LaboratoryInfoPage extends Vue {
  private search = "";

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadLaboratory);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateLaboratory);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditLaboratory);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratory);
  }

  get laboratory(): Laboratory | undefined {
    return LaboratoryModule.Laboratory;
  }

  get LaboratoryId(): string {
    return AuthModule.LaboratoryId ?? "";
  }

  async onEdit(): Promise<void> {
    this.$router.push({
      name: "laboratoryarea-private-page-edit",
      params: { id: this.LaboratoryId },
    });
  }

  async loadPageInfo() {
    await LaboratoryModule.ReadLaboratory(this.LaboratoryId);
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
