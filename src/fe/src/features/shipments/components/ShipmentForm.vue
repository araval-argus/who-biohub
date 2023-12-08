<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton v-if="backButtonVisible" @back="onBack"></BackButton>
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation class="ma-2">
        <div>
          <v-row class="mb-5">
            <v-col cols="12" md="8" lg="4">
              <text-field
                v-model="model.ReferenceNumber"
                label="Reference Number"
                :readonly="!canEdit"
                :prepend-icon="prependIcon"
                :properties-errors="propertiesErrors"
                property-name="ReferenceNumber"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field v-model="model.From" label="From" readonly>
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field v-model="model.To" label="To" readonly> </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field
                v-model="model.StatusOfRequest"
                label="Status Of Request"
                readonly
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="4">
              <date-picker
                v-model="model.CompletedOn"
                label="Completed On"
                readonly
              >
              </date-picker>
            </v-col>
          </v-row>
        </div>
        <CardActionsSaveCancel
          class="mb-10"
          v-if="canEdit"
          @save="onSave"
          @cancel="onCancel"
        >
        </CardActionsSaveCancel>

        <PickupDeliveryCompletedTable
          :can-edit="false"
          :booking-forms="model.BookingForms"
          :is-pickup="true"
          :is-delivery="true"
        ></PickupDeliveryCompletedTable>
        <DocumentsTable
          :can-create="false"
          :can-edit="false"
          :can-delete="false"
          :documents="CurrentFolderDocuments"
          :breadcrumbs-items="documentBreadcrumbsItems"
          :loading="false"
          @selected="selectedDocument"
        >
        </DocumentsTable>
        <EFormsTable
          :can-create="false"
          :can-edit="false"
          :can-delete="false"
          :eForms="CurrentFolderEForms"
          :breadcrumbs-items="eFormBreadcrumbsItems"
          :loading="false"
          @selected="selectedEForm"
        >
        </EFormsTable>
        <ShipmentMaterialsTable
          :bio-hub-facility-id="model.BioHubFacilityId"
        ></ShipmentMaterialsTable>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import BackButton from "@/components/BackButton.vue";
import { Shipment } from "@/models/Shipment";
import ShipmentMaterialsTable from "./ShipmentMaterialsTable.vue";
import CardActionsSaveCancel from "../../../components/CardActionsSaveCancel.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import { ShipmentModule } from "../store";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { EFormItemType } from "@/models/enums/EFormItemType";
import { Document } from "@/models/Document";
import { DocumentType } from "@/models/enums/DocumentType";
import { DocumentModule } from "../../documents/store";
import DocumentsTable from "../../documents/components/DocumentsTable.vue";
import { EForm } from "@/models/EForm";
import EFormsTable from "../../eforms/components/EFormsTable.vue";
import PickupDeliveryCompletedTable from "../../worklistItemsCommonComponents/PickupDeliveryComponents/PickupDeliveryCompletedTable.vue";

@Component({
  components: {
    BackButton,
    DatePicker,
    TextField,
    ShipmentMaterialsTable,
    DocumentsTable,
    EFormsTable,
    CardActionsSaveCancel,
    PickupDeliveryCompletedTable,
  },
})
export default class ShipmentForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private folderId = null;

  documentBreadcrumbsItems: Array<BreadcrumbsItem> = [];
  eFormBreadcrumbsItems: Array<BreadcrumbsItem> = [];

  // Props
  @Prop({ type: Boolean, default: true })
  readonly backButtonVisible: boolean;

  @Prop({ required: true, type: String, default: "Shipment" })
  readonly title: string;

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  $refs!: {
    form: any;
  };

  @Model("update", { type: Object }) model!: Shipment;

  get Documents(): Array<Document> {
    return ShipmentModule.Documents;
  }

  get CurrentFolderDocuments(): Array<Document> {
    return ShipmentModule.CurrentFolderDocuments;
  }

  get EForms(): Array<EForm> {
    return ShipmentModule.EForms;
  }

  get CurrentFolderEForms(): Array<EForm> {
    return ShipmentModule.CurrentFolderEForms;
  }

  get propertiesErrors(): any {
    return ShipmentModule.ErrorMessage;
  }

  get prependIcon(): string {
    if (this.canEdit == true) {
      return "mdi-pencil-circle-outline";
    }
    return "";
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  async onSave(): Promise<void> {
    await ShipmentModule.UpdateShipment()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    ShipmentModule.SET_ERROR(undefined);
    this.$router.back();
  }

  validate() {
    this.$refs.form.validate();
  }

  sortByName(a: any, b: any) {
    if (a.Name > b.Name) {
      return 1;
    }
    if (a.Name < b.Name) {
      return -1;
    }
    return 0;
  }

  async selectedDocument(item: Document): Promise<void> {
    if (item.Type == DocumentType.Folder) {
      if (item.Id) {
        if (this.documentBreadcrumbsItems.length == 0) {
          this.documentBreadcrumbsItems.push({
            text: "...",
            to: null,
          } as unknown as BreadcrumbsItem);
        }

        const index = this.documentBreadcrumbsItems.findIndex(
          (bi) => bi.to == item.Id
        );
        if (index == -1) {
          this.documentBreadcrumbsItems.push({
            text: item.Name,
            to: item.Id,
          } as BreadcrumbsItem);
        } else {
          this.documentBreadcrumbsItems = this.documentBreadcrumbsItems.slice(
            0,
            index + 1
          );
        }
        this.documentBreadcrumbsItems.forEach((bi) => (bi.disabled = false));
        this.documentBreadcrumbsItems[
          this.documentBreadcrumbsItems.length - 1
        ].disabled = true;
        ShipmentModule.SET_CURRENT_FOLDER_DOCUMENTS(item.Id);
      } else {
        this.documentBreadcrumbsItems = [];
        ShipmentModule.SET_CURRENT_FOLDER_DOCUMENTS(null);
      }
    } else {
      DocumentModule.SET_DOCUMENT(item);
      await DocumentModule.ReadDocument();
    }
  }

  selectedEForm(item: EForm): void {
    if (item.Type == EFormItemType.Folder) {
      if (item.Id) {
        if (this.eFormBreadcrumbsItems.length == 0) {
          this.eFormBreadcrumbsItems.push({
            text: "...",
            to: null,
          } as unknown as BreadcrumbsItem);
        }

        const index = this.eFormBreadcrumbsItems.findIndex(
          (bi) => bi.to == item.Id
        );
        if (index == -1) {
          this.eFormBreadcrumbsItems.push({
            text: item.Name,
            to: item.Id,
          } as BreadcrumbsItem);
        } else {
          this.eFormBreadcrumbsItems = this.eFormBreadcrumbsItems.slice(
            0,
            index + 1
          );
        }
        this.eFormBreadcrumbsItems.forEach((bi) => (bi.disabled = false));
        this.eFormBreadcrumbsItems[
          this.eFormBreadcrumbsItems.length - 1
        ].disabled = true;
        ShipmentModule.SET_CURRENT_FOLDER_EFORMS(item.Id);
      } else {
        this.eFormBreadcrumbsItems = [];
        ShipmentModule.SET_CURRENT_FOLDER_EFORMS(null);
      }
    } else {
      window.open(item.Url, "_blank");
    }
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
