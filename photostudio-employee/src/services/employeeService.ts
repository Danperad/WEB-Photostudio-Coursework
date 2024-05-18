import axiosInstant from "../utils/axiosInstant.ts";
import {Employee, EmployeeService} from "@models/*";
import {Err, Ok, Result} from "ts-results";
import {AxiosError} from "axios";

export async function getAvailableEmployees(start: Date, duration: number, serviceId: number) : Promise<Result<Employee[], AxiosError>> {
  try {
    const res = await axiosInstant.get('employees/available', {
      params: {
        start: start,
        duration: duration,
        serviceId: serviceId
      }
    });
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function getServicesByEmployee(employeeId: number, showAll?: boolean) : Promise<Result<EmployeeService[], AxiosError>> {
  try {
    const res = await axiosInstant.get<EmployeeService[]>(`applications/`, {params: {employeeId, showAll}});
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}

export async function updateServicesStatus(serviceId: number, statusId: number) : Promise<Result<EmployeeService, AxiosError>> {
  try {
    const res = await axiosInstant.post<EmployeeService>(`applications/update`, {orderServiceId: serviceId, statusId});
    return Ok(res.data);
  } catch (error) {
    const err = error as AxiosError;
    return Err(err);
  }
}
