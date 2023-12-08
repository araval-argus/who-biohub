<template>
  <CourierForm
    v-if="isCourierSet && canEdit"
    ref="courierForm"
    v-model="courier"
    title="Courier Edit"
  >
    <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
    </CardActionsSaveCancel>
  </CourierForm>
  <div v-else><span>No Courier selected</span></div>
</template>

<script lang="ts">
import CourierForm from "./components/CourierForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
import { CourierModule } from "./store";
import { Courier } from "@/models/Courier";
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

  get isCourierSet(): boolean {
    return CourierModule.Courier !== undefined;
  }

  get courier(): Courier {
    const lab = CourierModule.Courier;
    if (lab) return lab;

    throw { message: "no courier selected" };
  }

  set courier(lab: Courier) {
    CourierModule.SET_COURIER(lab);
  }

  async onSave(): Promise<void> {
    this.$refs.courierForm.validate();
    await CourierModule.UpdateCourier()
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
    await CountryModule.ListCountries();
    await CourierModule.ReadCourier(this.$route.params.id);
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
