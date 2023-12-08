import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  ListSMTA2WorkflowItemEventQuery,
  ListSMTA2WorkflowItemEventResponse,
} from "./models/ListSMTA2WorkflowItemEvent";

export interface iSMTA2WorkflowItemEventsService {
  list(
    query: ListSMTA2WorkflowItemEventQuery
  ): Promise<Either<ListSMTA2WorkflowItemEventResponse, CommunicationError>>;
}

export class SMTA2WorkflowItemEventsService
  implements iSMTA2WorkflowItemEventsService
{
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "smta2workflowitemevents/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTA2WorkflowItemEventQuery
  ): Promise<Either<ListSMTA2WorkflowItemEventResponse, CommunicationError>> {
    const url = this.buildUrl(query.SMTA2WorkflowItemId);
    const response =
      await this.httpClient.get<ListSMTA2WorkflowItemEventResponse>(url);
    return response;
  }
}
