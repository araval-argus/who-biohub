import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Country } from "@/models/Country";
import { CountriesService } from "@/services/countries/CountriesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { countries } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListCountryResponse } from "@/services/countries/models/ListCountry";
import { CreateCountryResponse } from "@/services/countries/models/CreateCountry";
import { DeleteCountryResponse } from "@/services/countries/models/DeleteCountry";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadCountryQuery } from "@/services/countries/models/ReadCountry";
import { ReadCountryResponse } from "@/services/countries/models/ReadCountry";
import { AppModule } from "@/store/MainStore";

export interface CountryState {
  CountryCreate: Country | undefined;
  Country: Country | undefined;
  Countries: Array<Country>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "countries",
  store: store,
})
class CountryStore extends VuexModule implements CountryState {
  // Private variables
  private countryCreate: { value: Country } = {
    value: this.emptyCountry,
  };

  private country: { value: Country | undefined } = {
    value: undefined,
  };

  private countries: { value: Array<Country> } = { value: countries };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_COUNTRY_CREATE(country: Country): void {
    this.countryCreate.value = country;
  }

  // Details - Edit
  @Mutation
  public SET_COUNTRY(country: Country | undefined): void {
    this.country.value = country;
  }

  @Mutation
  public CLEAR_COUNTRY(): void {
    this.country.value = undefined;
  }

  // List
  @Mutation
  public SET_COUNTRIES(countries: Array<Country>): void {
    this.countries.value = countries;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateCountry(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CountriesService();
    const country = this.countryCreate.value;
    if (country === undefined) {
      this.SET_ERROR({
        message:
          "CountriesStore: not expecting country to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(country);
    if (isLeft(response)) {
      const createResponse: CreateCountryResponse = response.value;
      country.Id = createResponse.Id;
      this.SET_COUNTRY(country);
      this.SET_COUNTRY_CREATE(this.emptyCountry);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListCountries(): Promise<void> {
    const service = new CountriesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListCountryResponse = response.value;
      this.SET_COUNTRIES(listResponse.Countries);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListCountriesPublic(): Promise<void> {
    const service = new CountriesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListCountryResponse = response.value;
      this.SET_COUNTRIES(listResponse.Countries);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ReadCountry(id: string): Promise<void> {
    const service = new CountriesService();
    const query: ReadCountryQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadCountryResponse = response.value;
      this.SET_COUNTRY(readResponse.Country);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateCountry(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CountriesService();
    const country: Country | undefined = this.Country;
    if (!country) {
      this.SET_ERROR({
        message:
          "CountriesStore: not expecting country to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(country);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteCountry(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CountriesService();
    const country: Country | undefined = this.Country;
    if (!country) {
      this.SET_ERROR({
        message:
          "CountriesStore: not expecting country to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(country);
    if (isLeft(response)) {
      const deleteCountryResponse: DeleteCountryResponse = response.value;
      this.SET_COUNTRY(undefined);
      AppModule.HideLoading();
      return;
    }

    const error = (response as Right<CommunicationError>).value;
    this.SET_ERROR(error as AppError);
    AppModule.HideLoading();
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get Country(): Country | undefined {
    return this.country.value;
  }

  get Countries(): Country[] {
    return this.countries.value ?? new Array<Country>();
  }

  get CountryCreate(): Country {
    return this.countryCreate.value;
  }

  get emptyCountry(): Country {
    return Object.create({
      Id: "",
      Uncode: "",
      Name: "",
      FullName: "",
      Iso2: "",
      Iso3: "",
      IsActive: false,
      Latitude: 0.0,
      Longitude: 0.0,
      GmtHour: 0,
      GmtMin: 0,
    });
  }
}

export const CountryModule = getModule(CountryStore, store);
