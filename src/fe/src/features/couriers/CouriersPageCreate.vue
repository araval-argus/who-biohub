<template>
  <v-container v-if="canCreate" fluid>
    <CourierForm ref="courierForm" v-model="courier" title="Create Courier">
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </CourierForm>
  </v-container>
</template>

<script lang="ts">
import CourierForm from "./components/CourierForm.vue";
import CardActionsSaveCancel from "@/components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { CourierModule } from "./store";
import { Courier } from "@/models/Courier";
import { BSLLevelModule } from "../bsllevels/store";
import { CountryModule } from "../countries/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { CourierForm, CardActionsSaveCancel } })
export default class CouriersPageEdit extends Vue {
  $refs!: {
    courierForm: CourierForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadCourier);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateCourier);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditCourier);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteCourier);
  }

  get courier(): Courier {
    return CourierModule.CourierCreate;
  }

  set courier(courier: Courier) {
    CourierModule.SET_COURIER_CREATE(courier);
  }

  async onSave(): Promise<void> {
    this.$refs.courierForm.validate();
    await CourierModule.CreateCourier()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    CourierModule.SET_ERROR(undefined);
    this.$router.back();
  }

  async loadPageInfo() {
    await BSLLevelModule.ListBSLLevels();
    await CountryModule.ListCountries();
    CourierModule.CLEAR_COURIER_CREATE();
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
