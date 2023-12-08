import { Courier } from "@/models/Courier";

export interface ReadCourierQuery {
  Id: string;
}

export interface ReadCourierResponse {
  Courier: Courier;
}
