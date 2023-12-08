import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";

import { isLeft, isRight, ParseError } from "@/utils/either";
import { eForms } from "./mock";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
//import { EFormType } from "@/models/enums/EFormType";
import { EFormsService } from "@/services/eforms/EFormsService";
import { Annex2OfSMTA1Data } from "@/models/Annex2OfSMTA1Data";
import { BookingFormOfSMTA1Data } from "@/models/BookingFormOfSMTA1Data";
import { ReadAnnex2OfSMTA1Response } from "@/services/eforms/models/ReadAnnex2OfSMTA1";
import { ReadBookingFormOfSMTA1Response } from "@/services/eforms/models/ReadBookingFormOfSMTA1";
import { Annex2OfSMTA2Data } from "@/models/Annex2OfSMTA2Data";
import { ReadAnnex2OfSMTA2Response } from "@/services/eforms/models/ReadAnnex2OfSMTA2";
import { BiosafetyChecklistOfSMTA2Data } from "@/models/BiosafetyChecklistOfSMTA2Data";
import { ReadBiosafetyChecklistOfSMTA2Response } from "@/services/eforms/models/ReadBiosafetyChecklistOfSMTA2";
import { BookingFormOfSMTA2Data } from "@/models/BookingFormOfSMTA2Data";
import { ReadBookingFormOfSMTA2Response } from "@/services/eforms/models/ReadBookingFormOfSMTA2";
import { EForm } from "@/models/EForm";
import { ListEFormsResponse } from "@/services/eforms/models/ListEForms";
import { EFormItemType } from "@/models/enums/EFormItemType";

//import { ListEFormsResponse } from "@/services/eforms/models/ListEForms";
//import { EFormFileType } from "@/models/enums/EFormFileType";

export interface EFormState {
  Error: AppError | undefined;
  ErrorMessage: any;
  Annex2OfSMTA1: Annex2OfSMTA1Data | undefined;
  BookingFormOfSMTA1: BookingFormOfSMTA1Data | undefined;
  EFormBuffer: any;
  Annex2OfSMTA2: Annex2OfSMTA2Data | undefined;
  BiosafetyChecklistOfSMTA2: BiosafetyChecklistOfSMTA2Data | undefined;
  BookingFormOfSMTA2: BookingFormOfSMTA2Data | undefined;
  //EForms: Array<EForm>;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "eforms",
  store: store,
})
class EFormStore extends VuexModule implements EFormState {
  // Private variables
  private eForms: { value: Array<EForm> } = {
    value: eForms,
  };

  private eForm: { value: EForm | undefined } = {
    value: undefined,
  };

  private currentFolderEForms: { value: Array<EForm> } = {
    value: [],
  };

  private annex2OfSMTA1: { value: Annex2OfSMTA1Data | undefined } = {
    value: undefined,
  };

  private bookingFormOfSMTA1: { value: BookingFormOfSMTA1Data | undefined } = {
    value: undefined,
  };

  private annex2OfSMTA2: { value: Annex2OfSMTA2Data | undefined } = {
    value: undefined,
  };

  private biosafetyChecklistOfSMTA2: {
    value: BiosafetyChecklistOfSMTA2Data | undefined;
  } = {
    value: undefined,
  };

  private bookingFormOfSMTA2: { value: BookingFormOfSMTA2Data | undefined } = {
    value: undefined,
  };

  private eFormBuffer: { value: any } = {
    value: undefined,
  };

  // private eforms: { value: Array<EForm> } = {
  //   value: eforms,
  // };

  // private currentFolderEForms: { value: Array<EForm> } = {
  //   value: [],
  // };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  @Mutation
  public SET_EFORMS(eForms: Array<EForm>): void {
    this.eForms.value = eForms;
  }

  @Mutation
  public SET_EFORM(eForm: EForm | undefined): void {
    this.eForm.value = eForm;
  }

  @Mutation
  public SET_CURRENT_FOLDER_EFORMS(folderId: string | null): void {
    this.currentFolderEForms.value = this.eForms.value.filter((r) => {
      return r.ParentId == folderId;
    });
  }

  // Details - Edit
  @Mutation
  public SET_ANNEX2OFSMTA1(annex2OfSMTA1: Annex2OfSMTA1Data | undefined): void {
    this.annex2OfSMTA1.value = annex2OfSMTA1;
  }

  @Mutation
  public SET_BOOKINGFORMOFSMTA1(
    bookingFormOfSMTA1: BookingFormOfSMTA1Data | undefined
  ): void {
    this.bookingFormOfSMTA1.value = bookingFormOfSMTA1;
  }

  @Mutation
  public SET_ANNEX2OFSMTA2(annex2OfSMTA2: Annex2OfSMTA2Data | undefined): void {
    this.annex2OfSMTA2.value = annex2OfSMTA2;
  }

  @Mutation
  public SET_BIOSAFETYCHECKLISTOFSMTA2(
    biosafetyChecklistOfSMTA2: BiosafetyChecklistOfSMTA2Data | undefined
  ): void {
    this.biosafetyChecklistOfSMTA2.value = biosafetyChecklistOfSMTA2;
  }

  @Mutation
  public SET_BOOKINGFORMOFSMTA2(
    bookingFormOfSMTA2: BookingFormOfSMTA2Data | undefined
  ): void {
    this.bookingFormOfSMTA2.value = bookingFormOfSMTA2;
  }

  @Mutation
  public SET_EFORM_BUFFER(arrayBuffer: any): void {
    this.eFormBuffer.value = arrayBuffer;
  }

  // @Mutation
  // public CLEAR_EFORM(): void {
  //   this.eForm.value = undefined;
  // }

