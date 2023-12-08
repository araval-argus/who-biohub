import store from "@/store";
import { Watch } from "vue-property-decorator";
import {
  VuexModule,
  Module,
  Mutation,
  getModule,
} from "vuex-module-decorators";

@Module({ namespaced: true, dynamic: true, store: store, name: "MainStore" })
class MainStore extends VuexModule {
  private errorNotifications: Array<string> | undefined = [];

  private successNotifications: Array<string> | undefined = [];

  private isLoadingActive: boolean;

  @Mutation
  public SetErrorNotifications(error: any): void {
    if (error !== undefined) {
      if (typeof error === "string") {
        if (this.errorNotifications == undefined) {
          this.errorNotifications = new Array<string>();
        }
        if (error != "") {
          this.errorNotifications.push(error);
        }
      } else {
        if (error.size > 0) {
          const array = Array.from(error.keys());
          array.forEach((x) => {
            if (this.errorNotifications == undefined) {
              this.errorNotifications = new Array<string>();
            }
            if (typeof x === "string" && x != undefined && x != "")
              this.errorNotifications.push(x.toString());
          });
        }
      }
    }
  }

  @Mutation
  public SetSuccessNotifications(text: string | Array<string>): void {
    let outputArray = new Array<string>();

    if (typeof text === "string") {
      outputArray.push(text);
    } else {
      outputArray = text;
    }

    this.successNotifications = outputArray;
  }

  @Mutation
  public ShowLoading(): void {
    this.isLoadingActive = true;
  }

  @Mutation
  public HideLoading(): void {
    this.isLoadingActive = false;
  }

  @Mutation
  public ClearErrorNotifications(): void {
    this.errorNotifications = new Array<string>();
  }

  @Mutation
  public ClearSuccessNotifications(): void {
    this.successNotifications = new Array<string>();
  }

  get ErrorNotifications(): Array<string> | undefined {
    return this.errorNotifications;
  }

  get SuccessNotifications(): Array<string> | undefined {
    return this.successNotifications;
  }

  get IsLoadingActive(): boolean {
    return this.isLoadingActive;
  }
}

export const AppModule = getModule(MainStore, store);
