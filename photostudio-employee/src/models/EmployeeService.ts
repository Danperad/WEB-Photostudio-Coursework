import {OrderService} from "./OrderService.ts";
import {Client} from "./Client.ts";

interface EmployeeService extends OrderService {
  client: Client
}

export type {EmployeeService}
