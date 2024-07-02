import {OrderService} from "./OrderService.ts";
import {OrderServicePackage} from "./OrderServicePackage.ts";
import {Status} from "./Status.ts";
import {Client} from "./Client.ts";

export interface Order {
  number?: number,
  client: number | Client,
  dateTime?: Date,
  servicePackage?: OrderServicePackage
  services?: OrderService[]
  status?: Status,
  totalPrice?: number
}
