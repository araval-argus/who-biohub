import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  ListSMTA1WorkflowItemEventQuery,
  ListSMTA1WorkflowItemEventResponse,
} from "./models/ListSMTA1WorkflowItemEvent";

export interface iSMTA1WorkflowItemEventsService {
  list(
    query: ListSMTA1WorkflowItemEventQuery
  ): Promise<Either<ListSMTA1WorkflowItemEventResponse, CommunicationError>>;
}

export class SMTA1WorkflowItemEventsService
  implements iSMTA1WorkflowItemEventsService
{
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "smta1workflowitemevents/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTA1WorkflowItemEventQuery
  ): Promise<Either<ListSMTA1WorkflowItemEventResponse, CommunicationError>> {
    const url = this.buildUrl(query.SMTA1WorkflowItemId);
    const response =
      await this.httpClient.get<ListSMTA1WorkflowItemEventResponse>(url);
    return response;
  }
}
