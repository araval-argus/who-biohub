<template>
  <div>
    <CourierBookingFormInfo
      v-if="canRead"
      v-model="courier"
      title="Booking Form Details"
      readonly
    >
    </CourierBookingFormInfo>
  </div>
</template>

<script lang="ts">
import CourierBookingFormInfo from "./components/CourierBookingFormInfo.vue";
import { Component, Vue } from "vue-property-decorator";
import { Courier } from "@/models/Courier";
import { CourierModule } from "./store";
import { CountryModule } from "../countries/store";
import { AppModule } from "../../store/MainStore";
import { AuthModule } from "../auth/store";
import { RoleType } from "@/models/enums/RoleType";
import { Role } from "@/models/Role";
import { User } from "@/models/User";
import { RoleModule } from "../roles/store";
import { users } from "../users/mock";
import { UserModule } from "../users/store";
import UsersTable from "../users/components/UsersTable.vue";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import CourierBookingFormsTable from "./components/CourierBookingFormsTable.vue";
import { TransportCategoryModule } from "../transportCategories/store";
import { MaterialProductModule } from "../materialProducts/store";

@Component({
  components: { CourierBookingFormInfo },
})
export default class CouriersBookingFormPageDetails extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadCourier);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateCourier);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditCourier);
  }

  async loadPageInfo() {
    await TransportCategoryModule.ListTransportCategories();
    await MaterialProductModule.ListMaterialProducts();
    await CourierModule.ReadCourierBookingForm(this.$route.params.id);
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