  // List
  // @Mutation
  // public SET_EFORMS(templates: Array<EForm>): void {
  //   this.eforms.value = templates;
  // }

  // @Mutation
  // public SET_CURRENT_FOLDER_EFORMS(folderId: string | null): void {
  //   this.currentFolderEForms.value = this.eforms.value.filter((r) => {
  //     return r.ParentId == folderId;
  //   });
  // }

  // @Action({ rawError: true })
  // public async ListEForms(): Promise<void> {
  //   const service = new EFormsService();
  //   const response = await service.list();
  //   if (isLeft(response)) {
  //     const listResponse: ListEFormsResponse = response.value;
  //     this.SET_EFORMS(listResponse.EForms);
  //     this.SET_CURRENT_FOLDER_EFORMS(null);

  //     return;
  //   }
  //   if (
  //     response.value.message !== undefined &&
  //     response.value.message["ErrorType"] != 3
  //   ) {
  //     AppModule.SetErrorNotifications(this.ErrorMessage);
  //   }
  //   this.SET_ERROR(response.value as AppError);
  //   throw response.value;
  // }

  @Action({ rawError: true })
  public async ListEForms(): Promise<void> {
    const service = new EFormsService();
    const response = await service.list();
    if (isLeft(response)) {
      const listResponse: ListEFormsResponse = response.value;
      this.SET_EFORMS(listResponse.EForms);
      this.SET_CURRENT_FOLDER_EFORMS(null);

      return;
    }
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ReadAnnex2OfSMTA1(id: string): Promise<void> {
    const service = new EFormsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.readAnnex2OfSMTA1({ Id: id });
      if (isLeft(response)) {
        const readResponse: ReadAnnex2OfSMTA1Response = response.value;

        this.SET_ANNEX2OFSMTA1(readResponse.Annex2OfSMTA1Data);
        return;
      }

      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      throw response.value;
    }
  }

  @Action({ rawError: true })
  public async ReadBookingFormOfSMTA1(id: string): Promise<void> {
    const service = new EFormsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.readBookingFormOfSMTA1({ Id: id });
      if (isLeft(response)) {
        const readResponse: ReadBookingFormOfSMTA1Response = response.value;
        this.SET_BOOKINGFORMOFSMTA1(readResponse.BookingFormOfSMTA1Data);
        return;
      }

      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      throw response.value;
    }
  }

  @Action({ rawError: true })
  public async ReadAnnex2OfSMTA2(id: string): Promise<void> {
    const service = new EFormsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.readAnnex2OfSMTA2({ Id: id });
      if (isLeft(response)) {
        const readResponse: ReadAnnex2OfSMTA2Response = response.value;

        this.SET_ANNEX2OFSMTA2(readResponse.Annex2OfSMTA2Data);
        return;
      }

      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      throw response.value;
    }
  }

  @Action({ rawError: true })
  public async ReadBiosafetyChecklistOfSMTA2(id: string): Promise<void> {
    const service = new EFormsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.readBiosafetyChecklistOfSMTA2({ Id: id });
      if (isLeft(response)) {
        const readResponse: ReadBiosafetyChecklistOfSMTA2Response =
          response.value;

        this.SET_BIOSAFETYCHECKLISTOFSMTA2(
          readResponse.BiosafetyChecklistOfSMTA2Data
        );
        return;
      }

      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      throw response.value;
    }
  }

  @Action({ rawError: true })
  public async ReadBookingFormOfSMTA2(id: string): Promise<void> {
    const service = new EFormsService();
    if (id !== undefined && name !== undefined) {
      const response = await service.readBookingFormOfSMTA2({ Id: id });
      if (isLeft(response)) {
        const readResponse: ReadBookingFormOfSMTA2Response = response.value;
        this.SET_BOOKINGFORMOFSMTA2(readResponse.BookingFormOfSMTA2Data);
        return;
      }

      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      throw response.value;
    }
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get EForms(): EForm[] {
    return this.eForms.value ?? new Array<EForm>();
  }

  get CurrentFolderEForms(): EForm[] {
    return this.currentFolderEForms.value ?? new Array<EForm>();
  }

  get Annex2OfSMTA1(): Annex2OfSMTA1Data | undefined {
    return this.annex2OfSMTA1.value;
  }

  get BookingFormOfSMTA1(): BookingFormOfSMTA1Data | undefined {
    return this.bookingFormOfSMTA1.value;
  }

  get Annex2OfSMTA2(): Annex2OfSMTA2Data | undefined {
    return this.annex2OfSMTA2.value;
  }

  get BiosafetyChecklistOfSMTA2(): BiosafetyChecklistOfSMTA2Data | undefined {
    return this.biosafetyChecklistOfSMTA2.value;
  }

  get BookingFormOfSMTA2(): BookingFormOfSMTA2Data | undefined {
    return this.bookingFormOfSMTA2.value;
  }

  get EFormBuffer(): any {
    return this.eFormBuffer.value;
  }

  get emptyEForm(): EForm {
    return Object.create({
      Id: "",
      Name: "",
      Url: "",
      Type: EFormItemType.EForm,
      ApprovedTime: new Date(),
      ParentId: "",
    } as EForm);
  }

  // get EForms(): EForm[] {
  //   return this.eforms.value ?? new Array<EForm>();
  // }

  // get CurrentFolderEForms(): EForm[] {
  //   return this.currentFolderEForms.value ?? new Array<EForm>();
  // }

  // get emptyEForm(): EForm {
  //   return Object.create({
  //     Id: "",
  //     Name: "",
  //     Extension: undefined,
  //     Type: EFormType.File,
  //     UploadTime: new Date(),
  //     UploadedBy: "",
  //     ParentId: "",
  //   } as EForm);
  // }
}

export const EFormModule = getModule(EFormStore, store);
