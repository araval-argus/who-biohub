import { AppError } from "@/models/shared/Error";
import { Either, isLeft, isRight, Left, Right } from "@/utils/either";
import * as msal from "@azure/msal-browser";
import { InteractionRequiredAuthError } from "@azure/msal-browser";

export class MsalError extends AppError {
  code: string | undefined;

  constructor(error: Error, code?: string);
  constructor(error: string, code?: string);
  constructor(...params: any[]) {
    if (typeof params[0] === "string") {
      super(params[0]);
    } else {
      super(params[0].message);
    }

    this.code = params[1];
  }
}

export interface iMsalClient {
  loginPopupAsync(): Promise<Either<msal.AuthenticationResult, MsalError>>;
  loginRedirectAsync(): Promise<Either<boolean, MsalError>>;
  loginSilentAsync(): Promise<Either<boolean, MsalError>>;
  acquireTokenSilentAsync(): Promise<
    Either<msal.AuthenticationResult, MsalError>
  >;
  acquireTokenPopupAsync(): Promise<
    Either<msal.AuthenticationResult, MsalError>
  >;
  acquireTokenRedirectAsync(): Promise<Either<boolean, MsalError>>;
  startLoginPopupAsync(): Promise<
    Either<msal.AuthenticationResult | undefined, MsalError>
  >;
  startLoginRedirectAsync(): Promise<
    Either<msal.AuthenticationResult | undefined, MsalError>
  >;
  getMyAccountInfo(): Either<msal.AccountInfo, MsalError>;
  logoutPopupAsync(): Promise<Either<boolean, MsalError>>;
  logoutRedirectAsync(): Promise<Either<boolean, MsalError>>;
}

export class MsalClient implements iMsalClient {
  private msalInstance: msal.PublicClientApplication;

  constructor() {
    const configuration: msal.Configuration = {
      auth: {
        clientId: process.env?.VUE_APP_MSAL_CLIENT_ID ?? "",
        redirectUri: process.env?.VUE_APP_MSAL_REDIRECT_URI,
        postLogoutRedirectUri:
          process.env?.VUE_APP_MSAL_POST_LOGOUT_REDIRECT_URI,
        authority: process.env?.VUE_APP_MSAL_AUTHORITY,
      },
      cache: {
        cacheLocation: "localStorage",
        storeAuthStateInCookie: true,
      },
    };

    this.msalInstance = new msal.PublicClientApplication(configuration);
  }
  private readonly popupRequest: msal.PopupRequest = {
    scopes: process.env?.VUE_APP_MSAL_SCOPES?.split(",") ?? [],
    prompt: "select_account",
  };
  private readonly redirectRequest: msal.RedirectRequest = {
    scopes: process.env?.VUE_APP_MSAL_SCOPES?.split(",") ?? [],
  };

