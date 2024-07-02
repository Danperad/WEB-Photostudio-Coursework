import {OrderService, Service} from "@models/*";

type NewServiceProps = {
  service: Service,
  startTime: Date,
  duration: number,
  isAllowSelect: () => boolean,
  onComplete: (newService: OrderService) => void
}

export type {NewServiceProps}
