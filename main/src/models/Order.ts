import {Status} from "./Status.ts";
import {OrderService} from "./OrderService.ts";
import {ServicePackage} from "./Models.ts";

type Order = {
    number: number;
    dateTime: Date;
    status: Status;
    totalPrice: number
    services: OrderService[]
    servicePackage?: ServicePackage
}
export type {Order}