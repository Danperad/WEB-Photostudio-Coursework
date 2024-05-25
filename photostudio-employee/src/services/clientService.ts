import axiosInstant from "../utils/axiosInstant.ts";
import {Client} from "@models/*";
import {Err, Ok, Result} from "ts-results";
import {AxiosError} from "axios";

export async function getClients(search?: string | undefined): Promise<Result<Client[], AxiosError>> {
  try {
    const res = await axiosInstant.get('clients', {
      params: {
        search: search,
      }
    });
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function addClient(client: Client): Promise<Result<Client, AxiosError>> {
  try {
    const res = await axiosInstant.post('clients', client);
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}
