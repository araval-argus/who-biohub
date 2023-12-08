import { TemperatureUnitOfMeasure } from "@/models/TemperatureUnitOfMeasure";

export interface ReadTemperatureUnitOfMeasureQuery {
  Id: string;
}

export interface ReadTemperatureUnitOfMeasureResponse {
  TemperatureUnitOfMeasure: TemperatureUnitOfMeasure;
}
