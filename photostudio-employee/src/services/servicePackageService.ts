import {Err, Ok, Result} from "ts-results";
import {ServicePackage} from "@models/*";
import {AxiosError} from "axios";
import axiosInstant from "../utils/axiosInstant.ts";

export async function getServicesPackage(): Promise<Result<ServicePackage[], AxiosError>> {
  try {
    const res = await axiosInstant.get<ServicePackage[]>('packages/available', {
      params: {
        start: new Date()
      }
    });
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}
