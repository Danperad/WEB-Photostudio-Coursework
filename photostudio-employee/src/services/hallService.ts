import axiosInstant from "../utils/axiosInstant.ts";
import {Hall} from "@models/*";
import {AxiosError} from "axios";
import {Err, Ok, Result} from "ts-results";

export async function getAvailableHalls(start: Date, duration: number) : Promise<Result<Hall[], AxiosError>> {
  try {
    const res = await axiosInstant.get<Hall[]>('halls/available', {
      params: {
        start: start,
        duration: duration
      }
    });
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}
