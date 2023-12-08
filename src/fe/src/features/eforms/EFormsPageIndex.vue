<template>
  <div v-if="canRead">
    <EFormsTable
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canDelete"
      :eForms="eForms"
      :breadcrumbs-items="breadcrumbsItems"
      :loading="loading"
      @selected="selected"
      @createFolder="createFolder"
      @addTemplate="addFile"
      @edit="editEForm"
      @delete="deleteItem"
    >
    </EFormsTable>

    <DialogComponent ref="dialog" :dialog-text="dialogText"> </DialogComponent>
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      :message="confirmationDialogMessage"
      @onConfirm="confirmUpdateEForm"
    ></ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import EFormsTable from "./components/EFormsTable.vue";
import { EFormModule } from "./store";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { AppModule } from "../../store/MainStore";
import { BreadcrumbsItem } from "@/models/shared/BreadcrumbsItem";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { createFormPopupItem } from "../../utils/helper";
import FormPopup from "../../components/FormPopup.vue";
import { EForm } from "@/models/EForm";
import { EFormType } from "@/models/enums/EFormType";
import { EFormItemType } from "@/models/enums/EFormItemType";
import { DropdownItem } from "@/models/DropdownItem";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import DialogComponent from "../../components/DialogComponent.vue";
import ConfirmationDialogComponent from "../../components/ConfirmationDialogComponent.vue";

@Component({
  components: {
    EFormsTable,
    CardActionsGenericButton,
    FormPopup,
    DialogComponent,
    ConfirmationDialogComponent,
  },
})
export default class EFormsPageIndex extends Vue {
  $refs!: {
    newFolderPopup: FormPopup;
    newFilePopup: FormPopup;
    editTemplatePopup: FormPopup;
    dialog: DialogComponent;
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  breadcrumbsItems: Array<BreadcrumbsItem> = [];

  newFolderPopupItems: Array<FormPopupItem> = [];
  newFilePopupItems: Array<FormPopupItem> = [];
  editTemplatePopupItems: Array<FormPopupItem> = [];
  editTemplatePopopTitle = "";

  dialogText = "";

  confirmationDialogMessage = "";

  private openSaveOrDelete = false;
  private download = false;

  get loading(): boolean {
    return !this.download && AppModule.IsLoadingActive;
  }

  get templateFileTypeItems(): Array<DropdownItem> {
    return [
      {
        Value: EFormType.Annex2OfSMTA1,
        Text: "Annex 2 of SMTA 1",
      },
      {
        Value: EFormType.Annex2OfSMTA2,
        Text: "Annex 2 of SMTA 2",
      },
      {
        Value: EFormType.BookingFormOfSMTA1,
        Text: "Booking Form of SMTA 1",
      },
      {
        Value: EFormType.BookingFormOfSMTA2,
        Text: "Booking Form of SMTA 2",
      },
      {
        Value: EFormType.BiosafetyChecklistOfSMTA2,
        Text: "Biosafety Checklist",
      },
    ] as DropdownItem[];
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadEForm);
  }

  async loadPageInfo() {
    await EFormModule.ListEForms();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    if (!this.openSaveOrDelete) AppModule.HideLoading();
  }

  get error(): any {
    return EFormModule.ErrorMessage;
  }

  get eForms(): Array<EForm> {
    return EFormModule.CurrentFolderEForms;
  }

  async selected(item: EForm): Promise<void> {
    if (item.Type == EFormItemType.Folder) {
      this.showLoading();
      if (item.Id) {
        if (this.breadcrumbsItems.length == 0) {
          this.breadcrumbsItems.push({
            text: "...",
            to: null,
          } as unknown as BreadcrumbsItem);
        }

        const index = this.breadcrumbsItems.findIndex((bi) => bi.to == item.Id);
        if (index == -1) {
          this.breadcrumbsItems.push({
            text: item.Name,
            to: item.Id,
          } as BreadcrumbsItem);
        } else {
          this.breadcrumbsItems = this.breadcrumbsItems.slice(0, index + 1);
        }
        this.breadcrumbsItems.forEach((bi) => (bi.disabled = false));
        this.breadcrumbsItems[this.breadcrumbsItems.length - 1].disabled = true;
        EFormModule.SET_CURRENT_FOLDER_EFORMS(item.Id);
      } else {
        this.breadcrumbsItems = [];
        await EFormModule.ListEForms();
      }
    } else {
      this.download = true;
      this.showLoading();
      EFormModule.SET_EFORM(item);
      window.open(item.Url, "_blank");
      //await EFormModule.ReadEForm();
    }
    this.hideLoading();
  }

  private showLoading() {
    this.openSaveOrDelete = true;
    AppModule.ShowLoading();
  }

  private hideLoading() {
    this.openSaveOrDelete = false;
    this.download = false;
    AppModule.HideLoading();
  }
}
</script>
