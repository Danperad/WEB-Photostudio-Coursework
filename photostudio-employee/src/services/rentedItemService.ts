import axiosInstant from "../utils/axiosInstant.ts";
import {Item} from "@models/*";
import {AxiosError} from "axios";
import {Err, Ok, Result} from "ts-results";

export async function getAvailableItems(start: Date, duration: number) : Promise<Result<Item[], AxiosError>> {
  try {
    const res = await axiosInstant.get<Item[]>('items/available', {
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
