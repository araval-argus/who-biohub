import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  ListWorklistToBioHubItemEventQuery,
  ListWorklistToBioHubItemEventResponse,
} from "./models/ListWorklistToBioHubItemEvent";

export interface iWorklistToBioHubItemEventsService {
  list(
    query: ListWorklistToBioHubItemEventQuery
  ): Promise<Either<ListWorklistToBioHubItemEventResponse, CommunicationError>>;
}

export class WorklistToBioHubItemEventsService
  implements iWorklistToBioHubItemEventsService
{
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "worklisttobiohubitemevents/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListWorklistToBioHubItemEventQuery
  ): Promise<
    Either<ListWorklistToBioHubItemEventResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.WorklistToBioHubItemId);
    const response =
      await this.httpClient.get<ListWorklistToBioHubItemEventResponse>(url);
    return response;
  }
}
