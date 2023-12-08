import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  ListWorklistFromBioHubItemEventQuery,
  ListWorklistFromBioHubItemEventResponse,
} from "./models/ListWorklistFromBioHubItemEvent";

export interface iWorklistFromBioHubItemEventsService {
  list(
    query: ListWorklistFromBioHubItemEventQuery
  ): Promise<
    Either<ListWorklistFromBioHubItemEventResponse, CommunicationError>
  >;
}

export class WorklistFromBioHubItemEventsService
  implements iWorklistFromBioHubItemEventsService
{
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "worklistfrombiohubitemevents/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListWorklistFromBioHubItemEventQuery
  ): Promise<
    Either<ListWorklistFromBioHubItemEventResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.WorklistFromBioHubItemId);
    const response =
      await this.httpClient.get<ListWorklistFromBioHubItemEventResponse>(url);
    return response;
  }
}
