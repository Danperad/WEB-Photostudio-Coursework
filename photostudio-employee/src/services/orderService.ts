import axiosInstant from "../utils/axiosInstant.ts";
import {Client, Order, OrderService, Status} from "@models/*";
import {AxiosError} from "axios";
import {Err, Ok, Result} from "ts-results";

export async function getOrdersByClient(client: Client): Promise<Result<Order[], AxiosError>> {
  try {
    const res = await axiosInstant.get<Order[]>(`orders/client/${client.id}`);
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function getAllOrders(search?: string): Promise<Result<Order[], AxiosError>> {
  try {
    const res = await axiosInstant.get<Order[]>(`orders/`, {
      params: {
        search: search
      }
    });
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function getServicesByOrders(order: Order): Promise<Result<OrderService[], AxiosError>> {
  try {
    const res = await axiosInstant.get<OrderService[]>(`applications/order/${order.number}`);
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function addNewOrder(order: Order): Promise<Result<Order, AxiosError>> {
  try {
    const res = await axiosInstant.post<Order>(`orders`, order);
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function updateOrderStatus(order: Order, status: Status): Promise<Result<Order, AxiosError>> {
  try {
    const res = await axiosInstant.post<Order>(`orders/update`, {orderId: order.number, statusId: status.id});
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}
