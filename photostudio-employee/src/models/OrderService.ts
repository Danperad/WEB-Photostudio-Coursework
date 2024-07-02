import {Service} from "./Service.ts";
import {Employee} from "./Employee.ts";
import {Item} from "./Item.ts";
import {Hall} from "./Hall.ts";
import {Status} from "./Status.ts";
import {Client} from "./Client.ts";

interface OrderService {
  id?: number,
  client?: Client | number,
  service: Service | number,
  employee?: Employee | number,
  item?: Item | number,
  count?: number,
  hall?: Hall | number,
  startDateTime?: Date,
  duration?: number,
  isFullTime?: boolean,
  status?: Status
  cost?: number
}

export type {OrderService}
