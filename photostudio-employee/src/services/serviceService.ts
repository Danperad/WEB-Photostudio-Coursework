import axiosInstant from "../utils/axiosInstant.ts";
import {Service} from "@models/*";
import {AxiosError} from "axios";
import {Err, Ok, Result} from "ts-results";

export async function getServices() : Promise<Result<Service[], AxiosError>> {
  try {
    const res = await axiosInstant.get<Service[]>('services');
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}
