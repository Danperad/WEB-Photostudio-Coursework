import {Status} from "./Status.ts";
import {Employee, Hall, RentedItem, Service} from "./Models.ts";

type OrderService = {
  id: number
  orderStatus: number,
  service: Service,
  item?: RentedItem,
  count?: number,
  employee: Employee,
  hall?: Hall
  startDateTime?: Date,
  duration?: number,
  isFullTime?: boolean,
  status?: Status
  cost: number
}

export type {OrderService}
