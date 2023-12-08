import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Courier } from "@/models/Courier";
import { CourierBookingForm } from "@/models/CourierBookingForm";
import { CouriersService } from "@/services/couriers/CouriersService";
import { isLeft, isRight, Right, ParseError } from "@/utils/either";
import { couriers } from "./mock";
import { courierBookingForms } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListCourierResponse } from "@/services/couriers/models/ListCourier";
import { CreateCourierResponse } from "@/services/couriers/models/CreateCourier";
import { DeleteCourierResponse } from "@/services/couriers/models/DeleteCourier";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadCourierQuery } from "@/services/couriers/models/ReadCourier";
import { ReadCourierResponse } from "@/services/couriers/models/ReadCourier";

import { ListCourierBookingFormQuery } from "@/services/couriers/models/ListCourierBookingForm";
import { ListCourierBookingFormResponse } from "@/services/couriers/models/ListCourierBookingForm";
import { ReadCourierBookingFormQuery } from "@/services/couriers/models/ReadCourierBookingForm";
import { ReadCourierBookingFormResponse } from "@/services/couriers/models/ReadCourierBookingForm";
import { AppModule } from "../../store/MainStore";
import { CourierBookingFormUser } from "@/models/CourierBookingFormUser";

export interface CourierState {
  CourierCreate: Courier | undefined;
  Courier: Courier | undefined;
  Couriers: Array<Courier>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "couriers",
  store: store,
})
class CourierStore extends VuexModule implements CourierState {
  // Private variables
  private courierCreate: { value: Courier } = {
    value: this.emptyCourier,
  };

  private courier: { value: Courier | undefined } = {
    value: undefined,
  };

  private couriers: { value: Array<Courier> } = {
    value: couriers,
  };

  private courierBookingForms: { value: Array<CourierBookingForm> } = {
    value: courierBookingForms,
  };

  private courierBookingForm: { value: CourierBookingForm | undefined } = {
    value: undefined,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_COURIER_CREATE(courier: Courier): void {
    this.courierCreate.value = courier;
  }

  // Details - Edit

  @Mutation
  public SET_COURIER(courier: Courier | undefined): void {
    this.courier.value = courier;
  }

  @Mutation
  public CLEAR_COURIER(): void {
    this.courier.value = undefined;
  }

  @Mutation
  public SET_COURIERBOOKINGFORM(
    courierBookingForm: CourierBookingForm | undefined
  ): void {
    this.courierBookingForm.value = courierBookingForm;
  }

  @Mutation
  public CLEAR_COURIERBOOKINGFORM(): void {
    this.courierBookingForm.value = undefined;
  }

  @Mutation
  public CLEAR_COURIER_CREATE(): void {
    this.courierCreate.value = Object.create({
      Id: "",
      Name: "",
      WHOAccountNumber: "",
      Email: "",
      IsActive: false,
      Description: "",
      Address: "",
      Latitude: "",
      Longitude: "",
      CountryId: "",
    });
  }

  // List
  @Mutation
  public SET_COURIERS(couriers: Array<Courier>): void {
    this.couriers.value = couriers;
  }

  @Mutation
  public SET_COURIERBOOKINGFORMS(
    courierBookingForms: Array<CourierBookingForm>
  ): void {
    this.courierBookingForms.value = courierBookingForms;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateCourier(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CouriersService();
    const courier = this.courierCreate.value;
    if (courier === undefined) {
      this.SET_ERROR({
        message:
          "CouriersStore: not expecting courier to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(courier);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateCourierResponse = response.value;
      courier.Id = createResponse.Id;
      this.SET_COURIER(courier);
      this.SET_COURIER_CREATE(this.emptyCourier);
      AppModule.SetSuccessNotifications("Courier successfully created");

      AppModule.HideLoading();
      return;
    }
    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    AppModule.HideLoading();
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListCouriers(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new CouriersService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListCourierResponse = response.value;
      this.SET_COURIERS(listResponse.Couriers);
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
  public async ReadCourier(id: string): Promise<void> {
    const service = new CouriersService();
    const query: ReadCourierQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadCourierResponse = response.value;
      this.SET_COURIER(readResponse.Courier);
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

  @Action({ rawError: true })
  public async ListCourierBookingForms(courierId: string): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new CouriersService();
    const query: ListCourierBookingFormQuery = { CourierId: courierId };
    const response = await service.listBookingForms(query);
    if (isLeft(response)) {
      const listResponse: ListCourierBookingFormResponse = response.value;
      this.SET_COURIERBOOKINGFORMS(listResponse.CourierBookingForms);
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
  public async ReadCourierBookingForm(id: string): Promise<void> {
    const service = new CouriersService();
    const query: ReadCourierBookingFormQuery = { Id: id };
    const response = await service.readBookingForm(query);
    if (isLeft(response)) {
      const readResponse: ReadCourierBookingFormResponse = response.value;
      this.SET_COURIERBOOKINGFORM(readResponse.CourierBookingForm);
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

  @Action({ rawError: true })
  public async UpdateCourier(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CouriersService();
    const courier: Courier | undefined = this.Courier;
    if (!courier) {
      this.SET_ERROR({
        message:
          "CouriersStore: not expecting courier to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(courier);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_COURIER(courier);
      AppModule.SetSuccessNotifications("Courier successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteCourier(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CouriersService();
    const courier: Courier | undefined = this.Courier;
    if (!courier) {
      this.SET_ERROR({
        message:
          "CouriersStore: not expecting courier to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(courier);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      const deleteCourierResponse: DeleteCourierResponse = response.value;
      this.SET_COURIER(undefined);
      AppModule.SetSuccessNotifications("Courier successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get Courier(): Courier | undefined {
    return this.courier.value;
  }

  get CourierBookingForm(): CourierBookingForm | undefined {
    return this.courierBookingForm.value;
  }

  get BookingFormPickupUsers(): CourierBookingFormUser[] {
    return (
      this.courierBookingForm.value?.BookingFormPickupUsers ??
      new Array<CourierBookingFormUser>()
    );
  }

  get BookingFormCourierUsers(): CourierBookingFormUser[] {
    return (
      this.courierBookingForm.value?.BookingFormCourierUsers ??
      new Array<CourierBookingFormUser>()
    );
  }

  get Couriers(): Courier[] {
    return this.couriers.value ?? new Array<Courier>();
  }

  get CourierBookingForms(): CourierBookingForm[] {
    return this.courierBookingForms.value ?? new Array<CourierBookingForm>();
  }

  get CourierCreate(): Courier {
    return this.courierCreate.value;
  }

  get emptyCourier(): Courier {
    return Object.create({
      Id: "",
      Name: "",
      WHOAccountNumber: "",
      Email: "",
      IsActive: false,
      Description: "",
      Address: "",
      Latitude: "",
      Longitude: "",
      CountryId: "",
    });
  }
}

export const CourierModule = getModule(CourierStore, store);
