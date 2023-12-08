import { AppError } from "@/models/shared/Error";

export class Left<A> {
  tag = "left";
  value: A;

  constructor(a: A) {
    this.value = a;
  }
}

export class Right<B> {
  tag = "right";
  value: B;

  constructor(b: B) {
    this.value = b;
  }
}

export type Either<A, B> = Left<A> | Right<B>;

export function isLeft<A, B>(val: Either<A, B>): val is Left<A> {
  if ((val as Left<A>).tag == "left") return true;
  return false;
}

export function isRight<A, B>(val: Either<A, B>): val is Right<B> {
  if ((val as Right<B>).tag == "right") return true;
  return false;
}

export function ParseError(error: AppError | undefined): AppError | undefined {
  if (typeof error !== "undefined") {
    if (
      error.message !== undefined &&
      error.message["Messages"] !== undefined &&
      error.message["Messages"] !== ""
    ) {
      const properties = Object.getOwnPropertyNames(error.message.Messages);
      const msg = new Map<string, string>();
      properties.forEach((property) => {
        msg.set(property, error.message.Messages[property]);
      });
      return new AppError(msg);
    } else {
      return error;
    }
  } else {
    return error;
  }
}
