import { AuthModule } from "@/features/auth/store";
import { AppError } from "@/models/shared/Error";
import { Either, Left, Right } from "@/utils/either";
import { saveDownloadedFile } from "@/utils/helper";
import axios, { Axios, AxiosResponse } from "axios";

export class CommunicationError extends AppError {
  error: object;
  code: number;

  constructor(response: AxiosResponse) {
    super(response.data);

    this.error = response.data ?? response.statusText;
    this.code = response.status;
  }
}

export interface iHttpClientPublic {
  getPublic<R>(url: string): Promise<Either<R, CommunicationError>>;
  postPublic<T, R>(
    url: string,
    data: T
  ): Promise<Either<R, CommunicationError>>;
  patchPublic<T, R>(
    url: string,
    data: T
  ): Promise<Either<R, CommunicationError>>;
  downloadFileFromStoragePublic(
    url: string,
    filename: string
  ): Promise<Either<any, CommunicationError>>;
}

export interface iHttpClient {
  get<R>(url: string): Promise<Either<R, CommunicationError>>;
  post<T, R>(url: string, data: T): Promise<Either<R, CommunicationError>>;
  postFile<R>(
    url: string,
    formData: FormData
  ): Promise<Either<R, CommunicationError>>;
  patch<T, R>(url: string, data: T): Promise<Either<R, CommunicationError>>;
  patchFile<R>(
    url: string,
    formData: FormData
  ): Promise<Either<R, CommunicationError>>;
  delete<R>(url: string): Promise<Either<R, CommunicationError>>;
  downloadFile(
    url: string,
    filename: string
  ): Promise<Either<any, CommunicationError>>;

  downloadFileFromStorage(
    url: string,
    filename: string
  ): Promise<Either<any, CommunicationError>>;

  uploadFileToStorage<R>(
    url: string,
    formData: File
  ): Promise<Either<R, CommunicationError>>;
}

export class HttpClient implements iHttpClient {
  private http: Axios;
  private httpAzureStorage: Axios;
  private baseUrl;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl?.replace(/\/$/, "") + "/";
    this.http = axios.create({ baseURL: this.baseUrl });
    this.httpAzureStorage = axios.create();

    this.http.interceptors.request.use(
      (config) => {
        if (config && config.headers) {
          config.headers["Authorization"] = `Bearer ${AuthModule.UserToken}`;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );
  }

  public async get<T>(url: string): Promise<Either<T, CommunicationError>> {
    let response: AxiosResponse<T>;
    try {
      response = await this.http.get<T>(url);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  public async downloadFile(
    url: string,
    filename: string
  ): Promise<Either<any, CommunicationError>> {
    let err: any;
    try {
      await this.http
        .get(url, { responseType: "blob" })
        .then((response) => {
          saveDownloadedFile(response.data, filename);
        })
        .catch((e: any) => {
          err = new Right(new CommunicationError(e.response));
        });
    } catch (e: any) {
      err = new Right(new CommunicationError(e.response));
    }

    if (err !== undefined) {
      return err;
    } else {
      return new Left(null);
    }
  }

  public async downloadFileFromStorage(
    url: string,
    filename: string
  ): Promise<Either<any, CommunicationError>> {
    let err: any;
    try {
      await this.httpAzureStorage
        .get(url, {
          headers: {
            "x-ms-blob-type": "BlockBlob",
          },
          responseType: "blob",
        })
        .then((response) => {
          saveDownloadedFile(response.data, filename);
        })
        .catch((e: any) => {
          err = new Right(new CommunicationError(e.response));
        });
    } catch (e: any) {
      err = new Right(new CommunicationError(e.response));
    }

    if (err !== undefined) {
      return err;
    } else {
      return new Left(null);
    }
  }

  public async uploadFileToStorage<R>(
    url: string,
    formData: File
  ): Promise<Either<any, CommunicationError>> {
    let err: any;
    let response: AxiosResponse<R>;
    try {
      response = await this.httpAzureStorage.put(url, formData, {
        headers: {
          "x-ms-blob-type": "BlockBlob",
        },
      });

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      err = new Right(new CommunicationError(e.response));
    }

    if (err !== undefined) {
      return err;
    } else {
      return new Left(null);
    }
  }

  async post<T, R>(
    url: string,
    data: T
  ): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;

    try {
      response = await this.http.post<R>(url, data);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  async postFile<R>(
    url: string,
    formData: FormData
  ): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;
    try {
      response = await this.http.post<R>(url, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  async patch<T, R>(
    url: string,
    data: T
  ): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;
    try {
      response = await this.http.patch<R>(url, data);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  async patchFile<R>(
    url: string,
    formData: FormData
  ): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;
    try {
      response = await this.http.patch<R>(url, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  async delete<R>(url: string): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;
    try {
      response = await this.http.delete<R>(url);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  private isError(response: AxiosResponse): boolean {
    return response.status < 200 || response.status >= 300;
  }
}

export class HttpClientPublic implements iHttpClientPublic {
  private http: Axios;
  private baseUrl;
  private httpAzureStorage: Axios;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl?.replace(/\/$/, "") + "/";
    this.http = axios.create({ baseURL: this.baseUrl });
    this.httpAzureStorage = axios.create();
    this.http.interceptors.request.use(
      (config) => {
        // TODO: implement authentication logic here
        return config;
      },
      (error) => Promise.reject(error)
    );
  }

  public async getPublic<T>(
    url: string
  ): Promise<Either<T, CommunicationError>> {
    let response: AxiosResponse<T>;
    try {
      response = await this.http.get<T>(url);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  async postPublic<T, R>(
    url: string,
    data: T
  ): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;

    try {
      response = await this.http.post<R>(url, data);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  async patchPublic<T, R>(
    url: string,
    data: T
  ): Promise<Either<R, CommunicationError>> {
    let response: AxiosResponse<R>;
    try {
      response = await this.http.patch<R>(url, data);

      if (this.isError(response)) {
        const err = new CommunicationError(response);
        return new Right(err);
      }

      return new Left(response.data);
    } catch (e: any) {
      return new Right(new CommunicationError(e.response));
    }
  }

  public async downloadFileFromStoragePublic(
    url: string,
    filename: string
  ): Promise<Either<any, CommunicationError>> {
    let err: any;
    try {
      await this.httpAzureStorage
        .get(url, {
          headers: {
            "x-ms-blob-type": "BlockBlob",
          },
          responseType: "blob",
        })
        .then((response) => {
          saveDownloadedFile(response.data, filename);
        })
        .catch((e: any) => {
          err = new Right(new CommunicationError(e.response));
        });
    } catch (e: any) {
      err = new Right(new CommunicationError(e.response));
    }

    if (err !== undefined) {
      return err;
    } else {
      return new Left(null);
    }
  }

  private isError(response: AxiosResponse): boolean {
    return response.status < 200 || response.status >= 300;
  }
}
