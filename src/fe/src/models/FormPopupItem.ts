import { InputType } from "./enums/InputType";

export interface FormPopupItem {
  Type: InputType;
  Label: string;
  PropertyName: string;
  Value: unknown;
  Items: Array<any>;
  Required: boolean;
  Readonly: boolean;
  Hide: boolean;
}
