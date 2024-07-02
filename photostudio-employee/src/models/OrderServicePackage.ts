import {ServicePackage} from "./ServicePackage.ts";

interface OrderServicePackage {
  servicePackage: ServicePackage | number
  startDateTime: Date
}

export type {OrderServicePackage}
