import { Coordinates } from "@/models/shared/Coordinates";
import { PopupFooter } from "@/models/shared/PopupFooter";

export class MapElementInfo {
  Coordinates: Coordinates;
  PopupHeader: string;
  PopupAddress: string;
  PopupCountry: string;
  PopupFooter: Array<PopupFooter>;
  Group: string;
  IconColor: string;
  ToBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  FromBioHubConnectedInstitutesLatLng: Array<Coordinates>;

  constructor(
    Latitude: number,
    Longitude: number,
    PopupHeader: string,
    PopupAddress: string,
    PopupCountry: string,
    PopupFooter: Array<PopupFooter>,
    Group: string,
    IconColor: string,
    ToBioHubConnectedInstitutesLatLng: Array<Coordinates>,
    FromBioHubConnectedInstitutesLatLng: Array<Coordinates>
  ) {
    this.Coordinates = new Coordinates(Latitude, Longitude);
    this.PopupHeader = PopupHeader;
    this.PopupAddress = PopupAddress;
    this.PopupCountry = PopupCountry;
    this.PopupFooter = PopupFooter;
    this.Group = Group;
    this.IconColor = IconColor;
    this.ToBioHubConnectedInstitutesLatLng = ToBioHubConnectedInstitutesLatLng;
    this.FromBioHubConnectedInstitutesLatLng =
      FromBioHubConnectedInstitutesLatLng;
  }
}