  async loginPopupAsync(): Promise<
    Either<msal.AuthenticationResult, MsalError>
  > {
    try {
      const loginResult = await this.msalInstance.loginPopup(this.popupRequest);
      this.msalInstance.setActiveAccount(loginResult.account);
      return new Left(loginResult);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async loginRedirectAsync(): Promise<Either<boolean, MsalError>> {
    try {
      await this.msalInstance.loginRedirect(this.redirectRequest);
      return new Left(true);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async loginSilentAsync(): Promise<Either<boolean, MsalError>> {
    try {
      let redirectUri: string = process.env?.VUE_APP_MSAL_REDIRECT_URI ?? "";
      redirectUri = redirectUri.replace("auth/", "");
      const result = await this.msalInstance.ssoSilent({
        scopes: process.env?.VUE_APP_MSAL_SCOPES?.split(",") ?? [],
        redirectUri: redirectUri,
      });

      if (result) {
        this.msalInstance.setActiveAccount(result.account);
        return new Left(true);
      } else {
        return new Right(
          this.getMsalError(new AppError("Account info not found"))
        );
      }
    } catch (error) {
      if (error instanceof InteractionRequiredAuthError) {
        try {
          const loginResponse = await this.loginPopupAsync();
          return new Left(true);
        } catch (err) {
          return new Right(this.getMsalError(err));
        }
      }
      return new Right(this.getMsalError(error));
    }
  }

  async acquireTokenSilentAsync(): Promise<
    Either<msal.AuthenticationResult, MsalError>
  > {
    try {
      return new Left(
        await this.msalInstance.acquireTokenSilent(this.popupRequest)
      );
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async acquireTokenPopupAsync(): Promise<
    Either<msal.AuthenticationResult, MsalError>
  > {
    try {
      return new Left(
        await this.msalInstance.acquireTokenPopup(this.popupRequest)
      );
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async acquireTokenRedirectAsync(): Promise<Either<boolean, MsalError>> {
    try {
      await this.msalInstance.acquireTokenRedirect(this.redirectRequest);
      return new Left(true);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async startLoginPopupAsync(): Promise<
    Either<msal.AuthenticationResult | undefined, MsalError>
  > {
    try {
      let authenticationResult: msal.AuthenticationResult | undefined =
        undefined;
      const getMyAccountInfoResult = this.getMyAccountInfo();
      if (isLeft(getMyAccountInfoResult)) {
        let acquireTokenSilentResult = await this.acquireTokenSilentAsync();
        if (
          isLeft(acquireTokenSilentResult) &&
          acquireTokenSilentResult.value != null
        ) {
          authenticationResult = acquireTokenSilentResult.value ?? undefined;
        } else if (isRight(acquireTokenSilentResult)) {
          if (
            acquireTokenSilentResult.value.message.indexOf(
              "interaction_required"
            ) !== -1
          ) {
            const acquireTokenPopupResult = await this.acquireTokenPopupAsync();
            if (isLeft(acquireTokenPopupResult)) {
              acquireTokenSilentResult = await this.acquireTokenSilentAsync();
              if (isLeft(acquireTokenSilentResult)) {
                authenticationResult = acquireTokenSilentResult.value;
              } else {
                return acquireTokenSilentResult;
              }
            } else {
              return acquireTokenPopupResult;
            }
          } else {
            return acquireTokenSilentResult;
          }
        }
      } else {
        const loginPopupResult = await this.loginPopupAsync();
        if (isLeft(loginPopupResult)) {
          let acquireTokenSilentResult = await this.acquireTokenSilentAsync();
          if (isLeft(acquireTokenSilentResult)) {
            acquireTokenSilentResult = await this.acquireTokenSilentAsync();
            if (isLeft(acquireTokenSilentResult)) {
              authenticationResult = acquireTokenSilentResult.value;
            } else {
              return acquireTokenSilentResult;
            }
          } else {
            return acquireTokenSilentResult;
          }
        } else {
          return loginPopupResult;
        }
      }

      return new Left(authenticationResult);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async startLoginRedirectAsync(): Promise<
    Either<msal.AuthenticationResult | undefined, MsalError>
  > {
    try {
      let accountInfo: msal.AuthenticationResult | undefined = undefined;
      const getMyAccountInfoResult = this.getMyAccountInfo();
      if (isLeft(getMyAccountInfoResult)) {
        const acquireTokenSilentResult = await this.acquireTokenSilentAsync();
        if (
          isLeft(acquireTokenSilentResult) &&
          acquireTokenSilentResult.value != null
        ) {
          accountInfo = acquireTokenSilentResult.value ?? undefined;
        } else if (isRight(acquireTokenSilentResult)) {
          if (
            acquireTokenSilentResult.value.message.indexOf(
              "interaction_required"
            ) !== -1
          ) {
            await this.acquireTokenRedirectAsync();
          } else {
            return acquireTokenSilentResult;
          }
        }
      } else {
        await this.loginRedirectAsync();
      }

      return new Left(accountInfo);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  getMyAccountInfo(): Either<msal.AccountInfo, MsalError> {
    try {
      const accountInfo = this.msalInstance.getActiveAccount();
      if (accountInfo) {
        return new Left(accountInfo);
      } else {
        return new Right(
          this.getMsalError(new AppError("Account info not found"))
        );
      }
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async logoutPopupAsync(): Promise<Either<boolean, MsalError>> {
    try {
      await this.msalInstance.logoutPopup(this.popupRequest);
      return new Left(true);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  async logoutRedirectAsync(): Promise<Either<boolean, MsalError>> {
    try {
      await this.msalInstance.logoutRedirect(this.redirectRequest);
      return new Left(true);
    } catch (error) {
      return new Right(this.getMsalError(error));
    }
  }

  private getMsalError(error: any): MsalError {
    let result: MsalError;

    if (error instanceof msal.AuthError) {
      result = new MsalError(error, error.errorCode);
    } else if (error instanceof MsalError) {
      result = error;
    } else if (error instanceof AppError) {
      result = new MsalError(error.message, "");
    } else {
      result = new MsalError(error);
    }

    return result;
  }
}
